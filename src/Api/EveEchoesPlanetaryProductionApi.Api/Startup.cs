namespace EveEchoesPlanetaryProductionApi.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using EveEchoesPlanetaryProductionApi.Api.Infrastructure.Extensions;
    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Data;
    using EveEchoesPlanetaryProductionApi.Data.Common;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Data.Seeding;
    using EveEchoesPlanetaryProductionApi.Services;
    using EveEchoesPlanetaryProductionApi.Services.Data;
    using EveEchoesPlanetaryProductionApi.Services.EveEchoesMarket;
    using EveEchoesPlanetaryProductionApi.Services.Messaging;
    using EveEchoesPlanetaryProductionApi.Services.Settings;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Newtonsoft.Json;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EveEchoesPlanetaryProductionApiDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services
                .AddIdentity<User, Role>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 0;

                    options.User.RequireUniqueEmail = true;

                    options.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<EveEchoesPlanetaryProductionApiDbContext>()
                .AddDefaultTokenProviders(); // just adds the default providers to generate tokens for a password reset, 2-factor authentication, change email, and change telephone.

            services.Configure<JwtSettings>(this.configuration.GetSection("Jwt"));
            services.AddAuth(this.configuration.GetSection("Jwt").Get<JwtSettings>());

            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = this.configuration.GetConnectionString("DefaultConnection");
                options.SchemaName = "dbo";
                options.TableName = "CacheItemsPrices";
            });

            services.AddMemoryCache();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            // Data
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();
            services.AddAutoMapper();

            // Application Services
            services.AddTransient<IPlanetsService, PlanetsService>();
            services.AddTransient<ISolarSystemsService, SolarSystemsService>();
            services.AddTransient<IItemsPricesService, ItemsPricesService>();
            services.AddTransient<IItemsService, ItemsService>();
            services.AddTransient<IPlanetaryResourcesService, PlanetaryResourcesService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IRegionsService, RegionsService>();
            services.AddTransient<IConstellationService, ConstellationService>();

            // SendGrid
            var sendGridKey = this.configuration.GetSection("SendGrid:ApiKey").Value;
            services.AddTransient<IEmailSender>(x => new SendGridEmailSender(sendGridKey));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Seed static data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<EveEchoesPlanetaryProductionApiDbContext>();
                dbContext.Database.Migrate();

                new EveEchoesPlanetaryProductionApiDbContextSeeder()
                    .SeedAsync(dbContext, serviceScope.ServiceProvider)
                    .GetAwaiter()
                    .GetResult();
            }

            // app.Use(async (context, next) =>
            // {
            //    if (!context.Connection.LocalIpAddress.Equals(context.Connection.RemoteIpAddress))
            //    {
            //        var errors = new List<ApiErrorModel>()
            //        {
            //            new ApiErrorModel() { Code = "Unauthorized", Description = "Unauthorized" },
            //        };

            // context.Response.StatusCode = (int)HttpStatusCode.OK;
            //        context.Response.ContentType = GlobalConstants.JsonContentType;
            //        await context.Response.WriteAsync(JsonConvert.SerializeObject(errors));

            // return;
            //    }

            // await next.Invoke();
            // });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            // Global Error Handling
            app.UseExceptionHandler(
                alternativeApp =>
                {
                    alternativeApp.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            context.Response.ContentType = GlobalConstants.JsonContentType;
                            var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                            if (exceptionHandlerFeature?.Error != null)
                            {
                                var ex = exceptionHandlerFeature.Error;
                                while (ex is AggregateException aggregateException
                                       && aggregateException.InnerExceptions.Any())
                                {
                                    ex = aggregateException.InnerExceptions.First();
                                }

                                // TODO: Log it
                                var exceptionMessage = ex.Message;
                                if (env.IsDevelopment())
                                {
                                    exceptionMessage = ex.ToString();
                                }

                                var errors = new List<ApiErrorModel>()
                                {
                                    new ApiErrorModel() { Code = "GLOBAL", Description = exceptionMessage },
                                };

                                await context.Response
                                    .WriteAsync(JsonConvert.SerializeObject(errors))
                                    .ConfigureAwait(continueOnCapturedContext: false);
                            }
                        });
                });

            app.UseRouting();

            app.UseAuth();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}

namespace EveEchoesPlanetaryProductionApi.Api
{
    using EveEchoesPlanetaryProductionApi.Api.Infrastructure.Extensions;
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
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

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

            /* services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EveEchoesPlanetaryProductionApi.Api", Version = "v1" });
            });*/

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

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();

                // app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EveEchoesPlanetaryProductionApi.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

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

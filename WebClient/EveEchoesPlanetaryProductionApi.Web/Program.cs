namespace EveEchoesPlanetaryProductionApi.Web
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Web.Services;

    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddSingleton<IAlertService, AlertService>();
            builder.Services.AddSingleton<IAppDataService, AppDataService>();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<ILocalStorageService, LocalStorageService>();
            builder.Services.AddTransient<IHttpService, HttpService>();
            builder.Services.AddTransient<IEveApiService, EveApiService>();
            builder.Services.AddTransient<ISolarSystemsProvider, SolarSystemsProvider>();
            builder.Services.AddTransient<IConstellationsProvider, ConstellationsProvider>();
            builder.Services.AddTransient<IRegionsProvider, RegionsProvider>();
            builder.Services.AddTransient<IPlanetaryResourcesProvider, PlanetaryResourcesProvider>();

            var host = builder.Build();

            var accountService = host.Services.GetRequiredService<IAccountService>();
            await accountService.Initialize();

            await host.RunAsync();
        }
    }
}

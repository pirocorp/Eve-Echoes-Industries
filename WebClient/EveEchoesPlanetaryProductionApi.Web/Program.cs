namespace EveEchoesPlanetaryProductionApi.Web
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class Program
    {
        // TODO: Add Stylecop to Web Client projects

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<IAppDataService, AppDataService>();
            builder.Services.AddTransient<IEveApiService, EveApiService>();

            await builder.Build().RunAsync();
        }
    }
}

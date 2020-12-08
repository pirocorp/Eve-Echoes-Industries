namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Api.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById;

    public interface IEveApiService
    {
        Task<int> GetRegionsCountAsync();

        Task<int> GetConstellationsCountAsync();

        Task<int> GetSolarSystemsCountAsync();

        Task<SolarSystemServiceModel> GetRandomSolarSystemAsync();

        Task<SolarSystemServiceModel> GetSolarSystemAsync(long solarSystemId);
    }

    public class EveApiService : IEveApiService
    {
        private readonly HttpClient httpClient;

        public EveApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<int> GetRegionsCountAsync()
            => (await this.httpClient.GetFromJsonAsync<CountModel>("api/regions/count"))?.Count ?? 0;

        public async Task<int> GetConstellationsCountAsync()
            => (await this.httpClient.GetFromJsonAsync<CountModel>("api/constellations/count"))?.Count ?? 0;

        public async Task<int> GetSolarSystemsCountAsync()
            => (await this.httpClient.GetFromJsonAsync<CountModel>("api/solarSystems/count"))?.Count ?? 0;

        public async Task<SolarSystemServiceModel> GetRandomSolarSystemAsync()
            => await this.httpClient.GetFromJsonAsync<SolarSystemServiceModel>("api/SolarSystems");

        public async Task<SolarSystemServiceModel> GetSolarSystemAsync(long solarSystemId)
            => await this.httpClient.GetFromJsonAsync<SolarSystemServiceModel>($"api/SolarSystems/{solarSystemId}");
    }
}

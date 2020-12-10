namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSystems;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.Search;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById;

    public class SolarSystemsProvider : ISolarSystemsProvider
        {
            private readonly HttpClient httpClient;

            public SolarSystemsProvider(HttpClient httpClient)
            {
                this.httpClient = httpClient;
            }

            public async Task<int> GetCountAsync()
                => (await this.httpClient.GetFromJsonAsync<CountModel>("api/solarSystems/count"))?.Count ?? 0;

            public async Task<IEnumerable<SolarSystemListingModel>> GetPageAsync(int page = 1)
                => (await this.httpClient.GetFromJsonAsync<SystemsPageModel>($"api/solarSystems/page/{page}"))?.Systems;

            public async Task<SolarSystemServiceModel> GetRandomAsync()
                => await this.httpClient.GetFromJsonAsync<SolarSystemServiceModel>("api/SolarSystems");

            public async Task<SolarSystemServiceModel> GetAsync(long solarSystemId)
                => await this.httpClient.GetFromJsonAsync<SolarSystemServiceModel>($"api/SolarSystems/{solarSystemId}");

            public async Task<SearchResultModel> GetSearchResultsAsync(string searchTerm, int page = 1)
                => await this.httpClient.GetFromJsonAsync<SearchResultModel>($"api/solarSystems/search/{searchTerm}/{page}");
        }
}

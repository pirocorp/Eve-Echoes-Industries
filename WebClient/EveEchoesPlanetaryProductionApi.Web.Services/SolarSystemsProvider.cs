namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Api.Models.SolarSystems.GetBestSystemsInRegion;
    using Api.Models.SolarSystems.GetSimpleDetails;
    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSystems;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.Search;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystemServiceModel;

    public class SolarSystemsProvider : ISolarSystemsProvider
    {
        private readonly HttpClient httpClient;

        public SolarSystemsProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<int> GetCountAsync()
            => (await this.httpClient.GetFromJsonAsync<CountModel>("api/systems/count"))?.Count ?? 0;

        public async Task<SolarSystemServiceModel> GetRandomAsync()
            => await this.httpClient.GetFromJsonAsync<SolarSystemServiceModel>("api/systems/random");

        public async Task<SolarSystemServiceModel> GetAsync(long systemId)
            => await this.httpClient.GetFromJsonAsync<SolarSystemServiceModel>($"api/systems/{systemId}");

        public async Task<SolarSystemSimpleDetailsModel> GetSolarSystemSimpleDetails(long systemId)
            => await this.httpClient.GetFromJsonAsync<SolarSystemSimpleDetailsModel>($"/api/systems/{systemId}/short");

        public async Task<IEnumerable<SolarSystemListingModel>> GetPageAsync(int page = 1)
            => (await this.httpClient.GetFromJsonAsync<SystemsPageModel>($"api/systems/page/{page}"))?.Systems;
        
        public async Task<SearchResultModel> GetSearchResultsAsync(string searchTerm, int page = 1)
            => await this.httpClient.GetFromJsonAsync<SearchResultModel>($"api/systems/search/{searchTerm}/page/{page}");

        public async Task<BestRegionModel> GetBestSystemsInRange(int range, long systemId, InputModel model)
        {
            var result =  await this.httpClient.PostAsJsonAsync($"api/systems/{systemId}/range/{range}", model);

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await result.Content.ReadFromJsonAsync<BestRegionModel>();
        }
    }
}

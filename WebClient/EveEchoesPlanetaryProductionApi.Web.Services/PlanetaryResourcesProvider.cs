namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Api.Models.PlanetaryResources;
    using EveEchoesPlanetaryProductionApi.Api.Models.PlanetaryResources.GetAllPlanetResourcesWithPrices;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;

    public class PlanetaryResourcesProvider : IPlanetaryResourcesProvider
    {
        private readonly HttpClient httpClient;

        public PlanetaryResourcesProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<string>> GetPlanetaryResources()
            => await this.httpClient.GetFromJsonAsync<IEnumerable<string>>("api/resources/simple/all");

        public async Task<int> GetPlanetaryResourcesCount()
            => (await this.GetPlanetaryResources()).Count();

        public async Task<IEnumerable<PlanetaryResource>> GetPlanetaryResourcesCurrentPrices()
            => (await this.httpClient.GetFromJsonAsync<GetAllPlanetResourcesWithPricesModel>("api/resources/all"))?.Resources;

        public async Task<BestPlanetaryResourcesModel> GetBestResourcesInConstellation(long constellationId, BestInputModel model)
        {
            var result =  await this.httpClient.PostAsJsonAsync($"api/resources/constellation/{constellationId}", model);

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await result.Content.ReadFromJsonAsync<BestPlanetaryResourcesModel>();
        }

        public async Task<BestPlanetaryResourcesModel> GetBestResourcesInRegion(long regionId, BestInputModel model)
        {
            var result =  await this.httpClient.PostAsJsonAsync($"api/resources/region/{regionId}", model);

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await result.Content.ReadFromJsonAsync<BestPlanetaryResourcesModel>();
        }

        public async Task<BestPlanetaryResourcesModel> GetBestResourcesInRange(int range, long solarSystemId, BestInputModel model)
        {
            var result =  await this.httpClient.PostAsJsonAsync($"api/resources/{range}/{solarSystemId}", model);

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await result.Content.ReadFromJsonAsync<BestPlanetaryResourcesModel>();
        }
    }
}
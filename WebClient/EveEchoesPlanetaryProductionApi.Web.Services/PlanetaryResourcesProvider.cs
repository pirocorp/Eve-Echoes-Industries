namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.PlanetaryResources.GetAllPlanetResourcesWithPrices;

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
    }
}
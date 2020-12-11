namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class PlanetaryResourcesProvider : IPlanetaryResourcesProvider
    {
        private readonly HttpClient httpClient;

        public PlanetaryResourcesProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<string>> GetPlanetaryResources()
            => await this.httpClient.GetFromJsonAsync<IEnumerable<string>>("api/resources/all");
    }
}
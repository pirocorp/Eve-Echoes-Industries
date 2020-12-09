namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Api.Models;
    using Api.Models.Regions;

    public class RegionsProvider : IRegionsProvider
    {
        private readonly HttpClient httpClient;

        public RegionsProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<int> GetCountAsync()
            => (await this.httpClient.GetFromJsonAsync<CountModel>("api/regions/count"))?.Count ?? 0;

        public async Task<IEnumerable<RegionListingModel>> GetPageAsync(int page = 1)
            => (await this.httpClient.GetFromJsonAsync<RegionsPage>($"api/regions/{page}"))?.Regions;
    }
}
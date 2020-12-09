namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Api.Models;
    using Api.Models.Constellations.GetConstellations;
    using Api.Models.Constellations.GetDetails;

    public class ConstellationsProvider : IConstellationsProvider
    {
        private readonly HttpClient httpClient;

        public ConstellationsProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<int> GetCountAsync()
            => (await this.httpClient.GetFromJsonAsync<CountModel>("api/constellations/count"))?.Count ?? 0;

        public async Task<IEnumerable<ConstellationListingModel>> GetPageAsync(int page = 1)
            => (await this.httpClient.GetFromJsonAsync<ConstellationPage>($"api/constellations/{page}"))?.Constellations;

        public async Task<ConstellationDetails> GetDetailsAsync(long id)
            => await this.httpClient.GetFromJsonAsync<ConstellationDetails>($"api/constellation/{id}");
    }
}
namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetConstellations;
    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetDetails;
    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetSimpleDetails;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetBestSystemsInConstellation;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;

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
            => (await this.httpClient.GetFromJsonAsync<ConstellationPage>($"api/constellations/page/{page}"))?.Constellations;

        public async Task<ConstellationDetails> GetDetailsAsync(long id)
            => await this.httpClient.GetFromJsonAsync<ConstellationDetails>($"api/constellations/{id}");

        public async Task<ConstellationSimpleDetailsModel> GetSimpleDetailsAsync(long constellationId)
            => await this.httpClient.GetFromJsonAsync<ConstellationSimpleDetailsModel>($"api/constellations/{constellationId}/short");

        public async Task<BestConstellationModel> GetBestSystemsInConstellation(long constellationId, InputModel model)
        {
            var result = await this.httpClient.PostAsJsonAsync($"api/systems/constellations/{constellationId}", model);

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await result.Content.ReadFromJsonAsync<BestConstellationModel>();
        }
    }
}

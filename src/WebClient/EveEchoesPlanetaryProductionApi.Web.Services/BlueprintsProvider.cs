namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Blueprints;
    using EveEchoesPlanetaryProductionApi.Api.Models.Blueprints.GetBlueprint;

    public class BlueprintsProvider : IBlueprintsProvider
    {
        private readonly HttpClient httpClient;

        public BlueprintsProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<BlueprintTypeModel>> GetBlueprintTypesAsync()
            => await this.httpClient.GetFromJsonAsync<IEnumerable<BlueprintTypeModel>>("api/blueprints/types");

        public async Task<GetBlueprintModel> GetBlueprintAsync(string blueprintId)
            => await this.httpClient.GetFromJsonAsync<GetBlueprintModel>($"api/blueprints/{blueprintId}");

        public async Task<BrowseBlueprintsModel> BrowseBlueprints(BrowseBlueprintsInputModel model, int page = 1)
        {
            var result = await this.httpClient.PostAsJsonAsync($"api/blueprints/page/{page}", model);

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await result.Content.ReadFromJsonAsync<BrowseBlueprintsModel>();
        }

        public async Task<BrowseBlueprintsModel> SearchBlueprintsAsync(BrowseBlueprintsInputModel model, string searchTerm, int page = 1)
        {
            var result = await this.httpClient.PostAsJsonAsync($"api/blueprints/search/{searchTerm}/page/{page}", model);

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await result.Content.ReadFromJsonAsync<BrowseBlueprintsModel>();
        }
    }
}

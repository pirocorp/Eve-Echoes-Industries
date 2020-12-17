namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Api.Models;
    using Api.Models.Regions.GetDetails;
    using Api.Models.Regions.GetRegions;
    using Api.Models.Regions.GetSimpleDetails;
    using Api.Models.SolarSystems.GetBestSystemsInRegion;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;

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
            => (await this.httpClient.GetFromJsonAsync<RegionsPage>($"api/regions/page/{page}"))?.Regions;

        public async Task<RegionDetails> GetDetailsAsync(long regionId)
            => await this.httpClient.GetFromJsonAsync<RegionDetails>($"api/regions/{regionId}");

        public async Task<RegionSimpleDetailsModel> GetSimpleDetailsAsync(long regionId)
            => await this.httpClient.GetFromJsonAsync<RegionSimpleDetailsModel>($"/api/regions/{regionId}/short");

        public async Task<BestRegionModel> GetBestSystemsInRegion(long regionId, InputModel model)
        {
            var result =  await this.httpClient.PostAsJsonAsync($"api/systems/regions/{regionId}", model);

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await result.Content.ReadFromJsonAsync<BestRegionModel>();
        }
    }
}

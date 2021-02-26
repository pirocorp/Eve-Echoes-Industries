namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Locations;

    public class LocationProvider : ILocationProvider
    {
        private readonly HttpClient httpClient;

        public LocationProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<LocationModel> GetRandomAsync()
            => await this.httpClient.GetFromJsonAsync<LocationModel>("api/locations/random");

        public async Task<LocationModel> GetLocationAsync(long systemId)
            => await this.httpClient.GetFromJsonAsync<LocationModel>($"api/locations/{systemId}");
    }
}

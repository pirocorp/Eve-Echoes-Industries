namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Locations;
    using EveEchoesPlanetaryProductionApi.Services.Data;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ISolarSystemsService solarSystemService;

        public LocationsController(ISolarSystemsService solarSystemService)
        {
            this.solarSystemService = solarSystemService;
        }

        [Route("~/api/locations/random")]
        public async Task<LocationModel> GetRandomLocation()
            => await this.solarSystemService.GetRandomSystemAsync<LocationModel>();

        [Route("~/api/locations/{systemId}")]
        public async Task<LocationModel> GetLocation(long systemId)
            => await this.solarSystemService.GetSystemAsync<LocationModel>(systemId);
    }
}

namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Services.Data;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.GetSolarSystemById;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class SolarSystemsController : ControllerBase
    {
        private readonly ISolarSystemService solarSystemService;

        public SolarSystemsController(
            ISolarSystemService solarSystemService)
        {
            this.solarSystemService = solarSystemService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SolarSystemServiceModel>> GetSolarSystem(long id)
        {
            var solarSystem = await this.solarSystemService.GetByIdAsync(id);

            if (solarSystem is null)
            {
                return this.NotFound();
            }

            return solarSystem;
        }
    }
}

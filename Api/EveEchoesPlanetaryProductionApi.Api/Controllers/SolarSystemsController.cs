namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems;
    using EveEchoesPlanetaryProductionApi.Services.Data;
    using EveEchoesPlanetaryProductionApi.Services.EveEchoesMarket;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class SolarSystemsController : ControllerBase
    {
        private readonly IItemsPricesService itemsPricesService;
        private readonly ISolarSystemService solarSystemService;

        public SolarSystemsController(
            IItemsPricesService itemsPricesService,
            ISolarSystemService solarSystemService)
        {
            this.itemsPricesService = itemsPricesService;
            this.solarSystemService = solarSystemService;
        }

        [HttpGet("{parameter}")]
        public async Task<ActionResult<SolarSystemApiModel>> GetSolarSystem(string parameter)
        {
            var isId = long.TryParse(parameter, out var id);

            SolarSystemApiModel solarSystem;

            if (isId)
            {
                solarSystem = await this.solarSystemService.GetById<SolarSystemApiModel>(id);
            }
            else
            {
                solarSystem = await this.solarSystemService.GetByName<SolarSystemApiModel>(parameter);
            }

            if (solarSystem is null)
            {
                return this.NotFound();
            }

            var itemPrices = await this.itemsPricesService.GetHistoricalPricesForItemById(11707060002);

            return solarSystem;
        }
    }
}

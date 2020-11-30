namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems;
    using EveEchoesPlanetaryProductionApi.Services.Data;
    using EveEchoesPlanetaryProductionApi.Services.EveEchoesMarket;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class SolarSystemsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IItemsPricesService itemsPricesService;
        private readonly ISolarSystemService solarSystemService;

        public SolarSystemsController(
            IMapper mapper,
            IItemsPricesService itemsPricesService,
            ISolarSystemService solarSystemService)
        {
            this.mapper = mapper;
            this.itemsPricesService = itemsPricesService;
            this.solarSystemService = solarSystemService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SolarSystemApiModel>> GetSolarSystem(long id)
        {
            var solarSystem = await this.solarSystemService.GetById<SolarSystemApiModel>(id);

            if (solarSystem is null)
            {
                return this.NotFound();
            }

            foreach (var planet in solarSystem.Planets)
            {
                foreach (var resource in planet.PlanetResources)
                {
                    var price = await this.itemsPricesService.GetLatestPrice(resource.ItemId);
                    resource.Price = this.mapper.Map<SolarSystemPlanetPlanetResourcePriceModel>(price);
                }
            }

            return solarSystem;
        }
    }
}

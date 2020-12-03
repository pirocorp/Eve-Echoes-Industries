namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetBestSolarSystemPlanetaryResourcesValues;
    using EveEchoesPlanetaryProductionApi.Services.Data;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetBestPlanetaryResourcesById;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class SolarSystemsController : ControllerBase
    {
        private readonly ISolarSystemsService solarSystemService;

        public SolarSystemsController(
            ISolarSystemsService solarSystemService)
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

        [HttpPost("{id}")]
        public async Task<ActionResult<SolarSystemBestModel>> GetBestSolarSystemPlanetaryResourcesValues(long id, [FromBody]GetBestSolarSystemPlanetaryResourcesValuesInputModel model)
        {
            var selectorIsParsedSuccessful = Enum.TryParse<PriceSelector>(model.PriceSelector, out var priceSelector);

            if (!selectorIsParsedSuccessful)
            {
                return this.BadRequest();
            }

            var sol = await this.solarSystemService.GetBestPlanetaryResourcesByIdAsync(id, priceSelector);

            if (sol is null)
            {
                return this.NotFound();
            }

            sol.MiningPlanets = model.MiningPlanets;

            return sol;
        }

        [HttpPost]
        [Route("~/api/solarSystems/{range}/{id}")]
        public async Task<ActionResult<SolarSystemBestModel>> GetBestSolarSystemPlanetaryResourcesValues(long id, int range, [FromBody]GetBestSolarSystemPlanetaryResourcesValuesInputModel model)
        {
            var selectorIsParsedSuccessful = Enum.TryParse<PriceSelector>(model.PriceSelector, out var priceSelector);

            if (!selectorIsParsedSuccessful)
            {
                return this.BadRequest();
            }

            var sol = await this.solarSystemService.GetBestPlanetaryResourcesInRangeAsync(id, priceSelector, range, model.MiningPlanets);

            if (sol is null)
            {
                return this.NotFound();
            }

            return sol;
        }
    }
}

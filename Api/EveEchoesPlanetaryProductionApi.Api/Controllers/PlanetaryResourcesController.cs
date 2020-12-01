namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Items.GetBestPlanetaryResourcesInRange;
    using EveEchoesPlanetaryProductionApi.Services.Data;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.PlanetaryResources;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class PlanetaryResourcesController : ControllerBase
    {
        private readonly IPlanetaryResourcesService planetaryResourcesService;

        public PlanetaryResourcesController(IPlanetaryResourcesService planetaryResourcesService)
        {
            this.planetaryResourcesService = planetaryResourcesService;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<IEnumerable<PlanetaryResourceServiceModel>>> GetBestPlanetaryResourcesInRange(long id, [FromBody]GetBestPlanetaryResourcesInRangeInputModel model)
        {
            var selectorIsParsedSuccessful = Enum.TryParse<PriceSelector>(model.PriceSelector, out var priceSelector);

            if (!selectorIsParsedSuccessful)
            {
                return this.BadRequest();
            }

            var resources =
                await this.planetaryResourcesService.GetBestPlanetaryResourcesInRangeAsync(id, priceSelector, model.Range, model.MiningPlanets);

            return this.Ok(resources);
        }
    }
}

namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Items.GetBestPlanetaryResourcesInRange;
    using EveEchoesPlanetaryProductionApi.Api.Models.PlanetaryResources.GetAllPlanetResourcesWithPrices;
    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Services.Data;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.PlanetaryResources;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class PlanetaryResourcesController : ControllerBase
    {
        private readonly IPlanetaryResourcesService planetaryResourcesService;
        private readonly IItemsService itemsService;

        public PlanetaryResourcesController(
            IPlanetaryResourcesService planetaryResourcesService,
            IItemsService itemsService)
        {
            this.planetaryResourcesService = planetaryResourcesService;
            this.itemsService = itemsService;
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

        [Route("~/api/resources/simple/all")]
        public async Task<IEnumerable<string>> GetAllPlanetResources()
            => await this.planetaryResourcesService.GetAllPlanetaryResources();

        [Route("~/api/resources/all")]
        public async Task<ActionResult<GetAllPlanetResourcesWithPricesModel>> GetAllPlanetResourcesWithPrices()
        {
            var resources = (await this.planetaryResourcesService
                .GetAllPlanetaryResources<PlanetaryResource>())
                .ToList();

            var prices = await this.itemsService
                .GetLatestItemsPricesAsync(GlobalConstants.Items.GetPlanetaryResourcesIds());

            foreach (var resource in resources)
            {
                resource.Price = prices[resource.Id];
            }

            var model = new GetAllPlanetResourcesWithPricesModel()
            {
                Resources = resources,
            };

            return model;
        }
    }
}

namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Items.GetBestPlanetaryResourcesInRange;
    using EveEchoesPlanetaryProductionApi.Api.Models.PlanetaryResources;
    using EveEchoesPlanetaryProductionApi.Api.Models.PlanetaryResources.GetAllPlanetResourcesWithPrices;
    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Services.Data;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.PlanetaryResources;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [ApiController]
    [Route("api/[controller]")]
    public class PlanetaryResourcesController : ControllerBase
    {
        private readonly IPlanetaryResourcesService planetaryResourcesService;
        private readonly IItemsService itemsService;
        private readonly IOptions<ApiBehaviorOptions> apiBehaviorOptions;

        public PlanetaryResourcesController(
            IPlanetaryResourcesService planetaryResourcesService,
            IItemsService itemsService,
            IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            this.planetaryResourcesService = planetaryResourcesService;
            this.itemsService = itemsService;
            this.apiBehaviorOptions = apiBehaviorOptions;
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

        [Route("~/api/resources/constellation/{constellationId:long}")]
        public async Task<IActionResult> BestPlanetaryResourcesInConstellation(long constellationId, BestInputModel input)
        {
            var priceSelectorSuccess = Enum.TryParse<PriceSelector>(input.Price, out var priceSelector);

            if (!priceSelectorSuccess)
            {
                this.ModelState.AddModelError(nameof(BestInputModel.Price), "Invalid price selector");
                return this.apiBehaviorOptions.Value.InvalidModelStateResponseFactory(this.ControllerContext);
            }

            if (priceSelector is PriceSelector.UserProvided && input.Prices is null)
            {
                this.ModelState.AddModelError(nameof(BestInputModel.Prices), "User prices are not provided");
                return this.apiBehaviorOptions.Value.InvalidModelStateResponseFactory(this.ControllerContext);
            }

            input.PriceSelector = priceSelector;

            var (count, resources) = await this.planetaryResourcesService
                .GetBestResourcesInConstellation(constellationId, input);

            var model = new BestPlanetaryResourcesModel()
            {
                Count = count,
                Resources = resources,
            };

            return this.Ok(model);
        }

        [Route("~/api/resources/region/{regionId:long}")]
        public async Task<IActionResult> BestPlanetaryResourcesInRegion(long regionId, BestInputModel input)
        {
            var priceSelectorSuccess = Enum.TryParse<PriceSelector>(input.Price, out var priceSelector);

            if (!priceSelectorSuccess)
            {
                this.ModelState.AddModelError(nameof(BestInputModel.Price), "Invalid price selector");
                return this.apiBehaviorOptions.Value.InvalidModelStateResponseFactory(this.ControllerContext);
            }

            if (priceSelector is PriceSelector.UserProvided && input.Prices is null)
            {
                this.ModelState.AddModelError(nameof(BestInputModel.Prices), "User prices are not provided");
                return this.apiBehaviorOptions.Value.InvalidModelStateResponseFactory(this.ControllerContext);
            }

            input.PriceSelector = priceSelector;

            var (count, resources) = await this.planetaryResourcesService
                .GetBestResourcesInRegion(regionId, input);

            var model = new BestPlanetaryResourcesModel()
            {
                Count = count,
                Resources = resources,
            };

            return this.Ok(model);
        }
    }
}

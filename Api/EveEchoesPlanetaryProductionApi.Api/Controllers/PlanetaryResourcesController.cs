namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.PlanetaryResources;
    using EveEchoesPlanetaryProductionApi.Api.Models.PlanetaryResources.GetAllPlanetResourcesWithPrices;
    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Services.Data;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetBestPlanetaryResourcesById;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [ApiController]
    public class PlanetaryResourcesController : ControllerBase
    {
        private readonly IPlanetaryResourcesService planetaryResourcesService;
        private readonly IItemsService itemsService;
        private readonly ISolarSystemsService solarSystemsService;
        private readonly IOptions<ApiBehaviorOptions> apiBehaviorOptions;

        public PlanetaryResourcesController(
            IPlanetaryResourcesService planetaryResourcesService,
            IItemsService itemsService,
            ISolarSystemsService solarSystemsService,
            IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            this.planetaryResourcesService = planetaryResourcesService;
            this.itemsService = itemsService;
            this.solarSystemsService = solarSystemsService;
            this.apiBehaviorOptions = apiBehaviorOptions;
        }

        [Route("~/api/resources/list")]
        public async Task<IEnumerable<string>> GetResourcesList()
            => await this.planetaryResourcesService.GetAllPlanetaryResources();

        [Route("~/api/resources")]
        public async Task<ActionResult<GetAllPlanetResourcesWithPricesModel>> GetAllResources()
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

        [HttpPost]
        [Route("~/api/resources/constellations/{constellationId:long}")]
        public async Task<IActionResult> BestResourcesInConstellation(long constellationId, BestInputModel input)
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

        [HttpPost]
        [Route("~/api/resources/regions/{regionId:long}")]
        public async Task<IActionResult> BestResourcesInRegion(long regionId, BestInputModel input)
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

        [HttpPost]
        [Route("~/api/resources/systems/{systemId}")]
        public async Task<ActionResult<SolarSystemBestModel>> GetSystemBestResources(long systemId, [FromBody]BestInputModel model)
        {
            var selectorIsParsedSuccessful = Enum.TryParse<PriceSelector>(model.Price, out var priceSelector);

            if (!selectorIsParsedSuccessful)
            {
                return this.BadRequest();
            }

            var sol = await this.solarSystemsService.GetResourcesInSystemByIdAsync(systemId, priceSelector);

            if (sol is null)
            {
                return this.NotFound();
            }

            sol.MiningPlanets = model.MiningPlanets;

            return sol;
        }

        [HttpPost]
        [Route("~/api/resources/systems/{systemId}/range/{range}")]
        public async Task<IActionResult> BestResourcesInRegion(int range, long systemId, BestInputModel input)
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
                .GetBestResourcesInRange(range, systemId, input);

            var model = new BestPlanetaryResourcesModel()
            {
                Count = count,
                Resources = resources,
            };

            return this.Ok(model);
        }
    }
}

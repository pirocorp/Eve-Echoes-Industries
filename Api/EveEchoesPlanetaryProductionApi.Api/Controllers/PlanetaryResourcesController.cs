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
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [ApiController]
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

        [Route("~/api/resources/list")]
        public async Task<IEnumerable<string>> GetResourcesList()
            => await this.planetaryResourcesService.GetResourcesListAsync();

        [Route("~/api/resources")]
        public async Task<ActionResult<GetAllPlanetResourcesWithPricesModel>> GetResources()
        {
            var resources = (await this.planetaryResourcesService
                .GetResourcesAsync<PlanetaryResource>())
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
        public async Task<IActionResult> BestResourcesInConstellation(long constellationId, InputModel input)
        {
            var priceSelectorSuccess = Enum.TryParse<PriceSelector>(input.Price, out var priceSelector);

            if (!priceSelectorSuccess)
            {
                this.ModelState.AddModelError(nameof(InputModel.Price), "Invalid price selector");
                return this.apiBehaviorOptions.Value.InvalidModelStateResponseFactory(this.ControllerContext);
            }

            if (priceSelector is PriceSelector.UserProvided && input.Prices is null)
            {
                this.ModelState.AddModelError(nameof(InputModel.Prices), "User prices are not provided");
                return this.apiBehaviorOptions.Value.InvalidModelStateResponseFactory(this.ControllerContext);
            }

            input.PriceSelector = priceSelector;

            var (count, resources) = await this.planetaryResourcesService
                .GetBestResourcesInConstellationAsync(constellationId, input);

            var model = new BestPlanetaryResourcesModel()
            {
                Count = count,
                Resources = resources,
            };

            return this.Ok(model);
        }

        [HttpPost]
        [Route("~/api/resources/regions/{regionId:long}")]
        public async Task<IActionResult> BestResourcesInRegion(long regionId, InputModel input)
        {
            var priceSelectorSuccess = Enum.TryParse<PriceSelector>(input.Price, out var priceSelector);

            if (!priceSelectorSuccess)
            {
                this.ModelState.AddModelError(nameof(InputModel.Price), "Invalid price selector");
                return this.apiBehaviorOptions.Value.InvalidModelStateResponseFactory(this.ControllerContext);
            }

            if (priceSelector is PriceSelector.UserProvided && input.Prices is null)
            {
                this.ModelState.AddModelError(nameof(InputModel.Prices), "User prices are not provided");
                return this.apiBehaviorOptions.Value.InvalidModelStateResponseFactory(this.ControllerContext);
            }

            input.PriceSelector = priceSelector;

            var (count, resources) = await this.planetaryResourcesService
                .GetBestResourcesInRegionAsync(regionId, input);

            var model = new BestPlanetaryResourcesModel()
            {
                Count = count,
                Resources = resources,
            };

            return this.Ok(model);
        }

        [HttpPost]
        [Route("~/api/resources/systems/{systemId}/range/{range}")]
        public async Task<IActionResult> BestResourcesInRange(int range, long systemId, InputModel input)
        {
            var priceSelectorSuccess = Enum.TryParse<PriceSelector>(input.Price, out var priceSelector);

            if (!priceSelectorSuccess)
            {
                this.ModelState.AddModelError(nameof(InputModel.Price), "Invalid price selector");
                return this.apiBehaviorOptions.Value.InvalidModelStateResponseFactory(this.ControllerContext);
            }

            if (priceSelector is PriceSelector.UserProvided && input.Prices is null)
            {
                this.ModelState.AddModelError(nameof(InputModel.Prices), "User prices are not provided");
                return this.apiBehaviorOptions.Value.InvalidModelStateResponseFactory(this.ControllerContext);
            }

            input.PriceSelector = priceSelector;

            var (count, resources) = await this.planetaryResourcesService
                .GetBestResourcesInRangeAsync(range, systemId, input);

            var model = new BestPlanetaryResourcesModel()
            {
                Count = count,
                Resources = resources,
            };

            return this.Ok(model);
        }
    }
}

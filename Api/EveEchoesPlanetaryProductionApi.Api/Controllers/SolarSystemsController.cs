namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Api.Models.BestSystemModel;
    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations.BestSolarSystemsInConstellation;
    using EveEchoesPlanetaryProductionApi.Api.Models.Regions.GetBestSolarSystemsInRegionAsync;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetBestSystemInRange;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSolarSystemSimpleDetails;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSystems;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.Search;
    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Services.Data;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [ApiController]
    public class SolarSystemsController : ControllerBase
    {
        private readonly ISolarSystemsService solarSystemService;
        private readonly IConstellationService constellationService;
        private readonly IRegionsService regionsService;
        private readonly IOptions<ApiBehaviorOptions> apiBehaviorOptions;

        public SolarSystemsController(
            ISolarSystemsService solarSystemService,
            IConstellationService constellationService,
            IRegionsService regionsService,
            IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            this.solarSystemService = solarSystemService;
            this.constellationService = constellationService;
            this.regionsService = regionsService;
            this.apiBehaviorOptions = apiBehaviorOptions;
        }

        [Route("~/api/systems/count")]
        public async Task<CountModel> GetSystemsCount()
        {
            var model = new CountModel()
            {
                Count = await this.solarSystemService.GetSolarSystemsCount(),
            };

            return model;
        }

        [Route("~/api/systems/random")]
        public async Task<ActionResult<SolarSystemServiceModel>> GetRandomSystem()
            => await this.solarSystemService.GetRandomAsync();

        [Route("~/api/systems/{systemId}")]
        public async Task<ActionResult<SolarSystemServiceModel>> GetSystemDetails(long systemId)
        {
            var solarSystem = await this.solarSystemService.GetByIdAsync(systemId);

            if (solarSystem is null)
            {
                return this.NotFound();
            }

            return solarSystem;
        }

        [Route("~/api/systems/{systemId}/short")]
        public async Task<SolarSystemSimpleDetailsModel> GetSimpleSystemDetails(long systemId)
            => await this.solarSystemService.GetByIdAsync<SolarSystemSimpleDetailsModel>(systemId);

        [Route("~/api/systems/page/{page?}")]
        public async Task<ActionResult<SystemsPageModel>> GetSystems(int page = 1)
        {
            if (page <= 0)
            {
                return this.BadRequest();
            }

            var model = new SystemsPageModel()
            {
                Systems = await this.solarSystemService.GetAllAsync<SolarSystemListingModel>(GlobalConstants.Ui.SolarSystemsSearchPageSize, page),
            };

            return model;
        }

        [Route("~/api/systems/search/{searchTerm}/page/{page?}")]
        public async Task<ActionResult<SearchResultModel>> Search(string searchTerm, int page = 1)
        {
            var (results, count) = await this.solarSystemService
                .Search<SearchResultSolarSystemModel>(searchTerm, GlobalConstants.Ui.SolarSystemsSearchPageSize, page);

            var model = new SearchResultModel()
            {
                Results = results,
                Count = count,
            };

            return model;
        }

        [HttpPost]
        [Route("~/api/systems/{systemId}/range/{range}")]
        public async Task<IActionResult> GetBestSystemInRange(long systemId, int range, [FromBody]BestInputModel input)
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

            if (range > GlobalConstants.MaxRange)
            {
                range = GlobalConstants.MaxRange;
            }

            if (range < GlobalConstants.MinRange)
            {
                range = GlobalConstants.MinRange;
            }

            input.PriceSelector = priceSelector;

            var result = await this.solarSystemService.GetBestSolarSystemInRange<BestSystemModel>(systemId, range, input);

            var model = new BestRangeModel
            {
                Count = result.Count,
                Systems = result.Systems,
            };

            return this.Ok(model);
        }

        [HttpPost]
        [Route("~/api/systems/constellations/{constellationId}")]
        public async Task<IActionResult> GetBestSolarSystemsInConstellation(long constellationId, [FromBody]BestInputModel input)
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

            var model = new BestConstellationModel
            {
                Systems = await this.constellationService
                    .GetBestSolarSystem<BestSystemModel>(constellationId, input),
            };

            return this.Ok(model);
        }

        [HttpPost]
        [Route("~/api/systems/regions/{regionId}")]
        public async Task<IActionResult> GetBestSolarSystemsInRegionAsync(long regionId, [FromBody] BestInputModel input)
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

            var model = new BestRegionModel()
            {
                Count = await this.regionsService.GetSolarSystemsCountInRegionAsync(regionId),
                Systems = await this.regionsService.GetBestSolarSystemAsync<BestSystemModel>(regionId, input),
            };

            return this.Ok(model);
        }
    }
}

namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Api.Models.BestSystemModel;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetBestSystemInRange;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetBestSystemsInConstellation;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetBestSystemsInRegion;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSimpleDetails;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSystems;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.Search;
    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Services.Data;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystemServiceModel;
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
        public async Task<CountModel> GetCount()
        {
            var model = new CountModel()
            {
                Count = await this.solarSystemService.GetCountAsync(),
            };

            return model;
        }

        [Route("~/api/systems/random")]
        public async Task<ActionResult<SolarSystemServiceModel>> GetRandomSystem()
            => await this.solarSystemService.GetRandomSystemAsync();

        [Route("~/api/systems/{systemId}")]
        public async Task<ActionResult<SolarSystemServiceModel>> GetDetails(long systemId)
        {
            var solarSystem = await this.solarSystemService.GetSystemAsync(systemId);

            if (solarSystem is null)
            {
                return this.NotFound();
            }

            return solarSystem;
        }

        [Route("~/api/systems/{systemId}/short")]
        public async Task<SolarSystemSimpleDetailsModel> GetSimpleDetails(long systemId)
            => await this.solarSystemService.GetSystemAsync<SolarSystemSimpleDetailsModel>(systemId);

        [Route("~/api/systems/page/{page?}")]
        public async Task<IActionResult> GetSystems(int page = 1)
        {
            if (page <= 0)
            {
                return this.BadRequest();
            }

            var model = new SystemsPageModel()
            {
                Systems = await this.solarSystemService.GetSystemsAsync<SolarSystemListingModel>(GlobalConstants.Ui.SolarSystemsSearchPageSize, page),
            };

            return this.Ok(model);
        }

        [Route("~/api/systems/search/{searchTerm}/page/{page?}")]
        public async Task<IActionResult> Search(string searchTerm, int page = 1)
        {
            var (results, count) = await this.solarSystemService
                .SearchAsync<SearchResultSolarSystemModel>(searchTerm, GlobalConstants.Ui.SolarSystemsSearchPageSize, page);

            var model = new SearchResultModel()
            {
                Results = results,
                Count = count,
            };

            return this.Ok(model);
        }

        [HttpPost]
        [Route("~/api/systems/{systemId}/range/{range}")]
        public async Task<IActionResult> GetBestSystemInRange(long systemId, int range, [FromBody]InputModel input)
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

            if (range > GlobalConstants.MaxRange)
            {
                range = GlobalConstants.MaxRange;
            }

            if (range < GlobalConstants.MinRange)
            {
                range = GlobalConstants.MinRange;
            }

            input.PriceSelector = priceSelector;

            var result = await this.solarSystemService.GetBestSystemInRangeAsync<BestSystemModel>(systemId, range, input);

            var model = new BestRangeModel
            {
                Count = result.Count,
                Systems = result.Systems,
            };

            return this.Ok(model);
        }

        [HttpPost]
        [Route("~/api/systems/constellations/{constellationId}")]
        public async Task<IActionResult> GetBestSystemsInConstellation(long constellationId, [FromBody]InputModel input)
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

            var model = new BestConstellationModel
            {
                Systems = await this.constellationService
                    .GetBestSystemInConstellationAsync<BestSystemModel>(constellationId, input),
            };

            return this.Ok(model);
        }

        [HttpPost]
        [Route("~/api/systems/regions/{regionId}")]
        public async Task<IActionResult> GetBestSystemsInRegion(long regionId, [FromBody] InputModel input)
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

            var model = new BestRegionModel()
            {
                Count = await this.regionsService.GetSystemsCountInRegionAsync(regionId),
                Systems = await this.regionsService.GetBestSystemsInRegionAsync<BestSystemModel>(regionId, input),
            };

            return this.Ok(model);
        }
    }
}

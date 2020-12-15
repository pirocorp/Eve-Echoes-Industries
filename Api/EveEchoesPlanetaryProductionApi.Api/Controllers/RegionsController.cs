namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Api.Models.BestSystemModel;
    using EveEchoesPlanetaryProductionApi.Api.Models.Regions.GetBestSolarSystemsInRegionAsync;
    using EveEchoesPlanetaryProductionApi.Api.Models.Regions.GetDetails;
    using EveEchoesPlanetaryProductionApi.Api.Models.Regions.GetRegions;
    using EveEchoesPlanetaryProductionApi.Api.Models.Regions.GetSimpleDetails;
    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Services.Data;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    public class RegionsController : ControllerBase
    {
        private readonly IRegionsService regionsService;
        private readonly IOptions<ApiBehaviorOptions> apiBehaviorOptions;

        public RegionsController(
            IRegionsService regionsService,
            IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            this.regionsService = regionsService;
            this.apiBehaviorOptions = apiBehaviorOptions;
        }

        [Route("~/api/regions/count")]
        public async Task<ActionResult<CountModel>> GetRegionsCount()
        {
            var model = new CountModel()
            {
                Count = await this.regionsService.GetCountAsync(),
            };

            return model;
        }

        [Route("~/api/regions/{page?}")]
        public async Task<ActionResult<RegionsPage>> GetRegions(int page = 1)
        {
            if (page <= 0)
            {
                return this.BadRequest();
            }

            var model = new RegionsPage()
            {
                Regions = await this.regionsService.GetAllAsync<RegionListingModel>(GlobalConstants.Ui.ConstellationsPageSize, page),
            };

            return model;
        }

        [Route("~/api/region/simple/{regionId}")]
        public async Task<ActionResult<RegionSimpleDetailsModel>> GetSimpleDetails(long regionId)
            => await this.regionsService.GetByIdAsync<RegionSimpleDetailsModel>(regionId);

        [Route("~/api/region/{regionId}")]
        public async Task<ActionResult<RegionDetails>> GetDetails(long regionId)
            => await this.regionsService.GetByIdAsync<RegionDetails>(regionId);

        [HttpPost]
        [Route("~/api/solarSystems/best/region/{regionId}")]
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
                Systems = await this.regionsService.GetBestSolarSystemAsync<BestSystemModel>(regionId, input),
            };

            return this.Ok(model);
        }
    }
}

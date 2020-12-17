namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Api.Models.Regions.GetDetails;
    using EveEchoesPlanetaryProductionApi.Api.Models.Regions.GetRegions;
    using EveEchoesPlanetaryProductionApi.Api.Models.Regions.GetSimpleDetails;
    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Services.Data;

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

        [Route("~/api/regions/page/{page?}")]
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

        [Route("~/api/regions/{regionId}")]
        public async Task<ActionResult<RegionDetails>> GetDetails(long regionId)
            => await this.regionsService.GetByIdAsync<RegionDetails>(regionId);

        [Route("~/api/regions/{regionId}/short")]
        public async Task<ActionResult<RegionSimpleDetailsModel>> GetSimpleDetails(long regionId)
            => await this.regionsService.GetByIdAsync<RegionSimpleDetailsModel>(regionId);
    }
}

namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System.Threading.Tasks;
    using Common;
    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Api.Models.Regions;
    using EveEchoesPlanetaryProductionApi.Services.Data;

    using Microsoft.AspNetCore.Mvc;

    public class RegionsController : ControllerBase
    {
        private readonly IRegionsService regionsService;

        public RegionsController(IRegionsService regionsService)
        {
            this.regionsService = regionsService;
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
                Regions = await this.regionsService.GetAllAsync<RegionListingModel>(GlobalConstants.Ui.RegionsPageSize, page),
            };

            return model;
        }
    }
}

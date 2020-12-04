namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models;
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
                Count = await this.regionsService.GetCount(),
            };

            return model;
        }
    }
}

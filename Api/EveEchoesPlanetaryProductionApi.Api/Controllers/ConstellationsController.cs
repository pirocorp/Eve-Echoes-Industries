namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data;

    using Microsoft.AspNetCore.Mvc;

    public class ConstellationsController : ControllerBase
    {
        private readonly IConstellationService constellationService;

        public ConstellationsController(IConstellationService constellationService)
        {
            this.constellationService = constellationService;
        }

        [Route("~/api/constellations/count")]
        public async Task<ActionResult<CountModel>> GetRegionsCount()
        {
            var model = new CountModel()
            {
                Count = await this.constellationService.GetCount(),
            };

            return model;
        }
    }
}

namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetConstellations;
    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Services.Data;

    using Microsoft.AspNetCore.Mvc;
    using Models.Constellations.GetConstellation;

    public class ConstellationsController : ControllerBase
    {
        private readonly IConstellationService constellationService;

        public ConstellationsController(IConstellationService constellationService)
        {
            this.constellationService = constellationService;
        }

        [Route("~/api/constellations/count")]
        public async Task<ActionResult<CountModel>> GetConstellationsCount()
        {
            var model = new CountModel()
            {
                Count = await this.constellationService.GetCountAsync(),
            };

            return model;
        }

        [Route("~/api/constellations/{page?}")]
        public async Task<ActionResult<ConstellationPage>> GetConstellations(int page = 1)
        {
            if (page <= 0)
            {
                return this.BadRequest();
            }

            var model = new ConstellationPage()
            {
                Constellations = await this.constellationService.GetAllAsync<ConstellationListingModel>(GlobalConstants.Ui.ConstellationsPageSize, page),
            };

            return model;
        }

        [Route("~/api/constellation/{id}")]
        public async Task<ActionResult<ConstellationDetails>> GetConstellation(long id)
            => await this.constellationService.GetByIdAsync<ConstellationDetails>(id);
    }
}

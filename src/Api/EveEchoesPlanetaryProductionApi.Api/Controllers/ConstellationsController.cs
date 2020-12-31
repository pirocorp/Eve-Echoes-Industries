namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetConstellations;
    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetDetails;
    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetSimpleDetails;
    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Services.Data;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class ConstellationsController : ControllerBase
    {
        private readonly IConstellationService constellationService;

        public ConstellationsController(
            IConstellationService constellationService)
        {
            this.constellationService = constellationService;
        }

        [Route("~/api/constellations/count")]
        public async Task<ActionResult<CountModel>> GetCount()
        {
            var model = new CountModel()
            {
                Count = await this.constellationService.GetCountAsync(),
            };

            return model;
        }

        [Route("~/api/constellations/page/{page?}")]
        public async Task<ActionResult<ConstellationPage>> GetConstellations(int page = 1)
        {
            if (page <= 0)
            {
                return this.BadRequest();
            }

            var model = new ConstellationPage()
            {
                Constellations = await this.constellationService.GetConstellationsAsync<ConstellationListingModel>(GlobalConstants.Ui.ConstellationsPageSize, page),
            };

            return model;
        }

        [Route("~/api/constellations/{constellationId}")]
        public async Task<ActionResult<ConstellationDetails>> GetDetails(long constellationId)
            => await this.constellationService.GetConstellationAsync<ConstellationDetails>(constellationId);

        [Route("~/api/constellations/{constellationId}/short")]
        public async Task<ActionResult<ConstellationSimpleDetailsModel>> GetSimpleDetails(long constellationId)
            => await this.constellationService.GetConstellationAsync<ConstellationSimpleDetailsModel>(constellationId);
    }
}

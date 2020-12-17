namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Api.Models.BestSystemModel;
    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations.BestSolarSystemsInConstellation;
    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetConstellations;
    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetDetails;
    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetSimpleDetails;
    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Services.Data;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [ApiController]
    public class ConstellationsController : ControllerBase
    {
        private readonly IConstellationService constellationService;
        private readonly IOptions<ApiBehaviorOptions> apiBehaviorOptions;

        public ConstellationsController(
            IConstellationService constellationService,
            IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            this.constellationService = constellationService;
            this.apiBehaviorOptions = apiBehaviorOptions;
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
                Constellations = await this.constellationService.GetAllAsync<ConstellationListingModel>(GlobalConstants.Ui.ConstellationsPageSize, page),
            };

            return model;
        }

        [Route("~/api/constellations/{constellationId}")]
        public async Task<ActionResult<ConstellationDetails>> GetDetails(long constellationId)
            => await this.constellationService.GetByIdAsync<ConstellationDetails>(constellationId);

        [Route("~/api/constellations/{constellationId}/short")]
        public async Task<ActionResult<ConstellationSimpleDetailsModel>> GetSimpleDetails(long constellationId)
            => await this.constellationService.GetByIdAsync<ConstellationSimpleDetailsModel>(constellationId);
    }
}

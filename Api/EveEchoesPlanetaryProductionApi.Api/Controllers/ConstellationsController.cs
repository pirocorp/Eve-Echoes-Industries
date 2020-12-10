namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetConstellations;
    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetDetails;
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
        public async Task<ActionResult<ConstellationDetails>> GetDetails(long id)
            => await this.constellationService.GetByIdAsync<ConstellationDetails>(id);

        [HttpPost]
        [Route("~/api/solarSystems/best/constellation/{constellationId}")]
        public async Task<IActionResult> GetBestSolarSystemInConstellation(long constellationId, [FromBody]BestInputModel model)
        {
            var priceSelectorSuccess = Enum.TryParse<PriceSelector>(model.Price, out var priceSelector);

            if (!priceSelectorSuccess)
            {
                this.ModelState.AddModelError(nameof(BestInputModel.Price), "Invalid price selector");
                return this.apiBehaviorOptions.Value.InvalidModelStateResponseFactory(this.ControllerContext);
            }

            model.PriceSelector = priceSelector;

            var result = await this.constellationService.GetBestSolarSystem(constellationId, model);

            return this.Ok(result);
        }
    }
}

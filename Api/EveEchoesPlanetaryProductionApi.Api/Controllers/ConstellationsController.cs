namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models;
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
    using Models.BestSystemModel;

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

        [Route("~/api/constellation/simple/{id}")]
        public async Task<ActionResult<ConstellationSimpleDetailsModel>> GetSimpleDetails(long id)
            => await this.constellationService.GetByIdAsync<ConstellationSimpleDetailsModel>(id);

        [HttpPost]
        [Route("~/api/solarSystems/best/constellation/{constellationId}")]
        public async Task<IActionResult> GetBestSolarSystemsInConstellation(long constellationId, [FromBody]BestInputModel input)
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

            var model = new BestConstellationModel
            {
                Systems = await this.constellationService
                    .GetBestSolarSystem<BestSystemModel>(constellationId, input),
            };

            return this.Ok(model);
        }
    }
}

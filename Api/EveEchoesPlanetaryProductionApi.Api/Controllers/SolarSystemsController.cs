namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Api.Models.BestSystemModel;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetBestSystemInRange;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSystems;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.Search;
    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Services.Data;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetBestPlanetaryResourcesById;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [ApiController]
    [Route("api/[controller]")]
    public class SolarSystemsController : ControllerBase
    {
        private readonly ISolarSystemsService solarSystemService;
        private readonly IOptions<ApiBehaviorOptions> apiBehaviorOptions;

        public SolarSystemsController(
            ISolarSystemsService solarSystemService,
            IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            this.solarSystemService = solarSystemService;
            this.apiBehaviorOptions = apiBehaviorOptions;
        }

        public async Task<ActionResult<SolarSystemServiceModel>> GetSolarSystem()
            => await this.solarSystemService.GetRandomAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<SolarSystemServiceModel>> GetSolarSystem(long id)
        {
            var solarSystem = await this.solarSystemService.GetByIdAsync(id);

            if (solarSystem is null)
            {
                return this.NotFound();
            }

            return solarSystem;
        }

        [Route("~/api/solarSystems/count")]
        public async Task<CountModel> GetSolarSystemsCount()
        {
            var model = new CountModel()
            {
                Count = await this.solarSystemService.GetSolarSystemsCount(),
            };

            return model;
        }

        [Route("~/api/solarSystems/page/{page}")]
        public async Task<ActionResult<SystemsPageModel>> GetSystems(int page = 1)
        {
            if (page <= 0)
            {
                return this.BadRequest();
            }

            var model = new SystemsPageModel()
            {
                Systems = await this.solarSystemService.GetAllAsync<SolarSystemListingModel>(GlobalConstants.Ui.SolarSystemsSearchPageSize, page),
            };

            return model;
        }

        [Route("~/api/solarSystems/search/{searchTerm}/{page?}")]
        public async Task<ActionResult<SearchResultModel>> Search(string searchTerm, int page = 1)
        {
            var (results, count) = await this.solarSystemService
                .Search<SearchResultSolarSystemModel>(searchTerm, GlobalConstants.Ui.SolarSystemsSearchPageSize, page);

            var model = new SearchResultModel()
            {
                Results = results,
                Count = count,
            };

            return model;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<SolarSystemBestModel>> GetSystemBestResources(long id, [FromBody]BestInputModel model)
        {
            var selectorIsParsedSuccessful = Enum.TryParse<PriceSelector>(model.Price, out var priceSelector);

            if (!selectorIsParsedSuccessful)
            {
                return this.BadRequest();
            }

            var sol = await this.solarSystemService.GetResourcesInSystemByIdAsync(id, priceSelector);

            if (sol is null)
            {
                return this.NotFound();
            }

            sol.MiningPlanets = model.MiningPlanets;

            return sol;
        }

        [HttpPost]
        [Route("~/api/solarSystems/{range}/{id}")]
        public async Task<IActionResult> GetBestSystemInRange(long id, int range, [FromBody]BestInputModel input)
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

            var result = await this.solarSystemService.GetBestSolarSystemInRange<BestSystemModel>(id, range, input);

            var model = new BestRangeModel
            {
                Count = result.Count,
                Systems = result.Systems,
            };

            return this.Ok(model);
        }
    }
}

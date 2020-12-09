namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetBestSolarSystemPlanetaryResourcesValues;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSystems;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.Search;
    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Services.Data;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetBestPlanetaryResourcesById;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SolarSystemsController : ControllerBase
    {
        private readonly ISolarSystemsService solarSystemService;

        public SolarSystemsController(
            ISolarSystemsService solarSystemService)
        {
            this.solarSystemService = solarSystemService;
        }

        [AllowAnonymous]
        public async Task<ActionResult<SolarSystemServiceModel>> GetSolarSystem()
            => await this.solarSystemService.GetRandomAsync();

        [AllowAnonymous]
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

        [AllowAnonymous]
        [Route("~/api/solarSystems/count")]
        public async Task<CountModel> GetSolarSystemsCount()
        {
            var model = new CountModel()
            {
                Count = await this.solarSystemService.GetSolarSystemsCount(),
            };

            return model;
        }

        [AllowAnonymous]
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

        [HttpGet]
        [AllowAnonymous]
        [Route("~/api/solarSystems/search/{searchTerm}/{page?}")]
        public async Task<ActionResult<IEquatable<SearchResultModel>>> Search(string searchTerm, int page = 1)
        {
            var (results, count) = await this.solarSystemService
                .Search<SearchResultSolarSystemModel>(searchTerm, GlobalConstants.Ui.SolarSystemsSearchPageSize, page);

            var model = new SearchResultModel()
            {
                Results = results,
                Count = count,
            };

            return this.Ok(model);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<SolarSystemBestModel>> GetBestSolarSystemPlanetaryResourcesValues(long id, [FromBody]GetBestSolarSystemPlanetaryResourcesValuesInputModel model)
        {
            var selectorIsParsedSuccessful = Enum.TryParse<PriceSelector>(model.PriceSelector, out var priceSelector);

            if (!selectorIsParsedSuccessful)
            {
                return this.BadRequest();
            }

            var sol = await this.solarSystemService.GetBestPlanetaryResourcesByIdAsync(id, priceSelector);

            if (sol is null)
            {
                return this.NotFound();
            }

            sol.MiningPlanets = model.MiningPlanets;

            return sol;
        }

        [HttpPost]
        [Route("~/api/solarSystems/{range}/{id}")]
        public async Task<ActionResult<SolarSystemBestModel>> GetBestSolarSystemPlanetaryResourcesValues(long id, int range, [FromBody]GetBestSolarSystemPlanetaryResourcesValuesInputModel model)
        {
            var selectorIsParsedSuccessful = Enum.TryParse<PriceSelector>(model.PriceSelector, out var priceSelector);

            if (!selectorIsParsedSuccessful)
            {
                return this.BadRequest();
            }

            var sol = await this.solarSystemService.GetBestPlanetaryResourcesInRangeAsync(id, priceSelector, range, model.MiningPlanets);

            if (sol is null)
            {
                return this.NotFound();
            }

            return sol;
        }
    }
}

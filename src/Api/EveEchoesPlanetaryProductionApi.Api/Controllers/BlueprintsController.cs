namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Blueprints;
    using EveEchoesPlanetaryProductionApi.Api.Models.Blueprints.GetBlueprint;
    using EveEchoesPlanetaryProductionApi.Services.Data;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class BlueprintsController : ControllerBase
    {
        private readonly IBlueprintsService blueprintsService;

        public BlueprintsController(IBlueprintsService blueprintsService)
        {
            this.blueprintsService = blueprintsService;
        }

        [Route("~/api/blueprints/types")]
        public async Task<IActionResult> GetBlueprintTypes()
        {
            var model = await this.blueprintsService
                .GetBlueprintTypesAsync<BlueprintTypeModel>();

            return this.Ok(model);
        }

        [Route("~/api/blueprints/{blueprintId:Guid}")]
        public async Task<IActionResult> GetBlueprint(string blueprintId)
        {
            var model = await this.blueprintsService
                .GetBlueprintAsync<GetBlueprintModel>(blueprintId);

            return this.Ok(model);
        }

        [HttpPost]
        [Route("~/api/blueprints/page/{page?}")]
        public async Task<IActionResult> BrowseBlueprints([FromBody] BrowseBlueprintsInputModel inputModel, int page = 1)
            => await this.GetBlueprints(inputModel, null, page);

        [HttpPost]
        [Route("~/api/blueprints/search/{searchTerm}/page/{page?}")]
        public async Task<IActionResult> Search([FromBody] BrowseBlueprintsInputModel inputModel, string searchTerm, int page = 1)
            => await this.GetBlueprints(inputModel, searchTerm, page);

        private async Task<IActionResult> GetBlueprints([FromBody] BrowseBlueprintsInputModel inputModel, string searchTerm, int page = 1)
        {
            if (page <= 0)
            {
                return this.BadRequest();
            }

            // Default behavior is show all types.
            if (inputModel is null || !inputModel.Types.Any())
            {
                inputModel = await this.DefaultInputModel();
            }

            int count;
            IEnumerable<BlueprintListingModel> blueprints;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                (count, blueprints) = await this.blueprintsService
                    .GetBlueprintsPageAsync<BlueprintListingModel>(inputModel.Types, page);
            }
            else
            {
                (count, blueprints) = await this.blueprintsService
                    .SearchAsync<BlueprintListingModel>(inputModel.Types, searchTerm, page);
            }

            var model = new BrowseBlueprintsModel()
            {
                Count = count,
                Blueprints = blueprints,
            };

            return this.Ok(model);
        }

        private async Task<BrowseBlueprintsInputModel> DefaultInputModel()
            => new ()
            {
                Types = (await this.blueprintsService
                        .GetBlueprintTypesAsync<BlueprintTypeModel>())
                    .Select(t => t.Id)
                    .ToList(),
            };
    }
}

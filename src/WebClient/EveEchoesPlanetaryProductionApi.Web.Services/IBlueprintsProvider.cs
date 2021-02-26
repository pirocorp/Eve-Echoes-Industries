namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Blueprints;
    using EveEchoesPlanetaryProductionApi.Api.Models.Blueprints.GetBlueprint;

    public interface IBlueprintsProvider
    {
        Task<IEnumerable<BlueprintTypeModel>> GetBlueprintTypesAsync();

        Task<GetBlueprintModel> GetBlueprintAsync(string blueprintId);

        Task<BrowseBlueprintsModel> BrowseBlueprints(BrowseBlueprintsInputModel model, int page = 1);

        Task<BrowseBlueprintsModel> SearchBlueprintsAsync(BrowseBlueprintsInputModel model, string searchTerm, int page = 1);
    }
}

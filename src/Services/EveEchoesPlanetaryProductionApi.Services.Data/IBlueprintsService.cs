namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlueprintsService
    {
        Task<IEnumerable<TOut>> GetBlueprintTypesAsync<TOut>();

        Task<TOut> GetBlueprintAsync<TOut>(string blueprintId);

        Task<(int Total, IEnumerable<TOut> Results)> GetBlueprintsPageAsync<TOut>(IEnumerable<long> types, int page);

        Task<(int Total, IEnumerable<TOut> Results)> SearchAsync<TOut>(IEnumerable<long> types, string searchTerm, int page);
    }
}

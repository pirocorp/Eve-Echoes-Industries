namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Services.Data.Models;

    public interface IConstellationService
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<TOut>> GetConstellationsAsync<TOut>(int pageSize, int page = 1);

        Task<TOut> GetConstellationAsync<TOut>(long constellationId);

        Task<IEnumerable<TOut>> GetBestSystemInConstellationAsync<TOut>(long constellationId, InputModel input);
    }
}

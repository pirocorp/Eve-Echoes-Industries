namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Services.Data.Models;

    public interface IConstellationService
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<TOut>> GetAllAsync<TOut>(int pageSize, int page = 1);

        Task<TOut> GetByIdAsync<TOut>(long id);

        Task<IEnumerable<TOut>> GetBestSolarSystem<TOut>(long constellationId, BestInputModel input);
    }
}

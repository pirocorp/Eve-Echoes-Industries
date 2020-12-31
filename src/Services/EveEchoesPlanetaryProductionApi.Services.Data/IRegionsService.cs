namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Services.Data.Models;

    public interface IRegionsService
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<TOut>> GetAllAsync<TOut>(int pageSize, int page = 1);

        Task<TOut> GetRegionAsync<TOut>(long id);

        Task<IEnumerable<TOut>> GetBestSystemsInRegionAsync<TOut>(long regionId, InputModel input);

        Task<int> GetSystemsCountInRegionAsync(long regionId);
    }
}

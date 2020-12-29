namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystemServiceModel;

    public interface ISolarSystemsService
    {
        Task<SolarSystemServiceModel> GetRandomSystemAsync();

        Task<SolarSystemServiceModel> GetSystemAsync(long systemId);

        Task<int> GetCountAsync();

        Task<IEnumerable<TOut>> GetSystemsAsync<TOut>(int pageSize, int page = 1);

        Task<(IEnumerable<TOut> results, int count)> SearchAsync<TOut>(string searchTerm, int pageSize, int page = 1);

        Task<TOut> GetSystemAsync<TOut>(long id);

        Task<(int Count, IEnumerable<TOut> Systems)> GetBestSystemInRangeAsync<TOut>(long solarSystemId, int range, InputModel input);

        Task<List<long>> GetSystemsInRangeIdsAsync(int range, long solarSystemId);
    }
}

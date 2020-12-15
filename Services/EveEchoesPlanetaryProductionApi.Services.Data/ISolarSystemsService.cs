namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetBestPlanetaryResourcesById;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    public interface ISolarSystemsService
    {
        Task<SolarSystemServiceModel> GetRandomAsync();

        Task<SolarSystemServiceModel> GetByIdAsync(long id);

        Task<int> GetSolarSystemsCount();

        Task<IEnumerable<TOut>> GetAllAsync<TOut>(int pageSize, int page = 1);

        Task<(IEnumerable<TOut> results, int count)> Search<TOut>(string searchTerm, int pageSize, int page = 1);

        Task<string> GetSolarSystemNameAsync(long id);

        Task<TOut> GetByIdAsync<TOut>(long id);

        Task<TOut> GetByNameAsync<TOut>(string name);

        Task<SolarSystemBestModel> GetResourcesInSystemByIdAsync(long id, PriceSelector priceSelector);

        Task<(int Count, IEnumerable<TOut> Systems)> GetBestSolarSystemInRange<TOut>(long solarSystemId, int range, BestInputModel input);

        Task<List<long>> GetSolarSystemsInRangeIds(int range, long solarSystemId);
    }
}

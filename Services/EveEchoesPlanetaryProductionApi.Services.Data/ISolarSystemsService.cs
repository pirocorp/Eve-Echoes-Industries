namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetBestPlanetaryResourcesById;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    public interface ISolarSystemsService
    {
        Task<SolarSystemServiceModel> GetByIdAsync(long id);

        Task<int> GetSolarSystemsCount();

        Task<(IEnumerable<TOut> results, int count)> Search<TOut>(string searchTerm, int pageSize, int page = 1);

        Task<string> GetSolarSystemNameAsync(long id);

        Task<SolarSystemBestModel> GetBestPlanetaryResourcesByIdAsync(long id, PriceSelector priceSelector);

        Task<SolarSystemBestModel> GetBestPlanetaryResourcesInRangeAsync(long solarSystemId, PriceSelector priceSelector, int range, int miningPlanets);

        Task<TOut> GetByIdAsync<TOut>(long id);

        Task<TOut> GetByNameAsync<TOut>(string name);

        Task<List<long>> GetSolarSystemsInRangeIds(int range, long solarSystemId);
    }
}

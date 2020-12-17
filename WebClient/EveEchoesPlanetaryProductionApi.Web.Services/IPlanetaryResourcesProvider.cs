namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Api.Models.PlanetaryResources;
    using Api.Models.PlanetaryResources.GetAllPlanetResourcesWithPrices;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;

    public interface IPlanetaryResourcesProvider
    {
        Task<IEnumerable<string>> GetPlanetaryResources();

        Task<int> GetPlanetaryResourcesCount();

        Task<IEnumerable<PlanetaryResource>> GetPlanetaryResourcesCurrentPrices();

        Task<BestPlanetaryResourcesModel> GetBestResourcesInConstellation(long constellationId, BestInputModel model);

        Task<BestPlanetaryResourcesModel> GetBestResourcesInRegion(long regionId, BestInputModel model);

        Task<BestPlanetaryResourcesModel> GetBestResourcesInRange(int range, long solarSystemId, BestInputModel model);
    }
}

namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Api.Models.PlanetaryResources.BestPlanetaryResourcesInConstellation;
    using Api.Models.PlanetaryResources.GetAllPlanetResourcesWithPrices;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;

    public interface IPlanetaryResourcesProvider
    {
        Task<IEnumerable<string>> GetPlanetaryResources();

        Task<int> GetPlanetaryResourcesCount();

        Task<IEnumerable<PlanetaryResource>> GetPlanetaryResourcesCurrentPrices();

        Task<BestPlanetaryResourcesInConstellationModel> GetBestResourcesInConstellation(long constellationId, BestInputModel model);
    }
}

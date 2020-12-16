namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Api.Models.PlanetaryResources.GetAllPlanetResourcesWithPrices;

    public interface IPlanetaryResourcesProvider
    {
        Task<IEnumerable<string>> GetPlanetaryResources();

        Task<int> GetPlanetaryResourcesCount();

        Task<IEnumerable<PlanetaryResource>> GetPlanetaryResourcesCurrentPrices();
    }
}

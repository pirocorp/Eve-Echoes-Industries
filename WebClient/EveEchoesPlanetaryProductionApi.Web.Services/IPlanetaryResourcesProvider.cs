namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPlanetaryResourcesProvider
    {
        Task<IEnumerable<string>> GetPlanetaryResources();
    }
}

namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations;
    using EveEchoesPlanetaryProductionApi.Api.Models.Regions;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSystems;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById;

    public interface IEveApiService
    {
        Task<int> GetRegionsCountAsync();

        Task<int> GetConstellationsCountAsync();

        Task<int> GetSolarSystemsCountAsync();

        Task<SolarSystemServiceModel> GetRandomSolarSystemAsync();

        Task<SolarSystemServiceModel> GetSolarSystemAsync(long solarSystemId);

        Task<IEnumerable<RegionListingModel>> GetRegionsPageAsync(int page = 1);

        Task<IEnumerable<ConstellationListingModel>> GetConstellationsPageAsync(int page = 1);

        Task<IEnumerable<SolarSystemListingModel>> GetSystemsPageAsync(int page = 1);
    }
}

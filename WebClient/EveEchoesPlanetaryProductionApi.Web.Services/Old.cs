namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Api.Models.Constellations.GetConstellations;
    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations;
    using EveEchoesPlanetaryProductionApi.Api.Models.Regions;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSystems;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById;

    public interface Old
    {
        public Task<int> GetRegionsCountAsync();

        public Task<int> GetConstellationsCountAsync();

        public Task<int> GetSolarSystemsCountAsync();

        public Task<SolarSystemServiceModel> GetRandomSolarSystemAsync();

        public Task<SolarSystemServiceModel> GetSolarSystemAsync(long solarSystemId);

        public Task<IEnumerable<RegionListingModel>> GetRegionsPageAsync(int page = 1);

        public Task<IEnumerable<ConstellationListingModel>> GetConstellationsPageAsync(int page = 1);

        Task<IEnumerable<SolarSystemListingModel>> GetSystemsPageAsync(int page = 1);
    }
}

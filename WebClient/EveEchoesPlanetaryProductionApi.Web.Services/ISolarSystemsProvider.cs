namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSolarSystemSimpleDetails;
    using EveEchoesPlanetaryProductionApi.Api.Models.Regions.GetBestSolarSystemsInRegionAsync;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSystems;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.Search;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystemServiceModel;

    public interface ISolarSystemsProvider
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<SolarSystemListingModel>> GetPageAsync(int page = 1);

        Task<SolarSystemServiceModel> GetRandomAsync();

        Task<SolarSystemServiceModel> GetAsync(long solarSystemId);

        Task<SolarSystemSimpleDetailsModel> GetSolarSystemSimpleDetails(long solarSystemId);

        Task<SearchResultModel> GetSearchResultsAsync(string searchTerm, int page = 1);

        Task<BestRegionModel> GetBestSystemsInRange(int range, long systemId, InputModel model);
    }
}
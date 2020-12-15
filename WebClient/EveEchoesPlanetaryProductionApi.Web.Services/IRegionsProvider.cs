namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Regions.GetBestSolarSystemsInRegionAsync;
    using EveEchoesPlanetaryProductionApi.Api.Models.Regions.GetDetails;
    using EveEchoesPlanetaryProductionApi.Api.Models.Regions.GetRegions;
    using EveEchoesPlanetaryProductionApi.Api.Models.Regions.GetSimpleDetails;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;

    public interface IRegionsProvider
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<RegionListingModel>> GetPageAsync(int page = 1);

        Task<RegionDetails> GetDetailsAsync(long regionId);

        Task<RegionSimpleDetailsModel> GetSimpleDetailsAsync(long regionId);

        Task<BestRegionModel> GetBestSystemsInRegion(long regionId, BestInputModel model);
    }
}
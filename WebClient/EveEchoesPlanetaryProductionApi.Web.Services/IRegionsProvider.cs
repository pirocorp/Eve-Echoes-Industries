namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Api.Models.Regions.GetDetails;
    using Api.Models.Regions.GetRegions;

    public interface IRegionsProvider
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<RegionListingModel>> GetPageAsync(int page = 1);

        Task<RegionDetails> GetDetailsAsync(long regionId);
    }
}
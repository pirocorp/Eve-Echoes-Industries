namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Api.Models.Regions;

    public interface IRegionsProvider
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<RegionListingModel>> GetPageAsync(int page = 1);
    }
}
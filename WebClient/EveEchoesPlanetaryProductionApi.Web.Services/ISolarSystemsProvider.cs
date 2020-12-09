namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Api.Models.SolarSystems.GetSystems;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById;

    public interface ISolarSystemsProvider
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<SolarSystemListingModel>> GetPageAsync(int page = 1);

        Task<SolarSystemServiceModel> GetRandomAsync();

        Task<SolarSystemServiceModel> GetAsync(long solarSystemId);
    }
}
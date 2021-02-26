namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetConstellations;
    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetDetails;
    using EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetSimpleDetails;
    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetBestSystemsInConstellation;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;

    public interface IConstellationsProvider
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<ConstellationListingModel>> GetPageAsync(int page = 1);

        Task<ConstellationDetails> GetDetailsAsync(long id);

        Task<BestConstellationModel> GetBestSystemsInConstellation(long constellationId, InputModel model);

        Task<ConstellationSimpleDetailsModel> GetSimpleDetailsAsync(long id);
    }
}

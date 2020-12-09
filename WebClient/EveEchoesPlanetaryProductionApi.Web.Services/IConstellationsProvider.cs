namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Api.Models.Constellations.GetConstellations;
    using Api.Models.Constellations.GetDetails;

    public interface IConstellationsProvider
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<ConstellationListingModel>> GetPageAsync(int page = 1);

        Task<ConstellationDetails> GetDetailsAsync(long id);
    }
}
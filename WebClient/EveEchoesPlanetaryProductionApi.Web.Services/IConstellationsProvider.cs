namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Api.Models.Constellations.GetConstellation;
    using Api.Models.Constellations.GetConstellations;

    public interface IConstellationsProvider
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<ConstellationListingModel>> GetPageAsync(int page = 1);

        Task<ConstellationDetails> GetDetailsAsync(long id);
    }
}
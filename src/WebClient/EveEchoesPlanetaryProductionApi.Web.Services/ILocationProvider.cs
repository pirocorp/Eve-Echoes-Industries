namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Locations;

    public interface ILocationProvider
    {
        Task<LocationModel> GetRandomAsync();

        Task<LocationModel> GetLocationAsync(long systemId);
    }
}

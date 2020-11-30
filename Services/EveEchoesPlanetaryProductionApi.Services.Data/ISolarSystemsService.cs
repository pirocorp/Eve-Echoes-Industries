namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetBestPlanetaryResourcesById;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    public interface ISolarSystemsService
    {
        Task<SolarSystemServiceModel> GetByIdAsync(long id);

        Task<SolarSystemBestModel> GetBestPlanetaryResourcesByIdAsync(long id, PriceSelector priceSelector);

        Task<TOut> GetByIdAsync<TOut>(long id);

        Task<TOut> GetByNameAsync<TOut>(string name);
    }
}

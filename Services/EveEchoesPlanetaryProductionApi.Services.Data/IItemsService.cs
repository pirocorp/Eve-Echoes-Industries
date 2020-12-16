namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.IItemsService;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;
    using Models;

    public interface IItemsService
    {
        Task<IEnumerable<ItemServiceModel>> GetPlanetaryResources(PriceSelector priceSelector);

        Task<IEnumerable<ItemServiceModel>> GetPlanetaryResources(PricesModel prices);

        Task<ItemPrice> GetLatestPricesAsync(long id);

        Task<IDictionary<long, ItemPrice>> GetLatestItemsPricesAsync(IEnumerable<long> itemIds);
    }
}

namespace EveEchoesPlanetaryProductionApi.Services.EveEchoesMarket
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    public interface IItemsPricesService
    {
        Task<IEnumerable<ItemPrice>> GetHistoricalPricesForItemByIdAsync(long id);

        Task<ItemPrice> GetLatestPriceAsync(long id);

        Task<IDictionary<long, ItemPrice>> GetItemPricesAsync(IEnumerable<long> itemIds);
    }
}

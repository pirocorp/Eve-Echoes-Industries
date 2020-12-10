namespace EveEchoesPlanetaryProductionApi.Services.EveEchoesMarket
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    public interface IItemsPricesService
    {
        Task<IEnumerable<ItemPrice>> GetHistoricalPricesForItemByIdAsync(long id);

        Task<ItemPrice> GetLatestPricesAsync(long itemId);
    }
}

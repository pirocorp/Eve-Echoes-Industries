namespace EveEchoesPlanetaryProductionApi.Services.EveEchoesMarket
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    public interface IItemsPricesService
    {
        Task<IEnumerable<ItemPrice>> GetHistoricalPricesForItemById(long id);

        Task<ItemPrice> GetLatestPrice(long id);
    }
}

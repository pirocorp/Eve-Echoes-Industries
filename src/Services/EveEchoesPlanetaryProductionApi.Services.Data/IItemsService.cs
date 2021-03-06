﻿namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.IItemsService;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    public interface IItemsService
    {
        Task<TOut> GetItem<TOut>(string name);

        Task<IEnumerable<ItemServiceModel>> GetPlanetaryResources(PriceSelector priceSelector);

        Task<IEnumerable<ItemServiceModel>> GetPlanetaryResources(PricesModel prices);

        Task<ItemPrice> GetLatestPricesAsync(long id);

        Task<IDictionary<long, ItemPrice>> GetLatestItemsPricesAsync(IEnumerable<long> itemIds);
    }
}

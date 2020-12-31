namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Common.Extensions;
    using EveEchoesPlanetaryProductionApi.Data;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.IItemsService;
    using EveEchoesPlanetaryProductionApi.Services.EveEchoesMarket;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;

    /// <summary>
    /// This service returns current price/s for item.
    /// It uses Memory Cache because prices are actualized on (30-60)min interval.
    /// Memory cache reduces calls to database.
    /// </summary>
    public class ItemsService : IItemsService
    {
        private readonly IMemoryCache memoryCache;
        private readonly EveEchoesPlanetaryProductionApiDbContext dbContext;
        private readonly IItemsPricesService itemsPricesService;

        public ItemsService(
            IMemoryCache memoryCache,
            EveEchoesPlanetaryProductionApiDbContext dbContext,
            IItemsPricesService itemsPricesService)
        {
            this.memoryCache = memoryCache;
            this.dbContext = dbContext;
            this.itemsPricesService = itemsPricesService;
        }

        public async Task<IEnumerable<ItemServiceModel>> GetPlanetaryResources(PricesModel prices)
        {
            var items = await this.GetItemServiceModels();
            var currentPrices = prices.GetType().GetProperties()
                .ToDictionary(p => p.Name, p => p);

            foreach (var item in items)
            {
                var price = currentPrices[item.Name.RemoveSpaces()].GetValue(prices) as decimal? ?? 0;
                item.Price = price;
            }

            return items;
        }

        public async Task<IEnumerable<ItemServiceModel>> GetPlanetaryResources(PriceSelector priceSelector)
        {
            var items = await this.GetItemServiceModels();

            var selectorFunction = this.GetSelectorFunction(priceSelector);
            items.ForEach(selectorFunction);

            return items;
        }

        public async Task<IDictionary<long, ItemPrice>> GetLatestItemsPricesAsync(IEnumerable<long> itemIds)
        {
            var ids = itemIds?.Distinct().ToArray();

            if (ids is null || !ids.Any())
            {
                return null;
            }

            var itemPrices = new Dictionary<long, ItemPrice>();

            foreach (var itemId in ids)
            {
                var price = await this.GetLatestPricesAsync(itemId);
                itemPrices.Add(itemId, price);
            }

            return itemPrices;
        }

        public async Task<ItemPrice> GetLatestPricesAsync(long id)
        {
            var key = $"Last{id}";

            if (!this.memoryCache.TryGetValue(key, out ItemPrice cacheEntry))
            {
                cacheEntry = await this.itemsPricesService.GetLatestPricesAsync(id);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(GlobalConstants.InMemoryPlanetaryResourcesCachingInSeconds));

                this.memoryCache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        private async Task<List<ItemServiceModel>> GetItemServiceModels()
        {
            var planetaryResourcesIds = GlobalConstants.Items.GetPlanetaryResourcesIds().ToList();

            var items = await this.dbContext.Items
                .Where(i => planetaryResourcesIds.Any(x => x.Equals(i.Id)))
                .To<ItemServiceModel>()
                .ToListAsync();

            return items;
        }

        private Action<ItemServiceModel> GetSelectorFunction(PriceSelector priceSelector)
            => priceSelector switch
                {
                    PriceSelector.Sell => i => i.Price = this.GetLatestPricesAsync(i.Id).GetAwaiter().GetResult().Sell,
                    PriceSelector.Buy => i => i.Price = this.GetLatestPricesAsync(i.Id).GetAwaiter().GetResult().Buy,
                    PriceSelector.LowestSell => i => i.Price = this.GetLatestPricesAsync(i.Id).GetAwaiter().GetResult().LowestSell,
                    PriceSelector.HighestBuy => i => i.Price = this.GetLatestPricesAsync(i.Id).GetAwaiter().GetResult().HighestBuy,
                    PriceSelector.UserProvided => null,
                    _ => null
                };
    }
}

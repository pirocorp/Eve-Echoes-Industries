namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Data;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.IItemsService;
    using EveEchoesPlanetaryProductionApi.Services.EveEchoesMarket;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;

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

        public async Task<IEnumerable<ItemServiceModel>> GetPlanetaryResources(PriceSelector priceSelector)
        {
            var planetaryResources = GlobalConstants.Items.GetPlanetaryResourcesIds().ToList();
            var key = nameof(planetaryResources);

            if (!this.memoryCache.TryGetValue(key, out List<ItemServiceModel> cacheEntry))
            {
                cacheEntry = await this.dbContext.Items
                    .Where(i => planetaryResources.Any(x => x.Equals(i.Id)))
                    .To<ItemServiceModel>()
                    .ToListAsync();

                var selectorFunction = this.GetSelectorFunction(priceSelector);
                cacheEntry.ForEach(selectorFunction);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(GlobalConstants.InMemoryPlanetaryResourcesCachingInSeconds));

                this.memoryCache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        private Action<ItemServiceModel> GetSelectorFunction(PriceSelector priceSelector)
        {
            switch (priceSelector)
            {
                case PriceSelector.Sell:
                    return i => i.Price = this.itemsPricesService.GetLatestPriceAsync(i.Id).GetAwaiter().GetResult().Sell;
                case PriceSelector.Buy:
                    return i => i.Price = this.itemsPricesService.GetLatestPriceAsync(i.Id).GetAwaiter().GetResult().Buy;
                case PriceSelector.LowestSell:
                    return i => i.Price = this.itemsPricesService.GetLatestPriceAsync(i.Id).GetAwaiter().GetResult().LowestSell;
                case PriceSelector.HighestBuy:
                    return i => i.Price = this.itemsPricesService.GetLatestPriceAsync(i.Id).GetAwaiter().GetResult().HighestBuy;
                case PriceSelector.UserProvided:
                    return null;
                default:
                    return null;
            }
        }
    }
}

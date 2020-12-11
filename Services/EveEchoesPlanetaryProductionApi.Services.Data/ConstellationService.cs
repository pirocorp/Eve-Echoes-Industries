namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Data;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.ConstellationService.BestSolarSystemInConstellation;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.IItemsService;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SystemsBestModel;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;

    public class ConstellationService : IConstellationService
    {
        private readonly IItemsService itemsService;
        private readonly EveEchoesPlanetaryProductionApiDbContext dbContext;
        private readonly IMemoryCache memoryCache;

        public ConstellationService(
            IItemsService itemsService,
            IMemoryCache memoryCache,
            EveEchoesPlanetaryProductionApiDbContext dbContext)
        {
            this.itemsService = itemsService;
            this.dbContext = dbContext;
            this.memoryCache = memoryCache;
        }

        public async Task<int> GetCountAsync()
        {
            var key = $"{nameof(ConstellationService)} Count";

            if (!this.memoryCache.TryGetValue(key, out int cacheEntry))
            {
                cacheEntry = await this.dbContext.Constellations.CountAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(GlobalConstants.InMemoryCachingConstellationsCountInDays));

                this.memoryCache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        public async Task<IEnumerable<TOut>> GetAllAsync<TOut>(int pageSize, int page = 1)
            => await this.dbContext.Constellations
                .OrderBy(r => r.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .To<TOut>()
                .ToListAsync();

        public async Task<TOut> GetByIdAsync<TOut>(long id)
            => await this.dbContext.Constellations
                .Where(c => c.Id.Equals(id))
                .To<TOut>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<TOut>> GetBestSolarSystem<TOut>(long constellationId, BestInputModel input)
        {
            if (input.PriceSelector is PriceSelector.UserProvided)
            {
                throw new NotImplementedException();
            }
            else
            {
                return await this.GetBestByExternalPrices<TOut>(constellationId, input);
            }
        }

        private static void PopulatePrices(IEnumerable<SystemBestModel> systems, IEnumerable<ItemServiceModel> prices)
        {
            systems
                .SelectMany(s => s.Planets)
                .SelectMany(s => s.Resources)
                .ToList()
                .ForEach(r => r.Price = prices.FirstOrDefault(p => p.Id.Equals(r.Id))?.Price ?? 0);
        }

        private static IEnumerable<SystemBestModel> OrderByValue(IList<SystemBestModel> systems, int miningPlanets)
        {
            foreach (var system in systems)
            {
                var planets = system.Planets.ToList();
                foreach (var planet in planets)
                {
                    planet.Resources = planet.Resources.OrderByDescending(r => r.Price * (decimal) r.Output).ToList();
                }

                system.Planets = planets
                    .OrderByDescending(p => p.Resources.Select(r => r.Price * (decimal) r.Output).FirstOrDefault());
            }

            return systems
                .OrderByDescending(s => s.Planets
                    .Select(p => p.Resources.Select(r => r.Price * (decimal) r.Output).FirstOrDefault())
                    .Take(miningPlanets)
                    .Sum());
        }

        private async Task<IEnumerable<TOut>> GetBestByExternalPrices<TOut>(long constellationId, BestInputModel input)
        {
            var prices = await this.itemsService.GetPlanetaryResources(input.PriceSelector);

            var model = await this.dbContext.Constellations
                .Where(c => c.Id.Equals(constellationId))
                .To<BestSolarSystemInConstellationModel>()
                .FirstOrDefaultAsync();

            PopulatePrices(model.Systems, prices);

            model.Systems = OrderByValue(model.Systems.ToList(), input.MiningPlanets);

            var miningPlanets = input.MiningPlanets;
            return model.Systems.AsQueryable().To<TOut>(new { miningPlanets }).ToList();
        }
    }
}

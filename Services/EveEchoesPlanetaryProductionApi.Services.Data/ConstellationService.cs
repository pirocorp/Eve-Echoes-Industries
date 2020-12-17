namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Data;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;

    public class ConstellationService : BestSystemService, IConstellationService
    {
        private readonly IMemoryCache memoryCache;

        public ConstellationService(
            IItemsService itemsService,
            IMemoryCache memoryCache,
            EveEchoesPlanetaryProductionApiDbContext dbContext)
            : base(itemsService, dbContext)
        {
            this.memoryCache = memoryCache;
        }

        public async Task<int> GetCountAsync()
        {
            var key = $"{nameof(ConstellationService)} Count";

            if (!this.memoryCache.TryGetValue(key, out int cacheEntry))
            {
                cacheEntry = await this.DbContext.Constellations.CountAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(GlobalConstants.InMemoryCachingConstellationsCountInDays));

                this.memoryCache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        public async Task<IEnumerable<TOut>> GetConstellationsAsync<TOut>(int pageSize, int page = 1)
            => await this.DbContext.Constellations
                .OrderBy(r => r.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .To<TOut>()
                .ToListAsync();

        public async Task<TOut> GetConstellationAsync<TOut>(long constellationId)
            => await this.DbContext.Constellations
                .Where(c => c.Id.Equals(constellationId))
                .To<TOut>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<TOut>> GetBestSystemInConstellationAsync<TOut>(long constellationId, InputModel input)
            => (await this.GetBestSolarSystem(input, s => s.ConstellationId.Equals(constellationId)))
                .AsQueryable()
                .To<TOut>(new { miningPlanets = input.MiningPlanets })
                .ToList();
    }
}

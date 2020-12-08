namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;

    public class ConstellationService : IConstellationService
    {
        private readonly EveEchoesPlanetaryProductionApiDbContext dbContext;
        private readonly IMemoryCache memoryCache;

        public ConstellationService(
            IMemoryCache memoryCache,
            EveEchoesPlanetaryProductionApiDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.memoryCache = memoryCache;
        }

        public async Task<int> GetCount()
        {
            var key = $"{nameof(ConstellationService)} Count";

            if (!this.memoryCache.TryGetValue(key, out int cacheEntry))
            {
                cacheEntry = await this.dbContext.Regions.CountAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(GlobalConstants.InMemoryCachingConstellationsCountInDays));

                this.memoryCache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }
    }
}
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

    public class RegionsService : BestSystemService, IRegionsService
    {
        private readonly IMemoryCache memoryCache;

        public RegionsService(
            IMemoryCache memoryCache,
            IItemsService itemsService,
            EveEchoesPlanetaryProductionApiDbContext dbContext)
            : base(itemsService, dbContext)
        {
            this.memoryCache = memoryCache;
        }

        public async Task<int> GetCountAsync()
        {
            var key = $"{nameof(RegionsService)} Count";

            if (!this.memoryCache.TryGetValue(key, out int cacheEntry))
            {
                cacheEntry = await this.DbContext.Regions.CountAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(GlobalConstants.InMemoryCachingRegionsCountInDays));

                this.memoryCache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        public async Task<IEnumerable<TOut>> GetAllAsync<TOut>(int pageSize, int page = 1)
            => await this.DbContext.Regions
                .OrderBy(r => r.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .To<TOut>()
                .ToListAsync();

        public async Task<TOut> GetByIdAsync<TOut>(long id)
            => await this.DbContext.Regions
                .Where(r => r.Id.Equals(id))
                .To<TOut>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<TOut>> GetBestSolarSystemAsync<TOut>(long regionId, BestInputModel input)
            => (await this.GetBestSolarSystem(input, s => s.RegionId.Equals(regionId)))
                .AsQueryable()
                .Skip((input.Page - 1) * GlobalConstants.Ui.BestSystemResultsSize)
                .Take(GlobalConstants.Ui.BestSystemResultsSize)
                .To<TOut>(new { miningPlanets = input.MiningPlanets })
                .ToList();
    }
}

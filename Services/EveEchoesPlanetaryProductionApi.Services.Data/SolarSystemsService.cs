namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Common.Extensions;
    using EveEchoesPlanetaryProductionApi.Data;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetBestPlanetaryResourcesById;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;

    public class SolarSystemsService : BestSystemService, ISolarSystemsService
    {
        private readonly IMapper mapper;
        private readonly IMemoryCache memoryCache;

        public SolarSystemsService(
            IMapper mapper,
            IItemsService itemsService,
            IMemoryCache memoryCache,
            EveEchoesPlanetaryProductionApiDbContext dbContext)
            : base(itemsService, dbContext)
        {
            this.mapper = mapper;
            this.memoryCache = memoryCache;
        }

        public async Task<SolarSystemServiceModel> GetRandomAsync()
        {
            var solarSystem = await this.DbContext.SolarSystems
                .OrderBy(ss => Guid.NewGuid())
                .To<SolarSystemServiceModel>()
                .FirstOrDefaultAsync();

            await this.PopulatePricesAsync(solarSystem);

            return solarSystem;
        }

        public async Task<SolarSystemServiceModel> GetByIdAsync(long id)
        {
            var solarSystem = await this.GetByIdAsync<SolarSystemServiceModel>(id);

            if (solarSystem is null)
            {
                return null;
            }

            await this.PopulatePricesAsync(solarSystem);

            return solarSystem;
        }

        public async Task<int> GetSolarSystemsCount()
        {
            var key = $"{nameof(SolarSystemsService)} Count";

            if (!this.memoryCache.TryGetValue(key, out int cacheEntry))
            {
                cacheEntry = await this.DbContext.SolarSystems.CountAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(GlobalConstants.InMemoryCachingSolarSystemCountInDays));

                this.memoryCache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }

        public async Task<IEnumerable<TOut>> GetAllAsync<TOut>(int pageSize, int page = 1)
            => await this.DbContext.SolarSystems
                .OrderBy(r => r.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .To<TOut>()
                .ToListAsync();

        public async Task<(IEnumerable<TOut> results, int count)> Search<TOut>(string searchTerm, int pageSize, int page = 1)
        {
            var query = this.DbContext.SolarSystems
                .Where(ss => ss.Name.ToLower().Contains(searchTerm.ToLower()));

            var count = await query.CountAsync();

            var results = await query
                .OrderBy(ss => ss.Name)
                .To<TOut>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (results, count);
        }

        public async Task<string> GetSolarSystemNameAsync(long id)
            => await this.DbContext.SolarSystems
                .Where(ss => ss.Id.Equals(id))
                .Select(ss => ss.Name)
                .FirstOrDefaultAsync();

        public async Task<SolarSystemBestModel> GetResourcesInSystemByIdAsync(long id, PriceSelector priceSelector)
        {
            var solarSystem = await this.GetByIdAsync<SolarSystemBestModel>(id);
            await this.PopulateSolarSystemResourcesPrice(priceSelector, solarSystem);

            return solarSystem;
        }

        public async Task<(int Count, IEnumerable<TOut> Systems)> GetBestSolarSystemInRange<TOut>(long solarSystemId, int range, BestInputModel input)
        {
            var systemsInRangeIds = await this.GetSolarSystemsInRangeIds(range, solarSystemId);

            var systems = (await this.GetBestSolarSystem(input, x => systemsInRangeIds.Contains(x.Id)))
                .AsQueryable()
                .Skip((input.Page - 1) * GlobalConstants.Ui.BestSystemResultsSize)
                .Take(GlobalConstants.Ui.BestSystemResultsSize)
                .To<TOut>(new { miningPlanets = input.MiningPlanets })
                .ToList();

            return (systemsInRangeIds.Count, systems);
        }

        public async Task<TOut> GetByIdAsync<TOut>(long id)
            => await this.GetAsync<TOut>(ss => ss.Id.Equals(id));

        public async Task<TOut> GetByNameAsync<TOut>(string name)
            => await this.GetAsync<TOut>(ss => ss.Name.Equals(name));

        public async Task<List<long>> GetSolarSystemsInRangeIds(int range, long solarSystemId)
        {
            var solarSystemNameParameter = new SqlParameter("@systemId", solarSystemId);
            var distanceParameter = new SqlParameter("@distance", range.ToString());

            var sql = "[graph].[AllNeighboursWithinGivenDistanceById] @systemId, @distance";
            var response = await this.DbContext.TargetSystems
                .FromSqlRaw(sql, solarSystemNameParameter, distanceParameter)
                .ToListAsync();

            var systemsInRangeIds = response
                .Select(r => r.Jumps.GetLastJump())
                .Select(long.Parse)
                .Distinct()
                .ToList();

            return systemsInRangeIds;
        }

        private async Task PopulatePricesAsync(SolarSystemServiceModel solarSystem)
        {
            var itemIds = solarSystem.Planets
                .SelectMany(p => p.PlanetResources)
                .Select(pr => pr.ItemId)
                .Distinct()
                .ToList();

            var itemPrices = await this.ItemsService.GetLatestItemsPricesAsync(itemIds);

            foreach (var planet in solarSystem.Planets)
            {
                foreach (var resource in planet.PlanetResources)
                {
                    resource.Price =
                        this.mapper.Map<SolarSystemServicePlanetPlanetResourcePriceModel>(itemPrices[resource.ItemId]);
                }
            }
        }

        private async Task PopulateSolarSystemResourcesPrice(PriceSelector priceSelector, SolarSystemBestModel solarSystem)
        {
            var planetaryResources = (await this.ItemsService.GetPlanetaryResources(priceSelector))
                .ToDictionary(x => x.Name, x => x);

            var updatedResources = solarSystem.PlanetResources.ToList();
            updatedResources.ForEach(ur => ur.Price = planetaryResources[ur.ItemName].Price);

            updatedResources = updatedResources
                .OrderByDescending(pr => pr.Price * (decimal)pr.Output)
                .DistinctBy(pr => pr.PlanetName)
                .ToList();

            solarSystem.PlanetResources = updatedResources;
        }

        private async Task<TOut> GetAsync<TOut>(Expression<Func<SolarSystem, bool>> predicate)
            => await this.DbContext.SolarSystems
                .Where(predicate)
                .To<TOut>()
                .FirstOrDefaultAsync();
    }
}

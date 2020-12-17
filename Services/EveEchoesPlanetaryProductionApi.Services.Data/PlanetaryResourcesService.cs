namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Data;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.IItemsService;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    using Microsoft.EntityFrameworkCore;

    public class PlanetaryResourcesService : IPlanetaryResourcesService
    {
        private readonly EveEchoesPlanetaryProductionApiDbContext dbContext;
        private readonly ISolarSystemsService solarSystemsService;
        private readonly IItemsService itemsService;

        public PlanetaryResourcesService(
            EveEchoesPlanetaryProductionApiDbContext dbContext,
            ISolarSystemsService solarSystemsService,
            IItemsService itemsService)
        {
            this.dbContext = dbContext;
            this.solarSystemsService = solarSystemsService;
            this.itemsService = itemsService;
        }

        public async Task<IEnumerable<string>> GetAllPlanetaryResources()
        {
            var planetaryResourcesIds = GlobalConstants.Items.GetPlanetaryResourcesIds().ToList();

            return await this.dbContext.Items
                .Where(i => planetaryResourcesIds.Contains(i.Id))
                .Select(i => i.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<TOut>> GetAllPlanetaryResources<TOut>()
        {
            var planetaryResourcesIds = GlobalConstants.Items.GetPlanetaryResourcesIds().ToList();

            return await this.dbContext.Items
                .Where(i => planetaryResourcesIds.Contains(i.Id))
                .To<TOut>()
                .ToListAsync();
        }

        public async Task<(int Count, IEnumerable<BestResourceServiceModel> Resources)> GetBestResourcesInConstellation(
            long constellationId,
            BestInputModel input)
        {
            var resources = await this.dbContext.Constellations
                .Where(c => c.Id.Equals(constellationId))
                .SelectMany(c => c.SolarSystems)
                .SelectMany(ss => ss.Planets)
                .SelectMany(p => p.PlanetResources)
                .To<BestResourceServiceModel>()
                .ToListAsync();

            await this.PopulatePrices(input, resources);

            var page = GetPage(input, resources, GlobalConstants.Ui.BestResourcesPageSize);

            return (resources.Count, page);
        }

        public async Task<(int Count, IEnumerable<BestResourceServiceModel> Resources)> GetBestResourcesInRegion(
            long regionId,
            BestInputModel input)
        {
            var resources = await this.dbContext.Regions
                .Where(r => r.Id.Equals(regionId))
                .SelectMany(r => r.SolarSystems)
                .SelectMany(ss => ss.Planets)
                .SelectMany(p => p.PlanetResources)
                .To<BestResourceServiceModel>()
                .ToListAsync();

            await this.PopulatePrices(input, resources);

            var page = GetPage(input, resources, GlobalConstants.Ui.BestResourcesPageSize);

            return (resources.Count, page);
        }

        public async Task<(int Count, IEnumerable<BestResourceServiceModel> Resources)> GetBestResourcesInRange(
            int range,
            long solarSystemId,
            BestInputModel input)
        {
            var resources = (await this
                .GetBestPlanetaryResourcesInRangeAsync<BestResourceServiceModel>(solarSystemId, range)).ToList();

            await this.PopulatePrices(input, resources);

            var page = GetPage(input, resources, GlobalConstants.Ui.BestResourcesPageSize);

            return (resources.Count, page);
        }

        private static IEnumerable<BestResourceServiceModel> GetPage(
            BestInputModel input,
            IEnumerable<BestResourceServiceModel> resources,
            int pageSize)
        {
            var pageOfResources = resources
                .OrderByDescending(i => i.ResourceValue)
                .Skip((input.Page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return pageOfResources;
        }

        private async Task PopulatePrices(BestInputModel input, IEnumerable<BestResourceServiceModel> resources)
        {
            Dictionary<long, ItemServiceModel> prices;

            if (input.PriceSelector is PriceSelector.UserProvided)
            {
                prices = (await this.itemsService.GetPlanetaryResources(input.Prices))
                    .ToDictionary(i => i.Id, i => i);
            }
            else
            {
                prices = (await this.itemsService.GetPlanetaryResources(input.PriceSelector))
                    .ToDictionary(i => i.Id, i => i);
            }

            foreach (var resource in resources)
            {
                resource.Price = prices[resource.ItemId].Price;
            }
        }

        private async Task<IEnumerable<TOut>> GetBestPlanetaryResourcesInRangeAsync<TOut>(long solarSystemId, int range)
        {
            var solarSystemsInRange = await this.solarSystemsService.GetSolarSystemsInRangeIds(range, solarSystemId);

            return await this.dbContext.PlanetsResources
                .Where(pr => solarSystemsInRange.Contains(pr.Planet.SolarSystemId))
                .To<TOut>()
                .ToListAsync();
        }
    }
}

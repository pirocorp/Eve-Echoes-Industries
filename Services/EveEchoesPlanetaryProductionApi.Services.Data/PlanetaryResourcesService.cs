﻿namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Data;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.IItemsService;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.PlanetaryResources;
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

        public async Task<IEnumerable<PlanetaryResourceServiceModel>> GetBestPlanetaryResourcesInRangeAsync(
            long solarSystemId, PriceSelector priceSelector, int range, int resourcesCount)
        {
            var solarSystemsInRange = await this.solarSystemsService.GetSolarSystemsInRangeIds(range, solarSystemId);

            var resources = await this.dbContext.PlanetsResources
                .Where(pr => solarSystemsInRange.Contains(pr.Planet.SolarSystemId))
                .To<PlanetaryResourceServiceModel>()
                .ToListAsync();

            var planetaryResourcesPrices = (await this.itemsService.GetPlanetaryResources(priceSelector))
                .ToDictionary(x => x.Id, x => x);

            foreach (var resource in resources)
            {
                resource.Price = planetaryResourcesPrices[resource.ItemId].Price;
            }

            return resources
                .OrderByDescending(x => x.ResourceValue)
                .Take(resourcesCount);
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

        public async Task<(int Count, IEnumerable<BestResourceServiceModel> Resources)> GetBestResourcesInConstellation(long constellationId, BestInputModel input)
        {
            var resources = await this.dbContext.Constellations
                .Where(c => c.Id.Equals(constellationId))
                .SelectMany(c => c.SolarSystems)
                .SelectMany(ss => ss.Planets)
                .SelectMany(p => p.PlanetResources)
                .To<BestResourceServiceModel>()
                .ToListAsync();

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

            var pageOfResources = resources
                .OrderByDescending(i => i.ResourceValue)
                .Skip((input.Page - 1) * GlobalConstants.Ui.BestResourcesPageSize)
                .Take(GlobalConstants.Ui.BestResourcesPageSize)
                .ToList();

            return (resources.Count, pageOfResources);
        }
    }
}

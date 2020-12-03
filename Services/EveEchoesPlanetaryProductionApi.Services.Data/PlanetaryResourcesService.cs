namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Data;
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

        public async Task<IEnumerable<PlanetaryResourceServiceModel>> GetBestPlanetaryResourcesInRangeAsync(long solarSystemId, PriceSelector priceSelector, int range, int resourcesCount)
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
    }
}

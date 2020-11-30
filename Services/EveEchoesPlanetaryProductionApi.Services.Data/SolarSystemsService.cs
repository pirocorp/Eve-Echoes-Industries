namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using AutoMapper;
    using Common;
    using EveEchoesPlanetaryProductionApi.Data;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetBestPlanetaryResourcesById;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById;
    using EveEchoesPlanetaryProductionApi.Services.EveEchoesMarket;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    using Microsoft.EntityFrameworkCore;

    public class SolarSystemsService : ISolarSystemsService
    {
        private readonly IMapper mapper;
        private readonly IItemsPricesService itemsPricesService;
        private readonly IItemsService itemsService;
        private readonly EveEchoesPlanetaryProductionApiDbContext dbContext;

        public SolarSystemsService(
            IMapper mapper,
            IItemsPricesService itemsPricesService,
            IItemsService itemsService,
            EveEchoesPlanetaryProductionApiDbContext dbContext)
        {
            this.mapper = mapper;
            this.itemsPricesService = itemsPricesService;
            this.itemsService = itemsService;
            this.dbContext = dbContext;
        }

        public async Task<SolarSystemServiceModel> GetByIdAsync(long id)
        {
            var solarSystem = await this.GetByIdAsync<SolarSystemServiceModel>(id);

            if (solarSystem is null)
            {
                return null;
            }

            var itemIds = solarSystem.Planets
                .SelectMany(p => p.PlanetResources)
                .Select(pr => pr.ItemId)
                .Distinct()
                .ToList();

            var itemPrices = await this.itemsPricesService.GetItemPricesAsync(itemIds);

            foreach (var planet in solarSystem.Planets)
            {
                foreach (var resource in planet.PlanetResources)
                {
                    resource.Price = this.mapper.Map<SolarSystemServicePlanetPlanetResourcePriceModel>(itemPrices[resource.ItemId]);
                }
            }

            return solarSystem;
        }

        public async Task<SolarSystemBestModel> GetBestPlanetaryResourcesByIdAsync(long id, PriceSelector priceSelector)
        {
            var solarSystem = await this.GetByIdAsync<SolarSystemBestModel>(id);
            var planetaryResources = (await this.itemsService.GetPlanetaryResources(priceSelector))
                .ToDictionary(x => x.Name, x => x);

            var updatedResources = solarSystem.PlanetResources.ToList();
            updatedResources.ForEach(ur => ur.Price = planetaryResources[ur.ItemName].Price);

            updatedResources = updatedResources
                .OrderByDescending(pr => pr.Price * (decimal) pr.Output)
                .DistinctBy(pr => pr.PlanetName)
                .ToList();

            solarSystem.PlanetResources = updatedResources;

            return solarSystem;
        }

        public async Task<TOut> GetByIdAsync<TOut>(long id)
            => await this.GetAsync<TOut>(ss => ss.Id.Equals(id));

        public async Task<TOut> GetByNameAsync<TOut>(string name)
            => await this.GetAsync<TOut>(ss => ss.Name.Equals(name));

        private async Task<TOut> GetAsync<TOut>(Expression<Func<SolarSystem, bool>> predicate)
            => await this.dbContext.SolarSystems
                .Where(predicate)
                .To<TOut>()
                .FirstOrDefaultAsync();
    }
}
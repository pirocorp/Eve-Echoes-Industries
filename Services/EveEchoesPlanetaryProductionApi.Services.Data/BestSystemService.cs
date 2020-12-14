﻿namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Data;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.IItemsService;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SystemsBestModel;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;
    using Microsoft.EntityFrameworkCore;

    public abstract class BestSystemService
    {
        private readonly IItemsService itemsService;

        protected BestSystemService(
            IItemsService itemsService,
            EveEchoesPlanetaryProductionApiDbContext dbContext)
        {
            this.itemsService = itemsService;
            this.DbContext = dbContext;
        }

        protected EveEchoesPlanetaryProductionApiDbContext DbContext { get; set; }

        protected async Task<IEnumerable<SystemBestModel>> GetBestSolarSystem(
            BestInputModel input,
            Expression<Func<SolarSystem, bool>> solarSystems)
        {
            if (input.PriceSelector is PriceSelector.UserProvided)
            {
                return await this.GetBestByUserProvidedPrices(input, solarSystems);
            }
            else
            {
                return await this.GetBestByExternalPrices(input, solarSystems);
            }
        }

        private static void PopulatePrices(IEnumerable<SystemBestModel> systems, IEnumerable<ItemServiceModel> prices)
            => systems
                .SelectMany(s => s.Planets)
                .SelectMany(s => s.Resources)
                .ToList()
                .ForEach(r => r.Price = prices.FirstOrDefault(p => p.Id.Equals(r.Id))?.Price ?? 0);

        private static List<SystemBestModel> OrderByValue(IList<SystemBestModel> systems, int miningPlanets)
        {
            foreach (var system in systems)
            {
                var planets = system.Planets.ToList();
                foreach (var planet in planets)
                {
                    planet.Resources = planet.Resources.OrderByDescending(r => r.Price * (decimal) r.Output).ToList();
                }

                system.Planets = planets
                    .OrderByDescending(p => p.Resources.Select(r => r.Price * (decimal) r.Output).FirstOrDefault())
                    .ToList();
            }

            return systems
                .OrderByDescending(s => s.Planets
                    .Select(p => p.Resources.Select(r => r.Price * (decimal) r.Output).FirstOrDefault())
                    .Take(miningPlanets)
                    .Sum())
                .ToList();
        }

        private async Task<IEnumerable<SystemBestModel>> GetBestByUserProvidedPrices(
            BestInputModel input,
            Expression<Func<SolarSystem, bool>> solarSystems)
        {
            var prices = await this.itemsService.GetPlanetaryResources(input.Prices);

            return await this.CalculateBest(input, prices, solarSystems);
        }

        private async Task<IEnumerable<SystemBestModel>> GetBestByExternalPrices(
            BestInputModel input,
            Expression<Func<SolarSystem, bool>> solarSystems)
        {
            var prices = await this.itemsService.GetPlanetaryResources(input.PriceSelector);

            return await this.CalculateBest(input, prices, solarSystems);
        }

        private async Task<IEnumerable<SystemBestModel>> CalculateBest(
            BestInputModel input,
            IEnumerable<ItemServiceModel> prices,
            Expression<Func<SolarSystem, bool>> solarSystems)
        {
            var systems = await this.DbContext.SolarSystems
                .Where(solarSystems)
                .To<SystemBestModel>()
                .ToListAsync();

            PopulatePrices(systems, prices);

            systems = OrderByValue(systems, input.MiningPlanets);

            return systems;
        }
    }
}

namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystemServiceModel;

    public class AppDataService : IAppDataService
    {
        public AppDataService()
        {
            this.PlanetaryResourcesPrices = new Dictionary<string, decimal>();
        }

        public SolarSystemServiceModel CurrentSolarSystem { get; set; }

        public int? RegionsCount { get; set; }

        public int? ConstellationsCount { get; set; }

        public int? SolarSystemCount { get; set; }

        public int? PlanetaryResourcesCount { get; set; }

        public IDictionary<string, decimal> PlanetaryResourcesPrices { get; set; }
    }
}
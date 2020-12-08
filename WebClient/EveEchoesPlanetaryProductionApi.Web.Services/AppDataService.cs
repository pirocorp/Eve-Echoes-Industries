namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById;

    public class AppDataService : IAppDataService
    {
        public AppDataService()
        {
            this.PlanetaryResourcesPrices = new Dictionary<string, decimal>();
        }

        public SolarSystemServiceModel CurrentSolarSystem { get; set; }

        public IDictionary<string, decimal> PlanetaryResourcesPrices { get; set; }
    }
}
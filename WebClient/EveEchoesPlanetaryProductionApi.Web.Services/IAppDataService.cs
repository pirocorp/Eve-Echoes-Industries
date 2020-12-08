namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Collections.Generic;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById;

    public interface IAppDataService
    {
        public SolarSystemServiceModel CurrentSolarSystem { get; set; }

        IDictionary<string, decimal> PlanetaryResourcesPrices { get; set; }
    }
}

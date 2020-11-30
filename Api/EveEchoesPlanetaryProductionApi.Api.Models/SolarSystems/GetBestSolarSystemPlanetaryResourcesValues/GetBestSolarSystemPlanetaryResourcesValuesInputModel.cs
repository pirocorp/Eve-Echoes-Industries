namespace EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetBestSolarSystemPlanetaryResourcesValues
{
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    public class GetBestSolarSystemPlanetaryResourcesValuesInputModel
    {
        public string PriceSelector { get; set; }

        public int MiningPlanets { get; set; }
    }
}

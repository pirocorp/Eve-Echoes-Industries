namespace EveEchoesPlanetaryProductionApi.Web.Models
{
    public class SolarSystemInfoPlanetPlanetaryResourceModel
    {
        public string Name { get; set; }

        public string Richness { get; set; }

        public double Output { get; set; }

        public SolarSystemInfoPlanetPlanetaryResourcePriceModel Price { get; set; }
    }
}

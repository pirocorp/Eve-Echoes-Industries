namespace EveEchoesPlanetaryProductionApi.Web.Models
{
    using System.Collections.Generic;

    public class SolarSystemInfoPlanetModel
    {
        public SolarSystemInfoPlanetModel()
        {
            this.PlanetResources = new List<SolarSystemInfoPlanetPlanetaryResourceModel>();
        }

        public string Name { get; set; }

        public string PlanetType { get; set; }

        public IEnumerable<SolarSystemInfoPlanetPlanetaryResourceModel> PlanetResources { get; set; }
    }
}

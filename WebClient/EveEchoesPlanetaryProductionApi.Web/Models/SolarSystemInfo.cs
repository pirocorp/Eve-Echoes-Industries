namespace EveEchoesPlanetaryProductionApi.Web.Models
{
    using System.Collections.Generic;

    public class SolarSystemInfo
    {
        public SolarSystemInfo()
        {
            this.Planets = new List<SolarSystemInfoPlanetModel>();
        }

        public string Name { get; set; }

        public string Constellation { get; set; }

        public string Region { get; set; }

        public IEnumerable<SolarSystemInfoPlanetModel> Planets { get; set; }
    }
}

namespace EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class SolarSystemServiceModel : IMapFrom<SolarSystem>
    {
        public SolarSystemServiceModel()
        {
            this.Planets = new List<SolarSystemServicePlanetModel>();
        }

        public string Name { get; set; }

        [JsonPropertyName("constellation")]
        public string ConstellationName { get; set; }

        [JsonPropertyName("region")]
        public string RegionName { get; set; }

        public IEnumerable<SolarSystemServicePlanetModel> Planets { get; set; }
    }
}

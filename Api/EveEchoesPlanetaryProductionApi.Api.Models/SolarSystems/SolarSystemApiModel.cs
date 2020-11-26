namespace EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class SolarSystemApiModel : IMapFrom<SolarSystem>
    {
        public SolarSystemApiModel()
        {
            this.Planets = new List<SolarSystemPlanetModel>();
        }

        public string Name { get; set; }

        [JsonPropertyName("constellation")]
        public string ConstellationName { get; set; }

        [JsonPropertyName("region")]
        public string RegionName { get; set; }

        public IEnumerable<SolarSystemPlanetModel> Planets { get; set; }
    }
}

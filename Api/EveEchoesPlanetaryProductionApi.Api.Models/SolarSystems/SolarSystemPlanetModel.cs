namespace EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class SolarSystemPlanetModel : IMapFrom<Planet>
    {
        public SolarSystemPlanetModel()
        {
            this.PlanetResources = new List<SolarSystemPlanetPlanetResourceModel>();
        }

        public string Name { get; set; }

        [JsonPropertyName("planetType")]
        public string PlanetTypeName { get; set; }

        public IEnumerable<SolarSystemPlanetPlanetResourceModel> PlanetResources { get; set; }
    }
}

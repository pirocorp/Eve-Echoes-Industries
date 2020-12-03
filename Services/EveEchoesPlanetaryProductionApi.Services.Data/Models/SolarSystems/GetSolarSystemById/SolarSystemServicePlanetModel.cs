namespace EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class SolarSystemServicePlanetModel : IMapFrom<Planet>
    {
        public SolarSystemServicePlanetModel()
        {
            this.PlanetResources = new List<SolarSystemServicePlanetPlanetResourceModel>();
        }

        public string Name { get; set; }

        [JsonPropertyName("planetType")]
        public string PlanetTypeName { get; set; }

        public IEnumerable<SolarSystemServicePlanetPlanetResourceModel> PlanetResources { get; set; }
    }
}

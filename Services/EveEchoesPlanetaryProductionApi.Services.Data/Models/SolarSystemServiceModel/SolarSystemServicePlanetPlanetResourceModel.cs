namespace EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetSolarSystemById
{
    using System.Text.Json.Serialization;

    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class SolarSystemServicePlanetPlanetResourceModel : IMapFrom<PlanetResource>
    {
        [JsonIgnore]
        public long ItemId { get; set; }

        [JsonPropertyName("name")]
        public string ItemName { get; set; }

        [JsonPropertyName("richness")]
        public string RichnessName { get; set; }

        public double Output { get; set; }

        public SolarSystemServicePlanetPlanetResourcePriceModel Price { get; set; }
    }
}

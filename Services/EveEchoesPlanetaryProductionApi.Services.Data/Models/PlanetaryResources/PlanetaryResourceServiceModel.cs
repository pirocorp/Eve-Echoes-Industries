namespace EveEchoesPlanetaryProductionApi.Services.Data.Models.PlanetaryResources
{
    using System.Text.Json.Serialization;

    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class PlanetaryResourceServiceModel : IMapFrom<PlanetResource>
    {
        [JsonPropertyName("solarSystem")]
        public string PlanetSolarSystemName { get; set; }

        [JsonPropertyName("planet")]
        public string PlanetName { get; set; }

        public long ItemId { get; set; }

        public string ItemName { get; set; }

        public double Output { get; set; }

        public decimal Price { get; set; }

        [IgnoreMap]
        public decimal ResourceValue => this.Price * (decimal)this.Output;
    }
}

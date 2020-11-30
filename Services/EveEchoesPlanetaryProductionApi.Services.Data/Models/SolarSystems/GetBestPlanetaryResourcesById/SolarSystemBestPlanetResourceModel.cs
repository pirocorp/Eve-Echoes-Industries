namespace EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetBestPlanetaryResourcesById
{
    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class SolarSystemBestPlanetResourceModel : IMapFrom<PlanetResource>
    {
        public string PlanetName { get; set; }

        public string ItemName { get; set; }

        public double Output { get; set; }

        [IgnoreMap]
        public decimal Price { get; set; }
    }
}

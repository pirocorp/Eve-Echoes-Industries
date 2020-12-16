namespace EveEchoesPlanetaryProductionApi.Api.Models.PlanetaryResources.GetAllPlanetResourcesWithPrices
{
    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    public class PlanetaryResource : IMapFrom<Item>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        [IgnoreMap]
        public ItemPrice Price { get; set; }
    }
}

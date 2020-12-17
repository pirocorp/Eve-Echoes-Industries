namespace EveEchoesPlanetaryProductionApi.Api.Models.PlanetaryResources.GetResources
{
    using AutoMapper;
    using Data.Models;
    using Services.Mapping;
    using Services.Models.EveEchoesMarket;

    public class PlanetaryResource : IMapFrom<Item>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        [IgnoreMap]
        public ItemPrice Price { get; set; }
    }
}

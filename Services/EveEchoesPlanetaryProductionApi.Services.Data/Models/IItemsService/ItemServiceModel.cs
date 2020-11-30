namespace EveEchoesPlanetaryProductionApi.Services.Data.Models.IItemsService
{
    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class ItemServiceModel : IMapFrom<Item>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        [IgnoreMap]
        public decimal Price { get; set; }
    }
}

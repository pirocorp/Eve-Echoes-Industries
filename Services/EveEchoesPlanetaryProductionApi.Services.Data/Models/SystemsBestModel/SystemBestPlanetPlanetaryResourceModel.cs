namespace EveEchoesPlanetaryProductionApi.Services.Data.Models.SystemsBestModel
{
    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class SystemBestPlanetPlanetaryResourceModel : IMapFrom<PlanetResource>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public string Item { get; set; }

        public string Richness { get; set; }

        public double Output { get; set; }

        [IgnoreMap]
        public decimal Price { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<PlanetResource, SystemBestPlanetPlanetaryResourceModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ItemId))
                .ForMember(d => d.Item, opt => opt.MapFrom(s => s.Item.Name))
                .ForMember(d => d.Richness, opt => opt.MapFrom(s => s.Richness.Name));
        }
    }
}

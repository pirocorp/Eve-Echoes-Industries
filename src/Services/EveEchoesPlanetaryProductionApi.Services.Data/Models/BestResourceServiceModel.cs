namespace EveEchoesPlanetaryProductionApi.Services.Data.Models
{
    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class BestResourceServiceModel : IMapFrom<PlanetResource>, IHaveCustomMappings
    {
        public string System { get; set; }

        public long SystemId { get; set; }

        public string Planet { get; set; }

        public long ItemId { get; set; }

        public string Item { get; set; }

        public double Output { get; set; }

        [IgnoreMap]
        public decimal Price { get; set; }

        [IgnoreMap]
        public decimal ResourceValue => this.Price * (decimal)this.Output;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<PlanetResource, BestResourceServiceModel>()
                .ForMember(d => d.System, opt => opt.MapFrom(s => s.Planet.SolarSystem.Name))
                .ForMember(d => d.SystemId, opt => opt.MapFrom(s => s.Planet.SolarSystem.Id))
                .ForMember(d => d.Planet, opt => opt.MapFrom(s => s.Planet.Name))
                .ForMember(d => d.Item, opt => opt.MapFrom(s => s.Item.Name));
        }
    }
}

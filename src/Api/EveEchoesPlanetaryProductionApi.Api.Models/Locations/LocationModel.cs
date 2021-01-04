namespace EveEchoesPlanetaryProductionApi.Api.Models.Locations
{
    using AutoMapper;

    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class LocationModel : IMapFrom<SolarSystem>, IHaveCustomMappings
    {
        public string System { get; set; }

        public long SystemId { get; set; }

        public string Constellation { get; set; }

        public long ConstellationId { get; set; }

        public string Region { get; set; }

        public long RegionId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<SolarSystem, LocationModel>()
                .ForMember(l => l.System, opt => opt.MapFrom(s => s.Name))
                .ForMember(l => l.SystemId, opt => opt.MapFrom(s => s.Id))
                .ForMember(l => l.Constellation, opt => opt.MapFrom(s => s.Constellation.Name))
                .ForMember(l => l.Region, opt => opt.MapFrom(s => s.Region.Name));
        }
    }
}

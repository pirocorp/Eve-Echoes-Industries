namespace EveEchoesPlanetaryProductionApi.Api.Models.Regions.GetRegions
{
    using System.Linq;

    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class RegionListingModel : IMapFrom<Region>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Constellations { get; set; }

        public int Systems { get; set; }

        public int Planets { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Region, RegionListingModel>()
                .ForMember(r => r.Constellations, opt => opt.MapFrom(r => r.Constellations.Count()))
                .ForMember(r => r.Systems, opt => opt.MapFrom(r => r.SolarSystems.Count()))
                .ForMember(r => r.Planets, opt => opt.MapFrom(r => r.Planets.Count()));
        }
    }
}

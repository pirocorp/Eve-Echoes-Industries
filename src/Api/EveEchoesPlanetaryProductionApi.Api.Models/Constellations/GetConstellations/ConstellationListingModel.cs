namespace EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetConstellations
{
    using System.Linq;

    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class ConstellationListingModel : IMapFrom<Constellation>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Systems { get; set; }

        public int Planets { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Constellation, ConstellationListingModel>()
                .ForMember(c => c.Systems, opt => opt.MapFrom(c => c.SolarSystems.Count()))
                .ForMember(c => c.Planets, opt => opt.MapFrom(c => c.Planets.Count()));
        }
    }
}

namespace EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSystems
{
    using System.Linq;

    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class SolarSystemListingModel : IMapFrom<SolarSystem>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Planets { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<SolarSystem, SolarSystemListingModel>()
                .ForMember(s => s.Planets, opt => opt.MapFrom(d => d.Planets.Count()));
        }
    }
}

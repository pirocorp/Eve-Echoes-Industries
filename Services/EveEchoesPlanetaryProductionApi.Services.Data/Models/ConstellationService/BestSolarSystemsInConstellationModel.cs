namespace EveEchoesPlanetaryProductionApi.Services.Data.Models.ConstellationService
{
    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class BestSolarSystemsInConstellationModel : BestSolarSystemsServiceModel, IMapFrom<SolarSystem>, IHaveCustomMappings
    {
        public int Count { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<SolarSystem, BestSolarSystemsInConstellationModel>()
                .ForMember(d => d.Count, opt => opt.MapFrom(s => s))
                .ForMember(d => d.Systems, opt => opt.MapFrom(s => s));
        }
    }
}

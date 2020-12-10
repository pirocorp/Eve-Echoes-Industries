namespace EveEchoesPlanetaryProductionApi.Services.Data.Models.ConstellationService.BestSolarSystemInConstellation
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SystemsBestModel;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class BestSolarSystemInConstellationModel : IMapFrom<Constellation>, IHaveCustomMappings
    {
        public int Count { get; set; }

        public IEnumerable<SystemBestModel> Systems { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Constellation, BestSolarSystemInConstellationModel>()
                .ForMember(d => d.Count, opt => opt.MapFrom(s => s.SolarSystems.Count()))
                .ForMember(d => d.Systems, opt => opt.MapFrom(s => s.SolarSystems));
        }
    }
}

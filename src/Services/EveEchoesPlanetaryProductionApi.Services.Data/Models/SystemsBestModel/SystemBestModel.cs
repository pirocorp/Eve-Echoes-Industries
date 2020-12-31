namespace EveEchoesPlanetaryProductionApi.Services.Data.Models.SystemsBestModel
{
    using System.Collections.Generic;

    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class SystemBestModel : IMapFrom<SolarSystem>, IHaveCustomMappings
    {
        public long Id { get; set; }

        public string System { get; set; }

        public IEnumerable<SystemBestPlanetModel> Planets { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<SolarSystem, SystemBestModel>()
                .ForMember(d => d.System, opt => opt.MapFrom(s => s.Name));
        }
    }
}

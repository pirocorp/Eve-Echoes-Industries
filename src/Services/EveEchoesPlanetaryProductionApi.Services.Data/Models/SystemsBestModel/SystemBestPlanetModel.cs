namespace EveEchoesPlanetaryProductionApi.Services.Data.Models.SystemsBestModel
{
    using System.Collections.Generic;

    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class SystemBestPlanetModel : IMapFrom<Planet>, IHaveCustomMappings
    {
        public SystemBestPlanetModel()
        {
            this.Resources = new List<SystemBestPlanetPlanetaryResourceModel>();
        }

        public string Planet { get; set; }

        public string Type { get; set; }

        public IEnumerable<SystemBestPlanetPlanetaryResourceModel> Resources { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Planet, SystemBestPlanetModel>()
                .ForMember(d => d.Planet, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Type, opt => opt.MapFrom(s => s.PlanetType.Name))
                .ForMember(d => d.Resources, opt => opt.MapFrom(s => s.PlanetResources));
        }
    }
}

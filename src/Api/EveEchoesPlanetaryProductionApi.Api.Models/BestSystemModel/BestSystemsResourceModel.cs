namespace EveEchoesPlanetaryProductionApi.Api.Models.BestSystemModel
{
    using System.Linq;

    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SystemsBestModel;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class BestSystemsResourceModel : IMapFrom<SystemBestPlanetModel>, IHaveCustomMappings
    {
        public string Planet { get; set; }

        public string Item { get; set; }

        public string Richness { get; set; }

        public double Output { get; set; }

        public decimal Price { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<SystemBestPlanetModel, BestSystemsResourceModel>()
                .ForMember(d => d.Item, opt => opt.MapFrom(s => s.Resources.First().Item))
                .ForMember(d => d.Richness, opt => opt.MapFrom(s => s.Resources.First().Richness))
                .ForMember(d => d.Output, opt => opt.MapFrom(s => s.Resources.First().Output))
                .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Resources.First().Price));
        }
    }
}

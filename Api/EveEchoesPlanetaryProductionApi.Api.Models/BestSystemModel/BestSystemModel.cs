namespace EveEchoesPlanetaryProductionApi.Api.Models.BestSystemModel
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SystemsBestModel;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class BestSystemModel : IMapFrom<SystemBestModel>, IHaveCustomMappings
    {
        public BestSystemModel()
        {
            this.Resources = new List<BestSystemsResourceModel>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public int Planets { get; set; }

        public decimal EstimatedValue { get; set; }

        public IEnumerable<BestSystemsResourceModel> Resources { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            var miningPlanets = default(int);

            configuration
                .CreateMap<SystemBestModel, BestSystemModel>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.System))
                .ForMember(d => d.Planets, opt => opt.MapFrom(s => s.Planets.Count()))
                .ForMember(d => d.Resources, opt => opt.MapFrom(s => s.Planets.Take(miningPlanets)))
                .ForMember(d => d.EstimatedValue, opt => opt
                    .MapFrom(s => s.Planets
                        .Select(p => p.Resources
                            .Select(r => r.Price * (decimal) r.Output)
                            .First())
                        .Take(miningPlanets)
                        .Sum()));
        }
    }
}

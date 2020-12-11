namespace EveEchoesPlanetaryProductionApi.Api.Models.Constellations.BestSolarSystemsInConstellation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SystemsBestModel;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class BestSystemsInConstellationModel : IMapFrom<SystemBestModel>, IHaveCustomMappings
    {
        public BestSystemsInConstellationModel()
        {
            this.Resources = new List<BestSystemsResourceInConstellationModel>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public int Planets { get; set; }

        public decimal EstimatedValue { get; set; }

        public IEnumerable<BestSystemsResourceInConstellationModel> Resources { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            var miningPlanets = default(int);

            configuration
                .CreateMap<SystemBestModel, BestSystemsInConstellationModel>()
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

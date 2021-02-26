namespace EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.Search
{
    using System;
    using System.Linq;

    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class SearchResultSolarSystemModel : IMapFrom<SolarSystem>, IHaveCustomMappings
    {
        private double security;

        public long Id { get; set; }

        public long RegionId { get; set; }

        public string Region { get; set; }

        public long ConstellationId { get; set; }

        public string Constellation { get; set; }

        public string Name { get; set; }

        public double Security
        {
            get => Math.Round(this.security, 1, MidpointRounding.AwayFromZero);
            set => this.security = value;
        }

        public int Planets { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<SolarSystem, SearchResultSolarSystemModel>()
                .ForMember(d => d.Region, opt => opt.MapFrom(s => s.Region.Name))
                .ForMember(d => d.Constellation, opt => opt.MapFrom(s => s.Constellation.Name))
                .ForMember(d => d.Planets, opt => opt.MapFrom(s => s.Planets.Count()));
        }
    }
}

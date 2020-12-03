namespace EveEchoesPlanetaryProductionApi.Services.Data.Models.SolarSystems.GetBestPlanetaryResourcesById
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;

    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class SolarSystemBestModel : IMapFrom<SolarSystem>, IHaveCustomMappings
    {
        public SolarSystemBestModel()
        {
            this.PlanetResources = new List<SolarSystemBestPlanetResourceModel>();
        }

        public string Name { get; set; }

        [JsonPropertyName("constellation")]
        public string ConstellationName { get; set; }

        [JsonPropertyName("region")]
        public string RegionName { get; set; }

        [IgnoreMap]
        public int MiningPlanets { get; set; }

        [IgnoreMap]
        public decimal SolarSystemValue =>
            this.PlanetResources
                .Take(this.MiningPlanets)
                .Sum(pr => (pr.Price * (decimal)pr.Output));

        public IEnumerable<SolarSystemBestPlanetResourceModel> PlanetResources { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SolarSystem, SolarSystemBestModel>()
                .ForMember(x => x.PlanetResources, opt =>
                {
                    opt.MapFrom(y => y.Planets.SelectMany(p => p.PlanetResources));
                });
        }
    }
}

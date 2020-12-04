namespace EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.Search
{
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class SearchResultSolarSystemModel : IMapFrom<SolarSystem>
    {
        public long Id { get; set; }

        public string RegionName { get; set; }

        public string ConstellationName { get; set; }

        public string Name { get; set; }

        public double Security { get; set; }
    }
}

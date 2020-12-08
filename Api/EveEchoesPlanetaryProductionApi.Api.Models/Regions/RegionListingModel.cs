namespace EveEchoesPlanetaryProductionApi.Api.Models.Regions
{
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class RegionListingModel : IMapFrom<Region>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int ConstellationsCount { get; set; }

        public int SolarSystemsCount { get; set; }

        public int PlanetsCount { get; set; }
    }
}

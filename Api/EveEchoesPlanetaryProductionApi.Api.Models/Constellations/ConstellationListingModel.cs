namespace EveEchoesPlanetaryProductionApi.Api.Models.Constellations
{
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class ConstellationListingModel : IMapFrom<Constellation>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int SolarSystemsCount { get; set; }

        public int PlanetsCount { get; set; }
    }
}

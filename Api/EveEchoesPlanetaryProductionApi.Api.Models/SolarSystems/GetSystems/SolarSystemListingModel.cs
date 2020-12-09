namespace EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSystems
{
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class SolarSystemListingModel : IMapFrom<SolarSystem>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int PlanetsCount { get; set; }
    }
}

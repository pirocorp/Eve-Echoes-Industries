namespace EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetSimpleDetails
{
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class ConstellationSimpleDetailsModel : IMapFrom<Constellation>
    {
        public string Name { get; set; }

        public string RegionName { get; set; }
    }
}

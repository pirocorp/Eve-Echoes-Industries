namespace EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetConstellation
{
    using System.Collections.Generic;

    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSystems;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class ConstellationDetails : IMapFrom<Constellation>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long RegionId { get; set; }

        public string RegionName { get; set; }

        public IEnumerable<SolarSystemListingModel> SolarSystems { get; set; }
    }
}

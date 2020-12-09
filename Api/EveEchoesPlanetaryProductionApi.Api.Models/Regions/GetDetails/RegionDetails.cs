namespace EveEchoesPlanetaryProductionApi.Api.Models.Regions.GetDetails
{
    using System.Collections.Generic;

    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class RegionDetails : IMapFrom<Region>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<RegionConstellationDetails> Constellations { get; set; }
    }
}

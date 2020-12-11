namespace EveEchoesPlanetaryProductionApi.Api.Models.Constellations.BestSolarSystemsInConstellation
{
    using System.Collections.Generic;

    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class BestConstellationModel : IMapFrom<Constellation>
    {
        [IgnoreMap]
        public IEnumerable<BestSystemsInConstellationModel> Systems { get; set; }
    }
}

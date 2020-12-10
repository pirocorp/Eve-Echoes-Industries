namespace EveEchoesPlanetaryProductionApi.Api.Models.Constellations.BestSolarSystemInConstellation
{
    using System.Collections.Generic;

    public class BestSolarSystemInConstellationModel
    {
        public int Count { get; set; }

        public IEnumerable<BestSolarSystemInConstellationSystemModel> Systems { get; set; }
    }
}

namespace EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetBestSystemInRange
{
    using System.Collections.Generic;

    using EveEchoesPlanetaryProductionApi.Api.Models.BestSystemModel;

    public class BestRangeModel
    {
        public int Count { get; set; }

        public IEnumerable<BestSystemModel> Systems { get; set; }
    }
}

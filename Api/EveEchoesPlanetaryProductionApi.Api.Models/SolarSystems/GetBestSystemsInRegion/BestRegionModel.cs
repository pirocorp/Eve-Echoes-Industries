namespace EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetBestSystemsInRegion
{
    using System.Collections.Generic;
    using BestSystemModel;

    public class BestRegionModel
    {
        public int Count { get; set; }

        public IEnumerable<BestSystemModel> Systems { get; set; }
    }
}

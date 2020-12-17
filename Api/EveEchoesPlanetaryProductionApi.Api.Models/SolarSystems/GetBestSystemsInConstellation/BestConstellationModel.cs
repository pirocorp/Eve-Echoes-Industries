namespace EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetBestSystemsInConstellation
{
    using System.Collections.Generic;
    using BestSystemModel;

    public class BestConstellationModel
    {
        public IEnumerable<BestSystemModel> Systems { get; set; }
    }
}

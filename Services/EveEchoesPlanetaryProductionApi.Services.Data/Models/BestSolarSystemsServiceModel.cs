namespace EveEchoesPlanetaryProductionApi.Services.Data.Models
{
    using System.Collections.Generic;

    using EveEchoesPlanetaryProductionApi.Services.Data.Models.SystemsBestModel;

    public class BestSolarSystemsServiceModel
    {
        public IEnumerable<SystemBestModel> Systems { get; set; }
    }
}

﻿namespace EveEchoesPlanetaryProductionApi.Api.Models.Regions.GetBestSolarSystemsInRegionAsync
{
    using System.Collections.Generic;

    using EveEchoesPlanetaryProductionApi.Api.Models.BestSystemModel;

    public class BestRegionModel
    {
        public IEnumerable<BestSystemModel> Systems { get; set; }
    }
}

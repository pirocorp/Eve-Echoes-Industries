namespace EveEchoesPlanetaryProductionApi.Api.Models.PlanetaryResources
{
    using System.Collections.Generic;

    using EveEchoesPlanetaryProductionApi.Services.Data.Models;

    public class BestPlanetaryResourcesModel
    {
        public int Count { get; set; }

        public IEnumerable<BestResourceServiceModel> Resources { get; set; }
    }
}

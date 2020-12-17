namespace EveEchoesPlanetaryProductionApi.Api.Models.PlanetaryResources.BestPlanetaryResourcesInConstellation
{
    using System.Collections.Generic;

    using EveEchoesPlanetaryProductionApi.Services.Data.Models;

    public class BestPlanetaryResourcesInConstellationModel
    {
        public int Count { get; set; }

        public IEnumerable<BestResourceServiceModel> Resources { get; set; }
    }
}

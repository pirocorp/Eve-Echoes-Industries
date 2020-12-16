namespace EveEchoesPlanetaryProductionApi.Api.Models.PlanetaryResources.GetAllPlanetResourcesWithPrices
{
    using System.Collections.Generic;

    public class GetAllPlanetResourcesWithPricesModel
    {
        public IEnumerable<PlanetaryResource> Resources { get; set; }
    }
}

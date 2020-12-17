namespace EveEchoesPlanetaryProductionApi.Api.Models.PlanetaryResources.GetResources
{
    using System.Collections.Generic;

    public class GetAllPlanetResourcesWithPricesModel
    {
        public IEnumerable<PlanetaryResource> Resources { get; set; }
    }
}

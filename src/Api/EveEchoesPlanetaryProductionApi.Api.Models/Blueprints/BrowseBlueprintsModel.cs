namespace EveEchoesPlanetaryProductionApi.Api.Models.Blueprints
{
    using System.Collections.Generic;

    public class BrowseBlueprintsModel
    {
        public int Count { get; set; }

        public IEnumerable<BlueprintListingModel> Blueprints { get; set; }
    }
}

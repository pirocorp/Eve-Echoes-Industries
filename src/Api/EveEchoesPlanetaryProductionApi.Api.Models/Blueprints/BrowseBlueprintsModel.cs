namespace EveEchoesPlanetaryProductionApi.Api.Models.Blueprints
{
    using System.Collections.Generic;

    public class BrowseBlueprintsModel
    {
        public int Count { get; init; }

        public IEnumerable<BlueprintListingModel> Blueprints { get; set; }

        public void Deconstruct(out int count, out IEnumerable<BlueprintListingModel> blueprints)
        {
            count = this.Count;
            blueprints = this.Blueprints;
        }
    }
}

namespace EveEchoesPlanetaryProductionApi.Api.Models.Blueprints
{
    using System.Collections.Generic;

    public class BrowseBlueprintsInputModel
    {
        public IEnumerable<long> Types { get; set; }
    }
}

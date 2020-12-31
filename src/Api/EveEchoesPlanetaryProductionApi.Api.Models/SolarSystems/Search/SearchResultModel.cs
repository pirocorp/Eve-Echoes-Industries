namespace EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.Search
{
    using System.Collections.Generic;

    public class SearchResultModel
    {
        public int Count { get; set; }

        public IEnumerable<SearchResultSolarSystemModel> Results { get; set; }
    }
}

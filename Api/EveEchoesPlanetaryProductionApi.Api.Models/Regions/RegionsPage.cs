namespace EveEchoesPlanetaryProductionApi.Api.Models.Regions
{
    using System.Collections.Generic;

    public class RegionsPage
    {
        public IEnumerable<RegionListingModel> Regions { get; set; }
    }
}

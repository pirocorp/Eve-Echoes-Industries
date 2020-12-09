namespace EveEchoesPlanetaryProductionApi.Api.Models.Regions
{
    using System.Collections.Generic;

    public class RegionsPage
    {
        public RegionsPage()
        {
            this.Regions = new List<RegionListingModel>();
        }

        public IEnumerable<RegionListingModel> Regions { get; set; }
    }
}

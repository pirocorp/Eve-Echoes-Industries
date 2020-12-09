namespace EveEchoesPlanetaryProductionApi.Api.Models.Constellations
{
    using System.Collections.Generic;

    public class ConstellationPage
    {
        public ConstellationPage()
        {
            this.Constellations = new List<ConstellationListingModel>();
        }

        public IEnumerable<ConstellationListingModel> Constellations { get; set; }
    }
}

namespace EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetConstellations
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

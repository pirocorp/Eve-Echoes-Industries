namespace EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSystems
{
    using System.Collections.Generic;

    public class SystemsPageModel
    {
        public SystemsPageModel()
        {
            this.Systems = new List<SolarSystemListingModel>();
        }

        public IEnumerable<SolarSystemListingModel> Systems { get; set; }
    }
}

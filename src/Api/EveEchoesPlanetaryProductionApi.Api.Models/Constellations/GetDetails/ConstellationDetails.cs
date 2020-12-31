namespace EveEchoesPlanetaryProductionApi.Api.Models.Constellations.GetDetails
{
    using System.Collections.Generic;

    using EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSystems;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class ConstellationDetails : IMapFrom<Constellation>
    {
        public IEnumerable<SolarSystemListingModel> SolarSystems { get; set; }
    }
}

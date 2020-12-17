namespace EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSimpleDetails
{
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class SolarSystemSimpleDetailsModel : IMapFrom<SolarSystem>
    {
        public string Name { get; set; }
    }
}

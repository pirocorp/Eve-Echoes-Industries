namespace EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems.GetSolarSystemSimpleDetails
{
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class SolarSystemSimpleDetailsModel : IMapFrom<SolarSystem>
    {
        public string Name { get; set; }
    }
}

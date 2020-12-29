namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    public interface IEveApiService
    {
        ISolarSystemsProvider Systems { get; }

        IConstellationsProvider Constellations { get; }

        IRegionsProvider Regions { get; }

        IPlanetaryResourcesProvider PlanetaryResources { get; }
    }
}

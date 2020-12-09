namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    public interface IEveApiService
    {
        ISolarSystemsProvider Systems { get; set; }

        IConstellationsProvider Constellations { get; set; }

        IRegionsProvider Regions { get; set; }
    }
}
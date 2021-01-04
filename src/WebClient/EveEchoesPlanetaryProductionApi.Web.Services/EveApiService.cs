namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    public class EveApiService : IEveApiService
    {
        public EveApiService(
            ISolarSystemsProvider solarSystemsProvider,
            IConstellationsProvider constellationsProvider,
            IRegionsProvider regionsProvider,
            IPlanetaryResourcesProvider planetaryResourcesProvider,
            ILocationProvider locationProvider)
        {
            this.Systems = solarSystemsProvider;
            this.Constellations = constellationsProvider;
            this.Regions = regionsProvider;
            this.PlanetaryResources = planetaryResourcesProvider;
            this.Locations = locationProvider;
        }

        public ISolarSystemsProvider Systems { get; }

        public IConstellationsProvider Constellations { get; }

        public IRegionsProvider Regions { get; }

        public IPlanetaryResourcesProvider PlanetaryResources { get; }

        public ILocationProvider Locations { get; }
    }
}

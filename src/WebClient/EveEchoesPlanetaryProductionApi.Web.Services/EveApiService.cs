namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    public class EveApiService : IEveApiService
    {
        public EveApiService(
            ISolarSystemsProvider solarSystemsProvider,
            IConstellationsProvider constellationsProvider,
            IRegionsProvider regionsProvider,
            IPlanetaryResourcesProvider planetaryResourcesProvider,
            ILocationProvider locationProvider,
            IBlueprintsProvider blueprintsProvider,
            IItemsProvider itemsProvider)
        {
            this.Systems = solarSystemsProvider;
            this.Constellations = constellationsProvider;
            this.Regions = regionsProvider;
            this.PlanetaryResources = planetaryResourcesProvider;
            this.Locations = locationProvider;
            this.Blueprints = blueprintsProvider;
            this.Items = itemsProvider;
        }

        public ISolarSystemsProvider Systems { get; }

        public IConstellationsProvider Constellations { get; }

        public IRegionsProvider Regions { get; }

        public IPlanetaryResourcesProvider PlanetaryResources { get; }

        public ILocationProvider Locations { get; }

        public IBlueprintsProvider Blueprints { get; }

        public IItemsProvider Items { get; }
    }
}

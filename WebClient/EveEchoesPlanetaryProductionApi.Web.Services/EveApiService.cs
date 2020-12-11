namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    public class EveApiService : IEveApiService
    {
        public EveApiService(
            ISolarSystemsProvider solarSystemsProvider,
            IConstellationsProvider constellationsProvider,
            IRegionsProvider regionsProvider, 
            IPlanetaryResourcesProvider planetaryResourcesProvider)
        {
            this.Systems = solarSystemsProvider;
            this.Constellations = constellationsProvider;
            this.Regions = regionsProvider;
            this.PlanetaryResources = planetaryResourcesProvider;
        }

        public ISolarSystemsProvider Systems { get; }

        public IConstellationsProvider Constellations { get; }

        public IRegionsProvider Regions { get; }

        public IPlanetaryResourcesProvider PlanetaryResources { get; }
    }
}
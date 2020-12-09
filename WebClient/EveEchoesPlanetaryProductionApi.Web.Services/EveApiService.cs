namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    public class EveApiService : IEveApiService
    {
        public EveApiService(
            ISolarSystemsProvider solarSystemsProvider,
            IConstellationsProvider constellationsProvider,
            IRegionsProvider regionsProvider)
        {
            this.Systems = solarSystemsProvider;
            this.Constellations = constellationsProvider;
            this.Regions = regionsProvider;
        }

        public ISolarSystemsProvider Systems { get; set; }

        public IConstellationsProvider Constellations { get; set; }

        public IRegionsProvider Regions { get; set; }
    }
}
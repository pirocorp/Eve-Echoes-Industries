namespace EveEchoesPlanetaryProductionApi.Common
{
    public static class GlobalConstants
    {
        public const char CsvDelimiter = ',';

        public const string RoutePathDelimiter = "->";

        public const string MarketApiHostName = "https://api.eve-echoes-market.com";

        public const string MarketApiItemEndPoint = "market-stats";

        public const int MarketApiCachingIntervalInMinutes = 30;

        public static class FilePaths
        {
            public const string ItemsCsvFilePath = "../../Data/EveEchoesPlanetaryProductionApi.Data/Static Data/Items.csv";

            public const string PlanetsResourcesCsvFilePath = "../../Data/EveEchoesPlanetaryProductionApi.Data/Static Data/Planets Resources.csv";

            public const string PlanetTypesCsvFilePath = "../../Data/EveEchoesPlanetaryProductionApi.Data/Static Data/Planet Types.csv";

            public const string RichnessCsvFilePath = "../../Data/EveEchoesPlanetaryProductionApi.Data/Static Data/Richness.csv";

            public const string RegionsCsvFilePath = "../../Data/EveEchoesPlanetaryProductionApi.Data/Static Data/Regions.csv";

            public const string ConstellationsCsvFilePath = "../../Data/EveEchoesPlanetaryProductionApi.Data/Static Data/Constellations.csv";

            public const string SolarSystemsCsvFilePath = "../../Data/EveEchoesPlanetaryProductionApi.Data/Static Data/SolarSystems.csv";

            public const string PlanetsCsvFilePath = "../../Data/EveEchoesPlanetaryProductionApi.Data/Static Data/Planets.csv";

            public const string RegionsJumpsCsvFilePath = "../../Data/EveEchoesPlanetaryProductionApi.Data/Static Data/RegionsJumps.csv";

            public const string ConstellationsJumpsCsvFilePath = "../../Data/EveEchoesPlanetaryProductionApi.Data/Static Data/ConstellationsJumps.csv";

            public const string SolarSystemsJumpsCsvFilePath = "../../Data/EveEchoesPlanetaryProductionApi.Data/Static Data/SolarSystemsJumps.csv";
        }
    }
}

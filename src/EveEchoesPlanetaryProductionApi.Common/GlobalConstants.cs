namespace EveEchoesPlanetaryProductionApi.Common
{
    using System.Collections.Generic;

    public static class GlobalConstants
    {
        public const char CsvDelimiter = ',';

        public const string RoutePathDelimiter = "->";

        public const string MarketApiHostName = "https://api.eve-echoes-market.com";

        public const string MarketApiItemEndPoint = "market-stats";

        public const int MarketApiCachingIntervalInMinutes = 60;

        public const int InMemoryPlanetaryResourcesCachingInSeconds = 15 * 60;

        public const int InMemoryCachingRegionsCountInDays = 1;

        public const int InMemoryCachingConstellationsCountInDays = 1;

        public const int InMemoryCachingSolarSystemCountInDays = 1;

        public const int MaxRange = 15;

        public const int MinRange = 2;

        public const string JsonContentType = "application/json";

        public static class Items
        {
            public const long PlanetaryResourcesStartId = 42001000000;

            public const long PlanetaryResourcesEndId = 42002000017;

            private const long PlanetaryResourcesPartOneEndId = 42001000033;

            private const long PlanetaryResourcesPartTwoStartId = 42002000012;

            public static IEnumerable<long> GetPlanetaryResourcesIds()
            {
                for (var i = PlanetaryResourcesStartId; i <= PlanetaryResourcesPartOneEndId; i++)
                {
                    yield return i;
                }

                for (var i = PlanetaryResourcesPartTwoStartId; i <= PlanetaryResourcesEndId; i++)
                {
                    yield return i;
                }
            }
        }

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

            public const string ItemTypesCsvFilePath = "../../Data/EveEchoesPlanetaryProductionApi.Data/Static Data/ItemTypes.csv";

            public const string ConfirmEmailTemplate = "./Resources/ConfirmEmailTemplate.html";
        }

        public static class Ui
        {
            public const int SolarSystemsSearchPageSize = 10;

            public const int RegionsPageSize = 10;

            public const int ConstellationsPageSize = 10;

            public const int BestSystemResultsSize = 10;

            public const int BestResourcesPageSize = 10;

            public const int MaxColonies = 6;
        }

        public static class Email
        {
            public const string From = "admin@echoesindustries.com";
            public const string FromName = "Admin";
            public const string Subject = "Email confirmation";
            public const string EmailValidationPath = "/account/confirm";
            public const string EmailValidationParameter = "token";
        }
    }
}

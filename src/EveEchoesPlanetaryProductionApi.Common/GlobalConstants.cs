namespace EveEchoesPlanetaryProductionApi.Common
{
    using System;
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
            public const string ConfirmEmailTemplate = "./Resources/ConfirmEmailTemplate.html";

            private const string AspCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

            public static string ItemsCsvFilePath => GeneratePath("Items.csv");

            public static string BluePrintsCsvFilePath => GeneratePath("Blueprints.csv");

            public static string SolarSystemsJumpsCsvFilePath => GeneratePath("SolarSystemsJumps.csv");

            public static string PlanetsResourcesCsvFilePath => GeneratePath("Planets Resources.csv");

            public static string PlanetTypesCsvFilePath => GeneratePath("Planet Types.csv");

            public static string RichnessCsvFilePath => GeneratePath("Richness.csv");

            public static string RegionsCsvFilePath => GeneratePath("Regions.csv");

            public static string ConstellationsCsvFilePath => GeneratePath("Constellations.csv");

            public static string SolarSystemsCsvFilePath => GeneratePath("SolarSystems.csv");

            public static string PlanetsCsvFilePath => GeneratePath("Planets.csv");

            public static string RegionsJumpsCsvFilePath => GeneratePath("RegionsJumps.csv");

            public static string ConstellationsJumpsCsvFilePath => GeneratePath("ConstellationsJumps.csv");

            public static string ItemTypesCsvFilePath => GeneratePath("ItemTypes.csv");

            private static string GeneratePath(string fileName)
                => (Environment.GetEnvironmentVariable(AspCoreEnvironment) ?? string.Empty) == "Development"
                    ? $"../../Data/EveEchoesPlanetaryProductionApi.Data/Static Data/{fileName}"
                    : $"./Static Data/{fileName}";
        }

        public static class Ui
        {
            public const int SolarSystemsSearchPageSize = 10;

            public const int RegionsPageSize = 10;

            public const int ConstellationsPageSize = 10;

            public const int BestSystemResultsSize = 10;

            public const int BestResourcesPageSize = 10;

            public const int BlueprintsPageSize = 10;

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

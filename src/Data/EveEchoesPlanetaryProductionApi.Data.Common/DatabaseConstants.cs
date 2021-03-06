﻿namespace EveEchoesPlanetaryProductionApi.Data.Common
{
    public class DatabaseConstants
    {
        public const int EntitiesPropertyNameLength = 100;

        public const double MinSecurityLevel = -1;

        public const double MaxSecurityLevel = 1;

        public const int MinTechLevel = 1;

        public const int MaxTechLevel = 10;

        public class SeedingConstants
        {
            public const string ItemErrorParseMessage = "Item {0} cannot be parsed.";

            public const string RegionErrorParseMessage = "Region {0} cannot be parsed.";

            public const string ConstellationErrorParseMessage = "Constellation {0} cannot be parsed.";

            public const string SolarSystemErrorParseMessage = "Solar system {0} cannot be parsed.";
        }
    }
}

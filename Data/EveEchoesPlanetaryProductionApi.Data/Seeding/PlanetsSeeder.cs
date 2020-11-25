namespace EveEchoesPlanetaryProductionApi.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class PlanetsSeeder : ISeeder
    {
        public async Task SeedAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Planets.AnyAsync())
            {
                return;
            }

            var logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger(typeof(EveEchoesPlanetaryProductionApiDbContextSeeder));

            await SeedPlanetsAsync(dbContext, logger);
        }

        private static async Task SeedPlanetsAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, ILogger logger)
        {
            var planets = new List<Planet>();

            await foreach (var line in CsvFileService.ReadCsvDataLineByLineAsync(GlobalConstants.FilePaths.PlanetsCsvFilePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var lineArgs = line.Split(GlobalConstants.CsvDelimiter, StringSplitOptions.RemoveEmptyEntries);

                var planetIdParseSuccess = long.TryParse(lineArgs[0], out var planetId);
                var regionIdParseSuccess = long.TryParse(lineArgs[1], out var regionId);
                var constellationIdParseSuccess = long.TryParse(lineArgs[2], out var constellationId);
                var solarSystemIdParseSuccess = long.TryParse(lineArgs[3], out var solarSystemId);
                var planetName = lineArgs[4];
                var planetTypeIdParseSuccess = long.TryParse(lineArgs[5], out var planetTypeId);

                if (!planetIdParseSuccess
                    || !regionIdParseSuccess
                    || !constellationIdParseSuccess
                    || !solarSystemIdParseSuccess
                    || !planetTypeIdParseSuccess)
                {
                    logger.LogWarning($"Can't parse planet");
                    logger.LogWarning(line);
                }

                var planet = new Planet()
                {
                    Id = planetId,
                    RegionId = regionId,
                    ConstellationId = constellationId,
                    SolarSystemId = solarSystemId,
                    Name = planetName,
                    PlanetTypeId = planetTypeId,
                };

                planets.Add(planet);
            }

            await dbContext.AddRangeAsync(planets);
            await dbContext.SaveChangesWithExplicitIdentityInsertAsync(nameof(EveEchoesPlanetaryProductionApiDbContext
                .Planets));
        }
    }
}

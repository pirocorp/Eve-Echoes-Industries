namespace EveEchoesPlanetaryProductionApi.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Data.Common;
    using EveEchoesPlanetaryProductionApi.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class SolarSystemsSeeder : ISeeder
    {
        public async Task SeedAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, IServiceProvider serviceProvider)
        {
            var logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger(typeof(EveEchoesPlanetaryProductionApiDbContextSeeder));

            if (await dbContext.SolarSystems.AnyAsync())
            {
                return;
            }

            await SeedSolarSystemsAsync(dbContext, logger);
        }

        private static async Task SeedSolarSystemsAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, ILogger logger)
        {
            await foreach (var line in CsvFileService.ReadCsvDataLineByLineAsync(GlobalConstants.FilePaths.SolarSystemsCsvFilePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var lineArgs = line.Split(GlobalConstants.CsvDelimiter, StringSplitOptions.RemoveEmptyEntries);

                var regionIdParseSuccess = long.TryParse(lineArgs[0], out var regionId);
                var constellationParseSuccess = long.TryParse(lineArgs[1], out var constellationId);
                var solarSystemSuccess = long.TryParse(lineArgs[2], out var solarSystemId);
                var solarSystemName = lineArgs[3];

                var security = double.Parse(lineArgs[21]);

                if (!regionIdParseSuccess
                    || !constellationParseSuccess
                    || !solarSystemSuccess)
                {
                    logger.LogWarning(string.Format(
                        DatabaseConstants.SeedingConstants.SolarSystemErrorParseMessage,
                        solarSystemName));
                    logger.LogWarning(line);
                    continue;
                }

                var solarSystem = new SolarSystem()
                {
                    RegionId = regionId,
                    ConstellationId = constellationId,
                    Id = solarSystemId,
                    Name = solarSystemName,
                    Security = security,
                };

                await dbContext.AddAsync(solarSystem);
            }

            await dbContext.SaveChangesWithExplicitIdentityInsertAsync(nameof(EveEchoesPlanetaryProductionApiDbContext
                .SolarSystems));
        }
    }
}

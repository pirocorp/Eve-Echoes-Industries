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

    public class ConstellationsSeeder : ISeeder
    {
        public async Task SeedAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Constellations.AnyAsync())
            {
                return;
            }

            var logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger(typeof(EveEchoesPlanetaryProductionApiDbContextSeeder));

            await SeedConstellationsAsync(dbContext, logger);
        }

        private static async Task SeedConstellationsAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, ILogger logger)
        {
            await foreach (var line in CsvFileService.ReadCsvDataLineByLineAsync(GlobalConstants.FilePaths
                .ConstellationsCsvFilePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var lineArgs = line.Split(GlobalConstants.CsvDelimiter, StringSplitOptions.RemoveEmptyEntries);

                var regionIdParseSuccess = long.TryParse(lineArgs[0], out var regionId);
                var constellationParseSuccess = long.TryParse(lineArgs[1], out var constellationId);
                var constellationName = lineArgs[2];

                if (!regionIdParseSuccess || !constellationParseSuccess)
                {
                    logger.LogWarning(string.Format(DatabaseConstants.SeedingConstants.ConstellationErrorParseMessage,
                        constellationName));
                    logger.LogWarning(line);
                    continue;
                }

                var constellation = new Constellation()
                {
                    RegionId = regionId,
                    Id = constellationId,
                    Name = constellationName,
                };

                await dbContext.AddAsync(constellation);
            }

            await dbContext.SaveChangesWithExplicitIdentityInsertAsync(nameof(EveEchoesPlanetaryProductionApiDbContext
                .Constellations));
        }
    }
}

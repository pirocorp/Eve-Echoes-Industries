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

    public class RegionSeeder : ISeeder
    {
        public async Task SeedAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Regions.AnyAsync())
            {
                return;
            }

            var logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger(typeof(EveEchoesPlanetaryProductionApiDbContextSeeder));

            await SeedRegionsAsync(dbContext, logger);
        }

        private static async Task SeedRegionsAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, ILogger logger)
        {
            await foreach (var line in CsvFileService.ReadCsvDataLineByLineAsync(GlobalConstants.FilePaths.RegionsCsvFilePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var lineArgs = line.Split(GlobalConstants.CsvDelimiter, StringSplitOptions.RemoveEmptyEntries);

                var success = long.TryParse(lineArgs[0], out var regionId);
                var regionName = lineArgs[1];

                if (!success)
                {
                    logger.LogWarning(string.Format(DatabaseConstants.SeedingConstants.RegionErrorParseMessage, regionName));
                    logger.LogWarning(line);
                    continue;
                }

                var region = new Region()
                {
                    Id = regionId,
                    Name = regionName,
                };

                await dbContext.AddAsync(region);
            }

            await dbContext.SaveChangesWithExplicitIdentityInsertAsync(nameof(EveEchoesPlanetaryProductionApiDbContext
                .Regions));
        }
    }
}

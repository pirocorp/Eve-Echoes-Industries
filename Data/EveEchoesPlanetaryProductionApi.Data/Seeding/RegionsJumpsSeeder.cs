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

    public class RegionsJumpsSeeder : ISeeder
    {
        public async Task SeedAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.RegionsJumps.AnyAsync())
            {
                return;
            }

            var logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger(typeof(EveEchoesPlanetaryProductionApiDbContextSeeder));

            await SeedRegionJumps(dbContext, logger);
        }

        private static async Task SeedRegionJumps(EveEchoesPlanetaryProductionApiDbContext dbContext, ILogger logger)
        {
            var regionsJumps = new List<RegionJump>();

            await foreach (var line in CsvFileService.ReadCsvDataLineByLineAsync(GlobalConstants.FilePaths.RegionsJumpsCsvFilePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var lineArgs = line.Split(GlobalConstants.CsvDelimiter, StringSplitOptions.RemoveEmptyEntries);

                var fromRegionIdSuccess = long.TryParse(lineArgs[0], out var fromRegionId);
                var toRegionIdSuccess = long.TryParse(lineArgs[1], out var toRegionId);

                if (!fromRegionIdSuccess
                    || !toRegionIdSuccess)
                {
                    logger.LogWarning($"Can't parse region jump");
                    logger.LogWarning(line);
                    continue;
                }

                var regionJump = new RegionJump()
                {
                    FromRegionId = fromRegionId,
                    ToRegionId = toRegionId,
                };

                regionsJumps.Add(regionJump);
            }

            await dbContext.AddRangeAsync(regionsJumps);
            await dbContext.SaveChangesAsync();
        }
    }
}

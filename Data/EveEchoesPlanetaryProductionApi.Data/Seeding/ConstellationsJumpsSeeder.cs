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

    public class ConstellationsJumpsSeeder : ISeeder
    {
        public async Task SeedAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.ConstellationsJumps.AnyAsync())
            {
                return;
            }

            var logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger(typeof(EveEchoesPlanetaryProductionApiDbContextSeeder));

            await SeedConstellationsJumps(dbContext, logger);
        }

        private static async Task SeedConstellationsJumps(EveEchoesPlanetaryProductionApiDbContext dbContext, ILogger logger)
        {
            var constellationsJumps = new List<ConstellationJump>();

            await foreach (var line in CsvFileService.ReadCsvDataLineByLineAsync(GlobalConstants.FilePaths.ConstellationsJumpsCsvFilePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var lineArgs = line.Split(GlobalConstants.CsvDelimiter, StringSplitOptions.RemoveEmptyEntries);

                var fromRegionIdSuccess = long.TryParse(lineArgs[0], out var fromRegionId);
                var fromConstellationIdSuccess = long.TryParse(lineArgs[1], out var fromConstellationId);
                var toConstellationIdSuccess = long.TryParse(lineArgs[2], out var toConstellationId);
                var toRegionIdSuccess = long.TryParse(lineArgs[3], out var toRegionId);

                if (!fromRegionIdSuccess
                    || !toRegionIdSuccess
                    || !fromConstellationIdSuccess
                    || !toConstellationIdSuccess)
                {
                    logger.LogWarning($"Can't parse constellation jump");
                    logger.LogWarning(line);
                    continue;
                }

                var constellationJump = new ConstellationJump()
                {
                    FromRegionId = fromRegionId,
                    ToRegionId = toRegionId,
                    FromConstellationId = fromConstellationId,
                    ToConstellationId = toConstellationId,
                };

                constellationsJumps.Add(constellationJump);
            }

            await dbContext.AddRangeAsync(constellationsJumps);
            await dbContext.SaveChangesAsync();
        }
    }
}

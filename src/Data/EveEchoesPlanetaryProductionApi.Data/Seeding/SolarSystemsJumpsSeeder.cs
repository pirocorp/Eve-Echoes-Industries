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

    public class SolarSystemsJumpsSeeder : ISeeder
    {
        public async Task SeedAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.SolarSystemsJumps.AnyAsync())
            {
                return;
            }

            var logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger(typeof(EveEchoesPlanetaryProductionApiDbContextSeeder));

            await SeedSolarSystemsJumpsAsync(dbContext, logger);
        }

        private static async Task SeedSolarSystemsJumpsAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, ILogger logger)
        {
            var solarSystemsJumps = new List<SolarSystemJump>();

            await foreach (var line in CsvFileService.ReadCsvDataLineByLineAsync(GlobalConstants.FilePaths.SolarSystemsJumpsCsvFilePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var lineArgs = line.Split(GlobalConstants.CsvDelimiter, StringSplitOptions.RemoveEmptyEntries);

                var fromRegionIdSuccess = long.TryParse(lineArgs[0], out var fromRegionId);
                var fromConstellationIdSuccess = long.TryParse(lineArgs[1], out var fromConstellationId);

                var fromSolarSystemSuccess = long.TryParse(lineArgs[2], out var fromSolarSystemId);
                var toSolarSystemSuccess = long.TryParse(lineArgs[3], out var toSolarSystemId);

                var toConstellationIdSuccess = long.TryParse(lineArgs[4], out var toConstellationId);
                var toRegionIdSuccess = long.TryParse(lineArgs[5], out var toRegionId);

                if (!fromRegionIdSuccess
                    || !toRegionIdSuccess
                    || !fromConstellationIdSuccess
                    || !toConstellationIdSuccess
                    || !fromSolarSystemSuccess
                    || !toSolarSystemSuccess)
                {
                    logger.LogWarning($"Can't parse solar system jump");
                    logger.LogWarning(line);
                    continue;
                }

                var solarSystemJump = new SolarSystemJump()
                {
                    FromRegionId = fromRegionId,
                    FromConstellationId = fromConstellationId,
                    FromSolarSystemId = fromSolarSystemId,
                    ToSolarSystemId = toSolarSystemId,
                    ToConstellationId = toConstellationId,
                    ToRegionId = toRegionId,
                };

                solarSystemsJumps.Add(solarSystemJump);
            }

            await dbContext.AddRangeAsync(solarSystemsJumps);
            await dbContext.SaveChangesAsync();
        }
    }
}

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

    public class PlanetsResourcesSeeder : ISeeder
    {
        public async Task SeedAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.PlanetsResources.AnyAsync())
            {
                return;
            }

            var logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger(typeof(EveEchoesPlanetaryProductionApiDbContextSeeder));

            await SeedPlanetsResourcesAsync(dbContext, logger);
        }

        private static async Task SeedPlanetsResourcesAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, ILogger logger)
        {
            var planetsResources = new List<PlanetResource>();

            await foreach (var line in CsvFileService.ReadCsvDataLineByLineAsync(GlobalConstants.FilePaths
                .PlanetsResourcesCsvFilePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var lineArgs = line.Split(GlobalConstants.CsvDelimiter, StringSplitOptions.RemoveEmptyEntries);

                var planetIdParseSuccess = long.TryParse(lineArgs[0], out var planetId);
                var itemId = (await dbContext.Items.FirstOrDefaultAsync(i => i.Name.Equals(lineArgs[6])))?.Id ?? 0;
                var richnessId = (await dbContext.Richnesses.FirstOrDefaultAsync(i => i.Name.Equals(lineArgs[7])))?.Id ?? 0;
                var outputParseSuccess = double.TryParse(lineArgs[8], out var output);

                if (!planetIdParseSuccess
                    || itemId is 0
                    || richnessId is 0
                    || !outputParseSuccess)
                {
                    logger.LogWarning($"Can't parse planet resource");
                    logger.LogWarning(line);
                    continue;
                }

                var planetResource = new PlanetResource()
                {
                    PlanetId = planetId,
                    ItemId = itemId,
                    RichnessId = richnessId,
                    Output = output,
                };

                planetsResources.Add(planetResource);
            }

            await dbContext.AddRangeAsync(planetsResources);
            await dbContext.SaveChangesAsync();
        }
    }
}

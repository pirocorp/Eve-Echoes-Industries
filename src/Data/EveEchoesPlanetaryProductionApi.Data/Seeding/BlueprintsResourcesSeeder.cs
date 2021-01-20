namespace EveEchoesPlanetaryProductionApi.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class BlueprintsResourcesSeeder : ISeeder
    {
        public async Task SeedAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.BlueprintsResources.AnyAsync())
            {
                return;
            }

            var logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger(typeof(BlueprintsResourcesSeeder));

            try
            {
                await SeedBlueprintsResources(dbContext, logger);
            }
            catch (Exception ex)
            {
                logger.LogCritical($"Seeder {nameof(BlueprintSeeder)} failed with error: {ex.Message}");
            }
        }

        private static async Task SeedBlueprintsResources(EveEchoesPlanetaryProductionApiDbContext dbContext, ILogger logger)
        {
            var csvValues = new[]
            {
                "Name", "Type", "Tech Level", "Tritanium", "Pyerite", "Mexallon", "Isogen", "Nocxium", "Zydrine",
                "Megacyte", "Morphite", "Lustering Alloy", "Sheen Compound", "Gleaming Alloy", "Condensed Alloy",
                "Precious Alloy", "Motley Compound", "Fiber Composite", "Lucent Compound", "Opulent Compound",
                "Glossy Compound", "Crystal Compound", "Dark Compound", "Base Metals", "Heavy Metals",
                "Reactive Metals", "Noble Metals", "Toxic Metals", "Reactive Gas", "Noble Gas", "Industrial Fibers",
                "Supertensile Plastics", "Polyaramids", "Coolant", "Condensates", "Construction Blocks", "Nanites",
                "Silicate Glass", "Smartfab Units", "Charred Micro Circuit", "Fried Interface Circuit",
                "Tripped Power Circuit", "Smashed Trigger Unit", "Damaged Close-in Weapon System",
                "Scorched Telemetry Processor", "Contaminated Lorentz Fluid", "Conductive Polymer",
                "Contaminated Nanite Polymer", "Defective Current Pump", "Production Cost", "Production Time",
                "Production Count",
            };

            await foreach (var line in CsvFileService.ReadCsvDataLineByLineAsync(GlobalConstants.FilePaths.BluePrintsCsvFilePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var lineArgs = line.Split(GlobalConstants.CsvDelimiter, StringSplitOptions.RemoveEmptyEntries);

                await SeedCurrentBlueprintResources(dbContext, logger, lineArgs, csvValues);
            }
        }

        private static async Task SeedCurrentBlueprintResources(EveEchoesPlanetaryProductionApiDbContext dbContext, ILogger logger, string[] lineArgs, string[] csvValues)
        {
            var resourceTasks = new List<ValueTask<EntityEntry<BlueprintResource>>>();

            var name = lineArgs[0];
            var blueprintName = $"{name} Blueprint";
            var blueprint = await dbContext.Blueprints.FirstOrDefaultAsync(b => b.BlueprintItem.Name.Equals(blueprintName));

            if (blueprint is null)
            {
                logger.LogError($"Can't find blueprint with name: {name}");
            }

            for (var i = 3; i < 49; i++)
            {
                var itemName = csvValues[i];
                var itemQuantitySuccess = long.TryParse(lineArgs[i], out var itemQuantity);

                var item = await dbContext.Items.Where(i => i.Name.Equals(itemName)).FirstOrDefaultAsync();

                if (!itemQuantitySuccess
                    || item is null)
                {
                    logger.LogError("Can't parse blueprint resource");
                    logger.LogError($"Resource: {itemName}, quantity: {itemQuantity}");

                    continue;
                }

                if (itemQuantity == 0)
                {
                    continue;
                }

                var blueprintResource = new BlueprintResource()
                {
                    Blueprint = blueprint,
                    Item = item,
                    Quantity = itemQuantity,
                };

                resourceTasks.Add(dbContext.BlueprintsResources.AddAsync(blueprintResource));
            }

            var tasks = resourceTasks.Select(vt => vt.AsTask()).ToArray();
            await Task.WhenAll(tasks);

            await dbContext.SaveChangesAsync();
        }
    }
}

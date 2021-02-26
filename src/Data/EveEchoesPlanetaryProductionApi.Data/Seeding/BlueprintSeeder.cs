namespace EveEchoesPlanetaryProductionApi.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class BlueprintSeeder : ISeeder
    {
        private static long frigateBlueprintMissingId = 90000001001;
        private static long destroyerBlueprintMissingId = 90000002002;
        private static long cruiserBlueprintMissingId = 90000003001;
        private static long battlecruiserBlueprintMissingId = 90000004001;
        private static long battleshipBlueprintMissingId = 90000005001;
        private static long industrialBlueprintMissingId = 90000006001;
        private static long moduleBlueprintMissingId = 90000007001;
        private static long rigBlueprintMissingId = 90000008001;
        private static long droneBlueprintMissingId = 90000009001;
        private static long structureBlueprintMissingId = 90000010001;
        private static long defaultBlueprintMissingId = 90000011001;

        public async Task SeedAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Blueprints.AnyAsync())
            {
                return;
            }

            var logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger(typeof(BlueprintSeeder));

            try
            {
                await SeedBlueprints(dbContext, logger);
            }
            catch (Exception ex)
            {
                logger.LogCritical($"Seeder {nameof(BlueprintSeeder)} failed with error: {ex.Message}");
            }
        }

        private static async Task SeedBlueprints(EveEchoesPlanetaryProductionApiDbContext dbContext, ILogger logger)
        {
            await foreach (var line in CsvFileService.ReadCsvDataLineByLineAsync(GlobalConstants.FilePaths.BluePrintsCsvFilePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var lineArgs = line.Split(GlobalConstants.CsvDelimiter, StringSplitOptions.RemoveEmptyEntries);

                var name = lineArgs[0];
                var type = lineArgs[1];
                var techSuccess = int.TryParse(lineArgs[2], out var tech);

                var productionCostSuccess = long.TryParse(lineArgs[49], out var productionCost);
                var productionTimeSuccess = long.TryParse(lineArgs[50], out var productionTime);
                var productionCountSuccess = long.TryParse(lineArgs[51], out var productionCount);

                var product = await dbContext.Items
                    .Where(i => i.Name.Equals(name))
                    .FirstOrDefaultAsync();

                var productType = await dbContext.ItemTypes
                    .Where(t => t.Name.Equals(type))
                    .FirstOrDefaultAsync();

                var blueprintName = $"{name} Blueprint";

                var blueprintItem = await dbContext.Items
                    .Where(i => i.Name.Equals(blueprintName))
                    .FirstOrDefaultAsync() ?? await CreateBlueprint(dbContext, blueprintName, productType);

                if (product is null
                    || productType is null
                    || blueprintItem is null
                    || !techSuccess
                    || !productionCostSuccess
                    || !productionTimeSuccess
                    || !productionCountSuccess)
                {
                    logger.LogError("Cant parse blueprint");
                    logger.LogError(line);

                    continue;
                }

                var blueprint = new Blueprint()
                {
                    BlueprintItem = blueprintItem,
                    Product = product,
                    ProductType = productType,
                    TechLevel = tech,
                    ProductionCost = productionCost,
                    ProductionTime = productionTime,
                    ProductionCount = productionCount,
                };

                await dbContext.Blueprints.AddAsync(blueprint);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task<Item> CreateBlueprint(EveEchoesPlanetaryProductionApiDbContext dbContext, string blueprintName, ItemType productType)
        {
            var type = await dbContext.ItemTypes.Where(t => t.Name.Equals("Blueprint")).FirstOrDefaultAsync();

            var item = new Item()
            {
                ItemType = type,
                Id = GetBlueprintId(productType),
                Name = blueprintName,
            };

            using (dbContext.Database.OpenConnectionAsync())
            {
                await dbContext.Items.AddAsync(item);
                await dbContext.SaveChangesWithExplicitIdentityInsertAsync(nameof(EveEchoesPlanetaryProductionApiDbContext.Items));
            }

            return item;
        }

        private static long GetBlueprintId(ItemType type)
            => type.Name switch
            {
                "Frigate" => frigateBlueprintMissingId++,
                "Destroyer" => destroyerBlueprintMissingId++,
                "Cruiser" => cruiserBlueprintMissingId++,
                "Battlecruiser" => battlecruiserBlueprintMissingId++,
                "Battleship" => battleshipBlueprintMissingId++,
                "Industrial" => industrialBlueprintMissingId++,
                "Module" => moduleBlueprintMissingId++,
                "Rig" => rigBlueprintMissingId++,
                "Drone" => droneBlueprintMissingId++,
                "Structure" => structureBlueprintMissingId++,
                _ => defaultBlueprintMissingId++
            };
    }
}

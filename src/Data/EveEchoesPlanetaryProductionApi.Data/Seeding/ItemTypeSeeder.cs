namespace EveEchoesPlanetaryProductionApi.Data.Seeding
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class ItemTypeSeeder : ISeeder
    {
        public async Task SeedAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, [NotNull] IServiceProvider serviceProvider)
        {
            if (await dbContext.ItemTypes.AnyAsync())
            {
                return;
            }

            var logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger(typeof(ItemTypeSeeder));

            using (var dbContextTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    await this.SeedItemTypesAsync(dbContext);
                    await this.AddTypeToItems(dbContext);

                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    logger.LogCritical($"Seeder ItemTypeSeeder failed with error: {ex.Message}");
                }
            }
        }

        private async Task SeedItemTypesAsync(EveEchoesPlanetaryProductionApiDbContext dbContext)
        {
            await foreach (var line in CsvFileService.ReadCsvDataLineByLineAsync(GlobalConstants.FilePaths.ItemTypesCsvFilePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var lineArgs = line.Split(GlobalConstants.CsvDelimiter, StringSplitOptions.RemoveEmptyEntries);
                var itemTypeName = lineArgs[0];

                var itemType = new ItemType()
                {
                    Name = itemTypeName,
                };

                await dbContext.AddAsync(itemType);
            }

            await dbContext.SaveChangesAsync();
        }

        private async Task AddTypeToItems(EveEchoesPlanetaryProductionApiDbContext dbContext)
        {
            var items = await dbContext.Items.ToListAsync();
            var itemTypes = await dbContext.ItemTypes.ToDictionaryAsync(x => x.Name, y => y);

            foreach (var item in items)
            {
                var type = this.GetItemType(item);

                item.ItemType = itemTypes[type];
            }

            await dbContext.SaveChangesAsync();
        }

        private string GetItemType(Item item)
        {
            if (item.Id <= 10190002001)
            {
                return "Frigate";
            }
            else if (item.Id <= 10290000410)
            {
                return "Destroyer";
            }
            else if (item.Id <= 10390002001)
            {
                return "Cruiser";
            }
            else if (item.Id <= 10490000408)
            {
                return "Battlecruiser";
            }
            else if (item.Id <= 10590002001)
            {
                return "Battleship";
            }
            else if (item.Id <= 10690001408)
            {
                return "Industrial";
            }
            else if (item.Id <= 11592000024)
            {
                return "Module";
            }
            else if (item.Id <= 11799030008)
            {
                return "Rig";
            }
            else if (item.Id <= 14090330012)
            {
                return "Drone";
            }
            else if (item.Id <= 27000000014)
            {
                return "Structure";
            }
            else if (item.Id <= 27021000016)
            {
                return "Datacore";
            }
            else if (item.Id <= 28007000000)
            {
                return "Misc";
            }
            else if (item.Id <= 28008000019)
            {
                return "Story Missions";
            }
            else if (item.Id <= 28009000012)
            {
                return "Supply Chest";
            }
            else if (item.Id <= 28010000002)
            {
                return "Chip";
            }
            else if (item.Id <= 41000000008)
            {
                return "Mineral";
            }
            else if (item.Id <= 41400000011)
            {
                return "Salvaged Material";
            }
            else if (item.Id <= 42002000017)
            {
                return "Planetary Resource";
            }
            else if (item.Id <= 44130000002)
            {
                return "Ship Debris";
            }
            else if (item.Id <= 51015000004)
            {
                return "Ore";
            }
            else if (item.Id <= 77000000014)
            {
                return "Blueprint";
            }
            else if (item.Id <= 80502301120)
            {
                return "SKIN";
            }
            else
            {
                return "Misc";
            }
        }
    }
}

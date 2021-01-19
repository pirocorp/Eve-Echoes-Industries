namespace EveEchoesPlanetaryProductionApi.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Data.Common;
    using EveEchoesPlanetaryProductionApi.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class ItemsSeeder : ISeeder
    {
        public async Task SeedAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, IServiceProvider serviceProvider)
        {
            var logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger(typeof(EveEchoesPlanetaryProductionApiDbContextSeeder));

            if (await dbContext.Items.AnyAsync())
            {
                await UpdateItems(dbContext, logger, false);
                return;
            }

            await SeedItemsAsync(dbContext, logger);
        }

        private static async Task UpdateItems(EveEchoesPlanetaryProductionApiDbContext dbContext, ILogger logger, bool update)
        {
            if (!update)
            {
                return;
            }

            await foreach (var line in CsvFileService.ReadCsvDataLineByLineAsync(GlobalConstants.FilePaths.ItemsCsvFilePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var itemArgs = line.Split(GlobalConstants.CsvDelimiter, StringSplitOptions.RemoveEmptyEntries);

                var success = long.TryParse(itemArgs[0], out var itemId);

                if (!success)
                {
                    logger.LogError("Can't parse item");
                    logger.LogError(line);
                    continue;
                }

                if (await dbContext.Items.AnyAsync(i => i.Id.Equals(itemId)))
                {
                    continue;
                }

                var item = await CreateItem(itemArgs, dbContext);

                await dbContext.AddAsync(item);
                await dbContext.SaveChangesWithExplicitIdentityInsertAsync(nameof(EveEchoesPlanetaryProductionApiDbContext.Items));
            }
        }

        private static async Task SeedItemsAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, ILogger logger)
        {
            await foreach (var line in CsvFileService.ReadCsvDataLineByLineAsync(GlobalConstants.FilePaths.ItemsCsvFilePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var itemArgs = line.Split(GlobalConstants.CsvDelimiter, StringSplitOptions.RemoveEmptyEntries);

                var item = await CreateItem(itemArgs, dbContext);

                if (item is null)
                {
                    var name = itemArgs[1];

                    logger.LogWarning(string.Format(DatabaseConstants.SeedingConstants.ItemErrorParseMessage, name));
                    logger.LogWarning(line);

                    continue;
                }

                if (await dbContext.Items.AnyAsync(i => i.Id.Equals(item.Id)))
                {
                    continue;
                }

                await dbContext.AddAsync(item);
            }

            await dbContext.SaveChangesWithExplicitIdentityInsertAsync(nameof(EveEchoesPlanetaryProductionApiDbContext.Items));
        }

        private static async Task<Item> CreateItem(string[] itemArgs, EveEchoesPlanetaryProductionApiDbContext dbContext)
        {
            var success = long.TryParse(itemArgs[0], out var itemId);
            var name = itemArgs[1];

            if (!success)
            {
                return null;
            }

            var item = new Item()
            {
                Id = itemId,
                Name = name,
            };

            item.ItemTypeId = await dbContext.ItemTypes
                .Where(it => it.Name.Equals(ItemTypeSeeder.GetItemType(item)))
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            return item;
        }
    }
}

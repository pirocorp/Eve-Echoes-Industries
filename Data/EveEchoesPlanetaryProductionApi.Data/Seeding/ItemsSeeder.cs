namespace EveEchoesPlanetaryProductionApi.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Common;
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

            await foreach (var line in CsvFileService.ReadCsvDataLineByLineAsync(GlobalConstants.FilePaths.ItemsCsvFilePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var itemArgs = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

                var success = long.TryParse(itemArgs[0], out var itemId);
                var name = itemArgs[1];

                if (!success)
                {
                    logger.LogWarning($"Item {name} cannot be parsed.");
                    logger.LogWarning(line);
                    continue;
                }

                var item = new Item()
                {
                    Id = itemId,
                    Name = name,
                };

                if (await dbContext.Items.AnyAsync(i => i.Id.Equals(itemId)))
                {
                    continue;
                }

                await dbContext.AddAsync(item);
            }

            await dbContext.SaveChangesWithExplicitIdentityInsertAsync();
        }
    }
}

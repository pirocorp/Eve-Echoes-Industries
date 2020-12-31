namespace EveEchoesPlanetaryProductionApi.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Data.Models;

    using Microsoft.EntityFrameworkCore;

    public class RichnessSeeder : ISeeder
    {
        public async Task SeedAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Richnesses.AnyAsync())
            {
                return;
            }

            await SeedRichnessAsync(dbContext);
        }

        private static async Task SeedRichnessAsync(EveEchoesPlanetaryProductionApiDbContext dbContext)
        {
            await foreach (var line in CsvFileService.ReadCsvDataLineByLineAsync(GlobalConstants.FilePaths.RichnessCsvFilePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var lineArgs = line.Split(GlobalConstants.CsvDelimiter, StringSplitOptions.RemoveEmptyEntries);
                var richnessName = lineArgs[0];

                var richness = new Richness()
                {
                    Name = richnessName,
                };

                await dbContext.AddAsync(richness);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}

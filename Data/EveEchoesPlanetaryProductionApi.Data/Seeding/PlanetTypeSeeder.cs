namespace EveEchoesPlanetaryProductionApi.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class PlanetTypeSeeder : ISeeder
    {
        public async Task SeedAsync(EveEchoesPlanetaryProductionApiDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.PlanetTypes.AnyAsync())
            {
                return;
            }

            await SeedPlanetTypes(dbContext);
        }

        private static async Task SeedPlanetTypes(EveEchoesPlanetaryProductionApiDbContext dbContext)
        {
            await foreach (var line in CsvFileService.ReadCsvDataLineByLineAsync(GlobalConstants.FilePaths.PlanetTypes))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var lineArgs = line.Split(GlobalConstants.CsvDelimiter, StringSplitOptions.RemoveEmptyEntries);
                var planetTypeName = lineArgs[0];

                var planetType = new PlanetType()
                {
                    Name = planetTypeName,
                };

                await dbContext.AddAsync(planetType);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}

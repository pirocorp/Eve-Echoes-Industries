namespace EveEchoesPlanetaryProductionApi.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class EveEchoesPlanetaryProductionApiDbContextSeeder : ISeeder
    {
        public async Task SeedAsync([NotNull]EveEchoesPlanetaryProductionApiDbContext dbContext, [NotNull]IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger(typeof(EveEchoesPlanetaryProductionApiDbContextSeeder));

            var seeders = new List<ISeeder>
            {
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
            }
        }
    }
}

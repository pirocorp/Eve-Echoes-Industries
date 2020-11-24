namespace EveEchoesPlanetaryProductionApi.Data
{
    using System.IO;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Design-time services will automatically discover implementations
    /// of this interface that are in the startup assembly or the same
    /// assembly as the derived context.
    /// </summary>
    /// <remarks>
    /// It will be used for migrations with CLI or PMC.
    /// </remarks>
    /// <remarks>
    /// It will use configuration from appsettings.json.
    /// </remarks>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EveEchoesPlanetaryProductionApiDbContext>
    {
        public EveEchoesPlanetaryProductionApiDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<EveEchoesPlanetaryProductionApiDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);

            return new EveEchoesPlanetaryProductionApiDbContext(builder.Options);
        }
    }
}

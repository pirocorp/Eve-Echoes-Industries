namespace EveEchoesPlanetaryProductionApi.Data
{
    using System.Threading.Tasks;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class EveEchoesPlanetaryProductionApiDbContext : DbContext
    {
        public EveEchoesPlanetaryProductionApiDbContext(DbContextOptions<EveEchoesPlanetaryProductionApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Constellation> Constellations { get; set; }

        public DbSet<ConstellationJump> ConstellationJumps { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Planet> Planets { get; set; }

        public DbSet<PlanetResource> PlanetResources { get; set; }

        public DbSet<PlanetType> PlanetTypes { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<RegionJump> RegionJumps { get; set; }

        public DbSet<Richness> Richnesses { get; set; }

        public DbSet<SolarSystem> SolarSystems { get; set; }

        public DbSet<SolarSystemJump> SolarSystemJumps { get; set; }

        public async Task<int> SaveChangesWithExplicitIdentityInsertAsync(string table)
        {
            await this.Database.OpenConnectionAsync();

            var count = 0;

            try
            {
                await this.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.{table} ON");
                count = await this.SaveChangesAsync();
                await this.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.{table} OFF");
            }
            finally
            {
                await this.Database.CloseConnectionAsync();
            }

            return count;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureRelations(builder);
        }

        /// <summary>
        /// Applies all entity configurations which implements IEntityTypeConfiguration.
        /// </summary>
        /// <param name="builder">ModelBuilder.</param>
        private static void ConfigureRelations(ModelBuilder builder)
            => builder.ApplyConfigurationsFromAssembly(typeof(Planet).Assembly);
    }
}

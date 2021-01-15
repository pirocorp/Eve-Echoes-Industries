namespace EveEchoesPlanetaryProductionApi.Data
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class EveEchoesPlanetaryProductionApiDbContext : IdentityDbContext<User, Role, string>
    {
        public EveEchoesPlanetaryProductionApiDbContext(DbContextOptions<EveEchoesPlanetaryProductionApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Constellation> Constellations { get; set; }

        public DbSet<ConstellationJump> ConstellationsJumps { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Planet> Planets { get; set; }

        public DbSet<PlanetResource> PlanetsResources { get; set; }

        public DbSet<PlanetType> PlanetTypes { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<RegionJump> RegionsJumps { get; set; }

        public DbSet<Richness> Richnesses { get; set; }

        public DbSet<SolarSystem> SolarSystems { get; set; }

        public DbSet<SolarSystemJump> SolarSystemsJumps { get; set; }

        public DbSet<TargetSystem> TargetSystems { get; set; }

        public DbSet<ItemType> ItemTypes { get; set; }

        /// <summary>
        /// Used when identity column value is specified explicitly when inserting new item in table.
        /// </summary>
        /// <param name="table">Name of table.</param>
        /// <returns></returns>
        public async Task<int> SaveChangesWithExplicitIdentityInsertAsync(string table)
        {
            await this.Database.OpenConnectionAsync();

            int count;

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

            builder.Entity<TargetSystem>(ts =>
            {
                ts.HasNoKey();
                ts.ToView(null);
            });

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

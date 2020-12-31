namespace EveEchoesPlanetaryProductionApi.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EveEchoesPlanetaryProductionApi.Data.Common;
    using EveEchoesPlanetaryProductionApi.Data.Common.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SolarSystem : NamedModel<long>, IEntityTypeConfiguration<SolarSystem>
    {
        public SolarSystem()
        {
            this.Planets = new HashSet<Planet>();

            this.SolarSystemAsOrigins = new HashSet<SolarSystemJump>();
            this.SolarSystemAsDestinations = new HashSet<SolarSystemJump>();
        }

        [Range(DatabaseConstants.MinSecurityLevel, DatabaseConstants.MaxSecurityLevel)]
        public double Security { get; set; }

        public long RegionId { get; set; }

        public virtual Region Region { get; set; }

        public long ConstellationId { get; set; }

        public virtual Constellation Constellation { get; set; }

        /// <summary>
        /// Gets or sets planets in this solar system.
        /// </summary>
        public virtual IEnumerable<Planet> Planets { get; set; }

        /// <summary>
        /// Gets or sets jumps in which current solar system is origin.
        /// </summary>
        public virtual IEnumerable<SolarSystemJump> SolarSystemAsOrigins { get; set; }

        /// <summary>
        /// Gets or sets jumps in which current solar system is destination.
        /// </summary>
        public virtual IEnumerable<SolarSystemJump> SolarSystemAsDestinations { get; set; }

        /// <summary>
        /// This method will be used by ApplyConfigurationsFromAssembly
        /// in OnModelCreating method.
        /// </summary>
        /// <param name="solarSystem"></param>
        public void Configure(EntityTypeBuilder<SolarSystem> solarSystem)
        {
            solarSystem
                .HasIndex(ss => ss.Name)
                .IsUnique();

            solarSystem
                .HasOne(ss => ss.Region)
                .WithMany(r => r.SolarSystems)
                .HasForeignKey(ss => ss.RegionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            solarSystem
                .HasOne(ss => ss.Constellation)
                .WithMany(c => c.SolarSystems)
                .HasForeignKey(ss => ss.ConstellationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

namespace EveEchoesPlanetaryProductionApi.Data.Models
{
    using System.Collections.Generic;

    using EveEchoesPlanetaryProductionApi.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class Constellation : NamedModel<int>, IEntityTypeConfiguration<Constellation>
    {
        public Constellation()
        {
            this.Planets = new HashSet<Planet>();
            this.SolarSystems = new HashSet<SolarSystem>();

            this.ConstellationAsOrigins = new HashSet<ConstellationJump>();
            this.ConstellationAsDestinations = new HashSet<ConstellationJump>();

            this.JumpsFromSolarSystemsInThisConstellation = new HashSet<SolarSystemJump>();
            this.JumpsToSolarSystemsInThisConstellation = new HashSet<SolarSystemJump>();
        }

        public int RegionId { get; set; }

        public virtual Region Region { get; set; }

        /// <summary>
        /// Gets or sets planets in this constellation.
        /// </summary>
        public IEnumerable<Planet> Planets { get; set; }

        /// <summary>
        /// Gets or sets solar systems in this constellation.
        /// </summary>
        public virtual IEnumerable<SolarSystem> SolarSystems { get; set; }

        /// <summary>
        /// Gets or sets jumps in which current constellation is origin.
        /// </summary>
        public virtual IEnumerable<ConstellationJump> ConstellationAsOrigins { get; set; }

        /// <summary>
        /// Gets or sets jumps in which current constellation is destination.
        /// </summary>
        public virtual IEnumerable<ConstellationJump> ConstellationAsDestinations { get; set; }

        /// <summary>
        /// Gets or sets jumps originating from solar systems in this constellation.
        /// </summary>
        public virtual IEnumerable<SolarSystemJump> JumpsFromSolarSystemsInThisConstellation { get; set; }

        /// <summary>
        /// Gets or sets jumps with destination solar systems in current constellation.
        /// </summary>
        public virtual IEnumerable<SolarSystemJump> JumpsToSolarSystemsInThisConstellation { get; set; }

        /// <summary>
        /// This method will be used by ApplyConfigurationsFromAssembly
        /// in OnModelCreating method.
        /// </summary>
        /// <param name="constellation"></param>
        public void Configure(EntityTypeBuilder<Constellation> constellation)
        {
            constellation
                .HasIndex(c => c.Name)
                .IsUnique();

            constellation
                .HasOne(c => c.Region)
                .WithMany(r => r.Constellations)
                .HasForeignKey(c => c.RegionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

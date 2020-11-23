namespace EveEchoesPlanetaryProductionApi.Data.Models
{
    using System.Collections.Generic;

    using EveEchoesPlanetaryProductionApi.Data.Common.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class Region : NamedModel<int>, IEntityTypeConfiguration<Region>
    {
        public Region()
        {
            this.Planets = new HashSet<Planet>();
            this.SolarSystems = new HashSet<SolarSystem>();
            this.Constellations = new HashSet<Constellation>();

            this.RegionAsOrigins = new HashSet<RegionJump>();
            this.RegionAsDestinations = new HashSet<RegionJump>();

            this.JumpsFromConstellationInThisRegion = new HashSet<ConstellationJump>();
            this.JumpsToConstellationInThisRegion = new HashSet<ConstellationJump>();

            this.JumpsFromSolarSystemsInThisRegion = new HashSet<SolarSystemJump>();
            this.JumpsToSolarSystemsInThisRegion = new HashSet<SolarSystemJump>();
        }

        /// <summary>
        /// Gets or sets planets in this region.
        /// </summary>
        public virtual IEnumerable<Planet> Planets { get; set; }

        /// <summary>
        /// Gets or sets solar systems in this region.
        /// </summary>
        public virtual IEnumerable<SolarSystem> SolarSystems { get; set; }

        /// <summary>
        /// Gets or sets constellations in current region.
        /// </summary>
        public virtual IEnumerable<Constellation> Constellations { get; set; }

        /// <summary>
        /// Gets or sets jumps in which current region is origin.
        /// </summary>
        public virtual IEnumerable<RegionJump> RegionAsOrigins { get; set; }

        /// <summary>
        /// Gets or sets jumps in which current region is destination.
        /// </summary>
        public virtual IEnumerable<RegionJump> RegionAsDestinations { get; set; }

        /// <summary>
        /// Gets or sets jumps originating from constellations in this region.
        /// </summary>
        public virtual IEnumerable<ConstellationJump> JumpsFromConstellationInThisRegion { get; set; }

        /// <summary>
        /// Gets or sets jumps with destination constellation in current region.
        /// </summary>
        public virtual IEnumerable<ConstellationJump> JumpsToConstellationInThisRegion { get; set; }

        /// <summary>
        /// Gets or sets jumps originating from solar systems in this region.
        /// </summary>
        public virtual IEnumerable<SolarSystemJump> JumpsFromSolarSystemsInThisRegion { get; set; }

        /// <summary>
        /// Gets or sets jumps with destination solar systems in current region.
        /// </summary>
        public virtual IEnumerable<SolarSystemJump> JumpsToSolarSystemsInThisRegion { get; set; }

        /// <summary>
        /// This method will be used by ApplyConfigurationsFromAssembly
        /// in OnModelCreating method.
        /// </summary>
        /// <param name="region"></param>
        public void Configure(EntityTypeBuilder<Region> region)
        {
            region
                .HasIndex(r => r.Name)
                .IsUnique();
        }
    }
}

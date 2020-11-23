namespace EveEchoesPlanetaryProductionApi.Data.Models
{
    using System.Collections.Generic;

    using EveEchoesPlanetaryProductionApi.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class Richness : NamedModel<int>, IEntityTypeConfiguration<Richness>
    {
        public Richness()
        {
            this.PlanetResources = new HashSet<PlanetResource>();
        }

        public virtual IEnumerable<PlanetResource> PlanetResources { get; set; }

        /// <summary>
        /// This method will be used by ApplyConfigurationsFromAssembly
        /// in OnModelCreating method.
        /// </summary>
        /// <param name="richness"></param>
        public void Configure(EntityTypeBuilder<Richness> richness)
        {
            richness
                .HasIndex(r => r.Name)
                .IsUnique();
        }
    }
}

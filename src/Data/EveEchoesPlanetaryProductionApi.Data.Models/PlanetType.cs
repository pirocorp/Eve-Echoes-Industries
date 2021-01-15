namespace EveEchoesPlanetaryProductionApi.Data.Models
{
    using System.Collections.Generic;

    using EveEchoesPlanetaryProductionApi.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PlanetType : NamedModel<long>, IEntityTypeConfiguration<PlanetType>
    {
        public PlanetType()
        {
            this.Planets = new HashSet<Planet>();
        }

        /// <summary>
        /// Gets or sets all planets of this type.
        /// </summary>
        public virtual IEnumerable<Planet> Planets { get; set; }

        /// <summary>
        /// This method will be used by ApplyConfigurationsFromAssembly
        /// in OnModelCreating method.
        /// </summary>
        /// <param name="planetType"></param>
        public void Configure(EntityTypeBuilder<PlanetType> planetType)
        {
            planetType
                .HasIndex(pt => pt.Name)
                .IsUnique();
        }
    }
}

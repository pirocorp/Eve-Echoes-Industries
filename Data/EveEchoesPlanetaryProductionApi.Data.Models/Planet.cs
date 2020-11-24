namespace EveEchoesPlanetaryProductionApi.Data.Models
{
    using System.Collections.Generic;

    using EveEchoesPlanetaryProductionApi.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class Planet : NamedModel<int>, IEntityTypeConfiguration<Planet>
    {
        public Planet()
        {
            this.PlanetResources = new HashSet<PlanetResource>();
        }

        public int RegionId { get; set; }

        public virtual Region Region { get; set; }

        public int ConstellationId { get; set; }

        public virtual Constellation Constellation { get; set; }

        public int SolarSystemId { get; set; }

        public virtual SolarSystem SolarSystem { get; set; }

        public int PlanetTypeId { get; set; }

        public virtual PlanetType PlanetType { get; set; }

        public virtual IEnumerable<PlanetResource> PlanetResources { get; set; }

        public void Configure(EntityTypeBuilder<Planet> planet)
        {
            planet
                .HasIndex(p => p.Name)
                .IsUnique();

            planet
                .HasOne(p => p.Region)
                .WithMany(r => r.Planets)
                .HasForeignKey(p => p.RegionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            planet
                .HasOne(p => p.Constellation)
                .WithMany(c => c.Planets)
                .HasForeignKey(p => p.ConstellationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            planet
                .HasOne(p => p.SolarSystem)
                .WithMany(ss => ss.Planets)
                .HasForeignKey(p => p.SolarSystemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            planet
                .HasOne(p => p.PlanetType)
                .WithMany(pt => pt.Planets)
                .HasForeignKey(p => p.PlanetTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

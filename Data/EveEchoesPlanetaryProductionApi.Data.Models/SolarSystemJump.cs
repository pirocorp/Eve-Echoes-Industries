namespace EveEchoesPlanetaryProductionApi.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SolarSystemJump : IEntityTypeConfiguration<SolarSystemJump>
    {
        public int FromRegionId { get; set; }

        public virtual Region FromRegion { get; set; }

        public int FromConstellationId { get; set; }

        public virtual Constellation FromConstellation { get; set; }

        public int FromSolarSystemId { get; set; }

        public virtual SolarSystem FromSolarSystem { get; set; }

        public int ToSolarSystemId { get; set; }

        public virtual SolarSystem ToSolarSystem { get; set; }

        public int ToConstellationId { get; set; }

        public virtual Constellation ToConstellation { get; set; }

        public int ToRegionId { get; set; }

        public virtual Region ToRegion { get; set; }

        public void Configure(EntityTypeBuilder<SolarSystemJump> solarSystemJump)
        {
            solarSystemJump.HasKey(key => new { key.FromSolarSystemId, key.ToSolarSystemId });

            solarSystemJump
                .HasOne(ssj => ssj.FromSolarSystem)
                .WithMany(ss => ss.SolarSystemAsOrigins)
                .HasForeignKey(ssj => ssj.FromSolarSystemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            solarSystemJump
                .HasOne(ssj => ssj.ToSolarSystem)
                .WithMany(ss => ss.SolarSystemAsDestinations)
                .HasForeignKey(ssj => ssj.ToSolarSystemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            solarSystemJump
                .HasOne(ssj => ssj.FromConstellation)
                .WithMany(c => c.JumpsFromSolarSystemsInThisConstellation)
                .HasForeignKey(ssj => ssj.FromConstellationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            solarSystemJump
                .HasOne(ssj => ssj.ToConstellation)
                .WithMany(c => c.JumpsToSolarSystemsInThisConstellation)
                .HasForeignKey(ssj => ssj.ToConstellationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            solarSystemJump
                .HasOne(ssj => ssj.FromRegion)
                .WithMany(r => r.JumpsFromSolarSystemsInThisRegion)
                .HasForeignKey(ssj => ssj.FromRegionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            solarSystemJump
                .HasOne(ssj => ssj.ToRegion)
                .WithMany(r => r.JumpsToSolarSystemsInThisRegion)
                .HasForeignKey(ssj => ssj.ToRegionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

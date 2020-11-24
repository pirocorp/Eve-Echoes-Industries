namespace EveEchoesPlanetaryProductionApi.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ConstellationJump : IEntityTypeConfiguration<ConstellationJump>
    {
        public long FromRegionId { get; set; }

        public virtual Region FromRegion { get; set; }

        public long FromConstellationId { get; set; }

        public virtual Constellation FromConstellation { get; set; }

        public long ToConstellationId { get; set; }

        public virtual Constellation ToConstellation { get; set; }

        public long ToRegionId { get; set; }

        public virtual Region ToRegion { get; set; }

        /// <summary>
        /// This method will be used by ApplyConfigurationsFromAssembly
        /// in OnModelCreating method.
        /// </summary>
        /// <param name="constellationJump"></param>
        public void Configure(EntityTypeBuilder<ConstellationJump> constellationJump)
        {
            constellationJump.HasKey(key => new { key.FromConstellationId, key.ToConstellationId });

            constellationJump
                .HasOne(cj => cj.FromConstellation)
                .WithMany(r => r.ConstellationAsOrigins)
                .HasForeignKey(cj => cj.FromConstellationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            constellationJump
                .HasOne(cj => cj.ToConstellation)
                .WithMany(r => r.ConstellationAsDestinations)
                .HasForeignKey(cj => cj.ToConstellationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            constellationJump
                .HasOne(cj => cj.FromRegion)
                .WithMany(r => r.JumpsFromConstellationlonghisRegion)
                .HasForeignKey(cj => cj.FromRegionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            constellationJump
                .HasOne(cj => cj.ToRegion)
                .WithMany(r => r.JumpsToConstellationlonghisRegion)
                .HasForeignKey(cj => cj.ToRegionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

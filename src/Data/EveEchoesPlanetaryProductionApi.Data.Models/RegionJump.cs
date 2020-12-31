namespace EveEchoesPlanetaryProductionApi.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RegionJump : IEntityTypeConfiguration<RegionJump>
    {
        public long FromRegionId { get; set; }

        public virtual Region FromRegion { get; set; }

        public long ToRegionId { get; set; }

        public virtual Region ToRegion { get; set; }

        /// <summary>
        /// This method will be used by ApplyConfigurationsFromAssembly
        /// in OnModelCreating method.
        /// </summary>
        /// <param name="regionJump"></param>
        public void Configure(EntityTypeBuilder<RegionJump> regionJump)
        {
            regionJump.HasKey(key => new { key.FromRegionId, key.ToRegionId });

            regionJump
                .HasOne(rj => rj.FromRegion)
                .WithMany(r => r.RegionAsOrigins)
                .HasForeignKey(rj => rj.FromRegionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            regionJump
                .HasOne(rj => rj.ToRegion)
                .WithMany(r => r.RegionAsDestinations)
                .HasForeignKey(rj => rj.ToRegionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

namespace EveEchoesPlanetaryProductionApi.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PlanetResource : IEntityTypeConfiguration<PlanetResource>
    {
        public int PlanetId { get; set; }

        public virtual Planet Planet { get; set; }

        public int ItemId { get; set; }

        public virtual Item Item { get; set; }

        public int RichnessId { get; set; }

        public virtual Richness Richness { get; set; }

        [Range(0, double.MaxValue)]
        public double Output { get; set; }

        /// <summary>
        /// This method will be used by ApplyConfigurationsFromAssembly
        /// in OnModelCreating method.
        /// </summary>
        /// <param name="planetResource"></param>
        public void Configure(EntityTypeBuilder<PlanetResource> planetResource)
        {
            planetResource.HasKey(key => new { key.PlanetId, key.ItemId });

            planetResource
                .HasOne(pr => pr.Planet)
                .WithMany(p => p.PlanetResources)
                .HasForeignKey(pr => pr.PlanetId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            planetResource
                .HasOne(pr => pr.Item)
                .WithMany(i => i.PlanetResources)
                .HasForeignKey(pr => pr.ItemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            planetResource
                .HasOne(pr => pr.Richness)
                .WithMany(r => r.PlanetResources)
                .HasForeignKey(pr => pr.RichnessId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

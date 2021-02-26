namespace EveEchoesPlanetaryProductionApi.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BlueprintResource : IEntityTypeConfiguration<BlueprintResource>
    {
        public string BlueprintId { get; set; }

        public virtual Blueprint Blueprint { get; set; }

        public long ItemId { get; set; }

        public virtual Item Item { get; set; }

        public long Quantity { get; set; }

        public void Configure(EntityTypeBuilder<BlueprintResource> blueprintResource)
        {
            blueprintResource.HasKey(key => new { key.BlueprintId, key.ItemId });

            blueprintResource
                .HasOne(br => br.Blueprint)
                .WithMany(b => b.Resources)
                .HasForeignKey(br => br.BlueprintId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            blueprintResource
                .HasOne(br => br.Item)
                .WithMany()
                .HasForeignKey(br => br.ItemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

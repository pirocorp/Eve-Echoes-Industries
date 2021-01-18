namespace EveEchoesPlanetaryProductionApi.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EveEchoesPlanetaryProductionApi.Data.Common;
    using EveEchoesPlanetaryProductionApi.Data.Common.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class Blueprint : BaseModel<string>, IEntityTypeConfiguration<Blueprint>
    {
        public Blueprint()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Resources = new HashSet<BlueprintResource>();
        }

        // Unique Required
        public long BlueprintItemId { get; set; }

        // Principal Entity
        public virtual Item BlueprintItem { get; set; }

        // Required
        public long ProductTypeId { get; set; }

        public virtual ItemType ProductType { get; set; }

        // Required Unique
        public long ProductId { get; set; }

        // Principal Entity
        public virtual Item Product { get; set; }

        [Range(DatabaseConstants.MinTechLevel, DatabaseConstants.MaxTechLevel)]
        public int TechLevel { get; set; }

        public long ProductionCost { get; set; }

        public long ProductionTime { get; set; }

        public long ProductionCount { get; set; }

        public virtual IEnumerable<BlueprintResource> Resources { get; set; }

        public void Configure(EntityTypeBuilder<Blueprint> blueprint)
        {
            blueprint
                .HasIndex(b => b.BlueprintItemId)
                .IsUnique();

            blueprint
                .HasOne(b => b.BlueprintItem)
                .WithOne(i => i.Blueprint)
                .HasForeignKey<Blueprint>(b => b.BlueprintItemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            blueprint
                .HasOne(b => b.ProductType)
                .WithMany(pt => pt.Blueprints)
                .HasForeignKey(b => b.ProductTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            blueprint
                .HasOne(b => b.Product)
                .WithOne()
                .HasForeignKey<Blueprint>(b => b.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

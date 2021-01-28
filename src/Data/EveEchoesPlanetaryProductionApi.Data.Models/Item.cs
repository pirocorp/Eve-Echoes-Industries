namespace EveEchoesPlanetaryProductionApi.Data.Models
{
    using System.Collections.Generic;

    using EveEchoesPlanetaryProductionApi.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class Item : NamedModel<long>, IEntityTypeConfiguration<Item>
    {
        public Item()
        {
            this.PlanetResources = new HashSet<PlanetResource>();
        }

        public long ItemTypeId { get; set; }

        public virtual ItemType ItemType { get; set; }

        // Dependent Entity in One to One Relation
        public virtual Blueprint Blueprint { get; set; }

        public virtual IEnumerable<PlanetResource> PlanetResources { get; set; }

        /// <summary>
        /// This method will be used by ApplyConfigurationsFromAssembly
        /// in OnModelCreating method.
        /// </summary>
        /// <param name="item"></param>
        public void Configure(EntityTypeBuilder<Item> item)
        {
            item
                .HasIndex(i => i.Name)
                .IsUnique();

            item
                .HasOne(i => i.ItemType)
                .WithMany(item => item.Items)
                .HasForeignKey(i => i.ItemTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

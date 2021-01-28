namespace EveEchoesPlanetaryProductionApi.Data.Models
{
    using System.Collections.Generic;

    using EveEchoesPlanetaryProductionApi.Data.Common.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ItemType : NamedModel<long>, IEntityTypeConfiguration<ItemType>
    {
        public ItemType()
        {
            this.Items = new HashSet<Item>();
            this.Blueprints = new HashSet<Blueprint>();
        }

        /// <summary>
        /// Gets or sets all items of this type.
        /// </summary>
        public virtual IEnumerable<Item> Items { get; set; }

        public virtual IEnumerable<Blueprint> Blueprints { get; set; }

        public void Configure(EntityTypeBuilder<ItemType> itemType)
        {
            itemType
                .HasIndex(it => it.Name)
                .IsUnique();
        }
    }
}

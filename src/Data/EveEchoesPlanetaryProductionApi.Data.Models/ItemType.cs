namespace EveEchoesPlanetaryProductionApi.Data.Models
{
    using System.Collections.Generic;

    using EveEchoesPlanetaryProductionApi.Data.Common.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ItemType : NamedModel<long>, IEntityTypeConfiguration<ItemType>
    {
        /// <summary>
        /// Gets or sets all items of this type.
        /// </summary>
        public virtual IEnumerable<Item> Items { get; set; }

        public void Configure(EntityTypeBuilder<ItemType> itemType)
        {
            itemType
                .HasIndex(it => it.Name)
                .IsUnique();
        }
    }
}

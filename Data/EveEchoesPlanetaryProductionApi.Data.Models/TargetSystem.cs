namespace EveEchoesPlanetaryProductionApi.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    [Keyless]
    public class TargetSystem : IEntityTypeConfiguration<TargetSystem>
    {
        public string Jumps { get; set; }

        public void Configure(EntityTypeBuilder<TargetSystem> targetSystem)
        {
            targetSystem.HasNoKey();
        }
    }
}

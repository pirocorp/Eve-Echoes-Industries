namespace EveEchoesPlanetaryProductionApi.Data.Common.Models
{
    using System.ComponentModel.DataAnnotations;

    public class NamedModel<T> : BaseModel<T>
    {
        [Required]
        [MaxLength(DatabaseConstants.EntitiesPropertyNameLength)]
        public string Name { get; set; }
    }
}

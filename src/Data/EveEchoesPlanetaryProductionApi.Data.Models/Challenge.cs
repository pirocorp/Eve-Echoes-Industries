namespace EveEchoesPlanetaryProductionApi.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using EveEchoesPlanetaryProductionApi.Data.Common.Models;

    public class Challenge : BaseModel<string>
    {
        [Required]
        public string Value { get; set; }
    }
}

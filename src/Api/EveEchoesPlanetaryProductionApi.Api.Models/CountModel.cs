namespace EveEchoesPlanetaryProductionApi.Api.Models
{
    using System.Text.Json.Serialization;

    public class CountModel
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}

namespace EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket
{
    using System;
    using System.Text.Json.Serialization;

    public class ItemPrice
    {
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }

        [JsonPropertyName("sell")]
        public decimal Sell { get; set; }

        [JsonPropertyName("buy")]
        public decimal Buy { get; set; }

        [JsonPropertyName("lowest_sell")]
        public decimal LowestSell { get; set; }

        [JsonPropertyName("highest_buy")]
        public decimal HighestBuy { get; set; }

        [JsonPropertyName("volume")]
        public long Volume { get; set; }
    }
}

namespace EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket
{
    using System;

    public class ItemPrice
    {
        public DateTime Time { get; set; }

        public decimal Sell { get; set; }

        public decimal Buy { get; set; }

        public decimal LowestSell { get; set; }

        public decimal HighestBuy { get; set; }

        public long Volume { get; set; }
    }
}

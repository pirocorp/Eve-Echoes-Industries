namespace EveEchoesPlanetaryProductionApi.Web.Models
{
    using System;

    public class SolarSystemInfoPlanetPlanetaryResourcePriceModel
    {
        public DateTime Time { get; set; }

        public decimal Sell { get; set; }

        public decimal Buy { get; set; }

        public decimal LowestSell { get; set; }

        public decimal HighestBuy { get; set; }
    }
}

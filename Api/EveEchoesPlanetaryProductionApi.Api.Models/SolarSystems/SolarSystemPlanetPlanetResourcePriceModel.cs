﻿namespace EveEchoesPlanetaryProductionApi.Api.Models.SolarSystems
{
    using System;

    using EveEchoesPlanetaryProductionApi.Services.Mapping;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    public class SolarSystemPlanetPlanetResourcePriceModel : IMapFrom<ItemPrice>
    {
        public DateTime Time { get; set; }

        public decimal Sell { get; set; }

        public decimal Buy { get; set; }

        public decimal LowestSell { get; set; }

        public decimal HighestBuy { get; set; }
    }
}

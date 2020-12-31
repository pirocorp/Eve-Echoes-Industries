namespace EveEchoesPlanetaryProductionApi.Services.EveEchoesMarket
{
    public static class EveEchoesMarketConstants
    {
        // Json price format
        // {
        //      "time": 1599562800,         // a UNIX timestamp (seconds)
        //      "sell": 264000.0,           // a calculated sell price for the item
        //      "buy": 60000.0,             // a calculated buy price for the item
        //      "lowest_sell": 80000.0,     // the lowest sell price for the item
        //      "highest_buy": 60000.0,     // the highest buy price for the item
        //      "volume": null              // a predicted volume for the item
        // }
        public class PriceItem
        {
            public const string Time = "time";

            public const string Sell = "sell";

            public const string Buy = "buy";

            public const string LowestSell = "lowest_sell";

            public const string HighestBuy = "highest_buy";

            public const string Volume = "volume";
        }
    }
}

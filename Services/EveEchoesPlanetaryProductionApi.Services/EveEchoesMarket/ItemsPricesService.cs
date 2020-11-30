namespace EveEchoesPlanetaryProductionApi.Services.EveEchoesMarket
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Common.Extensions;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;
    using Microsoft.Extensions.Caching.Distributed;

    using static EveEchoesMarketConstants;

    public class ItemsPricesService : IItemsPricesService
    {
        private readonly IDistributedCache distributedCache;
        private readonly string url;

        public ItemsPricesService(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
            this.url = $"{GlobalConstants.MarketApiHostName}/{GlobalConstants.MarketApiItemEndPoint}";
        }

        /// <summary>
        /// Json response format [
        ///     {
        ///         "time": 1599562800,         // a UNIX timestamp (seconds)
        ///         "sell": 264000.0,           // a calculated sell price for the item
        ///         "buy": 60000.0,             // a calculated buy price for the item
        ///         "lowest_sell": 80000.0,     // the lowest sell price for the item
        ///         "highest_buy": 60000.0,     // the highest buy price for the item
        ///         "volume": null              // a predicted volume for the item
        ///     }
        /// ].
        /// </summary>
        /// <param name="id">Item's id.</param>
        /// <returns></returns>
        public async Task<IEnumerable<ItemPrice>> GetHistoricalPricesForItemByIdAsync(long id)
        {
            var cachedValue = await this.distributedCache.GetAsync(id.ToString());

            string json;

            if (cachedValue is null)
            {
                json = await this.GetItemDataFromWebApiAsync(id);
                await this.SetDataToCacheAsync(id.ToString(), json);
            }
            else
            {
                json = Encoding.UTF8.GetString(cachedValue);
            }

            var itemPrices = GetItemsPrices(json);

            return itemPrices;
        }

        public async Task<ItemPrice> GetLatestPriceAsync(long id)
        {
            var key = $"Last{id}";

            var cachedValue = await this.distributedCache.GetAsync(key);

            string json;

            if (cachedValue is null)
            {
                var lastPrice = (await this.GetHistoricalPricesForItemByIdAsync(id)).Last();

                json = JsonSerializer.Serialize(lastPrice);
                await this.SetDataToCacheAsync(key, json);

                return lastPrice;
            }

            json = Encoding.UTF8.GetString(cachedValue);
            var result = JsonDocument.Parse(json);

            return ParseItemPrice(result.RootElement);
        }

        public async Task<IDictionary<long, ItemPrice>> GetItemPricesAsync(IEnumerable<long> itemIds)
        {
            if (itemIds is null || !itemIds.Any())
            {
                return null;
            }

            itemIds = itemIds.Distinct();

            var itemPrices = new Dictionary<long, ItemPrice>();

            foreach (var itemId in itemIds)
            {
                var price = await this.GetLatestPriceAsync(itemId);
                itemPrices.Add(itemId, price);
            }

            return itemPrices;
        }

        private static ItemPrice ParseItemPrice(JsonElement price)
        {
            var timeAsString = price.GetProperty(PriceItem.Time).ToString() ?? string.Empty;
            var isUnixTime = long.TryParse(timeAsString, out var time);

            DateTime dateTime;

            if (isUnixTime)
            {
                dateTime = DateTimeExtensions.DateTimeFromUnixTimestamp(time);
            }
            else
            {
                dateTime = DateTime.Parse(timeAsString);
            }

            _ = decimal.TryParse(price.GetProperty(PriceItem.Sell).ToString(), out var sell);
            _ = decimal.TryParse(price.GetProperty(PriceItem.Buy).ToString(), out var buy);
            _ = decimal.TryParse(price.GetProperty(PriceItem.LowestSell).ToString(), out var lowestSell);
            _ = decimal.TryParse(price.GetProperty(PriceItem.HighestBuy).ToString(), out var highestBuy);
            _ = long.TryParse(price.GetProperty(PriceItem.Volume).ToString(), out var volume);

            var itemPrice = new ItemPrice()
            {
                Time = dateTime,
                Sell = sell,
                Buy = buy,
                LowestSell = lowestSell,
                HighestBuy = highestBuy,
                Volume = volume,
            };
            return itemPrice;
        }

        private static List<ItemPrice> GetItemsPrices(string json)
        {
            var result = JsonDocument.Parse(json);

            var itemsPrices = new List<ItemPrice>();

            foreach (var price in result.RootElement.EnumerateArray())
            {
                var itemPrice = ParseItemPrice(price);

                itemsPrices.Add(itemPrice);
            }

            return itemsPrices;
        }

        private async Task<string> GetItemDataFromWebApiAsync(long id)
        {
            using var httpClient = new HttpClient();

            using var response = await httpClient.GetAsync($"{this.url}/{id}");

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        private async Task SetDataToCacheAsync(string key, string json)
        {
            if (json != null)
            {
                var options = new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(GlobalConstants.MarketApiCachingIntervalInMinutes),
                };

                await this.distributedCache.SetAsync(key, Encoding.ASCII.GetBytes(json), options);
            }
        }
    }
}

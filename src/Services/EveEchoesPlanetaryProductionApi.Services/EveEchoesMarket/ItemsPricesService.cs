﻿namespace EveEchoesPlanetaryProductionApi.Services.EveEchoesMarket
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

    /// <summary>
    /// This service is consuming external API for item prices and uses Distributed SQL Cache.
    /// </summary>
    public class ItemsPricesService : IItemsPricesService
    {
        private readonly IDistributedCache distributedCache;
        private readonly string url;
        private readonly string keyTemplate = "Last{0}";
        private readonly string jsonError = "No item found with id";

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
        /// <remarks>Not found item response: No item found with id.</remarks>
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

            if (json.Contains(this.jsonError))
            {
                return null;
            }

            var itemPrices = GetItemsPrices(json);

            return itemPrices;
        }

        public async Task<ItemPrice> GetLatestPricesAsync(long itemId)
        {
            var key = string.Format(this.keyTemplate, itemId);
            var cachedValue = await this.distributedCache.GetAsync(key);

            string json;

            if (cachedValue is null)
            {
                var lastPrice = (await this.GetHistoricalPricesForItemByIdAsync(itemId))?.Last();

                if (lastPrice is null)
                {
                    return DefaultLastPriceValue();
                }

                json = JsonSerializer.Serialize(lastPrice);
                await this.SetDataToCacheAsync(key, json);

                return lastPrice;
            }

            json = Encoding.UTF8.GetString(cachedValue);
            var result = JsonDocument.Parse(json);

            return ParseItemPrice(result.RootElement);
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

        private static ItemPrice DefaultLastPriceValue()
            => new ()
                {
                    Time = DateTime.UtcNow,
                    Sell = 0,
                    Buy = 0,
                    LowestSell = 0,
                    HighestBuy = 0,
                    Volume = 0,
                };

        private static string DefaultJsonValue()
        {
            var defaultItemPrice = new[]
            {
                DefaultLastPriceValue(),
            };

            return JsonSerializer.Serialize(defaultItemPrice);
        }

        private async Task<string> GetItemDataFromWebApiAsync(long id)
        {
            using var httpClient = new HttpClient();

            try
            {
                using var response = await httpClient.GetAsync($"{this.url}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return DefaultJsonValue();
                }

                var json = await response.Content.ReadAsStringAsync();
                return json;
            }
            catch (Exception e)
            {
                if (e is HttpRequestException)
                {
                    return DefaultJsonValue();
                }

                throw;
            }
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

namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Items.GetItem;
    using EveEchoesPlanetaryProductionApi.Api.Models.Items.GetItemsPrices;
    using EveEchoesPlanetaryProductionApi.Services.Data;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService itemsService;

        public ItemsController(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }

        [HttpPost]
        [Route("~/api/items/{itemId:long}/price")]
        public async Task<IActionResult> GetItemPrice(long itemId, [FromBody] GetItemInputModel inputModel)
        {
            var success = Enum.TryParse<PriceSelector>(inputModel.PriceSelector, out var priceSelector);

            if (!success)
            {
                return this.BadRequest();
            }

            var item = await this.itemsService.GetLatestPricesAsync(itemId);

            var price = GetPrice(item, priceSelector);

            var model = new GetItemPriceModel()
            {
                Price = price,
                Time = item.Time,
            };

            return this.Ok(model);
        }

        [HttpPost]
        [Route("~/api/items/prices")]
        public async Task<IActionResult> GetItemsPrices([FromBody] GetItemsPricesInputModel inputModel)
        {
            var success = Enum.TryParse<PriceSelector>(inputModel.PriceSelector, out var priceSelector);

            if (!success)
            {
                return this.BadRequest();
            }

            var items = await this.itemsService.GetLatestItemsPricesAsync(inputModel.ItemIds);

            var model = new GetItemsPricesModel()
            {
                Prices = items.Select(i => new GetItemsPricesPriceModel()
                {
                    Id = i.Key,
                    Time = i.Value.Time,
                    Price = GetPrice(i.Value, priceSelector),
                }),
            };

            return this.Ok(model);
        }

        private static decimal GetPrice(ItemPrice item, PriceSelector selector)
            => selector switch
            {
                PriceSelector.Sell => item.Sell,
                PriceSelector.Buy => item.Buy,
                PriceSelector.LowestSell => item.LowestSell,
                PriceSelector.HighestBuy => item.HighestBuy,
                _ => 0M
            };
    }
}

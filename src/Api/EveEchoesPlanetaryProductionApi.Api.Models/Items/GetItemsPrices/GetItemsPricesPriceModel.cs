namespace EveEchoesPlanetaryProductionApi.Api.Models.Items.GetItemsPrices
{
    using System;

    public class GetItemsPricesPriceModel
    {
        public long Id { get; set; }

        public decimal Price { get; set; }

        public DateTime Time { get; set; }
    }
}

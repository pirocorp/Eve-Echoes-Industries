namespace EveEchoesPlanetaryProductionApi.Api.Models.Items.GetItemsPrices
{
    using System.Collections.Generic;

    public class GetItemsPricesInputModel
    {
        public IEnumerable<long> ItemIds { get; set; }

        public string PriceSelector { get; set; }
    }
}

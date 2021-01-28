namespace EveEchoesPlanetaryProductionApi.Api.Models.Items.GetItemsPrices
{
    using System.Collections.Generic;

    public class GetItemsPricesModel
    {
        public IEnumerable<GetItemsPricesPriceModel> Prices { get; set; }
    }
}

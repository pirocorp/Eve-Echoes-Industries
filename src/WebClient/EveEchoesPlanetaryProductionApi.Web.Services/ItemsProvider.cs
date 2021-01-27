namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Items.GetItem;
    using EveEchoesPlanetaryProductionApi.Api.Models.Items.GetItemsPrices;

    public class ItemsProvider : IItemsProvider
    {
        private readonly HttpClient httpClient;

        public ItemsProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<GetItemPriceModel> GetItemPrice(GetItemInputModel model, long itemId)
        {
            var result = await this.httpClient.PostAsJsonAsync($"api/items/{itemId}/price", model);

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await result.Content.ReadFromJsonAsync<GetItemPriceModel>();
        }

        public async Task<GetItemsPricesModel> GetItemsPrices(GetItemsPricesInputModel model)
        {
            var result = await this.httpClient.PostAsJsonAsync("api/items/prices", model);

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return await result.Content.ReadFromJsonAsync<GetItemsPricesModel>();
        }
    }
}
namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Items.GetItem;
    using EveEchoesPlanetaryProductionApi.Api.Models.Items.GetItemsPrices;

    public interface IItemsProvider
    {
        Task<GetItemPriceModel> GetItemPrice(GetItemInputModel model, long itemId);

        Task<GetItemsPricesModel> GetItemsPrices(GetItemsPricesInputModel model);
    }
}

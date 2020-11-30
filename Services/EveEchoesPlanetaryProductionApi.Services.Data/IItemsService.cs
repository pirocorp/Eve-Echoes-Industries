namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Services.Data.Models.IItemsService;
    using EveEchoesPlanetaryProductionApi.Services.Models.EveEchoesMarket;

    public interface IItemsService
    {
        Task<IEnumerable<ItemServiceModel>> GetPlanetaryResources(PriceSelector priceSelector);
    }
}

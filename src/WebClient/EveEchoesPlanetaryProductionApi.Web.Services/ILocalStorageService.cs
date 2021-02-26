namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Threading.Tasks;

    public interface ILocalStorageService
    {
        Task<T> GetItem<T>(string key)
            where T : class;

        Task SetItem<T>(string key, T value)
            where T : class;

        Task RemoveItem(string key);
    }
}

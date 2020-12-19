namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Text.Json;
    using System.Threading.Tasks;

    using Microsoft.JSInterop;

    public class LocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime runtimeJs;

        public LocalStorageService(IJSRuntime runtimeJs)
        {
            this.runtimeJs = runtimeJs;
        }

        public async Task<T> GetItem<T>(string key)
            where T : class
        {
            var json = await this.runtimeJs.InvokeAsync<string>("localStorage.getItem", key);

            return json is null ? null : JsonSerializer.Deserialize<T>(json);
        }

        public async Task SetItem<T>(string key, T value)
            where T : class
        {
            await this.runtimeJs.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));
        }

        public async Task RemoveItem(string key)
        {
            await this.runtimeJs.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}

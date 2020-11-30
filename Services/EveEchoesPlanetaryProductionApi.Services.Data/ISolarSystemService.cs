namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Services.Data.Models.GetSolarSystemById;

    public interface ISolarSystemService
    {
        Task<SolarSystemServiceModel> GetByIdAsync(long id);

        Task<TOut> GetByIdAsync<TOut>(long id);

        Task<TOut> GetByNameAsync<TOut>(string name);
    }
}

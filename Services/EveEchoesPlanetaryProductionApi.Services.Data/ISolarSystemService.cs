namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Threading.Tasks;

    public interface ISolarSystemService
    {
        Task<TOut> GetById<TOut>(long id);

        Task<TOut> GetByName<TOut>(string name);
    }
}

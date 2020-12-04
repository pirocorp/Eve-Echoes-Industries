namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Threading.Tasks;

    public interface IRegionsService
    {
        Task<int> GetCount();
    }
}

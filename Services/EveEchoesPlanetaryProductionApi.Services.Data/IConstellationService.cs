namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Threading.Tasks;

    public interface IConstellationService
    {
        Task<int> GetCount();
    }
}

namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IConstellationService
    {
        Task<int> GetCount();

        Task<IEnumerable<TOut>> GetAllAsync<TOut>(int pageSize, int page = 1);
    }
}

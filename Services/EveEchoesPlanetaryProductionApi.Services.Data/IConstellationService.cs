﻿namespace EveEchoesPlanetaryProductionApi.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IConstellationService
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<TOut>> GetAllAsync<TOut>(int pageSize, int page = 1);

        Task<TOut> GetByIdAsync<TOut>(long id);
    }
}

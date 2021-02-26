namespace EveEchoesPlanetaryProductionApi.Services
{
    using System.Collections.Generic;

    using EveEchoesPlanetaryProductionApi.Data.Models;

    public interface IAuthService
    {
        string GenerateJwt(User user, IList<string> roles);
    }
}

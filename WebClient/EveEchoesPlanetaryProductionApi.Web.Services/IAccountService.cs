namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Auth;

    public interface IAccountService
    {
        Task Initialize();

        Task<bool> Login(UserLoginInputModel model);

        Task Register(UserRegisterInputModel model);

        Task Logout();
    }
}

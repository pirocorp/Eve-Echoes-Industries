namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Models.Auth;
    using EveEchoesPlanetaryProductionApi.Web.Common;

    using Microsoft.AspNetCore.Components;

    public class AccountService : IAccountService
    {
        private readonly NavigationManager navigationManager;
        private readonly IHttpService httpService;
        private readonly ILocalStorageService localStorageService;
        private readonly IAppDataService appDataService;

        public AccountService(
            NavigationManager navigationManager,
            IHttpService httpService,
            ILocalStorageService localStorageService,
            IAppDataService appDataService)
        {
            this.navigationManager = navigationManager;
            this.httpService = httpService;
            this.localStorageService = localStorageService;
            this.appDataService = appDataService;
        }

        public async Task Initialize()
        {
            this.appDataService.User = await this.localStorageService.GetItem<UserResponseModel>(PresentationConstants.UserKey);
            this.appDataService.StateHasChanged();
        }

        public async Task<bool> Login(UserLoginInputModel model)
        {
            this.appDataService.User = await this.httpService.Post<UserResponseModel>("api/users/signIn", model);

            if (this.appDataService.User is null)
            {
                this.appDataService.StateHasChanged();
                return false;
            }

            await this.localStorageService.SetItem(PresentationConstants.UserKey, this.appDataService.User);
            this.appDataService.StateHasChanged();
            return true;
        }

        public async Task Register(UserRegisterInputModel model)
        {
            await this.httpService.Post("api/users/signUp", model);
            this.appDataService.StateHasChanged();
        }

        public async Task Logout()
        {
            this.appDataService.User = null;
            await this.localStorageService.RemoveItem(PresentationConstants.UserKey);
            this.navigationManager.NavigateTo("/");
            this.appDataService.StateHasChanged();
        }
    }
}

namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Api.Infrastructure;
    using EveEchoesPlanetaryProductionApi.Api.Models.Auth;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IAuthService authService;

        public AuthController(
            UserManager<User> userManager,
            IAuthService authService)
        {
            this.userManager = userManager;
            this.authService = authService;
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody]UserRegisterInputModel inputModel)
        {
            var user = new User()
            {
                UserName = inputModel.Username,
                Email = inputModel.Email,
            };

            var userCreatedResult = await this.userManager.CreateAsync(user, inputModel.Password);

            if (userCreatedResult.Succeeded)
            {
                return this.Ok();
            }

            return this.BadRequest(userCreatedResult.Errors);
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SigIn([FromBody]UserLoginInputModel inputModel)
        {
            var user = await this.userManager.Users.SingleOrDefaultAsync(u => u.UserName.Equals(inputModel.Username));

            if (user is null)
            {
                return this.BadRequest(new { Error = ApiMessagesConstants.InvalidCredentials });
            }

            var userSignInResult = await this.userManager.CheckPasswordAsync(user, inputModel.Password);

            if (userSignInResult)
            {
                var roles = await this.userManager.GetRolesAsync(user);
                return this.Ok(this.authService.GenerateJwt(user, roles));
            }

            return this.BadRequest(new { Error = ApiMessagesConstants.InvalidCredentials });
        }
    }
}

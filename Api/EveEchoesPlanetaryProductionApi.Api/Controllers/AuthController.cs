namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Api.Infrastructure;
    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Api.Models.Auth;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IAuthService authService;
        private readonly IMapper mapper;

        public AuthController(
            UserManager<User> userManager,
            IAuthService authService,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.authService = authService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("~/api/users/signIn")]
        public async Task<IActionResult> SigIn([FromBody]UserLoginInputModel inputModel)
        {
            var user = await this.userManager.Users.SingleOrDefaultAsync(u => u.UserName.Equals(inputModel.Username));
            var userSignInResult = await this.userManager.CheckPasswordAsync(user, inputModel.Password);

            if (user is null || !userSignInResult)
            {
                var error = new ApiErrorModel()
                {
                    Code = nameof(user),
                    Description = ApiMessagesConstants.InvalidCredentials,
                };

                return this.BadRequest(new List<ApiErrorModel> { error });
            }

            var roles = await this.userManager.GetRolesAsync(user);

            var response = this.mapper.Map<UserResponseModel>(user);
            response.Token = this.authService.GenerateJwt(user, roles);

            return this.Ok(response);
        }

        [HttpPost]
        [Route("~/api/users/signUp")]
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
    }
}

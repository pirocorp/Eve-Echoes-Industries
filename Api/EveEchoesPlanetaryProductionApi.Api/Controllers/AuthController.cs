namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Api.Infrastructure;
    using EveEchoesPlanetaryProductionApi.Api.Models;
    using EveEchoesPlanetaryProductionApi.Api.Models.Auth;
    using EveEchoesPlanetaryProductionApi.Common;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services;
    using EveEchoesPlanetaryProductionApi.Services.Messaging;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IAuthService authService;
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;

        public AuthController(
            UserManager<User> userManager,
            IAuthService authService,
            IMapper mapper,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.authService = authService;
            this.mapper = mapper;
            this.emailSender = emailSender;
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
                await this.SendEmailValidation(user);

                return this.Ok();
            }

            return this.BadRequest(userCreatedResult.Errors);
        }

        [HttpPost]
        [Route("~/api/users/email/confirm")]
        public async Task<IActionResult> ConfirmEmail([FromBody] EmailConfirmationInput input)
        {
            var user = await this.userManager.FindByEmailAsync(input.Email);

            var result = await this.userManager.ConfirmEmailAsync(user, input.Token);

            if (result.Succeeded)
            {
                return this.Ok();
            }

            var error = new ApiErrorModel()
            {
                Code = nameof(user),
                Description = "Email wasn't confirmed",
            };

            return this.BadRequest(new List<ApiErrorModel> { error });
        }

        private async Task SendEmailValidation(User user)
        {
            var userId = await this.userManager.GetUserIdAsync(user);
            var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var emailConfirmationUrl = this.Request.Scheme
                                       + Uri.SchemeDelimiter
                                       + this.Request.Host
                                       + GlobalConstants.Email.EmailValidationPath
                                       + "/"
                                       + GlobalConstants.Email.EmailValidationParameter
                                       + "/"
                                       + code
                                       + "/"
                                       + nameof(GlobalConstants.Email).ToLower()
                                       + "/"
                                       + user.Email;

            var html = await System.IO.File.ReadAllTextAsync(GlobalConstants.FilePaths.ConfirmEmailTemplate);
            html = html.Replace("{url}", emailConfirmationUrl);

            await this.emailSender.SendEmailAsync(
                GlobalConstants.Email.From,
                GlobalConstants.Email.FromName,
                user.Email,
                GlobalConstants.Email.Subject,
                html);
        }
    }
}

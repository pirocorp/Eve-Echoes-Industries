namespace EveEchoesPlanetaryProductionApi.Api.Models.Auth
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class UserRegisterInputModel : IMapTo<User>, IHaveCustomMappings
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "The Password field must be a minimum of 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [IgnoreMap]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<UserRegisterInputModel, User>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.Username));
        }
    }
}

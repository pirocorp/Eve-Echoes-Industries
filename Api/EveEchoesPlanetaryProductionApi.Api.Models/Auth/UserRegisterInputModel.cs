namespace EveEchoesPlanetaryProductionApi.Api.Models.Auth
{
    using System.ComponentModel.DataAnnotations;

    public class UserRegisterInputModel
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

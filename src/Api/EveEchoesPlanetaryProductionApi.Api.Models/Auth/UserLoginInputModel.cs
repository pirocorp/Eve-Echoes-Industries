namespace EveEchoesPlanetaryProductionApi.Api.Models.Auth
{
    using System.ComponentModel.DataAnnotations;

    public class UserLoginInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

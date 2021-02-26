namespace EveEchoesPlanetaryProductionApi.Api.Models.Auth
{
    using AutoMapper;
    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Mapping;

    public class UserResponseModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [IgnoreMap]
        public string Token { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<User, UserResponseModel>()
                .ForMember(d => d.Username, opt => opt.MapFrom(s => s.UserName));
        }
    }
}

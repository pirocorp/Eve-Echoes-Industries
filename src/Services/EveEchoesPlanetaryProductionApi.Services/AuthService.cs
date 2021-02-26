namespace EveEchoesPlanetaryProductionApi.Services
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;

    using EveEchoesPlanetaryProductionApi.Data.Models;
    using EveEchoesPlanetaryProductionApi.Services.Settings;

    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    public class AuthService : IAuthService
    {
        private readonly IOptionsSnapshot<JwtSettings> jwtSettings;

        public AuthService(IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        public string GenerateJwt(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtSettings.Value.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(this.jwtSettings.Value.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: this.jwtSettings.Value.Issuer,
                audience: this.jwtSettings.Value.Issuer,
                claims,
                expires: expires,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

using CA_Final_Regia.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CA_Final_Regia.Services.JwtService
{
    public class JwtService(IConfiguration configuration) : IJwtService
    {
        public readonly IConfiguration _configuration = configuration;

        public string GenerateToken(string username, string role)
        {
            var key = _configuration["Jwt:Key"];
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha512Signature);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role),
            };

            var jwt = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(4),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}

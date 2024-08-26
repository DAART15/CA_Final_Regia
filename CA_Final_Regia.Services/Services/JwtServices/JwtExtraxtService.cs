using CA_Final_Regia.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace CA_Final_Regia.Services.Services.JwtServices
{
    public class JwtExtraxtService : IJwtExtraxtService
    {
        public Guid GetAccountIdFromJwtToken(string authorizationHeader)
        {
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return Guid.Empty;
            }
            string trimedJwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(trimedJwtToken);
            var usernameClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(usernameClaim?.Value, out Guid accountId))
            {
                return Guid.Empty;
            }
            return accountId;
        }
    }
}

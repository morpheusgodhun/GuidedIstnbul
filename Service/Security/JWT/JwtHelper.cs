using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Security.JWT
{
    public class JwtHelper
    {
        public static string GetJwtToken(string username, JwtSettings jwtSettings, TimeSpan expiration, Claim[] additionalClaims = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.UniqueName,username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            if (additionalClaims is object)
            {
                var claimList = new List<Claim>(claims);
                claimList.AddRange(additionalClaims);
                claims = claimList.ToArray();
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: DateTime.UtcNow.Add(expiration),
                claims: claims,
                signingCredentials: creds
            );

            return tokenHandler.WriteToken(token);
        }
        public static ClaimsPrincipal ValidateToken(string token, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey); //appsettings den gelmeli

            var claims = tokenHandler.ValidateToken(token, new TokenValidationParameters //hata fırlatabilir
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
            //var jwtToken = (JwtSecurityToken)validatedToken;

            return claims;
        }
    }
}

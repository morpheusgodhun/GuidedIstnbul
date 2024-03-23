using GuidePanel.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GuidePanel.Helpers
{

    public class Cookie : ICookie
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly string JwtCookieKey = "JwtCookie";
        public Cookie(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetCookie() => (_httpContextAccessor.HttpContext.Request.Cookies[JwtCookieKey] ?? "").Trim();

        public UserViewModel getMemberInfo()
        {
            var tokenStr = GetCookie();

            var guestUser = new UserViewModel() { Role = "Guest" };

            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return guestUser;

            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken? token = tokenHandler.ReadJwtToken(tokenStr);

            if (token == null)
                return guestUser;

            try
            {
                UserViewModel memberInfo = new()
                {
                    UserId = token.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault(),
                    UserName = token.Claims.Where(x => x.Type == ClaimTypes.Name).Select(x => x.Value).FirstOrDefault(),
                    Role = token.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).FirstOrDefault(),
                    Permission = token.Claims.Where(x => x.Type == "Permission").Select(x => x.Value).ToList()
                };

                return memberInfo;
            }
            catch (Exception)
            {
                return guestUser;
            }

        }
        public async Task SetLoginCookie(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken? jwtToken = tokenHandler.ReadJwtToken(token);

            if (jwtToken != null)
            {
                ClaimsIdentity identity = new(jwtToken.Claims, JwtBearerDefaults.AuthenticationScheme);
                AuthenticationProperties props = new()
                {
                    IsPersistent = true,
                };
                await _httpContextAccessor.HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), props);

                //CookieOptions options = new()
                //{
                //    Expires = DateTime.Now.AddDays(2),
                //    MaxAge = TimeSpan.FromDays(2),
                //    HttpOnly = true,
                //};

                //_httpContextAccessor.HttpContext.Response.Cookies.Append(CookieKey, token, options);
            }
        }
        //logout
        public async Task RemoveJWT()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync();
            //_httpContextAccessor.HttpContext.Response.Cookies.Delete(CookieKey);
            //_httpContextAccessor.HttpContext.Response.Cookies.Delete("JwtCookie"); //program cs de eklenen jwt cookie 
        }
    }
    public interface ICookie
    {
        string GetCookie();
        Task SetLoginCookie(string value);
        Task RemoveJWT();

    }
}

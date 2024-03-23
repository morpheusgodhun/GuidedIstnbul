using Core.StaticClass;
using GuideWeb.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GuideWeb.Helpers
{
    public class Cookie : ICookie
    {
        private readonly IHttpContextAccessor _contextAccessor;
        readonly string LanguageCookieKey = "language";
        readonly string JwtCookieKey = "security-token";
        readonly IConfiguration _configuration;
        public Cookie(IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }
        public string getJwtCookie()
        {
            return (_contextAccessor.HttpContext?.Request.Cookies[JwtCookieKey] ?? "").Trim();
        }
        public string getMemberId()
        {
            var tokenStr = getJwtCookie();

            if (string.IsNullOrEmpty(tokenStr))
                return "";

            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken? token = tokenHandler.ReadJwtToken(tokenStr);

            if (token == null)
                return "";

            var MemberId = token.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault() ?? "";

            return MemberId;
        }
        public UserViewModel getMemberInfo()
        {
            var tokenStr = getJwtCookie();

            var guestUser = new UserViewModel() { Role = "Guest" };

            if (string.IsNullOrEmpty(tokenStr))
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
        public string getMemberAgentId()
        {
            var tokenStr = getJwtCookie();

            if (string.IsNullOrEmpty(tokenStr))
                return "";

            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken? token = tokenHandler.ReadJwtToken(tokenStr);

            if (token == null)
                return "";

            var agentId = token.Claims.Where(x => x.Type == "Agent").Select(x => x.Value).FirstOrDefault() ?? "";
            return agentId;
        }
        public string getMemberGuideId()
        {
            var tokenStr = getJwtCookie();

            if (string.IsNullOrEmpty(tokenStr))
                return "";

            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken? token = tokenHandler.ReadJwtToken(tokenStr);

            if (token == null)
                return "";

            var MemberId = token.Claims.Where(x => x.Type == "Guide").Select(x => x.Value).FirstOrDefault() ?? "";

            return MemberId;
        }
        public void SetLanguageCookie(int langId)
        {
            string langPrefix = LanguageList.GetPrefix(langId);
            _contextAccessor.HttpContext?.Response.Cookies.Append(LanguageCookieKey, langPrefix, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(30),
                HttpOnly = true,
                IsEssential = true,
                MaxAge = TimeSpan.FromDays(30),
                //Domain = _configuration.GetValue<string>("DomainUrl")
            });
        }
        public Language GetLanguage()
        {
            string languagePrefix = string.Empty;
            _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(LanguageCookieKey, out languagePrefix);

            if (string.IsNullOrEmpty(languagePrefix))
                return null;

            var language = LanguageList.GetLangaugeByPrefix(languagePrefix);
            return language is not null ? language : null;
        }

        public void ChangeLanguage(int targetLanguageId)
        {
            string newLanguagePrefix = LanguageList.GetPrefix(targetLanguageId);
            _contextAccessor.HttpContext?.Response.Cookies.Delete(LanguageCookieKey);
            _contextAccessor.HttpContext?.Response.Cookies.Append(LanguageCookieKey, newLanguagePrefix);
        }
    }

    public interface ICookie
    {
        public string getJwtCookie();
        public string getMemberId();
        public UserViewModel getMemberInfo();
        public string getMemberAgentId();
        public string getMemberGuideId();
        void SetLanguageCookie(int langId);
        Language GetLanguage();
        void ChangeLanguage(int targetLanguageId);
    }

}

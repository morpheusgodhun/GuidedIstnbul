
using Core.IService;
using Dto.ApiWebDtos;
using Dto.ApiWebDtos.ApiToWebDtos.GeneralPages;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using GuideWeb.APIHandler;
using GuideWeb.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GuideWeb.Controllers
{
    public class AuthController : CustomBaseController
    {
        private readonly IGoogleRecaptchaService _recaptchaService;
        public AuthController(IApiHandler apiHandler, IConfiguration configuration, ICookie cookie, IGoogleRecaptchaService recaptchaService) : base(apiHandler, configuration, cookie)
        {
            _recaptchaService = recaptchaService;
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginPostDto loginDto)
        {
            List<string> errorMessages = new List<string>();

            string url = _url + "Auth/Login";
            var response = _apiHandler.PostApi<CustomResponseDto<string>>(loginDto, url);
            var captchaResult = await _recaptchaService.CheckCaptcha2();
            if (response.StatusCode == StatusCodes.Status404NotFound) errorMessages.Add(response.Errors.First());
            if (captchaResult["error-codes"] != null)
            {
                string captchaErrorMessage = captchaResult["error-codes"].ToString();
                captchaErrorMessage = captchaErrorMessage.Replace("[", "").Replace("]", "").Replace("-", " ");
                errorMessages.Add(captchaErrorMessage);
            }



            //if (!captchaResult) errorMessages.Add("Google ReCaptcha Doğrulama Başarısız Oldu. Lütfen Tekrar Deneyiniz");

            TempData["ErrorMessages"] = errorMessages;


            if (response.Errors != null)
            {
                ModelState.AddModelError("Password", "Kullanıcı adı veya şifre hatalı");
                string loginRedirectUrl = $"/{_cookie.GetLanguage().UrlPrefix}/login";
                return Redirect(loginRedirectUrl); //TODO: Burası değişecek değişmesi lazım kesin bilgi / değişti , modelState errors nasıl gidecek?
            }

            if (captchaResult["error-codes"] != null)
            {
                ModelState.AddModelError("GoogleReCaptcha", "Doğrulama Başarısız");
            }

            //    string loginRedirectUrl = $"/{_cookie.GetLanguage().UrlPrefix}/login";
            //    return Redirect(loginRedirectUrl);

            //}



            JwtSecurityTokenHandler tokenHandler = new();

            JwtSecurityToken? token = tokenHandler.ReadJwtToken(response.Data);
            if (token != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(token.Claims, JwtBearerDefaults.AuthenticationScheme);
                var authProps = new AuthenticationProperties
                {
                    IsPersistent = true
                };
                HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProps);
                //Id Dönülecek GERİYE
                //string name = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
                //string RoleIdentifier = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

                /*
                 claims.Add(new Claim(ClaimTypes.Email, loginPostDto.Email));
                claims.Add(new Claim(ClaimTypes.Name, nameSurname));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, nameSurname));
                claims.Add(new Claim(ClaimTypes.Role, item));
                 */

                /*ç
                var MemberName = token.Claims.Where(x => x.Type == ClaimTypes.Name).Select(x => x.Value).FirstOrDefault();
                var MemberId = token.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault();
                var roles = token.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
                */

                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(2),
                    MaxAge = TimeSpan.FromDays(2),
                    HttpOnly = true
                };

                Response.Cookies.Append("security-token", response.Data.ToString(), options);
                //HttpContext.Response.Cookies.Append("MemberId", MemberId, options);
                //HttpContext.Response.Cookies.Append("CompanyId", result.CompanyId.ToString(), options);
                //HttpContext.Response.Cookies.Append("roles", roles.FirstOrDefault(), options);
            }
            return RedirectPermanent("/");
        }

        public async Task<RedirectResult> Register(RegisterPostDto registerPostDto)
        {
            string url = _url + "Auth/Register";
            List<string> errorMessages = new List<string>();

            var response = _apiHandler.PostApi<CustomResponseNullDto>(registerPostDto, url);
            var captchaResult = await _recaptchaService.CheckCaptcha2();


            if (response.StatusCode == StatusCodes.Status400BadRequest) errorMessages.Add(response.Errors.First());
            if (captchaResult["error-codes"] != null)
            {
                string captchaErrorMessage = captchaResult["error-codes"].ToString();
                captchaErrorMessage = captchaErrorMessage.Replace("[", "").Replace("]", "").Replace("-", " ");
                errorMessages.Add(captchaErrorMessage);
            }

            TempData["ErrorMessages"] = errorMessages;

            if (response.Errors != null)
            {
                ModelState.AddModelError("Register", "Bu Email ile kayıtlı kullanıcı var");
                string redirectUrl = $"/{_cookie.GetLanguage().UrlPrefix}/register";
                return Redirect(redirectUrl);
            }
            if (captchaResult["error-codes"] != null)
            {
                ModelState.AddModelError("GoogleReCaptcha", "Doğrulama Başarısız");
                string redirectUrl = $"/{_cookie.GetLanguage().UrlPrefix}/register";
                return Redirect(redirectUrl);
            }

            /*
            if (response.StatusCode == 400 || response.Errors.Count > 0)


            if (response.StatusCode == 400 || response.Errors?.Count > 0)

            {
                ViewBag.EmailAlreadyExistMessage = response.Errors.FirstOrDefault();
                return RedirectToAction("Register", "CustomPage", new { error = "Error" }, "");
            }*/

            //logine yönlendiriliyor
            string loginRedirectUrl = $"/{_cookie.GetLanguage().UrlPrefix}/login";

            return Redirect(loginRedirectUrl);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Response.Cookies.Delete("security-token");
            //HttpContext.Response.Cookies.Delete("MemberId");
            //HttpContext.Response.Cookies.Delete("CompanyId");
            //HttpContext.Response.Cookies.Delete("roles");
            return Redirect("/"); //RedirectToAction("Index", "Auth");
        }
    }
}

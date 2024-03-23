using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using FluentValidation;
using FluentValidation.Results;
using GuidePanel.APIHandler;
using GuidePanel.FluentValidation.Auth;
using GuidePanel.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GuidePanel.Controllers
{
    public class AuthController : CustomBaseController
    {
        readonly IValidator<LoginPostDto> _loginValidator;
        readonly ICookie _cookie;
        public AuthController(IApiHandler apiHandler, IConfiguration configuration, IValidator<LoginPostDto> loginValidator, ICookie cookie) : base(apiHandler, configuration)
        {
            _loginValidator = loginValidator;
            _cookie = cookie;
        }
        public IActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginPostDto loginPostDto)
        {
            ValidationResult validationResult = _loginValidator.Validate(loginPostDto);
            if (!validationResult.IsValid)
            {
                return View();
            }

            string url = _url + "Auth/PanelLogin";
            CustomResponseDto<string> loginResponse = _apiHandler.PostApi<CustomResponseDto<string>>(loginPostDto, url);
            if (loginResponse.Errors is not null && loginResponse.Errors.Count > 0)
            {
                if (loginResponse.StatusCode == 404)
                    ViewBag.UserNotFoundErrorMessage = "User not found";
                if (loginResponse.StatusCode == 400)
                    ViewBag.WrongPasswordErrorMessage = "Invalid password";
                if (loginResponse.StatusCode == 401)
                {
                    //TODO 
                }

                return View();
            }
            if (loginResponse.StatusCode == 200)
            {
                await _cookie.SetLoginCookie(loginResponse.Data);
                //base.SignIn() --> cookie classındaki setLoginCookie methodundaki httpContext.SignInAsync() methodu ile aynı işi yapıyor.
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            //HttpContext.User.
            await _cookie.RemoveJWT();
            return RedirectToAction("Login", "Auth");
        }
        public IActionResult Unauthorized()
        {
            return View();
        }
    }
}

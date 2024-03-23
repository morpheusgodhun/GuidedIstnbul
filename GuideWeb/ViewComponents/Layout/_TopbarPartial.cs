using Core.StaticClass;
using Dto.ApiWebDtos.ApiToWebDtos.Layout;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.GeneralDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using GuideWeb.APIHandler;
using GuideWeb.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GuideWeb.ViewComponents.Layout
{
    [ViewComponent(Name = "_TopbarPartial")]
    public class _TopbarPartial : ViewComponent
    {
        private readonly IApiHandler _apiHandler;
        private readonly IConfiguration _configuration;
        private readonly ICookie _cookie;
        private readonly string _url;

        public _TopbarPartial(IApiHandler apiHandler, IConfiguration configuration, ICookie cookie)
        {
            _apiHandler = apiHandler;
            _configuration = configuration;
            _url = _configuration["BaseUrl"];
            _cookie = cookie;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            int languageId = 1;
            string url1 = _url + $"WebCustomPage/CustomPageInfo/{languageId}";
            CustomResponseDto<CustomPageConstantValueDto> customPage = await _apiHandler.GetAsync<CustomResponseDto<CustomPageConstantValueDto>>(url1);
            var member = _cookie.getMemberInfo();

            //CustomResponseDto<>
            var pageModel = (member, customPage.Data);
            if (pageModel.Data  is null)
            {
                return View();
            }
            else
            {
                return View(pageModel);
            }


            //var member = _cookie.getMemberId();


            //return View(member);

            //int languageId = 1;

            //string url = _url + "WebLayout/Topbar/" + languageId;
            //CustomResponseDto<NavbarDto> navbarDto = _apiHandler.GetApi<CustomResponseDto<NavbarDto>>(url);

            //if (navbarDto is null)
            //{
            //    return View();
            //}
            //else
            //{
            //    return View(navbarDto.Data);
            //}
        }
    }
}

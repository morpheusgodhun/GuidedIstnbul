using Core.StaticClass;
using Dto.ApiWebDtos.ApiToWebDtos.Contact;
using Dto.ApiWebDtos.ApiToWebDtos.Faq;
using Dto.ApiWebDtos.GeneralDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using GuideWeb.APIHandler;
using GuideWeb.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GuideWeb.Controllers
{
    public class ContactController : CustomBaseController
    {
        public ContactController(IApiHandler apiHandler, IConfiguration configuration, ICookie cookie) : base(apiHandler, configuration, cookie)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            string url = _url + "WebContact/GetContact/1";
            CustomResponseDto<GetContactDto> getContactDto = _apiHandler.GetApi<CustomResponseDto<GetContactDto>>(url);


            string countryUrl = _url + "PanelOtherOption/CountrySelectList";
            CustomResponseDto<List<SelectListOption>> countrySelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOption>>>(countryUrl);

            string findUsUrl = _url + "PanelSystemOption/HowDidYouFindUsSelectList";
            CustomResponseDto<List<SelectListOptionDto>> findUsSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(findUsUrl);

            ViewBag.CountrySelectList = countrySelectList.Data;
            ViewBag.FindUsSelectList = findUsSelectList.Data;

            if (getContactDto is null)
            {
                return View();
            }
            else
            {
                return View(getContactDto.Data);
            }
        }

        [HttpPost]
        public IActionResult Index(ContactPostDto contactPostDto)
        {
            string url = _url + "WebContact/PostContact";
            _apiHandler.PostApi<CustomResponseNullDto>(contactPostDto, url);
            return RedirectToAction("Index");
        }
    }
}

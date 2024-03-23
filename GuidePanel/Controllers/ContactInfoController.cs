using Dto.ApiPanelDtos.ContactInfoDtos;
using Dto.ApiPanelDtos.LanguageDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.ContactInfoManagement)]

    public class ContactInfoController : CustomBaseController
    {
        public ContactInfoController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            string url = _url + "PanelContactInfo/ContactInfoList";
            CustomResponseDto<List<ContactInfoListDto>> contactInfoListDtos = _apiHandler.GetApi<CustomResponseDto<List<ContactInfoListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);
            if (contactInfoListDtos is null)
            {
                return View();
            }
            else
            {
                var model = (contactInfoListDtos.Data, languageListDto.Data);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddContactInfo()
        {
            string url = _url + "PanelContactInfo/ContactInfoTypeList";
            CustomResponseDto<List<SelectListOption>> typeList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOption>>>(url);


            ViewBag.TypeList = typeList.Data;

            return View();
        }

        [HttpPost]
        public IActionResult AddContactInfo(AddContactInfoDto addContactInfoDto)
        {
            string url = _url + "PanelContactInfo/AddContactInfo";
            _apiHandler.PostApi<CustomResponseNullDto>(addContactInfoDto, url);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult LanguageEditContactInfo(string id, string languageId)
        {

            string url = _url + "PanelContactInfo/LanguageEditContactInfo/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditContactInfoDto> languageEditContactInfoDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditContactInfoDto>>(url);

            if (languageEditContactInfoDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditContactInfoDto.Data);
            }
        }

        [HttpPost]
        public IActionResult LanguageEditContactInfo(LanguageEditContactInfoDto languageEditContactInfoDto)
        {
            string url = _url + "PanelContactInfo/LanguageEditContactInfo";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditContactInfoDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleContactInfoStatus(string id)
        {
            string url = _url + "PanelContactInfo/ToggleContactInfoStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }
    }
}

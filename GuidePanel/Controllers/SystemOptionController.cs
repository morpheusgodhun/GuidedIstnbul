using Dto.ApiPanelDtos.LanguageDtos;
using Dto.ApiPanelDtos.SystemOptionDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.SystemOptionManagement)]

    public class SystemOptionController : CustomBaseController
    {
        public SystemOptionController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            string url = _url + "PanelSystemOption/SystemOptionList";
            CustomResponseDto<List<SystemOptionListDto>> systemOptionListDto = _apiHandler.GetApi<CustomResponseDto<List<SystemOptionListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);
            if (systemOptionListDto is null)
            {
                return View();
            }
            else
            {
                var model = (systemOptionListDto.Data, languageListDto.Data);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddSystemOption()
        {
            string systemOptionCategories = _url + "PanelSystemOption/SystemOptionCategorySelectList";
            CustomResponseDto<List<SelectListOption>> systemOptionCategoryDtos = _apiHandler.GetApi<CustomResponseDto<List<SelectListOption>>>(systemOptionCategories);

            ViewBag.SystemOptionCategories = systemOptionCategoryDtos.Data;

            return View();
        }

        [HttpPost]
        public IActionResult AddSystemOption(AddSystemOptionDto addSystemOptionDto)
        {
            string url = _url + "PanelSystemOption/AddSystemOption";
            _apiHandler.PostApi<CustomResponseNullDto>(addSystemOptionDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditSystemOption(string id)
        {
            string systemOptionCategories = _url + "PanelSystemOption/SystemOptionCategorySelectList";
            CustomResponseDto<List<SelectListOption>> systemOptionCategoryDtos = _apiHandler.GetApi<CustomResponseDto<List<SelectListOption>>>(systemOptionCategories);

            ViewBag.SystemOptionCategories = systemOptionCategoryDtos.Data;

            string url = _url + "PanelSystemOption/EditSystemOption/" + id;
            CustomResponseDto<EditSystemOptionDto> editSystemOptionDto = _apiHandler.GetApi<CustomResponseDto<EditSystemOptionDto>>(url);

            string url1 = _url + "PanelSystemOption/GetSystemOptionName/" + id;
            CustomResponseDto<string> systemOptionNameResponse = _apiHandler.GetApi<CustomResponseDto<string>>(url1);

            ViewBag.SystemOptionName = systemOptionNameResponse.Data;

            if (editSystemOptionDto is null)
            {
                return View();
            }
            else
            {
                return View(editSystemOptionDto.Data);
            }
        }
        [HttpPost]
        public IActionResult EditSystemOption(EditSystemOptionDto editSystemOptionDto)
        {
            string url = _url + "PanelSystemOption/EditSystemOption";
            _apiHandler.PostApi<CustomResponseNullDto>(editSystemOptionDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LanguageEditSystemOption(string id, string languageId)
        {
            string url = _url + "PanelSystemOption/LanguageEditSystemOption/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditSystemOptionDto> languageEditSystemOptionDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditSystemOptionDto>>(url);

            if (languageEditSystemOptionDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditSystemOptionDto.Data);
            }
        }
        [HttpPost]
        public IActionResult LanguageEditSystemOption(LanguageEditSystemOptionDto languageEditSystemOptionDto)
        {
            string url = _url + "PanelSystemOption/LanguageEditSystemOption";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditSystemOptionDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleSystemOptionStatus(string id)
        {
            string url = _url + "PanelSystemOption/ToggleSystemOptionStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }
    }
}

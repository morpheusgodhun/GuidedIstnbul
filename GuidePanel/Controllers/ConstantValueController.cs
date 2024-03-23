using Dto.ApiPanelDtos.ConstantValueDtos;
using Dto.ApiPanelDtos.LanguageDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.ConstantValueManagement)]

    public class ConstantValueController : CustomBaseController
    {
        public ConstantValueController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            string url = _url + "PanelConstantValue/ConstantValueList";
            CustomResponseDto<List<ConstantValueListDto>> constantValueListDto = _apiHandler.GetApi<CustomResponseDto<List<ConstantValueListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);
            if (constantValueListDto is null)
            {
                return View();
            }
            else
            {
                var model = (constantValueListDto.Data, languageListDto.Data);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddConstantValue()
        {
            string pageUrl = _url + "PanelPage/PageSelectList";
            CustomResponseDto<List<SelectListOptionDto>> pageSelectList = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(pageUrl);

            ViewBag.PageSelectList = pageSelectList.Data;

            return View();
        }

        [HttpPost]
        public IActionResult AddConstantValue(AddConstantValueDto addConstantValueDto)
        {
            string url = _url + "PanelConstantValue/AddConstantValue";
            _apiHandler.PostApi<CustomResponseNullDto>(addConstantValueDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LanguageEditConstantValue(string id, string languageId)
        {
            string url = _url + "PanelConstantValue/LanguageEditConstantValue/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditConstantValueDto> languageEditConstantValueDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditConstantValueDto>>(url);
            if (languageEditConstantValueDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditConstantValueDto.Data);
            }
        }

        [HttpPost]
        public IActionResult LanguageEditConstantValue(LanguageEditConstantValueDto languageEditConstantValueDto)
        {
            string url = _url + "PanelConstantValue/LanguageEditConstantValue";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditConstantValueDto, url);
            return RedirectToAction("Index");
        }
    }
}

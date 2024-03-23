using Dto.ApiPanelDtos.FaqCategoryDtos;
using Dto.ApiPanelDtos.LanguageDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.FaqCategoryManagement)]

    public class FaqCategoryController : CustomBaseController
    {
        public FaqCategoryController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            string url = _url + "PanelFaqCategory/FaqCategoryList";
            CustomResponseDto<List<FaqCategoryListDto>> faqCategoryListDto = _apiHandler.GetApi<CustomResponseDto<List<FaqCategoryListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);
            if (faqCategoryListDto is null)
            {
                return View();
            }
            else
            {
                var model = (faqCategoryListDto.Data, languageListDto.Data);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddFaqCategory()
        {
            string url = $"{_url}PanelFaqCategory/GetPageSelectList";
            var response = _apiHandler.GetApi<CustomResponseDto<List<SelectListOptionDto>>>(url);
            if (response.Data is not null)
                ViewBag.PageSelectList = response.Data;

            return View();
        }

        [HttpPost]
        public IActionResult AddFaqCategory(AddFaqCategoryDto addFaqCategoryDto)
        {
            string url = _url + "PanelFaqCategory/AddFaqCategory";
            _apiHandler.PostApi<CustomResponseNullDto>(addFaqCategoryDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditFaqCategory(Guid id)
        {
            string url = _url + "PanelFaqCategory/EditFaqCategory/" + id;
            CustomResponseDto<EditFaqCategoryDto> editFaqCategoryDto = _apiHandler.GetApi<CustomResponseDto<EditFaqCategoryDto>>(url);
            return View(editFaqCategoryDto.Data);
        }

        [HttpPost]
        public IActionResult EditFaqCategory(EditFaqCategoryDto editFaqCategoryDto)
        {
            string url = _url + "PanelFaqCategory/EditFaqCategory";
            _apiHandler.PostApi<CustomResponseNullDto>(editFaqCategoryDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LanguageEditFaqCategory(Guid id, int languageId)
        {
            string url = _url + "PanelFaqCategory/LanguageEditFaqCategory/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditFaqCategoryDto> languageEditFaqCategoryDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditFaqCategoryDto>>(url);
            return View(languageEditFaqCategoryDto.Data);
        }
        [HttpPost]
        public IActionResult LanguageEditFaqCategory(LanguageEditFaqCategoryDto languageEditFaqCategoryDto)
        {
            string url = _url + "PanelFaqCategory/LanguageEditFaqCategory";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditFaqCategoryDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleFaqCategoryStatus(Guid id)
        {
            string url = _url + "PanelFaqCategory/ToggleFaqCategoryStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }
    }
}

using Dto.ApiPanelDtos.FaqCategoryDtos;
using Dto.ApiPanelDtos.FaqManagementDtos;
using Dto.ApiPanelDtos.LanguageDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.FaqManagement)]

    public class FaqController : CustomBaseController
    {
        public FaqController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            string url = _url + "PanelFaq/FaqList";
            CustomResponseDto<List<FaqListDto>> faqListDto = _apiHandler.GetApi<CustomResponseDto<List<FaqListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);
            if (faqListDto is null)
            {
                return View();
            }
            else
            {
                var model = (faqListDto.Data, languageListDto.Data);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddFaq()
        {
            string url = _url + "PanelFaqCategory/FaqCategoryListForSelect";
            CustomResponseDto<List<FaqCategoryListForSelectDto>> faqCategoryListForSelect = _apiHandler.GetApi<CustomResponseDto<List<FaqCategoryListForSelectDto>>>(url);

            ViewBag.FaqCategories = faqCategoryListForSelect.Data;

            return View();
        }

        [HttpPost]
        public IActionResult AddFaq(AddFaqDto addFaqDto)
        {
            string url = _url + "PanelFaq/AddFaq";
            _apiHandler.PostApi<CustomResponseNullDto>(addFaqDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditFaq(string id)
        {
            string url = _url + "PanelFaq/EditFaq/" + id;
            CustomResponseDto<EditFaqDto> editFaqDto = _apiHandler.GetApi<CustomResponseDto<EditFaqDto>>(url);

            string url2 = _url + "PanelFaqCategory/FaqCategoryListForSelect";
            CustomResponseDto<List<FaqCategoryListForSelectDto>> faqCategoryListForSelect = _apiHandler.GetApi<CustomResponseDto<List<FaqCategoryListForSelectDto>>>(url2);

            ViewBag.FaqCategories = faqCategoryListForSelect.Data;

            if (editFaqDto is null)
            {
                return View();
            }
            else
            {
                return View(editFaqDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditFaq(EditFaqDto editFaqDto)
        {
            string url = _url + "PanelFaq/EditFaq";
            _apiHandler.PostApi<CustomResponseNullDto>(editFaqDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LanguageEditFaq(string id, string languageId)
        {
            string url = _url + "PanelFaq/LanguageEditFaq/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditFaqDto> languageEditFaqDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditFaqDto>>(url);


            if (languageEditFaqDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditFaqDto.Data);
            }
        }

        [HttpPost]
        public IActionResult LanguageEditFaq(LanguageEditFaqDto languageEditFaqDto)
        {
            string url = _url + "PanelFaq/LanguageEditFaq";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditFaqDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleFaqStatus(string id)
        {
            string url = _url + "PanelFaq/ToggleFaqStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }
    }
}

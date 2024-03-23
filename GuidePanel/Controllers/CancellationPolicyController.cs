using Dto.ApiPanelDtos.CancellationPolicyDtos;
using Dto.ApiPanelDtos.LanguageDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.CancellationPolicyManagement)]
    public class CancellationPolicyController : CustomBaseController
    {
        public CancellationPolicyController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            string url = _url + "PanelCancellationPolicy/CancellationPolicyList";
            CustomResponseDto<List<CancellationPolicyListDto>> cancellationPolicyListDto = _apiHandler.GetApi<CustomResponseDto<List<CancellationPolicyListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);
            if (cancellationPolicyListDto is null)
            {
                return View();
            }
            else
            {
                var model = (cancellationPolicyListDto.Data, languageListDto.Data);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddCancellationPolicy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCancellationPolicy(AddCancellationPolicyDto addCancellationPolicyDto)
        {
            string url = _url + "PanelCancellationPolicy/AddCancellationPolicy";
            _apiHandler.PostApi<CustomResponseNullDto>(addCancellationPolicyDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditCancellationPolicy(string id)
        {
            string url = _url + "PanelCancellationPolicy/EditCancellationPolicy/" + id;
            CustomResponseDto<EditCancellationPolicyDto> editCancellationPolicyDto = _apiHandler.GetApi<CustomResponseDto<EditCancellationPolicyDto>>(url);
            if (editCancellationPolicyDto is null)
            {
                return View();
            }
            else
            {
                return View(editCancellationPolicyDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditCancellationPolicy(EditCancellationPolicyDto editCancellationPolicyDto)
        {
            string url = _url + "PanelCancellationPolicy/EditCancellationPolicy";
            _apiHandler.PostApi<CustomResponseNullDto>(editCancellationPolicyDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LanguageEditCancellationPolicy(string id, string languageId)
        {
            string url = _url + "PanelCancellationPolicy/LanguageEditCancellationPolicy/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditCancellationPolicyDto> languageEditCancellationPolicyDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditCancellationPolicyDto>>(url);
            if (languageEditCancellationPolicyDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditCancellationPolicyDto.Data);
            }
        }

        [HttpPost]
        public IActionResult LanguageEditCancellationPolicy(LanguageEditCancellationPolicyDto languageEditCancellationPolicyDto)
        {
            string url = _url + "PanelCancellationPolicy/LanguageEditCancellationPolicy";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditCancellationPolicyDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleCancellationPolicyStatus(string id)
        {
            string url = _url + "PanelCancellationPolicy/ToggleCancellationPolicyStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }
    }
}

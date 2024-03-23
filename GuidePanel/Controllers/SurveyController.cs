
using Dto.ApiPanelDtos.LanguageDtos;
using Dto.ApiPanelDtos.SurveyDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.SurveyManagement)]
    public class SurveyController : CustomBaseController
    {
        public SurveyController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            string url = _url + "PanelSurvey/SurveyList";
            CustomResponseDto<List<SurveyListDto>> surveyListDto = _apiHandler.GetApi<CustomResponseDto<List<SurveyListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);
            if (surveyListDto is null)
            {
                return View();
            }
            else
            {
                var model = (surveyListDto.Data, languageListDto.Data);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddSurvey()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSurvey(AddSurveyDto addSurveyDto)
        {
            string url = _url + "PanelSurvey/AddSurvey";
            _apiHandler.PostApi<CustomResponseNullDto>(addSurveyDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditSurvey(string id)
        {
            string url = _url + "PanelSurvey/EditSurvey/" + id;
            CustomResponseDto<EditSurveyDto> editSurveyDto = _apiHandler.GetApi<CustomResponseDto<EditSurveyDto>>(url);

            string url1 = _url + "PanelSurvey/GetSurveyQuestion/" + id;
            CustomResponseDto<string> surveyQuestionResponse = _apiHandler.GetApi<CustomResponseDto<string>>(url1);

            ViewBag.QuestionLangItem = surveyQuestionResponse.Data;
            if (editSurveyDto is null)
            {
                return View();
            }
            else
            {
                return View(editSurveyDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditSurvey(EditSurveyDto editSurveyDto)
        {
            string url = _url + "PanelSurvey/EditSurvey";
            _apiHandler.PostApi<CustomResponseNullDto>(editSurveyDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LanguageEditSurvey(string id, string languageId)
        {
            string url = _url + "PanelSurvey/LanguageEditSurvey/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditSurveyDto> languageEditSurveyDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditSurveyDto>>(url);

            if (languageEditSurveyDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditSurveyDto.Data);
            }
        }

        [HttpPost]
        public IActionResult LanguageEditSurvey(LanguageEditSurveyDto languageEditSurveyDto)
        {
            string url = _url + "PanelSurvey/LanguageEditSurvey";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditSurveyDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleSurveyStatus(string id)
        {
            string url = _url + "PanelSurvey/ToggleSurveyStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }
    }
}

using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Dto.ApiPanelDtos.FaqCategoryDtos;
using Dto.ApiPanelDtos.SurveyDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelSurveyController : ControllerBase
    {

        private readonly ISurveyService _surveyService;
        private readonly ISurveyLanguageService _surveyLanguageService;

        public PanelSurveyController(ISurveyService surveyService, ISurveyLanguageService surveyLanguageService)
        {
            _surveyService = surveyService;
            _surveyLanguageService = surveyLanguageService;
        }

        [HttpGet]
        public CustomResponseDto<List<SurveyListDto>> SurveyList()
        {
            var surveyListDtos = (from survey in _surveyService.GetAll(x => !x.IsDeleted)
                                  join surveyLanguage in _surveyLanguageService.GetAll(x => !x.IsDeleted)
                                  on survey.Id equals surveyLanguage.SurveyID
                                  where surveyLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                  select new SurveyListDto
                                  {
                                      SurveyID = survey.Id,
                                      Status = survey.Status,
                                      Order = survey.Order,
                                      Question = surveyLanguage.Question
                                  }).ToList();

            return CustomResponseDto<List<SurveyListDto>>.Success(200, surveyListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddSurvey(AddSurveyDto addSurveyDto)
        {
            Survey survey = new Survey()
            {
                Order = addSurveyDto.Order
            };

            _surveyService.Add(survey);

            foreach (var language in LanguageList.AllLanguages)
            {
                SurveyLanguageItem surveyLanguageItem = new SurveyLanguageItem()
                {
                    SurveyID = survey.Id,
                    LanguageID = language.Id,
                    Question = addSurveyDto.Question
                };

                _surveyLanguageService.Add(surveyLanguageItem);
            }

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditSurveyDto> EditSurvey(Guid id)
        {

            Survey survey = _surveyService.GetById(id);

            EditSurveyDto editSurveyDto = new EditSurveyDto()
            {
                SurveyID = survey.Id,
                Order = survey.Order
            };


            return CustomResponseDto<EditSurveyDto>.Success(200, editSurveyDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditSurvey(EditSurveyDto editSurveyDto)
        {

            Survey survey = _surveyService.GetById(editSurveyDto.SurveyID);

            survey.Order = editSurveyDto.Order;
            _surveyService.Update(survey);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditSurveyDto> LanguageEditSurvey(Guid id, int languageId)
        {

            SurveyLanguageItem surveyLanguageItem = _surveyLanguageService.Where(x => x.SurveyID == id && x.LanguageID == languageId).FirstOrDefault();

            LanguageEditSurveyDto languageEditSurveyDto = new LanguageEditSurveyDto()
            {
                SurveyLanguageItemID = surveyLanguageItem.Id,
                LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name,
                Question = surveyLanguageItem.Question
            };

            return CustomResponseDto<LanguageEditSurveyDto>.Success(200, languageEditSurveyDto);
        }

        [HttpPost]
        public CustomResponseNullDto LanguageEditSurvey(LanguageEditSurveyDto languageEditSurveyDto)
        {
            SurveyLanguageItem surveyLanguageItem = _surveyLanguageService.GetById(languageEditSurveyDto.SurveyLanguageItemID);

            surveyLanguageItem.Question = languageEditSurveyDto.Question;

            _surveyLanguageService.Update(surveyLanguageItem);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<string> GetSurveyQuestion(Guid id)
        {
            var question = _surveyLanguageService.GetBySurveyId(id).Question;
            return CustomResponseDto<string>.Success(200, question);
        }
        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleSurveyStatus(Guid id)
        {
            _surveyService.ToggleStatus(id);


            return CustomResponseNullDto.Success(204);
        }
    }
}

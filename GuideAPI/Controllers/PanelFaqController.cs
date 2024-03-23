using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Dto.ApiPanelDtos.FaqCategoryDtos;
using Dto.ApiPanelDtos.FaqManagementDtos;
using Dto.ApiPanelDtos.TagManagementDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelFaqController : ControllerBase
    {

        private readonly IFaqService _faqService;
        private readonly IFaqLanguageService _faqLanguageService;
        private readonly IFaqCategoryLanguageService _faqCategoryLanguageService;

        public PanelFaqController(IFaqService faqService, IFaqLanguageService faqLanguageService, IFaqCategoryLanguageService faqCategoryLanguageService)
        {
            _faqService = faqService;
            _faqLanguageService = faqLanguageService;
            _faqCategoryLanguageService = faqCategoryLanguageService;
        }

        [HttpGet]
        public CustomResponseDto<List<FaqListDto>> FaqList()
        {

            var faqListDtos = (from faq in _faqService.GetAll(x => !x.IsDeleted)
                               join faqCategoryLanguage in _faqCategoryLanguageService.GetAll(x => !x.IsDeleted)
                               on faq.FaqCategoryID equals faqCategoryLanguage.FaqCategoryID
                               where faqCategoryLanguage.LangaugeID == LanguageList.BaseLanguage.Id
                               join faqLanguageItem in _faqLanguageService.GetAll(x => !x.IsDeleted)
                               on faq.Id equals faqLanguageItem.FaqID
                               where faqLanguageItem.LangaugeID == LanguageList.BaseLanguage.Id
                               select new FaqListDto()
                               {
                                   FaqID = faq.Id,
                                   FaqCategory = faqCategoryLanguage.DisplayName,
                                   Status = faq.Status,
                                   Order = faq.Order,
                                   Question = faqLanguageItem.Question,
                                   Answer = faqLanguageItem.Answer

                               }).ToList();



            return CustomResponseDto<List<FaqListDto>>.Success(200, faqListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddFaq(AddFaqDto addFaqDto)
        {
            Faq faq = new Faq()
            {
                Order = addFaqDto.Order,
                FaqCategoryID = addFaqDto.FaqCategoryID
            };

            _faqService.Add(faq);

            foreach (var language in LanguageList.AllLanguages)
            {
                FaqLangaugeItem faqLangaugeItem = new FaqLangaugeItem()
                {
                    FaqID = faq.Id,
                    LangaugeID = language.Id,
                    Answer = addFaqDto.Answer,
                    Question = addFaqDto.Question
                };

                _faqLanguageService.Add(faqLangaugeItem);
            }

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditFaqDto> EditFaq(Guid id)
        {
            var faq = _faqService.GetById(id);
            var faqQuestion = _faqLanguageService.Where(x => x.FaqID == id && x.LangaugeID == 1).FirstOrDefault().Question;
            var editFaqDto = new EditFaqDto()
            {
                FaqID = faq.Id,
                FaqCategoryID = faq.FaqCategoryID,
                Order = faq.Order,
                FaqQuestion = faqQuestion,
            };

            return CustomResponseDto<EditFaqDto>.Success(200, editFaqDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditFaq(EditFaqDto editFaqDto)
        {
            var faq = _faqService.GetById(editFaqDto.FaqID);
            faq.FaqCategoryID = editFaqDto.FaqCategoryID;
            faq.Order = editFaqDto.Order;

            _faqService.Update(faq);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditFaqDto> LanguageEditFaq(Guid id, int languageId)
        {
            var faqLanguageItem = _faqLanguageService.Where(x => x.FaqID == id && x.LangaugeID == languageId).FirstOrDefault();

            var language = LanguageList.AllLanguages.Where(x => x.Id == languageId).FirstOrDefault();

            LanguageEditFaqDto languageEditFaqDto = new LanguageEditFaqDto()
            {
                LanguageName = language.Name,
                FaqLanguageItemID = faqLanguageItem.Id,
                Answer = faqLanguageItem.Answer,
                Question = faqLanguageItem.Question,
            };

            return CustomResponseDto<LanguageEditFaqDto>.Success(200, languageEditFaqDto);
        }

        [HttpPost]
        public CustomResponseNullDto LanguageEditFaq(LanguageEditFaqDto languageEditFaqDto)
        {

            FaqLangaugeItem faqLangaugeItem = _faqLanguageService.GetById(languageEditFaqDto.FaqLanguageItemID);

            faqLangaugeItem.Answer = languageEditFaqDto.Answer;
            faqLangaugeItem.Question = languageEditFaqDto.Question;

            _faqLanguageService.Update(faqLangaugeItem);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleFaqStatus(Guid id)
        {

            _faqService.ToggleStatus(id);


            return CustomResponseNullDto.Success(204);
        }
    }
}

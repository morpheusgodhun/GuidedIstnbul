using Core.Entities;
using Core.IService;
using Core.IUnitOfWork;
using Core.StaticClass;
using Data.Repository;
using Dto.ApiPanelDtos.CustomPageManagementDto;
using Dto.ApiPanelDtos.FaqCategoryDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Services;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelFaqCategoryController : ControllerBase
    {
        private readonly IFaqCategoryService _faqCategoryService;
        private readonly IFaqCategoryLanguageService _faqCategoryLanguageService;
        private readonly IPageService _pageService;

        public PanelFaqCategoryController(IFaqCategoryService faqCategoryService, IFaqCategoryLanguageService faqCategoryLanguageService, IPageService pageService)
        {
            _faqCategoryService = faqCategoryService;
            _faqCategoryLanguageService = faqCategoryLanguageService;
            _pageService = pageService;
        }

        [HttpGet]
        public CustomResponseDto<List<FaqCategoryListDto>> FaqCategoryList()
        {
            var faqCategoryListDtos = (from faqCategory in _faqCategoryService.GetAll(x => !x.IsDeleted)
                                       join faqCategoryLanguageItem in _faqCategoryLanguageService.GetAll(x => !x.IsDeleted)
                                       on faqCategory.Id equals faqCategoryLanguageItem.FaqCategoryID
                                       where faqCategoryLanguageItem.LangaugeID == LanguageList.BaseLanguage.Id
                                       select new FaqCategoryListDto
                                       {
                                           CategoryID = faqCategory.Id,
                                           CategoryName = faqCategoryLanguageItem.DisplayName,
                                           Order = faqCategory.Order,
                                           Status = faqCategory.Status
                                       }).ToList();

            return CustomResponseDto<List<FaqCategoryListDto>>.Success(200, faqCategoryListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddFaqCategory(AddFaqCategoryDto addFaqCategoryDto)
        {
            FaqCategory faqCategory = new FaqCategory()
            {
                Order = addFaqCategoryDto.Order,
                PageId = new Guid(addFaqCategoryDto.PageId)
            };
            _faqCategoryService.Add(faqCategory);

            foreach (var language in LanguageList.AllLanguages)
            {
                FaqCategoryLanguageItem faqCategoryLanguageItem = new FaqCategoryLanguageItem()
                {
                    FaqCategoryID = faqCategory.Id,
                    LangaugeID = language.Id,
                    DisplayName = addFaqCategoryDto.CategoryName,
                };

                _faqCategoryLanguageService.Add(faqCategoryLanguageItem);
            }

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditFaqCategoryDto> EditFaqCategory(Guid id)
        {
            var faqCategory = _faqCategoryService.GetById(id);
            string faqCategoryName = _faqCategoryLanguageService.Where(x => x.FaqCategoryID == id && x.LangaugeID == 1).FirstOrDefault().DisplayName;
            var editFaqCategoryDto = new EditFaqCategoryDto()
            {
                FaqCategoryID = faqCategory.Id,
                Order = faqCategory.Order,
                FaqCategoryName = faqCategoryName
            };

            return CustomResponseDto<EditFaqCategoryDto>.Success(200, editFaqCategoryDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditFaqCategory(EditFaqCategoryDto editFaqCategoryDto)
        {
            var faqCategory = _faqCategoryService.GetById(editFaqCategoryDto.FaqCategoryID);
            faqCategory.Order = editFaqCategoryDto.Order;
            _faqCategoryService.Update(faqCategory);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditFaqCategoryDto> LanguageEditFaqCategory(Guid id, int languageId)
        {

            var faqCategoryLanguageItem = _faqCategoryLanguageService.Where(x => x.FaqCategoryID == id && x.LangaugeID == languageId).FirstOrDefault();

            var language = LanguageList.AllLanguages.Where(x => x.Id == languageId).FirstOrDefault();

            LanguageEditFaqCategoryDto languageEditFaqCategoryDto = new LanguageEditFaqCategoryDto()
            {
                FaqCategoryLanguageItemID = faqCategoryLanguageItem.Id,
                CategoryName = faqCategoryLanguageItem.DisplayName,
                LanguageName = language.Name
            };

            return CustomResponseDto<LanguageEditFaqCategoryDto>.Success(200, languageEditFaqCategoryDto);
        }

        [HttpPost]
        public CustomResponseNullDto LanguageEditFaqCategory(LanguageEditFaqCategoryDto languageEditFaqCategoryDto)
        {
            FaqCategoryLanguageItem faqCategoryLanguageItem = _faqCategoryLanguageService.GetById(languageEditFaqCategoryDto.FaqCategoryLanguageItemID);

            faqCategoryLanguageItem.DisplayName = languageEditFaqCategoryDto.CategoryName;

            _faqCategoryLanguageService.Update(faqCategoryLanguageItem);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleFaqCategoryStatus(Guid id)
        {
            _faqCategoryService.ToggleStatus(id);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet]
        public CustomResponseDto<List<FaqCategoryListForSelectDto>> FaqCategoryListForSelect()
        {
            List<FaqCategoryListForSelectDto> faqCategoryListForSelects =
                                       (from faqCategory in _faqCategoryService.GetAll(x => !x.IsDeleted)
                                        join faqCategoryLanguageItem in _faqCategoryLanguageService.GetAll(x => !x.IsDeleted)
                                        on faqCategory.Id equals faqCategoryLanguageItem.FaqCategoryID
                                        where faqCategoryLanguageItem.LangaugeID == LanguageList.BaseLanguage.Id
                                        select new FaqCategoryListForSelectDto
                                        {
                                            CategoryID = faqCategory.Id,
                                            CategoryName = faqCategoryLanguageItem.DisplayName,
                                            Order = faqCategory.Order
                                        }).ToList();


            return CustomResponseDto<List<FaqCategoryListForSelectDto>>.Success(200, faqCategoryListForSelects);
        }

        [HttpGet] //sadece custom pageler --> pageType = 2
        public CustomResponseDto<List<SelectListOptionDto>> GetPageSelectList()
        {
            List<SelectListOptionDto> customPageListDtos = (from page in _pageService.GetAll(x => !x.IsDeleted)
                                                            where page.Type == 2 || page.Type == 1
                                                            select new SelectListOptionDto()
                                                            {
                                                                OptionID = page.Id,
                                                                Option = page.PageName,
                                                            }).ToList();
            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, customPageListDtos);
        }
    }
}

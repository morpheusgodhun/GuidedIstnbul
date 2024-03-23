using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Dto.ApiPanelDtos.FaqCategoryDtos;
using Dto.ApiPanelDtos.SurveyDtos;
using Dto.ApiPanelDtos.TourCategoryDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelTourCategoryController : ControllerBase
    {
        private readonly ITourCategoryService _tourCategoryService;
        private readonly ITourCategoryLanguageService _tourCategoryLanguageService;
        public PanelTourCategoryController(ITourCategoryService tourCategoryService, ITourCategoryLanguageService tourCategoryLanguageService)
        {
            _tourCategoryService = tourCategoryService;
            _tourCategoryLanguageService = tourCategoryLanguageService;
        }

        [HttpGet]
        public CustomResponseDto<List<TourCategoryListDto>> TourCategoryList()
        {
            List<TourCategoryListDto> tourCategoryListDtos =
                (from tourCategory in _tourCategoryService.GetAll(x => !x.IsDeleted)
                 join tourCategoryLanguage in _tourCategoryLanguageService.GetAll(x => !x.IsDeleted)
                 on tourCategory.Id equals tourCategoryLanguage.TourCategoryID
                 where tourCategoryLanguage.LanguageID == LanguageList.BaseLanguage.Id
                 select new TourCategoryListDto
                 {
                     TourCategoryID = tourCategory.Id,
                     CategoryName = tourCategoryLanguage.CategoryName,
                     IconPath = tourCategory.IconPath,
                     Status = tourCategory.Status
                 }).ToList();

            return CustomResponseDto<List<TourCategoryListDto>>.Success(200, tourCategoryListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddTourCategory(AddTourCategoryDto addTourCategoryDto)
        {
            TourCategory tourCategory = new()
            {
                IconPath = addTourCategoryDto.IconPath,
                BannerImagePath = addTourCategoryDto.BannerImagePath
            };

            _tourCategoryService.Add(tourCategory);

            foreach (var language in LanguageList.AllLanguages)
            {
                TourCategoryLanguageItem tourCategoryLanguageItem = new TourCategoryLanguageItem()
                {
                    TourCategoryID = tourCategory.Id,
                    LanguageID = language.Id,
                    CategoryName = addTourCategoryDto.CategoryName,
                    Slug = addTourCategoryDto.Slug,
                    Content = string.IsNullOrEmpty(addTourCategoryDto.Content) ? " " : addTourCategoryDto.Content
                };

                _tourCategoryLanguageService.Add(tourCategoryLanguageItem);
            }

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditTourCategoryDto> EditTourCategory(Guid id)
        {

            TourCategory tourCategory = _tourCategoryService.GetById(id);

            EditTourCategoryDto editTourCategoryDto = new EditTourCategoryDto()
            {
                TourCategoryID = id,
                BannerImagePath = tourCategory.BannerImagePath,
                IconPath = tourCategory.IconPath,
            };


            return CustomResponseDto<EditTourCategoryDto>.Success(200, editTourCategoryDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditTourCategory(EditTourCategoryDto editTourCategory)
        {

            TourCategory tourCategory = _tourCategoryService.GetById(editTourCategory.TourCategoryID);

            tourCategory.BannerImagePath = editTourCategory.BannerImagePath;
            tourCategory.IconPath = editTourCategory.IconPath;
            _tourCategoryService.Update(tourCategory);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditTourCategoryDto> LanguageEditTourCategory(Guid id, int languageId)
        {

            TourCategoryLanguageItem tourCategoryLanguageItem = _tourCategoryLanguageService.Where(x => x.TourCategoryID == id && x.LanguageID == languageId).FirstOrDefault();

            LanguageEditTourCategoryDto languageEditTourCategoryDto = new LanguageEditTourCategoryDto()
            {
                TourCategoryLanguageID = tourCategoryLanguageItem.Id,
                CategoryName = tourCategoryLanguageItem.CategoryName,
                Content = tourCategoryLanguageItem.Content,
                Slug = tourCategoryLanguageItem.Slug,
                LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name
            };

            return CustomResponseDto<LanguageEditTourCategoryDto>.Success(200, languageEditTourCategoryDto);
        }

        [HttpPost]
        public CustomResponseNullDto LanguageEditTourCategory(LanguageEditTourCategoryDto languageEditTourCategoryDto)
        {
            TourCategoryLanguageItem tourCategoryLanguageItem = _tourCategoryLanguageService.GetById(languageEditTourCategoryDto.TourCategoryLanguageID);

            tourCategoryLanguageItem.CategoryName = languageEditTourCategoryDto.CategoryName;
            tourCategoryLanguageItem.Content = string.IsNullOrEmpty(languageEditTourCategoryDto.Content) ? " " : languageEditTourCategoryDto.Content;
            tourCategoryLanguageItem.Slug = languageEditTourCategoryDto.Slug;

            _tourCategoryLanguageService.Update(tourCategoryLanguageItem);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleTourCategoryStatus(Guid id)
        {

            _tourCategoryService.ToggleStatus(id);


            return CustomResponseNullDto.Success(204);
        }
        [HttpGet("{id}")]
        public CustomResponseDto<string> GetTourCategoryName(Guid id)
        {
            string tourCategoryName = _tourCategoryService.GetTourCategoryName(id);
            return CustomResponseDto<string>.Success(200, tourCategoryName);
        }


        //TourCategory Select List
        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> TourCategorySelectList()
        {
            List<SelectListOptionDto> tourCategorySelectList =
                (from tourCategory in _tourCategoryService.GetAll(x => !x.IsDeleted)
                 join languageItem in _tourCategoryLanguageService.GetAll(x => !x.IsDeleted)
                 on tourCategory.Id equals languageItem.TourCategoryID
                 where languageItem.LanguageID == LanguageList.BaseLanguage.Id
                 select new SelectListOptionDto()
                 {
                     OptionID = tourCategory.Id,
                     Option = languageItem.CategoryName
                 }).ToList();


            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, tourCategorySelectList);
        }
    }
}

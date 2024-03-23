using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Dto.ApiPanelDtos.FaqCategoryDtos;
using Dto.ApiPanelDtos.SystemOptionDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelSystemOptionController : ControllerBase
    {
        private readonly ISystemOptionService _systemOptionService;
        private readonly ISystemOptionLanguageService _systemOptionLanguageService;

        public PanelSystemOptionController(ISystemOptionService systemOptionService, ISystemOptionLanguageService systemOptionLanguageService)
        {
            _systemOptionService = systemOptionService;
            _systemOptionLanguageService = systemOptionLanguageService;
        }

        [HttpGet]
        public CustomResponseDto<List<SystemOptionListDto>> SystemOptionList()
        {
            List<SystemOptionListDto> systemOptionListDtos =
                (from systemOption in _systemOptionService.GetAll(x => !x.IsDeleted)
                 join sytemOptionLanguageItem in _systemOptionLanguageService.GetAll(x => !x.IsDeleted)
                 on systemOption.Id equals sytemOptionLanguageItem.SystemOptionId
                 where sytemOptionLanguageItem.LanguageID == LanguageList.BaseLanguage.Id
                 select new SystemOptionListDto()
                 {
                     SystemOptionID = systemOption.Id,
                     Order = systemOption.Order,
                     Status = systemOption.Status,
                     SystemOptionCategoryName = SystemOptionCategoryList.CategoryList.FirstOrDefault(x => x.ID == systemOption.SystemOptionCategoryID).Value,
                     SystemOptionName = sytemOptionLanguageItem.Name


                 }).ToList();


            return CustomResponseDto<List<SystemOptionListDto>>.Success(200, systemOptionListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddSystemOption(AddSystemOptionDto addSystemOptionDto)
        {
            SystemOption systemOption = new SystemOption()
            {
                SystemOptionCategoryID = addSystemOptionDto.SystemOptionCategoryID,
                Order = addSystemOptionDto.Order,
            };
            _systemOptionService.Add(systemOption);

            foreach (var language in LanguageList.AllLanguages)
            {
                SystemOptionLanguageItem systemOptionLanguageItem = new SystemOptionLanguageItem()
                {
                    SystemOptionId = systemOption.Id,
                    Name = addSystemOptionDto.SystemOptionName,
                    LanguageID = language.Id,
                };

                _systemOptionLanguageService.Add(systemOptionLanguageItem);
            }

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditSystemOptionDto> EditSystemOption(Guid id)
        {

            SystemOption systemOption = _systemOptionService.GetById(id);

            EditSystemOptionDto editSystemOptionDto = new()
            {
                SystemOptionID = systemOption.Id,
                Order = systemOption.Order,
                SystemOptionCategoryID = systemOption.SystemOptionCategoryID
            };

            return CustomResponseDto<EditSystemOptionDto>.Success(204, editSystemOptionDto);
        }
        [HttpGet("{id}")]
        public CustomResponseDto<string> GetSystemOptionName(Guid id)
        {
            var systemOptionName = _systemOptionService.GetSystemOptionById(id, 1).SystemOptionLanguageItems.FirstOrDefault(x => x.LanguageID == 1).Name;
            return CustomResponseDto<string>.Success(200, systemOptionName);
        }

        [HttpPost]
        public CustomResponseNullDto EditSystemOption(EditSystemOptionDto editSystemOptionDto)
        {

            SystemOption systemOption = _systemOptionService.GetById(editSystemOptionDto.SystemOptionID);

            systemOption.SystemOptionCategoryID = editSystemOptionDto.SystemOptionCategoryID;
            systemOption.Order = editSystemOptionDto.Order;
            _systemOptionService.Update(systemOption);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditSystemOptionDto> LanguageEditSystemOption(Guid id, int languageId)
        {

            SystemOptionLanguageItem systemOptionLanguageItem = _systemOptionLanguageService.
                Where(x => x.SystemOptionId == id && x.LanguageID == languageId).FirstOrDefault();

            if (systemOptionLanguageItem is null)
            {
                return CustomResponseDto<LanguageEditSystemOptionDto>.Fail(404, "Not found");
            }
            else
            {
                LanguageEditSystemOptionDto languageEditSystemOptionDto = new LanguageEditSystemOptionDto()
                {
                    SystemOptionLanguageItemID = systemOptionLanguageItem.Id,
                    SystemOptionName = systemOptionLanguageItem.Name,
                    LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name
                };

                return CustomResponseDto<LanguageEditSystemOptionDto>.Success(204, languageEditSystemOptionDto);
            }

        }

        [HttpPost]
        public CustomResponseNullDto LanguageEditSystemOption(LanguageEditSystemOptionDto languageEditSystemOption)
        {
            SystemOptionLanguageItem systemOptionLanguageItem = _systemOptionLanguageService.GetById(languageEditSystemOption.SystemOptionLanguageItemID);

            systemOptionLanguageItem.Name = languageEditSystemOption.SystemOptionName;
            _systemOptionLanguageService.Update(systemOptionLanguageItem);


            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleSystemOptionStatus(Guid id)
        {
            _systemOptionService.ToggleStatus(id);

            return CustomResponseNullDto.Success(204);
        }

        //System Option Category - Select List
        [HttpGet]
        public CustomResponseDto<List<SelectListOption>> SystemOptionCategorySelectList()
        {
            List<SelectListOption> systemOptionCategorySelectListDtos = SystemOptionCategoryList.CategoryList;

            return CustomResponseDto<List<SelectListOption>>.Success(200, systemOptionCategorySelectListDtos);
        }


        //Segment Select List
        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> SegmentSelectList()
        {
            List<SelectListOptionDto> SelectList =
                (from systemOption in _systemOptionService.GetAll(x => !x.IsDeleted)
                 where systemOption.SystemOptionCategoryID == 1
                 join languageItem in _systemOptionLanguageService.GetAll(x => !x.IsDeleted)
                 on systemOption.Id equals languageItem.SystemOptionId
                 where languageItem.LanguageID == LanguageList.BaseLanguage.Id
                 select new SelectListOptionDto()
                 {
                     OptionID = systemOption.Id,
                     Option = languageItem.Name
                 }).ToList();


            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, SelectList);
        }

        //Tour Type Select List
        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> TourTypeSelectList()
        {
            List<SelectListOptionDto> SelectList =
                (from systemOption in _systemOptionService.GetAll(x => !x.IsDeleted)
                 where systemOption.SystemOptionCategoryID == 2
                 join languageItem in _systemOptionLanguageService.GetAll(x => !x.IsDeleted)
                 on systemOption.Id equals languageItem.SystemOptionId
                 where languageItem.LanguageID == LanguageList.BaseLanguage.Id
                 select new SelectListOptionDto()
                 {
                     OptionID = systemOption.Id,
                     Option = languageItem.Name
                 }).ToList();


            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, SelectList);
        }

        //Inclusion-Exclusion Select List
        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> InclusionExclusionSelectList()
        {
            List<SelectListOptionDto> SelectList =
                (from systemOption in _systemOptionService.GetAll(x => !x.IsDeleted)
                 where systemOption.SystemOptionCategoryID == 3
                 join languageItem in _systemOptionLanguageService.GetAll(x => !x.IsDeleted)
                 on systemOption.Id equals languageItem.SystemOptionId
                 where languageItem.LanguageID == LanguageList.BaseLanguage.Id
                 select new SelectListOptionDto()
                 {
                     OptionID = systemOption.Id,
                     Option = languageItem.Name
                 }).ToList();


            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, SelectList);
        }

        //SightToSeeSelectList
        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> SightToSeeSelectList()
        {
            List<SelectListOptionDto> SelectList =
                (from systemOption in _systemOptionService.GetAll(x => !x.IsDeleted)
                 where systemOption.SystemOptionCategoryID == 4
                 join languageItem in _systemOptionLanguageService.GetAll(x => !x.IsDeleted)
                 on systemOption.Id equals languageItem.SystemOptionId
                 where languageItem.LanguageID == LanguageList.BaseLanguage.Id
                 select new SelectListOptionDto()
                 {
                     OptionID = systemOption.Id,
                     Option = languageItem.Name
                 }).ToList();


            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, SelectList);
        }

        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> HowDidYouFindUsSelectList()
        {
            List<SelectListOptionDto> SelectList =
                (from systemOption in _systemOptionService.GetAll(x => !x.IsDeleted)
                 where systemOption.SystemOptionCategoryID == 5
                 join languageItem in _systemOptionLanguageService.GetAll(x => !x.IsDeleted)
                 on systemOption.Id equals languageItem.SystemOptionId
                 where languageItem.LanguageID == LanguageList.BaseLanguage.Id
                 select new SelectListOptionDto()
                 {
                     OptionID = systemOption.Id,
                     Option = languageItem.Name
                 }).ToList();


            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, SelectList);
        }



    }
}

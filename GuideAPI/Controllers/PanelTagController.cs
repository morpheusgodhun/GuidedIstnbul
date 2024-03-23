using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Core.StaticValues;
using Data.Repository;
using Dto.ApiPanelDtos.FaqManagementDtos;
using Dto.ApiPanelDtos.TagManagementDtos;
using Dto.ApiPanelDtos.TourCategoryDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelTagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly ITagLanguageService _tagLanguageService;

        public PanelTagController(ITagService tagService, ITagLanguageService tagLanguageService)
        {
            _tagService = tagService;
            _tagLanguageService = tagLanguageService;
        }


        [HttpGet]
        public CustomResponseDto<List<TagListDto>> TagList()
        {
            List<TagListDto> tagListDtos = _tagService.GetTagListDtos();
            return CustomResponseDto<List<TagListDto>>.Success(200, tagListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddTag(AddTagDto addTagDto)
        {
            //AddTag

            //_tagService.AddTag(addTagDto);
            Tag tag = new Tag()
            {
                IconPath = addTagDto.IconPath,
                TagCategories = JsonSerializer.Serialize(addTagDto.TagCategoryIDs)
            };
            _tagService.Add(tag);

            foreach (var language in LanguageList.AllLanguages)
            {
                TagLanguageItem tagLanguageItem = new TagLanguageItem()
                {
                    TagID = tag.Id,
                    DisplayName = addTagDto.DisplayName,
                    LangaugeID = language.Id,
                    Slug = addTagDto.Slug
                };

                _tagLanguageService.Add(tagLanguageItem);
            }






            return CustomResponseNullDto.Success(200);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditTagDto> EditTag(Guid id)
        {
            EditTagDto editTagDto = _tagService.GetEditTagDto(id);
            return CustomResponseDto<EditTagDto>.Success(200, editTagDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditTag(EditTagDto editTagDto)
        {
            _tagService.EditTag(editTagDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditTagDto> LanguageEditTag(Guid id, int languageId)
        {
            LanguageEditTagDto languageEditTagDto = _tagService.GetLanguageEditTagDto(id, languageId);
            return CustomResponseDto<LanguageEditTagDto>.Success(200, languageEditTagDto);
        }

        [HttpPost]
        public CustomResponseNullDto LanguageEditTag(LanguageEditTagDto languageEditTagDto)
        {
            //_tagService.LanguageEditTag(languageEditTagDto);

            var tagLanguageItem = _tagLanguageService.Where(x => x.Id == languageEditTagDto.TagLanguageItemID).FirstOrDefault();
            //TagLanguageItem tagLanguageItemService = _tagLanguageItems.Find(languageEditTagDto.TagLanguageItemID);
            tagLanguageItem.DisplayName = languageEditTagDto.DisplayName;
            tagLanguageItem.Slug = languageEditTagDto.Slug;
            _tagLanguageService.Update(tagLanguageItem);

            //tagLanguageItemService.DisplayName = languageEditTagDto.DisplayName;
            //tagLanguageItemService.Update(tagLanguageItem);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleTagStatus(Guid id)
        {
            _tagService.ToggleStatus(id);
            return CustomResponseNullDto.Success(204);
        }
        [HttpGet("{id}")]
        public CustomResponseDto<string> GetTagDisplayName(Guid id)
        {
            var tagDisplayName = _tagLanguageService.GetTagDisplayName(id, 1);
            return CustomResponseDto<string>.Success(200, tagDisplayName);
        }

        //Tag Select List
        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> TagSelectList()
        {

            List<SelectListOptionDto> tagSelectList = _tagService.TagSelectList();
            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, tagSelectList);
        }

        //Tag Select List
        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> TagSelectListForBlog()
        {
            List<SelectListOptionDto> tagSelectList = _tagService.TagSelectListForBlog();
            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, tagSelectList);
        }

        //Tag Select List
        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> TagSelectListForProduct()
        {
            List<SelectListOptionDto> tagSelectList = _tagService.TagSelectListForProduct();
            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, tagSelectList);
        }
    }
}

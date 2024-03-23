using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Dto.ApiPanelDtos.BlogCategoryDtos;
using Dto.ApiPanelDtos.BlogDtos;
using Dto.ApiPanelDtos.FaqManagementDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Services;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelBlogCategoryController : ControllerBase
    {
        private readonly IBlogCategoryService _blogCategoryService;
        private readonly IBlogCategoryLanguageService _blogCategoryLanguageService;
        private readonly IRouteService _routeService;

        public PanelBlogCategoryController(IBlogCategoryService blogCategoryService, IBlogCategoryLanguageService blogCategoryLanguageService, IRouteService routeService)
        {
            _blogCategoryService = blogCategoryService;
            _blogCategoryLanguageService = blogCategoryLanguageService;
            _routeService = routeService;
        }

        [HttpGet]
        public CustomResponseDto<List<BlogCategoryListDto>> BlogCategoryList()
        {
            List<BlogCategoryListDto> blogCategoryListDtos = _blogCategoryService.GetBlogCategoryListDtos();
            return CustomResponseDto<List<BlogCategoryListDto>>.Success(200, blogCategoryListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddBlogCategory(AddBlogCategoryDto addBlogCategoryDto)
        {
            //_blogCategoryService.AddBlogCategory(addBlogCategoryDto);
            //return CustomResponseNullDto.Success(204);


            BlogCategory blogCategory = new BlogCategory()
            {
                Order = addBlogCategoryDto.Order
            };

            _blogCategoryService.Add(blogCategory);

            foreach (var language in LanguageList.AllLanguages)
            {
                BlogCategoryLanguageItem blogCategoryLanguageItem = new BlogCategoryLanguageItem()
                {
                    BlogCategoryID = blogCategory.Id,
                    LanguageID = language.Id,
                    CategoryName = addBlogCategoryDto.BlogCategoryName,
                    Slug = addBlogCategoryDto.Slug,
                    SitemapInclude = addBlogCategoryDto.SitemapInclude,
                };

                _blogCategoryLanguageService.Add(blogCategoryLanguageItem);


            }

            return CustomResponseNullDto.Success(204);

        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditBlogCategoryDto> EditBlogCategory(Guid id)
        {
            EditBlogCategoryDto editBlogCategoryDto = _blogCategoryService.GetEditBlogCategoryDto(id);
            return CustomResponseDto<EditBlogCategoryDto>.Success(200, editBlogCategoryDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditBlogCategory(EditBlogCategoryDto editBlogCategoryDto)
        {
            _blogCategoryService.EditBlogCategory(editBlogCategoryDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditBlogCategoryDto> LanguageEditBlogCategory(Guid id, int languageId)
        {
            //LanguageEditBlogCategoryDto languageEditBlogCategoryDto = _blogCategoryService.GetLanguageEditBlogCategoryDto(id, languageId);


            BlogCategoryLanguageItem blogCategoryLanguageItem = _blogCategoryLanguageService.Where(x => x.LanguageID == languageId && x.BlogCategoryID == id).FirstOrDefault();

            Core.Entities.Route? route = _routeService.Where(x => x.EntityId == id).FirstOrDefault();

            LanguageEditBlogCategoryDto languageEditBlogCategoryDto = new LanguageEditBlogCategoryDto()
            {
                BlogCategoryLanguageItemID = blogCategoryLanguageItem.Id,
                BlogCategoryName = blogCategoryLanguageItem.CategoryName,
                LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name,
                Slug = blogCategoryLanguageItem.Slug,
                SitemapInclude = route is not null ? route.SitemapInclude : false

            };

            return CustomResponseDto<LanguageEditBlogCategoryDto>.Success(200, languageEditBlogCategoryDto);
        }

        [HttpPost]
        public CustomResponseNullDto LanguageEditBlogCategory(LanguageEditBlogCategoryDto languageEditBlogCategoryDto)
        {
            //_blogCategoryService.LanguageEditBlogCategory(languageEditBlogCategoryDto);

            BlogCategoryLanguageItem blogCategoryLanguage = _blogCategoryLanguageService.GetById(languageEditBlogCategoryDto.BlogCategoryLanguageItemID);
            blogCategoryLanguage.CategoryName = languageEditBlogCategoryDto.BlogCategoryName;
            blogCategoryLanguage.Slug = languageEditBlogCategoryDto.Slug;
            blogCategoryLanguage.SitemapInclude = languageEditBlogCategoryDto.SitemapInclude;
            //blogCategoryLanguage.LanguageID = LanguageList.AllLanguages.FirstOrDefault(x => x.Name == languageEditBlogCategoryDto.LanguageName).Id;
            _blogCategoryLanguageService.Update(blogCategoryLanguage);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleBlogCategoryStatus(Guid id)
        {
            _blogCategoryService.ToggleStatus(id);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> BlogCategorySelectList()
        {
            List<SelectListOptionDto> blogCategory = _blogCategoryService.BlogCategorySelectList();
            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, blogCategory);
        }
    }

}

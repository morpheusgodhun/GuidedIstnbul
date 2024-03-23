using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Dto.ApiPanelDtos.BlogDtos;
using Dto.ApiPanelDtos.CancellationPolicyDtos;
using Dto.ApiPanelDtos.CustomPageManagementDto;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelCustomPageController : ControllerBase
    {

        private readonly IPageService _pageService;
        private readonly IPageLanguageService _languageService;
        private readonly IRouteService _routeService;

        public PanelCustomPageController(IPageService pageService, IPageLanguageService languageService, IRouteService routeService)
        {
            _pageService = pageService;
            _languageService = languageService;
            _routeService = routeService;
        }

        [HttpGet]
        public CustomResponseDto<List<CustomPageListDto>> CustomPageList()
        {
            List<CustomPageListDto> customPageListDtos = (from page in _pageService.GetAll(x => !x.IsDeleted)
                                                          where page.Type == 2
                                                          select new CustomPageListDto()
                                                          {
                                                              PageID = page.Id,
                                                              PageName = page.PageName,
                                                              Status = page.Status,
                                                              IsAllActive = page.IsAllActive,
                                                          }).ToList();


            return CustomResponseDto<List<CustomPageListDto>>.Success(200, customPageListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddCustomPage(AddCustomPageDto addCustomPageDto)
        {
            Page page = new Page()
            {
                PageName = addCustomPageDto.PageName,
                Type = 2,
                ImagePath = addCustomPageDto.ImagePath
            };

            _pageService.Add(page);

            foreach (var language in LanguageList.AllLanguages)
            {
                PageLanguageItem pageLanguageItem = new PageLanguageItem()
                {
                    PageID = page.Id,
                    LanguageId = language.Id,
                    DisplayName = addCustomPageDto.DisplayName,
                    Slug = addCustomPageDto.Slug,
                    Content = addCustomPageDto.Content,
                    Title = addCustomPageDto.Title,
                    Subtitle = addCustomPageDto.Subtitle,
                    SitemapInclude = addCustomPageDto.SitemapInclude,

                };

                _languageService.Add(pageLanguageItem);
            }

            return CustomResponseNullDto.Success(204);
        }


        [HttpGet("{id}")]
        public CustomResponseDto<EditCustomPageDto> EditCustomPage(Guid id)
        {

            Page page = _pageService.GetById(id);

            EditCustomPageDto editCustomPageDto = new EditCustomPageDto()
            {
                PageID = page.Id,
                PageName = page.PageName,
                ImagePath = page.ImagePath
            };

            return CustomResponseDto<EditCustomPageDto>.Success(200, editCustomPageDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditCustomPage(EditCustomPageDto editCustomPageDto)
        {
            Page page = _pageService.GetById(editCustomPageDto.PageID);

            page.ImagePath = editCustomPageDto.ImagePath;
            _pageService.Update(page);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditCustomPageDto> LanguageEditCustomPage(Guid id, int languageId)
        {
            PageLanguageItem pageLanguageItem = _languageService.Where(x => x.PageID == id && x.LanguageId == languageId).FirstOrDefault();

            Page page = _pageService.GetById(pageLanguageItem.PageID);

            Core.Entities.Route? route = _routeService.Where(x => x.EntityId == id).FirstOrDefault();

            LanguageEditCustomPageDto languageEditCustomPageDto = new LanguageEditCustomPageDto()
            {
                PageName = page.PageName,
                PageLanguageItemID = pageLanguageItem.Id,
                Content = pageLanguageItem.Content,
                DisplayName = pageLanguageItem.DisplayName,
                LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name,
                Slug = pageLanguageItem.Slug,
                Title = pageLanguageItem.Title,
                Subtitle = pageLanguageItem.Subtitle,
                SitemapInclude = route is not null ? route.SitemapInclude : false
            };


            return CustomResponseDto<LanguageEditCustomPageDto>.Success(200, languageEditCustomPageDto);
        }

        [HttpPost]
        public CustomResponseNullDto LanguageEditCustomPage(LanguageEditCustomPageDto languageEditCustomPageDto)
        {
            PageLanguageItem pageLanguageItem = _languageService.GetById(languageEditCustomPageDto.PageLanguageItemID);

            pageLanguageItem.Slug = languageEditCustomPageDto.Slug;
            pageLanguageItem.Content = languageEditCustomPageDto.Content;
            pageLanguageItem.DisplayName = languageEditCustomPageDto.DisplayName;
            pageLanguageItem.Title = languageEditCustomPageDto.Title;
            pageLanguageItem.Subtitle = languageEditCustomPageDto.Subtitle;
            pageLanguageItem.SitemapInclude = languageEditCustomPageDto.SitemapInclude;

            _languageService.Update(pageLanguageItem);


            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleCustomPageStatus(Guid id)
        {
            _pageService.ToggleStatus(id);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> PageSelectListForComment()
        {
            List<SelectListOptionDto> pageList = new List<SelectListOptionDto>()
            {

                //new SelectListOptionDto()
                //{
                //    OptionID = "5",
                //    Option = "Home Page"
                //},
                //new SelectListOptionDto()
                //{
                //    OptionID = "6",
                //    Option = "Contact Page"
                //},
                //new SelectListOptionDto()
                //{
                //    OptionID = "7",
                //    Option = "About Me"
                //},
                //new SelectListOptionDto()
                //{
                //    OptionID = "8",
                //    Option = "Blog"
                //},
            };

            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, pageList);
        }

    }
}

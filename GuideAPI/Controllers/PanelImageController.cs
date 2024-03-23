using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Dto.ApiPanelDtos.CommentDtos;
using Dto.ApiPanelDtos.ImageManagementDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelImageController : ControllerBase
    {
        private readonly IPageService _pageService;
        private readonly IPageLanguageService _pageLanguageService;

        public PanelImageController(IPageService pageService, IPageLanguageService pageLanguageService)
        {
            _pageService = pageService;
            _pageLanguageService = pageLanguageService;
        }

        [HttpGet]
        public CustomResponseDto<List<ImageListDto>> ImageList()
        {
            List<ImageListDto> imageListDto =
                    (from page in _pageService.GetAll(x => !x.IsDeleted && !x.IsHomePage)
                     where page.Type == 1 && !page.IsHomePage
                     select new ImageListDto()
                     {
                         ImageID = page.Id,
                         PageName = page.PageName,
                         ImagePath = page.ImagePath
                     }).ToList();

            return CustomResponseDto<List<ImageListDto>>.Success(200, imageListDto);
        }


        [HttpGet("{id}")]
        public CustomResponseDto<EditImageDto> EditImage(Guid id)
        {
            Page page = _pageService.GetById(id);

            EditImageDto editImageDto = new EditImageDto()
            {
                ImageID = page.Id,
                ImagePath = page.ImagePath,
                PageName = page.PageName
            };

            return CustomResponseDto<EditImageDto>.Success(200, editImageDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditImage(EditImageDto editImage)
        {
            Page page = _pageService.GetById(editImage.ImageID);

            page.ImagePath = editImage.ImagePath;
            _pageService.Update(page);

            return CustomResponseNullDto.Success(200);
        }

        [HttpGet("{id}/{languageId}")]
        public CustomResponseDto<LanguageEditImageDto> LanguageEditImage(Guid id, int languageId)
        {
            PageLanguageItem pageLanguageItem = _pageLanguageService.Where(x => x.PageID == id && x.LanguageId == languageId).FirstOrDefault();
            Page page = _pageService.GetById(id);

            LanguageEditImageDto languageEditImageDto = new LanguageEditImageDto()
            {
                ImageLanguageItemID = pageLanguageItem.Id,
                LanguageID = languageId,
                Name = page.PageName,
                DisplayName = pageLanguageItem.DisplayName,
                LanguageName = LanguageList.AllLanguages.FirstOrDefault(x => x.Id == languageId).Name
            };

            return CustomResponseDto<LanguageEditImageDto>.Success(200, languageEditImageDto);
        }

        [HttpPost]
        public CustomResponseNullDto LanguageEditImage(LanguageEditImageDto languageEditImageDto)
        {
            PageLanguageItem pageLanguageItem = _pageLanguageService.GetById(languageEditImageDto.ImageLanguageItemID);
            pageLanguageItem.DisplayName = languageEditImageDto.DisplayName;
            _pageLanguageService.Update(pageLanguageItem);

            return CustomResponseNullDto.Success(204);
        }
    }
}

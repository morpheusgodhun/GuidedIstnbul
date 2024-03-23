using Dto.ApiPanelDtos.BlogCategoryDtos;
using Dto.ApiPanelDtos.LanguageDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.BlogCategoryManagement)]
    public class BlogCategoryController : CustomBaseController
    {
        public BlogCategoryController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {

            string url = _url + "PanelBlogCategory/BlogCategoryList";
            CustomResponseDto<List<BlogCategoryListDto>> blogCategoryListDto = _apiHandler.GetApi<CustomResponseDto<List<BlogCategoryListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);
            if (blogCategoryListDto is null)
            {
                return View();
            }
            else
            {
                var model = (blogCategoryListDto.Data, languageListDto.Data);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddBlogCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBlogCategory(AddBlogCategoryDto addBlogCategoryDto)
        {
            string url = _url + "PanelBlogCategory/AddBlogCategory";
            _apiHandler.PostApi<CustomResponseNullDto>(addBlogCategoryDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditBlogCategory(string id)
        {
            string url = _url + "PanelBlogCategory/EditBlogCategory/" + id;
            CustomResponseDto<EditBlogCategoryDto> editBlogCategoryDto = _apiHandler.GetApi<CustomResponseDto<EditBlogCategoryDto>>(url);
            if (editBlogCategoryDto is null)
            {
                return View();
            }
            else
            {
                return View(editBlogCategoryDto.Data);
            }
        }
        [HttpPost]
        public IActionResult EditBlogCategory(EditBlogCategoryDto editBlogCategoryDto)
        {
            string url = _url + "PanelBlogCategory/EditBlogCategory";
            _apiHandler.PostApi<CustomResponseNullDto>(editBlogCategoryDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LanguageEditBlogCategory(string id, string languageId)
        {
            string url = _url + "PanelBlogCategory/LanguageEditBlogCategory/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditBlogCategoryDto> languageEditBlogCategoryDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditBlogCategoryDto>>(url);
            if (languageEditBlogCategoryDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditBlogCategoryDto.Data);
            }
        }
        [HttpPost]
        public IActionResult LanguageEditBlogCategory(LanguageEditBlogCategoryDto languageEditBlogCategoryDto)
        {
            string url = _url + "PanelBlogCategory/LanguageEditBlogCategory";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditBlogCategoryDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleBlogCategoryStatus(string id)
        {
            string url = _url + "PanelBlogCategory/ToggleBlogCategoryStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }

    }
}

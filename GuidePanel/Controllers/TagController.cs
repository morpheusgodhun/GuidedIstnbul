using Dto.ApiPanelDtos.LanguageDtos;
using Dto.ApiPanelDtos.TagManagementDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using GuidePanel.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.TagManagement)]

    public class TagController : CustomBaseController
    {
        private readonly IFileUpload _fileUpload;
        public TagController(IApiHandler apiHandler, IConfiguration configuration, IFileUpload fileUpload) : base(apiHandler, configuration)
        {
            _fileUpload = fileUpload;
        }

        public IActionResult Index()
        {
            string url = _url + "PanelTag/TagList";
            CustomResponseDto<List<TagListDto>> tagListDtos = _apiHandler.GetApi<CustomResponseDto<List<TagListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);

            if (tagListDtos is null)
            {
                return View();
            }
            else
            {
                var model = (tagListDtos.Data, languageListDto.Data);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddTag()
        {
            string tagCategoryUrl = _url + "PanelOtherOption/TagCategorySelectList";
            CustomResponseDto<List<SelectListOption>> tagCategoryDtos = _apiHandler.GetApi<CustomResponseDto<List<SelectListOption>>>(tagCategoryUrl);

            ViewBag.TagCategories = tagCategoryDtos.Data;

            return View();
        }

        [HttpPost]
        public IActionResult AddTag(AddTagDto addTagDto, IFormFile Icon)
        {
            string url = _url + "PanelTag/AddTag";
            if (Icon != null)
            {
                addTagDto.IconPath = _fileUpload.FileUploads(Icon);
            }

            addTagDto.IconPath = string.IsNullOrEmpty(addTagDto.IconPath) ? "Failed" : addTagDto.IconPath;
            _apiHandler.PostApi<CustomResponseNullDto>(addTagDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditTag(string id)
        {
            string tagCategoryUrl = _url + "PanelOtherOption/TagCategorySelectList";
            CustomResponseDto<List<SelectListOption>> tagCategoryDtos = _apiHandler.GetApi<CustomResponseDto<List<SelectListOption>>>(tagCategoryUrl);

            ViewBag.TagCategories = tagCategoryDtos.Data;
            string url = _url + "PanelTag/EditTag/" + id;
            CustomResponseDto<EditTagDto> editTagDto = _apiHandler.GetApi<CustomResponseDto<EditTagDto>>(url);

            string url1 = $"{_url}PanelTag/GetTagDisplayName/{id}";
            CustomResponseDto<string> tagDisplayNameResponse = _apiHandler.GetApi<CustomResponseDto<string>>(url1);
            ViewBag.TagDisplayName = tagDisplayNameResponse.Data;

            if (editTagDto is null)
            {
                return View();
            }
            else
            {
                return View(editTagDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditTag(EditTagDto editTagDto, IFormFile Icon)
        {
            string url = _url + "PanelTag/EditTag";
            if (Icon != null)
            {
                editTagDto.IconPath = _fileUpload.FileUploads(Icon);
            }
            _apiHandler.PostApi<CustomResponseNullDto>(editTagDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LanguageEditTag(string id, string languageId)
        {
            string url = _url + "PanelTag/LanguageEditTag/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditTagDto> languageEditTagDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditTagDto>>(url);

            if (languageEditTagDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditTagDto.Data);
            }
        }

        [HttpPost]
        public IActionResult LanguageEditTag(LanguageEditTagDto languageEditTagDto)
        {
            string url = _url + "PanelTag/LanguageEditTag";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditTagDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleTagStatus(string id)
        {
            string url = _url + "PanelTag/ToggleTagStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }
    }
}

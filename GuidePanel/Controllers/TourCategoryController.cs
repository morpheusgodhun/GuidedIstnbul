using Dto.ApiPanelDtos.CustomPageManagementDto;
using Dto.ApiPanelDtos.LanguageDtos;
using Dto.ApiPanelDtos.TourCategoryDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using GuidePanel.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.TourCategoryManagement)]

    public class TourCategoryController : CustomBaseController
    {
        private readonly IFileUpload _fileUpload;
        public TourCategoryController(IApiHandler apiHandler, IConfiguration configuration, IFileUpload fileUpload) : base(apiHandler, configuration)
        {
            _fileUpload = fileUpload;
        }

        public IActionResult Index()
        {

            string url = _url + "PanelTourCategory/TourCategoryList";
            CustomResponseDto<List<TourCategoryListDto>> tourCategoryListDtos = _apiHandler.GetApi<CustomResponseDto<List<TourCategoryListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);
            if (tourCategoryListDtos is null)
            {
                return View();
            }
            else
            {
                var model = (tourCategoryListDtos.Data, languageListDto.Data);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddTourCategory()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddTourCategory(AddTourCategoryDto addTourCategoryDto, IFormFile Icon, IFormFile BannerImage)
        {
            if (Icon != null)
            {
                addTourCategoryDto.IconPath = _fileUpload.FileUploads(Icon);
            }
            if (BannerImage != null)
            {
                addTourCategoryDto.BannerImagePath = _fileUpload.FileUploads(BannerImage);
            }
            string url = _url + "PanelTourCategory/AddTourCategory";
            _apiHandler.PostApi<CustomResponseNullDto>(addTourCategoryDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditTourCategory(string id)
        {
            string url = _url + "PanelTourCategory/EditTourCategory/" + id;
            CustomResponseDto<EditTourCategoryDto> editTourCategoryDto = _apiHandler.GetApi<CustomResponseDto<EditTourCategoryDto>>(url);

            string url1 = _url + "PanelTourCategory/GetTourCategoryName/" + id;
            CustomResponseDto<string> tourCategoryNameResponse = _apiHandler.GetApi<CustomResponseDto<string>>(url1);

            ViewBag.TourCategoryName = tourCategoryNameResponse.Data;

            return View(editTourCategoryDto.Data);
        }

        [HttpPost]
        public IActionResult EditTourCategory(EditTourCategoryDto editTourCategoryDto, IFormFile Icon, IFormFile BannerImage)
        {
            if (Icon != null)
            {
                editTourCategoryDto.IconPath = _fileUpload.FileUploads(Icon);
            }
            if (BannerImage != null)
            {
                editTourCategoryDto.BannerImagePath = _fileUpload.FileUploads(BannerImage);
            }
            string url = _url + "PanelTourCategory/EditTourCategory";
            _apiHandler.PostApi<CustomResponseNullDto>(editTourCategoryDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LanguageEditTourCategory(string id, string languageId)
        {
            string url = _url + "PanelTourCategory/LanguageEditTourCategory/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditTourCategoryDto> languageEditTourCategoryDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditTourCategoryDto>>(url);

            return View(languageEditTourCategoryDto.Data);
        }

        [HttpPost]
        public IActionResult LanguageEditTourCategory(LanguageEditTourCategoryDto languageEditTourCategoryDto)
        {
            string url = _url + "PanelTourCategory/LanguageEditTourCategory";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditTourCategoryDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleTourCategoryStatus(string id)
        {
            string url = _url + "PanelTourCategory/ToggleTourCategoryStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }
    }
}


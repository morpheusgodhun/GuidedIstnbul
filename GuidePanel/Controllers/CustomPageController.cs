using Dto.ApiPanelDtos.CustomPageManagementDto;
using Dto.ApiPanelDtos.LanguageDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using GuidePanel.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.CustomPageManagement)]

    public class CustomPageController : CustomBaseController
    {
        private readonly IFileUpload _fileUpload;
        public CustomPageController(IApiHandler apiHandler, IConfiguration configuration, IFileUpload fileUpload) : base(apiHandler, configuration)
        {
            _fileUpload = fileUpload;
        }


        public IActionResult Index()
        {
            string url = _url + "PanelCustomPage/CustomPageList";
            CustomResponseDto<List<CustomPageListDto>> customPageListDto = _apiHandler.GetApi<CustomResponseDto<List<CustomPageListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);
            if (customPageListDto is null)
            {
                return View();
            }
            else
            {
                var model = (customPageListDto.Data, languageListDto.Data);
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult AddCustomPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCustomPage(AddCustomPageDto addCustomPageDto, IFormFile Image)
        {
            string url = _url + "PanelCustomPage/AddCustomPage";
            if (Image != null)
            {
                addCustomPageDto.ImagePath = _fileUpload.FileUploads(Image);
            }
            _apiHandler.PostApi<CustomResponseNullDto>(addCustomPageDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditCustomPage(string id)
        {
            string url = _url + "PanelCustomPage/EditCustomPage/" + id;
            CustomResponseDto<EditCustomPageDto> editCustomPage = _apiHandler.GetApi<CustomResponseDto<EditCustomPageDto>>(url);

            if (editCustomPage is null)
            {
                return View();
            }
            else
            {
                return View(editCustomPage.Data);
            }
        }


        [HttpPost]
        public IActionResult EditCustomPage(EditCustomPageDto editCustomPage, IFormFile Image)
        {
            string url = _url + "PanelCustomPage/EditCustomPage";
            if (Image != null)
            {
                editCustomPage.ImagePath = _fileUpload.FileUploads(Image);
            }
            _apiHandler.PostApi<CustomResponseNullDto>(editCustomPage, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditLanguageCustomPage(string id, string languageId)
        {

            string url = _url + "PanelCustomPage/LanguageEditCustomPage/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditCustomPageDto> languageEditCustomPageDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditCustomPageDto>>(url);

            if (languageEditCustomPageDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditCustomPageDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditLanguageCustomPage(LanguageEditCustomPageDto languageEditCustomPageDto)
        {
            string url = _url + "PanelCustomPage/LanguageEditCustomPage";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditCustomPageDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleCustomPageStatus(string id)
        {
            string url = _url + "PanelCustomPage/ToggleCustomPageStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }
    }
}

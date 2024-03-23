using Dto.ApiPanelDtos.CancellationPolicyDtos;
using Dto.ApiPanelDtos.ImageManagementDtos;
using Dto.ApiPanelDtos.LanguageDtos;
using Dto.ApiWebDtos.WebToApiDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using GuidePanel.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.ImageManagement)]
    public class ImageController : CustomBaseController
    {
        private readonly IFileUpload _fileUpload;
        public ImageController(IApiHandler apiHandler, IConfiguration configuration, IFileUpload fileUpload) : base(apiHandler, configuration)
        {
            _fileUpload = fileUpload;
        }


        public IActionResult Index()
        {
            string url = _url + "PanelImage/ImageList";
            CustomResponseDto<List<ImageListDto>> imageListDto = _apiHandler.GetApi<CustomResponseDto<List<ImageListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);
            if (imageListDto is null)
            {
                return View();
            }
            else
            {
                var model = (imageListDto.Data, languageListDto.Data);
                return View(model);
            }

        }

        [HttpGet]
        public IActionResult EditImage(string id)
        {
            string url = _url + "PanelImage/EditImage/" + id;
            CustomResponseDto<EditImageDto> getEditImageDto = _apiHandler.GetApi<CustomResponseDto<EditImageDto>>(url);

            if (getEditImageDto is null)
            {
                return View();
            }
            else
            {
                return View(getEditImageDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditImage(EditImageDto editImageDto, IFormFile Image)
        {
            if (Image != null)
            {
                editImageDto.ImagePath = _fileUpload.FileUploads(Image);
            }
            string url = _url + "PanelImage/EditImage";
            _apiHandler.PostApi<CustomResponseNullDto>(editImageDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LanguageEditImage(string id, string languageId)
        {
            string url = _url + "PanelImage/LanguageEditImage/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditImageDto> languageEditImageDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditImageDto>>(url);

            if (languageEditImageDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditImageDto.Data);
            }
        }

        [HttpPost]
        public IActionResult LanguageEditImage(LanguageEditImageDto languageEditImageDto)
        {

            string url = _url + "PanelImage/LanguageEditImage";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditImageDto, url);
            return RedirectToAction("Index");
        }
    }
}

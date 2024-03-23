using Dto.ApiPanelDtos.InfoCardDtos;
using Dto.ApiPanelDtos.LanguageDtos;
using Dto.ApiPanelDtos.TagManagementDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using GuidePanel.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.InfoCardManagement)]
    public class InfoCardController : CustomBaseController
    {
        private readonly IFileUpload _fileUpload;

        public InfoCardController(IApiHandler apiHandler, IConfiguration configuration, IFileUpload fileUpload) : base(apiHandler, configuration)
        {
            _fileUpload = fileUpload;
        }

        public IActionResult Index()
        {
            string url = _url + "PanelInfoCard/InfoCardList";
            CustomResponseDto<List<InfoCardListDto>> infoCardListDto = _apiHandler.GetApi<CustomResponseDto<List<InfoCardListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);

            if (infoCardListDto is null)
            {
                return View();
            }
            else
            {
                var model = (infoCardListDto.Data, languageListDto.Data);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddInfoCard()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddInfoCard(AddInfoCardDto addInfoCardDto, IFormFile Icon)
        {
            string url = _url + "PanelInfoCard/AddInfoCard";
            if (Icon != null)
            {
                addInfoCardDto.IconPath = _fileUpload.FileUploads(Icon);
            }
            _apiHandler.PostApi<CustomResponseNullDto>(addInfoCardDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditInfoCard(string id)
        {

            string url = _url + "PanelInfoCard/EditInfoCard/" + id;
            CustomResponseDto<EditInfoCardDto> editInfoCardDto = _apiHandler.GetApi<CustomResponseDto<EditInfoCardDto>>(url);



            if (editInfoCardDto is null)
            {
                return View();
            }
            else
            {
                return View(editInfoCardDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditInfoCard(EditInfoCardDto editInfoCardDto, IFormFile Icon)
        {
            string url = _url + "PanelInfoCard/EditInfoCard";
            if (Icon != null)
            {
                editInfoCardDto.IconPath = _fileUpload.FileUploads(Icon);
            }
            _apiHandler.PostApi<CustomResponseNullDto>(editInfoCardDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LanguageEditInfoCard(string id, string languageId)
        {
            string url = _url + "PanelInfoCard/LanguageEditInfoCard/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditInfoCardDto> languageEditInfoCardDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditInfoCardDto>>(url);

            if (languageEditInfoCardDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditInfoCardDto.Data);
            }
        }

        [HttpPost]
        public IActionResult LanguageEditInfoCard(LanguageEditInfoCardDto languageEditInfoCardDto)
        {
            string url = _url + "PanelInfoCard/LanguageEditInfoCard";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditInfoCardDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleInfoCardStatus(string id)
        {
            string url = _url + "PanelInfoCard/ToggleInfoCardStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }
    }
}

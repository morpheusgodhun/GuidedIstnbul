
using Dto.ApiPanelDtos.DestinationDtos;
using Dto.ApiPanelDtos.LanguageDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.DestinationManagement)]

    public class DestinationController : CustomBaseController
    {
        public DestinationController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            string url = _url + "PanelDestination/DestinationList";
            CustomResponseDto<List<DestinationListDto>> destinationListDto = _apiHandler.GetApi<CustomResponseDto<List<DestinationListDto>>>(url);

            string languageUrl = _url + "PanelLanguage/LanguageList";
            CustomResponseDto<List<LanguageListDto>> languageListDto = _apiHandler.GetApi<CustomResponseDto<List<LanguageListDto>>>(languageUrl);
            if (destinationListDto is null)
            {
                return View();
            }
            else
            {
                var model = (destinationListDto.Data, languageListDto.Data);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddDestination()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDestination(AddDestinationDto addDestinationDto)
        {
            string url = _url + "PanelDestination/AddDestination";
            _apiHandler.PostApi<CustomResponseNullDto>(addDestinationDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditDestination(string id)
        {
            string url = _url + "PanelDestination/EditDestination/" + id;
            CustomResponseDto<EditDestinationDto> editDestinationDto = _apiHandler.GetApi<CustomResponseDto<EditDestinationDto>>(url);

            if (editDestinationDto is null)
            {
                return View();
            }
            else
            {
                return View(editDestinationDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditDestination(EditDestinationDto editDestinationDto)
        {
            string url = _url + "PanelDestination/EditDestination";
            _apiHandler.PostApi<CustomResponseNullDto>(editDestinationDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LanguageEditDestination(string id, string languageId)
        {
            string url = _url + "PanelDestination/LanguageEditDestination/" + id + "/" + languageId;
            CustomResponseDto<LanguageEditDestinationDto> languageEditDestinationDto = _apiHandler.GetApi<CustomResponseDto<LanguageEditDestinationDto>>(url);

            if (languageEditDestinationDto is null)
            {
                return View();
            }
            else
            {
                return View(languageEditDestinationDto.Data);
            }
        }

        [HttpPost]
        public IActionResult LanguageEditDestination(LanguageEditDestinationDto languageEditDestinationDto)
        {
            string url = _url + "PanelDestination/LanguageEditDestination";
            _apiHandler.PostApi<CustomResponseNullDto>(languageEditDestinationDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleDestinationStatus(string id)
        {
            string url = _url + "PanelDestination/ToggleDestinationStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleShowOnCustomMade(string id)
        {
            string url = _url + "PanelDestination/ToggleShowOnCustomMade/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }
    }
}


using Dto.ApiPanelDtos.PersonPolicyDtos;
using Dto.ApiPanelDtos.SeoDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.SeoManagement)]
    public class SeoController : CustomBaseController
    {
        public SeoController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            string url = _url + "PanelSeo/SeoList";
            CustomResponseDto<List<SeoListDto>> seoListDto = _apiHandler.GetApi<CustomResponseDto<List<SeoListDto>>>(url);

            if (seoListDto is null)
            {
                return View();
            }
            else
            {
                return View(seoListDto.Data);
            }
        }

        [HttpGet]
        public IActionResult AddSeo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSeo(AddSeoDto addSeoDto)
        {
            string url = _url + "PanelSeo/AddSeo";
            _apiHandler.PostApi<CustomResponseNullDto>(addSeoDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditSeo(string id)
        {
            string url = _url + "PanelSeo/EditSeo/" + id;
            CustomResponseDto<EditSeoDto> editSeoDto = _apiHandler.GetApi<CustomResponseDto<EditSeoDto>>(url);

            if (editSeoDto is null)
            {
                return View();
            }
            else
            {
                return View(editSeoDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditSeo(EditSeoDto editSeoDto)
        {
            string url = _url + "PanelSeo/EditSeo";
            _apiHandler.PostApi<CustomResponseNullDto>(editSeoDto, url);
            return RedirectToAction("Index");
        }
    }
}

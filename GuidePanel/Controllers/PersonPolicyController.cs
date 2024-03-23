using Dto.ApiPanelDtos.PersonPolicyDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.PersonPolicyManagement)]

    public class PersonPolicyController : CustomBaseController
    {
        public PersonPolicyController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            string url = _url + "PanelPersonPolicy/PersonPolicyList";
            CustomResponseDto<List<PersonPolicyListDto>> personPolicyListDto = _apiHandler.GetApi<CustomResponseDto<List<PersonPolicyListDto>>>(url);

            if (personPolicyListDto is null)
            {
                return View();
            }
            else
            {
                return View(personPolicyListDto.Data);
            }
        }

        [HttpGet]
        public IActionResult AddPersonPolicy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPersonPolicy(AddPersonPolicyDto addPersonPolicyDto)
        {
            string url = _url + "PanelPersonPolicy/AddPersonPolicy";
            _apiHandler.PostApi<CustomResponseNullDto>(addPersonPolicyDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditPersonPolicy(string id)
        {
            string url = _url + "PanelPersonPolicy/EditPersonPolicy/" + id;
            CustomResponseDto<EditPersonPolicyDto> editPersonPolicyDto = _apiHandler.GetApi<CustomResponseDto<EditPersonPolicyDto>>(url);

            if (editPersonPolicyDto is null)
            {
                return View();
            }
            else
            {
                return View(editPersonPolicyDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditPersonPolicy(EditPersonPolicyDto editPersonPolicyDto)
        {
            string url = _url + "PanelPersonPolicy/EditPersonPolicy";
            _apiHandler.PostApi<CustomResponseNullDto>(editPersonPolicyDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult TogglePersonPolicyStatus(string id)
        {
            string url = _url + "PanelPersonPolicy/TogglePersonPolicyStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }


    }
}

using Dto.ApiPanelDtos.ChildPolicyDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.ChildPolicyManagement)]

    public class ChildPolicyController : CustomBaseController
    {
        public ChildPolicyController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            string url = _url + "PanelChildPolicy/ChildPolicyList";
            CustomResponseDto<List<ChildPolicyListDto>> childPolicyListDto = _apiHandler.GetApi<CustomResponseDto<List<ChildPolicyListDto>>>(url);

            if (childPolicyListDto is null)
            {
                return View();
            }
            else
            {
                return View(childPolicyListDto.Data);
            }
        }

        [HttpGet]
        public IActionResult AddChildPolicy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddChildPolicy(AddChildPolicyDto addChildPolicyDto)
        {
            string url = _url + "PanelChildPolicy/AddChildPolicy";
            _apiHandler.PostApi<CustomResponseNullDto>(addChildPolicyDto, url);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditChildPolicy(string id)
        {
            string url = _url + "PanelChildPolicy/EditChildPolicy/" + id;
            CustomResponseDto<EditChildPolicyDto> editChildPolicyDto = _apiHandler.GetApi<CustomResponseDto<EditChildPolicyDto>>(url);

            if (editChildPolicyDto is null)
            {
                return View();
            }
            else
            {
                return View(editChildPolicyDto.Data);
            }
        }

        [HttpPost]
        public IActionResult EditChildPolicy(EditChildPolicyDto editChildPolicyDto)
        {
            string url = _url + "PanelChildPolicy/EditChildPolicy";
            _apiHandler.PostApi<CustomResponseNullDto>(editChildPolicyDto, url);
            return RedirectToAction("Index");
        }

        public IActionResult ToggleChildPolicyStatus(string id)
        {
            string url = _url + "PanelChildPolicy/ToggleChildPolicyStatus/" + id;
            _apiHandler.GetApi<CustomResponseNullDto>(url);
            return RedirectToAction("Index");
        }


    }
}

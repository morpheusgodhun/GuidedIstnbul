using Dto.ApiPanelDtos.AgentDtos;
using Dto.ApiPanelDtos.ProductDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.AgentManagement)]

    public class AgentController : CustomBaseController
    {
        public AgentController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {

            string url = _url + "WebUserPage/GetAgents/1";
            CustomResponseDto<List<AgentDto>> getApplyAgentDto = _apiHandler.GetApi<CustomResponseDto<List<AgentDto>>>(url);
            string url2 = _url + "WebUserPage/GetProductAdvanceDiscount";
            CustomResponseDto<List<ProductNameAndIdDto>> ProductAdvanceDiscounts = _apiHandler.GetApi<CustomResponseDto<List<ProductNameAndIdDto>>>(url2);

            if (getApplyAgentDto is null)
            {
                return View();
            }
            else
            {
                var pageModel = (getApplyAgentDto.Data, ProductAdvanceDiscounts.Data);
                return View(pageModel);
            }
        }
        [HttpGet]
        public IActionResult GetAdvanceDiscountInfos(string Id)
        {
            string url = _url + $"WebUserPage/GetAdvanceDiscountInfos/{Id}";
            CustomResponseDto<List<Many_Agent_ProductDto>> discountInfos = _apiHandler.GetApi<CustomResponseDto<List<Many_Agent_ProductDto>>>(url);
            return Json(discountInfos);
        }
        [HttpPost]
        public IActionResult PostAdvanceDiscountInfos([FromBody] List<Many_Agent_ProductDto> agentProducts)
        {
            string url = _url + $"WebUserPage/PostAdvanceDiscountInfos";
            CustomResponseNullDto result = _apiHandler.PostApi<CustomResponseNullDto>(agentProducts, url);
            return View();
        }

        [HttpPost]
        public IActionResult Approve(Guid AgentId, int DiscountRate)
        {
            string url = _url + "WebUserPage/ApproveAgent";
            var model = new AgentApprove { AgentId = AgentId, Discount = DiscountRate };
            CustomResponseNullDto result = _apiHandler.PostApi<CustomResponseNullDto>(model, url);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Reject(Guid AgentId)
        {
            string url = _url + "WebUserPage/RejectAgent";
            var model = new AgentApprove { AgentId = AgentId };
            CustomResponseNullDto result = _apiHandler.PostApi<CustomResponseNullDto>(model, url);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ChangeStatus(Guid AgentId)//
        {
            string url = _url + "WebUserPage/ChangeStatus?";
            var model = new AgentApprove { AgentId = AgentId };
            CustomResponseNullDto result = _apiHandler.PostApi<CustomResponseNullDto>(model, url);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UserRequests(Guid agentId)
        {
            string url = _url + "PanelAgent/GetAgentUserRequestList/" + agentId;
            var response = _apiHandler.GetApi<CustomResponseDto<List<AgentUserRequestListDto>>>(url);
            if (response is not null)
                return View(response.Data);
            else
                return NotFound();
        }
        [HttpGet]
        public IActionResult ApproveUserRequest(string requestId)
        {
            string url = _url + "PanelAgent/ApproveUserRequest";
            _apiHandler.PostApi<CustomResponseNullDto>(new AgentUserRequestApprove
            {
                RequestID = requestId,
            }, url);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RejectUserRequest(string requestId)
        {
            string url = _url + "PanelAgent/RejectUserRequest";
            _apiHandler.PostApi<CustomResponseNullDto>(new AgentUserRequestApprove
            {
                RequestID = requestId,
            }, url);
            return RedirectToAction(nameof(Index));
        }

    }
}//

using Core.Entities;
using Core.IRepository;
using Core.IService;
using Dto.ApiPanelDtos.AgentDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelAgentController : ControllerBase
    {
        readonly IAgentUserRequestService _agentUserRequestService;

        public PanelAgentController(IAgentUserRequestService agentUserRequestService)
        {
            _agentUserRequestService = agentUserRequestService;
        }

        //Agent Select List
        [HttpGet]
        public CustomResponseDto<List<SelectListOptionDto>> AgentSelectList()
        {
            List<SelectListOptionDto> agentSelectList = new List<SelectListOptionDto>()
            {
                //new SelectListOptionDto()
                //{
                //    OptionID = "1",
                //    Option = "GIT"
                //},
                //new SelectListOptionDto()
                //{
                //    OptionID = "2",
                //    Option = "Venus Agent"
                //},
                //new SelectListOptionDto()
                //{
                //    OptionID = "3",
                //    Option = "Selenity Agent"
                //},
                //new SelectListOptionDto()
                //{
                //    OptionID = "4",
                //    Option = "Infinity Agent"
                //},
            };


            return CustomResponseDto<List<SelectListOptionDto>>.Success(200, agentSelectList);
        }
        [HttpGet("{agentId}")]
        public CustomResponseDto<List<AgentUserRequestListDto>> GetAgentUserRequestList(string agentId)
        {
            var data = _agentUserRequestService.GetAgentUserRequestList(agentId);
            return CustomResponseDto<List<AgentUserRequestListDto>>.Success(200, data);

        }
        [HttpPost]
        public CustomResponseNullDto ApproveUserRequest(AgentUserRequestApprove agentUserRequestApprove)
        {
            _agentUserRequestService.ApproveUserRequest(agentUserRequestApprove.RequestID);
            return CustomResponseNullDto.Success(200);
        }
        public CustomResponseNullDto RejectUserRequest(AgentUserRequestApprove agentUserRequestApprove)
        {
            _agentUserRequestService.RejectUserRequest(agentUserRequestApprove.RequestID);
            return CustomResponseNullDto.Success(200);
        }
    }
}

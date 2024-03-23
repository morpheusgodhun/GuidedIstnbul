using Core.Entities;
using Core.IService;
using Dto.ApiPanelDtos.ChildPolicyDtos;
using Dto.ApiPanelDtos.PersonPolicyDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelChildPolicyController : ControllerBase
    {
        private readonly IChildPolicyService _childPolicyService;

        public PanelChildPolicyController(IChildPolicyService childPolicyService)
        {
            _childPolicyService = childPolicyService;
        }

        [HttpGet]
        public CustomResponseDto<List<ChildPolicyListDto>> ChildPolicyList()
        {

            return CustomResponseDto<List<ChildPolicyListDto>>.Success(200, _childPolicyService.GetChildPolicyListDtos());   
        }

        [HttpPost]
        public CustomResponseNullDto AddChildPolicy(AddChildPolicyDto addChildPolicyDto)
        {
            _childPolicyService.AddChildPolicy(addChildPolicyDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditChildPolicyDto> EditChildPolicy(Guid id)
        {
            return CustomResponseDto<EditChildPolicyDto>.Success(200, _childPolicyService.GetEditChildPolicyDto(id));  
        }

        [HttpPost]
        public CustomResponseNullDto EditChildPolicy(EditChildPolicyDto editChildPolicyDto)
        {

            _childPolicyService.EditChildPolicy(editChildPolicyDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleChildPolicyStatus(Guid id)
        {
            _childPolicyService.ToggleStatus(id);
            return CustomResponseNullDto.Success(204);
        }
    }
}

using Core.Entities;
using Core.IService;
using Dto.ApiPanelDtos.PersonPolicyDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelPersonPolicyController : ControllerBase
    {

        private readonly IPersonPolicyService _personPolicyService;

        public PanelPersonPolicyController(IPersonPolicyService personPolicyService)
        {
            _personPolicyService = personPolicyService;
        }

        [HttpGet]
        public CustomResponseDto<List<PersonPolicyListDto>> PersonPolicyList()
        {
            var personPolicyListDtos = _personPolicyService.GetPersonPolicyListDto();
            return CustomResponseDto<List<PersonPolicyListDto>>.Success(200, personPolicyListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddPersonPolicy(AddPersonPolicyDto addPersonPolicyDto)
        {
            _personPolicyService.AddPersonPolicy(addPersonPolicyDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditPersonPolicyDto> EditPersonPolicy(Guid id)
        {
            EditPersonPolicyDto editPersonPolicyDto = _personPolicyService.GetEditPersonPolicyDto(id);
            return CustomResponseDto<EditPersonPolicyDto>.Success(200, editPersonPolicyDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditPersonPolicy(EditPersonPolicyDto editPersonPolicyDto)
        {
            _personPolicyService.EditPersonPolicy(editPersonPolicyDto);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto TogglePersonPolicyStatus(Guid id)
        {
             _personPolicyService.ToggleStatus(id);
            return CustomResponseNullDto.Success(204);
        }

        [HttpGet]
        public CustomResponseDto<List<PersonPolicyDto>> PersonPolicyDtoList()
        {
            var personPolicyDtos = _personPolicyService.PersonPolicyDtoList();
            return CustomResponseDto<List<PersonPolicyDto>>.Success(200, personPolicyDtos);
        }

}
}

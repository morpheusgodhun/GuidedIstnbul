using Core.IService;
using Dto.ApiPanelDtos.CustomerDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelCustomerController : CustomBaseController
    {
        readonly IUserService _userService;

        public PanelCustomerController(IUserService userService)
        {
            _userService = userService;
        }
        public CustomResponseDto<List<CustomerListDto>> GetCustomers()
        {
            List<CustomerListDto> data = _userService.GetCustomerList();
            return CustomResponseDto<List<CustomerListDto>>.Success(200, data);
        }

    }
}

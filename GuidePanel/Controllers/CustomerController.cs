using Dto.ApiPanelDtos.CustomerDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.CustomerManagement)]
    public class CustomerController : CustomBaseController
    {
        public CustomerController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }
        [HttpGet]
        public IActionResult Index()
        {
            string url = _url + "PanelCustomer/GetCustomers";
            CustomResponseDto<List<CustomerListDto>> customerList = _apiHandler.GetApi<CustomResponseDto<List<CustomerListDto>>>(url);
            if (customerList is null)
            {
                return View();
            }
            else
            {
                return View(customerList.Data);
            }
        }
    }
}

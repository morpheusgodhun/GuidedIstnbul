using Dto.ApiPanelDtos.ContactMessageDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.ContactMessageManagement)]

    public class ContactMessageController : CustomBaseController
    {
        public ContactMessageController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }

        public IActionResult Index()
        {
            string url = _url + "PanelContactMessage/ContactMessageList";
            CustomResponseDto<List<ContactMessageListDto>> contactMessageListDto = _apiHandler.GetApi<CustomResponseDto<List<ContactMessageListDto>>>(url);

            if (contactMessageListDto is null)
            {
                return View();
            }
            else
            {
                return View(contactMessageListDto.Data);
            }
        }

        public IActionResult ContactMessageDetail(string id)
        {
            string url = _url + "PanelContactMessage/ContactMessageDetail/" + id;
            CustomResponseDto<ContactMessageDetailDto> contactMessageDetailDto = _apiHandler.GetApi<CustomResponseDto<ContactMessageDetailDto>>(url);

            if (contactMessageDetailDto is null)
            {
                return View();
            }
            else
            {
                return View(contactMessageDetailDto.Data);
            }
        }
    }
}

using Dto.ApiPanelDtos.MailTemplateDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using GuidePanel.APIHandler;
using GuidePanel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuidePanel.Controllers
{
    [HasPermissionAuthorize(Permission.MailManagement)]

    public class MailController : CustomBaseController
    {
        public MailController(IApiHandler apiHandler, IConfiguration configuration) : base(apiHandler, configuration)
        {
        }
        public IActionResult Index()
        {
            string url = _url + "PanelMail/GetMailTemplatesToCreate";
            CustomResponseDto<List<SelectListOption>> responseDto = _apiHandler.GetApi<CustomResponseDto<List<SelectListOption>>>(url);
            return View(responseDto.Data);
        }
        [HttpGet]
        public IActionResult AddMailTemplateItem(AddTemplateResponseDto responseDto)
        {
            ViewBag.TemplateName = responseDto.TemplateName;
            return View(new AddMailTemplateItemDto
            {
                SendMailTemplateId = responseDto.SendMailTemplateId,
            });
        }
        [HttpPost]
        public IActionResult AddMailTemplateItem(AddMailTemplateItemDto dto)
        {
            string url = _url + "PanelMail/AddMailTemplateItem";
            CustomResponseNullDto response = _apiHandler.PostApi<CustomResponseNullDto>(dto, url);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        //aslında id değil -- mailTemplateNo 
        public IActionResult AddMailTemplate(int id)
        {
            var url = _url + "PanelMail/CreateTemplate";
            var response = _apiHandler.PostApi<CustomResponseDto<AddTemplateResponseDto>>(new AddTemplateDto { TemplateNo = id }, url);
            if (response.StatusCode == 200)
                return RedirectToAction(nameof(AddMailTemplateItem), new AddTemplateResponseDto
                {
                    SendMailTemplateId = response.Data.SendMailTemplateId,
                    TemplateName = response.Data.TemplateName
                });
            else
                return View();
        }

    }
}

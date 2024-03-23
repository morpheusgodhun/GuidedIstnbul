using Core.IService;
using Core.StaticValues;
using Data.Repository;
using Dto.ApiPanelDtos.MailTemplateDtos;
using Dto.ApiWebDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelMailController : ControllerBase
    {
        readonly ISendMailTemplateService _sendMailTemplateService;
        public PanelMailController(ISendMailTemplateService sendMailTemplateService)
        {
            _sendMailTemplateService = sendMailTemplateService;
        }

        [HttpGet]
        public CustomResponseDto<List<SelectListOption>> GetMailTemplatesToCreate()
        {
            var data = _sendMailTemplateService.GetTemplateListToCreateLanguageItem();
            return CustomResponseDto<List<SelectListOption>>.Success(200, data);
        }
        [HttpGet]
        public CustomResponseDto<List<MailTemplateListDto>> GetMails()
        {
            var data = _sendMailTemplateService.GetTemplateList();
            return CustomResponseDto<List<MailTemplateListDto>>.Success(200, data);
        }

        [HttpPost]
        public CustomResponseDto<AddTemplateResponseDto> CreateTemplate(AddTemplateDto dto)
        {
            var (isAdded, addedId, templateName) = _sendMailTemplateService.AddMailTemplate(dto.TemplateNo);

            if (isAdded)
                return CustomResponseDto<AddTemplateResponseDto>.Success(200, new AddTemplateResponseDto { SendMailTemplateId = addedId, TemplateName = templateName });
            else
                return CustomResponseDto<AddTemplateResponseDto>.Fail(400, "Hata");
        }
        [HttpPost]
        public CustomResponseNullDto AddMailTemplateItem(AddMailTemplateItemDto dto)
        {
            _sendMailTemplateService.AddMailTemplateItem(dto);
            return CustomResponseNullDto.Success(200);
        }
    }
}

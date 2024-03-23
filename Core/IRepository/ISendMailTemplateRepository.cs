using Core.Entities;
using Dto.ApiPanelDtos.MailTemplateDtos;
using Dto.ApiPanelDtos.SendMailDtos;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface ISendMailTemplateRepository : IGenericRepository<SendMailTemplate>
    {
        SendMailTemplateDto GetTemplateByNo(int no, int languageId = 1);
        List<MailTemplateListDto> GetTemplateListDtos();
        List<SelectListOption> GetTemplateListToCreateLanguageItem();

    }
}

using Core.Entities;
using Dto.ApiPanelDtos.MailTemplateDtos;
using Dto.ApiPanelDtos.SendMailDtos;
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    public interface ISendMailTemplateService : IGenericService<SendMailTemplate>
    {
        SendMailTemplateDto GetTemplateByNo(int no);
        List<MailTemplateListDto> GetTemplateList();
        /// <summary>
        /// </summary>
        /// <returns>
        /// languageItema sahip olmayan templateleri dönüyor. panelden mailTemplateLanguageItem eklemek için yazdım.
        /// </returns>
        List<SelectListOption> GetTemplateListToCreateLanguageItem();
        void AddMailTemplateItem(AddMailTemplateItemDto template);
        (bool isAdded, Guid addedId, string templateName) AddMailTemplate(int templateNo);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.MailTemplateDtos
{
    public class MailTemplateListDto
    {
        public string MailTemplateId { get; set; }
        public string TemplateNo { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public int LanguageId { get; set; }
        public string TemplateName { get; set; }
    }

    public class CreateMailTemplateListDto //eklenecek mail template dto
    {
    }

}

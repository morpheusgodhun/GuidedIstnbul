using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.MailTemplateDtos
{
    public class AddTemplateDto
    {
        public int TemplateNo { get; set; }
    }
    public class AddTemplateResponseDto
    {
        public Guid SendMailTemplateId { get; set; }
        public string TemplateName { get; set; }
    }
}

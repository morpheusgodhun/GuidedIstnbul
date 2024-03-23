using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.MailTemplateDtos
{
    public class AddMailTemplateItemDto
    {
        public Guid SendMailTemplateId { get; set; }
        public string TurkishSubject { get; set; }
        public string TurkishContent { get; set; }
        public string EnglishSubject { get; set; }
        public string EnglishContent { get; set; }
    }
}

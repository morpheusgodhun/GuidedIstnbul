using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SendMailTemplateLanguageItem : BaseEntity
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public int LanguageId { get; set; }

        public Guid SendMailTemplateId { get; set; }
        public SendMailTemplate SendMailTemplate { get; set; }
    }
}

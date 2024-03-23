using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SendMailTemplate : BaseEntity
    {
        public int Template { get; set; } //template adı enum ile tutulacak
        public ICollection<SendMailTemplateLanguageItem> SendMailTemplateLanguageItems { get; set; }

        //public int Minutes { get; set; } // kaç dk önce yada sonra gönderilsin
    }
}

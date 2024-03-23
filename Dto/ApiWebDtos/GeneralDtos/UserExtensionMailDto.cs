using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.GeneralDtos
{
    public class UserExtensionMailDto
    {
        public string UserId { get; set; }
        public int MailTemplateType { get; set; } //SendMailTemplateName enum
        public string UrlCode { get; set; }
        public string ResetCode { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}

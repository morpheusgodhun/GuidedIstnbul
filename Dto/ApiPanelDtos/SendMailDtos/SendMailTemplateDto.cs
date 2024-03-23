using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.SendMailDtos
{
    public class SendMailTemplateDto
    {
        public int Template { get; set; } // template id
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}

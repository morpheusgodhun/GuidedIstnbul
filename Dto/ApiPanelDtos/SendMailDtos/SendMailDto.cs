using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.SendMailDtos
{
    public class SendMailDto
    {
        public string Email { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
        public DateTime SendTime { get; set; }
    }
}

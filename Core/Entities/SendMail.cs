using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SendMail : BaseEntity
    {
        public string Email { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
        public DateTime SendTime { get; set; }
        public bool IsSended { get; set; }
        public int TemplateNo { get; set; }
    }
}

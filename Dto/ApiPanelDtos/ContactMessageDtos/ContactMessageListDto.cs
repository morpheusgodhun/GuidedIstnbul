using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ContactMessageDtos
{
    public class ContactMessageListDto
    {
        public Guid ContactMessageID { get; set; }
        public DateTime SendingDate { get; set; }
        public string SenderName { get; set; }
        public string SenderPhone { get; set; }
        public string Subject { get; set; }
        
    }
}

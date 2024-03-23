﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ContactMessageDtos
{
    public class ContactMessageDetailDto
    {
        public Guid ContactMessageID { get; set; }
        public DateTime SendingDate { get; set; }
        public string SenderName { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string HowFindUs { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}

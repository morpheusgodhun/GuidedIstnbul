﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.AdditionalServiceDtos
{
    public class AdditionalServiceListDto
    {
        public Guid AdditionalServiceID { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public bool IsPerPerson { get; set; } // if 0 it is Per Group
        public bool IsPerDay { get; set; }  //if 0 it is Per Product
        public bool IsSpecialNumber { get; set; }  // if 0 it is According to the number of participant
    }
}

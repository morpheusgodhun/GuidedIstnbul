﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.FaqManagementDtos
{
    public class AddFaqDto
    {
        public int Order { get; set; }
        public Guid FaqCategoryID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}

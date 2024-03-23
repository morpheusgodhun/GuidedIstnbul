﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.AdditionalServiceDtos
{
    public class AddAdditionalServiceOptionPriceDto
    {
        public Guid OptionID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Priority { get; set; }
        public List<AdditionalServiceOptionPriceItemDto> Prices { get; set; }

    }

}

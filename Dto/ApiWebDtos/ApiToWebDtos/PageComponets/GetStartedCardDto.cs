﻿
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.PageComponets
{
    public class GetStartedCardDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public string Slug { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.SystemOptionDtos
{
    public class LanguageEditSystemOptionDto
    {
        public Guid SystemOptionLanguageItemID { get; set; }
        public string? LanguageName { get; set; }
        public string SystemOptionName { get; set; }
    }
}

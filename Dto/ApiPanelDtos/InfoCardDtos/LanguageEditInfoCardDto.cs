﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.InfoCardDtos
{
    public class LanguageEditInfoCardDto
    {
        public Guid LanguageInfoCardID { get; set; }
        public string? LanguageName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}

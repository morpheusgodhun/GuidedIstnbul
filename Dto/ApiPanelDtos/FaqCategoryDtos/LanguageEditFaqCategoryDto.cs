using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.FaqCategoryDtos
{
    public class LanguageEditFaqCategoryDto
    {
        public Guid FaqCategoryLanguageItemID { get; set; }
        public string? LanguageName { get; set; }
        public string CategoryName { get; set; }
    }
}

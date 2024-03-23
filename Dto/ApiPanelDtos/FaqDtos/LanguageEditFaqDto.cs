using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.FaqManagementDtos
{
    public class LanguageEditFaqDto
    {

        public Guid FaqLanguageItemID { get; set; }
        public string? LanguageName { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}

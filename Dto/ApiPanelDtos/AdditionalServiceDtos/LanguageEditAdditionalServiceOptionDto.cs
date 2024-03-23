using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.AdditionalServiceDtos
{
    public class LanguageEditAdditionalServiceOptionDto
    {
        public Guid OptionLanguageItemID { get; set; }
        public string? LanguageName { get; set; }
        public string OptionName { get; set; }
        public Guid AdditionalServiceID { get; set; }
    }
}

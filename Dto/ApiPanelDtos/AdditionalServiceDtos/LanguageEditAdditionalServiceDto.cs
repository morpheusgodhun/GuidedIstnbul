using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.AdditionalServiceDtos
{
    public class LanguageEditAdditionalServiceDto
    {
        public Guid AdditionalServiceLanguageItemID { get; set; }
        public string? LanguageName { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}

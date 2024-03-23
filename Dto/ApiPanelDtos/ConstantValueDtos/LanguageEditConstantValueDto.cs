using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ConstantValueDtos
{
    public class LanguageEditConstantValueDto
    {
        public Guid ConstantValueLanguageItemID { get; set; }
        public string? LanguageName { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}

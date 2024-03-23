using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.DestinationDtos
{
    public class LanguageEditDestinationDto
    {
        public Guid DestinationLanguageItemID { get; set; }
        public string? LanguageName { get; set; }
        public string DestinationName { get; set; }
    }
}

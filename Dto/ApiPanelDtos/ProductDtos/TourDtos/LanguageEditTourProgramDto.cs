using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.TourDtos
{
    public class LanguageEditTourProgramDto
    {
        public Guid TourID { get; set; }
        public int LanguageID { get; set; }
        public string? LanguageName { get; set; }
        public List<LanguageEditTourProgramItemDto> Programs { get; set; }
    }

    public class LanguageEditTourProgramItemDto
    {
        public Guid TourProgramLanguageItemID { get; set; }
        public int Day { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}

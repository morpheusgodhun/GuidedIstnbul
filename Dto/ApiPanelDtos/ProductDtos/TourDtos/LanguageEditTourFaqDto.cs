using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.TourDtos
{
    public class LanguageEditTourFaqDto
    {
        public Guid FaqLanguageID { get; set; }
        public string? LanguageName { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}

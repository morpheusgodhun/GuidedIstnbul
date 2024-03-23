using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.ServiceDtos
{
    public class LanguageEditServiceDto
    {
        public Guid ProductLanguageID { get; set; }
        public Guid ServiceLanguageID { get; set; }
        public string? LanguageName { get; set; }
        public string ProductName { get; set; }
        public string DisplayName { get; set; }
        public string Slug { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
    }
}

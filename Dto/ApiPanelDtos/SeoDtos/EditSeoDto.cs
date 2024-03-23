using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.SeoDtos
{
    public class EditSeoDto
    {
        public Guid SeoID { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKey { get; set; }
        public string MetaDescription { get; set; }
        public string PageLink { get; set; }
        public Guid? RouteId { get; set; }
    }
}

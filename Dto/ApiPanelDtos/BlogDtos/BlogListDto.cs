using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.BlogDtos
{
    public class BlogListDto
    {
        public Guid BlogID { get; set; }
        public string BlogTitle { get; set; }
        public string BlogCategory { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public bool ShowOnFaq { get; set; }
        public bool ShowOnHomepage { get; set; }
    }
}

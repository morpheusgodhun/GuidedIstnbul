using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.CustomPageManagementDto
{
    public class AddCustomPageDto
    {
        public string PageName { get; set; }
        public string DisplayName { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }

        public bool SitemapInclude { get; set; }
    }
}

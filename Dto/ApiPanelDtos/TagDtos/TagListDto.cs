using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.TagManagementDtos
{
    public class TagListDto
    {
        public Guid TagID { get; set; }
        public bool Status { get; set; }
        public string IconPath { get; set; }
        public string TagName { get; set; } 
        public List<string> TagCategories { get; set; }
    }
}

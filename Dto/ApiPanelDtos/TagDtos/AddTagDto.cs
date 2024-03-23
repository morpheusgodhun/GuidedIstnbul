using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.TagManagementDtos
{
    public class AddTagDto
    {
        public List<int> TagCategoryIDs { get; set; }
        public string? IconPath { get; set; }
        public string DisplayName { get; set; }
        public string? Slug { get; set; }
    }
}

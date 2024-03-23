using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.TagManagementDtos
{
    public class EditTagDto
    {
        public Guid TagID { get; set; }
        public string? IconPath { get; set; }
        public List<int> TagCategoryIDs { get; set; }
    }
}

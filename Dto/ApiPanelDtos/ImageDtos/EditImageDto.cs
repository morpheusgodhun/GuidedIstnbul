using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ImageManagementDtos
{
    public class EditImageDto
    {
        public Guid ImageID { get; set; }
        public string? PageName { get; set; }
        public string ImagePath { get; set; }
    }
}

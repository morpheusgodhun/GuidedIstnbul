using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.GeneralDtos
{
    public class EditProductImageDto
    {
        public Guid ProductID { get; set; }
        public Guid ProductImageID { get; set; }
        public string ImagePath { get; set; }
        public int Order { get; set; }
    }
}

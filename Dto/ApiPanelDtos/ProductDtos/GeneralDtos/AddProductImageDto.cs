using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.GeneralDtos
{
    public class AddProductImageDto
    {
        public Guid ProductID { get; set; }
        public int Order { get; set; }
        public string ImagePath { get; set; }
    }
}

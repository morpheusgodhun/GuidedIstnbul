using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.GeneralDtos
{
    public class ProductImageDto
    {
        public Guid ImageID { get; set; }
        public string ImagePath { get; set; }
        public int Order { get; set; }
    }
}

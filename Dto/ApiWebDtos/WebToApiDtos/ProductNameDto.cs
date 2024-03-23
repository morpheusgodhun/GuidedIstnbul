using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos
{
    public class ProductNameDto
    {
        public string ProductName { get; set; }
        public bool IsTour { get; set; }
        public Guid Id { get; set; }
    }
}

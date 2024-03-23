using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.GeneralDtos
{
    public class CategoryDto
    {
        public Guid? ID { get; set; }
        public string? Name { get; set; }
        public int? Order { get; set; }
        public string? Slug { get; set; }
    }
}

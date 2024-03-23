using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos
{
    public class BlogsWithNameAndIdDto
    {
        public string BlogTitle { get; set; }
        public Guid BlogId { get; set; }
        public string Slug { get; set; }
    }
}

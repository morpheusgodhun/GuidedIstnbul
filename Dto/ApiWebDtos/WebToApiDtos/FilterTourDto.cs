using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos
{
    public class FilterTourDto
    {
        public List<Guid> Destination { get; set; }
        public List<Guid> Month { get; set; }
        public List<Guid> TourType { get; set; }
        public List<Guid> Category { get; set; }
        public List<Guid> Special { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos
{
    public class FindTourPostDto
    {
        public string WhereToCityID { get; set; }
        public string MountID { get; set; }
        public string TourTypeID { get; set; }
    }
}

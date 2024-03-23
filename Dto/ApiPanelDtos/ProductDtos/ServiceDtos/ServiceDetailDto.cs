using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.ServiceDtos
{
    public class ServiceDetailDto
    {
        public string ProductName { get; set; }
        public string DisplayName { get; set; }
        public string Slug { get; set; }
        public string YoutubeLink { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; }
        public string CancellationPolicy { get; set; }
        public string CutOffTime { get; set; }
        public bool IsChildPolicyActive { get; set; }

    }
}

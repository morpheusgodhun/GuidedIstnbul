using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.TourDtos
{
    public class TourDetailDto
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public string OperatorAgent { get; set; }
        public bool Status { get; set; }
        public bool IsPerPerson { get; set; }
        public bool IsPerDay { get; set; }
        public string Type { get; set; }
        public string Segment { get; set; }
        public string StartCity { get; set; }
        public string SuggestedStartTime { get; set; }
        public List<string> Destinations { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Inclusions { get; set; }
        public List<string> Exclusions { get; set; }
        public List<string> SightsToSee { get; set; }
        public List<Guid> PostedRoleTemplateIds { get; set; } //turun gözüktüğü roller
        public string Duration { get; set; }
        public string SelectableDurations { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public string CancellationPolicy { get; set; }
        public List<string> Tags { get; set; }
        public string CutOffTime { get; set; }
    }
}

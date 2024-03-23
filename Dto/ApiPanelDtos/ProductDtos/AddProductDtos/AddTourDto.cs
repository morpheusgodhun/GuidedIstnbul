using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.AddProductDtos
{
    public class AddTourDto
    {
        public Guid ProductID { get; set; }
        public Guid? TourTypeID { get; set; }
        public Guid? SegmentID { get; set; }
        public int? StartCityID { get; set; }
        public List<Guid>? DestinationIDs { get; set; }
        public List<Guid>? TourCategoryIDs { get; set; }
        public List<int>? StartTimeIDs { get; set; }
        public int SuggestedStartTimeID { get; set; }
        public int? Duration { get; set; }
        public List<int>? SelectableDurations { get; set; }
        public string? DurationText { get; set; }
        public string? StartPoint { get; set; }
        public string? EndPoint { get; set; }
        public List<Guid>? InclusionsIDs { get; set; }
        public List<Guid>? ExclusionsIDs { get; set; }
        public List<Guid>? SightsToSeeIDs { get; set; }
        public bool? IsPerPerson { get; set; }
        public bool? IsPerDay { get; set; }
        public string? Content { get; set; }
        public List<AddAdditionalServiceToProductDto>? AdditionalServices { get; set; }
        public bool IsPersonLimited { get; set; }
        public int PersonLimit { get; set; }
    }
}

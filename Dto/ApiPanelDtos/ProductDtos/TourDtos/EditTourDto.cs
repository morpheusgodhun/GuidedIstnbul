using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.TourDtos
{
    public class EditTourDto
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public Guid? OperatorAgentID { get; set; }
        public string? YoutubeLink { get; set; }
        public string BannerImagePath { get; set; }
        public string CardImagePath { get; set; }
        public Guid CancellationPolicyID { get; set; }
        public List<Guid> TagIDs { get; set; }
        public int CutOffDay { get; set; }
        public int CutOffHour { get; set; }
        public decimal CustomerDepositoAmount { get; set; }
        public decimal AgentDepositoAmount { get; set; }
        public int DayOfPayment { get; set; }
        public int MinimumPayoutPercent { get; set; }
        public bool IsChildPolicyActive { get; set; }
        public Guid TourTypeID { get; set; }
        public Guid SegmentID { get; set; }
        public int StartingCityID { get; set; }
        public List<Guid> DestinationIDs { get; set; }
        public List<Guid> TourCategoryIDs { get; set; }
        public List<int> StartTimeIDs { get; set; }
        public List<int> SelectableDurations { get; set; }
        public int SuggestedStartTimeID { get; set; }
        public int Duration { get; set; }
        public List<Guid> InclusionIDs { get; set; }
        public List<Guid> ExclusionIDs { get; set; }
        public List<Guid> SightToSeeIDs { get; set; }
        public bool IsPerPerson { get; set; }
        public bool IsPerDay { get; set; }
        public int Order { get; set; }
        public bool IsPersonLimited { get; set; }
        public int PersonLimit { get; set; }
    }
}

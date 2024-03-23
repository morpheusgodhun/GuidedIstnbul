using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.AddProductDtos
{
    public class AddProductDto
    {
        public Guid OperatorAgentID { get; set; } //TODO: Buna bakılabilir kullanılıyormu
        public string ProductName { get; set; }
        public string DisplayName { get; set; }
        public string Slug { get; set; }
        public string? YoutubeLink { get; set; }
        public string BannerImagePath { get; set; }
        public string CardImagePath { get; set; }
        public int Order { get; set; }
        public Guid CancellationPolicyID { get; set; }
        public List<Guid> TagIDs { get; set; }
        public int CutOffDay { get; set; }
        public int CutOffHour { get; set; }
        public decimal CustomerDepositoAmount { get; set; }
        public decimal AgentDepositoAmount { get; set; }
        public int DayOfPayment { get; set; }
        public bool ShowOnHomepage { get; set; }
        public int MinimumPayoutPercent { get; set; }
        public bool ActivateChildPolicy { get; set; }
        public bool IsProductTour { get; set; }
        public Guid? RequestId { get; set; } //varsa CustomTourRequestId 

    }
}

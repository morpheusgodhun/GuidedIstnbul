using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.ProductDtos.ServiceDtos
{
    public class EditServiceDto
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public string YoutubeLink { get; set; }
        public string BannerImagePath { get; set; }
        public string CardImagePath { get; set; }
        public Guid CancellationPolicyID { get; set; }
        public List<Guid> TagIDs { get; set; }
        public int CutOfDay { get; set; }
        public int CutOfHour { get; set; }
        public decimal CustomerDeposito { get; set; }
        public decimal AgentDeposito { get; set; }
        public int DayOfPayment { get; set; }
        public decimal MinimumPayoutPercent { get; set; }
        public bool IsChildPolicyActive { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.CustomTourDtos
{
    public class AnswerOfferDto
    {
        public bool Confirmed { get; set; }
        public Guid OfferId { get; set; }
        public string Answer { get; set; }
    }
}

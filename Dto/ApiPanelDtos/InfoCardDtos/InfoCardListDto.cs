using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.InfoCardDtos
{
    public class InfoCardListDto
    {
        public Guid InfoCardId { get; set; }
        public bool Status { get; set; }
        public string IconPath { get; set; }
        public string Title { get; set; }
    }
}

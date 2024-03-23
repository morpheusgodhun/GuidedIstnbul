using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.InfoCardDtos
{
    public class EditInfoCardDto
    {
        public Guid InfoCardID { get; set; }
        public string IconPath { get; set; }
        public string? Title { get; set; }
    }
}

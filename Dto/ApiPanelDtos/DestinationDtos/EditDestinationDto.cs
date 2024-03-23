using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.DestinationDtos
{
    public class EditDestinationDto
    {
        public Guid DestinationID { get; set; }
        public int Order { get; set; }
        [ValidateNever]
        public string? DestinationName { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.AdditionalServiceDtos
{
    public class EditAdditionalServiceOptionDto
    {
        public Guid OptionID { get; set; }
        public string OptionName { get; set; }
        public int Order { get; set; }
        public List<Guid> FormInputIDs { get; set; }

        [ValidateNever]
        public string? AdditionalServiceId { get; set; }
    }
}

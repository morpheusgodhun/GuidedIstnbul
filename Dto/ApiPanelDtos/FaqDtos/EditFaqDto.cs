using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.FaqManagementDtos
{
    public class EditFaqDto
    {
        public Guid FaqID { get; set; }
        public int Order { get; set; }
        public Guid FaqCategoryID { get; set; }
        [ValidateNever]
        public string? FaqQuestion { get; set; }
    }
}

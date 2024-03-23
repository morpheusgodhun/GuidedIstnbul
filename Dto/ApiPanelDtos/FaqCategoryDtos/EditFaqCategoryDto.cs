using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.FaqCategoryDtos
{
    public class EditFaqCategoryDto
    {
        public Guid FaqCategoryID { get; set; }
        public int Order { get; set; }
        [ValidateNever]
        public string? FaqCategoryName { get; set; }
    }
}

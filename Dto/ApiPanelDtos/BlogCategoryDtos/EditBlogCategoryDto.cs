using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.BlogCategoryDtos
{
    public class EditBlogCategoryDto
    {
        public Guid BlogCategoryID { get; set; }
        public int Order { get; set; }
        [ValidateNever]
        public string? BlogCategoryName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.CommentDtos
{
    public class AddCommentDto
    {
        public int CountryID { get; set; }
        public string FullName { get; set; }
        public string Comment { get; set; }
        public string ImagePath { get; set; }
    }
}

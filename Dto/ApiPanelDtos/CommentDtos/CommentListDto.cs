using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.CommentDtos
{
    public class CommentListDto
    {
        public Guid CommentID { get; set; }
        public bool Status { get; set; }
        public string Country { get; set; }
        public string FullName { get; set; }
        public string Comment { get; set; }
    }

    


}

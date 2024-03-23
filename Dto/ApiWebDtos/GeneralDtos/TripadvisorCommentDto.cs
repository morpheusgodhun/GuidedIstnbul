using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.GeneralDtos
{
    public class TripadvisorCommentDto
    {
        public string CommentID { get; set; }
        public string SenderName { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public int Order { get; set; }
    }
}

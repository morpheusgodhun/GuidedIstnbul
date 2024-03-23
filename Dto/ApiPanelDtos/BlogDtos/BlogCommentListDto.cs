using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.BlogDtos
{
    public class BlogCommentListDto
    {
        public string BlogID { get; set; }
        public string BlogCommentID { get; set; }
        public bool Status { get; set; }
        public DateTime SendDate { get; set; }
        public string? ToAnswer { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Message { get; set; }
        public string ProfilePhotoPath { get; set; }

    }
}

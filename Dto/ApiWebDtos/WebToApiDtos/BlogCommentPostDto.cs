using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos
{
    public class BlogCommentPostDto
    {
        public Guid BlogID { get; set; }
        public Guid UserID { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        //public string ProfilePhotoPath { get; set; } TODO: sonr açılacak. resim null geldiği için kapatıldı
        public Guid? AnsweredBlogCommentId { get; set; }
        public string CommentContent { get; set; }
        //public DateTime ReviewDate { get; set; }
    }
}

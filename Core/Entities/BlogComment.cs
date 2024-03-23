using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BlogComment : BaseEntity
    {
        public string NameSurname { get; set; }
        public string ProfilePhotoPath { get; set; }
        public Guid? AnsweredBlogCommentId { get; set; }
        public string Email { get; set; }
        public string CommentContent { get; set; }
        public DateTime ReviewDate { get; set; }

        [ForeignKey("Blog")]
        public Guid BlogId { get; set; }
        public Blog Blog { get; set; }

        
    }
}

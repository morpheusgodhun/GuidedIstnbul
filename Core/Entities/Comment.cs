using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Comment : BaseEntity
    {
        public string SenderName { get; set; }
        public string SenderImagePath { get; set; }
        public string CommentContent { get; set; }
        public int CountryID { get; set; }



    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ContactMessage : BaseEntity
    {
        public string SenderName { get; set; }
        public string? SenderPhoneNumber { get; set; }
        public string Email { get; set; }
        public int CountryID { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime SendingDate { get; set; }

        //SystemOptionID

        [ForeignKey("SystemOption")]
        public Guid SystemOptionID { get; set; }
        public SystemOption SystemOption { get; set; }
    }
}

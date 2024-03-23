using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class UserExtensionMail : BaseEntity
    {
        public Guid UserId { get; set; }
        public int MailTemplateType { get; set; } //SendMailTemplateName enum
        public Guid UrlCode { get; set; }
        public string? ResetCode { get; set; }
        public DateTime ExpireDate { get; set; }
        public Guid? ReservationId { get; set; }
        public decimal? Amount { get; set; }
        public string? Email { get; set; }
    }
}

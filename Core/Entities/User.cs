using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

        [ForeignKey("RoleTemplate")]
        public Guid RoleTemplateId { get; set; }
        public RoleTemplate RoleTemplate { get; set; }

        //TripadvisorComment
        public ICollection<TripadvisorComment> TripadvisorComments { get; set; }

        //TripadvisorComment (Tour-Guide) Many-To-Many
        public ICollection<Many_TripadvisorComment_TourGuide> Many_TripadvisorComment_TourGuides { get; set; }

        public ICollection<CustomTourRequest> CustomTourRequests { get;set; }

        public Guid? AgentId { get; set; }
        public string? Phone { get; set; }
        public Guid? GuideId { get; set; }
    }
}

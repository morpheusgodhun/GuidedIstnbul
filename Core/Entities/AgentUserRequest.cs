using Dto.ApiPanelDtos.AgentDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AgentUserRequest : BaseEntity
    {
        [ForeignKey("Agent")]
        public Guid AgentId { get; set; } //kullanıcıyı eklemek isteyen agent id
        public Agent Agent { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; } //agent tarafından eklenmek istenen user id
        public User User { get; set; }
        public Guid? RoleTemplateId { get; set; } //agent tarafından kullanıcıya atanmak istenen roleTemplate id
        public int ApproveStatus { get; set; }
    }
}

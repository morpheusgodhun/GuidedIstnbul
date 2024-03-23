using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.WebToApiDtos.Reservation
{
    //rez oluşurken 
    public class RegisterReservationUser
    {
        public Guid? UserId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid? RoleTemplate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserForLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? MemberId { get; set; }
        public string Namer { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string? Token { get; set; }
        public string? Message { get; set; }
        public List<string> MemberType { get; set; } = new();
    }
}

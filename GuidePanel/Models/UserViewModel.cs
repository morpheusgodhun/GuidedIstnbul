namespace GuidePanel.Models
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; } /* Member Guide AgentAdmin AgentMember */
        public List<string> Permission { get; set; }

        //public string AgentId { get; set; } 
    }
}

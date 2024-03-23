using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Core.Entities
{
    public class Agent : BaseEntity
    {
        public string AgentTitle { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public int CountryID { get; set; }
        public Guid? CityID { get; set; }
        public string TaxAdministration { get; set; }
        public string TaxNumber { get; set; }
        public string CompanyManager { get; set; }
        public string TradeRegistryNumber { get; set; }
        public string WebsiteLink { get; set; }
        public string Address { get; set; }
        public string LogoPath { get; set; }
        public int ApproveStatus { get; set; }
        public int ServicesDiscountRate { get; set; }
        public int DefaultTourDiscount { get; set; }
        public bool WithoutPay { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
    }
}

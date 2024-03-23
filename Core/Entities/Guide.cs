using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Guide:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<GuideLanguage> GuideLanguages { get; set; }
        public List<GuideCity> GuideCities { get; set; }
        public string ProfilPhotoPath { get; set; }
        public string LicenseFrontImagePath { get; set; }
        public string LicenseBackImagePath { get; set; }
        public DateTime BirthDate { get; set; }
        public string TurebLicenseNumber { get; set; }
        public string Tc { get; set; }
        public string RegisteredDirectoryRoom { get; set; }
        public bool ApproveConstent { get; set; }
        public int ApproveStatus { get; set; }
    }
}

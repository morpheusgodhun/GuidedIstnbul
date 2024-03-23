using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Agent
{
    public class AddAgentGuideUserDto
    {
        public Guid? Agent { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Guid> LanguageKnowIDs { get; set; }
        public List<Guid> SpecializeCityIDs { get; set; }
        public string? ProfilPhotoPath { get; set; }
        public string? LicenseFrontImagePath { get; set; }
        public string? LicenseBackImagePath { get; set; }
        public DateTime BirthDate { get; set; }
        public string TursebLicenseNumber { get; set; }
        public string Tc { get; set; }
        public string RegisteredDirectoryRoom { get; set; }
    }
}

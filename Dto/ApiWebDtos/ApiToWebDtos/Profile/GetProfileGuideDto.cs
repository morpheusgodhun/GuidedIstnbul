
using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiWebDtos.ApiToWebDtos.Profile
{
    public class GetProfileGuideDto
    {
        public List<ConstantValueDto> ConstantValues { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<SelectListOptionDto> LanguageKnowOptions { get; set; }
        public List<string> LanguageKnowIDs { get; set; }
        public List<SelectListOptionDto> SpecializeCityOptions { get; set; }
        public List<string> SpecializeCityIDs { get; set; }
        public string ProfilPhotoPath { get; set; }
        public string LicenseFrontImagePath { get; set; }
        public string LicenseBackImagePath { get; set; }
        public DateTime DateBirth { get; set; }
        public string TurebLicense { get; set; }
        public string Tc { get; set; }
        public string RegisteredDirectoryRoom { get; set; }
    }
}

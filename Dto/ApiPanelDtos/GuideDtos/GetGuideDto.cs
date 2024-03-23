using Dto.ApiPanelDtos.GuideDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos
{
    public class GetGuideDto
    {
        public List<GetGuideCityNameDto> CityNames { get; set; }
        public List<GetLanguageNameDto> LanguageNames { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<GetGuideLanguageDto> GuideLanguages { get; set; }
        public List<GetGuideCityDto> GuideCities { get; set; }
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

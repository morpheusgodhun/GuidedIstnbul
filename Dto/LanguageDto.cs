using Dto.ApiPanelDtos.AdditionalServiceDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LanguageDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? LanguageCode { get; set; }
        public string? IconPath { get; set; }
    }
    public class ChangeLanguagePostDto
    {
        public int LanguageId { get; set; }
    }
}

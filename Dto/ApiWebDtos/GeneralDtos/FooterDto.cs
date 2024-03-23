using Dto.ApiWebDtos.ApiToWebDtos.GeneralPages;
using Dto.ApiWebDtos.ApiToWebDtos.Tour;
using Dto.ApiWebDtos.WebToApiDtos;

namespace Dto.ApiWebDtos.GeneralDtos
{
    public class FooterDto
    {
        public string? Slug { get; set; }
        public Dictionary<string,string> SlugDict { get; set; } = new Dictionary<string,string>();
        public List<ConstantValueDto> ConstantValues { get; set; }
    }
}

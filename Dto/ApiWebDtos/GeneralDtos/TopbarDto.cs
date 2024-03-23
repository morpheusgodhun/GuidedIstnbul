namespace Dto.ApiWebDtos.GeneralDtos
{
    public class TopbarDto
    {
        public Dictionary<string, string> SlugDict { get; set; } = new Dictionary<string, string>();
        public List<ConstantValueDto> ConstantValues { get; set; }
    }
}

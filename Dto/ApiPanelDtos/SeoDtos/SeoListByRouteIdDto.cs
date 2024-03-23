namespace Dto.ApiPanelDtos.SeoDtos
{
    public class SeoListByRouteIdDto
    {
        public string MetaTitle { get; set; }
        public string MetaKey { get; set; }
        public string MetaDescription { get; set; }
        public string Link { get; set; }
        public Guid? RouteId { get; set; }
    }
}

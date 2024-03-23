namespace Dto.ApiPanelDtos.OperationDtos
{
    public class OperationGuideDto
    {
        public string OperationGuideId { get; set; }
        public string Guide { get; set; }
        public int Day { get; set; }
        public DateTime? Date { get; set; }
        public string GuideStatus { get; set; }
    }
}

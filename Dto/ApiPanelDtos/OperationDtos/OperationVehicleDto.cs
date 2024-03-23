namespace Dto.ApiPanelDtos.OperationDtos
{
    public class OperationVehicleDto
    {
        public string OperationVehicleId { get; set; }
        public string Vehicle { get; set; }
        public int Day { get; set; }
        public DateTime? Date { get; set; }
        public string VehicleStatus { get; set; }
    }
}

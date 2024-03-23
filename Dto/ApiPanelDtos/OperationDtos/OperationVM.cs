namespace Dto.ApiPanelDtos.OperationDtos
{
    public class OperationVM //daily olmalı
    {
        public string OperationId { get; set; }
        public string ReservationCode { get; set; }
        public string ReservationId { get; set; }
        public string ProductName { get; set; }
        public string ProductImagePath { get; set; }
        public string CustomerName { get; set; }
        public string OperationNote { get; set; }
        public int NumberOfParticipants { get; set; }
        public string Hotel { get; set; }
        public string PickUpPoint { get; set; }
        public int Day { get; set; } //kaçıncı günün işleri
        public DateTime Date { get; set; } //günün tarihi - operasyon tarihi vs değil & operationStartDate üstüne eklenecek
        public OperationVehicleDto? OperationVehicle { get; set; }
        public OperationGuideDto? OperationGuide { get; set; }
        public OperationAdditionalServiceDto? OperationAdditionalService { get; set; }
        public OperationNoteDto? OperationNotes { get; set; }
        public OperationShopDto OperationShop { get; set; }
        public OperationTicketDto OperationTicket { get; set; }
    }

}

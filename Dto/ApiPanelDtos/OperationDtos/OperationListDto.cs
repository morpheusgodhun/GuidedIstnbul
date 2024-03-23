using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ApiPanelDtos.OperationDtos
{
    public class OperationListDto
    {
        public string OperationId { get; set; }
        public string ReservationCode { get; set; }
        public string ReservationId { get; set; }
        public DateTime OperationStartDate { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public string OperationNote { get; set; }
        public int NumberOfParticipants { get; set; }
        public string Hotel { get; set; }
        //public int OperationTotalDay { get; set; }
        public string PickUpPoint { get; set; }
        public string ProductImagePath { get; set; }
        public List<OperationVehicleDto> OperationVehicles { get; set; }
        public List<OperationGuideDto> OperationGuides { get; set; }
        public List<OperationAdditionalServiceDto> OperationAdditionalServices { get; set; }
        public List<OperationNoteDto> OperationNotes { get; set; }
        public List<OperationShopDto> OperationShops { get; set; }
        public List<OperationTicketDto> OperationTickets { get; set; }
        //ticket
        //shop
        //notes
    }
    public class OperationListDto2
    {
        public int IsTipi { get; set; } //1 guide 2 driver 3 additional
        public string OperationId { get; set; }
        public string ReservationId { get; set; }
        public string RowId { get; set; }
        public string ReservationCode { get; set; }
        public DateTime? Date { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public string OperationNote { get; set; }
        public int NumberOfParticipants { get; set; }
        public string Hotel { get; set; }
        public string PickUpPoint { get; set; }
        public string GuideName { get; set; }
        public string DriverName { get; set; }
        public string AdditionalName { get; set; }
        public int Day { get; set; }


    }

}

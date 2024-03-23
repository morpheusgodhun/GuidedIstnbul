using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticValues
{
    public class MyReservationStatus
    {
        public static List<SelectListOption> Status = new List<SelectListOption>()
        {
            //new SelectListOption() { ID = 1, Value = "Incompleted Reservation" },
            //new SelectListOption() { ID = 2, Value = "Next Reservation" },
            //new SelectListOption() { ID = 3, Value = "Past Reservation" },
            //new SelectListOption() { ID = 9, Value = "Cancellation Request" },
            ////new SelectListOption() { ID = 4, Value = "In Process" },
            ///
            new SelectListOption() { ID = 1, Value = "Not Completed Reservation - Main Informations" },
            new SelectListOption() { ID = 2, Value = "Not Completed Reservation - Participant Informations" },
            new SelectListOption() { ID = 3, Value = "Not Completed Reservation - Billing Information" },
            new SelectListOption() { ID = 4, Value = "Pending" },
            new SelectListOption() { ID = 5, Value = "Confirmed" },
            new SelectListOption() { ID = 6, Value = "Rejected" },
            new SelectListOption() { ID = 7, Value = "Editted" },
            //new SelectListOption() { ID = 8, Value = "Edit Request" },//
            //new SelectListOption() { ID = 9, Value = "Cancellation Request" },//
            new SelectListOption() { ID = 10, Value = "On Operation" },
            new SelectListOption() { ID = 11, Value = "Completed" },
            new SelectListOption() { ID = 12, Value = "Cancelled" },
            new SelectListOption() { ID = 13, Value = "Complete Without Pay" },
            new SelectListOption() { ID = 14, Value = "Ask For Price" },
        };

        public static string GetValue(int id)
        {
            return Status.FirstOrDefault(x => x.ID == id).Value;
        }

        public enum No
        {
            //IncompletedReservation = 1,
            //NextReservation = 2,
            //PastReservation = 3

            NotCompletedReservationMainInformations = 1,
            NotCompletedReservationParticipantInformations = 2,
            NotCompletedReservationBillingInformation = 3,
            Pending = 4,
            Confirmed = 5,
            Rejected = 6,
            Editted = 7,
            //EditRequest = 8,
            //CancellationRequest = 9,
            OnOperation = 10,
            Completed = 11,
            Cancelled = 12,
            CompleteWithoutPay = 13,
            AskForPrice = 14,
        }
    }
}

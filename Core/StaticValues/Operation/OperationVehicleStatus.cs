using Dto.ApiWebDtos.GeneralDtos;

namespace Core.StaticValues.Operation
{
    public class OperationVehicleStatus
    {
        public List<SelectListOption> Status = new()
        {
            new SelectListOption() { ID = 1, Value = "Waiting" },
            new SelectListOption() { ID = 2, Value = "Confirmed" },
            new SelectListOption() { ID = 3, Value = "Canceled" },
            new SelectListOption() { ID = 4, Value = "Reserved" },
            new SelectListOption() { ID = 5, Value = "Detail Sent" },
            new SelectListOption() { ID = 6, Value = "Not Selected" },
        };
        public static string GetValue(int id)
        {
            return new OperationVehicleStatus().Status.FirstOrDefault(x => x.ID == id).Value;
        }
        public enum No
        {
            Waiting = 1,
            Confirmed = 2,
            Canceled = 3,
            Reserved = 4,
            DetailSent = 5,
            NotSelected = 6,
        }
    }
}

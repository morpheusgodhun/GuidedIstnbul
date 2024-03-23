using Dto.ApiWebDtos.GeneralDtos;

namespace Core.StaticValues.Operation
{
    public class MyOperationStatus
    {
        public List<SelectListOption> Status = new()
        {
            new SelectListOption() { ID = 1, Value = "Waiting" },
            new SelectListOption() { ID = 2, Value = "InOperation" },
            new SelectListOption() { ID = 3, Value = "Completed" },
            new SelectListOption() { ID = 4, Value = "Canceled" },
        };
        public static string GetValue(int id)
        {
            return new MyOperationStatus().Status.FirstOrDefault(x => x.ID == id).Value;
        }

        public enum No
        {
            Waiting = 1,
            InOperation = 2,
            Completed = 3,
            Canceled = 4,
        }
    }
}

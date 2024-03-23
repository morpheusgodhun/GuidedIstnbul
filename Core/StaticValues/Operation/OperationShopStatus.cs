using Dto.ApiWebDtos.GeneralDtos;

namespace Core.StaticValues.Operation
{
    public class OperationShopStatus
    {
        public static List<SelectListOption> Status = new()
        {
            new SelectListOption() { ID = 0, Value = "None" },
            new SelectListOption() { ID = 1, Value = "Directed" },
            new SelectListOption() { ID = 2, Value = "Visited" },
            new SelectListOption() { ID = 3, Value = "Shopped" },
        };
        public static string GetValue(int id)
        {
            return Status.FirstOrDefault(x => x.ID == id).Value;
        }
        public enum No
        {
            None = 0,
            Directed = 1,
            Visited = 2,
            Shopped = 3,
        }
    }
}

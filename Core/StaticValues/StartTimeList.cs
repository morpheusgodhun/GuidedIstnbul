using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticValues
{
    public class StartTimeList
    {
        public static List<SelectListOption> StartTimes { get; set; } = new()
        {
          new () { ID = 1, Value = "00:00" },
          new () { ID = 2, Value = "00:15" },
          new () { ID = 3, Value = "00:30" },
          new () { ID = 4, Value = "00:45" },
          new () { ID = 5, Value = "01:00" },
          new () { ID = 6, Value = "01:15" },
          new () { ID = 7, Value = "01:30" },
          new () { ID = 8, Value = "01:45" },
          new () { ID = 9, Value = "02:00" },
          new () { ID = 10, Value = "02:15" },
          new () { ID = 11, Value = "02:30" },
          new () { ID = 12, Value = "02:45" },
          new () { ID = 13, Value = "03:00" },
          new () { ID = 14, Value = "03:15" },
          new () { ID = 15, Value = "03:30" },
          new () { ID = 16, Value = "03:45" },
          new () { ID = 17, Value = "04:00" },
          new () { ID = 18, Value = "04:15" },
          new () { ID = 19, Value = "04:30" },
          new () { ID = 20, Value = "04:45" },
          new () { ID = 21, Value = "05:00" },
          new () { ID = 22, Value = "05:15" },
          new () { ID = 23, Value = "05:30" },
          new () { ID = 24, Value = "05:45" },
          new () { ID = 25, Value = "06:00" },
          new () { ID = 26, Value = "06:15" },
          new () { ID = 27, Value = "06:30" },
          new () { ID = 28, Value = "06:45" },
          new () { ID = 29, Value = "07:00" },
          new () { ID = 30, Value = "07:15" },
          new () { ID = 31, Value = "07:30" },
          new () { ID = 32, Value = "07:45" },
          new () { ID = 33, Value = "08:00" },
          new () { ID = 34, Value = "08:15" },
          new () { ID = 35, Value = "08:30" },
          new () { ID = 36, Value = "08:45" },
          new () { ID = 37, Value = "09:00" },
          new () { ID = 38, Value = "09:15" },
          new () { ID = 39, Value = "09:30" },
          new () { ID = 40, Value = "09:45" },
          new () { ID = 41, Value = "10:00" },
          new () { ID = 42, Value = "10:15" },
          new () { ID = 43, Value = "10:30" },
          new () { ID = 44, Value = "10:45" },
          new () { ID = 45, Value = "11:00" },
          new () { ID = 46, Value = "11:15" },
          new () { ID = 47, Value = "11:30" },
          new () { ID = 48, Value = "11:45" },
          new () { ID = 49, Value = "12:00" },
          new () { ID = 50, Value = "12:15" },
          new () { ID = 51, Value = "12:30" },
          new () { ID = 52, Value = "12:45" },
          new () { ID = 53, Value = "13:00" },
          new () { ID = 54, Value = "13:15" },
          new () { ID = 55, Value = "13:30" },
          new () { ID = 56, Value = "13:45" },
          new () { ID = 57, Value = "14:00" },
          new () { ID = 58, Value = "14:15" },
          new () { ID = 59, Value = "14:30" },
          new () { ID = 60, Value = "14:45" },
          new () { ID = 61, Value = "15:00" },
          new () { ID = 62, Value = "15:15" },
          new () { ID = 63, Value = "15:30" },
          new () { ID = 64, Value = "15:45" },
          new () { ID = 65, Value = "16:00" },
          new () { ID = 66, Value = "16:15" },
          new () { ID = 67, Value = "16:30" },
          new () { ID = 68, Value = "16:45" },
          new () { ID = 69, Value = "17:00" },
          new () { ID = 70, Value = "17:15" },
          new () { ID = 71, Value = "17:30" },
          new () { ID = 72, Value = "17:45" },
          new () { ID = 73, Value = "18:00" },
          new () { ID = 74, Value = "18:15" },
          new () { ID = 75, Value = "18:30" },
          new () { ID = 76, Value = "18:45" },
          new () { ID = 77, Value = "19:00" },
          new () { ID = 78, Value = "19:15" },
          new () { ID = 79, Value = "19:30" },
          new () { ID = 80, Value = "19:45" },
          new () { ID = 81, Value = "20:00" },
          new () { ID = 82, Value = "20:15" },
          new () { ID = 83, Value = "20:30" },
          new () { ID = 84, Value = "20:45" },
          new () { ID = 85, Value = "21:00" },
          new () { ID = 86, Value = "21:15" },
          new () { ID = 87, Value = "21:30" },
          new () { ID = 88, Value = "21:45" },
          new () { ID = 89, Value = "22:00" },
          new () { ID = 90, Value = "22:15" },
          new () { ID = 91, Value = "22:30" },
          new () { ID = 92, Value = "22:45" },
          new () { ID = 93, Value = "23:00" },
          new () { ID = 94, Value = "23:15" },
          new () { ID = 95, Value = "23:30" },
          new () { ID = 96, Value = "23:45" },
          new () { ID = 97, Value = "00:00" },
};

        public static string GetValue(int id)
        {
            return StartTimes.FirstOrDefault(x => x.ID == id).Value;
        }
        public static int GetId(string value)
        {
            return StartTimes.FirstOrDefault(x => x.Value == value).ID;
        }
    }
    public class OldStartTimes
    {
        public static List<SelectListOption> StartTimes = new()
            {
                new SelectListOption()
                {
                    ID = 1,
                    Value = "07:00"
                },
                new SelectListOption()
                {
                    ID = 2,
                    Value = "07:30"
                },
                new SelectListOption()
                {
                    ID = 3,
                    Value = "08:00"
                },
                new SelectListOption()
                {
                    ID = 4,
                    Value = "08:30"
                },
                new SelectListOption()
                {
                    ID = 5,
                    Value = "09:00"
                },
                new SelectListOption()
                {
                    ID = 6,
                    Value = "09:30"
                },
                new SelectListOption()
                {
                    ID = 7,
                    Value = "10:00"
                },
                new SelectListOption()
                {
                    ID = 8,
                    Value = "10:30"
                },
                new SelectListOption()
                {
                    ID = 9,
                    Value = "11:00"
                },
                new SelectListOption()
                {
                    ID = 10,
                    Value = "11:30"
                },
                new SelectListOption()
                {
                    ID = 11,
                    Value = "12:00"
                },
                new SelectListOption()
                {
                    ID = 12,
                    Value = "12:30"
                },
                new SelectListOption()
                {
                    ID = 13,
                    Value = "13:00"
                },
                new SelectListOption()
                {
                    ID = 14,
                    Value = "13:30"
                },
                new SelectListOption()
                {
                    ID = 15,
                    Value = "14:00"
                },
                new SelectListOption()
                {
                    ID = 16,
                    Value = "14:30"
                },
                new SelectListOption()
                {
                    ID = 17,
                    Value = "15:00"
                },
                new SelectListOption()
                {
                    ID = 18,
                    Value = "15:30"
                },
                new SelectListOption()
                {
                    ID = 19,
                    Value = "16:00"
                },
                new SelectListOption()
                {
                    ID = 20,
                    Value = "16:30"
                },
                new SelectListOption()
                {
                    ID = 21,
                    Value = "17:00"
                },
                new SelectListOption()
                {
                    ID = 22,
                    Value = "17:30"
                },
                new SelectListOption()
                {
                    ID = 23,
                    Value = "18:00"
                },
                new SelectListOption()
                {
                    ID = 24,
                    Value = "18:30"
                },
                new SelectListOption()
                {
                    ID = 25,
                    Value = "19:00"
                },
                new SelectListOption()
                {
                    ID = 26,
                    Value = "19:30"
                },
                new SelectListOption()
                {
                    ID = 27,
                    Value = "20:00"
                },
                new SelectListOption()
                {
                    ID = 28,
                    Value = "20:30"
                },
                new SelectListOption()
                {
                    ID = 29,
                    Value = "21:00"
                },
                new SelectListOption()
                {
                    ID = 30,
                    Value = "21:30"
                }
            };

        public static string GetValue(int id)
        {
            return StartTimes.FirstOrDefault(x => x.ID == id).Value;
        }
        public static int GetId(string value)
        {
            return StartTimes.FirstOrDefault(x => x.Value == value).ID;
        }
    }
}

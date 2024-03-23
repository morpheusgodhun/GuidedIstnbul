using Dto.ApiWebDtos.GeneralDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticValues
{
    public class LanguageSelectList
    {
        public List<SelectListOption> Languages = new List<SelectListOption>()
        {
            new SelectListOption() { ID = 1, Value = "Türkçe" },
            new SelectListOption() { ID = 2, Value = "English" },
            new SelectListOption() { ID = 3, Value = "Spanish" },
            new SelectListOption() { ID = 4, Value = "Arabic" },
        };

        public string GetValue(int id)
        {
            return new LanguageSelectList().Languages.FirstOrDefault(x => x.ID == id).Value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StaticClass
{
    public static class LanguageList
    {
        public static Language BaseLanguage = new Language()
        {
            Id = 1,
            Name = "English",
            Order = 1,
            UrlPrefix = "en"

        };

        public static List<Language> AllLanguages = new List<Language>()
        {
            new ()
            {
                Id = 1,
                Name = "English",
                Order = 1,
                UrlPrefix = "en"
            },
            new ()
            {
                Id = 2,
                Name = "Turkish",
                Order = 2,
                UrlPrefix = "tr"
            },
            //new ()
            //{
            //    Id = 3,
            //    Name = "Deutsch",
            //    Order = 3,
            //    UrlPrefix = "de"
            //},
            //new ()
            //{
            //    Id = 4,
            //    Name = "France",
            //    Order = 4,
            //    UrlPrefix = "fr"
            //},
        };

        public static string GetPrefix(int languageId)
        {
            return AllLanguages.FirstOrDefault(x => x.Id == languageId)?.UrlPrefix;
        }
        public static Language GetLangaugeByPrefix(string prefix)
        {
            return AllLanguages.FirstOrDefault(x => x.UrlPrefix == prefix);
        }

        public enum No
        {
            English = 1,
            Turkish = 2,
            //Deutsch = 3,
            //French = 4
        }
    }

    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string UrlPrefix { get; set; }
    }

}

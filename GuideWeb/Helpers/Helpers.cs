using Core.StaticClass;
using Dto.ApiWebDtos.GeneralDtos;
using System.Globalization;

namespace GuideWeb.Helpers
{
    public class Helpers
    {
        //write constant value
        public static string Wcv(List<ConstantValueDto> ConstantValues, string key)
        {
            var value = ConstantValues.FirstOrDefault(x => x.Key == key)?.Value ?? key;
            return value;
        }

        public static string Wprice(decimal price, string cultureInfo = "en-US")
        {
            var value = string.Format(new CultureInfo(cultureInfo), "{0:C}", price);
            return value;
        }

        public static string PrepareUrl(string languagePrefix, string slug)
        {
            string url = "/" + languagePrefix + "/" + slug;
            return url;
        }
    }
}

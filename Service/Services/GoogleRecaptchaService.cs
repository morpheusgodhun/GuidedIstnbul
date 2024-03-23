using Core.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Service.Services
{
    public class GoogleRecaptchaService : IGoogleRecaptchaService
    {
        //private readonly GoogleRecaptchaConfiguration _recaptchaConfig;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        public GoogleRecaptchaService(IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            // _recaptchaConfig = recaptchaConfig;
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }

        public async Task<bool> CheckCaptcha()
        {
            //string siteKey = _recaptchaConfig.SiteKey;
            //string secretKey = _recaptchaConfig.SecretKey;
            var secretKey = _configuration.GetSection("GoogleRecaptcha:SecretKey").Value;

            var postData = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("secret", secretKey),
                new KeyValuePair<string, string>("remoteip", _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()),
                new KeyValuePair<string, string>("response", _contextAccessor.HttpContext.Request.Form["g-recaptcha-response"])
            };

            var client = new HttpClient();
            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(postData));

            var o = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

         

            return (bool)o["success"];




        }

        public async Task<JObject> CheckCaptcha2()
        {
            var secretKey = _configuration.GetSection("GoogleRecaptcha:SecretKey").Value;

            var postData = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("secret", secretKey),
                new KeyValuePair<string, string>("remoteip", _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()),
                new KeyValuePair<string, string>("response", _contextAccessor.HttpContext.Request.Form["g-recaptcha-response"])
            };

            var client = new HttpClient();
            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(postData));

            var result = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            return result;
            //return (bool)o["success"];
        }
    }
    public class GoogleRecaptchaConfiguration
    {
        public string SiteKey { get; set; }
        public string SecretKey { get; set; }
    }
}

using Newtonsoft.Json.Linq;

namespace Core.IService
{
    public interface IGoogleRecaptchaService  
    {
        Task<bool> CheckCaptcha();
        Task<JObject> CheckCaptcha2();
    }
}

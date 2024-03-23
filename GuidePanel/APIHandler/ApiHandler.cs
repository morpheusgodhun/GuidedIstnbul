using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace GuidePanel.APIHandler
{
    public class ApiHandler : IApiHandler
    {

        //private readonly IHttpContextAccessor _contextAccessor;

        //public ApiHandler(IHttpContextAccessor contextAccessor)
        //{
        //    _contextAccessor = contextAccessor;
        //}

        public string GetToken()
        {
            //var token = _contextAccessor.HttpContext.Request.Cookies["security-token"];
            //if (token is null)
            //{
            //    return "";
            //}
            //return token;
            return "";
        }

        public T GetApi<T>(string url)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";
            httpRequest.ContentType = "application/json";
            //httpRequest.Headers.Add("Authorization", "Bearer ";
            var response = httpRequest.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                var model = JsonConvert.DeserializeObject<T>(result);
                response.Close();
                return model;
            }
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";
            httpRequest.ContentType = "application/json";
            var response = await httpRequest.GetResponseAsync();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = await reader.ReadToEndAsync();
                var model = JsonConvert.DeserializeObject<T>(result);
                response.Close();
                return model;
            }
        }
        public T PostApi<T>(dynamic dynamicModel, string Url)
        {

            var token = GetToken();
            var httpRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpRequest.Method = "POST";
            //httpRequest.Timeout = 100000;
            httpRequest.ContentType = "application/json";
            httpRequest.Headers.Add("Authorization", "Bearer " + token);
            string JsonData = JsonConvert.SerializeObject(dynamicModel);
            byte[] byteArray = Encoding.UTF8.GetBytes(JsonData);
            httpRequest.ContentLength = byteArray.Length;
            using (var stream = httpRequest.GetRequestStream())
            {
                stream.Write(byteArray, 0, byteArray.Length);
                stream.Close();
            }

            var response = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                T model = JsonConvert.DeserializeObject<T>(result);
                response.Close();
                return model;
            }

        }
        public T PostApiV2<T>(dynamic dynamicModel, string Url)
        {
            try
            {
                var token = GetToken();
                var httpRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json";
                httpRequest.Headers.Add("Authorization", "Bearer " + token);
                string JsonData = JsonConvert.SerializeObject(dynamicModel);
                byte[] byteArray = Encoding.UTF8.GetBytes(JsonData);
                httpRequest.ContentLength = byteArray.Length;
                var stream = httpRequest.GetRequestStream();

                stream.Write(byteArray, 0, byteArray.Length);
                stream.Close();

                //bunu niye değiştirdiniz
                var response = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    T model = JsonConvert.DeserializeObject<T>(result);
                    response.Close();
                    return model;
                }
            }
            catch (Exception ex)
            {

                throw new NotImplementedException();
            }
        }

        public string PostApiString(dynamic dynamicModel, string Url)
        {
            try
            {
                var token = GetToken();
                var httpRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json";
                httpRequest.Headers.Add("Authorization", "Bearer " + token);
                string JsonData = JsonConvert.SerializeObject(dynamicModel);
                byte[] byteArray = Encoding.UTF8.GetBytes(JsonData);
                httpRequest.ContentLength = byteArray.Length;
                Stream dataStream = httpRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                var response = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    response.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}

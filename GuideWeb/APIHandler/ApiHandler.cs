using Core.StaticClass;
using GuideWeb.Helpers;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net;
using System.Text;

namespace GuideWeb.APIHandler
{
    public class ApiHandler : IApiHandler
    {
        private readonly ICookie _cookie;

        public ApiHandler(ICookie cookie)
        {
            _cookie = cookie;
        }

        //private readonly IHttpContextAccessor _contextAccessor;

        //public ApiHandler(IHttpContextAccessor contextAccessor)
        //{
        //    _contextAccessor = contextAccessor;
        //}

        public string GetToken()
        {
            var token = _cookie.getJwtCookie();
            return token;
        }

        public T GetApi<T>(string url)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";
            httpRequest.ContentType = "application/" + "json";

            var token = GetToken();
            if (token is not null && !string.IsNullOrEmpty(token) && !string.IsNullOrWhiteSpace(token))
                httpRequest.Headers.Add("Authorization", "Bearer " + token);
            //httpRequest.Headers.Add("Bearer", token);


            var response = httpRequest.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = reader.ReadToEnd();
                var model = JsonConvert.DeserializeObject<T>(result);
                response.Close();
                return model;
            }

        }
        void AttachLanguageToHeader(WebHeaderCollection headers)
        {
            Language language = _cookie.GetLanguage();
            if (language is not null)
                headers.Add("language", language.UrlPrefix);
            else
                headers.Add("language", LanguageList.BaseLanguage.UrlPrefix);
        }
        public async Task<T> GetAsync<T>(string url)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";
            httpRequest.ContentType = "application/json";
            var token = GetToken();

            if (token is not null && !string.IsNullOrEmpty(token) && !string.IsNullOrWhiteSpace(token))
                httpRequest.Headers.Add("Authorization", "Bearer " + token);

            AttachLanguageToHeader(httpRequest.Headers);
            //httpRequest.Headers.Add("Bearer", GetToken());

            var response = await httpRequest.GetResponseAsync();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = await reader.ReadToEndAsync();
                var model = JsonConvert.DeserializeObject<T>(result);
                response.Close();
                return model;
            }
        }
        //çalıştıramadım, bakacam -mehmet ali
        public async Task<string> GetXml(string url)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";
            httpRequest.ContentType = "application/xml";

            var token = GetToken();

            if (token is not null && !string.IsNullOrEmpty(token) && !string.IsNullOrWhiteSpace(token))
                httpRequest.Headers.Add("Authorization", "Bearer " + token);

            var response = await httpRequest.GetResponseAsync();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var result = await reader.ReadToEndAsync();

                response.Close();
                return result;
            }
        }



        public async Task<T> PostAsync<T>(dynamic dynamicModel, string Url)
        {
            try
            {
                var token = GetToken();
                var httpRequest = WebRequest.CreateHttp(Url);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json";
                httpRequest.Headers.Add("Authorization", "Bearer " + token);
                string JsonData = JsonConvert.SerializeObject(dynamicModel);
                byte[] byteArray = Encoding.UTF8.GetBytes(JsonData);
                httpRequest.ContentLength = byteArray.Length;

                using var stream = await httpRequest.GetRequestStreamAsync();
                await stream.WriteAsync(byteArray);
                stream.Close();

                var response = await httpRequest.GetResponseAsync();

                using var streamReader = new StreamReader(response.GetResponseStream());

                var result = await streamReader.ReadToEndAsync();
                T model = JsonConvert.DeserializeObject<T>(result);
                response.Close();
                return model;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                //throw new NotImplementedException();
            }
        }
        public T PostApi<T>(dynamic dynamicModel, string Url)
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                //throw new NotImplementedException();
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

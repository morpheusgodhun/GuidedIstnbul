namespace GuideWeb.APIHandler
{
    public interface IApiHandler
    {
        T GetApi<T>(string url);
        T PostApi<T>(dynamic dynamicModel, string Url);
        string PostApiString(dynamic dynamicModel, string Url);
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(dynamic dynamicModel, string Url);
        Task<string> GetXml(string url);
    }
}

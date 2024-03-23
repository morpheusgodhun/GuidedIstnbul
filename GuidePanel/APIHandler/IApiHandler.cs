﻿namespace GuidePanel.APIHandler
{
    public interface IApiHandler
    {
        T GetApi<T>(string url);
        T PostApi<T>(dynamic dynamicModel, string Url);
        string PostApiString(dynamic dynamicModel, string Url);
    }
}

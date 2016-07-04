using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using RestSharp.Extensions.MonoHttp;
using System.Collections.Specialized;
using System.Linq;

namespace Memoirs.Android.Common
{
    public static class WebApiCaller
    {
        public static TimeSpan Timeout = TimeSpan.FromSeconds(5);
        public static TResponseValue Post<TResponseValue, TRequestValue>(string url, out HttpStatusCode statusCode, TRequestValue requestValue, string authtoken = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.Timeout = Timeout;
                //HttpContent content = new FormUrlEncodedContent(new[]
                //{
                //    new KeyValuePair<string, string>("grant_type", "password"),
                //    new KeyValuePair<string, string>("username", login),
                //    new KeyValuePair<string, string>("password", password),
                //});
                HttpContent content = new StringContent(JsonConvert.SerializeObject(requestValue));
                HttpResponseMessage response = client.PostAsync(url, content).Result;
                statusCode = response.StatusCode;
                TResponseValue jsonResponse = JsonConvert.DeserializeObject<TResponseValue>(response.Content.ReadAsStringAsync().Result);
                return jsonResponse;
            }
        }
        public static TReturnValue Put<TReturnValue, TRequestValue>(string url, TRequestValue patameters, string authtoken = null)
        {
            return default(TReturnValue);
        }

        public static TResponseValue Get<TResponseValue>(string url, out HttpStatusCode statusCode, Dictionary<string, string> parameters, string authtoken = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.Timeout = Timeout;
                
                if (parameters!=null)
                {
                    FormUrlEncodedContent param =
                   new FormUrlEncodedContent(parameters.Select(a => new KeyValuePair<string, string>(a.Key, a.Value)));
                    
                    url= $"{url.Trim('/')}?{param.ReadAsStringAsync().Result}";
                }
                
                if (!string.IsNullOrEmpty(authtoken))
                {
                    client.DefaultRequestHeaders.Add("Authorization", authtoken);
                }
                
                    HttpResponseMessage response = client.GetAsync(url).Result;
                

                statusCode = response.StatusCode;
                var responceString = response.Content.ReadAsStringAsync().Result;
                TResponseValue jsonResponse = JsonConvert.DeserializeObject<TResponseValue>(responceString);
                return jsonResponse;
            }
        }

        /// <summary>
        /// Adds or Updates the specified parameter to the Query String.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramName">Name of the parameter to add.</param>
        /// <param name="paramValue">Value for the parameter to add.</param>
        /// <returns>Url with added parameter.</returns>
        //public static Uri AddOrUpdateParameter(this Uri url, string paramName, string paramValue)
        //{
        //    var uriBuilder = new UriBuilder(url);
        //    NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);

        //    if (query.AllKeys.Contains(paramName))
        //    {
        //        query[paramName] = paramValue;
        //    }
        //    else
        //    {
        //        query.Add(paramName, paramValue);
        //    }
        //    uriBuilder.Query = query.ToString();

        //    return uriBuilder.Uri;
        //    return url;
        ////}
    }
}
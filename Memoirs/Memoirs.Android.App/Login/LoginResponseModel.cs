using System;

using Newtonsoft.Json;
using RestSharp;

namespace Memoirs.Android.App.Login
{
    public class LoginResponseModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty(propertyName:"expires_in")]
        [JsonConverter(typeof(ExpiresInSerializer))]
        public TimeSpan TimeToExpire { get; set; }
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty(".issued")]
        public string Issued { get; set; }
        [JsonProperty(".expires")]
        public string Expires { get; set; }
    }
}
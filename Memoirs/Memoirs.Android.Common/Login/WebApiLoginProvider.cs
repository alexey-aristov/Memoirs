using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ninject;
using Uri = System.Uri;

namespace Memoirs.Android.Common.Login
{
    public class WebApiLoginProvider:ILoginProvider
    {
        private string _accessToken;
        public LoginResult Login(string login, string password)
        {
            throw new NotSupportedException();

        }

        public async Task<LoginResult> LoginAsync(string login, string password)
        {
            try
            {
                //var connectivityManager = (ConnectivityManager)App.ContextWrapper.GetSystemService(Context.ConnectivityService);
                //NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
                //bool isOnline = (activeConnection != null) && activeConnection.IsConnected;

                var connectionChecker = Dependencies.Container.Get<IConnectionChecker>();
                var userManager  = Dependencies.Container.Get<IUserManager>();
                if (!connectionChecker.IsInternetAvailable())
                {
                    return new LoginResult()
                    {
                        Errors = new List<string>()
                        {
                            "Check connection to Internet."
                        }
                    };
                }
                string apiUrl = "http://aristov.me/api/Account/LoginUrlEncoded";

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.Timeout = TimeSpan.FromSeconds(2);
                    HttpContent content = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", login),
                    new KeyValuePair<string, string>("password", password),
                });
                    HttpResponseMessage response = client.PostAsync(apiUrl, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        LoginResponseModel jsonResponse = JsonConvert.DeserializeObject<LoginResponseModel>(response.Content.ReadAsStringAsync().Result);

                        var token = $"{jsonResponse.TokenType} {jsonResponse.AccessToken}";
                        userManager.User = new User()
                        {
                            Login = jsonResponse.UserName,
                            Token = token
                        };
                        return new LoginResult()
                        {
                            Token = token,
                            Login = jsonResponse.UserName
                        };
                    }
                    else
                    {
                        return new LoginResult()
                        {
                            Errors = new List<string>()
                            {
                                "Login failed."
                            }
                        };
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Dependencies.Container.Get<IUserManager>().User = null;
                return new LoginResult()
                {
                    Errors = new List<string>()
                    {
                        "Login failed."
                    }
                };
            }
        }

        public void Logout()
        {
        }
    }
}
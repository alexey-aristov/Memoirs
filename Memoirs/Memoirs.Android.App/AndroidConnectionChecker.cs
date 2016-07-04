

using Android.Content;
using Android.Net;
using Memoirs.Android.Common;

namespace Memoirs.Android.App
{
    public class AndroidConnectionChecker:IConnectionChecker
    {
        public bool IsInternetAvailable()
        {
            var connectivityManager = (ConnectivityManager)App.ContextWrapper.GetSystemService(Context.ConnectivityService);
            NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            bool isOnline = (activeConnection != null) && activeConnection.IsConnected;
            return isOnline;
        }
    }
}
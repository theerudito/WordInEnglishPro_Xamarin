using Android.Content;
using Android.Net;
using Xamarin.Essentials;

namespace WordInEnglishPro.Helpers
{
    public static class ValidationInternet
    {
        public static bool IsConnected()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
                return true;
            else
                return false;
        }
    }

    public static class ConnectivityHelper
    {
        // ver si esta restrindigo el internet en el celular

        public static bool IsInternetAvailable(Context context)
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            return (activeConnection != null) && activeConnection.IsConnected;
        }
    }
}
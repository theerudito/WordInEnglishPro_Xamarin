namespace WordInEnglishPro.Helpers
{
    public static class LocalStorange
    {
        public static string GetLocalStorange(string key)
        {
            return Xamarin.Essentials.Preferences.Get(key, string.Empty);
        }

        public static void SetLocalStorange(string key, string value)
        {
            Xamarin.Essentials.Preferences.Set(key, value);
        }
    }
}
using System.IO;
using System.Reflection;
using WordInEnglish;

namespace WordInEnglishPro.Helpers
{
    public static class SoundsApp
    {
        public static void SoundCorrect()
        {
            var audio = "WordInEnglishPro.Sound.correct.mp3";
            SoundPlay(audio);
        }

        public static void SoundInCorrect()
        {
            var audio = "WordInEnglishPro.Sound.error.mp3";
            SoundPlay(audio);
        }

        private static void SoundPlay(string audio)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream(audio);
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load(audioStream);
            player.Play();
        }
    }
}
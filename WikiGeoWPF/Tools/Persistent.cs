using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiGeoWPF
{
    class Persistent
    {
        public static Game game;
        public static int streak = 0;
        public static int maxStreak = 0;
        public static Dictionary<string, string> settings = new Dictionary<string, string>();
        

        public static bool IncreaseStreak()
        {
            streak++;
            if (streak > maxStreak)
            {
                maxStreak = streak;
                return true;
            }

            return false;
        }

        public class Settings
        {
            //public static readonly string[] languages = { "English", "German" };
            public static readonly Dictionary<string, string> languages = new Dictionary<string, string>()
            {
                {"English", "en"}, 
                {"Deutsch", "de"},
                {"日本語", "jp" },
                {"Français", "fr" },
                {"Simple English", "simple" },
                {"Alemannisch", "als" },
                {"Plattdüütsch", "nds" }
            };
            public static string language = "en";
            public static string languagePlain = "English";

            public static void SetLanguage(string fullLanguageName)
            {
                if(languages.ContainsKey(fullLanguageName))
                {
                    language = languages[fullLanguageName];
                    languagePlain = fullLanguageName;
                }
            }
        }
    }
}

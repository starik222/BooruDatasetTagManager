using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BooruDatasetTagManager
{
    internal class I18n
    {
        private static LanguageManager langManager;
        private static string currentLang = LanguageManager.defaultLang;
        private static Dictionary<string, string> currentLangDict;

        public static void Initialize(string language)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            currentLang = language;
            if (langManager == null)
            {
                langManager = new LanguageManager();
            }
            currentLangDict = langManager.Langs[currentLang];
        }

        public static string GetText(string key)
        {
            if (langManager == null)
            {
                langManager = new LanguageManager();
            }
            try
            {
                string result = "";
                if (!currentLangDict.TryGetValue(key, out result))
                {
                    result = key;
                }
                else
                {
                    result = result.Replace("\\n", "\n");
                }
                var hkItem = Program.Settings.Hotkeys[key];
                if (hkItem != null)
                {
                    result += $" ({hkItem.GetHotkeyString()})";
                }
                return result;
            }
            catch (Exception ex)
            {
                return key;
            }
        }

        public static List<string> GetLanguages()
        {
            var langs = langManager.Langs.Keys.ToList();
            langs.Sort();
            return langs;
        }

        public static string GetIndex(string text)
        {
            return currentLangDict.First(a => a.Value == text).Key;
        }

        public static string[] GetAllIndexes(string text)
        {
            return currentLangDict.Where(a => a.Value == text).Select(a => a.Key).ToArray();
        }

        //private static ResourceManager _resourceManager;

        //public static void Initialize(string language)
        //{
        //    Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
        //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
        //    _resourceManager = new ResourceManager("BooruDatasetTagManager.Resources", typeof(I18n).Assembly);
        //}

        //public static string GetText(string key)
        //{
        //    if (_resourceManager == null)
        //        throw new InvalidOperationException("I18n class has not been initialized.");
        //    try
        //    {
        //        string result = _resourceManager.GetString(key);
        //        var hkItem = Program.Settings.Hotkeys[key];
        //        if (hkItem != null)
        //        {
        //            result += $" ({hkItem.GetHotkeyString()})";
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return key;
        //    }
        //}
    }
}

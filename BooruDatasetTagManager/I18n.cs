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
        private static ResourceManager _resourceManager;

        public static void Initialize(string language)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            _resourceManager = new ResourceManager("BooruDatasetTagManager.Resources", typeof(I18n).Assembly);
        }

        public static string GetText(string key)
        {
            if (_resourceManager == null)
                throw new InvalidOperationException("I18n class has not been initialized.");
            string result = _resourceManager.GetString(key);
            var hkItem = Program.Settings.Hotkeys[key];
            if (hkItem != null)
            {
                result += $" ({hkItem.GetHotkeyString()})";
            }
            return result;
        }
    }
}

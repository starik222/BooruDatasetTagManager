using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager
{
    public class AppSettings
    {
        public string TranslationLanguage { get; set; }

        public AppSettings()
        {
            TranslationLanguage = "ru";
        }
    }
}

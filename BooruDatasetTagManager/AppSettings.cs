using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager
{
    public class AppSettings
    {
        public string TranslationLanguage { get; set; } = "ru";
        public int PreviewSize { get; set; } = 130;
        [JsonIgnore]
        public List<LanguageItem> AvaibleLanguages;
        private string settingsFile;

        public AppSettings(string appDir)
        {
            InitAvaibleLangs();
            LoadData(appDir);
        }

        public AppSettings()
        {
        }

        private void LoadData(string appDir)
        {
            settingsFile = Path.Combine(appDir, "settings.json");
            if (!File.Exists(settingsFile))
            {
                //Settings = new AppSettings();
                File.WriteAllText(settingsFile, JsonConvert.SerializeObject(this));
            }
            else
            {
                var tempSettings = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(settingsFile));
                TranslationLanguage = tempSettings.TranslationLanguage;
                PreviewSize = tempSettings.PreviewSize;
            }
        }

        public void SaveSettings()
        {
            File.WriteAllText(settingsFile, JsonConvert.SerializeObject(this));
        }

        public void InitAvaibleLangs()
        {
            AvaibleLanguages = new List<LanguageItem>();
            AvaibleLanguages.Add(new LanguageItem("Afrikaans", "af"));
            AvaibleLanguages.Add(new LanguageItem("Albanian", "sq"));
            AvaibleLanguages.Add(new LanguageItem("Amharic", "am"));
            AvaibleLanguages.Add(new LanguageItem("Arabic", "ar"));
            AvaibleLanguages.Add(new LanguageItem("Armenian", "hy"));
            AvaibleLanguages.Add(new LanguageItem("Assamese", "as"));
            AvaibleLanguages.Add(new LanguageItem("Aymara", "ay"));
            AvaibleLanguages.Add(new LanguageItem("Azerbaijani", "az"));
            AvaibleLanguages.Add(new LanguageItem("Bambara", "bm"));
            AvaibleLanguages.Add(new LanguageItem("Basque", "eu"));
            AvaibleLanguages.Add(new LanguageItem("Belarusian", "be"));
            AvaibleLanguages.Add(new LanguageItem("Bengali", "bn"));
            AvaibleLanguages.Add(new LanguageItem("Bhojpuri", "bho"));
            AvaibleLanguages.Add(new LanguageItem("Bosnian", "bs"));
            AvaibleLanguages.Add(new LanguageItem("Bulgarian", "bg"));
            AvaibleLanguages.Add(new LanguageItem("Catalan", "ca"));
            AvaibleLanguages.Add(new LanguageItem("Cebuano", "ceb"));
            AvaibleLanguages.Add(new LanguageItem("Chinese (Simplified)", "zh-CN"));
            AvaibleLanguages.Add(new LanguageItem("Chinese (Traditional)", "zh-TW"));
            AvaibleLanguages.Add(new LanguageItem("Corsican", "co"));
            AvaibleLanguages.Add(new LanguageItem("Croatian", "hr"));
            AvaibleLanguages.Add(new LanguageItem("Czech", "cs"));
            AvaibleLanguages.Add(new LanguageItem("Danish", "da"));
            AvaibleLanguages.Add(new LanguageItem("Dhivehi", "dv"));
            AvaibleLanguages.Add(new LanguageItem("Dogri", "doi"));
            AvaibleLanguages.Add(new LanguageItem("Dutch", "nl"));
            AvaibleLanguages.Add(new LanguageItem("English", "en"));
            AvaibleLanguages.Add(new LanguageItem("Esperanto", "eo"));
            AvaibleLanguages.Add(new LanguageItem("Estonian", "et"));
            AvaibleLanguages.Add(new LanguageItem("Ewe", "ee"));
            AvaibleLanguages.Add(new LanguageItem("Filipino (Tagalog)", "fil"));
            AvaibleLanguages.Add(new LanguageItem("Finnish", "fi"));
            AvaibleLanguages.Add(new LanguageItem("French", "fr"));
            AvaibleLanguages.Add(new LanguageItem("Frisian", "fy"));
            AvaibleLanguages.Add(new LanguageItem("Galician", "gl"));
            AvaibleLanguages.Add(new LanguageItem("Georgian", "ka"));
            AvaibleLanguages.Add(new LanguageItem("German", "de"));
            AvaibleLanguages.Add(new LanguageItem("Greek", "el"));
            AvaibleLanguages.Add(new LanguageItem("Guarani", "gn"));
            AvaibleLanguages.Add(new LanguageItem("Gujarati", "gu"));
            AvaibleLanguages.Add(new LanguageItem("Haitian Creole", "ht"));
            AvaibleLanguages.Add(new LanguageItem("Hausa", "ha"));
            AvaibleLanguages.Add(new LanguageItem("Hawaiian", "haw"));
            AvaibleLanguages.Add(new LanguageItem("Hebrew", "he"));
            AvaibleLanguages.Add(new LanguageItem("Hindi", "hi"));
            AvaibleLanguages.Add(new LanguageItem("Hmong", "hmn"));
            AvaibleLanguages.Add(new LanguageItem("Hungarian", "hu"));
            AvaibleLanguages.Add(new LanguageItem("Icelandic", "is"));
            AvaibleLanguages.Add(new LanguageItem("Igbo", "ig"));
            AvaibleLanguages.Add(new LanguageItem("Ilocano", "ilo"));
            AvaibleLanguages.Add(new LanguageItem("Indonesian", "id"));
            AvaibleLanguages.Add(new LanguageItem("Irish", "ga"));
            AvaibleLanguages.Add(new LanguageItem("Italian", "it"));
            AvaibleLanguages.Add(new LanguageItem("Japanese", "ja"));
            AvaibleLanguages.Add(new LanguageItem("Javanese", "jv"));
            AvaibleLanguages.Add(new LanguageItem("Kannada", "kn"));
            AvaibleLanguages.Add(new LanguageItem("Kazakh", "kk"));
            AvaibleLanguages.Add(new LanguageItem("Khmer", "km"));
            AvaibleLanguages.Add(new LanguageItem("Kinyarwanda", "rw"));
            AvaibleLanguages.Add(new LanguageItem("Konkani", "gom"));
            AvaibleLanguages.Add(new LanguageItem("Korean", "ko"));
            AvaibleLanguages.Add(new LanguageItem("Krio", "kri"));
            AvaibleLanguages.Add(new LanguageItem("Kurdish", "ku"));
            AvaibleLanguages.Add(new LanguageItem("Kurdish (Sorani)", "ckb"));
            AvaibleLanguages.Add(new LanguageItem("Kyrgyz", "ky"));
            AvaibleLanguages.Add(new LanguageItem("Lao", "lo"));
            AvaibleLanguages.Add(new LanguageItem("Latin", "la"));
            AvaibleLanguages.Add(new LanguageItem("Latvian", "lv"));
            AvaibleLanguages.Add(new LanguageItem("Lingala", "ln"));
            AvaibleLanguages.Add(new LanguageItem("Lithuanian", "lt"));
            AvaibleLanguages.Add(new LanguageItem("Luganda", "lg"));
            AvaibleLanguages.Add(new LanguageItem("Luxembourgish", "lb"));
            AvaibleLanguages.Add(new LanguageItem("Macedonian", "mk"));
            AvaibleLanguages.Add(new LanguageItem("Maithili", "mai"));
            AvaibleLanguages.Add(new LanguageItem("Malagasy", "mg"));
            AvaibleLanguages.Add(new LanguageItem("Malay", "ms"));
            AvaibleLanguages.Add(new LanguageItem("Malayalam", "ml"));
            AvaibleLanguages.Add(new LanguageItem("Maltese", "mt"));
            AvaibleLanguages.Add(new LanguageItem("Maori", "mi"));
            AvaibleLanguages.Add(new LanguageItem("Marathi", "mr"));
            AvaibleLanguages.Add(new LanguageItem("Meiteilon (Manipuri)", "mni-Mtei"));
            AvaibleLanguages.Add(new LanguageItem("Mizo", "lus"));
            AvaibleLanguages.Add(new LanguageItem("Mongolian", "mn"));
            AvaibleLanguages.Add(new LanguageItem("Myanmar (Burmese)", "my"));
            AvaibleLanguages.Add(new LanguageItem("Nepali", "ne"));
            AvaibleLanguages.Add(new LanguageItem("Norwegian", "no"));
            AvaibleLanguages.Add(new LanguageItem("Nyanja (Chichewa)", "ny"));
            AvaibleLanguages.Add(new LanguageItem("Odia (Oriya)", "or"));
            AvaibleLanguages.Add(new LanguageItem("Oromo", "om"));
            AvaibleLanguages.Add(new LanguageItem("Pashto", "ps"));
            AvaibleLanguages.Add(new LanguageItem("Persian", "fa"));
            AvaibleLanguages.Add(new LanguageItem("Polish", "pl"));
            AvaibleLanguages.Add(new LanguageItem("Portuguese (Portugal, Brazil)", "pt"));
            AvaibleLanguages.Add(new LanguageItem("Punjabi", "pa"));
            AvaibleLanguages.Add(new LanguageItem("Quechua", "qu"));
            AvaibleLanguages.Add(new LanguageItem("Romanian", "ro"));
            AvaibleLanguages.Add(new LanguageItem("Russian", "ru"));
            AvaibleLanguages.Add(new LanguageItem("Samoan", "sm"));
            AvaibleLanguages.Add(new LanguageItem("Sanskrit", "sa"));
            AvaibleLanguages.Add(new LanguageItem("Scots Gaelic", "gd"));
            AvaibleLanguages.Add(new LanguageItem("Sepedi", "nso"));
            AvaibleLanguages.Add(new LanguageItem("Serbian", "sr"));
            AvaibleLanguages.Add(new LanguageItem("Sesotho", "st"));
            AvaibleLanguages.Add(new LanguageItem("Shona", "sn"));
            AvaibleLanguages.Add(new LanguageItem("Sindhi", "sd"));
            AvaibleLanguages.Add(new LanguageItem("Sinhala (Sinhalese)", "si"));
            AvaibleLanguages.Add(new LanguageItem("Slovak", "sk"));
            AvaibleLanguages.Add(new LanguageItem("Slovenian", "sl"));
            AvaibleLanguages.Add(new LanguageItem("Somali", "so"));
            AvaibleLanguages.Add(new LanguageItem("Spanish", "es"));
            AvaibleLanguages.Add(new LanguageItem("Sundanese", "su"));
            AvaibleLanguages.Add(new LanguageItem("Swahili", "sw"));
            AvaibleLanguages.Add(new LanguageItem("Swedish", "sv"));
            AvaibleLanguages.Add(new LanguageItem("Tagalog (Filipino)", "tl"));
            AvaibleLanguages.Add(new LanguageItem("Tajik", "tg"));
            AvaibleLanguages.Add(new LanguageItem("Tamil", "ta"));
            AvaibleLanguages.Add(new LanguageItem("Tatar", "tt"));
            AvaibleLanguages.Add(new LanguageItem("Telugu", "te"));
            AvaibleLanguages.Add(new LanguageItem("Thai", "th"));
            AvaibleLanguages.Add(new LanguageItem("Tigrinya", "ti"));
            AvaibleLanguages.Add(new LanguageItem("Tsonga", "ts"));
            AvaibleLanguages.Add(new LanguageItem("Turkish", "tr"));
            AvaibleLanguages.Add(new LanguageItem("Turkmen", "tk"));
            AvaibleLanguages.Add(new LanguageItem("Twi (Akan)", "ak"));
            AvaibleLanguages.Add(new LanguageItem("Ukrainian", "uk"));
            AvaibleLanguages.Add(new LanguageItem("Urdu", "ur"));
            AvaibleLanguages.Add(new LanguageItem("Uyghur", "ug"));
            AvaibleLanguages.Add(new LanguageItem("Uzbek", "uz"));
            AvaibleLanguages.Add(new LanguageItem("Vietnamese", "vi"));
            AvaibleLanguages.Add(new LanguageItem("Welsh", "cy"));
            AvaibleLanguages.Add(new LanguageItem("Xhosa", "xh"));
            AvaibleLanguages.Add(new LanguageItem("Yiddish", "yi"));
            AvaibleLanguages.Add(new LanguageItem("Yoruba", "yo"));
            AvaibleLanguages.Add(new LanguageItem("Zulu", "zu"));
        }
    }

    public class LanguageItem
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public LanguageItem(string name, string code)
        {
            Name = name;
            Code = code;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}

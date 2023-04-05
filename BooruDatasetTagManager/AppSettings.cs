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
        public TranslationService TransService { get; set; } = TranslationService.GoogleTranslate;
        public bool OnlyManualTransInAutocomplete { get; set; } = false;
        public AutocompleteMode AutocompleteMode { get; set; } = AutocompleteMode.StartWith;
        public AutocompleteSort AutocompleteSort { get; set; } = AutocompleteSort.Alphabetical;
        public bool FixTagsOnLoad { get; set; } = true;
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
                TransService = tempSettings.TransService;
                OnlyManualTransInAutocomplete = tempSettings.OnlyManualTransInAutocomplete;
                AutocompleteMode = tempSettings.AutocompleteMode;
                AutocompleteSort = tempSettings.AutocompleteSort;
                FixTagsOnLoad = tempSettings.FixTagsOnLoad;
            }
        }

        public void SaveSettings()
        {
            File.WriteAllText(settingsFile, JsonConvert.SerializeObject(this));
        }

        public void InitAvaibleLangs()
        {
            AvaibleLanguages = new List<LanguageItem>
            {
                new LanguageItem("Afrikaans", "af"),
                new LanguageItem("Albanian", "sq"),
                new LanguageItem("Amharic", "am"),
                new LanguageItem("Arabic", "ar"),
                new LanguageItem("Armenian", "hy"),
                new LanguageItem("Assamese", "as"),
                new LanguageItem("Aymara", "ay"),
                new LanguageItem("Azerbaijani", "az"),
                new LanguageItem("Bambara", "bm"),
                new LanguageItem("Basque", "eu"),
                new LanguageItem("Belarusian", "be"),
                new LanguageItem("Bengali", "bn"),
                new LanguageItem("Bhojpuri", "bho"),
                new LanguageItem("Bosnian", "bs"),
                new LanguageItem("Bulgarian", "bg"),
                new LanguageItem("Catalan", "ca"),
                new LanguageItem("Cebuano", "ceb"),
                new LanguageItem("Chinese (Simplified)", "zh-CN"),
                new LanguageItem("Chinese (Traditional)", "zh-TW"),
                new LanguageItem("Corsican", "co"),
                new LanguageItem("Croatian", "hr"),
                new LanguageItem("Czech", "cs"),
                new LanguageItem("Danish", "da"),
                new LanguageItem("Dhivehi", "dv"),
                new LanguageItem("Dogri", "doi"),
                new LanguageItem("Dutch", "nl"),
                new LanguageItem("English", "en"),
                new LanguageItem("Esperanto", "eo"),
                new LanguageItem("Estonian", "et"),
                new LanguageItem("Ewe", "ee"),
                new LanguageItem("Filipino (Tagalog)", "fil"),
                new LanguageItem("Finnish", "fi"),
                new LanguageItem("French", "fr"),
                new LanguageItem("Frisian", "fy"),
                new LanguageItem("Galician", "gl"),
                new LanguageItem("Georgian", "ka"),
                new LanguageItem("German", "de"),
                new LanguageItem("Greek", "el"),
                new LanguageItem("Guarani", "gn"),
                new LanguageItem("Gujarati", "gu"),
                new LanguageItem("Haitian Creole", "ht"),
                new LanguageItem("Hausa", "ha"),
                new LanguageItem("Hawaiian", "haw"),
                new LanguageItem("Hebrew", "he"),
                new LanguageItem("Hindi", "hi"),
                new LanguageItem("Hmong", "hmn"),
                new LanguageItem("Hungarian", "hu"),
                new LanguageItem("Icelandic", "is"),
                new LanguageItem("Igbo", "ig"),
                new LanguageItem("Ilocano", "ilo"),
                new LanguageItem("Indonesian", "id"),
                new LanguageItem("Irish", "ga"),
                new LanguageItem("Italian", "it"),
                new LanguageItem("Japanese", "ja"),
                new LanguageItem("Javanese", "jv"),
                new LanguageItem("Kannada", "kn"),
                new LanguageItem("Kazakh", "kk"),
                new LanguageItem("Khmer", "km"),
                new LanguageItem("Kinyarwanda", "rw"),
                new LanguageItem("Konkani", "gom"),
                new LanguageItem("Korean", "ko"),
                new LanguageItem("Krio", "kri"),
                new LanguageItem("Kurdish", "ku"),
                new LanguageItem("Kurdish (Sorani)", "ckb"),
                new LanguageItem("Kyrgyz", "ky"),
                new LanguageItem("Lao", "lo"),
                new LanguageItem("Latin", "la"),
                new LanguageItem("Latvian", "lv"),
                new LanguageItem("Lingala", "ln"),
                new LanguageItem("Lithuanian", "lt"),
                new LanguageItem("Luganda", "lg"),
                new LanguageItem("Luxembourgish", "lb"),
                new LanguageItem("Macedonian", "mk"),
                new LanguageItem("Maithili", "mai"),
                new LanguageItem("Malagasy", "mg"),
                new LanguageItem("Malay", "ms"),
                new LanguageItem("Malayalam", "ml"),
                new LanguageItem("Maltese", "mt"),
                new LanguageItem("Maori", "mi"),
                new LanguageItem("Marathi", "mr"),
                new LanguageItem("Meiteilon (Manipuri)", "mni-Mtei"),
                new LanguageItem("Mizo", "lus"),
                new LanguageItem("Mongolian", "mn"),
                new LanguageItem("Myanmar (Burmese)", "my"),
                new LanguageItem("Nepali", "ne"),
                new LanguageItem("Norwegian", "no"),
                new LanguageItem("Nyanja (Chichewa)", "ny"),
                new LanguageItem("Odia (Oriya)", "or"),
                new LanguageItem("Oromo", "om"),
                new LanguageItem("Pashto", "ps"),
                new LanguageItem("Persian", "fa"),
                new LanguageItem("Polish", "pl"),
                new LanguageItem("Portuguese (Portugal, Brazil)", "pt"),
                new LanguageItem("Punjabi", "pa"),
                new LanguageItem("Quechua", "qu"),
                new LanguageItem("Romanian", "ro"),
                new LanguageItem("Russian", "ru"),
                new LanguageItem("Samoan", "sm"),
                new LanguageItem("Sanskrit", "sa"),
                new LanguageItem("Scots Gaelic", "gd"),
                new LanguageItem("Sepedi", "nso"),
                new LanguageItem("Serbian", "sr"),
                new LanguageItem("Sesotho", "st"),
                new LanguageItem("Shona", "sn"),
                new LanguageItem("Sindhi", "sd"),
                new LanguageItem("Sinhala (Sinhalese)", "si"),
                new LanguageItem("Slovak", "sk"),
                new LanguageItem("Slovenian", "sl"),
                new LanguageItem("Somali", "so"),
                new LanguageItem("Spanish", "es"),
                new LanguageItem("Sundanese", "su"),
                new LanguageItem("Swahili", "sw"),
                new LanguageItem("Swedish", "sv"),
                new LanguageItem("Tagalog (Filipino)", "tl"),
                new LanguageItem("Tajik", "tg"),
                new LanguageItem("Tamil", "ta"),
                new LanguageItem("Tatar", "tt"),
                new LanguageItem("Telugu", "te"),
                new LanguageItem("Thai", "th"),
                new LanguageItem("Tigrinya", "ti"),
                new LanguageItem("Tsonga", "ts"),
                new LanguageItem("Turkish", "tr"),
                new LanguageItem("Turkmen", "tk"),
                new LanguageItem("Twi (Akan)", "ak"),
                new LanguageItem("Ukrainian", "uk"),
                new LanguageItem("Urdu", "ur"),
                new LanguageItem("Uyghur", "ug"),
                new LanguageItem("Uzbek", "uz"),
                new LanguageItem("Vietnamese", "vi"),
                new LanguageItem("Welsh", "cy"),
                new LanguageItem("Xhosa", "xh"),
                new LanguageItem("Yiddish", "yi"),
                new LanguageItem("Yoruba", "yo"),
                new LanguageItem("Zulu", "zu")
            };
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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public bool FixTagsOnSaveLoad { get; set; } = true;
        public ImagePreviewType PreviewType { get; set; } = ImagePreviewType.PreviewInMainWindow;
        //public bool FixTagsOnSave { get; set; } = true;
        public string SeparatorOnLoad { get; set; } = ",";
        public string SeparatorOnSave { get; set; } = ", ";
        public string DefaultTagsFileExtension { get; set; } = "txt";
        public string CaptionFileExtensions
        {
            get
            {
                return string.Join(',', _tagsFilesExt);
            }
            set
            {
                _tagsFilesExt = value.Split(new char[] { ',' }, StringSplitOptions.TrimEntries);
            }
        }
        public int ShowAutocompleteAfterCharCount { get; set; } = 3;
        public bool AskSaveChanges { get; set; } = true;
        public int GridViewRowHeight { get; set; } = 29;
        public FontSettings GridViewFont { get; set; } = new FontSettings();
        public FontSettings AutocompleteFont { get; set; } = new FontSettings() { Name = "Segoe UI", Size = 9, GdiCharSet = 1 };

        public HotkeyData Hotkeys { get; set; }

        public InterragatorSettings AutoTagger { get; set; }

        private string settingsFile;

        public bool AutoSort { get; set; } = false;

        public string Language { get; set; } = "en-US";

        public string ColorScheme { get; set; } = "Classic";

        private string[] _tagsFilesExt = { "txt", "caption" };


        public AppSettings(string appDir)
        {
            InitAvaibleLangs();
            Hotkeys = new HotkeyData();
            Hotkeys.InitDefault();
            LoadData(appDir);
        }

        public AppSettings()
        {
            Hotkeys = new HotkeyData();
            Hotkeys.InitDefault();
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
                FixTagsOnSaveLoad = tempSettings.FixTagsOnSaveLoad;
                SeparatorOnLoad = tempSettings.SeparatorOnLoad;
                SeparatorOnSave = tempSettings.SeparatorOnSave;
                ShowAutocompleteAfterCharCount = tempSettings.ShowAutocompleteAfterCharCount;
                AskSaveChanges = tempSettings.AskSaveChanges;
                GridViewRowHeight = tempSettings.GridViewRowHeight;
                GridViewFont = tempSettings.GridViewFont;
                AutocompleteFont = tempSettings.AutocompleteFont;
                AutoSort = tempSettings.AutoSort || false;
                Language = tempSettings.Language;
                PreviewType = tempSettings.PreviewType;
                DefaultTagsFileExtension = tempSettings.DefaultTagsFileExtension;
                CaptionFileExtensions = tempSettings.CaptionFileExtensions;
                if (!string.IsNullOrEmpty(tempSettings.ColorScheme))
                    ColorScheme = tempSettings.ColorScheme;
                AutoTagger = tempSettings.AutoTagger;
                if (AutoTagger == null)
                {
                    AutoTagger = new InterragatorSettings();
                }

                if (tempSettings.Hotkeys != null)
                {
                    foreach (var item in tempSettings.Hotkeys.Items)
                    {
                        var hkItem = Hotkeys[item.Id];
                        if (hkItem != null)
                        {
                            hkItem.KeyData = item.KeyData;
                            hkItem.IsCtrl = item.IsCtrl;
                            hkItem.IsAlt = item.IsAlt;
                            hkItem.IsShift = item.IsShift;
                        }
                    }
                }
            }
        }

        public void SaveSettings()
        {
            File.WriteAllText(settingsFile, JsonConvert.SerializeObject(this));
        }

        public string[] GetTagFilesExtensions()
        {
            return _tagsFilesExt;
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
                new LanguageItem("Portuguese (Brazil)", "pt-BR"),
                new LanguageItem("Portuguese (Portugal)", "pt-PT"),
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

    public class InterragatorSettings
    {
        public List<KeyValuePair<string, float>> InterragatorParams { get; set; }
        public AutoTaggerSort SortMode { get; set; } = AutoTaggerSort.None;
        public NetworkUnionMode UnionMode { get; set; } = NetworkUnionMode.Addition;
        public NetworkResultSetMode SetMode { get; set; } = NetworkResultSetMode.AllWithReplacement;
        public bool SerializeVramUsage { get; set; } = false;
        public bool SkipInternetRequests { get; set; } = false;

        public InterragatorSettings()
        {
            InterragatorParams = new List<KeyValuePair<string, float>>();
        }
    }

    public class FontSettings
    {
        public string Name { get; set; }    = "Tahoma";
        public float Size { get; set; } = 14;
        public bool Bold { get; set; } = false;
        public byte GdiCharSet { get; set; } = 1;
        public bool Italic { get; set; } = false;
        public bool Strikeout { get; set; } = false;
        public bool Underline { get; set; } = false;

        public FontSettings() { }


        public Font GetFont()
        {
            List<FontStyle> resStyle = new List<FontStyle>();
            resStyle.Add(FontStyle.Regular);
            if (Bold)
                resStyle.Add(FontStyle.Bold);
            if (Italic)
                resStyle.Add(FontStyle.Italic);
            if(Strikeout)
                resStyle.Add(FontStyle.Strikeout);
            if(Underline) 
                resStyle.Add(FontStyle.Underline);
            return new Font(Name, Size, resStyle.Aggregate((x, y) => x |= y), GraphicsUnit.Point, GdiCharSet, false);
        }

        public static FontSettings Create(Font fnt)
        {
            FontSettings fs = new FontSettings();
            fs.Name = fnt.Name;
            fs.Underline = fnt.Underline;
            fs.GdiCharSet = fnt.GdiCharSet;
            fs.Bold = fnt.Bold;
            fs.Italic = fnt.Italic;
            fs.Size = fnt.Size;
            fs.Strikeout = fnt.Strikeout;
            return fs;
        }

        public override string ToString()
        {
            return $"{Name}; {Size}pt;";
        }
    }
}

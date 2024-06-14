using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager
{
    public class LanguageManager
    {
        public const string defaultLang = "en-US";
        public Dictionary<string, Dictionary<string, string>> Langs { get; set; }
        public LanguageManager()
        {
            Langs = new Dictionary<string, Dictionary<string, string>>();
            string[] files = Directory.GetFiles(Path.Combine(Program.AppPath, "Languages"), "*.txt", SearchOption.TopDirectoryOnly);
            foreach(string file in files)
                LoadLanguageFromFile(file);
            FixOldLangFilesFromDefault();
        }

        private void LoadLanguageFromFile(string filename)
        {
            Dictionary<string, string> langData = new Dictionary<string, string>();
            string[] fileData = File.ReadAllLines(filename, Encoding.UTF8);
            foreach (string line in fileData)
            {
                int spIndex = line.IndexOf('=');
                if (spIndex == -1)
                    continue;
                langData.Add(line.Substring(0,spIndex).Trim(), line.Substring(spIndex+1));
            }
            Langs[Path.GetFileNameWithoutExtension(filename)] = langData;
        }

        private void FixOldLangFilesFromDefault()
        {
            List<string> langToSave = new List<string>();
            foreach (string index in Langs[defaultLang].Keys)
            {
                foreach (var lang in Langs.Keys)
                {
                    if (lang == defaultLang)
                        continue;
                    if (!Langs[lang].ContainsKey(index))
                    {
                        Langs[lang].Add(index, Langs[defaultLang][index]);
                        if(!langToSave.Contains(lang))
                            langToSave.Add(lang);
                    }
                }
            }
            foreach (var toSave in langToSave)
                SaveLangFile(toSave);
        }

        private void SaveLangFile(string lang)
        {
            string filePath = Path.Combine(Program.AppPath, "Languages", lang + ".txt");
            StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8);
            foreach (string key in Langs[lang].Keys)
            {
                sw.WriteLine(key + "=" + Langs[lang][key]);
            }
            sw.Close();
        }
    }
}

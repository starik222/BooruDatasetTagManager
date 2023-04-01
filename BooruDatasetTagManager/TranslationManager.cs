using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Crypto;
using static System.Net.Mime.MediaTypeNames;

namespace BooruDatasetTagManager
{
    public class TranslationManager
    {
        private string _language;
        private string _workDir;
        public List<TransItem> Translations { get; set; }
        private AbstractTranslator translator;
        private string translationFilePath;

        public TranslationManager(string toLang, TranslationService service, string workDir)
        {
            _language = toLang;
            _workDir = workDir;
            Translations = new List<TransItem>();
            translator = AbstractTranslator.Create(service);
            translationFilePath = Path.Combine(_workDir, _language + ".txt");
        }

        public void LoadTranslations()
        {
            if (!File.Exists(translationFilePath))
            {
                var sw = File.CreateText(translationFilePath);
                sw.WriteLine("//Translation format: <original>=<translation>");
                sw.Dispose();
                return;
            }
            string[] lines = File.ReadAllLines(translationFilePath);
            foreach (var item in lines)
            {
                if (item.Trim().StartsWith("//"))
                    continue;
                var transItem = TransItem.Create(item);
                if (transItem != null && !Contains(transItem.OrigHash))
                {
                    Translations.Add(transItem);
                }
            }
        }

        public bool Contains(string orig)
        {
            return Translations.Exists(a => a.OrigHash == orig.ToLower().GetHashCode());
        }

        public bool Contains(int hash)
        {
            return Translations.Exists(a => a.OrigHash == hash);
        }

        public string GetTranslation(string text)
        {
            return GetTranslation(text.ToLower().Trim().GetHashCode());
        }

        public string GetTranslation(int hash)
        {
            var res = Translations.FirstOrDefault(a => a.OrigHash == hash);
            if (res == null)
                return null;
            return res.Trans;
        }

        public async Task<string> GetTranslationAsync(string text)
        {
            return await Task.Run(() =>
            {
                return GetTranslation(text);
            });
        }
        public void AddTranslation(string orig, string trans)
        {
            File.AppendAllText(translationFilePath, $"{orig}={trans}\r\n", Encoding.UTF8);
            Translations.Add(new TransItem(orig, trans));
        }

        public async Task AddTranslationAsync(string orig, string trans)
        {
            StreamWriter sw = new StreamWriter(translationFilePath, true, Encoding.UTF8);
            await sw.WriteLineAsync($"{orig}={trans}");
            sw.Close();
            Translations.Add(new TransItem(orig, trans));
        }

        public async Task<string> TranslateAsync(string text)
        {
            string result = await GetTranslationAsync(text);
            if (result != null)
                return result;
            result = await translator.TranslateAsync(text, "en", _language);
            await AddTranslationAsync(text, result);
            return result;
        }


        public class TransItem
        {
            public string Orig { get; private set; }
            public string Trans {get; set; }
            public int OrigHash { get; private set; }

            public TransItem(string orig, string trans)
            {
                Orig = orig;
                Trans = trans;
                OrigHash = orig.ToLower().GetHashCode();
            }

            public static TransItem Create(string text)
            {
                int index = text.LastIndexOf('=');
                if (index == -1)
                    return null;
                string orig = text.Substring(0, index).Trim();
                string trans = text.Substring(index + 1).Trim();
                return new TransItem(orig, trans);
            }


            public TransItem() { }
        }
    }
}

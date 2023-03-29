using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Crypto;

namespace BooruDatasetTagManager
{
    public class TranslationManager
    {
        private string _language;
        private string _workDir;
        public List<TransItem> Translations { get; set; }
        public TranslationManager(string lang, string workDir)
        {
            _language = lang;
            _workDir = workDir;
            Translations = new List<TransItem>();
        }

        public void LoadTranslations()
        {
            string fullPath = Path.Combine(_workDir, _language + ".txt");
            if (!File.Exists(fullPath))
            {
                var sw = File.CreateText(fullPath);
                sw.WriteLine("//Translation format: <original>=<translation>");
                sw.Dispose();
                return;
            }
            string[] lines = File.ReadAllLines(fullPath);
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

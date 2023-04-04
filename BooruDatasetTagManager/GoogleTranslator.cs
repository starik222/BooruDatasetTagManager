using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator;

namespace BooruDatasetTagManager
{
    public class GoogleTranslator: AbstractTranslator
    {
        private GoogleTranslatorV2 translatorV2;
        public GoogleTranslator() : base(TranslationService.GoogleTranslate)
        {
            translatorV2 = new GoogleTranslatorV2();
        }

        public override async Task<string> TranslateAsync(string text, string fromLang, string toLang)
        {
            string res =  await translatorV2.Translate(text.Replace('_', ' ').Replace('-', ' ').Replace("\\(", "(").Replace("\\)", ")"), 
                fromLang, toLang);
            await Task.Delay(500);//antispam:)
            if (res == "error")
                return null;
            return res;
        }
        public override void Dispose()
        {
            translatorV2.Dispose();
        }
    }
}

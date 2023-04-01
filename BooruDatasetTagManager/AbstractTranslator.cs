using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager
{
    public abstract class AbstractTranslator : IDisposable
    {
        public TranslationService Service { get; set; }
        public AbstractTranslator(TranslationService service)
        {
            Service = service;
        }

        public static AbstractTranslator Create(TranslationService service)
        {
            switch (service)
            {
                case TranslationService.GoogleTranslate:
                    {
                        return new GoogleTranslator();
                    }
                case TranslationService.ChineseTranslate:
                    {
                        return new ChineseTranslator();
                    }
                default:
                    throw new NotImplementedException("Translation service not implemented");
            }
        }

        public virtual async Task<string> TranslateAsync(string text, string fromLang, string toLang)
        {
            return null;
        }

        public abstract void Dispose();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager.AiApi
{
    public class TranslateRequest
    {
        public string Text { get; set; }
        public string FromLang { get; set; }
        public string ToLang { get; set; }
        public bool SkipInternetRequests { get; set; }
        public bool SerializeVramUsage { get; set; }
        public string ImageName { get; set; }
        public ModelParameters Model { get; set; }
    }
}

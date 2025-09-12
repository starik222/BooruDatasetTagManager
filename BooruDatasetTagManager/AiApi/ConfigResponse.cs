using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager.AiApi
{
    public class ConfigResponse : BaseResponse
    {
        public List<string> Interrogators { get; set; }
        public List<string> Editors { get; set; }
        public List<string> Translators { get; set; }
        public ConfigResponse()
        {
            Interrogators = new List<string>();
            Editors = new List<string>();
            Translators = new List<string>();
        }

        public List<string> GetList()
        {
            List<string> list = new List<string>();
            list.AddRange(Interrogators);
            list.AddRange(Editors);
            list.AddRange(Translators);
            return list;
        }
    }
}

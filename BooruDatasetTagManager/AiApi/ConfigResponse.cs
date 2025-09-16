using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager.AiApi
{
    public class ConfigResponse : BaseResponse
    {
        public List<ModelBaseInfo> Interrogators { get; set; }
        public List<ModelBaseInfo> Editors { get; set; }
        public List<ModelBaseInfo> Translators { get; set; }
        public ConfigResponse()
        {
            Interrogators = new List<ModelBaseInfo>();
            Editors = new List<ModelBaseInfo>();
            Translators = new List<ModelBaseInfo>();
        }

        public List<ModelBaseInfo> GetList()
        {
            List<ModelBaseInfo> list = new List<ModelBaseInfo>();
            list.AddRange(Interrogators);
            list.AddRange(Editors);
            list.AddRange(Translators);
            return list;
        }
        public List<string> GetNames()
        {
            List<string> names = new List<string>();
            names.AddRange(Interrogators.Select(a => a.ModelName));
            names.AddRange(Editors.Select(a => a.ModelName));
            names.AddRange(Translators.Select(a => a.ModelName));
            return names;
        }
    }
}

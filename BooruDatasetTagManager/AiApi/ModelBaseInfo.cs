using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager.AiApi
{
    public class ModelBaseInfo
    {
        public string ModelName { get; set; }
        public bool SupportedVideo { get; set; }
        public string RepositoryLink { get; set; }

        public override string ToString()
        {
            return ModelName;
        }
    }
}

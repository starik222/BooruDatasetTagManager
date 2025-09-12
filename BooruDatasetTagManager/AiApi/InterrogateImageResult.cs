using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager.AiApi
{
    public class InterrogateImageResult
    {
        public string ModelName { get; set; }
        public List<TagEntry> Tags { get; set; }

    }
}

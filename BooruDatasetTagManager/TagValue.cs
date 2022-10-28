using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager
{
    public class TagValue
    {
        public string Tag { get; set; }

        public TagValue(string text)
        {
            Tag = text;
        }
    }
}

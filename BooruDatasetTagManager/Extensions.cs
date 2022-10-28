using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BooruDatasetTagManager
{
    public static class Extensions
    {

        public static void AddRange(this List<TagValue> list, IEnumerable<string> range)
        {
            foreach (var item in range)
                list.Add(new TagValue(item));
        }
    }
}

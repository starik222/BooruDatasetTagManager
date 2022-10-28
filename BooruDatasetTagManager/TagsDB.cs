using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager
{
    public class TagsDB
    {
        public List<string> Tags;

        public TagsDB()
        {
            Tags = new List<string>();
        }


        public void LoadFromCSVFile(string fPath, bool append)
        {
            string[] lines = File.ReadAllLines(fPath);
            if (!append)
                Tags.Clear();
            foreach (var item in lines)
            {
                int index = item.LastIndexOf(',');
                string tag = item.Substring(0, index);
                if (!Tags.Contains(tag))
                    Tags.Add(tag);
            }
        }

        public void LoadFromTagFile(string fPath, bool append)
        {
            string[] lines = File.ReadAllLines(fPath);
            if (!append)
                Tags.Clear();
            Tags.AddRange(lines);
        }

        public void SaveTags(string fPath)
        {
            File.WriteAllLines(fPath, Tags);
        }

    }
}

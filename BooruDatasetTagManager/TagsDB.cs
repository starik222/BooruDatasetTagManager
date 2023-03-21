using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooruDatasetTagManager
{
    public class TagsDB
    {
        public AutoCompleteStringCollection Tags;
        public string[] tagList;

        public TagsDB()
        {
            Tags = new AutoCompleteStringCollection();
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
                tag = tag.Replace('_', ' ');
                tag = tag.Replace("(", "\\(");
                tag = tag.Replace(")", "\\)");
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
            tagList = lines;
        }

        public void SaveTags(string fPath)
        {
            File.WriteAllLines(fPath, Tags.Cast<string>());
        }

    }
}

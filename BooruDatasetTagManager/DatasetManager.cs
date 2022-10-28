using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooruDatasetTagManager
{
    public class DatasetManager
    {
        public Dictionary<string, DataItem> DataSet;

        public ImageList Images;

        public List<TagValue> AllTags;
        public List<TagValue> CommonTags;
        public DatasetManager()
        {
            DataSet = new Dictionary<string, DataItem>();
            AllTags = new List<TagValue>();
            CommonTags = new List<TagValue>();
        }

        public void SaveAll()
        {
            foreach (var item in DataSet)
            {

                File.WriteAllText(item.Value.TextFilePath, string.Join(", ", item.Value.Tags));
            }
        }

        public void UpdateData()
        {
            if (DataSet.Count == 0)
                return;
            AllTags.Clear();
            CommonTags.Clear();
            bool isFirst = true;
            foreach (var item in DataSet)
            {
                if (isFirst)
                {
                    CommonTags.AddRange(item.Value.Tags);
                }
                foreach (var tag in item.Value.Tags)
                {
                    if (!AllTags.Exists(a=>a.Tag.ToLower() == tag.ToLower()))
                    {
                        AllTags.Add(new TagValue(tag));
                    }
                }
                if (!isFirst)
                {
                    var delTags = GetTagsForDel(CommonTags, item.Value.Tags);
                    foreach (var delTag in delTags)
                    {
                        CommonTags.Remove(delTag);
                    }
                }
                else
                    isFirst = false;
            }
            AllTags.Sort((a, b) => a.Tag.CompareTo(b.Tag));
            CommonTags.Sort((a, b) => a.Tag.CompareTo(b.Tag));
        }

        public void AddTagToAll(string tag, AddingType addType)
        {
            tag = tag.ToLower();
            foreach (var item in DataSet)
            {
                if (!item.Value.Tags.Contains(tag))
                {
                    if (addType == AddingType.Down)
                        item.Value.Tags.Add(tag);
                    else if (addType == AddingType.Top)
                        item.Value.Tags.Insert(0, tag);
                    else if (addType == AddingType.Center)
                    {
                        item.Value.Tags.Insert(item.Value.Tags.Count/2, tag);
                    }
                }
                else if (addType != AddingType.Down)
                {
                    item.Value.Tags.Remove(tag);
                    int index = 0;
                    if (addType == AddingType.Center)
                        index = item.Value.Tags.Count / 2;
                    item.Value.Tags.Insert(index, tag);
                }
            }
        }

        public void DeleteTagFromAll(string tag)
        {
            tag = tag.ToLower();
            foreach (var item in DataSet)
            {
                if (item.Value.Tags.Contains(tag))
                    item.Value.Tags.Remove(tag);
            }
        }

        public void ReplaceTagInAll(string srcTag, string dstTag)
        {
            srcTag = srcTag.ToLower();
            dstTag = dstTag.ToLower();
            foreach (var item in DataSet)
            {
                int index = item.Value.Tags.IndexOf(srcTag);
                if (index != -1)
                {
                    int dstIndex = item.Value.Tags.IndexOf(dstTag);
                    if (dstIndex == -1)
                        item.Value.Tags[index] = dstTag;
                    else
                    {
                        item.Value.Tags.RemoveAt(index);
                    }
                }
            }
        }
        public List<string> FindTag(string tag)
        {
            List<string> foundedTags = new List<string>();
            foreach (var item in DataSet)
            {
                if (item.Value.Tags.Contains(tag.ToLower()))
                    foundedTags.Add(item.Key);
            }
            return foundedTags;
        }

        private List<TagValue> GetTagsForDel(List<TagValue> checkedList, List<string> srcList)
        {
            List<TagValue> delList = new List<TagValue>();
            foreach (var item in checkedList)
            {
                if (!srcList.Contains(item.Tag))
                    delList.Add(item);
            }
            return delList;
        }

        public void LoadFromFolder(string folder)
        {
            string[] imgs = Directory.GetFiles(folder, "*.png", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < imgs.Length; i++)
            {
                var dt = new DataItem(imgs[i]);
                DataSet.Add(dt.Name, dt );
            }
            Images = GetImageList(130, 130);
        }

        private ImageList GetImageList(int w, int h)
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(w, h);
            foreach (var item in DataSet)
            {
                imgList.Images.Add(item.Key, item.Value.Img);
            }
            return imgList;
        }

        public void UpdateImageList(int w, int h)
        {
            Images.Images.Clear();
            Images = GetImageList(w, h);
        }



        public void LoadLossFromFile(string fPath)
        {
            string lossStatPrefix = "Loss statistics for file ";
            string lossPrefix = "loss";
            string lossPattern = "loss:([0-9]*[.]?[0-9]+)±";
            string lastlossPrefix = "recent";
            string lastLossPattern = "recent \\d+ loss:([0-9]*[.]?[0-9]+)±";

            string[] lines = File.ReadAllLines(fPath);
            for (int i = 0; i+2 < lines.Length; i++)
            {
                if (lines[i].StartsWith(lossStatPrefix))
                {
                    string fName = Path.GetFileNameWithoutExtension(lines[i].Replace(lossStatPrefix, ""));
                    if (lines[i + 1].StartsWith(lossPrefix))
                    {
                        var m1 = Regex.Match(lines[i + 1], lossPattern, RegexOptions.IgnoreCase);
                        if (m1.Success)
                        {
                            float loss = (float)Convert.ToDouble(m1.Groups[1].Value.Replace('.', ','));

                            if (lines[i + 2].StartsWith(lastlossPrefix))
                            {
                                var m2 = Regex.Match(lines[i + 2], lastLossPattern, RegexOptions.IgnoreCase);
                                if (m2.Success)
                                {
                                    float lastLoss = (float)Convert.ToDouble(m2.Groups[1].Value.Replace('.', ','));
                                    if (DataSet.ContainsKey(fName))
                                    {
                                        DataSet[fName].Loss = loss;
                                        DataSet[fName].LastLoss = lastLoss;
                                        i += 2;
                                    }
                                    else
                                        continue;
                                }
                                else
                                    continue;
                            }
                            else
                                continue;
                        }
                        else
                            continue;
                    }
                    else
                        continue;
                }
                else
                    continue;
            }
        }


        public enum AddingType
        {
            Top,
            Center,
            Down
        }


        public class DataItem
        {
            public string Name { get; set; }
            public Image Img { get; set; }
            public List<string> Tags { get; set; }
            public string TextFilePath { get; set; }

            public float Loss { get; set; }
            public float LastLoss { get; set; }

            public DataItem()
            {
                Tags = new List<string>();
                Loss = -1;
                LastLoss = -1;
            }

            public DataItem(string imagePath)
            {
                Tags = new List<string>();
                Name = Path.GetFileNameWithoutExtension(imagePath);
                TextFilePath = Path.Combine(Path.GetDirectoryName(imagePath), Name + ".txt");
                GetTagsFromFile();
                Img = Image.FromFile(imagePath);
            }

            public void GetTagsFromFile()
            {
                string text = File.ReadAllText(TextFilePath);
                Tags = text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                for (int i = 0; i < Tags.Count; i++)
                    Tags[i] = Tags[i].Trim();
            }
        }
    }


}

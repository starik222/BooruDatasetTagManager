using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooruDatasetTagManager
{
    public class DatasetManager
    {
        public ConcurrentDictionary<string, DataItem> DataSet;
        public List<TagValue> AllTags;
        public List<TagValue> CommonTags;

        private int originalHash;

        public bool IsLossLoaded { get; private set; }

        private FilterType lastAndOperation = FilterType.Or;
        private IEnumerable<string> lastTagsFilter = null;

        public DatasetManager()
        {
            DataSet = new ConcurrentDictionary<string, DataItem>();
            AllTags = new List<TagValue>();
            CommonTags = new List<TagValue>();
        }

        public bool SaveAll()
        {
            bool saved = false;
            foreach (var item in DataSet)
            {
                if (item.Value.IsModified)
                {
                    item.Value.DeduplicateTags();
                    File.WriteAllText(item.Value.TextFilePath, item.Value.Tags.ToString());
                    saved = true;
                }
            }
            return saved;
        }

        public void UpdateData()
        {
            AllTags = DataSet
                .SelectMany(x => x.Value.Tags.TextTags)
                .Distinct()
                .OrderBy(x => x)
                .Select(x => new TagValue(x))
                .ToList();
            CommonTags = DataSet
                .Skip(1).Aggregate(
                    new HashSet<string>(DataSet.First().Value.Tags.TextTags),
                    (h, e) => { h.IntersectWith(e.Value.Tags.TextTags); return h; }
                )
                .OrderBy(x => x)
                .Select(x => new TagValue(x))
                .ToList();
        }

        public List<TagValue> GetFilteredAllTags(string filterText)
        {
            return AllTags.Where(a=>a.Tag.Contains(filterText)).ToList();
        }


        public bool Remove(string name)
        {
            return DataSet.TryRemove(name, out _);
        }

        private IEnumerable<DataItem> GetEnumerator(bool useFilter)
        {
            IEnumerable<DataItem> lst = null;
            if (useFilter)
            {
                lst = FilterLogic(lastAndOperation, lastTagsFilter);
            }
            else
                lst = DataSet.Select(a => a.Value);
            return lst;
        }

        public void AddTagToAll(string tag, bool skipExist, AddingType addType, int pos=-1, bool useFilter = false)
        {
            IEnumerable<DataItem> lst = GetEnumerator(useFilter);

            foreach (var item in lst)
            {
                item.Tags.AddTag(tag, skipExist, addType, pos);
            }
        }

        public void SetTagListToAll(List<string> tags, bool onlyEmpty)
        {
            foreach (var item in DataSet)
            {
                if (onlyEmpty)
                {
                    if (item.Value.Tags.Count == 0)
                    {
                        item.Value.Tags.AddRange(tags, true);
                    }
                }
                else
                {
                    item.Value.Tags.Clear();
                    item.Value.Tags.AddRange(tags, true);
                }
            }
        }

        public List<DataItem> GetDataSourceWithLastFilter(OrderType orderBy = OrderType.Name)
        {
            return GetDataSource(orderBy, lastAndOperation, lastTagsFilter);
        }

        /// <summary>
        /// Retrieves a list of DataItem objects, filtered and ordered based on the given parameters.
        /// </summary>
        /// <param name="orderBy">An optional parameter that specifies how the resulting list should be sorted.</param>
        /// <param name="andOp">An optional parameter that determines the logical operation to be used when filtering by tags.</param>
        /// <param name="filterByTags">An optional parameter that contains a list of tags to filter the data items by.</param>
        /// <returns>A filtered and ordered list of DataItem objects.</returns>
        public List<DataItem> GetDataSource(OrderType orderBy = OrderType.Name, FilterType andOp = FilterType.Or, IEnumerable<string> filterByTags = null)
        {
            // Store the last set of tags used for filtering. FilterLogic will use this value unless passed custom one
            lastTagsFilter = filterByTags;

            // Declare a list to store the filtered and ordered DataItem objects.
            List<DataItem> items = FilterLogic(andOp);

            // Sort the data items based on the orderBy parameter.
            switch (orderBy)
            {
                case OrderType.Name:
                    {
                        // Sort data items by their Name property using a custom string comparison method.
                        items.Sort((a, b) => FileNamesComparer.StrCmpLogicalW(a.Name, b.Name));
                        break;
                    }
                case OrderType.Loss:
                    {
                        // Sort data items by their Loss property.
                        items.Sort((a, b) => a.Loss.CompareTo(b.Loss));
                        break;
                    }
                case OrderType.LastLoss:
                    {
                        // Sort data items by their LastLoss property.
                        items.Sort((a, b) => a.LastLoss.CompareTo(b.LastLoss));
                        break;
                    }
                case OrderType.ImageModifyTime:
                    {
                        // Sort data items by their ImageModifyTime property.
                        items.Sort((a, b) => a.ImageModifyTime.CompareTo(b.ImageModifyTime));
                        break;
                    }
                case OrderType.TagsModifyTime:
                    {
                        // Sort data items by their TagsModifyTime property.
                        items.Sort((a, b) => a.TagsModifyTime.CompareTo(b.TagsModifyTime));
                        break;
                    }
            }
            // Return the filtered and sorted list of DataItem objects.
            return items;
        }

        public List<DataItem> FilterLogic(FilterType andOp = FilterType.Or, IEnumerable<string> filterByTags = null)
        {
            List<DataItem> items = null;
            if (filterByTags != null)
                lastTagsFilter = filterByTags;
            // Check if there are tags to filter by.
            if (lastTagsFilter != null)
            {
                switch (andOp)
                {
                    case FilterType.And:
                        // If the logical operation is AND, filter the data items by requiring all tags to be present.
                        items = DataSet.Values.Where(a => lastTagsFilter.All(t => a.Tags.Contains(t))).ToList();
                        break;
                    case FilterType.Or:
                        // If the logical operation is OR, filter the data items by requiring at least one tag to be present.
                        items = DataSet.Values.Where(a => lastTagsFilter.Any(t => a.Tags.Contains(t))).ToList();
                        break;
                    case FilterType.Not:
                        // If the logical operation is NOT, filter the data items by requiring none of the tags to be present.
                        items = DataSet.Values.Where(a => lastTagsFilter.All(t => !a.Tags.Contains(t))).ToList();
                        break;
                    case FilterType.Xor:
                        // If the logical operation is XOR, filter the data items by requiring exactly one tag to be present.
                        items = DataSet.Values.Where(a => lastTagsFilter.Count(t => a.Tags.Contains(t)) == 1).ToList();
                        break;
                    default:
                        throw new ArgumentException($"Invalid filter type: {andOp}");
                }
                // Store the last logical operation used for filtering, moved here so it is only updated if we actually perform the operation
                lastAndOperation = andOp;
            }
            // If there are no tags to filter by, return all data items.
            else
                items = DataSet.Values.ToList();

            return items;
        }

        public void DeleteTagFromAll(string tag, bool useFilter = false)
        {
            tag = tag.ToLower();
            IEnumerable<DataItem> lst = GetEnumerator(useFilter);

            foreach (var item in lst)
            {
                item.Tags.RemoveTag(tag, true);
            }
            UpdateData();
        }



        public void ReplaceTagInAll(string srcTag, string dstTag, bool useFilter = false)
        {
            srcTag = srcTag.ToLower();
            dstTag = dstTag.ToLower();
            IEnumerable<DataItem> lst = GetEnumerator(useFilter);

            foreach (var item in lst)
            {
                item.Tags.ReplaceTag(srcTag, dstTag);
            }
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

        public bool LoadFromFolder(string folder)
        {
            List<string> imagesExt = new List<string>() { ".jpg", ".png", ".bmp", ".jpeg", ".webp" };
            string[] imgs = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories);
            if (imgs.Length == 0)
            {
                IsLossLoaded = false;
                return false;
            }
            imgs = imgs.Where(a => imagesExt.Contains(Path.GetExtension(a).ToLower())).OrderBy(a => a, new FileNamesComparer()).ToArray();
            int imgSize = Program.Settings.PreviewSize;
            imgs.AsParallel().ForAll(x =>
            {
                var dt = new DataItem(x, imgSize);
                DataSet.TryAdd(dt.ImageFilePath, dt);
            });
            UpdateDatasetHash();
            IsLossLoaded = false;
            return true;
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

        public bool IsDataSetChanged()
        {
            return !originalHash.Equals(GetHashCode());
        }

        public void UpdateDatasetHash()
        {
            originalHash = GetHashCode();
        }

        public void LoadLossFromFile(string fPath)
        {
            string lossStatPrefix = "Loss statistics for file ";
            string lossPrefix = "loss";
            string lossPattern = "loss:([0-9]*[.]?[0-9]+)±";
            string lastlossPrefix = "recent";
            string lastLossPattern = "recent \\d+ loss:([0-9]*[.]?[0-9]+)±";
            try
            {
                string[] lines = File.ReadAllLines(fPath);
                for (int i = 0; i + 2 < lines.Length; i++)
                {
                    if (!lines[i].StartsWith(lossStatPrefix))
                        continue;
                    string fName = lines[i].Replace(lossStatPrefix, "");
                    if (!lines[i + 1].StartsWith(lossPrefix))
                        continue;
                    var m1 = Regex.Match(lines[i + 1], lossPattern, RegexOptions.IgnoreCase);
                    if (!m1.Success)
                        continue;
                    float loss = (float)Convert.ToDouble(m1.Groups[1].Value.Replace('.', ','));

                    if (!lines[i + 2].StartsWith(lastlossPrefix))
                        continue;
                    var m2 = Regex.Match(lines[i + 2], lastLossPattern, RegexOptions.IgnoreCase);
                    if (!m2.Success)
                        continue;
                    float lastLoss = (float)Convert.ToDouble(m2.Groups[1].Value.Replace('.', ','));
                    if (!DataSet.ContainsKey(fName))
                        continue;
                    DataSet[fName].Loss = loss;
                    DataSet[fName].LastLoss = lastLoss;
                    i += 2;

                }
            }
            catch
            {

            }
            IsLossLoaded = true;
        }

        public override int GetHashCode()
        {
            int result = 0;
            unchecked
            {
                foreach (var item in DataSet)
                    result = result * 31 + item.Value.GetHashCode();
            }
            return result;
        }

        public enum AddingType
        {
            Top,
            Center,
            Down,
            Custom
        }

        public enum OrderType
        {
            Name, 
            Loss,
            LastLoss,
            ImageModifyTime,
            TagsModifyTime
        }

        public class DataItem
        {
            [DisplayName("Image")]
            public Image Img { get; set; }
            public string Name { get; set; }
            [Browsable(false)]
            public EditableTagList Tags { get; set; }
            [Browsable(false)]
            public string TextFilePath { get; set; }
            //[Browsable(false)]
            public string ImageFilePath { get; set; }

            public float Loss { get; set; }
            public float LastLoss { get; set; }

            public DateTime ImageModifyTime { get; set; }
            public DateTime TagsModifyTime { get; set; }
            [Browsable(false)]
            public bool IsModified
            {
                get
                {
                    return originalHash != GetHashCode();
                }
            }

            private int originalHash;

            public DataItem()
            {
                Tags = new EditableTagList();
                Loss = -1;
                LastLoss = -1;
            }

            public DataItem(string imagePath, int imageSize)
            {
                Tags = new EditableTagList();
                ImageFilePath = imagePath;
                Name = Path.GetFileNameWithoutExtension(imagePath);
                ImageModifyTime = File.GetLastWriteTime(imagePath);
                TextFilePath = Path.Combine(Path.GetDirectoryName(imagePath), Name + ".txt");
                GetTagsFromFile();
                Img = MakeThumb(imagePath, imageSize);
            }

            public void DeduplicateTags()
            {
                if (IsModified)
                {
                    Tags.DeduplicateTags();
                }
            }


            Image MakeThumb(string imagePath, int imgSize)
            {

                using (var img = Extensions.GetImageFromFile(imagePath))
                {
                    var aspect = img.Width / (float)img.Height;

                    int newHeight = img.Height * imgSize / img.Width;
                    int newWidth = imgSize;

                    if (newHeight > imgSize)
                    {
                        newWidth = img.Width * imgSize / img.Height;
                        newHeight = imgSize;
                    }

                    return img.GetThumbnailImage(newWidth, newHeight, () => false, IntPtr.Zero);
                }
            }
            public void GetTagsFromFile()
            {
                if (File.Exists(TextFilePath))
                {
                    TagsModifyTime = File.GetLastWriteTime(TextFilePath);
                    string text = File.ReadAllText(TextFilePath);

                    var temp_tags = PromptParser.ParsePrompt(text, Program.Settings.SeparatorOnLoad);
                    Tags = new EditableTagList(temp_tags);
                }
                else
                {
                    Tags = new EditableTagList();
                    TagsModifyTime = DateTime.MinValue;
                }

                originalHash = GetHashCode();
            }

            public override string ToString()
            {
                return String.Join(Program.Settings.SeparatorOnSave, Tags);
            }

            public override int GetHashCode()
            {
                return ToString().GetHashCode();
            }
        }
    }


}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooruDatasetTagManager
{
    public class ImageSorter
    {
        public string RootDir { get; set; }
        public SortItem RootItem { get; set; }

        public Dictionary<string,List<string>> FileQueue { get; set; }
        private Dictionary<string, SortItem> indexedSortItems;

        public long FileIndex { get; set; } = 1;

        public ImageSorter(string rootDir)
        {
            RootDir = rootDir;
            RootItem = new SortItem();
            RootItem.Name = "Root";
            RootItem.Id = "Root";
            RootItem.Level = 0;
            FileQueue = new Dictionary<string, List<string>>();
            indexedSortItems = new Dictionary<string, SortItem>();
        }

        public void StartCopy()
        {
            foreach (var item in FileQueue)
            {
                string dstDir = Path.Combine(RootDir, indexedSortItems[item.Key].Path);
                Directory.CreateDirectory(dstDir);
                foreach (var file in item.Value)
                {
                    File.Copy(file, GetDstFile(file, dstDir));
                }
            }
            FileQueue.Clear();
        }

        public async Task StartCopyAsync()
        {
            await Task.Run(() => StartCopy());
        }

        private string GetDstFile(string origPath, string dstDir)
        {
            string dstFile = Path.Combine(dstDir, FileIndex.ToString() + Path.GetExtension(origPath));
            FileIndex++;
            if (File.Exists(dstFile))
            {
                int dIndex = 1;
                do
                {
                    dstFile = Path.Combine(dstDir, FileIndex.ToString() + "_(" + (dIndex++) + ")" + Path.GetExtension(origPath));
                } 
                while (File.Exists(dstFile));
            }
            return dstFile;
        }

        public void CreateFromTreeNode(TreeNode node)
        {
            if (node.Parent == null && node.Name == "Root")
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    RootItem.AddChild(childNode);
                }
            }
            indexedSortItems.Clear();
            UpdateSortItemIndex(RootItem);
        }

        public void UpdateSortItemIndex(SortItem item)
        {
            indexedSortItems.Add(item.Id, item);
            foreach (var childItem in item.Items)
            {
                UpdateSortItemIndex(childItem);
            }
        }

        public void AddFileQueue(string element, string filePath)
        {
            if (!FileQueue.ContainsKey(element))
                FileQueue.Add(element, new List<string>());
            if (!FileQueue[element].Contains(filePath))
                FileQueue[element].Add(filePath);
        }

        public void AddFileRangeQueue(string element, IEnumerable<string> filesPath)
        {
            if (!FileQueue.ContainsKey(element))
                FileQueue.Add(element, new List<string>());
            foreach (var item in filesPath)
            {
                if (!FileQueue[element].Contains(item))
                    FileQueue[element].Add(item);
            }
        }

        public class SortItem
        {
            public SortItem()
            {
                Items = new List<SortItem>();
            }
            public string Name { get; set; }
            public string Id { get; set; }
            public int Level { get; set; }
            public SortItem Parent { get; set; }
            public List<SortItem> Items { get; }

            public string Path
            {
                get
                {
                    List<string> tempLst = new List<string>();
                    SortItem curItem = this;
                    while (curItem.Parent != null)
                    {
                        tempLst.Add(curItem.Name);
                        curItem = curItem.Parent;
                    }
                    tempLst.Reverse();
                    return string.Join("\\", tempLst);
                }
            }

            public void AddChild(string name)
            {
                SortItem childItem = new SortItem();
                childItem.Parent = this;
                childItem.Name = name;
                childItem.Level = this.Level + 1;
                Items.Add(childItem);
            }

            public void AddChild(TreeNode node)
            {
                SortItem childItem = new SortItem();
                childItem.Parent = this;
                childItem.Name = node.Text;
                childItem.Id = node.Name;
                childItem.Level = this.Level + 1;
                if (node.Nodes != null && node.Nodes.Count > 0)
                {
                    foreach (TreeNode childNode in node.Nodes)
                    {
                        childItem.AddChild(childNode);
                    }
                }
                Items.Add(childItem);
            }
        }
    }
}

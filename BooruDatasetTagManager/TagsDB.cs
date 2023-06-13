using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Translator.Crypto;

namespace BooruDatasetTagManager
{
    [Serializable]
    public class TagsDB
    {
        public int Version;
        public List<TagItem> Tags;
        public Dictionary<string, long> LoadedFiles;
        private Dictionary<long, int> hashes;
        private const int curVersion = 100;

        public TagsDB()
        {
            Version = curVersion;
            Tags = new List<TagItem>();
            LoadedFiles = new Dictionary<string, long>();
            hashes = new Dictionary<long, int>();
        }

        private string[] ReadAllLines(byte[] data, Encoding encoding)
        {
            
            List<string> list = new List<string>();
            using (MemoryStream ms = new MemoryStream(data))
            {
                using (StreamReader streamReader = new StreamReader(ms, encoding))
                {
                    string item;
                    while ((item = streamReader.ReadLine()) != null)
                    {
                        list.Add(item);
                    }
                }
            }
            return list.ToArray();
        }

        public void ClearDb()
        {
            Tags.Clear();
            hashes.Clear();
        }

        public void ResetVersion()
        {
            Version = curVersion;
        }

        public void ClearLoadedFiles()
        {
            LoadedFiles.Clear();
        }

        public void LoadCSVFromDir(string dir)
        {
            FileInfo[] csvFiles = new DirectoryInfo(dir).GetFiles("*.csv", SearchOption.TopDirectoryOnly);
            foreach (var item in csvFiles)
                LoadFromCSVFile(item.FullName);
        }

        public void LoadTxtFromDir(string dir)
        {
            FileInfo[] txtFiles = new DirectoryInfo(dir).GetFiles("*.txt", SearchOption.TopDirectoryOnly);
            foreach (var item in txtFiles)
                LoadFromTxtFile(item.FullName);
        }

        public void LoadFromTxtFile(string fPath, bool append = true)
        {
            byte[] data = File.ReadAllBytes(fPath);
            long hash = Adler32.GenerateHash(data);
            string fName = Path.GetFileName(fPath);
            if (LoadedFiles.ContainsKey(fName))
            {
                if (LoadedFiles[fName] == hash)
                    return;
                else
                    LoadedFiles[fName] = hash;
            }
            else
            {
                LoadedFiles.Add(fName, hash);
            }


            string[] lines = ReadAllLines(data, Encoding.UTF8);
            if (!append)
                ClearDb();
            foreach (var item in lines)
            {
                AddTag(item, 0);
            }
        }


        public void LoadFromCSVFile(string fPath, bool append = true)
        {
            Regex r = new Regex("(.*?),(\\d+),(\\d+),(.*)");
            char[] splitter = { ',' };
            byte[] data = File.ReadAllBytes(fPath);
            long hash = Adler32.GenerateHash(data);
            string fName = Path.GetFileName(fPath);
            if (LoadedFiles.ContainsKey(fName))
            {
                if (LoadedFiles[fName] == hash)
                    return;
                else
                    LoadedFiles[fName] = hash;
            }
            else
            {
                LoadedFiles.Add(fName, hash);
            }


            string[] lines = ReadAllLines(data, Encoding.UTF8);
            if (!append)
                Tags.Clear();
            foreach (var item in lines)
            {
                Match match = r.Match(item);
                if (match.Success)
                {
                    string tagName = match.Groups[1].Value;
                    string[] aliases = match.Groups[4].Value.Replace("\"", "").Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                    AddTag(tagName, Convert.ToInt32(match.Groups[3].Value));
                    foreach (var al in aliases)
                    {
                        AddTag(al, Convert.ToInt32(match.Groups[3].Value), true, tagName);
                    }
                }
            }
        }

        public void SortTags()
        {
            Tags.Sort((a, b) => a.Tag.CompareTo(b.Tag));
        }

        private void AddTag(string tag, int count, bool isAlias = false, string parent = null)
        {
            if (string.IsNullOrWhiteSpace(tag))
                return;
            tag = PrepareTag(tag);
            if (Tags.Exists(a => a.Parent == tag))
                return;
            tag = tag.Trim().ToLower();
            long tagHash = tag.GetHash();

            int existTagIndex = -1;
            TagItem tagItem = null;
            if (hashes.TryGetValue(tagHash, out existTagIndex))
            {
                tagItem = Tags[existTagIndex];
                tagItem.Count += count;
            }
            else
            {
                tagItem = new TagItem();
                tagItem.SetTag(tag);
                tagItem.Count = count;
                tagItem.IsAlias = isAlias;
                tagItem.Parent = PrepareTag(parent);
                hashes.Add(tagItem.TagHash, Tags.Count);
                Tags.Add(tagItem);
            }
        }

        private string PrepareTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                return tag;
            tag = tag.Replace('_', ' ');
            tag = tag.Replace("\\(", "(");
            tag = tag.Replace("\\)", ")");
            return tag;
        }

        public bool IsNeedUpdate(string dirToCheck)
        {
            if (Version != curVersion)
                return true;
            FileInfo[] tagFiles = new DirectoryInfo(dirToCheck).GetFiles("*.csv", SearchOption.TopDirectoryOnly).
                Concat(new DirectoryInfo(dirToCheck).GetFiles("*.txt", SearchOption.TopDirectoryOnly)).ToArray();
            if (tagFiles.Length == 0)
                return false;
            if (LoadedFiles.Count != tagFiles.Length)
                return true;
            foreach (var item in tagFiles)
            {
                byte[] data = File.ReadAllBytes(item.FullName);
                long hash = Adler32.GenerateHash(data);
                if (!LoadedFiles.ContainsKey(item.Name))
                    return true;
                if(LoadedFiles[item.Name]!=hash) 
                    return true;
            }
            return false;
        }

        public void LoadTranslation(TranslationManager transManager)
        {
            bool onlyManual = Program.Settings.OnlyManualTransInAutocomplete;
            foreach (var tag in Tags)
            {
                tag.Translation = transManager.GetTranslation(tag.TagHash, onlyManual);
            }
        }

        public static TagsDB LoadFromTagFile(string fPath)
        {
            if (File.Exists(fPath))
            {
                try
                {
                    return (TagsDB)Extensions.LoadDataSet(File.ReadAllBytes(fPath));
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
                return new TagsDB();
        }

        public void SaveTags(string fPath)
        {
            Extensions.SaveDataSet(this, fPath);
        }

        [Serializable]
        public class TagItem
        {
            public string Tag { get; private set; }
            public long TagHash { get; private set; }
            public int Count;
            //public List<string> Aliases;
            public bool IsAlias;
            public string Parent;

            public string Translation;

            public TagItem()
            {
                //Aliases = new List<string>();
            }

            public void SetTag(string tag)
            {
                Tag = tag.Trim().ToLower();
                TagHash = Tag.GetHash();
            }

            public string GetTag()
            {
                if (IsAlias)
                    return Parent;
                else
                    return Tag;
            }

            public override string ToString()
            {
                if (IsAlias)
                    return $"{Tag} -> {Parent}{$" ({Count})"}{(string.IsNullOrEmpty(Translation) ? "" : $" [{Translation}]")}";
                else
                    return $"{Tag}{$" ({Count})"}{(string.IsNullOrEmpty(Translation) ? "" : $" [{Translation}]")}";
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static BooruDatasetTagManager.DatasetManager;

namespace BooruDatasetTagManager
{
    [Serializable]
    public class MultiSelectDataTable : DataTable
    {
        private List<DataItem> selectedDataItems;
        private bool isTranslateMode = false;
        public MultiSelectDataTable()
            : base()
        {
        }

        public MultiSelectDataTable(string tableName)
            : base(tableName)
        {
        }

        public MultiSelectDataTable(string tableName, string tableNamespace)
            : base(tableName, tableNamespace)
        {
        }

        protected override Type GetRowType()
        {
            return typeof(MultiSelectDataRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new MultiSelectDataRow(builder);
        }

        public void SetTranslationMode(bool translate)
        {
            this.isTranslateMode = translate;
        }

        public async Task CreateTableFromSelectedImages(List<DataItem> selectedDI)
        {
            selectedDataItems = selectedDI;
            selectedDataItems.Sort((a, b) => FileNamesComparer.StrCmpLogicalW(a.Name, b.Name));
            Dictionary<string, List<KeyValuePair<int, DataItem>>> table = new Dictionary<string, List<KeyValuePair<int, DataItem>>>();
            int maxCount = selectedDataItems.Max(a => a.Tags.Count);

            for (int i = 0; i < maxCount; i++)
            {
                for (int j = 0; j < selectedDataItems.Count; j++)
                {
                    var curTags = selectedDataItems[j];
                    if (i < curTags.Tags.Count)
                    {
                        if (table.ContainsKey(curTags.Tags[i].Tag))
                        {
                            table[curTags.Tags[i].Tag].Add(new KeyValuePair<int, DataItem>(i, curTags));
                        }
                        else
                        {
                            table.Add(curTags.Tags[i].Tag, new List<KeyValuePair<int, DataItem>>() { new KeyValuePair<int, DataItem>(i, curTags) });
                        }
                    }
                }
            }
            
            Columns.Add("Tag", typeof(string));
            Columns.Add("Image", typeof(string));
            Columns.Add("ImageName", typeof(string));
            if (isTranslateMode)
            {
                Columns.Add("Translation", typeof(string));
            }
            foreach (var item in table)
            {
                item.Value.Sort((x, y) => x.Value.Name.CompareTo(y.Value.Name));
                for (int i = 0; i < item.Value.Count; i++)
                {
                    MultiSelectDataRow row = (MultiSelectDataRow)NewRow();
                    row.SetAttribute("TextTag", item.Key);
                    row.SetAttribute("TagIndex", item.Value[i].Key);
                    row.SetAttribute("DataItem", item.Value[i].Value);
                    row["Tag"] = i == 0 ? item.Key : "";//tag
                    if (isTranslateMode)
                    {
                        row["Translation"] = i == 0 ? await Program.TransManager.TranslateAsync(item.Key) : "";//tag
                    }
                    row["Image"] = item.Value[i].Value.ImageFilePath;//ImgName
                    row["ImageName"] = item.Value[i].Value.Name;//ImgName
                    Rows.Add(row);
                }
            }
        }

        protected override void OnRowChanged(DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add)
            {

            }
            else if(e.Action!= DataRowAction.Change)
            {

            }
            if (e.Action == DataRowAction.Change)
            {
                if ((string)e.Row["Tag"] != ((MultiSelectDataRow)e.Row).GetTagText())
                {
                    ((MultiSelectDataRow)e.Row).GetDataItem().Tags[((MultiSelectDataRow)e.Row).GetTagIndex()].Tag = (string)e.Row["Tag"];
                }
            }
            base.OnRowChanged(e);
        }

        protected override void OnRowDeleting(DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Delete)
            {
                string textTag = ((MultiSelectDataRow)e.Row).GetTagText();
                int rowIndex = e.Row.Table.Rows.IndexOf(e.Row);
                if (rowIndex + 1 < Rows.Count)
                {
                    if (!string.IsNullOrWhiteSpace((string)e.Row["Tag"]) && ((MultiSelectDataRow)Rows[rowIndex + 1]).GetTagText() == textTag)
                    {
                        Rows[rowIndex + 1]["Tag"] = textTag;
                    }
                }
            }
            base.OnRowDeleting(e);
        }

        protected override void OnRowDeleted(DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Delete)
            {
                int tagIndex = ((MultiSelectDataRow)e.Row).GetTagIndex();
                string textTag = ((MultiSelectDataRow)e.Row).GetTagText();
                DataItem dataItem = ((MultiSelectDataRow)e.Row).GetDataItem();
                dataItem.Tags.RemoveAt(tagIndex);

                //Changing TagIndex after deleting tags
                for (int i = 0; i < Rows.Count; i++)
                {
                    var row = (MultiSelectDataRow)Rows[i];
                    if (row.GetDataItem().Equals(dataItem) && row.GetTagIndex() > tagIndex && row.GetTagText() != textTag)
                    {
                        row.SetTagIndex(row.GetTagIndex() - 1);
                    }
                }
            }
            base.OnRowDeleted(e);
        }


        public void SetTagValue(string tag, int index)
        {
            if (tag != ((MultiSelectDataRow)Rows[index]).GetTagText())
            {
                ((MultiSelectDataRow)Rows[index]).GetDataItem().Tags[index].Tag = tag;
            }
        }

        public void AddTag(string tag, bool skipExist, AddingType addType, int pos = -1)
        {
            if (selectedDataItems.Count == 0)
                return;
            bool existMode = false;
            List<KeyValuePair<int, DataItem>> addedItems = new List<KeyValuePair<int, DataItem>>();
            foreach (var item in selectedDataItems)
            {
                if (!existMode)
                {
                    if (item.Tags.Contains(tag))
                    {
                        existMode = true;
                    }
                }
                int addedIndex = item.Tags.AddTag(tag, skipExist, addType, pos);
                if (addedIndex != -1)
                {
                    addedItems.Add(new KeyValuePair<int, DataItem>(addedIndex, item));
                }
            }

            if (!existMode)
            {
                for (int i = 0; i < addedItems.Count; i++)
                {
                    {
                        MultiSelectDataRow row = (MultiSelectDataRow)NewRow();
                        row.SetAttribute("TextTag", tag);
                        row.SetAttribute("TagIndex", addedItems[i].Key);
                        row.SetAttribute("DataItem", addedItems[i].Value);
                        row["Tag"] = i == 0 ? tag : "";//tag
                        row["Image"] = addedItems[i].Value.ImageFilePath;//ImgName
                        row["ImageName"] = addedItems[i].Value.Name;//ImgName
                        //if (isTranslateMode)
                        //{
                        //    row["Translation"] = i == 0 ? await Program.TransManager.TranslateAsync(tag) : "";//tag
                        //}
                        Rows.Add(row);
                    }
                }
            }
            else
            {
                List<int> existIndexes = new List<int>();
                for (int i = 0; i < Rows.Count; i++)
                {
                    if (((MultiSelectDataRow)Rows[i]).GetTagText() == tag)
                    {
                        existIndexes.Add(i);
                    }
                }
                if (existIndexes.Count == 0)
                    throw new Exception("Exist mode but tag not found in table!");
                int startInsertIndex = existIndexes.Max() + 1;
                foreach (var item in addedItems)
                {
                    bool found = false;
                    foreach (var eIndex in existIndexes)
                    {
                        if (((MultiSelectDataRow)Rows[eIndex]).GetDataItem() == item.Value)
                        {
                            ((MultiSelectDataRow)Rows[eIndex]).ExtendedProperties["TagIndex"] = item.Key;
                            found = true; 
                            break;
                        }
                    }
                    if (!found)
                    {
                        MultiSelectDataRow row = (MultiSelectDataRow)NewRow();
                        row.SetAttribute("TextTag", tag);
                        row.SetAttribute("TagIndex", item.Key);
                        row.SetAttribute("DataItem", item.Value);
                        row["Tag"] = "";//tag
                        row["Image"] = item.Value.ImageFilePath;//ImgName
                        row["ImageName"] = item.Value.Name;//ImgName
                        Rows.InsertAt(row, startInsertIndex++);
                    }
                }
            }

            //Changing TagIndex after adding tags
            for (int i = 0; i < Rows.Count; i++)
            {
                var row = (MultiSelectDataRow)Rows[i];
                var indexForAddition = addedItems.Find(a => a.Value.Equals(row.GetDataItem())).Key;
                if (row.GetTagIndex() >= indexForAddition && row.GetTagText() != tag)
                {
                    row.SetTagIndex(row.GetTagIndex() + 1);
                }
            }
        }
    }

    [Serializable]
    public class MultiSelectDataRow : DataRow
    {
        public Dictionary<string, object> _extendedProperties = new Dictionary<string, object>();

        public Dictionary<string, object> ExtendedProperties
        {
            get { return _extendedProperties; }
        }

        public void SetAttribute(string name, object value)
        {
            ExtendedProperties[name] = value;
        }

        public MultiSelectDataRow()
            : base(null)
        {
        }

        public MultiSelectDataRow(DataRowBuilder builder)
            : base(builder)
        {
        }


        public DataItem GetDataItem()
        {
            if (ExtendedProperties["DataItem"] != null)
                return (DataItem)ExtendedProperties["DataItem"];
            else
                return null;
        }

        public int GetTagIndex()
        {
            if (ExtendedProperties["TagIndex"] != null)
                return (int)ExtendedProperties["TagIndex"];
            else
                return -1;
        }

        public void SetTagIndex(int tagIndex)
        {
            if (ExtendedProperties["TagIndex"] != null)
                ExtendedProperties["TagIndex"] = tagIndex;
            else
                throw new Exception("TagIndex not found in ExtendedProperties");
        }

        public string GetTagText()
        {
            if (ExtendedProperties["TextTag"] != null)
                return (string)ExtendedProperties["TextTag"];
            else
                return null;
        }
    }
}

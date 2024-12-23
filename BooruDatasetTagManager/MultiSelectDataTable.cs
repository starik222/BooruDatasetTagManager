using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
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
        private Dictionary<string, int> tagCount = new Dictionary<string, int>();
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

        public int GetTagsCount(string tag)
        {
            if (tagCount.ContainsKey(tag))
                return tagCount[tag];
            else
                return 0;
        }
        //For debug
        public void CheckIndexSync()
        {
            for (int i = 0; i < Rows.Count; i++)
            {
                var row = (MultiSelectDataRow)Rows[i];
                string textTag = row.GetTagText();
                DataItem di = row.GetDataItem();
                int index = row.GetTagIndex();
                if (index >= di.Tags.Count)
                    throw new Exception("index out of range");
                if (di.Tags[index].Tag != textTag)
                {
                    throw new Exception("Bad index");
                }
            }
        }

        public void EndEdit()
        {
            for (int i = 0; i < Rows.Count; i++)
            {
                Rows[i].EndEdit();
            }
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
                    IncreaseTagCount(item.Key);
                }
            }
#if DEBUG
            CheckIndexSync();
#endif
        }

        private void IncreaseTagCount(string tag)
        {
            if (tagCount.ContainsKey(tag))
                tagCount[tag]++;
            else
                tagCount[tag] = 1;
        }

        private void DecreaseTagCount(string tag)
        {
            if (tagCount.ContainsKey(tag))
                tagCount[tag]--;
        }

        protected override void OnRowChanged(DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Change)
            {
                var rowData = (MultiSelectDataRow)e.Row;
                if ((string)e.Row["Tag"] != rowData.GetTagText())
                {
                    var eTag = rowData.GetDataItem().Tags[rowData.GetTagIndex()];
                    DecreaseTagCount(eTag.Tag);
                    eTag.Tag = (string)e.Row["Tag"];
                    if (eTag.IsEditing)
                        eTag.EndEdit();
                    rowData.SetTagText((string)e.Row["Tag"]);
                    IncreaseTagCount(eTag.Tag);
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
                DecreaseTagCount(textTag);
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
#if DEBUG
            CheckIndexSync();
#endif
        }

        public List<DataItem> GetSelectedDataItems()
        {
            return selectedDataItems;
        }
        public void SetTagValue(string tag, int index)
        {
            var rowData = ((MultiSelectDataRow)Rows[index]);
            if (tag != rowData.GetTagText())
            {
                rowData.GetDataItem().Tags[index].Tag = tag;
                rowData.SetTagText(tag);
            }
        }

        public void UpdateDataForTag(string tag, List<KeyValuePair<DatasetManager.DataItem, bool>> data)
        {
            var lstToAdd = data.Where(a => a.Value).Select(a => a.Key).ToList();
            var lstToRemove = data.Where(a => !a.Value).Select(a=>a.Key).ToList();
            AddTag(tag, lstToAdd, true, AddingType.Down);

            for (int i = Rows.Count - 1; i >= 0; i--)
            {
                var dr = (MultiSelectDataRow)Rows[i];
                var drTag = dr.GetTagText();
                var drDataItem = dr.GetDataItem();
                if (tag == drTag && lstToRemove.Contains(drDataItem))
                {
                    this.Rows.Remove(dr);
                }
            }
        }

        public void AddTag(string tag, bool skipExist, AddingType addType, int pos = -1)
        {
            AddTag(tag, selectedDataItems, skipExist, addType, pos);
        }

        public void AddTag(string tag, List<DataItem> dataItemToUpdate, bool skipExist, AddingType addType, int pos = -1)
        {
            if (dataItemToUpdate.Count == 0)
                return;
            bool existMode = false;
            List<KeyValuePair<KeyValuePair<int, int>, DataItem>> addedItems = new List<KeyValuePair<KeyValuePair<int, int>, DataItem>>();
            foreach (var item in selectedDataItems)
            {
                if (!existMode)
                {
                    if (item.Tags.Contains(tag))
                    {
                        existMode = true;
                    }
                }
                if (dataItemToUpdate.Contains(item))
                {
                    var addedResult = item.Tags.AddTag(tag, skipExist, addType, pos);
                    if (addedResult.newIndex != -1)
                    {
                        addedItems.Add(new KeyValuePair<KeyValuePair<int, int>, DataItem>(new KeyValuePair<int, int>(addedResult.oldIndex, addedResult.newIndex), item));
                    }
                }
            }

            if (!existMode)
            {
                for (int i = 0; i < addedItems.Count; i++)
                {
                    {
                        MultiSelectDataRow row = (MultiSelectDataRow)NewRow();
                        row.SetAttribute("TextTag", tag);
                        row.SetAttribute("TagIndex", addedItems[i].Key.Value);
                        row.SetAttribute("DataItem", addedItems[i].Value);
                        row["Tag"] = i == 0 ? tag : "";//tag
                        row["Image"] = addedItems[i].Value.ImageFilePath;//ImgName
                        row["ImageName"] = addedItems[i].Value.Name;//ImgName
                        //if (isTranslateMode)
                        //{
                        //    row["Translation"] = i == 0 ? await Program.TransManager.TranslateAsync(tag) : "";//tag
                        //}
                        Rows.Add(row);
                        IncreaseTagCount(tag);
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
                            ((MultiSelectDataRow)Rows[eIndex]).ExtendedProperties["TagIndex"] = item.Key.Value;
                            found = true; 
                            break;
                        }
                    }
                    if (!found)
                    {
                        MultiSelectDataRow row = (MultiSelectDataRow)NewRow();
                        row.SetAttribute("TextTag", tag);
                        row.SetAttribute("TagIndex", item.Key.Value);
                        row.SetAttribute("DataItem", item.Value);
                        row["Tag"] = "";//tag
                        row["Image"] = item.Value.ImageFilePath;//ImgName
                        row["ImageName"] = item.Value.Name;//ImgName
                        Rows.InsertAt(row, startInsertIndex++);
                        IncreaseTagCount(tag);
                    }
                }
            }

            //Changing TagIndex after adding tags
            //MOVE PROBLEM!!!
            for (int i = 0; i < Rows.Count; i++)
            {
                var row = (MultiSelectDataRow)Rows[i];
                int tagIndex = row.GetTagIndex();
                var addRes = addedItems.Find(a => a.Value.Equals(row.GetDataItem())).Key;
                if (addRes.Key == addRes.Value)
                    continue;
                else if (addRes.Key == -1 && tagIndex >= addRes.Value && row.GetTagText() != tag)
                {
                    row.SetTagIndex(tagIndex + 1);
                }
                else if (addRes.Key != -1 && addRes.Key < addRes.Value && tagIndex > addRes.Key && tagIndex <= addRes.Value && row.GetTagText() != tag)
                {
                    row.SetTagIndex(tagIndex - 1);
                }
                else if (addRes.Key != -1 && addRes.Key > addRes.Value && tagIndex >= addRes.Value && tagIndex < addRes.Key && row.GetTagText() != tag)
                {
                    row.SetTagIndex(tagIndex + 1);
                }
            }
#if DEBUG
            CheckIndexSync();
#endif
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
        public void SetTagText(string tag)
        {
            if (ExtendedProperties["TextTag"] != null)
                ExtendedProperties["TextTag"] = tag;
            else
                throw new Exception("TextTag not found in ExtendedProperties");
        }
    }
}

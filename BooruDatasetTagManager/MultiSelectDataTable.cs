using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BooruDatasetTagManager.DatasetManager;

namespace BooruDatasetTagManager
{
    [Serializable]
    public class MultiSelectDataTable : DataTable
    {
        private List<DataItem> selectedDataItems;
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


        public void CreateTableFromSelectedImages(List<DataItem> selectedDI)
        {
            selectedDataItems = selectedDI;
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
                int aaa = 1; //for debug
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
                int tagIndex = ((MultiSelectDataRow)e.Row).GetTagIndex();
                string textTag = ((MultiSelectDataRow)e.Row).GetTagText();
                int rowIndex = e.Row.Table.Rows.IndexOf(e.Row);
                if (rowIndex + 1 < Rows.Count)
                {
                    if (!string.IsNullOrWhiteSpace((string)e.Row["Tag"]) && ((MultiSelectDataRow)Rows[rowIndex + 1]).GetTagText() == textTag)
                    {
                        Rows[rowIndex + 1]["Tag"] = textTag;
                    }
                }
                ((MultiSelectDataRow)e.Row).GetDataItem().Tags.RemoveAt(tagIndex);
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

        public void AddTag(string tag)
        {

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
            ExtendedProperties.Add(name, value);
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

        public string GetTagText()
        {
            if (ExtendedProperties["TextTag"] != null)
                return (string)ExtendedProperties["TextTag"];
            else
                return null;
        }
    }
}

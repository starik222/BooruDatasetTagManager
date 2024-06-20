using Manina.Windows.Forms;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static BooruDatasetTagManager.DatasetManager;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BooruDatasetTagManager
{
    public class EditableTagList : CollectionBase, IBindingList, ICloneable
    {
        public delegate void TagsListChangedHandler(object sender, string oldTag, string newTag, ListChangedType changedType);
        public event TagsListChangedHandler TagsListChanged;
        private ListChangedEventArgs resetEvent = new ListChangedEventArgs(ListChangedType.Reset, -1);
        private ListChangedEventHandler onListChanged;

        private List<EditableTagHistory> History = new List<EditableTagHistory>();
        private int HistoryPosition = 0;
        private bool isStoreHistory = true;

        private List<string> _tags;

        public List<string> TextTags { get { return _tags; } }

        public List<EditableTagHistory> HistoryForDebug { get { return History; } }

        public EditableTag this[int index]
        {
            get
            {
                return (EditableTag)(List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public EditableTagList(IEnumerable<string> tags) : base()
        {
            isStoreHistory = false;
            _tags = new List<string>();
            AddRange(tags, false);
            isStoreHistory = true;
        }

        //public EditableTagList(IEnumerable<PromptParser.PromptItem> tags) : base()
        //{
        //    isStoreHistory = false;
        //    _tags = new List<string>();
        //    foreach (var tag in tags)
        //    {
        //        int index = GetNextId();
        //        var eTag = new EditableTag(index, tag.Text, index);
        //        eTag.Weight = tag.Weight;
        //        Add(eTag, false);
        //    }
        //    isStoreHistory = true;
        //}

        public EditableTagList() : base()
        {
            _tags = new List<string>();
        }

        public void LoadFromPromptParserData(IEnumerable<PromptParser.PromptItem> tags)
        {
            isStoreHistory = false;
            _tags = new List<string>();
            foreach (var tag in tags)
            {
                int index = GetNextId();
                var eTag = new EditableTag(index, tag.Text, index);
                eTag.Weight = tag.Weight;
                Add(eTag, false);
            }
            isStoreHistory = true;
        }

        public override string ToString()
        {
            //DeduplicateTags(); //Not need??
            List<string> tempTagList = new List<string>();
            for (int i = 0; i < List.Count; i++)
            {
                tempTagList.Add(List[i].ToString());
            }
            string fixedSeparator = Program.Settings.SeparatorOnSave.Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t");
            return string.Join(fixedSeparator, tempTagList);
        }


        /// <summary>
        /// Undo
        /// </summary>
        public void PrevState()
        {
            if (HistoryPosition == 0)
                return;
            HistoryPosition--;
            //cancel last state
            var curHistory = History[HistoryPosition];
            if (curHistory.Type == EditableTagHistory.HistoryType.Add)
            {
                isStoreHistory = false;
                RemoveAt(curHistory.Index);
                isStoreHistory = true;
            }
            else if (curHistory.Type == EditableTagHistory.HistoryType.Remove)
            {
                Insert(curHistory.Index, curHistory.TagOld, false);
            }
            else if (curHistory.Type == EditableTagHistory.HistoryType.Modify)
            {
                List[curHistory.Index] = curHistory.TagOld;
                OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, curHistory.Index));
            }
            else if (curHistory.Type == EditableTagHistory.HistoryType.Move)
            {
                EditableTag tagToMove = curHistory.TagOld;
                tagToMove.Parent = this;
                isStoreHistory = false;
                RemoveAt(curHistory.Index);
                Insert(curHistory.OldIndex, tagToMove, false);
                isStoreHistory = true;
            }
            else if (curHistory.Type == EditableTagHistory.HistoryType.Clear)
            {
                isStoreHistory = false;
                List.Clear();
                foreach (var item in curHistory.ClearedTags)
                {
                    Add(item, false);
                }
                isStoreHistory = true;
            }
            else if (curHistory.Type == EditableTagHistory.HistoryType.Sort)
            {
                isStoreHistory = false;
                List.Clear();
                foreach (var item in curHistory.ClearedTags)
                {
                    Add(item, false);
                }
                isStoreHistory = true;
            }
        }
        /// <summary>
        /// Redo
        /// </summary>
        public void NextState()
        {
            if (HistoryPosition == History.Count)
                return;
            var curHistory = History[HistoryPosition];
            if (curHistory.Type == EditableTagHistory.HistoryType.Remove)
            {
                isStoreHistory = false;
                RemoveAt(curHistory.Index);
                isStoreHistory = true;
            }
            else if (curHistory.Type == EditableTagHistory.HistoryType.Add)
            {
                Insert(curHistory.Index, curHistory.TagOld, false);
            }
            else if (curHistory.Type == EditableTagHistory.HistoryType.Modify)
            {
                List[curHistory.Index] = curHistory.TagNew;
                OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, curHistory.Index));
            }
            else if (curHistory.Type == EditableTagHistory.HistoryType.Move)
            {
                EditableTag tagToMove = curHistory.TagOld;
                tagToMove.Parent = this;
                isStoreHistory = false;
                RemoveAt(curHistory.OldIndex);
                Insert(curHistory.Index, tagToMove, false);
                isStoreHistory = true;
            }
            else if (curHistory.Type == EditableTagHistory.HistoryType.Clear)
            {
                isStoreHistory = false;
                List.Clear();
                isStoreHistory = true;
            }
            else if (curHistory.Type == EditableTagHistory.HistoryType.Sort)
            {
                isStoreHistory = false;
                List.Clear();
                foreach (var item in curHistory.AddedTags)
                {
                    Add(item, false);
                }
                isStoreHistory = true;
            }
            HistoryPosition++;
        }

        /// <summary>
        /// For debug, сhecking list synchronization
        /// </summary>
        /// <returns></returns>
        public bool CheckSyncLists()
        {
            if(_tags.Count!=List.Count)
                return false;
            for (int i = 0; i < List.Count; i++)
            {
                if(_tags[i]!=((EditableTag)List[i]).Tag)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Removing empty tags and duplicates
        /// </summary>
        public void DeduplicateTags()
        {
            Program.EditableTagListLocker.Wait();
            isStoreHistory = false;
            for (int i = List.Count - 1; i >= 0; i--)
            {
                string tagToSearch = ((EditableTag)List[i]).Tag;
                if (string.IsNullOrWhiteSpace(tagToSearch))
                {
                    RemoveAt(i);
                    continue;
                }
                List<int> foundedTagIndexes = IndexOfAll(tagToSearch, 0, i);
                if (foundedTagIndexes.Count == 1)
                {
                    RemoveAt(i);
                }
                else if (foundedTagIndexes.Count > 1)
                {
                    RemoveAt(i);
                    for (int j = foundedTagIndexes.Count - 1; j >= 1; j--)
                        RemoveAt(j);
                }

            }
            isStoreHistory = true;
            Program.EditableTagListLocker.Release();
        }


        public bool Contains(string tag)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (((EditableTag)List[i]).Tag == tag)
                    return true;
            }
            return false;
        }

        public int IndexOf(string tag)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (((EditableTag)List[i]).Tag == tag)
                    return i;
            }
            return -1;
        }

        public List<int> IndexOfAll(string tag, int startIndex, int count)
        {
            List<int> list = new List<int>();
            for (int i = startIndex; i < count & i < List.Count; i++)
            {
                if (((EditableTag)List[i]).Tag == tag)
                    list.Add(i);
            }
            return list;
        }


        public void AddTag(string tag, bool storeHistory)
        {
            Add(new EditableTag(GetNextId(), tag), storeHistory);
        }

        public (int oldIndex, int newIndex) AddTag(string tag, bool skipExist, AddingType addType, int pos = -1)
        {
            int tagIndex = IndexOf(tag);
            if (skipExist && tagIndex != -1)
                return (tagIndex, tagIndex);
            int localCount = Count;
            if (tagIndex != -1)
            {
                switch (addType)
                {
                    case AddingType.Top:
                        {
                            Move(tagIndex, 0);
                            return (tagIndex, 0);
                        }
                    case AddingType.Center:
                        {
                            Move(tagIndex, localCount / 2);
                            return (tagIndex, localCount / 2);
                        }
                    case AddingType.Down:
                        {
                            Move(tagIndex, localCount - 1);
                            return (tagIndex, localCount - 1);
                        }
                    case AddingType.Custom:
                        {
                            if (pos >= localCount)
                            {
                                pos = localCount - 1;
                            }
                            else if (pos < 0)
                            {
                                pos = 0;
                            }
                            Move(tagIndex, pos);
                            return (tagIndex, pos);
                        }
                }
            }
            else
            {
                switch (addType)
                {
                    case AddingType.Top:
                        {
                            InsertTag(0, tag, true);
                            return (tagIndex, 0);
                        }
                    case AddingType.Center:
                        {
                            InsertTag(localCount / 2, tag, true);
                            return (tagIndex, localCount / 2);
                        }
                    case AddingType.Down:
                        {
                            AddTag(tag, true);
                            return (tagIndex, localCount);
                        }
                    case AddingType.Custom:
                        {
                            if (pos >= localCount)
                            {
                                AddTag(tag, true);
                                return (tagIndex, localCount);
                            }
                            else if (pos < 0)
                            {
                                InsertTag(0, tag, true);
                                return (tagIndex, 0);
                            }
                            else
                            {
                                InsertTag(pos, tag, true);
                                return (tagIndex, pos);
                            }
                        }
                }
            }
            return (tagIndex, -1);
        }

        public int Add(EditableTag item, bool storeHistory)
        {
            isStoreHistory = storeHistory;
            item.Parent = this;
            int res = List.Add(item);
            isStoreHistory = true;
            return res;
        }

        public void Insert(int index, EditableTag item, bool storeHistory)
        {
            isStoreHistory = storeHistory;
            item.Parent = this;
            List.Insert(index, item);
            isStoreHistory = true;
        }

        public void AddRange(IEnumerable<string> tags, bool storeHistory)
        {
            foreach (string tag in tags)
            {
                if (string.IsNullOrWhiteSpace(tag))
                    continue;
                int index = GetNextId();
                Add(new EditableTag(index, tag.ToLower().Trim(), index), storeHistory);
            }
        }

        public void AddRange(IEnumerable<PromptParser.PromptItem> tags, bool storeHistory)
        {
            foreach (var tag in tags)
            {
                int index = GetNextId();
                var eTag = new EditableTag(index, tag.Text, index);
                eTag.Weight = tag.Weight;
                Add(eTag, storeHistory);
            }
        }

        public void ReplaceTag(string oldTag, string newTag)
        {
            if (oldTag == newTag)
                return;
            int index = IndexOf(oldTag);
            if (index != -1)
            {
                int dstIndex = IndexOf(newTag);
                if (dstIndex == -1)
                {
                    ((EditableTag)List[index]).Tag = newTag;
                    ((EditableTag)List[index]).Weight = 1;
                }
                else
                {
                    RemoveAt(index);
                }
            }
        }

        public object AddNew()
        {
            return (EditableTag)((IBindingList)this).AddNew();
        }

        public object InsertNew(int index)
        {
            List.Insert(index, new EditableTag(GetNextId(), ""));
            return List[index];
        }

        public void InsertTag(int index, string tag, bool storeHistory)
        {
            Insert(index, new EditableTag(GetNextId(), tag), storeHistory);
        }

        public void Remove(EditableTag value, bool storeHistory)
        {
            isStoreHistory = storeHistory;
            List.Remove(value);
            isStoreHistory = true;
        }

        public void RemoveTag(string tag, bool storeHistory)
        {
            isStoreHistory = storeHistory;
            var indexes = IndexOfAll(tag, 0, Count);
            if (indexes.Count > 0)
            {
                for (int i = indexes.Count - 1; i >= 0; i--)
                {
                    RemoveAt(indexes[i]);
                }
            }
            isStoreHistory = true;
        }

        public int Move(int index, int toIndex)
        {
            if (index < 0 || index > Count - 1)
                throw new IndexOutOfRangeException();

            EditableTag tagToMove = (EditableTag)((EditableTag)List[index]).Clone();
            tagToMove.Parent = this;
            isStoreHistory = false;
            RemoveAt(index);
            if (toIndex < 0 || toIndex > Count)
            {
                toIndex = Count;
            }
            Insert(toIndex, tagToMove, false);
            isStoreHistory = true;
            var h = new EditableTagHistory();
            h.Index = toIndex;
            h.OldIndex = index;
            h.TagOld = (EditableTag)(tagToMove).Clone();
            h.Type = EditableTagHistory.HistoryType.Move;
            AddHistory(h);
            return toIndex;
        }

        public void Sort(int skipFirstCount = 0)
        {
            var h = new EditableTagHistory();
            h.Index = 0;
            foreach (EditableTag c in List)
            {
                var clonedETag = (EditableTag)c.Clone();
                clonedETag.Parent = null;
                h.ClearedTags.Add(clonedETag);
            }
            h.Type = EditableTagHistory.HistoryType.Sort;
            InnerList.Sort(skipFirstCount, InnerList.Count - skipFirstCount, new SortEditableTagListAscending());
            _tags.Sort(skipFirstCount, _tags.Count - skipFirstCount, new SortStringAscending());
            foreach (EditableTag c in List)
            {
                var clonedETag = (EditableTag)c.Clone();
                clonedETag.Parent = null;
                h.AddedTags.Add(clonedETag);
            }
            if (isStoreHistory)
                AddHistory(h);
            OnListChanged(resetEvent);
        }

        public void EndEdit()
        {
            for (int i = 0; i < List.Count; i++)
            {
                var eTag = (EditableTag)List[i];
                if (eTag.IsEditing)
                    eTag.EndEdit();
            }
        }

        public void EndEdit(int rowIndex)
        {
            var eTag = (EditableTag)List[rowIndex];
            if (eTag.IsEditing)
                eTag.EndEdit();
        }

        public bool IsEditMode()
        {
            for (int i = 0; i < List.Count; i++)
            {
                var eTag = (EditableTag)List[i];
                if (eTag.IsEditing)
                    return true;
            }
            return false;
        }


        public async Task TranslateAllAsync()
        {
            isStoreHistory = false;
            for (int i = 0; i < Count; i++)
            {
                EditableTag eTag = (EditableTag)List[i];
                if (string.IsNullOrEmpty(eTag.Translation))
                    eTag.Translation = await Program.TransManager.TranslateAsync(eTag.Tag);
            }
            isStoreHistory = true;
        }

        protected virtual void OnListChanged(ListChangedEventArgs ev)
        {
            if (onListChanged != null)
            {
                onListChanged(this, ev);
            }
            if (!CheckSyncLists())
            {
                CreateDataForDebug();
                throw new InvalidAsynchronousStateException("List desynchronization detected!\nPlease post the file \""+Path.Combine(Program.AppPath, "ErrorData.json") +"\" in the topic\nhttps://github.com/starik222/BooruDatasetTagManager/discussions/111");
            }
        }

        private void CreateDataForDebug()
        {
            ListsDebugInfo info = new ListsDebugInfo();
            for (int i = 0; i < InnerList.Count; i++)
            {
                string tag = ((EditableTag)InnerList[i]).Tag == null ? "NULL" : ((EditableTag)InnerList[i]).Tag;
                info.EditableList.Add(tag);
            }
            for (int i = 0; i < _tags.Count; i++)
            {
                string tag = _tags[i] == null ? "NULL" : _tags[i];
                info.TextList.Add(tag);
            }
            info.History = History;
            File.WriteAllText("ErrorData.json", JsonConvert.SerializeObject(info, Formatting.Indented));
        }

        public class ListsDebugInfo
        {
            public List<string> EditableList;
            public List<string> TextList;
            public List<EditableTagHistory> History;

            public ListsDebugInfo()
            {
                EditableList = new List<string>();
                TextList = new List<string>();
                History = new List<EditableTagHistory>();
            }

        }

        protected override void OnClear()
        {
            var h = new EditableTagHistory();
            h.Index = 0;
            foreach (EditableTag c in List)
            {
                var clonedETag = (EditableTag)c.Clone();
                clonedETag.Parent = null;
                h.ClearedTags.Add(clonedETag);
            }
            h.Type = EditableTagHistory.HistoryType.Clear;
            if (isStoreHistory)
                AddHistory(h);
        }


        protected override void OnClearComplete()
        {
            foreach(var item in _tags)
                TagsListChanged?.Invoke(this, null, item, ListChangedType.ItemDeleted);
            _tags.Clear();
            OnListChanged(resetEvent);
        }

        protected override void OnInsertComplete(int index, object value)
        {
            EditableTag c = (EditableTag)value;
            c.Parent = this;
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
        }

        protected override void OnInsert(int index, object value)
        {
            if (isStoreHistory)
            {
                var h = new EditableTagHistory();
                h.Index = index;
                h.TagOld = (EditableTag)((EditableTag)value).Clone();
                h.Type = EditableTagHistory.HistoryType.Add;
                AddHistory(h);
            }
            _tags.Insert(index, ((EditableTag)value).Tag);
            TagsListChanged?.Invoke(this, null, ((EditableTag)value).Tag, ListChangedType.ItemAdded);
            base.OnInsert(index, value);
        }

        protected override void OnRemove(int index, object value)
        {
            if (isStoreHistory)
            {
                var h = new EditableTagHistory();
                h.Index = index;
                h.TagOld = (EditableTag)((EditableTag)value).Clone();
                h.Type = EditableTagHistory.HistoryType.Remove;
                AddHistory(h);
            }
            TagsListChanged?.Invoke(this, null, _tags[index], ListChangedType.ItemDeleted);
            _tags.RemoveAt(index);
            base.OnRemove(index, value);
        }

        private void AddHistory(EditableTagHistory his)
        {
            if (HistoryPosition != History.Count)
            {
                while (History.Count != HistoryPosition)
                {
                    History.RemoveAt(History.Count - 1);
                }
            }
            History.Add(his);
            HistoryPosition++;
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            EditableTag c = (EditableTag)value;
            c.Parent = this;
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
        }

        protected override void OnSet(int index, object oldValue, object newValue)
        {
            base.OnSet(index, oldValue, newValue);
        }

        protected override void OnSetComplete(int index, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {

                EditableTag olddata = (EditableTag)oldValue;
                EditableTag newdata = (EditableTag)newValue;

                olddata.Parent = null;
                newdata.Parent = this;
                string oldTagText = _tags[index];
                _tags[index] = newdata.Tag;
                if (oldTagText != newdata.Tag)
                    TagsListChanged?.Invoke(this, oldTagText, newdata.Tag, ListChangedType.ItemChanged);
                OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
            }
        }

        // Called by EditableTag when it changes.
        internal void EditableTagChanged(EditableTag tag, bool storeHistory)
        {

            int index = List.IndexOf(tag);
            if (_tags[index] != tag.Tag)
                TagsListChanged?.Invoke(this, _tags[index], tag.Tag, ListChangedType.ItemChanged);
            _tags[index] = tag.Tag;
            if (storeHistory)
            {
                var h = new EditableTagHistory();
                h.Index = index;
                h.TagOld = tag.GetEditableTagFromBackup();
                h.TagNew = (EditableTag)tag.Clone();
                h.Type = EditableTagHistory.HistoryType.Modify;
                AddHistory(h);
            }
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
        }

        // Implements IBindingList.
        bool IBindingList.AllowEdit
        {
            get { return true; }
        }

        bool IBindingList.AllowNew
        {
            get { return true; }
        }

        bool IBindingList.AllowRemove
        {
            get { return true; }
        }

        bool IBindingList.SupportsChangeNotification
        {
            get { return true; }
        }

        bool IBindingList.SupportsSearching
        {
            get { return false; }
        }

        bool IBindingList.SupportsSorting
        {
            get { return false; }
        }

        // Events.
        public event ListChangedEventHandler ListChanged
        {
            add
            {
                onListChanged += value;
            }
            remove
            {
                onListChanged -= value;
            }
        }

        // Methods.
        object IBindingList.AddNew()
        {
            EditableTag c = new EditableTag(GetNextId(), "");
            List.Add(c);
            return c;
        }

        private int GetNextId()
        {
            if (List.Count == 0)
                return 0;
            int index = 0;
            for (int i = 0; i < this.Count; i++)
            {
                if (((EditableTag)List[i]).Id > index)
                    index = ((EditableTag)List[i]).Id;
            }
            return index + 1;
        }

        // Unsupported properties.
        bool IBindingList.IsSorted
        {
            get { throw new NotSupportedException(); }
        }

        ListSortDirection IBindingList.SortDirection
        {
            get { throw new NotSupportedException(); }
        }

        PropertyDescriptor IBindingList.SortProperty
        {
            get { throw new NotSupportedException(); }
        }

        // Unsupported Methods.
        void IBindingList.AddIndex(PropertyDescriptor property)
        {
            throw new NotSupportedException();
        }

        void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            throw new NotSupportedException();
        }

        int IBindingList.Find(PropertyDescriptor property, object key)
        {
            throw new NotSupportedException();
        }

        void IBindingList.RemoveIndex(PropertyDescriptor property)
        {
            throw new NotSupportedException();
        }

        void IBindingList.RemoveSort()
        {
            throw new NotSupportedException();
        }

        public object Clone()
        {
            EditableTagList eTagList = new EditableTagList();
            foreach (EditableTag item in List)
            {
                eTagList.Add((EditableTag)item.Clone(), false);
            }
            return eTagList;
        }

        private class SortEditableTagListAscending : IComparer
        {
            int IComparer.Compare(object x, object y)
            {
                EditableTag t1 = (EditableTag)x;
                EditableTag t2 = (EditableTag)y;
                //if (!t1.Sortiable)
                //    return 1;
                return t1.Tag.CompareTo(t2.Tag);
            }
        }

        private class SortStringAscending : IComparer<string>
        {
            int IComparer<string>.Compare(string x, string y)
            {
                return (x).CompareTo(y);
            }
        }
    }
}

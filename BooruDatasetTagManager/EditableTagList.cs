using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BooruDatasetTagManager
{
    public class EditableTagList : CollectionBase, IBindingList
    {

        private ListChangedEventArgs resetEvent = new ListChangedEventArgs(ListChangedType.Reset, -1);
        private ListChangedEventHandler onListChanged;

        private List<EditableTagHistory> History = new List<EditableTagHistory>();
        private int HistoryPosition = 0;
        private bool isStoreHistory = true;

        private List<string> _tags;

        public List<string> TextTags { get { return _tags; } }

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

        public EditableTagList() : base()
        {
            _tags = new List<string>();
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
            for (int i = List.Count - 1; i >= 0; i--)
            {
                string tagToSearch = ((EditableTag)List[i]).Tag;
                if (string.IsNullOrWhiteSpace(tagToSearch))
                    RemoveAt(i);
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

        public int Add(EditableTag item, bool storeHistory)
        {
            isStoreHistory = storeHistory;
            int res = List.Add(item);
            isStoreHistory = true;
            return res;
        }

        public void Insert(int index, EditableTag item, bool storeHistory)
        {
            isStoreHistory = storeHistory;
            List.Insert(index, item);
            isStoreHistory = true;
        }

        public void AddRange(IEnumerable<string> tags, bool storeHistory)
        {
            foreach (string tag in tags)
            {
                int index = GetNextId();
                Add(new EditableTag(index, tag, index), storeHistory);
            }
        }

        public void ReplaceTag(string oldTag, string newTag)
        {
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
                    RemoveAt(i);
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
                throw new InvalidAsynchronousStateException("List desynchronization detected!");
        }

        protected override void OnClear()
        {
            foreach (EditableTag c in List)
            {
                c.Parent = null;
            }
        }

        protected override void OnClearComplete()
        {
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

        protected override void OnSetComplete(int index, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {

                EditableTag olddata = (EditableTag)oldValue;
                EditableTag newdata = (EditableTag)newValue;

                olddata.Parent = null;
                newdata.Parent = this;
                _tags[index] = newdata.Tag;

                OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
            }
        }

        // Called by EditableTag when it changes.
        internal void EditableTagChanged(EditableTag tag, bool storeHistory)
        {

            int index = List.IndexOf(tag);

            _tags[index] = tag.Tag;

            //var h = new EditableTagHistory();
            //h.Index = index;
            //h.TagOld = tag.GetEditableTagFromBackup();
            //h.TagNew = (EditableTag)tag.Clone();
            //if (h.TagOld.Tag == "")
            //{
            //    h.Type = EditableTagHistory.HistoryType.Add;
            //    _tags.Insert(index, tag.Tag);
            //}
            //else
            //{
            //    h.Type = EditableTagHistory.HistoryType.Modify;
            //    _tags[index] = tag.Tag;
            //}


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
            EditableTag c = new EditableTag(GetNextId(), this.Count.ToString());
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
    }
}

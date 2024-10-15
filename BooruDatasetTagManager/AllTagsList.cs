using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager
{
    public class AllTagsList : CollectionBase, IBindingListView//, IBindingList
    {
        private ListChangedEventArgs resetEvent = new ListChangedEventArgs(ListChangedType.Reset, -1);
        private ListChangedEventHandler onListChanged;
        private ListSortDescriptionCollection sortDescriptions = new ListSortDescriptionCollection();
        private List<AllTagsItem> tagsList = new List<AllTagsItem>();

        private string filterText = string.Empty;
        private bool filterByCount = false;
        private int filterTagsCount = 0;

        private bool _isSorted = false;
        private PropertyDescriptor _propertyDescriptor;
        private ListSortDirection _sortDirection = ListSortDirection.Ascending;

        public AllTagsItem this[int index]
        {
            get
            {
                return (AllTagsItem)(List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public AllTagsList() : base()
        {
        }


        public void TranslateAllTags()
        {
            TranslateAllAsync();
        }

        public bool IsFilterByCount() => filterByCount;


        public async void TranslateAllAsync()
        {
            for (int i = 0; i < tagsList.Count; i++)
            {
                if (tagsList[i].IsNeedTranslate())
                {
                    if (!string.IsNullOrEmpty(tagsList[i].Tag))
                    {
                        string result = await Program.TransManager.TranslateAsync(tagsList[i].Tag);
                        tagsList[i].SetTranslation(result);
                    }
                    else
                    {
                        tagsList[i].SetTranslation("");
                    }
                    int listIndex = IndexOfInternal(List, tagsList[i]);
                    if (listIndex != -1)
                    {
                        OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, listIndex));
                    }
                }
            }
        }

        public void AddTag(string tag)
        {
            int indexTagsList = IndexOfTagsList(tag);
            int indexList = IndexOfList(tag);
            if (indexTagsList != -1)
            {
                tagsList[indexTagsList].Count++;
                if (_isSorted && _propertyDescriptor.Name == "Count")
                {
                    ((IBindingListView)this).ApplySort(_propertyDescriptor, _sortDirection);
                }
            }
            else
            {
                var tagItem = new AllTagsItem(tag);
                AddWithSortingInternal(tagsList, tagItem);
                if (indexList == -1)
                {
                    if (CheckFilterOnTag(tagItem, filterText))
                        AddWithSortingInternal(List, tagItem);
                }
            }
        }

        public void RemoveTag(string tag, bool allTags = false)
        {
            int indexTagsList = IndexOfTagsList(tag);
            int indexList = IndexOfList(tag);
            if (indexTagsList != -1)
            {
                if (allTags)
                {
                    tagsList.RemoveAt(indexTagsList);
                    if (indexList != -1)
                        RemoveAt(indexList);
                }
                else
                {
                    if (tagsList[indexTagsList].Count > 1)
                    {
                        tagsList[indexTagsList].Count--;
                        if (_isSorted && _propertyDescriptor.Name == "Count")
                        {
                            ((IBindingListView)this).ApplySort(_propertyDescriptor, _sortDirection);
                        }
                    }
                    else
                    {
                        tagsList.RemoveAt(indexTagsList);
                        if (indexList != -1)
                            RemoveAt(indexList);
                    }
                }
            }
        }

        public void ChangeTag(string oldTag, string newTag)
        {
            RemoveTag(oldTag);
            AddTag(newTag);
        }

        public string[] GetAllTagsList()
        {
            return tagsList.Select(x => x.Tag).ToArray();
        }

        private void AddWithSortingInternal(IList lst, AllTagsItem item)
        {
            item.Parent = this;
            if (lst.Count == 0)
            {
                lst.Add(item);
            }
            else
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    int compareResult = 0;
                    if (_propertyDescriptor == null)
                        compareResult = item.Tag.CompareTo(((AllTagsItem)lst[i]).Tag);
                    else if (_propertyDescriptor.Name == "Tag" && _sortDirection == ListSortDirection.Ascending)
                        compareResult = item.Tag.CompareTo(((AllTagsItem)lst[i]).Tag);
                    else if (_propertyDescriptor.Name == "Tag" && _sortDirection == ListSortDirection.Descending)
                        compareResult = ((AllTagsItem)lst[i]).Tag.CompareTo(item.Tag);
                    else if (_propertyDescriptor.Name == "Count" && _sortDirection == ListSortDirection.Ascending)
                    {
                        compareResult = item.Count.CompareTo(((AllTagsItem)lst[i]).Count);
                        if (compareResult == 0)
                        {
                            compareResult = item.Tag.CompareTo(((AllTagsItem)lst[i]).Tag);
                        }
                    }
                    else if (_propertyDescriptor.Name == "Count" && _sortDirection == ListSortDirection.Descending)
                    {
                        compareResult = ((AllTagsItem)lst[i]).Count.CompareTo(item.Count);
                        if (compareResult == 0)
                        {
                            compareResult = item.Tag.CompareTo(((AllTagsItem)lst[i]).Tag);
                        }
                    }
                    if (compareResult < 0)
                    {
                        lst.Insert(i, item);
                        return;
                    }
                }
                lst.Add(item);
            }
        }

        //public int Add(AllTagsItem item)
        //{
        //    item.Parent = this;
        //    int res = List.Add(item);
        //    return res;
        //}

        /// <summary>
        /// List<AllTagsItem> tagsList
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public int IndexOfTagsList(string tag)
        {
            return IndexOfInternal(tagsList, tag);
        }
        /// <summary>
        /// internal List
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public int IndexOfList(string tag)
        {
            return IndexOfInternal(List, tag);
        }

        private int IndexOfInternal(IList lst, string tag)
        {
            int hash = tag.GetHashCode();
            for (int i = 0; i < lst.Count; i++)
            {
                if (((AllTagsItem)lst[i]).GetHashCode() == hash)
                    return i;
            }
            return -1;
        }

        private int IndexOfInternal(IList lst, AllTagsItem tagItem)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                if (((AllTagsItem)lst[i]) == tagItem)
                    return i;
            }
            return -1;
        }

        public int FindTagStartWith(string tag)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (((AllTagsItem)List[i]).Tag.StartsWith(tag))
                    return i;
            }
            return -1;
        }

        private bool CheckFilterOnTag(AllTagsItem tagsItem, string filter)
        {
            if (filterText == string.Empty)
                return true;
            if (tagsItem.Tag.Contains(filter))
                return true;
            return false;
        }

        public void SetFilterByCount(int tagsCount)
        {
            filterTagsCount = tagsCount;
            foreach (var item in tagsList)
            {
                if (item.Count == tagsCount)
                {
                    if (!List.Contains(item))
                        AddWithSortingInternal(List, item);
                }
                else
                {
                    if (List.Contains(item))
                        List.Remove(item);
                }
            }
            filterByCount = true;
        }

        public void UpdateFilter()
        {
            if (filterByCount)
            {
                SetFilterByCount(filterTagsCount);
            }
            else
            {
                foreach (var item in tagsList)
                {
                    if (CheckFilterOnTag(item, filterText))
                    {
                        if (!List.Contains(item))
                            AddWithSortingInternal(List, item);
                    }
                    else
                    {
                        if (List.Contains(item))
                            List.Remove(item);
                    }
                }
            }
        }

        protected virtual void OnListChanged(ListChangedEventArgs ev)
        {
            if (onListChanged != null)
            {
                onListChanged(this, ev);
            }
        }

        protected override void OnClearComplete()
        {
            OnListChanged(resetEvent);
        }

        protected override void OnInsertComplete(int index, object value)
        {
            AllTagsItem c = (AllTagsItem)value;
            c.Parent = this;
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            AllTagsItem c = (AllTagsItem)value;
            c.Parent = this;
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
        }

        protected override void OnSetComplete(int index, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                AllTagsItem olddata = (AllTagsItem)oldValue;
                AllTagsItem newdata = (AllTagsItem)newValue;

                olddata.Parent = null;
                newdata.Parent = this;

                OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
            }
        }

        internal void AllTagsItemChanged(AllTagsItem tag)
        {
            int index = List.IndexOf(tag);
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
            get { return true; }
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
            AllTagsItem c = new AllTagsItem();
            List.Add(c);
            return c;
        }

        void IBindingListView.RemoveFilter()
        {
            filterText = string.Empty;
            filterByCount = false;
            UpdateFilter();
        }

        //private int GetNextId()
        //{
        //    if (List.Count == 0)
        //        return 0;
        //    int index = 0;
        //    for (int i = 0; i < this.Count; i++)
        //    {
        //        if (((EditableTag)List[i]).Id > index)
        //            index = ((EditableTag)List[i]).Id;
        //    }
        //    return index + 1;
        //}

        // Unsupported properties.
        bool IBindingList.IsSorted
        {
            get { return _isSorted; }
        }

        ListSortDirection IBindingList.SortDirection
        {
            get { return _sortDirection; }
        }

        PropertyDescriptor IBindingList.SortProperty
        {
            get { return _propertyDescriptor; }
        }

        public string Filter
        {
            get
            {
                return filterText;
            }
            set
            {
                filterText = value;
                UpdateFilter();
            }
        }

        public ListSortDescriptionCollection SortDescriptions
        {
            get { return sortDescriptions; }
        }

        public bool SupportsAdvancedSorting
        {
            get { return false; }
        }

        public bool SupportsFiltering
        {
            get { return true; }
        }

        // Unsupported Methods.
        void IBindingList.AddIndex(PropertyDescriptor property)
        {
            throw new NotSupportedException();
        }

        void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            _propertyDescriptor = property;
            _sortDirection = direction;
            _isSorted = true;
            if (property.Name == "Tag")
            {
                if (direction == ListSortDirection.Ascending)
                {
                    this.InnerList.Sort(new SortByTagNameAscending());
                    this.tagsList.Sort(new SortByTagNameTIAscending());
                }
                else
                {
                    this.InnerList.Sort(new SortByTagNameDescending());
                    this.tagsList.Sort(new SortByTagNameTIDescending());
                }
            }
            else if (property.Name == "Count")
            {
                if (direction == ListSortDirection.Ascending)
                {
                    this.InnerList.Sort(new SortByCountAscending());
                    this.tagsList.Sort(new SortByCountTIAscending());
                }
                else
                {
                    this.InnerList.Sort(new SortByCountDescending());
                    this.tagsList.Sort(new SortByCountTIDescending());
                }
            }
            else
                { throw new NotSupportedException(); }
            OnListChanged(resetEvent);
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

        public void ApplySort(ListSortDescriptionCollection sorts)
        {
            throw new NotImplementedException();
        }

        //public void RemoveFilter()
        //{
        //    throw new NotImplementedException();
        //}

        //public object Clone()
        //{
        //    EditableTagList eTagList = new EditableTagList();
        //    foreach (AllTagsItem item in List)
        //    {
        //        eTagList.Add((AllTagsItem)item.Clone(), false);
        //    }
        //    return eTagList;
        //}

        private class SortByTagNameAscending : IComparer
        {
            int IComparer.Compare(object x, object y)
            {
                AllTagsItem t1 = (AllTagsItem)x;
                AllTagsItem t2 = (AllTagsItem)y;
                return t1.Tag.CompareTo(t2.Tag);
            }
        }

        private class SortByTagNameDescending : IComparer
        {
            int IComparer.Compare(object x, object y)
            {
                AllTagsItem t1 = (AllTagsItem)x;
                AllTagsItem t2 = (AllTagsItem)y;
                return t2.Tag.CompareTo(t1.Tag);
            }
        }

        private class SortByTagNameTIAscending : IComparer<AllTagsItem>
        {
            int IComparer<AllTagsItem>.Compare(AllTagsItem x, AllTagsItem y)
            {
                return x.Tag.CompareTo(y.Tag);
            }
        }

        private class SortByTagNameTIDescending : IComparer<AllTagsItem>
        {
            int IComparer<AllTagsItem>.Compare(AllTagsItem x, AllTagsItem y)
            {
                return y.Tag.CompareTo(x.Tag);
            }
        }

        private class SortByCountAscending : IComparer
        {
            int IComparer.Compare(object x, object y)
            {
                AllTagsItem t1 = (AllTagsItem)x;
                AllTagsItem t2 = (AllTagsItem)y;
                var result = t1.Count.CompareTo(t2.Count);
                if(result == 0)
                    return t1.Tag.CompareTo(t2.Tag);
                return result;
            }
        }

        private class SortByCountDescending : IComparer
        {
            int IComparer.Compare(object x, object y)
            {
                AllTagsItem t1 = (AllTagsItem)x;
                AllTagsItem t2 = (AllTagsItem)y;
                var result = t2.Count.CompareTo(t1.Count);
                if (result == 0)
                    return t1.Tag.CompareTo(t2.Tag);
                return result;
            }
        }

        private class SortByCountTIAscending : IComparer<AllTagsItem>
        {
            int IComparer<AllTagsItem>.Compare(AllTagsItem x, AllTagsItem y)
            {
                var result = x.Count.CompareTo(y.Count);
                if (result == 0)
                    return x.Tag.CompareTo(y.Tag);
                return result;
            }
        }

        private class SortByCountTIDescending : IComparer<AllTagsItem>
        {
            int IComparer<AllTagsItem>.Compare(AllTagsItem x, AllTagsItem y)
            {
                var result = y.Count.CompareTo(x.Count);
                if (result == 0)
                    return x.Tag.CompareTo(y.Tag);
                return result;
            }
        }
    }
}

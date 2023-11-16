using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooruDatasetTagManager
{
    public class AllTagsList : CollectionBase, IBindingList
    {
        private ListChangedEventArgs resetEvent = new ListChangedEventArgs(ListChangedType.Reset, -1);
        private ListChangedEventHandler onListChanged;

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

        public int AddTag(string tag)
        {
            int index = IndexOf(tag);
            if (index != -1)
            {
                this[index].Count++;
            }
            else
            {
                index = Add(new AllTagsItem(tag));
            }
            return index;
        }

        public int RemoveTag(string tag, bool allTags = false)
        {
            int index = IndexOf(tag);
            if (index != -1)
            {
                if (allTags)
                {
                    RemoveAt(index);
                    index = -1;
                }
                else
                {
                    if (this[index].Count > 1)
                    {
                        this[index].Count--;
                    }
                    else
                    {
                        RemoveAt(index);
                        index = -1;
                    }
                }
            }
            return index;
        }

        public void ChangeTag(string oldTag, string newTag)
        {
            RemoveTag(oldTag);
            AddTag(newTag);
        }

        public int Add(AllTagsItem item)
        {
            item.Parent = this;
            int res = List.Add(item);
            return res;
        }

        public int IndexOf(string tag)
        {
            int hash = tag.GetHashCode();
            for (int i = 0; i < List.Count; i++)
            {
                if (((AllTagsItem)List[i]).GetHashCode() == hash)
                    return i;
            }
            return -1;    
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

                OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
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
            AllTagsItem c = new AllTagsItem();
            List.Add(c);
            return c;
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

        //public object Clone()
        //{
        //    EditableTagList eTagList = new EditableTagList();
        //    foreach (AllTagsItem item in List)
        //    {
        //        eTagList.Add((AllTagsItem)item.Clone(), false);
        //    }
        //    return eTagList;
        //}

        private class SortEditableTagListAscending : IComparer
        {
            int IComparer.Compare(object x, object y)
            {
                AllTagsItem t1 = (AllTagsItem)x;
                AllTagsItem t2 = (AllTagsItem)y;
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

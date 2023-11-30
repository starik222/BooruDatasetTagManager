using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace BooruDatasetTagManager
{
    public class AllTagsItem : IEditableObject//, ICloneable
    {
        struct AllTagsData
        {
            internal string tag;
            internal string translation;
            internal int count;
            internal int hash;

            public override bool Equals(object obj)
            {

                if (obj != null && obj.GetType() == typeof(AllTagsData))
                {
                    AllTagsData t2 = (AllTagsData)obj;
                    if (t2.hash == hash)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
        }


        private AllTagsList parent;
        private AllTagsData tagData;
        private AllTagsData backupData;
        private bool inTxn = false;
        private bool needTranslate = false;

        public string Tag
        {
            get
            {
                return tagData.tag;
            }
            set
            {
                tagData.tag = value;
                var hCode = tagData.tag.GetHashCode();
                if (tagData.hash != hCode)
                {
                    needTranslate = true;
                }
                tagData.hash = hCode;
                OnEditableTagChanged();
            }
        }

        public string Translation
        {
            get
            {
                return tagData.translation;
            }
            set
            {
                tagData.translation = value;
                needTranslate = false;
                OnEditableTagChanged();
            }
        }

        public int Count
        {
            get
            {
                return tagData.count;
            }
            set
            {
                tagData.count = value;
                OnEditableTagChanged();
            }
        }

        internal AllTagsList Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        public AllTagsItem()
        {
            tagData.translation = "";
            tagData.tag = "";
            tagData.count = 1;
            tagData.hash = tagData.tag.GetHashCode();
            needTranslate = false;
        }

        public AllTagsItem(string tag)
        {
            tagData.translation = "";
            tagData.tag = tag;
            tagData.count = 1;
            tagData.hash = tagData.tag.GetHashCode();
            if (!string.IsNullOrEmpty(tagData.tag))
                needTranslate = true;
        }

        public override int GetHashCode()
        {
            return tagData.hash;
        }

        public bool IsNeedTranslate()
        {
            return needTranslate;
        }

        public void SetTranslation(string value)
        {
            tagData.translation = value;
            needTranslate = false;
        }

        public void BeginEdit()
        {
            if (!inTxn)
            {
                backupData = tagData;
                inTxn = true;
            }
        }

        public void CancelEdit()
        {
            if (inTxn)
            {
                this.tagData = backupData;
                inTxn = false;
            }
        }

        public void EndEdit()
        {
            if (inTxn)
            {
                inTxn = false;
                if (!tagData.Equals(backupData))
                    OnEditableTagChanged();
                backupData = new AllTagsData();
            }
        }

        private void OnEditableTagChanged()
        {
            if (!inTxn && Parent != null)
            {
                Parent.AllTagsItemChanged(this);
            }
            //if (parent != null)
            //{
            //    for(int i=0;i<
            //}

        }
    }
}

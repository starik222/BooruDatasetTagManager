using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooruDatasetTagManager
{

    public class HotkeyData
    {
        public List<HotkeyItem> Items { get; set; }
        [JsonIgnore]
        public Dictionary<string, Action> Commands { get; set; }

        public HotkeyData()
        {
            Commands = new Dictionary<string, Action>();
            Items = new List<HotkeyItem>();
        }

        public HotkeyItem this[string id]
        {
            get
            {
                return Items.Find(x => x.Id == id);
            }
            set
            {
                int index = Items.FindIndex(x=> x.Id == id);
                if (index == -1)
                    Items.Add(value);
                else
                {
                    Items[index] = value;
                }
            }
        }


        public void InitDefault()
        {
            Items = new List<HotkeyItem>();
            Items.Add(new HotkeyItem("DatasetFocus", "Focus on image list", Keys.D1, true, false, false));
            Items.Add(new HotkeyItem("TagsFocus", "Focus on tag list", Keys.D2, true, false, false));
            Items.Add(new HotkeyItem("AllTagsFocus", "Focus on all tag list", Keys.D3, true, false, false));
            Items.Add(new HotkeyItem("AutoTagsFocus", "Focus on AutoTagger preview", Keys.D4, true, false, false));
            Items.Add(new HotkeyItem("PreviewTabFocus", "Focus on preview tab", Keys.D5, true, false, false));
            
            Items.Add(new HotkeyItem("MenuItemSaveChanges", "Save all changes", Keys.S, true, false, false));
            Items.Add(new HotkeyItem("MenuItemShowPreview", "Show preview window", Keys.P, true, false, false));
            Items.Add(new HotkeyItem("MenuHideAllTags", "Hide/show all tags window", Keys.J, true, false, false));
            Items.Add(new HotkeyItem("MenuHideTags", "Hide/show image tags window", Keys.K, true, false, false));
            Items.Add(new HotkeyItem("MenuHideDataset", "Hide/show dataset window", Keys.L, true, false, false));


            Items.Add(new HotkeyItem("BtnTagAdd", "Add new tag", Keys.E, true, false, false));
            Items.Add(new HotkeyItem("BtnTagDelete", "Remove selected tag", Keys.D, true, false, false));
            Items.Add(new HotkeyItem("BtnTagUndo", "Undo step", Keys.Z, true, false, false));
            Items.Add(new HotkeyItem("BtnTagRedo", "Redo step", Keys.Z, true, true, false));
            Items.Add(new HotkeyItem("BtnTagUp", "Up tag in list", Keys.PageUp, true, false, false));
            Items.Add(new HotkeyItem("BtnTagDown", "Down tag in list", Keys.PageDown, true, false, false));
            Items.Add(new HotkeyItem("BtnTagFindInAll", "Find selected tag in all list", Keys.F, true, false, false));
            Items.Add(new HotkeyItem("BtnTagAddToAll", "Add tag to all", Keys.W, true, false, false));
            Items.Add(new HotkeyItem("BtnTagAddToSelected", "Add tag to selected", Keys.W, false, true, false));
            Items.Add(new HotkeyItem("BtnTagAddToFiltered", "Add tag to filtered", Keys.W, true, true, false));
            Items.Add(new HotkeyItem("BtnTagDeleteForAll", "Remove tag to all", Keys.R, true, false, false));
            Items.Add(new HotkeyItem("BtnTagDeleteForSelected", "Remove tag from selected", Keys.R, false, true, false));
            Items.Add(new HotkeyItem("BtnTagDeleteForFiltered", "Remove tag from filtered", Keys.R, true, true, false));
            Items.Add(new HotkeyItem("BtnTagReplace", "Replace tag in all", Keys.G, true, false, false));
            Items.Add(new HotkeyItem("BtnImageFilter", "Find in dataset", Keys.F, true, true, false));
            Items.Add(new HotkeyItem("BtnImageExitFilter", "Reset dataset filter", Keys.G, true, true, false));
            Items.Add(new HotkeyItem("BtnTagMultiModeSwitch", "Switch filted mode", Keys.Y, true, false, false));
            Items.Add(new HotkeyItem("BtnTagFilter", "Filter in all tags", Keys.F, false, true, false));
            Items.Add(new HotkeyItem("BtnTagExitFilter", "Reset filter in all tags", Keys.G, false, true, false));
            Items.Add(new HotkeyItem("BtnMenuGenTagsWithCurrentSettings", "Generate tags with AutoTagger (current setting)", Keys.G, false, true, false));
            Items.Add(new HotkeyItem("BtnMenuGenTagsWithSetWindow", "Generate tags with AutoTagger (open settings window)", Keys.H, false, true, false));
            Items.Add(new HotkeyItem("toolStripPromptSortBtn", "Sort tags", Keys.Q, true, false, false));
        }
        
    }

    public class HotkeyItem : ICloneable
    {
        public string Id {get; set; }
        [JsonIgnore]
        public string Text {get; set; }
        public Keys KeyData {get; set; }

        public Keys FullKeyData
        {
            get => CalcFullKeyData();
        }
        public bool IsCtrl {get; set; }
        public bool IsShift {get; set; }
        public bool IsAlt {get; set; }

        public HotkeyItem()
        {
            
        }

        public HotkeyItem(string id, string text, Keys keyData, bool isCtrl, bool isShift, bool isAlt)
        {
            Id = id;
            Text = text;
            KeyData = keyData;
            IsCtrl = isCtrl;
            IsShift = isShift;
            IsAlt = isAlt;
        }

        public string GetHotkeyString()
        {
            List<string> parts = new List<string>();
            if (IsCtrl)
                parts.Add("Ctrl");
            if (IsAlt)
                parts.Add("Alt");
            if (IsShift)
                parts.Add("Shift");
            parts.Add(KeyData.ToString());

            return string.Join("+", parts);
        }

        private Keys CalcFullKeyData()
        {
            Keys res = KeyData;
            if (IsCtrl)
                res |= Keys.Control;
            if (IsShift) 
                res |= Keys.Shift;
            if (IsAlt) 
                res |= Keys.Alt;
            return res;
        }

        public void ResetModifiers()
        {
            IsAlt = false;
            IsShift = false;
            IsCtrl = false;
        }

        public object Clone()
        {
            HotkeyItem temp = new HotkeyItem(Id, Text, KeyData, IsCtrl, IsShift, IsAlt);
            return temp;
        }
    }
}

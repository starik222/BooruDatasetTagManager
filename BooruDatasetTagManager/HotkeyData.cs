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
            Items.Add(new HotkeyItem("DatasetFocus", "Focus on image list", Keys.D1, true, false, false));
            Items.Add(new HotkeyItem("TagsFocus", "Focus on tag list", Keys.D2, true, false, false));
            Items.Add(new HotkeyItem("AllTagsFocus", "Focus on all tag list", Keys.D3, true, false, false));
            Items.Add(new HotkeyItem("AddNewTag", "Add new tag", Keys.E, true, false, false));
            Items.Add(new HotkeyItem("DelNewTag", "Remove selected tag", Keys.D, true, false, false));
            
        }
        
    }

    public class HotkeyItem
    {
        public string Id {get; set; }
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
    }
}

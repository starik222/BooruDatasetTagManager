using BooruDatasetTagManager.AiApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooruDatasetTagManager
{
    [DesignerCategory("Code")]
    public  class ColouredCheckedListBox: CheckedListBox
    {    
        public Dictionary<int, Color> ItemsColor { get; set; }
        public ColouredCheckedListBox() : base()
        {
            ItemsColor = new Dictionary<int, Color>();
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Color c = e.BackColor;
            if (ItemsColor.ContainsKey(e.Index))
            {
                c = ItemsColor[e.Index];
            }
            DrawItemEventArgs e2 =
                new DrawItemEventArgs
                (
                    e.Graphics,
                    e.Font,
                    new Rectangle(e.Bounds.Location, e.Bounds.Size),
                    e.Index,
                    e.State,
                    e.ForeColor,
                    c
                );

            base.OnDrawItem(e2);
        }

        public int FindIndexByName(string name)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if(((ModelBaseInfo)Items[i]).ModelName == name)
                    return i;
            }
            return -1;
        }
    }
}

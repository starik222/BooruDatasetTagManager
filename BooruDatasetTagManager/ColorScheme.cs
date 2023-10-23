using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace UmaMusumeDBBrowser
{
    public class ColorSchemeManager
    {
        public List<ColorScheme> Items { get; set; }

        private int selectedSchemeIndex = -1;


        public delegate void CSData(object sender, EventArgs e);
        public event CSData SchemeChanded;



        public ColorScheme SelectedScheme
        {
            get
            {
                if (selectedSchemeIndex != -1)
                    return Items[selectedSchemeIndex];
                else
                    return null;
            }
        }

        public ColorSchemeManager()
        {
            Items = new List<ColorScheme>();
        }

        public void SelectScheme(string name)
        {
            int index = Items.FindIndex(a => a.SchemeName.Equals(name));
            if (index != selectedSchemeIndex)
            {
                selectedSchemeIndex = index;
                SchemeChanded?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Load(string path)
        {
            if (File.Exists(path))
            {
                var res = JsonConvert.DeserializeObject<ColorSchemeManager>(File.ReadAllText(path));
                Items = new List<ColorScheme>(res.Items);
                res.Items.Clear();
            }
            else
            {
                Items = new List<ColorScheme>();
                Items.Add(ColorScheme.GetDefaultColorScheme());
                Items.Add(ColorScheme.GetDarkDefaultColorScheme());
                Save(path);
            }
        }

        public void Save(string savePath)
        {
            File.WriteAllText(savePath, JsonConvert.SerializeObject(this));
        }

        public void ChangeColorScheme(Control ctrl, ColorScheme scheme)
        {
            if (ctrl is Button)
            {
                ((Button)ctrl).BackColor = scheme.ButtonStyle.BackColor;
                ((Button)ctrl).ForeColor = scheme.ButtonStyle.ForeColor;
                if (((Button)ctrl).BackColor == SystemColors.Control)
                    ((Button)ctrl).UseVisualStyleBackColor = true;
                else
                    ((Button)ctrl).UseVisualStyleBackColor = false;
            }
            else if (ctrl is TextBox || ctrl is RichTextBox || ctrl is NumericUpDown)
            {
                ctrl.BackColor = scheme.TextBoxStyle.BackColor;
                ctrl.ForeColor = scheme.TextBoxStyle.ForeColor;
            }
            else if (ctrl is ListBox || ctrl is CheckedListBox || ctrl is ComboBox)
            {
                ctrl.BackColor = scheme.ComboAndListBoxStyle.BackColor;
                ctrl.ForeColor = scheme.ComboAndListBoxStyle.ForeColor;
            }
            else if (ctrl is Form)
            {
                ((Form)ctrl).BackColor = scheme.FormStyle.BackColor;
                ((Form)ctrl).ForeColor = scheme.FormStyle.ForeColor;
            }
            else if (ctrl is DataGridView)
            {
                ((DataGridView)ctrl).DefaultCellStyle.BackColor = scheme.FormStyle.BackColor;
                ((DataGridView)ctrl).DefaultCellStyle.ForeColor = scheme.FormStyle.ForeColor;

                ((DataGridView)ctrl).BackgroundColor = scheme.GrigStyle.GridBackColor;
                ((DataGridView)ctrl).GridColor = scheme.GrigStyle.GridColor;
                ((DataGridView)ctrl).BackColor = scheme.GrigStyle.BackColor;
                ((DataGridView)ctrl).ForeColor = scheme.GrigStyle.ForeColor;
                ((DataGridView)ctrl).DefaultCellStyle.BackColor = scheme.GrigStyle.CellBackColor;
                ((DataGridView)ctrl).DefaultCellStyle.ForeColor = scheme.GrigStyle.CellForeColor;
                ((DataGridView)ctrl).DefaultCellStyle.SelectionBackColor = scheme.GrigStyle.SelectionCellBackColor;
                ((DataGridView)ctrl).DefaultCellStyle.SelectionForeColor = scheme.GrigStyle.SelectionCellForeColor;

                ((DataGridView)ctrl).ColumnHeadersDefaultCellStyle.BackColor = scheme.GrigStyle.CellBackColor;
                ((DataGridView)ctrl).ColumnHeadersDefaultCellStyle.ForeColor = scheme.GrigStyle.CellForeColor;
                ((DataGridView)ctrl).ColumnHeadersDefaultCellStyle.SelectionBackColor = scheme.GrigStyle.SelectionCellBackColor;
                ((DataGridView)ctrl).ColumnHeadersDefaultCellStyle.SelectionForeColor = scheme.GrigStyle.SelectionCellForeColor;

                ((DataGridView)ctrl).ColumnHeadersDefaultCellStyle.BackColor = scheme.GrigStyle.CellBackColor;
                ((DataGridView)ctrl).ColumnHeadersDefaultCellStyle.ForeColor = scheme.GrigStyle.CellForeColor;
                ((DataGridView)ctrl).ColumnHeadersDefaultCellStyle.SelectionBackColor = scheme.GrigStyle.SelectionCellBackColor;
                ((DataGridView)ctrl).ColumnHeadersDefaultCellStyle.SelectionForeColor = scheme.GrigStyle.SelectionCellForeColor;

                ((DataGridView)ctrl).EnableHeadersVisualStyles = false;
            }
            else if (ctrl is TabPage)
            {
                ((TabPage)ctrl).BackColor = scheme.TabControlStyle.BackColor;
                ((TabPage)ctrl).ForeColor = scheme.TabControlStyle.ForeColor;

                if (((TabPage)ctrl).BackColor == Color.Transparent || ((TabPage)ctrl).BackColor == SystemColors.Window)
                {
                    ((TabControl)((TabPage)ctrl).Parent).DrawMode = TabDrawMode.Normal;
                }
                else
                    ((TabControl)((TabPage)ctrl).Parent).DrawMode = TabDrawMode.OwnerDrawFixed;
            }
            else if (ctrl is Manina.Windows.Forms.TabControl)
            {
                Manina.Windows.Forms.TabControl tc = (Manina.Windows.Forms.TabControl)ctrl;
                tc.BackColor = scheme.TabControlStyle.BackColor;
                tc.ForeColor = scheme.TabControlStyle.ForeColor;
                foreach (var item in tc.Tabs)
                {
                    item.ForeColor = scheme.TabControlStyle.PageForeColor;
                    item.BackColor = scheme.TabControlStyle.PageBackColor;
                }
            }
            else if (ctrl is MenuStrip)
            {
                MenuStrip menu = (MenuStrip)ctrl;
                menu.BackColor = scheme.OtherStyle.BackColor;
                menu.ForeColor = scheme.OtherStyle.ForeColor;
                ChangeColorOnToolStripItemsCollection(menu.Items, scheme);
            }
            else if (ctrl is ToolStrip)
            {
                ((ToolStrip)ctrl).BackColor = scheme.ToolStripStyle.BackColor;
                ((ToolStrip)ctrl).ForeColor = scheme.ToolStripStyle.ForeColor;
                for (int i = 0; i < ((ToolStrip)ctrl).Items.Count; i++)
                {
                    ((ToolStrip)ctrl).Items[i].ForeColor = scheme.OtherStyle.ForeColor;
                    ((ToolStrip)ctrl).Items[i].BackColor = scheme.OtherStyle.BackColor;
                    if (((ToolStrip)ctrl).Items[i] is ToolStripDropDownButton)
                    {
                        ChangeColorOnToolStripItemsCollection(((ToolStripDropDownItem)((ToolStrip)ctrl).Items[i]).DropDownItems, scheme);
                    }
                    else if (((ToolStrip)ctrl).Items[i] is ToolStripComboBox)
                    {
                        ((ToolStrip)ctrl).Items[i].ForeColor = scheme.ComboAndListBoxStyle.ForeColor;
                        ((ToolStrip)ctrl).Items[i].BackColor = scheme.ComboAndListBoxStyle.BackColor;
                    }
                }
            }
            else if (ctrl is Label || ctrl is CheckBox)
            {
                ctrl.BackColor = scheme.LabelStyle.BackColor;
                ctrl.ForeColor = scheme.LabelStyle.ForeColor;
            }

            else
            {
                ctrl.BackColor = scheme.OtherStyle.BackColor;
                ctrl.ForeColor = scheme.OtherStyle.ForeColor;
            }
        }

        private void ChangeColorOnToolStripItemsCollection(ToolStripItemCollection itemCollection, ColorScheme scheme)
        {
            for (int i = 0; i < itemCollection.Count; i++)
            {
                itemCollection[i].ForeColor = scheme.ToolStripStyle.ForeColor;
                itemCollection[i].BackColor = scheme.ToolStripStyle.BackColor;
                var drItems = ((ToolStripDropDownItem)itemCollection[i]).DropDownItems;
                if (drItems.Count > 0)
                {
                    ChangeColorOnToolStripItemsCollection(drItems, scheme);
                }
            }
        }

        public void ChangeColorSchemeInConteiner(Control.ControlCollection collection, ColorScheme scheme)
        {
            foreach (Control item in collection)
            {
                if (item.Controls.Count > 0)
                {
                    ChangeColorSchemeInConteiner(item.Controls, scheme);
                }
                ChangeColorScheme(item, scheme);
            }
        }


        public class ColorScheme
        {
            public string SchemeName { get; set; }
            public GenericColorData ButtonStyle { get; set; }
            public GenericColorData TextBoxStyle { get; set; }
            public GenericColorData ComboAndListBoxStyle { get; set; }
            public GenericColorData FormStyle { get; set; }
            public GridColorData GrigStyle { get; set; }
            public TabColorData TabControlStyle { get; set; }
            public GenericColorData ToolStripStyle { get; set; }
            public GenericColorData LabelStyle { get; set; }
            public GenericColorData OtherStyle { get; set; }

            public override string ToString()
            {
                return SchemeName;
            }


            public static ColorScheme GetDefaultColorScheme()
            {
                ColorScheme scheme = new ColorScheme();
                scheme.SchemeName = "Classic";
                scheme.ButtonStyle = new GenericColorData() { BackColor = SystemColors.Control, ForeColor = SystemColors.ControlText };
                scheme.TextBoxStyle = new GenericColorData() { BackColor = SystemColors.Window, ForeColor = SystemColors.ControlText };
                scheme.ComboAndListBoxStyle = new GenericColorData() { BackColor = SystemColors.Window, ForeColor = SystemColors.ControlText };
                scheme.FormStyle = new GenericColorData() { BackColor = SystemColors.Control, ForeColor = SystemColors.ControlText };
                scheme.GrigStyle = new GridColorData()
                {
                    BackColor = SystemColors.Control,
                    ForeColor = SystemColors.ControlText,
                    GridBackColor = SystemColors.ControlDark,
                    GridColor = SystemColors.ControlDark,
                    CellBackColor = SystemColors.Window,
                    CellForeColor = SystemColors.ControlText,
                    SelectionCellBackColor = SystemColors.Highlight,
                    SelectionCellForeColor = SystemColors.HighlightText
                };
                scheme.TabControlStyle = new TabColorData()
                {
                    BackColor = SystemColors.Control,
                    ForeColor = SystemColors.ControlText,
                    PageBackColor = SystemColors.Window,
                    PageForeColor = SystemColors.ControlText
                };
                scheme.ToolStripStyle = new GenericColorData() { BackColor = SystemColors.Control, ForeColor = SystemColors.ControlText };
                scheme.LabelStyle = new GenericColorData() { BackColor = Color.Transparent, ForeColor = SystemColors.ControlText };
                scheme.OtherStyle = new GenericColorData() { BackColor = SystemColors.Control, ForeColor = SystemColors.ControlText };
                return scheme;
            }

            public static ColorScheme GetDarkDefaultColorScheme()
            {
                ColorScheme scheme = new ColorScheme();
                scheme.SchemeName = "Dark";
                scheme.ButtonStyle = new GenericColorData() { BackColor = Color.FromArgb(50, 50, 50), ForeColor = SystemColors.HighlightText };
                scheme.TextBoxStyle = new GenericColorData() { BackColor = Color.FromArgb(35, 35, 35), ForeColor = SystemColors.HighlightText };
                scheme.ComboAndListBoxStyle = new GenericColorData() { BackColor = Color.FromArgb(35, 35, 35), ForeColor = SystemColors.HighlightText };
                scheme.FormStyle = new GenericColorData() { BackColor = Color.FromArgb(40, 40, 40), ForeColor = SystemColors.HighlightText };
                scheme.GrigStyle = new GridColorData()
                {
                    BackColor = Color.FromArgb(40, 40, 40),
                    ForeColor = SystemColors.HighlightText,
                    GridBackColor = Color.FromArgb(40, 40, 40),
                    GridColor = Color.FromArgb(70, 70, 70),
                    CellBackColor = Color.FromArgb(40, 40, 40),
                    CellForeColor = SystemColors.HighlightText,
                    SelectionCellBackColor = Color.FromArgb(95, 95, 95),
                    SelectionCellForeColor = SystemColors.HighlightText
                };
                scheme.TabControlStyle = new TabColorData()
                {
                    BackColor = Color.FromArgb(55, 55, 55),
                    ForeColor = SystemColors.HighlightText,
                    PageBackColor = Color.FromArgb(40, 40, 40),
                    PageForeColor = SystemColors.HighlightText
                };
                scheme.ToolStripStyle = new GenericColorData() { BackColor = Color.FromArgb(40, 40, 40), ForeColor = SystemColors.HighlightText };
                scheme.LabelStyle = new GenericColorData() { BackColor = Color.Transparent, ForeColor = SystemColors.HighlightText };
                scheme.OtherStyle = new GenericColorData() { BackColor = Color.FromArgb(40, 40, 40), ForeColor = SystemColors.HighlightText };

                return scheme;
            }
        }

        public class GenericColorData
        {
            public Color BackColor { get; set; }
            public Color ForeColor { get; set; }
        }

        public class GridColorData : GenericColorData
        {
            public Color GridBackColor { get; set; }
            public Color GridColor { get; set; }
            public Color CellBackColor { get; set; }
            public Color CellForeColor { get; set; }
            public Color SelectionCellBackColor { get; set; }
            public Color SelectionCellForeColor { get; set; }
        }

        public class TabColorData : GenericColorData
        {
            public Color PageBackColor { get; set; }
            public Color PageForeColor { get; set; }
        }
    }
}

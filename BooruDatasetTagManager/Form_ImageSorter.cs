using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BooruDatasetTagManager
{
    public partial class Form_ImageSorter : Form
    {
        public Form_ImageSorter(ImageSorter sorter)
        {
            imgSorter = sorter;
            InitializeComponent();
            TableLayoutPanel rootPanel = new TableLayoutPanel();
            rootPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            rootPanel.Dock = DockStyle.Fill;
            splitContainer1.Panel1.Controls.Add(rootPanel);
            sorterElements = new Dictionary<string, Label>();
            CreateGrid(rootPanel, imgSorter.RootItem.Items, true);
        }
        private ImageSorter imgSorter;
        private Dictionary<string, Label> sorterElements;
        private string selectedElement;
        private List<string> imagesExt = new List<string>() { ".jpg", ".png", ".bmp", ".jpeg", ".webp" };
        private void Form_ImageSorter_Load(object sender, EventArgs e)
        {

        }


        private void CreateGrid(TableLayoutPanel rootCtrl, List<ImageSorter.SortItem> items, bool vertical)
        {
            TableLayoutPanel panel = rootCtrl;// new TableLayoutPanel();
            panel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            if (vertical)
            {
                panel.ColumnCount = items.Count;
                panel.RowCount = 1;
                float colSize = 100f / (float)items.Count;
                for (int i = 0; i < items.Count; i++)
                    panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, colSize));
            }
            else
            {
                panel.ColumnCount = 1;
                panel.RowCount = items.Count;
                float colSize = 100f / (float)items.Count;
                for (int i = 0; i < items.Count; i++)
                    panel.RowStyles.Add(new RowStyle(SizeType.Percent, colSize));
            }
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Items.Count > 0)
                {
                    TableLayoutPanel childPanel = new TableLayoutPanel();
                    childPanel.Dock = DockStyle.Fill;
                    if (vertical)
                        panel.Controls.Add(childPanel, i, 0);
                    else
                        panel.Controls.Add(childPanel, 0, i);
                    CreateGrid(childPanel, items[i].Items, !vertical);
                }
                else
                {
                    Label lbl = new Label();
                    lbl.AllowDrop = true;
                    lbl.Font = new Font(lbl.Font.FontFamily, 12f, FontStyle.Bold);
                    lbl.Name = items[i].Id;
                    lbl.Text = items[i].Id.Replace("|", "\n");
                    lbl.AutoSize = false;
                    lbl.TextAlign = ContentAlignment.MiddleCenter;
                    lbl.Dock = DockStyle.Fill;
                    if (vertical)
                        panel.Controls.Add(lbl, i, 0);
                    else
                        panel.Controls.Add(lbl, 0, i);
                    sorterElements.Add(items[i].Id, lbl);
                    lbl.DragEnter += Lbl_DragEnter;
                    lbl.DragDrop += Lbl_DragDrop;
                    lbl.MouseClick += Lbl_MouseClick;
                }
            }
        }

        private void Lbl_MouseClick(object sender, MouseEventArgs e)
        {
            SelectCell(((Label)sender).Name);
            UpdateFileList();
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripElement.Show((Label)sender, new Point(e.X, e.Y));
            }
        }

        private void UpdateFileList()
        {
            listBoxFiles.Items.Clear();
            if (imgSorter.FileQueue.ContainsKey(selectedElement))
                listBoxFiles.Items.AddRange(imgSorter.FileQueue[selectedElement].ToArray());
        }

        private void Lbl_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                AddFilesInQueue(((Label)sender).Name, fileNames);
                UpdateFileList();
            }
        }

        private void AddFilesInQueue(string element, string[] fileList)
        {
            foreach (string file in fileList)
            {
                FileAttributes attr = File.GetAttributes(file);

                if (attr.HasFlag(FileAttributes.Directory))
                    imgSorter.AddFileRangeQueue(element, GetImgFilesFromDirectory(file));
                else
                {
                    if (imagesExt.Contains(Path.GetExtension(file).ToLower()))
                        imgSorter.AddFileQueue(element, file);
                }
            }
        }

        private string[] GetImgFilesFromDirectory(string dir)
        {
            string[] imgs = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories);
            return imgs.Where(a => imagesExt.Contains(Path.GetExtension(a).ToLower())).OrderBy(a => a, new FileNamesComparer()).ToArray();
        }

        private void Lbl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                SelectCell(((Label)sender).Name);
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void SelectCell(string name)
        {
            selectedElement = name;
            foreach (var item in sorterElements)
            {
                if (item.Key == name)
                    item.Value.BackColor = Color.LightGreen;
                else
                    item.Value.BackColor = SystemColors.Window;
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteDataFromClipboard();
        }

        private void PasteDataFromClipboard()
        {
            if (string.IsNullOrEmpty(selectedElement))
                return;
            if (Clipboard.ContainsFileDropList())
            {
                string[] fileList = Clipboard.GetFileDropList().Cast<string>().ToArray();
                if (fileList.Length > 0)
                {
                    AddFilesInQueue(selectedElement, fileList);
                    UpdateFileList();
                }
            }
        }

        private void Form_ImageSorter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
                PasteDataFromClipboard();
        }

        private async void toolStripButton2_Click(object sender, EventArgs e)
        {
            StatusLabel.Text = "Please wait, copying files...";
            this.Enabled = false;
            await imgSorter.StartCopyAsync();
            StatusLabel.Text = "Complete!";
            this.Enabled = true;
            listBoxFiles.Items.Clear();
        }
    }
}

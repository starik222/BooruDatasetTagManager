using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Translator;

namespace BooruDatasetTagManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tagsBuffer = new List<string>();
            fPreview = new Form_preview();
            fPreview.FormClosed += FPreview_FormClosed;

            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            dataGridView1.RowsAdded += DataGridView1_RowsAdded;
            dataGridView1.RowsRemoved += DataGridView1_RowsRemoved;
        }

        private void DataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            SetChangedStatus(true);
        }

        private void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            SetChangedStatus(true);
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            SetChangedStatus(true);
        }

        List<string> tagsBuffer;

        private bool isAllTags = true;
        private bool isTranslate = false;
        private bool isFiltered = false;

        private Form_preview fPreview;
        private bool isShowPreview = false;
        private List<ListViewItem> listViewItems;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void FPreview_FormClosed(object sender, FormClosedEventArgs e)
        {
            fPreview = new Form_preview();
        }

        private void SetChangedStatus(bool changed)
        {
            toolStripButton1.Enabled = changed;
            toolStripButton11.Enabled = changed;
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            if (openFolderDialog.ShowDialog() != DialogResult.OK)
                return;
            Program.DataManager = new DatasetManager();
            Program.DataManager.LoadFromFolder(openFolderDialog.Folder);

            listView1.LargeImageList = Program.DataManager.Images;
            listView1.TileSize = new Size(Program.DataManager.Images.ImageSize.Width + 10, Program.DataManager.Images.ImageSize.Height + 10);
            listViewItems = new List<ListViewItem>();
            foreach (var item in Program.DataManager.DataSet)
            {
                ListViewItem lvItem = listView1.Items.Add(item.Key, "", item.Key);
                listViewItems.Add(lvItem);
            }
            Program.DataManager.UpdateData();
            BindTagList();
        }

        private async void FillTranslation()
        {
            LockEdit(true);
            SetStatus("Translating, please wait...");
            List<string> toTrans = new List<string>();
            string transLang = Program.Settings.TranslationLanguage;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                toTrans.Add((string)dataGridView1[0, i].Value);
            }
            await Task.Run(() =>
            {
                for (int i = 0; i < toTrans.Count; i++)
                {
                    toTrans[i] = Program.tools.TranslateText(toTrans[i], transLang, true);
                }
            });
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1["Translation", i].Value = toTrans[i];
            }
            SetStatus("Translation completed");
            LockEdit(false);
        }

        private void LockEdit(bool locked)
        {
            toolStrip2.Enabled = !locked;
            toolStrip1.Enabled = !locked;
            dataGridView1.ReadOnly = locked;
            dataGridView1.AllowDrop = !locked;
            dataGridView2.ReadOnly = locked;
            listView1.Enabled = !locked;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSelectedImageToGrid();
        }

        private void LoadSelectedImageToGrid()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                List<string> tags = Program.DataManager.DataSet[listView1.SelectedItems[0].Name].Tags;
                dataGridView1.Rows.Clear();
                foreach (var item in tags)
                    dataGridView1.Rows.Add(item);
                if (isShowPreview)
                {
                    fPreview.pictureBox1.Image = Program.DataManager.DataSet[listView1.SelectedItems[0].Name].Img;
                    fPreview.Show();
                }
                if (isTranslate)
                    FillTranslation();
                SetChangedStatus(false);
            }
        }

        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;
        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {

                    // Proceed with the drag and drop, passing in the list item.                    
                    DragDropEffects dropEffect = dataGridView1.DoDragDrop(
                    dataGridView1.Rows[rowIndexFromMouseDown],
                    DragDropEffects.Move);
                }
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            rowIndexFromMouseDown = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if (rowIndexFromMouseDown != -1)
            {
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                               e.Y - (dragSize.Height / 2)),
                                    dragSize);
            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = dataGridView1.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            rowIndexOfItemUnderMouseToDrop =
                dataGridView1.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                DataGridViewRow rowToMove = e.Data.GetData(
                    typeof(DataGridViewRow)) as DataGridViewRow;
                dataGridView1.Rows.RemoveAt(rowIndexFromMouseDown);
                dataGridView1.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);
                dataGridView1.ClearSelection();
                dataGridView1[0, rowIndexOfItemUnderMouseToDrop].Selected = true;

            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0)
                dataGridView1.Rows.Add();
            else
            {
                dataGridView1.Rows.Insert(dataGridView1.SelectedCells[0].RowIndex+1);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0)
                return;
            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedCells[0].RowIndex);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0 || dataGridView1.SelectedCells[0].RowIndex == 0)
                return;
            int curIndex = dataGridView1.SelectedCells[0].RowIndex;
            string upperValue = (string)dataGridView1[0, curIndex - 1].Value;
            if (isTranslate)
            {
                string upperValueTrans = (string)dataGridView1[1, curIndex - 1].Value;
                dataGridView1[1, curIndex - 1].Value = dataGridView1[1, curIndex].Value;
                dataGridView1[1, curIndex].Value = upperValueTrans;
            }
            dataGridView1[0, curIndex - 1].Value = dataGridView1[0, curIndex].Value;
            dataGridView1[0, curIndex].Value = upperValue;
            dataGridView1.ClearSelection();
            dataGridView1[0, curIndex - 1].Selected = true;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0 || dataGridView1.SelectedCells[0].RowIndex == dataGridView1.RowCount-1)
                return;
            int curIndex = dataGridView1.SelectedCells[0].RowIndex;
            string lowerValue = (string)dataGridView1[0, curIndex + 1].Value;

            if (isTranslate)
            {
                string lowerValueTrans = (string)dataGridView1[1, curIndex + 1].Value;
                dataGridView1[1, curIndex + 1].Value = dataGridView1[1, curIndex].Value;
                dataGridView1[1, curIndex].Value = lowerValueTrans;
            }

            dataGridView1[0, curIndex + 1].Value = dataGridView1[0, curIndex].Value;
            dataGridView1[0, curIndex].Value = lowerValue;
            dataGridView1.ClearSelection();
            dataGridView1[0, curIndex + 1].Selected = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            List<string> nTags = new List<string>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                nTags.Add((string)dataGridView1[0, i].Value);
            }
            Program.DataManager.DataSet[listView1.SelectedItems[0].Name].Tags = nTags;
            Program.DataManager.UpdateData();
            BindTagList();
            SetChangedStatus(false);
            SetStatus("Saved");
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            isAllTags = !isAllTags;
            if (isAllTags)
                label3.Text = "All tags";
            else
                label3.Text = "Common tags";
            BindTagList();
        }

        private void BindTagList()
        {
            if (Program.DataManager == null)
                return;
            if (isAllTags)
            {
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = Program.DataManager.AllTags;
                dataGridView2.Refresh();
            }
            else
            {
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = Program.DataManager.CommonTags;
                dataGridView2.Refresh();
            }
            dataGridView2.Columns["Tag"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Form_addTag addTag = new Form_addTag();
            if (dataGridView2.SelectedCells.Count > 0)
            {
                addTag.textBox1.Text = (string)dataGridView2.SelectedCells[0].Value;
                addTag.textBox1.SelectAll();
            }
            if (addTag.ShowDialog() == DialogResult.OK)
            {
                DatasetManager.AddingType addType = (DatasetManager.AddingType)Enum.Parse(typeof(DatasetManager.AddingType), (string)addTag.comboBox1.SelectedItem);
                Program.DataManager.AddTagToAll(addTag.textBox1.Text, addType);
                Program.DataManager.UpdateData();
                int valIndex = IndexOfValueInGrig(dataGridView1, "ImageTags", addTag.textBox1.Text);
                if (valIndex != -1)
                {
                    if (addType != DatasetManager.AddingType.Down)
                    {
                        dataGridView1.Rows.RemoveAt(valIndex);
                        int index = 0;
                        if (addType == DatasetManager.AddingType.Center)
                            index = dataGridView1.RowCount / 2;

                        dataGridView1.Rows.Insert(index, addTag.textBox1.Text);
                    }
                }
                else
                {
                    if (addType == DatasetManager.AddingType.Down)
                    {
                        dataGridView1.Rows.Add(addTag.textBox1.Text);

                    }
                    else
                    {
                        int index = 0;
                        if (addType == DatasetManager.AddingType.Center)
                            index = dataGridView1.RowCount / 2;
                        dataGridView1.Rows.Insert(index, addTag.textBox1.Text);
                    }
                        
                }
            }
            addTag.Close();
            BindTagList();
        }

        private int IndexOfValueInGrig(DataGridView gridView, string colName, string value)
        {
            for (int i = 0; i < gridView.RowCount; i++)
            {
                if (gridView[colName, i].Value != DBNull.Value)
                {
                    if ((string)gridView[colName, i].Value == value)
                        return i;
                }
                else if (value == null)
                    return i;
            }
            return -1;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count == 0)
                return;
            Form_replaceAll replaceAll = new Form_replaceAll();
            replaceAll.comboBox1.DataSource = Program.DataManager.AllTags;
            replaceAll.comboBox1.DisplayMember = "Tag";
            replaceAll.comboBox1.SelectedIndex = dataGridView2.SelectedCells[0].RowIndex;
            replaceAll.comboBox2.Items.AddRange(Program.DataManager.AllTags.Select(a => a.Tag).ToArray());
            if (replaceAll.ShowDialog() == DialogResult.OK)
            {
                Program.DataManager.ReplaceTagInAll(((TagValue)replaceAll.comboBox1.SelectedItem).Tag, (string)replaceAll.comboBox2.Text);
                Program.DataManager.UpdateData();
                int indexToReplace = -1;
                int indexToDelete = -1;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    string srcText = (string)dataGridView1[0, i].Value;
                    if (srcText == (string)replaceAll.comboBox2.Text)
                        indexToDelete = i;
                    else if (srcText == ((TagValue)replaceAll.comboBox1.SelectedItem).Tag)
                        indexToReplace = i;
                }
                if (indexToReplace != -1)
                {
                    dataGridView1[0, indexToReplace].Value = (string)replaceAll.comboBox2.Text;
                    if (indexToDelete != -1)
                        dataGridView1.Rows.RemoveAt(indexToDelete);
                }
            }
            replaceAll.Close();
            BindTagList();
        }

        private void saveAllChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.DataManager == null)
                return;
            Program.DataManager.SaveAll();
            SetStatus("Saved!");
            MessageBox.Show("Saved!");
        }

        private void showPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.DataManager == null)
                return;
            isShowPreview = !isShowPreview;
            showPreviewToolStripMenuItem.Checked = isShowPreview;
            if (isShowPreview)
            {
                if (listView1.SelectedItems.Count > 0)
                    fPreview.pictureBox1.Image = Program.DataManager.DataSet[listView1.SelectedItems[0].Name].Img;
                fPreview.Show();
            }
            else
            {
                fPreview.pictureBox1.Image = null;
                fPreview.Hide();
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                tagsBuffer.Clear();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    tagsBuffer.Add((string)dataGridView1[0, i].Value);
                }
                SetStatus("Copied!");
            }
            else
            {
                MessageBox.Show("First select an image");
            }
        }

        private void SetStatus(string text)
        {
            toolStripStatusLabel1.Text = text;
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < tagsBuffer.Count; i++)
                {
                    dataGridView1.Rows.Add(tagsBuffer[i]);
                }
                SetStatus("Pasted!");
            }
            else
            {
                MessageBox.Show("First select an image");
            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            LoadSelectedImageToGrid();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count > 0)
            {
                string delTag = (string)dataGridView2.SelectedCells[0].Value;
                Program.DataManager.DeleteTagFromAll(delTag);
                Program.DataManager.UpdateData();
                int index = -1;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if ((string)dataGridView1[0, i].Value == delTag)
                    {
                        index = i;
                        break;
                    }
                }
                if (index != -1)
                    dataGridView1.Rows.RemoveAt(index);
            }
            BindTagList();
        }

        private void translateTagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isTranslate = !isTranslate;
            translateTagsToolStripMenuItem.Checked = isTranslate;
            if (isTranslate)
            {
                dataGridView1.Columns.Add("Translation", "Translation");
                dataGridView1.Columns["Translation"].ReadOnly = true;
                dataGridView1.Columns["Translation"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            else
            {
                dataGridView1.Columns.Remove("Translation");
            }
        }

        private int findIndex = -1;
        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void SetFilter()
        {
            if (dataGridView2.SelectedCells.Count > 0)
            {
                if (isFiltered)
                {
                    ResetFilter();
                }
                string findTag = (string)dataGridView2.SelectedCells[0].Value;
                var res = Program.DataManager.FindTag(findTag);
                List<ListViewItem> findResult = new List<ListViewItem>();
                foreach (var item in res)
                {
                    findResult.AddRange(listView1.Items.Find(item, false));
                }
                listView1.Clear();
                listView1.Items.AddRange(findResult.ToArray());
                isFiltered = true;
                toolStripButton14.Enabled = true;
            }
        }

        private void ResetFilter()
        {
            if (isFiltered)
            {
                listView1.Clear();
                listView1.Items.AddRange(listViewItems.ToArray());
                isFiltered = false;
                toolStripButton14.Enabled = false;
            }
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            ResetFilter();
        }

        private void listView1_SizeChanged(object sender, EventArgs e)
        {
            //int s = (listView1.Width - 10) / 2;
            //Program.DataManager.UpdateImageList(s, s);
            //listView1.TileSize = new Size(s, s);
            //listView1.RedrawItems(0, listView1.Items.Count - 1, false);
            //listView1.Items[0].ima
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dataGridView1.SelectedCells.Count > 0)
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedCells[0].RowIndex);
            }
        }

        private void loadLossFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            Program.DataManager.LoadLossFromFile(openFileDialog.FileName);
            foreach (var item in Program.DataManager.DataSet)
            {
                listView1.Items[item.Key].Text = $"Loss: {item.Value.Loss}\nLast: {item.Value.LastLoss}";
            }
            listView1.View = View.LargeIcon;
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                dataGridView1.Rows.Clear();
                string text = Clipboard.GetText();
                string[] lines = text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lines.Length; i++)
                    dataGridView1.Rows.Add(lines[i].ToLower().Trim());
            }
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            List<string> lines = new List<string>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
                lines.Add((string)dataGridView1[0, i].Value);
            Form_Edit fPrint = new Form_Edit();
            fPrint.textBox1.Text = string.Join(", ", lines);
            fPrint.Show();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

        }
    }
}

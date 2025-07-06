using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooruDatasetTagManager
{
    public partial class Form_TagImagesGrid : Form
    {
        private string currentTag = "";
        public Form_TagImagesGrid()
        {
            InitializeComponent();
            TrackBarZoom.TrackBar.Minimum = 1;
            TrackBarZoom.TrackBar.Maximum = 1000;
            TrackBarZoom.TrackBar.Value = Program.Settings.TagImagesGridSize;
            TrackBarZoom.TrackBar.SmallChange = 50;
            TrackBarZoom.ValueChanged += TrackBarZoom_ValueChanged;
            Program.ColorManager.ChangeColorScheme(this, Program.ColorManager.SelectedScheme);
            Program.ColorManager.ChangeColorSchemeInConteiner(Controls, Program.ColorManager.SelectedScheme);
            SwitchLanguage();
        }

        private void SwitchLanguage()
        {
            this.Text = I18n.GetText("UITagImagesGridForm");
            BtnTgOk.Text = I18n.GetText("SettingBtnSave");
            BtnTgCancel.Text = I18n.GetText("SettingBtnCancel");
            LabelGridZoomText.Text = I18n.GetText("LabelGridZoomText");
            toolStripStatusLabelMSForm.Text = I18n.GetText("ToolStripStatusLabelMSForm");
        }

        private void TrackBarZoom_ValueChanged(object sender, EventArgs e)
        {
            flowLayoutPanelImages.SuspendLayout();
            for (int i = 0; i < flowLayoutPanelImages.Controls.Count; i++)
            {
                if (flowLayoutPanelImages.Controls[i] is CustomPictureBoxWithYN)
                {
                    CustomPictureBoxWithYN c = (CustomPictureBoxWithYN)flowLayoutPanelImages.Controls[i];
                    c.SetSize(TrackBarZoom.TrackBar.Value);

                }
            }
            Program.Settings.TagImagesGridSize = TrackBarZoom.TrackBar.Value;
            Program.Settings.SaveSettings();
            flowLayoutPanelImages.ResumeLayout();
        }

        private void Form_TagImagesGrid_Load(object sender, EventArgs e)
        {
            this.Text += ": " + currentTag;
        }
        /// <summary>
        /// Used to change the tag list when multiple selections are made
        /// </summary>
        /// <param name="item"></param>
        /// <param name="tag"></param>
        public void AddDataItemsEditTagInSelected(List<DatasetManager.DataItem> item, string tag)
        {
            currentTag = tag;
            List<KeyValuePair<DatasetManager.DataItem, bool>> toSort = new List<KeyValuePair<DatasetManager.DataItem, bool>>();
            item.ForEach(a=> toSort.Add(new KeyValuePair<DatasetManager.DataItem, bool>(a, a.Tags.Contains(tag))));
            var sotedList = toSort.OrderBy(a => a.Value).ToList();
            foreach (var listItem in sotedList)
            {
                CustomPictureBoxWithYN pictureBox = new CustomPictureBoxWithYN(TrackBarZoom.TrackBar.Value, TrackBarZoom.TrackBar.Value, listItem.Value);
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.SetSelectionMode(true);
                pictureBox.Image = Program.DataManager.GetImageFromFileWithCache(listItem.Key.ImageFilePath);
                pictureBox.SetDataSetItem(listItem.Key);
                flowLayoutPanelImages.Controls.Add(pictureBox);
            }
        }
        /// <summary>
        /// Used to modify selected images in a dataset
        /// </summary>
        /// <param name="item"></param>
        /// <param name="selected"></param>
        public void AddDataItemChangeSelection(DatasetManager.DataItem item, bool selected)
        {
            CustomPictureBoxWithYN pictureBox = new CustomPictureBoxWithYN(TrackBarZoom.TrackBar.Value, TrackBarZoom.TrackBar.Value, selected);
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.SetSelectionMode(true);
            pictureBox.Image = Program.DataManager.GetImageFromFileWithCache(item.ImageFilePath);
            pictureBox.SetDataSetItem(item);
            flowLayoutPanelImages.Controls.Add(pictureBox);
        }

        public List<KeyValuePair<DatasetManager.DataItem, bool>> GetResult(bool allData = false)
        {
            List<KeyValuePair<DatasetManager.DataItem, bool>> result = new List<KeyValuePair<DatasetManager.DataItem, bool>>();
            for (int i = 0; i < flowLayoutPanelImages.Controls.Count; i++)
            {
                if (flowLayoutPanelImages.Controls[i] is CustomPictureBoxWithYN)
                {
                    CustomPictureBoxWithYN c = (CustomPictureBoxWithYN)flowLayoutPanelImages.Controls[i];
                    if (allData || c.StateChanged)
                        result.Add(new KeyValuePair<DatasetManager.DataItem, bool>(c.GetDataSetItem(), c.StateYes));
                }
            }
            return result;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            for (int i = 0; i < flowLayoutPanelImages.Controls.Count; i++)
            {
                if (flowLayoutPanelImages.Controls[i] is CustomPictureBoxWithYN)
                {
                    CustomPictureBoxWithYN c = (CustomPictureBoxWithYN)flowLayoutPanelImages.Controls[i];
                    c.Image = null;

                }
            }
            base.OnClosing(e);
        }

        private void BtnTgOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void BtnTgCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}

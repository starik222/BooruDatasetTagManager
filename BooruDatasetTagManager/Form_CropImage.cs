using BooruDatasetTagManager.AiApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooruDatasetTagManager
{
    public partial class Form_CropImage : Form
    {
        public Form_CropImage()
        {
            InitializeComponent();
            Program.ColorManager.ChangeColorScheme(this, Program.ColorManager.SelectedScheme);
            Program.ColorManager.ChangeColorSchemeInConteiner(Controls, Program.ColorManager.SelectedScheme);
            SwitchLanguage();
        }

        private void SwitchLanguage()
        {
            this.Text = I18n.GetText("UICropImagesForm");
            buttonCheckConnection.Text = I18n.GetText("UICropImagesFormCheckBtn");
            groupBox1.Text = I18n.GetText("UICropImagesFormGroupText");
            label4.Text = I18n.GetText("UICropImagesFormModeLabel");
            radioButtonAllImages.Text = I18n.GetText("UICropImagesFormRadioAll");
            radioButtonOnlySelected.Text = I18n.GetText("UICropImagesFormRadioSelected");
            label1.Text = I18n.GetText("UICropImagesFormExcludeLabel");
            label2.Text = I18n.GetText("UICropImagesFormIncludeLabel");
            button5.Text = I18n.GetText("UICropImagesFormObSearchTextBtn");
            button4.Text = I18n.GetText("UICropImagesFormCropTestBtn");
            label3.Text = I18n.GetText("UICropImagesFormDescriptionLabel");
        }
        private bool connectSuccess = false;
        private async void button1_Click(object sender, EventArgs e)
        {
            if (!Program.AutoTagger.IsConnected)
            {

                if (!await Program.AutoTagger.ConnectAsync())
                {
                    MessageBox.Show(I18n.GetText("TipAutoTagUnableConnect"));
                }
            }
            if (Program.AutoTagger.IsConnected)
            {
                if (!Program.AutoTagger.Config.Interrogators.Contains("moondream2"))
                {
                    MessageBox.Show("moondream2 model not found!");
                }
                else
                {
                    connectSuccess = true;
                    buttonCheckConnection.Text = "Success!";
                    buttonCheckConnection.Enabled = false;
                    groupBox1.Enabled = true;
                }
            }
        }

        public async Task<MoondreamRect[]> DetectObjectsOnImage(string imgFilePath, string detectObjects)
        {
            ModelParameters model = new ModelParameters() { ModelName = "moondream2" };
            model.AdditionalParameters.Add(new ModelAdditionalParameters()
            {
                Key = "cmd",
                Value = "Object_detection",
                Type = "list"
            });
            model.AdditionalParameters.Add(new ModelAdditionalParameters()
            {
                Key = "query",
                Value = detectObjects,
                Type = "string"
            });
            var result = await Program.AutoTagger.InterrogateImage(imgFilePath, new List<ModelParameters>() { model }, Program.Settings.AutoTagger.SerializeVramUsage, Program.Settings.AutoTagger.SkipInternetRequests);
            return JsonConvert.DeserializeObject<MoondreamRect[]>(result.Items.First().Value.First().Tag);
            //if (rects.Length == 0)
            //    return new Rectangle();
            //MoondreamRect maxRect = new MoondreamRect()
            //{
            //    x_min = rects.Min(x => x.x_min),
            //    y_min = rects.Min(y => y.y_min),
            //    x_max = rects.Max(x => x.x_max),
            //    y_max = rects.Max(y => y.y_max)
            //};
            //return maxRect.ToRealRect(imgWidth, imgHeight);
        }

        public async Task<Rectangle> CalcCropRectangle(string imgFilePath, string includeObjects, string excludeObjects)
        {
            int imgWidth = 0;
            int imgHeight = 0;
            using (var fileStream = new FileStream(imgFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var image = Image.FromStream(fileStream, false, false))
                {
                    imgHeight = image.Height;
                    imgWidth = image.Width;
                }
            }
            List<MoondreamRect> includeRects = null;
            List<MoondreamRect> excludeRects = null;
            if (!string.IsNullOrWhiteSpace(includeObjects))
            {
                includeRects = (await DetectObjectsOnImage(imgFilePath, includeObjects)).Select(a => a.ToRealCoordinates(imgWidth, imgHeight)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(excludeObjects))
            {
                excludeRects = (await DetectObjectsOnImage(imgFilePath, excludeObjects)).Select(a => a.ToRealCoordinates(imgWidth, imgHeight)).ToList();
            }

            if (includeRects != null && excludeRects == null)
            {
                if (includeRects.Count == 0)
                    return new Rectangle(0, 0, imgWidth, imgHeight);
                MoondreamRect maxRect = new MoondreamRect()
                {
                    x_min = includeRects.Min(x => x.x_min),
                    y_min = includeRects.Min(y => y.y_min),
                    x_max = includeRects.Max(x => x.x_max),
                    y_max = includeRects.Max(y => y.y_max)
                };
                return maxRect.ToRealRect();
            }
            else if (includeRects == null && excludeRects != null)
            {
                return RectangleOperations.FindLargestRectangle(new MoondreamRect(0, 0, imgWidth, imgHeight), excludeRects).ToRealRect();
            }
            else if (includeRects != null && excludeRects != null)
            {
                //Removing excluded objects from the area
                var result = RectangleOperations.FindLargestRectangleIncludeRequired(new MoondreamRect(0, 0, imgWidth, imgHeight), excludeRects, includeRects);
                //Adding required objects
                //foreach (var item in includeRects)
                //{
                //    result = RectangleOperations.Join(result, item);
                //}
                return result.ToRealRect();
            }
            else
            {
                return new Rectangle(0, 0, imgWidth, imgHeight);
            }
        }

        public async Task<Rectangle> CalcCropRectangle(string imgFilePath)
        {
            return await CalcCropRectangle(imgFilePath, textBoxInclude.Text, textBoxExclude.Text);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.jpg;*.png;*.bmp;*.jpeg";
            openFileDialog.Title = "Select image";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            button4.Enabled = false;
            var res = await CalcCropRectangle(openFileDialog.FileName, textBoxInclude.Text, textBoxExclude.Text);

            Image image = Image.FromFile(openFileDialog.FileName);

            using (Graphics g = Graphics.FromImage(image))
            {
                g.DrawRectangle(new Pen(Brushes.Green, 4), res);
            }
            button4.Enabled = true;
            Form_preview preview = new Form_preview();
            preview.Show(image);
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.jpg;*.png;*.bmp;*.jpeg";
            openFileDialog.Title = "Select image";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            button5.Enabled = false;


            Image image = Image.FromFile(openFileDialog.FileName);
            Rectangle[] incObj = new Rectangle[0];
            Rectangle[] exclObj = new Rectangle[0];
            if (!string.IsNullOrEmpty(textBoxInclude.Text))
            {
                incObj = (await DetectObjectsOnImage(openFileDialog.FileName, textBoxInclude.Text))
                    .Select(a => a.ToRealRect(image.Width, image.Height)).ToArray();
            }
            if (!string.IsNullOrEmpty(textBoxExclude.Text))
            {
                exclObj = (await DetectObjectsOnImage(openFileDialog.FileName, textBoxExclude.Text))
                    .Select(a => a.ToRealRect(image.Width, image.Height)).ToArray();
            }
            using (Graphics g = Graphics.FromImage(image))
            {
                foreach (var item in exclObj)
                    g.DrawRectangle(new Pen(Brushes.Red, 4), item);
                foreach (var item in incObj)
                    g.DrawRectangle(new Pen(Brushes.Green, 4), item);
            }
            button5.Enabled = true;
            Form_preview preview = new Form_preview();
            preview.Show(image);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Form_CropImage_Load(object sender, EventArgs e)
        {
            buttonCheckConnection.PerformClick();
        }
    }
}

using Newtonsoft.Json;
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

namespace BooruDatasetTagManager
{
    public partial class Form_BGRemover : Form
    {
        public Form_BGRemover()
        {
            InitializeComponent();
            Program.ColorManager.ChangeColorScheme(this, Program.ColorManager.SelectedScheme);
            Program.ColorManager.ChangeColorSchemeInConteiner(Controls, Program.ColorManager.SelectedScheme);
            SwitchLanguage();
        }

        private void SwitchLanguage()
        {
            this.Text = I18n.GetText("UIBGRemovalForm");
            buttonCheckConnection.Text = I18n.GetText("UIBGRemovalFormCheckBtn");
            groupBox1.Text = I18n.GetText("UIBGRemovalFormGroupText");
            label4.Text = I18n.GetText("UIBGRemovalFormModeLabel");
            radioButtonAllImages.Text = I18n.GetText("UICropImagesFormRadioAll");
            radioButtonOnlySelected.Text = I18n.GetText("UICropImagesFormRadioSelected");
            label1.Text = I18n.GetText("UIBGRemovalFormModelsLabel");
            buttonRemovingTest.Text = I18n.GetText("UIBGRemovalFormRemovingTestBtn");
        }
        private bool connectSuccess = false;
        private async void buttonCheckConnection_Click(object sender, EventArgs e)
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
                connectSuccess = true;
                buttonCheckConnection.Text = "Success!";
                buttonCheckConnection.Enabled = false;
                listBoxModels.Items.AddRange((await Program.AutoTagger.GetListModelsByType("rmbg2")).ToArray());
                groupBox1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (listBoxModels.SelectedIndex == -1)
                return;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.jpg;*.png;*.bmp;*.jpeg";
            openFileDialog.Title = "Select image";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            buttonRemovingTest.Enabled = false;
            var res = await RemoveBackgroundAsync(openFileDialog.FileName, (string)listBoxModels.SelectedItem);
            if (res == null)
            {
                buttonRemovingTest.Enabled = true;
                return;
            }
            Image img = null;
            using (var ms = new MemoryStream(res))
            {
                img = Image.FromStream(ms);
            }
            buttonRemovingTest.Enabled = true;
            Form_preview preview = new Form_preview();
            preview.Show(img);
        }

        public string GetSelectedModel()
        {
            if (listBoxModels.SelectedIndex == -1)
                return null;
            return (string)listBoxModels.SelectedItem;
        }

        public async Task<byte[]> RemoveBackgroundAsync(string imgFilePath, string model)
        {
            List<Image_Interrogator_Ns.NetworkInterrogationParameters> parameters = new List<Image_Interrogator_Ns.NetworkInterrogationParameters>();
            //List<Image_Interrogator_Ns.AdditionalNetworkParameter> additionalParameters = new List<Image_Interrogator_Ns.AdditionalNetworkParameter>();
            //additionalParameters.Add(new Image_Interrogator_Ns.AdditionalNetworkParameter()
            //{
            //    Key = "cmd",
            //    Value = "Object_detection",
            //    Type = "list"
            //});
            //additionalParameters.Add(new Image_Interrogator_Ns.AdditionalNetworkParameter()
            //{
            //    Key = "query",
            //    Value = detectObjects,
            //    Type = "string"
            //});
            var pData = new Image_Interrogator_Ns.NetworkInterrogationParameters() { InterrogatorNetwork = model };
            //pData.AdditionalParameters.AddRange(additionalParameters);
            parameters.Add(pData);
            var result = await Program.AutoTagger.EditImage(imgFilePath, parameters, Program.Settings.AutoTagger.SerializeVramUsage, Program.Settings.AutoTagger.SkipInternetRequests);
            if (result.Success)
                return result.ImageData;
            return null;
        }

        private void Form_BGRemover_Load(object sender, EventArgs e)
        {
            buttonCheckConnection.PerformClick();
        }
    }
}

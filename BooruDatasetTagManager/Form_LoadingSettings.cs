using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BooruDatasetTagManager
{
    public partial class Form_LoadingSettings : Form
    {
        public Form_LoadingSettings()
        {
            InitializeComponent();
            checkBoxLoadPreview.Checked = Program.Settings.LoadSettingsLoadPreviewImages;
            checkBoxTagsFromMetadata.Checked = Program.Settings.LoadSettingsReadMetadata;
            numericUpDown1.Value= Program.Settings.PreviewSize;
            Program.ColorManager.ChangeColorScheme(this, Program.ColorManager.SelectedScheme);
            Program.ColorManager.ChangeColorSchemeInConteiner(Controls, Program.ColorManager.SelectedScheme);

        }

        private void Form_LoadingSettings_Load(object sender, EventArgs e)
        {
            SwitchLanguage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.Settings.LoadSettingsLoadPreviewImages = checkBoxLoadPreview.Checked;
            Program.Settings.LoadSettingsReadMetadata = checkBoxTagsFromMetadata.Checked;
            Program.Settings.PreviewSize = (int)numericUpDown1.Value;
            Program.Settings.SaveSettings();

            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void SwitchLanguage()
        {
            this.Text = I18n.GetText("FormLoadingSettings");
            checkBoxLoadPreview.Text = I18n.GetText("LoadingSettingsLoadPreviewImages");
            checkBoxTagsFromMetadata.Text = I18n.GetText("LoadingSettingsReadMetadata");
            labelPreviewSize.Text = I18n.GetText("SettingPreviewImageSize");
        }
    }
}

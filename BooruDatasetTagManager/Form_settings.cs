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
    public partial class Form_settings : Form
    {
        public Form_settings()
        {
            InitializeComponent();
        }

        private void Form_settings_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Program.Settings.AvaibleLanguages;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Code";
            comboBox1.SelectedValue = Program.Settings.TranslationLanguage;

            numericUpDown1.Value = Program.Settings.PreviewSize;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.Settings.PreviewSize != (int)numericUpDown1.Value)
            {
                MessageBox.Show("You must restart the application for the settings to take effect.");
            }
            Program.Settings.PreviewSize = (int)numericUpDown1.Value;
            if (Program.Settings.TranslationLanguage != (string)comboBox1.SelectedValue)
            {
                MessageBox.Show("Since the translation is cached, you may need to delete the cache.dt2 " +
                    "file\nto see the translation of strings already translated into another language.");
            }
            Program.Settings.TranslationLanguage = (string)comboBox1.SelectedValue;
            Program.Settings.SaveSettings();
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

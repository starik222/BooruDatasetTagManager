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
        private FontSettings gridFontSettings = null;
        private FontSettings autocompleteFontSettings = null;
        private void Form_settings_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Program.Settings.AvaibleLanguages;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Code";
            comboBox1.SelectedValue = Program.Settings.TranslationLanguage;
            comboBox2.Items.AddRange(Enum.GetNames(typeof(TranslationService)));
            comboBox2.SelectedItem = Program.Settings.TransService.ToString();
            checkBox1.Checked = Program.Settings.OnlyManualTransInAutocomplete;
            comboBox3.Items.AddRange(Enum.GetNames(typeof(AutocompleteMode)));
            comboBox3.SelectedItem = Program.Settings.AutocompleteMode.ToString();
            comboBox4.Items.AddRange(Enum.GetNames(typeof(AutocompleteSort)));
            comboBox4.SelectedItem = Program.Settings.AutocompleteSort.ToString();
            checkBox2.Checked = Program.Settings.FixTagsOnLoad;
            checkBox4.Checked = Program.Settings.FixTagsOnSave;
            textBox1.Text = Program.Settings.SeparatorOnLoad;
            textBox2.Text = Program.Settings.SeparatorOnSave;
            numericUpDown1.Value = Program.Settings.PreviewSize;
            numericUpDown2.Value = Program.Settings.ShowAutocompleteAfterCharCount;
            checkBox3.Checked = Program.Settings.AskSaveChanges;
            //UI
            numericUpDown3.Value = Program.Settings.GridViewRowHeight;
            label11.Text = Program.Settings.GridViewFont.ToString();
            gridFontSettings = Program.Settings.GridViewFont;
            label14.Text = Program.Settings.AutocompleteFont.ToString();
            autocompleteFontSettings = Program.Settings.AutocompleteFont;
            //--

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You must restart the application for the settings to take effect.");
            Program.Settings.PreviewSize = (int)numericUpDown1.Value;
            Program.Settings.ShowAutocompleteAfterCharCount = (int)numericUpDown2.Value;
            Program.Settings.TranslationLanguage = (string)comboBox1.SelectedValue;
            Program.Settings.TransService = (TranslationService)Enum.Parse(typeof(TranslationService), comboBox2.SelectedItem.ToString(), true);
            Program.Settings.OnlyManualTransInAutocomplete = checkBox1.Checked;
            Program.Settings.AutocompleteMode = (AutocompleteMode)Enum.Parse(typeof(AutocompleteMode), comboBox3.SelectedItem.ToString(), true);
            Program.Settings.AutocompleteSort = (AutocompleteSort)Enum.Parse(typeof(AutocompleteSort), comboBox4.SelectedItem.ToString(), true);
            Program.Settings.FixTagsOnLoad = checkBox2.Checked;
            Program.Settings.FixTagsOnSave = checkBox4.Checked;
            Program.Settings.SeparatorOnLoad = textBox1.Text;
            Program.Settings.SeparatorOnSave = textBox2.Text;
            Program.Settings.AskSaveChanges = checkBox3.Checked;
            //UI
            Program.Settings.GridViewRowHeight = (int)numericUpDown3.Value;
            Program.Settings.GridViewFont = gridFontSettings;
            Program.Settings.AutocompleteFont = autocompleteFontSettings;
            Program.Settings.SaveSettings();
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FontDialog dialog = new FontDialog();
            dialog.Font = gridFontSettings.GetFont();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                gridFontSettings = FontSettings.Create(dialog.Font);
                label11.Text = gridFontSettings.ToString();
            }
            dialog.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FontDialog dialog = new FontDialog();
            dialog.Font = autocompleteFontSettings.GetFont();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                autocompleteFontSettings = FontSettings.Create(dialog.Font);
                label14.Text = autocompleteFontSettings.ToString();
            }
            dialog.Dispose();
        }
    }
}

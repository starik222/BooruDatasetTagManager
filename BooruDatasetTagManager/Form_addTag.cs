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
    public partial class Form_addTag : Form
    {
        public Form_addTag()
        {
            InitializeComponent();
            tagTextBox = new AutoCompleteTextBox();
            tagTextBox.SetAutocompleteMode(Program.Settings.AutocompleteMode, Program.Settings.AutocompleteSort);
            tagTextBox.Values = Program.TagsList.Tags;
            tagTextBox.Location = new Point(label1.Location.X, label1.Location.Y + label1.Size.Height + 15);
            tagTextBox.Size = new Size(this.Width - tagTextBox.Location.X - 20, 23);
            Controls.Add(tagTextBox);
            tagTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            comboBox1.Items.AddRange(Extensions.GetFriendlyEnumValues<DatasetManager.AddingType>());
            comboBox1.SelectedIndex = Extensions.GetEnumIndexFromValue<DatasetManager.AddingType>("Down");

            tagTextBox.ItemSelectionComplete += TagTextBox_ItemSelectionComplete;
            Program.ColorManager.ChangeColorScheme(this, Program.ColorManager.SelectedScheme);
            Program.ColorManager.ChangeColorSchemeInConteiner(Controls, Program.ColorManager.SelectedScheme);
            SwitchLanguage();
        }

        private void TagTextBox_ItemSelectionComplete(object sender, EventArgs e)
        {
            afterFocus = true;
            button1.Focus();
        }

        private bool afterFocus = false;

        public AutoCompleteTextBox tagTextBox;

        private void button1_Click(object sender, EventArgs e)
        {
            if (afterFocus)
            {
                afterFocus = false;
            }
            else
                DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Form_addTag_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (Extensions.GetEnumItemFromFriendlyText<DatasetManager.AddingType>((string)comboBox1.SelectedItem) == DatasetManager.AddingType.Custom)
            {
                numericUpDown1.Visible = true;
            }
            else
                numericUpDown1.Visible = false;
        }

        private void Form_addTag_Shown(object sender, EventArgs e)
        {
            tagTextBox.Focus();
        }

        private void Form_addTag_MouseClick(object sender, MouseEventArgs e)
        {
            if (tagTextBox.IsListBoxVisible())
                tagTextBox.ResetListBox();
        }

        private void SwitchLanguage()
        {
            this.Text = I18n.GetText("UIAddTagForm");
            label2.Text = I18n.GetText("UIAddTagAddingPosition");
            label1.Text = I18n.GetText("UIAddTagTag");
            checkBoxSkipExist.Text = I18n.GetText("CheckBoxSkipExist");
        }
    }
}

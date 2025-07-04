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
    public partial class Form_backgroundReplace : Form
    {
        public Form_backgroundReplace()
        {
            InitializeComponent();
            Program.ColorManager.ChangeColorScheme(this, Program.ColorManager.SelectedScheme);
            Program.ColorManager.ChangeColorSchemeInConteiner(Controls, Program.ColorManager.SelectedScheme);
            SwitchLanguage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = pictureBox1.BackColor;
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;
            pictureBox1.BackColor = colorDialog.Color;
            colorDialog.Dispose();
        }

        private void SwitchLanguage()
        {
            this.Text = I18n.GetText("UIAutoTagForm");
            radioButton1.Text = I18n.GetText("UIAutoBackRepReplacementColor");
            radioButton2.Text = I18n.GetText("UIAutoBackRepRandomColor");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = pictureBox1.BackColor;
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;
            ListViewItem lvi = new ListViewItem(colorDialog.Color.Name);
            lvi.BackColor = colorDialog.Color;
            listView1.Items.Add(lvi);
            //listBox1.Items[listBox1.Items.Count - 1].
            colorDialog.Dispose();
        }

        private void Form_backgroundReplace_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
                listView1.SelectedItems[0].Remove();
        }
    }
}

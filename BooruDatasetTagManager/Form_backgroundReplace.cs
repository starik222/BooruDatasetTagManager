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
        }

        private void SwitchLanguage()
        {
            this.Text = I18n.GetText("UIAutoTagForm");
            label1.Text = I18n.GetText("UIAutoBackRepReplacementColor");
            checkBox1.Text = I18n.GetText("UIAutoBackRepRandomColor");
        }
    }
}

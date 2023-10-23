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
    public partial class Form_AutoTaggerSettings : Form
    {
        public Form_AutoTaggerSettings()
        {
            InitializeComponent();
            comboBoxSortMode.Items.AddRange(Enum.GetNames(typeof(AutoTaggerSort)));
            Program.ColorManager.ChangeColorScheme(this, Program.ColorManager.SelectedScheme);
            Program.ColorManager.ChangeColorSchemeInConteiner(Controls, Program.ColorManager.SelectedScheme);
        }

        private async void Form_AutoTaggerSettings_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (!Program.AutoTagger.IsConnected)
            {

                if (!await Program.AutoTagger.ConnectAsync())
                {
                    Label errLabel = new Label();
                    errLabel.Text = "Unable to connect to the Interrogator service! Please start the service from the interrogator_rpc folder.";
                    errLabel.Location = new Point(10, 10);
                    errLabel.Size = new Size(Width - 20, Height / 2 - 15);
                    errLabel.Font = new Font("Segoe UI", 12);
                    errLabel.ForeColor = Color.Red;
                    Controls.Add(errLabel);
                    errLabel.BringToFront();
                }
            }
            if (Program.AutoTagger.IsConnected)
            {
                comboBoxInterrogator.Items.AddRange(Program.AutoTagger.InterrogatorList.ToArray());
                button1.Enabled = true;
                trackBarThreshold.Value = (int)(Program.Settings.AutoTagger.Threshold * 100);
                labelPercent.Text = trackBarThreshold.Value.ToString() + "%";
                comboBoxSortMode.SelectedItem = Program.Settings.AutoTagger.SortMode.ToString();
                comboBoxInterrogator.SelectedItem = Program.Settings.AutoTagger.Name;
            }
        }

        private void trackBarThreshold_Scroll(object sender, EventArgs e)
        {
            labelPercent.Text = trackBarThreshold.Value.ToString() + "%";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.Settings.AutoTagger.Threshold = (float)trackBarThreshold.Value / 100f;
            Program.Settings.AutoTagger.SortMode = (AutoTaggerSort)Enum.Parse(typeof(AutoTaggerSort), (string)comboBoxSortMode.SelectedItem);
            Program.Settings.AutoTagger.Name = (string)comboBoxInterrogator.SelectedItem;
            Program.Settings.SaveSettings();
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

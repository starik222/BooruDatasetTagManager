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
            comboBoxUnionMode.Items.AddRange(Enum.GetNames(typeof(NetworkUnionMode)));
            comboBoxSetMode.Items.AddRange(Enum.GetNames(typeof(NetworkResultSetMode)));
            Program.ColorManager.ChangeColorScheme(this, Program.ColorManager.SelectedScheme);
            Program.ColorManager.ChangeColorSchemeInConteiner(Controls, Program.ColorManager.SelectedScheme);
            connectRechecker = new Timer();
            connectRechecker.Tick += ConnectRechecker_Tick;
            connectRechecker.Interval = 5000;
        }

        private async void ConnectRechecker_Tick(object sender, EventArgs e)
        {
            if (await Program.AutoTagger.ConnectAsync())
            {
                if (Controls.ContainsKey("errorLabel"))
                {
                    Controls.RemoveByKey("errorLabel");
                }
                connectRechecker.Stop();
                Form_AutoTaggerSettings_Load(sender, e);
            }
        }

        private Timer connectRechecker;

        private async void Form_AutoTaggerSettings_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (!Program.AutoTagger.IsConnected)
            {

                if (!await Program.AutoTagger.ConnectAsync())
                {
                    Label errLabel = new Label();
                    errLabel.Name = "errorLabel";
                    errLabel.Text = "Unable to connect to the Interrogator service! Please start the service from the interrogator_rpc folder.";
                    errLabel.Location = new Point(10, 10);
                    errLabel.Size = new Size(Width - 20, Height / 2 - 15);
                    errLabel.Font = new Font("Segoe UI", 12);
                    errLabel.ForeColor = Color.Red;
                    Controls.Add(errLabel);
                    errLabel.BringToFront();
                    connectRechecker.Start();
                }
            }
            if (Program.AutoTagger.IsConnected)
            {
                checkedListBoxcomboBoxInterrogators.Items.AddRange(Program.AutoTagger.InterrogatorList.ToArray());
                button1.Enabled = true;
                //For now, a single threshold is used for everyone, so the first value is taken.
                bool firstValue = true;
                foreach (var item in Program.Settings.AutoTagger.InterragatorParams)
                {
                    if (firstValue)
                    {
                        firstValue = false;
                        trackBarThreshold.Value = (int)(item.Value * 100);
                    }
                    int index = checkedListBoxcomboBoxInterrogators.Items.IndexOf(item.Key);
                    if (index != -1)
                    {
                        checkedListBoxcomboBoxInterrogators.SetItemChecked(index, true);
                    }
                }
                labelPercent.Text = trackBarThreshold.Value.ToString() + "%";
                comboBoxSortMode.SelectedItem = Program.Settings.AutoTagger.SortMode.ToString();
                comboBoxUnionMode.SelectedItem = Program.Settings.AutoTagger.UnionMode.ToString();
                comboBoxSetMode.SelectedItem = Program.Settings.AutoTagger.SetMode.ToString();
                checkBoxSerializeVRAM.Checked = Program.Settings.AutoTagger.SerializeVramUsage;
                checkBoxSkipInternet.Checked = Program.Settings.AutoTagger.SkipInternetRequests;
            }
        }

        private void trackBarThreshold_Scroll(object sender, EventArgs e)
        {
            labelPercent.Text = trackBarThreshold.Value.ToString() + "%";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float threshold = (float)trackBarThreshold.Value / 100f;
            Program.Settings.AutoTagger.InterragatorParams.Clear();
            for (int i = 0; i < checkedListBoxcomboBoxInterrogators.CheckedItems.Count; i++)
            {
                Program.Settings.AutoTagger.InterragatorParams.Add(new KeyValuePair<string, float>((string)checkedListBoxcomboBoxInterrogators.CheckedItems[i], threshold));
            }
            Program.Settings.AutoTagger.SortMode = (AutoTaggerSort)Enum.Parse(typeof(AutoTaggerSort), (string)comboBoxSortMode.SelectedItem);
            Program.Settings.AutoTagger.UnionMode = (NetworkUnionMode)Enum.Parse(typeof(NetworkUnionMode), (string)comboBoxUnionMode.SelectedItem);
            Program.Settings.AutoTagger.SetMode = (NetworkResultSetMode)Enum.Parse(typeof(NetworkResultSetMode), (string)comboBoxSetMode.SelectedItem);
            Program.Settings.AutoTagger.SerializeVramUsage = checkBoxSerializeVRAM.Checked;
            Program.Settings.AutoTagger.SkipInternetRequests = checkBoxSkipInternet.Checked;
            Program.Settings.SaveSettings();
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Form_AutoTaggerSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connectRechecker.Enabled)
                connectRechecker.Stop();
            if (Controls.ContainsKey("errorLabel"))
            {
                Controls.RemoveByKey("errorLabel");
            }
        }
    }
}

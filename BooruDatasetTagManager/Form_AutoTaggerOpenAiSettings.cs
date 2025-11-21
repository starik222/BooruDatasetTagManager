using BooruDatasetTagManager.AiApi;
using BooruDatasetTagManager.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooruDatasetTagManager
{
    public partial class Form_AutoTaggerOpenAiSettings : Form
    {
        public Form_AutoTaggerOpenAiSettings()
        {
            InitializeComponent();
            comboBoxSortMode.Items.AddRange(Extensions.GetFriendlyEnumValues<AutoTaggerSort>());
            comboBoxSetMode.Items.AddRange(Extensions.GetFriendlyEnumValues<NetworkResultSetMode>());
            comboBoxTagFilterMode.Items.AddRange(Extensions.GetFriendlyEnumValues<TagFilteringMode>());
            Program.ColorManager.ChangeColorScheme(this, Program.ColorManager.SelectedScheme);
            Program.ColorManager.ChangeColorSchemeInConteiner(Controls, Program.ColorManager.SelectedScheme);

            if (Program.OpenAiAutoTagger == null && !string.IsNullOrEmpty(Program.Settings.OpenAiAutoTagger.ConnectionAddress) && !string.IsNullOrEmpty(Program.Settings.OpenAiAutoTagger.ApiKey))
            {
                try
                {
                    Program.OpenAiAutoTagger = new AiOpenAiClient(Program.Settings.OpenAiAutoTagger.ConnectionAddress, Program.Settings.OpenAiAutoTagger.ApiKey, Program.Settings.OpenAiAutoTagger.RequestTimeout);
                }
                catch
                {
                    return;
                }
            }
            SwitchLanguage();


            connectRechecker = new Timer();
            connectRechecker.Tick += ConnectRechecker_Tick;
            connectRechecker.Interval = 5000;
        }


        private async void ConnectRechecker_Tick(object sender, EventArgs e)
        {
            var connetionResult = await Program.OpenAiAutoTagger.ConnectAsync();
            if (connetionResult.Result)
            {
                if (Controls.ContainsKey("errorLabel"))
                {
                    Controls.RemoveByKey("errorLabel");
                }
                connectRechecker.Stop();
                Form_AutoTaggerOpenAiSettings_Load(sender, e);
            }
        }

        private Timer connectRechecker;

        private async void Form_AutoTaggerOpenAiSettings_Load(object sender, EventArgs e)
        {
            if (Program.OpenAiAutoTagger == null)
            {
                MessageBox.Show(I18n.GetText("OpenAiTaggerInitError"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            buttonOk.Enabled = false;
            if (!Program.OpenAiAutoTagger.IsConnected)
            {
                var connectResult = await Program.OpenAiAutoTagger.ConnectAsync();
                if (!connectResult.Result)
                {
                    Label errLabel = new Label();
                    errLabel.Name = "errorLabel";
                    errLabel.Text = connectResult.ErrMessage;
                    errLabel.Location = this.Location;
                    errLabel.Size = this.Size;
                    errLabel.Font = new Font("Segoe UI", 12);
                    errLabel.ForeColor = Color.Red;
                    Controls.Add(errLabel);
                    errLabel.BringToFront();
                    connectRechecker.Start();
                }
            }
            if (Program.OpenAiAutoTagger.IsConnected)
            {
                listBoxModels.Items.AddRange(Program.OpenAiAutoTagger.Models.ToArray());
                buttonOk.Enabled = true;
                comboBoxSortMode.SelectedIndex = Extensions.GetEnumIndexFromValue<AutoTaggerSort>(Program.Settings.OpenAiAutoTagger.SortMode.ToString());
                comboBoxSetMode.SelectedIndex = Extensions.GetEnumIndexFromValue<NetworkResultSetMode>(Program.Settings.OpenAiAutoTagger.SetMode.ToString());
                comboBoxTagFilterMode.SelectedIndex = Extensions.GetEnumIndexFromValue<TagFilteringMode>(Program.Settings.OpenAiAutoTagger.TagFilteringMode.ToString());
                textBoxTagFilter.Text = Program.Settings.OpenAiAutoTagger.TagFilter;
                if (Program.OpenAiAutoTagger.Models.Contains(Program.Settings.OpenAiAutoTagger.Model))
                    listBoxModels.SelectedItem = Program.Settings.OpenAiAutoTagger.Model;
                checkBoxSplitString.Checked = Program.Settings.OpenAiAutoTagger.SplitString;
                textBoxSplitter.Text = Program.Settings.OpenAiAutoTagger.Splitter;
                if (Program.Settings.OpenAiAutoTagger.Temperature == -1)
                    trackBarTemperature.Value = -1;
                else
                    trackBarTemperature.Value = (int)(Program.Settings.OpenAiAutoTagger.Temperature * 100f);
                if (Program.Settings.OpenAiAutoTagger.TopP == -1)
                    trackBarTopP.Value = -1;
                else
                    trackBarTopP.Value = (int)(Program.Settings.OpenAiAutoTagger.TopP * 100f);
                if (Program.Settings.OpenAiAutoTagger.RepeatPenalty == 0)
                    trackBarRepeatPenalty.Value = 0;
                else
                    trackBarRepeatPenalty.Value = (int)(Program.Settings.OpenAiAutoTagger.RepeatPenalty * 100f);

                textBoxSystemPrompt.Text = Program.Settings.OpenAiAutoTagger.SystemPrompt;
                textBoxUserPrompt.Text = Program.Settings.OpenAiAutoTagger.UserPrompt;

                trackBarTemperature_ValueChanged(this, EventArgs.Empty);
                trackBarTopP_ValueChanged(this, EventArgs.Empty);
                trackBarRepeatPenalty_ValueChanged(this, EventArgs.Empty);
            }
        }


        private void SwitchLanguage()
        {
            this.Text = I18n.GetText("UIAutoTagForm");
            labelModels.Text = I18n.GetText("UIAutoTagInterrogatorLabel");
            label5.Text = I18n.GetText("UIAutoTagResultOutputMode");
            label2.Text = I18n.GetText("UIAutoTagSortMode");
            label6.Text = I18n.GetText("UIAutoTagFilteringMode");
            label7.Text = I18n.GetText("UIAutoTagFilter");
        }

        private void textBoxTagFilter_Validating(object sender, CancelEventArgs e)
        {
            Label label = label7;
            RemoveError(label, e);
            TagFilteringMode tagFilteringMode = Extensions.GetEnumItemFromFriendlyText<TagFilteringMode>(comboBoxTagFilterMode.SelectedItem.ToString());
            if (tagFilteringMode != TagFilteringMode.None && string.IsNullOrEmpty(textBoxTagFilter.Text))
            {
                DisplayError(label, I18n.GetText("Required"), e);
            }
            if (tagFilteringMode == TagFilteringMode.Regex)
            {
                try
                {
                    Regex.IsMatch("", textBoxTagFilter.Text);
                }
                catch
                {
                    DisplayError(label, I18n.GetText("TipInvalidRegex"), e);
                }
            }
        }

        private void DisplayError(Label label, string message, CancelEventArgs e)
        {
            Label errLabel = new Label();
            errLabel.Name = label.Name + "Error";
            errLabel.Text = message;
            errLabel.Location = new Point(label.Location.X + label.Width + 16, label.Location.Y);
            errLabel.AutoSize = true;
            errLabel.Font = new Font("Segoe UI", 9);
            errLabel.ForeColor = Color.Red;
            Controls.Add(errLabel);
            errLabel.BringToFront();
            errorProvider1.SetError(label, I18n.GetText("TipInvalidValue"));
            e.Cancel = true;
        }

        private void RemoveError(Label label, CancelEventArgs e)
        {
            string key = label.Name + "Error";
            if (Controls.ContainsKey(key))
            {
                Controls.RemoveByKey(key);
            }
            errorProvider1.SetError(label, string.Empty);
            e.Cancel = false;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Program.Settings.OpenAiAutoTagger.SortMode = Extensions.GetEnumItemFromFriendlyText<AutoTaggerSort>(comboBoxSortMode.SelectedItem.ToString());
            Program.Settings.OpenAiAutoTagger.SetMode = Extensions.GetEnumItemFromFriendlyText<NetworkResultSetMode>(comboBoxSetMode.SelectedItem.ToString());
            Program.Settings.OpenAiAutoTagger.TagFilteringMode = Extensions.GetEnumItemFromFriendlyText<TagFilteringMode>(comboBoxTagFilterMode.SelectedItem.ToString());
            Program.Settings.OpenAiAutoTagger.TagFilter = textBoxTagFilter.Text;
            Program.Settings.OpenAiAutoTagger.Model = (string)listBoxModels.SelectedItem;
            if (trackBarTemperature.Value == -1)
                Program.Settings.OpenAiAutoTagger.Temperature = -1;
            else
            {
                Program.Settings.OpenAiAutoTagger.Temperature = trackBarTemperature.Value / 100f;
            }

            if (trackBarTopP.Value == -1)
                Program.Settings.OpenAiAutoTagger.TopP = -1;
            else
            {
                Program.Settings.OpenAiAutoTagger.TopP = trackBarTopP.Value / 100f;
            }

            if (trackBarRepeatPenalty.Value == 0)
                Program.Settings.OpenAiAutoTagger.RepeatPenalty = 0;
            else
            {
                Program.Settings.OpenAiAutoTagger.RepeatPenalty = trackBarRepeatPenalty.Value / 100f;
            }

            Program.Settings.OpenAiAutoTagger.SystemPrompt = textBoxSystemPrompt.Text;
            Program.Settings.OpenAiAutoTagger.UserPrompt = textBoxUserPrompt.Text;

            Program.Settings.OpenAiAutoTagger.SplitString = checkBoxSplitString.Checked;
            Program.Settings.OpenAiAutoTagger.Splitter = textBoxSplitter.Text;

            if (ValidateChildren())
            {
                Program.Settings.SaveSettings();
                DialogResult = DialogResult.OK;
            }
        }

        private void trackBarTemperature_ValueChanged(object sender, EventArgs e)
        {
            if (trackBarTemperature.Value == -1)
                labelTempValue.Text = "-1";
            else
                labelTempValue.Text = (trackBarTemperature.Value / 100f).ToString();
        }

        private void trackBarTopP_ValueChanged(object sender, EventArgs e)
        {
            if (trackBarTopP.Value == -1)
                labelTopPValue.Text = "-1";
            else
                labelTopPValue.Text = (trackBarTopP.Value / 100f).ToString();
        }

        private void trackBarRepeatPenalty_ValueChanged(object sender, EventArgs e)
        {
            if (trackBarRepeatPenalty.Value == 0)
                labelRepeatPenaltyValue.Text = "0";
            else
                labelRepeatPenaltyValue.Text = (trackBarRepeatPenalty.Value / 100f).ToString();
        }
    }
}

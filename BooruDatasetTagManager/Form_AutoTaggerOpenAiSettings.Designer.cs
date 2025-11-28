namespace BooruDatasetTagManager
{
    partial class Form_AutoTaggerOpenAiSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label5 = new System.Windows.Forms.Label();
            textBoxTagFilter = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            comboBoxTagFilterMode = new System.Windows.Forms.ComboBox();
            comboBoxSortMode = new System.Windows.Forms.ComboBox();
            label6 = new System.Windows.Forms.Label();
            comboBoxSetMode = new System.Windows.Forms.ComboBox();
            listBoxModels = new System.Windows.Forms.ListBox();
            labelModels = new System.Windows.Forms.Label();
            OpenAiRequestSettingsTabs = new Manina.Windows.Forms.TabControl();
            tabRequest = new Manina.Windows.Forms.Tab();
            textBoxUserPrompt = new System.Windows.Forms.TextBox();
            textBoxSystemPrompt = new System.Windows.Forms.TextBox();
            labelUserPrompt = new System.Windows.Forms.Label();
            labelSystemPrompt = new System.Windows.Forms.Label();
            tabSettings = new Manina.Windows.Forms.Tab();
            numericUpDownVideoFrameCount = new System.Windows.Forms.NumericUpDown();
            labelVideoFrameCount = new System.Windows.Forms.Label();
            textBoxSplitter = new System.Windows.Forms.TextBox();
            checkBoxSplitString = new System.Windows.Forms.CheckBox();
            labelVideoFrameScaleValue = new System.Windows.Forms.Label();
            labelRepeatPenaltyValue = new System.Windows.Forms.Label();
            labelTopPValue = new System.Windows.Forms.Label();
            labelTempValue = new System.Windows.Forms.Label();
            trackBarVideoFrameScale = new System.Windows.Forms.TrackBar();
            trackBarRepeatPenalty = new System.Windows.Forms.TrackBar();
            trackBarTopP = new System.Windows.Forms.TrackBar();
            trackBarTemperature = new System.Windows.Forms.TrackBar();
            labelVideoFrameScale = new System.Windows.Forms.Label();
            labelRepeatPenalty = new System.Windows.Forms.Label();
            labelTopP = new System.Windows.Forms.Label();
            labelTemperature = new System.Windows.Forms.Label();
            buttonCancel = new System.Windows.Forms.Button();
            buttonOk = new System.Windows.Forms.Button();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            errorProvider1 = new System.Windows.Forms.ErrorProvider(components);
            OpenAiRequestSettingsTabs.SuspendLayout();
            tabRequest.SuspendLayout();
            tabSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownVideoFrameCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarVideoFrameScale).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarRepeatPenalty).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarTopP).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarTemperature).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(13, 9);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(112, 15);
            label5.TabIndex = 14;
            label5.Text = "Result output mode";
            // 
            // textBoxTagFilter
            // 
            textBoxTagFilter.Location = new System.Drawing.Point(13, 159);
            textBoxTagFilter.Name = "textBoxTagFilter";
            textBoxTagFilter.Size = new System.Drawing.Size(393, 23);
            textBoxTagFilter.TabIndex = 23;
            textBoxTagFilter.Validating += textBoxTagFilter_Validating;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(13, 53);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(62, 15);
            label2.TabIndex = 15;
            label2.Text = "Sort mode";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(13, 141);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(54, 15);
            label7.TabIndex = 22;
            label7.Text = "Tag Filter";
            // 
            // comboBoxTagFilterMode
            // 
            comboBoxTagFilterMode.CausesValidation = false;
            comboBoxTagFilterMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxTagFilterMode.FormattingEnabled = true;
            comboBoxTagFilterMode.Location = new System.Drawing.Point(13, 115);
            comboBoxTagFilterMode.Name = "comboBoxTagFilterMode";
            comboBoxTagFilterMode.Size = new System.Drawing.Size(393, 23);
            comboBoxTagFilterMode.TabIndex = 21;
            // 
            // comboBoxSortMode
            // 
            comboBoxSortMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxSortMode.FormattingEnabled = true;
            comboBoxSortMode.Location = new System.Drawing.Point(13, 71);
            comboBoxSortMode.Name = "comboBoxSortMode";
            comboBoxSortMode.Size = new System.Drawing.Size(393, 23);
            comboBoxSortMode.TabIndex = 17;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(13, 97);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(84, 15);
            label6.TabIndex = 20;
            label6.Text = "Filtering mode";
            // 
            // comboBoxSetMode
            // 
            comboBoxSetMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxSetMode.FormattingEnabled = true;
            comboBoxSetMode.Location = new System.Drawing.Point(13, 27);
            comboBoxSetMode.Name = "comboBoxSetMode";
            comboBoxSetMode.Size = new System.Drawing.Size(393, 23);
            comboBoxSetMode.TabIndex = 19;
            // 
            // listBoxModels
            // 
            listBoxModels.Dock = System.Windows.Forms.DockStyle.Fill;
            listBoxModels.FormattingEnabled = true;
            listBoxModels.HorizontalScrollbar = true;
            listBoxModels.ItemHeight = 15;
            listBoxModels.Location = new System.Drawing.Point(0, 22);
            listBoxModels.Name = "listBoxModels";
            listBoxModels.Size = new System.Drawing.Size(338, 601);
            listBoxModels.TabIndex = 24;
            // 
            // labelModels
            // 
            labelModels.AutoSize = true;
            labelModels.Dock = System.Windows.Forms.DockStyle.Top;
            labelModels.Location = new System.Drawing.Point(0, 0);
            labelModels.Name = "labelModels";
            labelModels.Padding = new System.Windows.Forms.Padding(0, 4, 0, 3);
            labelModels.Size = new System.Drawing.Size(46, 22);
            labelModels.TabIndex = 25;
            labelModels.Text = "Models";
            // 
            // OpenAiRequestSettingsTabs
            // 
            OpenAiRequestSettingsTabs.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            OpenAiRequestSettingsTabs.Controls.Add(tabRequest);
            OpenAiRequestSettingsTabs.Controls.Add(tabSettings);
            OpenAiRequestSettingsTabs.Location = new System.Drawing.Point(0, 0);
            OpenAiRequestSettingsTabs.Name = "OpenAiRequestSettingsTabs";
            OpenAiRequestSettingsTabs.SelectedIndex = 0;
            OpenAiRequestSettingsTabs.Size = new System.Drawing.Size(674, 578);
            OpenAiRequestSettingsTabs.TabIndex = 6;
            OpenAiRequestSettingsTabs.Tabs.Add(tabRequest);
            OpenAiRequestSettingsTabs.Tabs.Add(tabSettings);
            // 
            // tabRequest
            // 
            tabRequest.Controls.Add(textBoxUserPrompt);
            tabRequest.Controls.Add(textBoxSystemPrompt);
            tabRequest.Controls.Add(labelUserPrompt);
            tabRequest.Controls.Add(labelSystemPrompt);
            tabRequest.Location = new System.Drawing.Point(1, 23);
            tabRequest.Name = "tabRequest";
            tabRequest.Size = new System.Drawing.Size(672, 554);
            tabRequest.Text = "Chat message";
            // 
            // textBoxUserPrompt
            // 
            textBoxUserPrompt.Location = new System.Drawing.Point(3, 302);
            textBoxUserPrompt.MaxLength = 3276700;
            textBoxUserPrompt.Multiline = true;
            textBoxUserPrompt.Name = "textBoxUserPrompt";
            textBoxUserPrompt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textBoxUserPrompt.Size = new System.Drawing.Size(658, 245);
            textBoxUserPrompt.TabIndex = 1;
            // 
            // textBoxSystemPrompt
            // 
            textBoxSystemPrompt.Location = new System.Drawing.Point(3, 28);
            textBoxSystemPrompt.MaxLength = 3276700;
            textBoxSystemPrompt.Multiline = true;
            textBoxSystemPrompt.Name = "textBoxSystemPrompt";
            textBoxSystemPrompt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textBoxSystemPrompt.Size = new System.Drawing.Size(658, 245);
            textBoxSystemPrompt.TabIndex = 1;
            // 
            // labelUserPrompt
            // 
            labelUserPrompt.AutoSize = true;
            labelUserPrompt.Location = new System.Drawing.Point(3, 284);
            labelUserPrompt.Name = "labelUserPrompt";
            labelUserPrompt.Size = new System.Drawing.Size(79, 15);
            labelUserPrompt.TabIndex = 0;
            labelUserPrompt.Text = "User message";
            // 
            // labelSystemPrompt
            // 
            labelSystemPrompt.AutoSize = true;
            labelSystemPrompt.Location = new System.Drawing.Point(3, 10);
            labelSystemPrompt.Name = "labelSystemPrompt";
            labelSystemPrompt.Size = new System.Drawing.Size(94, 15);
            labelSystemPrompt.TabIndex = 0;
            labelSystemPrompt.Text = "System message";
            // 
            // tabSettings
            // 
            tabSettings.Controls.Add(numericUpDownVideoFrameCount);
            tabSettings.Controls.Add(labelVideoFrameCount);
            tabSettings.Controls.Add(textBoxSplitter);
            tabSettings.Controls.Add(checkBoxSplitString);
            tabSettings.Controls.Add(labelVideoFrameScaleValue);
            tabSettings.Controls.Add(labelRepeatPenaltyValue);
            tabSettings.Controls.Add(labelTopPValue);
            tabSettings.Controls.Add(labelTempValue);
            tabSettings.Controls.Add(trackBarVideoFrameScale);
            tabSettings.Controls.Add(trackBarRepeatPenalty);
            tabSettings.Controls.Add(trackBarTopP);
            tabSettings.Controls.Add(trackBarTemperature);
            tabSettings.Controls.Add(labelVideoFrameScale);
            tabSettings.Controls.Add(labelRepeatPenalty);
            tabSettings.Controls.Add(labelTopP);
            tabSettings.Controls.Add(labelTemperature);
            tabSettings.Controls.Add(label5);
            tabSettings.Controls.Add(comboBoxSetMode);
            tabSettings.Controls.Add(label6);
            tabSettings.Controls.Add(textBoxTagFilter);
            tabSettings.Controls.Add(comboBoxSortMode);
            tabSettings.Controls.Add(label2);
            tabSettings.Controls.Add(comboBoxTagFilterMode);
            tabSettings.Controls.Add(label7);
            tabSettings.Location = new System.Drawing.Point(0, 0);
            tabSettings.Name = "tabSettings";
            tabSettings.Size = new System.Drawing.Size(0, 0);
            tabSettings.Text = "Settings";
            // 
            // numericUpDownVideoFrameCount
            // 
            numericUpDownVideoFrameCount.Location = new System.Drawing.Point(226, 388);
            numericUpDownVideoFrameCount.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownVideoFrameCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownVideoFrameCount.Name = "numericUpDownVideoFrameCount";
            numericUpDownVideoFrameCount.Size = new System.Drawing.Size(180, 23);
            numericUpDownVideoFrameCount.TabIndex = 30;
            numericUpDownVideoFrameCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // labelVideoFrameCount
            // 
            labelVideoFrameCount.AutoSize = true;
            labelVideoFrameCount.Location = new System.Drawing.Point(13, 390);
            labelVideoFrameCount.Name = "labelVideoFrameCount";
            labelVideoFrameCount.Size = new System.Drawing.Size(105, 15);
            labelVideoFrameCount.TabIndex = 29;
            labelVideoFrameCount.Text = "Video frame count";
            // 
            // textBoxSplitter
            // 
            textBoxSplitter.Location = new System.Drawing.Point(226, 349);
            textBoxSplitter.Name = "textBoxSplitter";
            textBoxSplitter.Size = new System.Drawing.Size(180, 23);
            textBoxSplitter.TabIndex = 28;
            // 
            // checkBoxSplitString
            // 
            checkBoxSplitString.AutoSize = true;
            checkBoxSplitString.Location = new System.Drawing.Point(13, 351);
            checkBoxSplitString.Name = "checkBoxSplitString";
            checkBoxSplitString.Size = new System.Drawing.Size(108, 19);
            checkBoxSplitString.TabIndex = 27;
            checkBoxSplitString.Text = "Split string with";
            checkBoxSplitString.UseVisualStyleBackColor = true;
            // 
            // labelVideoFrameScaleValue
            // 
            labelVideoFrameScaleValue.AutoSize = true;
            labelVideoFrameScaleValue.Location = new System.Drawing.Point(115, 421);
            labelVideoFrameScaleValue.Name = "labelVideoFrameScaleValue";
            labelVideoFrameScaleValue.Size = new System.Drawing.Size(12, 15);
            labelVideoFrameScaleValue.TabIndex = 26;
            labelVideoFrameScaleValue.Text = "-";
            // 
            // labelRepeatPenaltyValue
            // 
            labelRepeatPenaltyValue.AutoSize = true;
            labelRepeatPenaltyValue.Location = new System.Drawing.Point(99, 290);
            labelRepeatPenaltyValue.Name = "labelRepeatPenaltyValue";
            labelRepeatPenaltyValue.Size = new System.Drawing.Size(12, 15);
            labelRepeatPenaltyValue.TabIndex = 26;
            labelRepeatPenaltyValue.Text = "-";
            // 
            // labelTopPValue
            // 
            labelTopPValue.AutoSize = true;
            labelTopPValue.Location = new System.Drawing.Point(47, 237);
            labelTopPValue.Name = "labelTopPValue";
            labelTopPValue.Size = new System.Drawing.Size(12, 15);
            labelTopPValue.TabIndex = 26;
            labelTopPValue.Text = "-";
            // 
            // labelTempValue
            // 
            labelTempValue.AutoSize = true;
            labelTempValue.Location = new System.Drawing.Point(87, 185);
            labelTempValue.Name = "labelTempValue";
            labelTempValue.Size = new System.Drawing.Size(12, 15);
            labelTempValue.TabIndex = 26;
            labelTempValue.Text = "-";
            // 
            // trackBarVideoFrameScale
            // 
            trackBarVideoFrameScale.AutoSize = false;
            trackBarVideoFrameScale.Location = new System.Drawing.Point(13, 439);
            trackBarVideoFrameScale.Maximum = 99;
            trackBarVideoFrameScale.Name = "trackBarVideoFrameScale";
            trackBarVideoFrameScale.Size = new System.Drawing.Size(393, 22);
            trackBarVideoFrameScale.TabIndex = 25;
            trackBarVideoFrameScale.ValueChanged += trackBarVideoFrameScale_ValueChanged;
            // 
            // trackBarRepeatPenalty
            // 
            trackBarRepeatPenalty.AutoSize = false;
            trackBarRepeatPenalty.Location = new System.Drawing.Point(13, 308);
            trackBarRepeatPenalty.Maximum = 200;
            trackBarRepeatPenalty.Name = "trackBarRepeatPenalty";
            trackBarRepeatPenalty.Size = new System.Drawing.Size(393, 22);
            trackBarRepeatPenalty.TabIndex = 25;
            trackBarRepeatPenalty.ValueChanged += trackBarRepeatPenalty_ValueChanged;
            // 
            // trackBarTopP
            // 
            trackBarTopP.AutoSize = false;
            trackBarTopP.Location = new System.Drawing.Point(13, 255);
            trackBarTopP.Maximum = 200;
            trackBarTopP.Minimum = -1;
            trackBarTopP.Name = "trackBarTopP";
            trackBarTopP.Size = new System.Drawing.Size(393, 22);
            trackBarTopP.TabIndex = 25;
            trackBarTopP.Value = -1;
            trackBarTopP.ValueChanged += trackBarTopP_ValueChanged;
            // 
            // trackBarTemperature
            // 
            trackBarTemperature.AutoSize = false;
            trackBarTemperature.Location = new System.Drawing.Point(13, 203);
            trackBarTemperature.Maximum = 200;
            trackBarTemperature.Minimum = -1;
            trackBarTemperature.Name = "trackBarTemperature";
            trackBarTemperature.Size = new System.Drawing.Size(393, 22);
            trackBarTemperature.TabIndex = 25;
            trackBarTemperature.Value = -1;
            trackBarTemperature.ValueChanged += trackBarTemperature_ValueChanged;
            // 
            // labelVideoFrameScale
            // 
            labelVideoFrameScale.AutoSize = true;
            labelVideoFrameScale.Location = new System.Drawing.Point(13, 421);
            labelVideoFrameScale.Name = "labelVideoFrameScale";
            labelVideoFrameScale.Size = new System.Drawing.Size(103, 15);
            labelVideoFrameScale.TabIndex = 24;
            labelVideoFrameScale.Text = "Video frame scale:";
            // 
            // labelRepeatPenalty
            // 
            labelRepeatPenalty.AutoSize = true;
            labelRepeatPenalty.Location = new System.Drawing.Point(13, 290);
            labelRepeatPenalty.Name = "labelRepeatPenalty";
            labelRepeatPenalty.Size = new System.Drawing.Size(88, 15);
            labelRepeatPenalty.TabIndex = 24;
            labelRepeatPenalty.Text = "Repeat penalty:";
            // 
            // labelTopP
            // 
            labelTopP.AutoSize = true;
            labelTopP.Location = new System.Drawing.Point(13, 237);
            labelTopP.Name = "labelTopP";
            labelTopP.Size = new System.Drawing.Size(36, 15);
            labelTopP.TabIndex = 24;
            labelTopP.Text = "TopP:";
            // 
            // labelTemperature
            // 
            labelTemperature.AutoSize = true;
            labelTemperature.Location = new System.Drawing.Point(13, 185);
            labelTemperature.Name = "labelTemperature";
            labelTemperature.Size = new System.Drawing.Size(76, 15);
            labelTemperature.TabIndex = 24;
            labelTemperature.Text = "Temperature:";
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonCancel.Location = new System.Drawing.Point(587, 588);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(75, 23);
            buttonCancel.TabIndex = 3;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonOk
            // 
            buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonOk.Location = new System.Drawing.Point(506, 588);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new System.Drawing.Size(75, 23);
            buttonOk.TabIndex = 2;
            buttonOk.Text = "OK";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(listBoxModels);
            splitContainer1.Panel1.Controls.Add(labelModels);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(buttonCancel);
            splitContainer1.Panel2.Controls.Add(OpenAiRequestSettingsTabs);
            splitContainer1.Panel2.Controls.Add(buttonOk);
            splitContainer1.Size = new System.Drawing.Size(1016, 623);
            splitContainer1.SplitterDistance = 338;
            splitContainer1.TabIndex = 26;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // Form_AutoTaggerOpenAiSettings
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1016, 623);
            Controls.Add(splitContainer1);
            Name = "Form_AutoTaggerOpenAiSettings";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Form_AutoTaggerOpenAiSettings";
            Load += Form_AutoTaggerOpenAiSettings_Load;
            OpenAiRequestSettingsTabs.ResumeLayout(false);
            tabRequest.ResumeLayout(false);
            tabRequest.PerformLayout();
            tabSettings.ResumeLayout(false);
            tabSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownVideoFrameCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarVideoFrameScale).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarRepeatPenalty).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarTopP).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarTemperature).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxTagFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxTagFilterMode;
        private System.Windows.Forms.ComboBox comboBoxSortMode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxSetMode;
        private System.Windows.Forms.ListBox listBoxModels;
        private System.Windows.Forms.Label labelModels;
        private Manina.Windows.Forms.TabControl OpenAiRequestSettingsTabs;
        private Manina.Windows.Forms.Tab tabRequest;
        private Manina.Windows.Forms.Tab tabSettings;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label labelTemperature;
        private System.Windows.Forms.TrackBar trackBarTemperature;
        private System.Windows.Forms.Label labelTempValue;
        private System.Windows.Forms.Label labelTopPValue;
        private System.Windows.Forms.TrackBar trackBarTopP;
        private System.Windows.Forms.Label labelTopP;
        private System.Windows.Forms.Label labelRepeatPenaltyValue;
        private System.Windows.Forms.TrackBar trackBarRepeatPenalty;
        private System.Windows.Forms.Label labelRepeatPenalty;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textBoxUserPrompt;
        private System.Windows.Forms.TextBox textBoxSystemPrompt;
        private System.Windows.Forms.Label labelUserPrompt;
        private System.Windows.Forms.Label labelSystemPrompt;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox textBoxSplitter;
        private System.Windows.Forms.CheckBox checkBoxSplitString;
        private System.Windows.Forms.NumericUpDown numericUpDownVideoFrameCount;
        private System.Windows.Forms.Label labelVideoFrameCount;
        private System.Windows.Forms.Label labelVideoFrameScaleValue;
        private System.Windows.Forms.TrackBar trackBarVideoFrameScale;
        private System.Windows.Forms.Label labelVideoFrameScale;
    }
}
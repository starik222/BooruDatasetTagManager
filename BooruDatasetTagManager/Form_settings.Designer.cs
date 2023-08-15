
namespace BooruDatasetTagManager
{
    partial class Form_settings
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
            label1 = new System.Windows.Forms.Label();
            comboBox1 = new System.Windows.Forms.ComboBox();
            LabelPreviewImageSize = new System.Windows.Forms.Label();
            numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            BtnSave = new System.Windows.Forms.Button();
            BtnCancel = new System.Windows.Forms.Button();
            SettingFrame = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            AutoSortCheckBox = new System.Windows.Forms.CheckBox();
            CheckAskChange = new System.Windows.Forms.CheckBox();
            textBox2 = new System.Windows.Forms.TextBox();
            LabelSeparatorSave = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            LabelSeparatorLoad = new System.Windows.Forms.Label();
            CheckFixSave = new System.Windows.Forms.CheckBox();
            CheckFixLoad = new System.Windows.Forms.CheckBox();
            comboAutocompSort = new System.Windows.Forms.ComboBox();
            LabelAutocompSort = new System.Windows.Forms.Label();
            comboAutocompMode = new System.Windows.Forms.ComboBox();
            LabelAutocompMode = new System.Windows.Forms.Label();
            LabelChars = new System.Windows.Forms.Label();
            LabelAutocompAfter = new System.Windows.Forms.Label();
            numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            tabPage3 = new System.Windows.Forms.TabPage();
            LabelLanguage = new System.Windows.Forms.Label();
            LanguageComboBox = new System.Windows.Forms.ComboBox();
            BtnAutocompFontChange = new System.Windows.Forms.Button();
            numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            label14 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            BtnGridviewFontChange = new System.Windows.Forms.Button();
            LabelTagHeight = new System.Windows.Forms.Label();
            LabelAutocompFont = new System.Windows.Forms.Label();
            LabelTagFont = new System.Windows.Forms.Label();
            tabPage2 = new System.Windows.Forms.TabPage();
            checkBox1 = new System.Windows.Forms.CheckBox();
            label3 = new System.Windows.Forms.Label();
            comboBox2 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SettingFrame.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(7, 15);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(116, 15);
            label1.TabIndex = 0;
            label1.Text = "Translation language";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(138, 12);
            comboBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(263, 23);
            comboBox1.TabIndex = 1;
            // 
            // LabelPreviewImageSize
            // 
            LabelPreviewImageSize.AutoSize = true;
            LabelPreviewImageSize.Location = new System.Drawing.Point(22, 12);
            LabelPreviewImageSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LabelPreviewImageSize.Name = "LabelPreviewImageSize";
            LabelPreviewImageSize.Size = new System.Drawing.Size(106, 15);
            LabelPreviewImageSize.TabIndex = 2;
            LabelPreviewImageSize.Text = "Preview image size";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new System.Drawing.Point(152, 13);
            numericUpDown1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numericUpDown1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new System.Drawing.Size(140, 23);
            numericUpDown1.TabIndex = 3;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // BtnSave
            // 
            BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            BtnSave.Location = new System.Drawing.Point(14, 360);
            BtnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new System.Drawing.Size(88, 27);
            BtnSave.TabIndex = 4;
            BtnSave.Text = "Save";
            BtnSave.UseVisualStyleBackColor = true;
            BtnSave.Click += BtnSave_Click;
            // 
            // BtnCancel
            // 
            BtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            BtnCancel.Location = new System.Drawing.Point(108, 360);
            BtnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new System.Drawing.Size(88, 27);
            BtnCancel.TabIndex = 4;
            BtnCancel.Text = "Cancel";
            BtnCancel.UseVisualStyleBackColor = true;
            BtnCancel.Click += BtnCancel_Click;
            // 
            // SettingFrame
            // 
            SettingFrame.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            SettingFrame.Controls.Add(tabPage1);
            SettingFrame.Controls.Add(tabPage3);
            SettingFrame.Controls.Add(tabPage2);
            SettingFrame.Location = new System.Drawing.Point(1, 2);
            SettingFrame.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            SettingFrame.Name = "SettingFrame";
            SettingFrame.SelectedIndex = 0;
            SettingFrame.Size = new System.Drawing.Size(465, 346);
            SettingFrame.TabIndex = 5;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(AutoSortCheckBox);
            tabPage1.Controls.Add(CheckAskChange);
            tabPage1.Controls.Add(textBox2);
            tabPage1.Controls.Add(LabelSeparatorSave);
            tabPage1.Controls.Add(textBox1);
            tabPage1.Controls.Add(LabelSeparatorLoad);
            tabPage1.Controls.Add(CheckFixSave);
            tabPage1.Controls.Add(CheckFixLoad);
            tabPage1.Controls.Add(comboAutocompSort);
            tabPage1.Controls.Add(LabelAutocompSort);
            tabPage1.Controls.Add(comboAutocompMode);
            tabPage1.Controls.Add(LabelAutocompMode);
            tabPage1.Controls.Add(LabelChars);
            tabPage1.Controls.Add(LabelAutocompAfter);
            tabPage1.Controls.Add(LabelPreviewImageSize);
            tabPage1.Controls.Add(numericUpDown2);
            tabPage1.Controls.Add(numericUpDown1);
            tabPage1.Location = new System.Drawing.Point(4, 24);
            tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage1.Size = new System.Drawing.Size(457, 318);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "General";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // AutoSortCheckBox
            // 
            AutoSortCheckBox.AutoSize = true;
            AutoSortCheckBox.Location = new System.Drawing.Point(7, 288);
            AutoSortCheckBox.Name = "AutoSortCheckBox";
            AutoSortCheckBox.Size = new System.Drawing.Size(215, 19);
            AutoSortCheckBox.TabIndex = 10;
            AutoSortCheckBox.Text = "Auto sort when changing selections";
            AutoSortCheckBox.UseVisualStyleBackColor = true;
            // 
            // CheckAskChange
            // 
            CheckAskChange.AutoSize = true;
            CheckAskChange.Location = new System.Drawing.Point(7, 258);
            CheckAskChange.Name = "CheckAskChange";
            CheckAskChange.Size = new System.Drawing.Size(288, 19);
            CheckAskChange.TabIndex = 9;
            CheckAskChange.Text = "Prompt to save changes when changing selection";
            CheckAskChange.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Location = new System.Drawing.Point(152, 137);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(100, 23);
            textBox2.TabIndex = 8;
            // 
            // LabelSeparatorSave
            // 
            LabelSeparatorSave.AutoSize = true;
            LabelSeparatorSave.Location = new System.Drawing.Point(22, 140);
            LabelSeparatorSave.Name = "LabelSeparatorSave";
            LabelSeparatorSave.Size = new System.Drawing.Size(100, 15);
            LabelSeparatorSave.TabIndex = 7;
            LabelSeparatorSave.Text = "Separator on save";
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(152, 105);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(100, 23);
            textBox1.TabIndex = 8;
            // 
            // LabelSeparatorLoad
            // 
            LabelSeparatorLoad.AutoSize = true;
            LabelSeparatorLoad.Location = new System.Drawing.Point(22, 108);
            LabelSeparatorLoad.Name = "LabelSeparatorLoad";
            LabelSeparatorLoad.Size = new System.Drawing.Size(100, 15);
            LabelSeparatorLoad.TabIndex = 7;
            LabelSeparatorLoad.Text = "Separator on load";
            // 
            // CheckFixSave
            // 
            CheckFixSave.AutoSize = true;
            CheckFixSave.Location = new System.Drawing.Point(7, 228);
            CheckFixSave.Name = "CheckFixSave";
            CheckFixSave.Size = new System.Drawing.Size(216, 19);
            CheckFixSave.TabIndex = 6;
            CheckFixSave.Text = "Fix tags on saving ('('->'\\(', ')'->'\\)' )";
            CheckFixSave.UseVisualStyleBackColor = true;
            // 
            // CheckFixLoad
            // 
            CheckFixLoad.AutoSize = true;
            CheckFixLoad.Location = new System.Drawing.Point(7, 198);
            CheckFixLoad.Name = "CheckFixLoad";
            CheckFixLoad.Size = new System.Drawing.Size(217, 19);
            CheckFixLoad.TabIndex = 6;
            CheckFixLoad.Text = "Fix tags on loading (replace '_' -> ' ')";
            CheckFixLoad.UseVisualStyleBackColor = true;
            // 
            // comboAutocompSort
            // 
            comboAutocompSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboAutocompSort.FormattingEnabled = true;
            comboAutocompSort.Location = new System.Drawing.Point(152, 73);
            comboAutocompSort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboAutocompSort.Name = "comboAutocompSort";
            comboAutocompSort.Size = new System.Drawing.Size(272, 23);
            comboAutocompSort.TabIndex = 5;
            // 
            // LabelAutocompSort
            // 
            LabelAutocompSort.AutoSize = true;
            LabelAutocompSort.Location = new System.Drawing.Point(22, 76);
            LabelAutocompSort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LabelAutocompSort.Name = "LabelAutocompSort";
            LabelAutocompSort.Size = new System.Drawing.Size(106, 15);
            LabelAutocompSort.TabIndex = 4;
            LabelAutocompSort.Text = "Autocomplete sort";
            // 
            // comboAutocompMode
            // 
            comboAutocompMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboAutocompMode.FormattingEnabled = true;
            comboAutocompMode.Location = new System.Drawing.Point(152, 42);
            comboAutocompMode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboAutocompMode.Name = "comboAutocompMode";
            comboAutocompMode.Size = new System.Drawing.Size(272, 23);
            comboAutocompMode.TabIndex = 5;
            // 
            // LabelAutocompMode
            // 
            LabelAutocompMode.AutoSize = true;
            LabelAutocompMode.Location = new System.Drawing.Point(22, 45);
            LabelAutocompMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LabelAutocompMode.Name = "LabelAutocompMode";
            LabelAutocompMode.Size = new System.Drawing.Size(117, 15);
            LabelAutocompMode.TabIndex = 4;
            LabelAutocompMode.Text = "Autocomplete mode";
            // 
            // LabelChars
            // 
            LabelChars.AutoSize = true;
            LabelChars.Location = new System.Drawing.Point(240, 167);
            LabelChars.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LabelChars.Name = "LabelChars";
            LabelChars.Size = new System.Drawing.Size(35, 15);
            LabelChars.TabIndex = 2;
            LabelChars.Text = "chars";
            // 
            // LabelAutocompAfter
            // 
            LabelAutocompAfter.AutoSize = true;
            LabelAutocompAfter.Location = new System.Drawing.Point(22, 168);
            LabelAutocompAfter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LabelAutocompAfter.Name = "LabelAutocompAfter";
            LabelAutocompAfter.Size = new System.Drawing.Size(140, 15);
            LabelAutocompAfter.TabIndex = 2;
            LabelAutocompAfter.Text = "Show autocomplete after";
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new System.Drawing.Point(184, 165);
            numericUpDown2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numericUpDown2.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown2.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new System.Drawing.Size(48, 23);
            numericUpDown2.TabIndex = 3;
            numericUpDown2.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(LabelLanguage);
            tabPage3.Controls.Add(LanguageComboBox);
            tabPage3.Controls.Add(BtnAutocompFontChange);
            tabPage3.Controls.Add(numericUpDown3);
            tabPage3.Controls.Add(label14);
            tabPage3.Controls.Add(label11);
            tabPage3.Controls.Add(BtnGridviewFontChange);
            tabPage3.Controls.Add(LabelTagHeight);
            tabPage3.Controls.Add(LabelAutocompFont);
            tabPage3.Controls.Add(LabelTagFont);
            tabPage3.Location = new System.Drawing.Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new System.Drawing.Size(457, 318);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "UI";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // LabelLanguage
            // 
            LabelLanguage.AutoSize = true;
            LabelLanguage.Location = new System.Drawing.Point(11, 110);
            LabelLanguage.Name = "LabelLanguage";
            LabelLanguage.Size = new System.Drawing.Size(59, 15);
            LabelLanguage.TabIndex = 6;
            LabelLanguage.Text = "Language";
            // 
            // LanguageComboBox
            // 
            LanguageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            LanguageComboBox.FormattingEnabled = true;
            LanguageComboBox.Items.AddRange(new object[] { "zh-CN", "en-US" });
            LanguageComboBox.Location = new System.Drawing.Point(187, 110);
            LanguageComboBox.Name = "LanguageComboBox";
            LanguageComboBox.Size = new System.Drawing.Size(121, 23);
            LanguageComboBox.TabIndex = 5;
            // 
            // BtnAutocompFontChange
            // 
            BtnAutocompFontChange.Location = new System.Drawing.Point(364, 78);
            BtnAutocompFontChange.Name = "BtnAutocompFontChange";
            BtnAutocompFontChange.Size = new System.Drawing.Size(75, 23);
            BtnAutocompFontChange.TabIndex = 4;
            BtnAutocompFontChange.Text = "Change...";
            BtnAutocompFontChange.UseVisualStyleBackColor = true;
            BtnAutocompFontChange.Click += BtnAutocompFontChange_Click;
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new System.Drawing.Point(187, 44);
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new System.Drawing.Size(62, 23);
            numericUpDown3.TabIndex = 3;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(187, 78);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(12, 15);
            label14.TabIndex = 2;
            label14.Text = "-";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(187, 14);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(12, 15);
            label11.TabIndex = 2;
            label11.Text = "-";
            // 
            // BtnGridviewFontChange
            // 
            BtnGridviewFontChange.Location = new System.Drawing.Point(364, 14);
            BtnGridviewFontChange.Name = "BtnGridviewFontChange";
            BtnGridviewFontChange.Size = new System.Drawing.Size(75, 23);
            BtnGridviewFontChange.TabIndex = 1;
            BtnGridviewFontChange.Text = "Change...";
            BtnGridviewFontChange.UseVisualStyleBackColor = true;
            BtnGridviewFontChange.Click += BtnGridviewFontChange_Click;
            // 
            // LabelTagHeight
            // 
            LabelTagHeight.AutoSize = true;
            LabelTagHeight.Location = new System.Drawing.Point(11, 46);
            LabelTagHeight.Name = "LabelTagHeight";
            LabelTagHeight.Size = new System.Drawing.Size(138, 15);
            LabelTagHeight.TabIndex = 0;
            LabelTagHeight.Text = "Tags gridview row height";
            // 
            // LabelAutocompFont
            // 
            LabelAutocompFont.AutoSize = true;
            LabelAutocompFont.Location = new System.Drawing.Point(11, 78);
            LabelAutocompFont.Name = "LabelAutocompFont";
            LabelAutocompFont.Size = new System.Drawing.Size(108, 15);
            LabelAutocompFont.TabIndex = 0;
            LabelAutocompFont.Text = "Autocomplete font";
            // 
            // LabelTagFont
            // 
            LabelTagFont.AutoSize = true;
            LabelTagFont.Location = new System.Drawing.Point(11, 14);
            LabelTagFont.Name = "LabelTagFont";
            LabelTagFont.Size = new System.Drawing.Size(103, 15);
            LabelTagFont.TabIndex = 0;
            LabelTagFont.Text = "Tags gridview font";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(checkBox1);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(comboBox2);
            tabPage2.Controls.Add(comboBox1);
            tabPage2.Location = new System.Drawing.Point(4, 24);
            tabPage2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage2.Size = new System.Drawing.Size(457, 318);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Translations";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(10, 74);
            checkBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(286, 19);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "Load only manual translations into autocomplete";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(7, 46);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(103, 15);
            label3.TabIndex = 0;
            label3.Text = "Translation service";
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new System.Drawing.Point(138, 43);
            comboBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new System.Drawing.Size(263, 23);
            comboBox2.TabIndex = 1;
            // 
            // Form_settings
            // 
            AcceptButton = BtnSave;
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            CancelButton = BtnCancel;
            ClientSize = new System.Drawing.Size(468, 396);
            Controls.Add(SettingFrame);
            Controls.Add(BtnCancel);
            Controls.Add(BtnSave);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "Form_settings";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Settings";
            Load += Form_settings_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            SettingFrame.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label LabelPreviewImageSize;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.TabControl SettingFrame;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboAutocompMode;
        private System.Windows.Forms.Label LabelAutocompMode;
        private System.Windows.Forms.ComboBox comboAutocompSort;
        private System.Windows.Forms.Label LabelAutocompSort;
        private System.Windows.Forms.CheckBox CheckFixLoad;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label LabelSeparatorSave;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label LabelSeparatorLoad;
        private System.Windows.Forms.Label LabelChars;
        private System.Windows.Forms.Label LabelAutocompAfter;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.CheckBox CheckAskChange;
        private System.Windows.Forms.CheckBox CheckFixSave;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label LabelTagFont;
        private System.Windows.Forms.Button BtnGridviewFontChange;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label LabelTagHeight;
        private System.Windows.Forms.Button BtnAutocompFontChange;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label LabelAutocompFont;
        private System.Windows.Forms.CheckBox AutoSortCheckBox;
        private System.Windows.Forms.Label LabelLanguage;
        private System.Windows.Forms.ComboBox LanguageComboBox;
    }
}
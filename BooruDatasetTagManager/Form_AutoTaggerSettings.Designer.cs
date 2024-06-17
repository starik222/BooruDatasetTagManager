namespace BooruDatasetTagManager
{
    partial class Form_AutoTaggerSettings
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
            label2 = new System.Windows.Forms.Label();
            comboBoxSortMode = new System.Windows.Forms.ComboBox();
            label3 = new System.Windows.Forms.Label();
            trackBarThreshold = new System.Windows.Forms.TrackBar();
            labelPercent = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            checkedListBoxcomboBoxInterrogators = new System.Windows.Forms.CheckedListBox();
            label4 = new System.Windows.Forms.Label();
            comboBoxUnionMode = new System.Windows.Forms.ComboBox();
            checkBoxSerializeVRAM = new System.Windows.Forms.CheckBox();
            checkBoxSkipInternet = new System.Windows.Forms.CheckBox();
            label5 = new System.Windows.Forms.Label();
            comboBoxSetMode = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)trackBarThreshold).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(70, 15);
            label1.TabIndex = 0;
            label1.Text = "Interrogator";
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 435);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(62, 15);
            label2.TabIndex = 0;
            label2.Text = "Sort mode";
            // 
            // comboBoxSortMode
            // 
            comboBoxSortMode.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBoxSortMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxSortMode.FormattingEnabled = true;
            comboBoxSortMode.Location = new System.Drawing.Point(12, 453);
            comboBoxSortMode.Name = "comboBoxSortMode";
            comboBoxSortMode.Size = new System.Drawing.Size(393, 23);
            comboBoxSortMode.TabIndex = 1;
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 479);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(59, 15);
            label3.TabIndex = 2;
            label3.Text = "Threshold";
            // 
            // trackBarThreshold
            // 
            trackBarThreshold.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            trackBarThreshold.Location = new System.Drawing.Point(12, 497);
            trackBarThreshold.Maximum = 100;
            trackBarThreshold.Name = "trackBarThreshold";
            trackBarThreshold.Size = new System.Drawing.Size(393, 45);
            trackBarThreshold.TabIndex = 3;
            trackBarThreshold.Scroll += trackBarThreshold_Scroll;
            // 
            // labelPercent
            // 
            labelPercent.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            labelPercent.AutoSize = true;
            labelPercent.Location = new System.Drawing.Point(85, 479);
            labelPercent.Name = "labelPercent";
            labelPercent.Size = new System.Drawing.Size(17, 15);
            labelPercent.TabIndex = 4;
            labelPercent.Text = "%";
            // 
            // button1
            // 
            button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            button1.Location = new System.Drawing.Point(12, 548);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 23);
            button1.TabIndex = 5;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            button2.Location = new System.Drawing.Point(93, 548);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 23);
            button2.TabIndex = 6;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // checkedListBoxcomboBoxInterrogators
            // 
            checkedListBoxcomboBoxInterrogators.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            checkedListBoxcomboBoxInterrogators.CheckOnClick = true;
            checkedListBoxcomboBoxInterrogators.FormattingEnabled = true;
            checkedListBoxcomboBoxInterrogators.Location = new System.Drawing.Point(12, 27);
            checkedListBoxcomboBoxInterrogators.Name = "checkedListBoxcomboBoxInterrogators";
            checkedListBoxcomboBoxInterrogators.ScrollAlwaysVisible = true;
            checkedListBoxcomboBoxInterrogators.Size = new System.Drawing.Size(393, 274);
            checkedListBoxcomboBoxInterrogators.TabIndex = 7;
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(12, 391);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(264, 15);
            label4.TabIndex = 0;
            label4.Text = "Mode for merging results with multiple selection";
            // 
            // comboBoxUnionMode
            // 
            comboBoxUnionMode.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBoxUnionMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxUnionMode.FormattingEnabled = true;
            comboBoxUnionMode.Location = new System.Drawing.Point(12, 409);
            comboBoxUnionMode.Name = "comboBoxUnionMode";
            comboBoxUnionMode.Size = new System.Drawing.Size(393, 23);
            comboBoxUnionMode.TabIndex = 1;
            // 
            // checkBoxSerializeVRAM
            // 
            checkBoxSerializeVRAM.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            checkBoxSerializeVRAM.AutoSize = true;
            checkBoxSerializeVRAM.Location = new System.Drawing.Point(12, 322);
            checkBoxSerializeVRAM.Name = "checkBoxSerializeVRAM";
            checkBoxSerializeVRAM.Size = new System.Drawing.Size(138, 19);
            checkBoxSerializeVRAM.TabIndex = 8;
            checkBoxSerializeVRAM.Text = "Serialize VRAM usage";
            checkBoxSerializeVRAM.UseVisualStyleBackColor = true;
            // 
            // checkBoxSkipInternet
            // 
            checkBoxSkipInternet.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            checkBoxSkipInternet.AutoSize = true;
            checkBoxSkipInternet.Location = new System.Drawing.Point(240, 322);
            checkBoxSkipInternet.Name = "checkBoxSkipInternet";
            checkBoxSkipInternet.Size = new System.Drawing.Size(139, 19);
            checkBoxSkipInternet.TabIndex = 8;
            checkBoxSkipInternet.Text = "Skip internet requests";
            checkBoxSkipInternet.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(12, 345);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(112, 15);
            label5.TabIndex = 0;
            label5.Text = "Result output mode";
            // 
            // comboBoxSetMode
            // 
            comboBoxSetMode.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBoxSetMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxSetMode.FormattingEnabled = true;
            comboBoxSetMode.Location = new System.Drawing.Point(12, 363);
            comboBoxSetMode.Name = "comboBoxSetMode";
            comboBoxSetMode.Size = new System.Drawing.Size(393, 23);
            comboBoxSetMode.TabIndex = 1;
            // 
            // Form_AutoTaggerSettings
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new System.Drawing.Size(417, 585);
            Controls.Add(checkBoxSkipInternet);
            Controls.Add(checkBoxSerializeVRAM);
            Controls.Add(checkedListBoxcomboBoxInterrogators);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(labelPercent);
            Controls.Add(trackBarThreshold);
            Controls.Add(label3);
            Controls.Add(comboBoxSetMode);
            Controls.Add(comboBoxUnionMode);
            Controls.Add(comboBoxSortMode);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form_AutoTaggerSettings";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Auto tagger settings";
            FormClosing += Form_AutoTaggerSettings_FormClosing;
            Load += Form_AutoTaggerSettings_Load;
            ((System.ComponentModel.ISupportInitialize)trackBarThreshold).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxSortMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBarThreshold;
        private System.Windows.Forms.Label labelPercent;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckedListBox checkedListBoxcomboBoxInterrogators;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxUnionMode;
        private System.Windows.Forms.CheckBox checkBoxSerializeVRAM;
        private System.Windows.Forms.CheckBox checkBoxSkipInternet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxSetMode;
    }
}
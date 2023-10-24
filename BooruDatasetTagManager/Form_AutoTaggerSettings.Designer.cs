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
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 285);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(62, 15);
            label2.TabIndex = 0;
            label2.Text = "Sort mode";
            // 
            // comboBoxSortMode
            // 
            comboBoxSortMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxSortMode.FormattingEnabled = true;
            comboBoxSortMode.Location = new System.Drawing.Point(12, 303);
            comboBoxSortMode.Name = "comboBoxSortMode";
            comboBoxSortMode.Size = new System.Drawing.Size(393, 23);
            comboBoxSortMode.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 329);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(59, 15);
            label3.TabIndex = 2;
            label3.Text = "Threshold";
            // 
            // trackBarThreshold
            // 
            trackBarThreshold.Location = new System.Drawing.Point(12, 347);
            trackBarThreshold.Maximum = 100;
            trackBarThreshold.Name = "trackBarThreshold";
            trackBarThreshold.Size = new System.Drawing.Size(393, 45);
            trackBarThreshold.TabIndex = 3;
            trackBarThreshold.Scroll += trackBarThreshold_Scroll;
            // 
            // labelPercent
            // 
            labelPercent.AutoSize = true;
            labelPercent.Location = new System.Drawing.Point(85, 329);
            labelPercent.Name = "labelPercent";
            labelPercent.Size = new System.Drawing.Size(17, 15);
            labelPercent.TabIndex = 4;
            labelPercent.Text = "%";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(12, 398);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 23);
            button1.TabIndex = 5;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(93, 398);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 23);
            button2.TabIndex = 6;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // checkedListBoxcomboBoxInterrogators
            // 
            checkedListBoxcomboBoxInterrogators.CheckOnClick = true;
            checkedListBoxcomboBoxInterrogators.FormattingEnabled = true;
            checkedListBoxcomboBoxInterrogators.Location = new System.Drawing.Point(12, 27);
            checkedListBoxcomboBoxInterrogators.Name = "checkedListBoxcomboBoxInterrogators";
            checkedListBoxcomboBoxInterrogators.ScrollAlwaysVisible = true;
            checkedListBoxcomboBoxInterrogators.Size = new System.Drawing.Size(393, 256);
            checkedListBoxcomboBoxInterrogators.TabIndex = 7;
            // 
            // Form_AutoTaggerSettings
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new System.Drawing.Size(417, 439);
            Controls.Add(checkedListBoxcomboBoxInterrogators);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(labelPercent);
            Controls.Add(trackBarThreshold);
            Controls.Add(label3);
            Controls.Add(comboBoxSortMode);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form_AutoTaggerSettings";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Auto tagger settings";
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
    }
}
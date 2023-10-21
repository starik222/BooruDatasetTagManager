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
            comboBoxInterrogator = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            comboBoxSortMode = new System.Windows.Forms.ComboBox();
            label3 = new System.Windows.Forms.Label();
            trackBarThreshold = new System.Windows.Forms.TrackBar();
            labelPercent = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
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
            // comboBoxInterrogator
            // 
            comboBoxInterrogator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxInterrogator.FormattingEnabled = true;
            comboBoxInterrogator.Location = new System.Drawing.Point(12, 27);
            comboBoxInterrogator.Name = "comboBoxInterrogator";
            comboBoxInterrogator.Size = new System.Drawing.Size(232, 23);
            comboBoxInterrogator.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 53);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(62, 15);
            label2.TabIndex = 0;
            label2.Text = "Sort mode";
            // 
            // comboBoxSortMode
            // 
            comboBoxSortMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxSortMode.FormattingEnabled = true;
            comboBoxSortMode.Location = new System.Drawing.Point(12, 71);
            comboBoxSortMode.Name = "comboBoxSortMode";
            comboBoxSortMode.Size = new System.Drawing.Size(232, 23);
            comboBoxSortMode.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 97);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(59, 15);
            label3.TabIndex = 2;
            label3.Text = "Threshold";
            // 
            // trackBarThreshold
            // 
            trackBarThreshold.Location = new System.Drawing.Point(12, 115);
            trackBarThreshold.Maximum = 100;
            trackBarThreshold.Name = "trackBarThreshold";
            trackBarThreshold.Size = new System.Drawing.Size(232, 45);
            trackBarThreshold.TabIndex = 3;
            trackBarThreshold.Scroll += trackBarThreshold_Scroll;
            // 
            // labelPercent
            // 
            labelPercent.AutoSize = true;
            labelPercent.Location = new System.Drawing.Point(85, 97);
            labelPercent.Name = "labelPercent";
            labelPercent.Size = new System.Drawing.Size(17, 15);
            labelPercent.TabIndex = 4;
            labelPercent.Text = "%";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(12, 166);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 23);
            button1.TabIndex = 5;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(93, 166);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 23);
            button2.TabIndex = 6;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form_AutoTaggerSettings
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new System.Drawing.Size(268, 213);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(labelPercent);
            Controls.Add(trackBarThreshold);
            Controls.Add(label3);
            Controls.Add(comboBoxSortMode);
            Controls.Add(comboBoxInterrogator);
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
        private System.Windows.Forms.ComboBox comboBoxInterrogator;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxSortMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBarThreshold;
        private System.Windows.Forms.Label labelPercent;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
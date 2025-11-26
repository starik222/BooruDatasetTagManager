namespace BooruDatasetTagManager
{
    partial class Form_LoadingSettings
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
            checkBoxLoadPreview = new System.Windows.Forms.CheckBox();
            checkBoxTagsFromMetadata = new System.Windows.Forms.CheckBox();
            labelPreviewSize = new System.Windows.Forms.Label();
            numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // checkBoxLoadPreview
            // 
            checkBoxLoadPreview.AutoSize = true;
            checkBoxLoadPreview.Location = new System.Drawing.Point(12, 12);
            checkBoxLoadPreview.Name = "checkBoxLoadPreview";
            checkBoxLoadPreview.Size = new System.Drawing.Size(137, 19);
            checkBoxLoadPreview.TabIndex = 0;
            checkBoxLoadPreview.Text = "Load preview images";
            checkBoxLoadPreview.UseVisualStyleBackColor = true;
            // 
            // checkBoxTagsFromMetadata
            // 
            checkBoxTagsFromMetadata.AutoSize = true;
            checkBoxTagsFromMetadata.Location = new System.Drawing.Point(12, 74);
            checkBoxTagsFromMetadata.Name = "checkBoxTagsFromMetadata";
            checkBoxTagsFromMetadata.Size = new System.Drawing.Size(256, 19);
            checkBoxTagsFromMetadata.TabIndex = 1;
            checkBoxTagsFromMetadata.Text = "Attempt to read tags from image metadata.";
            checkBoxTagsFromMetadata.UseVisualStyleBackColor = true;
            // 
            // labelPreviewSize
            // 
            labelPreviewSize.AutoSize = true;
            labelPreviewSize.Location = new System.Drawing.Point(12, 43);
            labelPreviewSize.Name = "labelPreviewSize";
            labelPreviewSize.Size = new System.Drawing.Size(106, 15);
            labelPreviewSize.TabIndex = 2;
            labelPreviewSize.Text = "Preview image size";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new System.Drawing.Point(148, 41);
            numericUpDown1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new System.Drawing.Size(120, 23);
            numericUpDown1.TabIndex = 3;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(169, 167);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 23);
            button1.TabIndex = 4;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(250, 167);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 23);
            button2.TabIndex = 5;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form_LoadingSettings
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new System.Drawing.Size(337, 202);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(numericUpDown1);
            Controls.Add(labelPreviewSize);
            Controls.Add(checkBoxTagsFromMetadata);
            Controls.Add(checkBoxLoadPreview);
            Name = "Form_LoadingSettings";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Loading settings";
            Load += Form_LoadingSettings_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Label labelPreviewSize;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.CheckBox checkBoxLoadPreview;
        public System.Windows.Forms.CheckBox checkBoxTagsFromMetadata;
        public System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}
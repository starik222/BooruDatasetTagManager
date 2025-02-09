namespace BooruDatasetTagManager
{
    partial class Form_BGRemover
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
            groupBox1 = new System.Windows.Forms.GroupBox();
            listBoxModels = new System.Windows.Forms.ListBox();
            label1 = new System.Windows.Forms.Label();
            radioButtonOnlySelected = new System.Windows.Forms.RadioButton();
            radioButtonAllImages = new System.Windows.Forms.RadioButton();
            label4 = new System.Windows.Forms.Label();
            button4 = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            buttonCheckConnection = new System.Windows.Forms.Button();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            groupBox1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(listBoxModels);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(radioButtonOnlySelected);
            groupBox1.Controls.Add(radioButtonAllImages);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Enabled = false;
            groupBox1.Location = new System.Drawing.Point(12, 48);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(376, 273);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Removing settings";
            // 
            // listBoxModels
            // 
            listBoxModels.FormattingEnabled = true;
            listBoxModels.ItemHeight = 15;
            listBoxModels.Location = new System.Drawing.Point(6, 61);
            listBoxModels.Name = "listBoxModels";
            listBoxModels.Size = new System.Drawing.Size(364, 169);
            listBoxModels.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 43);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(157, 15);
            label1.TabIndex = 8;
            label1.Text = "Background Removal Model";
            // 
            // radioButtonOnlySelected
            // 
            radioButtonOnlySelected.AutoSize = true;
            radioButtonOnlySelected.Location = new System.Drawing.Point(220, 17);
            radioButtonOnlySelected.Name = "radioButtonOnlySelected";
            radioButtonOnlySelected.Size = new System.Drawing.Size(137, 19);
            radioButtonOnlySelected.TabIndex = 1;
            radioButtonOnlySelected.Text = "Only selected images";
            radioButtonOnlySelected.UseVisualStyleBackColor = true;
            // 
            // radioButtonAllImages
            // 
            radioButtonAllImages.AutoSize = true;
            radioButtonAllImages.Checked = true;
            radioButtonAllImages.Location = new System.Drawing.Point(103, 17);
            radioButtonAllImages.Name = "radioButtonAllImages";
            radioButtonAllImages.Size = new System.Drawing.Size(80, 19);
            radioButtonAllImages.TabIndex = 0;
            radioButtonAllImages.TabStop = true;
            radioButtonAllImages.Text = "All images";
            radioButtonAllImages.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(6, 19);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(91, 15);
            label4.TabIndex = 7;
            label4.Text = "Cropping mode";
            // 
            // button4
            // 
            button4.Location = new System.Drawing.Point(168, 236);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(91, 23);
            button4.TabIndex = 7;
            button4.Text = "Removing test";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(87, 236);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(75, 23);
            button3.TabIndex = 5;
            button3.Text = "Cancel";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(6, 236);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 23);
            button2.TabIndex = 4;
            button2.Text = "OK";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // buttonCheckConnection
            // 
            buttonCheckConnection.Location = new System.Drawing.Point(12, 6);
            buttonCheckConnection.Name = "buttonCheckConnection";
            buttonCheckConnection.Size = new System.Drawing.Size(263, 23);
            buttonCheckConnection.TabIndex = 4;
            buttonCheckConnection.Text = "Check availability of moondream2 model";
            buttonCheckConnection.UseVisualStyleBackColor = true;
            buttonCheckConnection.Click += buttonCheckConnection_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new System.Drawing.Point(0, 343);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(406, 22);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(12, 17);
            toolStripStatusLabel1.Text = "-";
            // 
            // Form_BGRemover
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(406, 365);
            Controls.Add(groupBox1);
            Controls.Add(buttonCheckConnection);
            Controls.Add(statusStrip1);
            Name = "Form_BGRemover";
            Text = "Background Removal ";
            Load += Form_BGRemover_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.RadioButton radioButtonOnlySelected;
        public System.Windows.Forms.RadioButton radioButtonAllImages;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonCheckConnection;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxModels;
    }
}
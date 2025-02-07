namespace BooruDatasetTagManager
{
    partial class Form_CropImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_CropImage));
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            buttonCheckConnection = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            radioButtonOnlySelected = new System.Windows.Forms.RadioButton();
            radioButtonAllImages = new System.Windows.Forms.RadioButton();
            label4 = new System.Windows.Forms.Label();
            button5 = new System.Windows.Forms.Button();
            button4 = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            textBoxInclude = new System.Windows.Forms.TextBox();
            button2 = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            textBoxExclude = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            statusStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new System.Drawing.Point(0, 384);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(485, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(12, 17);
            toolStripStatusLabel1.Text = "-";
            // 
            // buttonCheckConnection
            // 
            buttonCheckConnection.Location = new System.Drawing.Point(12, 12);
            buttonCheckConnection.Name = "buttonCheckConnection";
            buttonCheckConnection.Size = new System.Drawing.Size(263, 23);
            buttonCheckConnection.TabIndex = 0;
            buttonCheckConnection.Text = "Check availability of moondream2 model";
            buttonCheckConnection.UseVisualStyleBackColor = true;
            buttonCheckConnection.Click += button1_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButtonOnlySelected);
            groupBox1.Controls.Add(radioButtonAllImages);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(button5);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(textBoxInclude);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(textBoxExclude);
            groupBox1.Controls.Add(label1);
            groupBox1.Enabled = false;
            groupBox1.Location = new System.Drawing.Point(12, 54);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(460, 176);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Crop settings";
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
            // button5
            // 
            button5.Location = new System.Drawing.Point(242, 132);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(115, 23);
            button5.TabIndex = 6;
            button5.Text = "Object Search Test";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button4
            // 
            button4.Location = new System.Drawing.Point(363, 132);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(91, 23);
            button4.TabIndex = 7;
            button4.Text = "Cropping test";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(87, 132);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(75, 23);
            button3.TabIndex = 5;
            button3.Text = "Cancel";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // textBoxInclude
            // 
            textBoxInclude.Location = new System.Drawing.Point(6, 103);
            textBoxInclude.Name = "textBoxInclude";
            textBoxInclude.Size = new System.Drawing.Size(448, 23);
            textBoxInclude.TabIndex = 3;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(6, 132);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 23);
            button2.TabIndex = 4;
            button2.Text = "OK";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 85);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(87, 15);
            label2.TabIndex = 0;
            label2.Text = "Include objects";
            // 
            // textBoxExclude
            // 
            textBoxExclude.Location = new System.Drawing.Point(6, 59);
            textBoxExclude.Name = "textBoxExclude";
            textBoxExclude.Size = new System.Drawing.Size(448, 23);
            textBoxExclude.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 41);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(89, 15);
            label1.TabIndex = 0;
            label1.Text = "Exclude objects";
            // 
            // label3
            // 
            label3.Location = new System.Drawing.Point(12, 233);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(460, 151);
            label3.TabIndex = 3;
            label3.Text = resources.GetString("label3.Text");
            // 
            // Form_CropImage
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(485, 406);
            Controls.Add(label3);
            Controls.Add(groupBox1);
            Controls.Add(buttonCheckConnection);
            Controls.Add(statusStrip1);
            Name = "Form_CropImage";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Cropping images with moondream2 model";
            Load += Form_CropImage_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button buttonCheckConnection;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxInclude;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxExclude;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.RadioButton radioButtonOnlySelected;
        public System.Windows.Forms.RadioButton radioButtonAllImages;
    }
}
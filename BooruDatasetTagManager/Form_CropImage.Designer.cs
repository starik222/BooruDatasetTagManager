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
            button1 = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
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
            // button1
            // 
            button1.Location = new System.Drawing.Point(12, 12);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(263, 23);
            button1.TabIndex = 1;
            button1.Text = "Check availability of moondream2 model";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // groupBox1
            // 
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
            groupBox1.Size = new System.Drawing.Size(460, 144);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Crop settings";
            // 
            // button5
            // 
            button5.Location = new System.Drawing.Point(242, 110);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(115, 23);
            button5.TabIndex = 6;
            button5.Text = "Object Search Test";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button4
            // 
            button4.Location = new System.Drawing.Point(363, 110);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(91, 23);
            button4.TabIndex = 5;
            button4.Text = "Cropping test";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(87, 110);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(75, 23);
            button3.TabIndex = 4;
            button3.Text = "Cancel";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // textBoxInclude
            // 
            textBoxInclude.Location = new System.Drawing.Point(6, 81);
            textBoxInclude.Name = "textBoxInclude";
            textBoxInclude.Size = new System.Drawing.Size(448, 23);
            textBoxInclude.TabIndex = 1;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(6, 110);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 23);
            button2.TabIndex = 3;
            button2.Text = "OK";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 63);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(87, 15);
            label2.TabIndex = 0;
            label2.Text = "Include objects";
            // 
            // textBoxExclude
            // 
            textBoxExclude.Location = new System.Drawing.Point(6, 37);
            textBoxExclude.Name = "textBoxExclude";
            textBoxExclude.Size = new System.Drawing.Size(448, 23);
            textBoxExclude.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 19);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(89, 15);
            label1.TabIndex = 0;
            label1.Text = "Exclude objects";
            // 
            // label3
            // 
            label3.Location = new System.Drawing.Point(12, 213);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(460, 171);
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
            Controls.Add(button1);
            Controls.Add(statusStrip1);
            Name = "Form_CropImage";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Cropping images with moondream2 model";
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
        private System.Windows.Forms.Button button1;
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
    }
}
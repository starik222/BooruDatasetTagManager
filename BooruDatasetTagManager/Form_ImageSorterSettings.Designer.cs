namespace BooruDatasetTagManager
{
    partial class Form_ImageSorterSettings
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Root");
            label1 = new System.Windows.Forms.Label();
            textBoxRootPath = new System.Windows.Forms.TextBox();
            button1 = new System.Windows.Forms.Button();
            treeView1 = new System.Windows.Forms.TreeView();
            groupBox1 = new System.Windows.Forms.GroupBox();
            button2 = new System.Windows.Forms.Button();
            checkBox1 = new System.Windows.Forms.CheckBox();
            textBoxNodeName = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            button3 = new System.Windows.Forms.Button();
            button4 = new System.Windows.Forms.Button();
            button5 = new System.Windows.Forms.Button();
            label3 = new System.Windows.Forms.Label();
            textBoxIndex = new System.Windows.Forms.TextBox();
            button6 = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(95, 15);
            label1.TabIndex = 0;
            label1.Text = "Output directory";
            // 
            // textBoxRootPath
            // 
            textBoxRootPath.Location = new System.Drawing.Point(113, 6);
            textBoxRootPath.Name = "textBoxRootPath";
            textBoxRootPath.ReadOnly = true;
            textBoxRootPath.Size = new System.Drawing.Size(561, 23);
            textBoxRootPath.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(680, 6);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(26, 23);
            button1.TabIndex = 2;
            button1.Text = "...";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // treeView1
            // 
            treeView1.Location = new System.Drawing.Point(12, 56);
            treeView1.Name = "treeView1";
            treeNode1.Name = "Root";
            treeNode1.Text = "Root";
            treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { treeNode1 });
            treeView1.Size = new System.Drawing.Size(445, 382);
            treeView1.TabIndex = 3;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(textBoxNodeName);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new System.Drawing.Point(463, 56);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(227, 128);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Adding item";
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(6, 91);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 23);
            button2.TabIndex = 3;
            button2.Text = "Add";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(6, 66);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(210, 19);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "Add to all items in the current level";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBoxNodeName
            // 
            textBoxNodeName.Location = new System.Drawing.Point(6, 37);
            textBoxNodeName.Name = "textBoxNodeName";
            textBoxNodeName.Size = new System.Drawing.Size(205, 23);
            textBoxNodeName.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 19);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(39, 15);
            label2.TabIndex = 0;
            label2.Text = "Name";
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(463, 386);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(237, 23);
            button3.TabIndex = 5;
            button3.Text = "OK";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new System.Drawing.Point(463, 415);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(237, 23);
            button4.TabIndex = 6;
            button4.Text = "Cancel";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new System.Drawing.Point(469, 190);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(75, 23);
            button5.TabIndex = 7;
            button5.Text = "Delete";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(469, 285);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(97, 15);
            label3.TabIndex = 8;
            label3.Text = "Name start index";
            // 
            // textBoxIndex
            // 
            textBoxIndex.Location = new System.Drawing.Point(469, 303);
            textBoxIndex.Name = "textBoxIndex";
            textBoxIndex.Size = new System.Drawing.Size(103, 23);
            textBoxIndex.TabIndex = 9;
            textBoxIndex.Text = "0";
            // 
            // button6
            // 
            button6.Location = new System.Drawing.Point(578, 303);
            button6.Name = "button6";
            button6.Size = new System.Drawing.Size(96, 23);
            button6.TabIndex = 10;
            button6.Text = "Try get index";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // Form_ImageSorterSettings
            // 
            AcceptButton = button2;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = button4;
            ClientSize = new System.Drawing.Size(712, 450);
            Controls.Add(button6);
            Controls.Add(textBoxIndex);
            Controls.Add(label3);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(groupBox1);
            Controls.Add(treeView1);
            Controls.Add(button1);
            Controls.Add(textBoxRootPath);
            Controls.Add(label1);
            Name = "Form_ImageSorterSettings";
            Text = "Form_ImageSorterSettings";
            Load += Form_ImageSorterSettings_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRootPath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBoxNodeName;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxIndex;
        private System.Windows.Forms.Button button6;
    }
}
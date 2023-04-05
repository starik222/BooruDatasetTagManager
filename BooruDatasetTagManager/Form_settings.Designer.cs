
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
            label2 = new System.Windows.Forms.Label();
            numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            comboBox4 = new System.Windows.Forms.ComboBox();
            label5 = new System.Windows.Forms.Label();
            comboBox3 = new System.Windows.Forms.ComboBox();
            label4 = new System.Windows.Forms.Label();
            tabPage2 = new System.Windows.Forms.TabPage();
            checkBox1 = new System.Windows.Forms.CheckBox();
            label3 = new System.Windows.Forms.Label();
            comboBox2 = new System.Windows.Forms.ComboBox();
            checkBox2 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(7, 15);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(106, 15);
            label2.TabIndex = 2;
            label2.Text = "Preview image size";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new System.Drawing.Point(127, 13);
            numericUpDown1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numericUpDown1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new System.Drawing.Size(140, 23);
            numericUpDown1.TabIndex = 3;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // button1
            // 
            button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            button1.Location = new System.Drawing.Point(14, 287);
            button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(88, 27);
            button1.TabIndex = 4;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            button2.Location = new System.Drawing.Point(108, 287);
            button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(88, 27);
            button2.TabIndex = 4;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new System.Drawing.Point(1, 2);
            tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(446, 273);
            tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(checkBox2);
            tabPage1.Controls.Add(comboBox4);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(comboBox3);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(numericUpDown1);
            tabPage1.Location = new System.Drawing.Point(4, 24);
            tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage1.Size = new System.Drawing.Size(438, 245);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "General";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // comboBox4
            // 
            comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new System.Drawing.Point(127, 76);
            comboBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new System.Drawing.Size(302, 23);
            comboBox4.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(8, 80);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(106, 15);
            label5.TabIndex = 4;
            label5.Text = "Autocomplete sort";
            // 
            // comboBox3
            // 
            comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new System.Drawing.Point(127, 45);
            comboBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new System.Drawing.Size(302, 23);
            comboBox3.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(8, 48);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(117, 15);
            label4.TabIndex = 4;
            label4.Text = "Autocomplete mode";
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
            tabPage2.Size = new System.Drawing.Size(438, 245);
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
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new System.Drawing.Point(8, 112);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new System.Drawing.Size(308, 19);
            checkBox2.TabIndex = 6;
            checkBox2.Text = "Fix tags on loading (replace '_' -> ' ', '('->'\\(', ')'->'\\)' )";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // Form_settings
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new System.Drawing.Size(449, 323);
            Controls.Add(tabControl1);
            Controls.Add(button2);
            Controls.Add(button1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "Form_settings";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Settings";
            Load += Form_settings_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}
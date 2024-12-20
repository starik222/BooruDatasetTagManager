namespace BooruDatasetTagManager
{
    partial class Form_TagImagesGrid
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
            flowLayoutPanelImages = new System.Windows.Forms.FlowLayoutPanel();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            BtnTgOk = new System.Windows.Forms.ToolStripButton();
            BtnTgCancel = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            TrackBarZoom = new ToolStripCustomMenuItem();
            LabelGridZoomText = new System.Windows.Forms.ToolStripLabel();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanelImages
            // 
            flowLayoutPanelImages.AutoScroll = true;
            flowLayoutPanelImages.AutoSize = true;
            flowLayoutPanelImages.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanelImages.Location = new System.Drawing.Point(0, 33);
            flowLayoutPanelImages.Name = "flowLayoutPanelImages";
            flowLayoutPanelImages.Size = new System.Drawing.Size(800, 417);
            flowLayoutPanelImages.TabIndex = 0;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { BtnTgOk, BtnTgCancel, toolStripSeparator1, LabelGridZoomText, TrackBarZoom });
            toolStrip1.Location = new System.Drawing.Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new System.Drawing.Size(800, 33);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // BtnTgOk
            // 
            BtnTgOk.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTgOk.Image = Properties.Resources.Apply;
            BtnTgOk.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTgOk.Name = "BtnTgOk";
            BtnTgOk.Size = new System.Drawing.Size(23, 30);
            BtnTgOk.Text = "Save";
            BtnTgOk.Click += BtnTgOk_Click;
            // 
            // BtnTgCancel
            // 
            BtnTgCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            BtnTgCancel.Image = Properties.Resources.Delete;
            BtnTgCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            BtnTgCancel.Name = "BtnTgCancel";
            BtnTgCancel.Size = new System.Drawing.Size(23, 30);
            BtnTgCancel.Text = "Cancel";
            BtnTgCancel.Click += BtnTgCancel_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // TrackBarZoom
            // 
            TrackBarZoom.Name = "TrackBarZoom";
            TrackBarZoom.Size = new System.Drawing.Size(200, 30);
            TrackBarZoom.Text = "Zoom";
            // 
            // LabelGridZoomText
            // 
            LabelGridZoomText.Name = "LabelGridZoomText";
            LabelGridZoomText.Size = new System.Drawing.Size(42, 30);
            LabelGridZoomText.Text = "Zoom:";
            // 
            // Form_TagImagesGrid
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(flowLayoutPanelImages);
            Controls.Add(toolStrip1);
            Name = "Form_TagImagesGrid";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Multi-select tag editor";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Load += Form_TagImagesGrid_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton BtnTgOk;
        private System.Windows.Forms.ToolStripButton BtnTgCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private ToolStripCustomMenuItem TrackBarZoom;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanelImages;
        private System.Windows.Forms.ToolStripLabel LabelGridZoomText;
    }
}
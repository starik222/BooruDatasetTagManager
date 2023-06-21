using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooruDatasetTagManager
{
    public partial class Form_preview : Form
    {
        public Form_preview()
        {
            InitializeComponent();
            this.MouseWheel += Form_preview_MouseWheel;
            loaded = false;
        }
        private bool loaded;
        private void Form_preview_MouseWheel(object sender, MouseEventArgs e)
        {
            var scale = 1 + (e.Delta > 0 ? 0.1f : -0.1f);
            this.Scale(new SizeF(scale, scale));
            var screen = Screen.FromControl(this);
            this.Location = new Point(screen.WorkingArea.Width / 2 - this.Width / 2, screen.WorkingArea.Height / 2 - this.Height / 2);
        }

        public void Show(string img)
        {
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = Extensions.GetImageFromFile(img);

            if (!loaded)
            {
                this.AutoSize = false;
                this.ClientSize = pictureBox1.Image.Size;
                this.pictureBox1.Dock = DockStyle.Fill;
                this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                loaded = true;
            }
            this.Show();
        }

        private void Form_preview_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                if (pictureBox1.Image != null)
                    pictureBox1.Image.Dispose();
            }
            else
            {

            }
        }
    }
}

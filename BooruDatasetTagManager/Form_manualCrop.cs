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
    public partial class Form_manualCrop : Form
    {
        private string imgPath;
        private Bitmap imgData;
        public Form_manualCrop(string imagePath)
        {
            imgPath = imagePath;
            InitializeComponent();
            imgData = (Bitmap)Program.DataManager.GetImageFromFileWithCache(imgPath);
            pictureBox1.Image = imgData;
        }

        private void Form_manualCrop_Load(object sender, EventArgs e)
        {

        }

        private bool startSelection = false;
        private Point startPoint = new Point();
        private Point endPoint = new Point();
        private Rectangle cropRect = new Rectangle();
        private Rectangle realCropRect = new Rectangle();

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            startPoint = e.Location;
            startSelection = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            startSelection = false;
            pictureBox1.Refresh();
            var imgLocation = CalcImageLocation();
            var inter = Rectangle.Intersect(imgLocation, cropRect);
            if (!inter.IsEmpty)
            {
                using (var g = pictureBox1.CreateGraphics())
                {
                    using (Pen pen = new Pen(Color.Red, 2))
                    {
                        g.DrawRectangle(pen, inter);
                    }
                }
                float mod = CalcZoomMod();
                realCropRect = new Rectangle(
                    (int)((float)(inter.X - imgLocation.X) / mod),
                    (int)((float)(inter.Y - imgLocation.Y) / mod),
                    (int)((float)inter.Width / mod),
                    (int)((float)inter.Height / mod));
            }
        }

        private Rectangle CalcImageLocation()
        {
            var imgSize = pictureBox1.Image.Size;
            var zoomSize = pictureBox1.Size;
            float mod = CalcZoomMod();
            int w = (int)(mod * imgSize.Width);
            int h = (int)(mod * imgSize.Height);
            int x = 0;
            int y = 0;
            int imgWidth = w;
            int imgHeight = h;
            if (w == zoomSize.Width)
            {
                x = 0;
                y = (int)((float)zoomSize.Height / 2 - (float)h / 2);
            }
            else
            {
                y = 0;
                x = (int)((float)zoomSize.Width / 2 - (float)w / 2);
            }
            return new Rectangle(x, y, imgWidth, imgHeight);
        }

        private float CalcZoomMod()
        {
            var imgSize = pictureBox1.Image.Size;
            var zoomSize = pictureBox1.Size;
            float mod = (float)zoomSize.Height / (float)imgSize.Height;
            if ((int)(mod * imgSize.Width) > zoomSize.Width)
                mod = (float)zoomSize.Width / (float)imgSize.Width;
            return mod;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (startSelection)
            {
                endPoint = e.Location;
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (startSelection && startPoint != endPoint)
            {
                int x = 0;
                int y = 0;
                int w = 0;
                int h = 0;
                if (startPoint.X <= endPoint.X)
                {
                    x = startPoint.X;
                    w = endPoint.X - startPoint.X;
                }
                else
                {
                    x = endPoint.X;
                    w = startPoint.X - endPoint.X;
                }

                if (startPoint.Y <= endPoint.Y)
                {
                    y = startPoint.Y;
                    h = endPoint.Y - startPoint.Y;
                }
                else
                {
                    y = endPoint.Y;
                    h = startPoint.Y - endPoint.Y;
                }

                cropRect = new Rectangle(x, y, w, h);
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    e.Graphics.DrawRectangle(pen, cropRect);
                }
            }
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            startPoint = new Point();
            endPoint = new Point();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var resultImage = imgData.Clone(realCropRect, imgData.PixelFormat);
            resultImage.Save(imgPath);
            resultImage.Dispose();
            Program.DataManager.RemoveFromCache(imgPath);
            imgData.Dispose();
            DialogResult = DialogResult.OK;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}

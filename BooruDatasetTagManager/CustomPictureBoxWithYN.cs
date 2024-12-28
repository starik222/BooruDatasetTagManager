using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooruDatasetTagManager
{
    public class CustomPictureBoxWithYN : PictureBox
    {
        public bool StateChanged { get; set; } = false;
        public bool StateYes { get; set; } = true;
        private bool initialState = false;
        private DatasetManager.DataItem dataSetItem;
        private Button bYes;
        private Button bNo;
        private Color yesColor = Color.LawnGreen;
        private Color noColor = Color.Salmon;
        public CustomPictureBoxWithYN(int w, int h ,bool isYes) : base()
        {
            this.Width = w;
            this.Height = h;
            bYes = new Button();
            bYes.Text = I18n.GetText("CustomPictureBoxYText");
            bYes.Width = 40;
            bYes.Height = 40;
            bYes.Enabled = isYes;
            bYes.Click += BYes_Click;
            bYes.BackColor = yesColor;

            bNo = new Button();
            bNo.Text = I18n.GetText("CustomPictureBoxNText");
            bNo.Width = 40;
            bNo.Height = 40;
            bNo.Enabled = isYes;
            bNo.Click += BNo_Click;
            bNo.BackColor = noColor;

            Controls.Add(bYes);
            Controls.Add(bNo);
            bYes.Location = new System.Drawing.Point(this.Width - 92, this.Height - 50);
            bNo.Location = new System.Drawing.Point(this.Width - 50, this.Height - 50);
            bYes.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            bNo.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            initialState = isYes;
            SetStateYN(isYes);
            this.SizeChanged += CustomPictureBoxWithYN_SizeChanged;
        }

        public void SetDataSetItem(DatasetManager.DataItem item)
        {
            dataSetItem = item;
        }

        public DatasetManager.DataItem GetDataSetItem()
        {
            return dataSetItem;
        }


        private void CustomPictureBoxWithYN_SizeChanged(object sender, EventArgs e)
        {
            bYes.Location = new System.Drawing.Point(this.Width - 92, this.Height - 50);
            bNo.Location = new System.Drawing.Point(this.Width - 50, this.Height - 50);
        }

        public void SetSize(int size)
        {
            this.Width = size;
            this.Height = size;
        }

        private void BNo_Click(object sender, EventArgs e)
        {
            SetStateYN(false);
        }

        private void BYes_Click(object sender, EventArgs e)
        {
            SetStateYN(true);
        }

        private void SetStateYN(bool state)
        {
            bNo.Enabled = state;
            bYes.Enabled = !state;
            StateYes = state;
            if (StateYes != initialState)
                StateChanged = true;
            else
                StateChanged = false;
            if (bYes.Enabled)
            {
                bYes.BackColor = yesColor;
                bNo.BackColor = SystemColors.Control;
            }
            else
            {
                bYes.BackColor = SystemColors.Control;
                bNo.BackColor = noColor;
            }
            Refresh();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Form f = new Form();
                f.WindowState = FormWindowState.Maximized;
                f.ControlBox = false;
                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                f.Controls.Add(pictureBox);
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.Image = this.Image;
                pictureBox.Click += (o, e) =>
                {
                    f.Close();
                };
                f.ShowDialog();
            }
            base.OnMouseClick(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle r = new Rectangle(this.ClientRectangle.X+1, this.ClientRectangle.Y+1, this.ClientRectangle.Width-2, this.ClientRectangle.Height-2);
            ControlPaint.DrawBorder(e.Graphics, r, StateYes ? yesColor : Color.Red, ButtonBorderStyle.Solid);
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, StateYes ? yesColor : Color.Red, ButtonBorderStyle.Solid);
            base.OnPaint(e);
        }
    }
}

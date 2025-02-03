using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace PaintfromScratch
{
    public partial class MainWindow : Form
    {
        private ColorCircle colorCircle;
        private PictureBox colorPreview;

        public MainWindow()
        {
            InitializeComponent();

        }

        private bool isPainting = false;
        private Point lastPoint;
        private bool isPushed_Brush = false;
        private Color brushColor = Color.Black;
        private float brushThickness = 3f;
        private DashStyle brushStyle = DashStyle.Solid;
        private void BrushButton_Click(object sender, EventArgs e)
        {
            isPushed_Brush = !isPushed_Brush;
            BrushButton.BackColor = isPushed_Brush ? Color.Green : Color.White;
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            TabControl tabControl;

            var existingTabControl = splitContainer1.Panel2.Controls.OfType<TabControl>().FirstOrDefault();
            if (existingTabControl != null)
            {
                tabControl = existingTabControl;
            }
            else
            {
                tabControl = new TabControl
                {
                    Dock = DockStyle.Fill,
                };
                splitContainer1.Panel2.Controls.Add(tabControl);
            }

            TabPage newTabPage = new TabPage($"Tab {tabControl.TabPages.Count + 1}");

            PictureBox pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                SizeMode = PictureBoxSizeMode.AutoSize
            };

            Bitmap canvasBitmap = new Bitmap(tabControl.Width, tabControl.Height);
            pictureBox.Image = canvasBitmap;
            pictureBox.Tag = canvasBitmap;

            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseUp += PictureBox_MouseUp;

            newTabPage.Controls.Add(pictureBox);
            tabControl.TabPages.Add(newTabPage);
            tabControl.SelectedTab = newTabPage;
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isPushed_Brush)
            {
                isPainting = true;
                lastPoint = e.Location;
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPainting)
            {
                PictureBox pictureBox = sender as PictureBox;
                if (pictureBox == null) return;

                Bitmap canvasBitmap = pictureBox.Tag as Bitmap;
                if (canvasBitmap == null) return;

                using (Graphics g = Graphics.FromImage(canvasBitmap))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (Pen pen = new Pen(brushColor, brushThickness))
                    {
                        pen.DashStyle = brushStyle;
                        g.DrawLine(pen, lastPoint, e.Location);
                    }
                }

                lastPoint = e.Location;
                pictureBox.Invalidate();
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPainting = false;
            }
        }
        private void ColorCircle_ColorSelected(object sender, Color selectedColor)
        {
            redSwitch.Value = selectedColor.R;
            greenSwitch.Value = selectedColor.G;
            blueSwitch.Value = selectedColor.B;
            colorPreview.BackColor = selectedColor;
        }

        private void RGB_ValueChanged(object sender, EventArgs e)
        {
            Color newColor = Color.FromArgb((int)redSwitch.Value, (int)greenSwitch.Value, (int)blueSwitch.Value);
            colorPreview.BackColor = newColor;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            brushColor = colorPreview.BackColor; 
        }
        private void ThicknessNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            brushThickness = (float)thicknessNumericUpDown.Value;
        }

        private void LineStyleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (LineStyleComboBox.SelectedItem.ToString())
            {
                case "Dash":
                    brushStyle = DashStyle.Dash;
                    break;
                case "Dot":
                    brushStyle = DashStyle.Dot;
                    break;
                case "DashDot":
                    brushStyle = DashStyle.DashDot;
                    break;
                case "DashDotDot":
                    brushStyle = DashStyle.DashDotDot;
                    break;
                default:
                    brushStyle = DashStyle.Solid;
                    break;
            }
        }
    }
}

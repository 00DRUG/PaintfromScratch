using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using PaintfromScratch;
namespace PaintfromScratch
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            CustomParameters();
        }
        private TabControl tabControl;

        private ColorCircle colorCircle;
        private PictureBox colorPreview;
        private ShapeType selectedShape = ShapeType.None;
        private bool isPainting = false;
        private Point lastPoint;
        private bool isPushed_Brush = false;
        private bool isPushed_Erase = false;
        private Color brushColor = Color.Black;
        private float brushThickness = 3f;
        private DashStyle brushStyle = DashStyle.Solid;
        private Point startShapePoint;
        private List<Shape> shapes = new List<Shape>();
        private void BrushButton_Click(object sender, EventArgs e)
        {
            isPushed_Brush = !isPushed_Brush;
            BrushButton.BackColor = isPushed_Brush ? Color.LightGreen : Color.Transparent;
        }
        private void EraseButton_Click(object sender, EventArgs e)
        {
            isPushed_Erase = !isPushed_Erase;
            EraseButton.BackColor = isPushed_Erase ? Color.LightGreen : Color.Transparent;
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            // Check if the tabControl already exists, otherwise create it
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
                    DrawMode = TabDrawMode.OwnerDrawFixed, // Enable custom drawing
                };
                splitContainer1.Panel2.Controls.Add(tabControl);

                // Subscribe to the DrawItem event to draw the close button
                tabControl.DrawItem += TabControl_DrawItem;
                tabControl.MouseDown += TabControl_MouseDown; // Handle close button clicks
            }

            // Create a new TabPage
            TabPage newTabPage = new TabPage($"Tab {tabControl.TabPages.Count + 1}");

            // Create a PictureBox for the new tab
            PictureBox pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.AutoSize
            };

            Bitmap canvasBitmap = new Bitmap(tabControl.Width, tabControl.Height);
            pictureBox.Image = canvasBitmap;
            pictureBox.Tag = canvasBitmap;

            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseUp += PictureBox_MouseUp;

            // Add the PictureBox to the new TabPage
            newTabPage.Controls.Add(pictureBox);

            // Add the new TabPage to the TabControl
            tabControl.TabPages.Add(newTabPage);
            tabControl.SelectedTab = newTabPage;
        }

        // Custom draw the tab headers with close buttons
        private void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tabControl = (TabControl)sender;
            TabPage tabPage = tabControl.TabPages[e.Index];
            Rectangle tabRect = tabControl.GetTabRect(e.Index);

            // Draw the tab text
            TextRenderer.DrawText(e.Graphics, tabPage.Text, tabControl.Font, tabRect, tabControl.ForeColor, TextFormatFlags.Left);

            // Draw the close button (×) on the tab header
            int closeButtonSize = 8;
            Rectangle closeButtonRect = new Rectangle(
                tabRect.Right - closeButtonSize - 1, // Position the button near the right edge
                tabRect.Top+1, // Center vertically
                closeButtonSize,
                closeButtonSize
            );

            // Draw the close button
            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.DrawLine(pen, closeButtonRect.Left, closeButtonRect.Top, closeButtonRect.Right, closeButtonRect.Bottom);
                e.Graphics.DrawLine(pen, closeButtonRect.Left, closeButtonRect.Bottom, closeButtonRect.Right, closeButtonRect.Top);
            }
        }

        // Handle mouse clicks on the tab headers
        private void TabControl_MouseDown(object sender, MouseEventArgs e)
        {
            TabControl tabControl = (TabControl)sender;

            // Check if the click is on a tab header
            for (int i = 0; i < tabControl.TabPages.Count; i++)
            {
                Rectangle tabRect = tabControl.GetTabRect(i);

                // Calculate the close button area
                int closeButtonSize = 15;
                Rectangle closeButtonRect = new Rectangle(
                    tabRect.Right - closeButtonSize - 5,
                    tabRect.Top + (tabRect.Height - closeButtonSize) / 2,
                    closeButtonSize,
                    closeButtonSize
                );

                // Check if the click is within the close button area
                if (closeButtonRect.Contains(e.Location))
                {
                    // Ask the user if they want to save before closing
                    DialogResult result = MessageBox.Show("Do you want to save this tab before closing?", "Save Tab", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Save the image from the PictureBox
                        PictureBox pictureBox = tabControl.TabPages[i].Controls.OfType<PictureBox>().FirstOrDefault();
                        if (pictureBox != null && pictureBox.Image != null)
                        {
                            using (SaveFileDialog saveDialog = new SaveFileDialog())
                            {
                                saveDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                                saveDialog.Title = "Save Drawing";
                                saveDialog.FileName = $"{tabControl.TabPages[i].Text}.png";

                                if (saveDialog.ShowDialog() == DialogResult.OK)
                                {
                                    pictureBox.Image.Save(saveDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                                }
                            }
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                        // No save, just close the tab
                        tabControl.TabPages.RemoveAt(i);
                    }

                    // Exit the loop after handling the click
                    break;
                }
            }
        }

        private void ShapeSelectButton_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            if (clickedItem == null) return;

            // If the clicked item is already checked, uncheck it and reset shape selection
            if (clickedItem.Checked)
            {
                clickedItem.Checked = false;
                selectedShape = ShapeType.None;
                return;
            }

            // Uncheck all shape buttons before checking the new one
            rectItem.Checked = false;
            ellipseItem.Checked = false;

            // Toggle the clicked item to checked
            clickedItem.Checked = true;

            // Set selectedShape based on the checked button
            if (clickedItem == rectItem)
            {
                selectedShape = ShapeType.Rectangle;
            }
            else if (clickedItem == ellipseItem)
            {
                selectedShape = ShapeType.Ellipse;
            }
        }


        private ContextMenuStrip contextMenuStrip;

        private void TabControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TabControl tabControl = sender as TabControl;
                if (tabControl == null) return;

                // Find the clicked tab index by checking the tab rectangles
                int tabIndex = -1;
                for (int i = 0; i < tabControl.TabCount; i++)
                {
                    Rectangle tabRect = tabControl.GetTabRect(i);
                    if (tabRect.Contains(e.Location))
                    {
                        tabIndex = i;
                        break;
                    }
                }

                if (tabIndex == -1) return;
                ContextMenuStrip contextMenu = new ContextMenuStrip();
                ToolStripMenuItem renameMenuItem = new ToolStripMenuItem("Rename Tab");

                renameMenuItem.Click += (s, args) =>
                {
                    if (tabIndex >= 0 && tabIndex < tabControl.TabCount)
                    {
                        TabPage selectedTab = tabControl.TabPages[tabIndex];

                        string newTabName = Microsoft.VisualBasic.Interaction.InputBox(
                            "Enter new tab name:", "Rename Tab", selectedTab.Text);

                        if (!string.IsNullOrEmpty(newTabName))
                        {
                           selectedTab.Text = newTabName;
                        }
                    }
                };

                contextMenu.Items.Add(renameMenuItem);
                contextMenu.Show(tabControl, e.Location);
            }
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            foreach (var shape in shapes)
            {
                shape.Draw(e.Graphics);
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isPushed_Brush)
                {
                    isPainting = true;
                    lastPoint = e.Location;
                }
                if (selectedShape != ShapeType.None)
                {
                    startShapePoint = e.Location;
                }
            }
        }
        private DateTime lastDrawTime = DateTime.MinValue;
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

                    using (Pen pen = new Pen(brushColor, Math.Max(brushThickness, 2f)))
                    {
                        pen.Alignment = PenAlignment.Center;
                        if (brushStyle == DashStyle.Dot)
                        {
                            pen.DashStyle = DashStyle.Custom;
                            pen.DashPattern = new float[] { 1, 3 }; 
                        }
                        else if (brushStyle == DashStyle.Dash)
                        {
                            pen.DashStyle = DashStyle.Custom;
                            pen.DashPattern = new float[] { 6, 3 }; 
                        }
                        else if (brushStyle == DashStyle.DashDot)
                        {
                            pen.DashStyle = DashStyle.Custom;
                            pen.DashPattern = new float[] { 6, 2, 2, 2 };
                        }
                        else if (brushStyle == DashStyle.DashDotDot)
                        {
                            pen.DashStyle = DashStyle.Custom;
                            pen.DashPattern = new float[] { 6, 2, 2, 2, 2, 2 };
                        }
                        else
                        {
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            pen.DashStyle = DashStyle.Solid;
                        }

                        g.DrawLine(pen, lastPoint, e.Location);
                    }
                }
                lastPoint = e.Location;
                pictureBox.Invalidate();
            }
        }
        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (isPainting)
            {
                isPainting = false;
            }
            if (selectedShape != ShapeType.None) // Додаємо фігуру після завершення малювання
            {
                PictureBox pictureBox = sender as PictureBox;
                if (pictureBox == null) return;

                Bitmap canvasBitmap = pictureBox.Tag as Bitmap;
                if (canvasBitmap == null) return;

                using (Graphics g = Graphics.FromImage(canvasBitmap))
                {
                    Shape shape = ShapeFactory.CreateShape(selectedShape, startShapePoint, e.Location, brushColor, brushThickness);
                    shapes.Add(shape);
                    shape.Draw(g);
                }

                pictureBox.Invalidate();
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
            brushStyle = LineStyleComboBox.SelectedItem.ToString() switch
            {
                "Dash" => DashStyle.Dash,
                "Dot" => DashStyle.Dot,
                "DashDot" => DashStyle.DashDot,
                "DashDotDot" => DashStyle.DashDotDot,
                _ => DashStyle.Solid
            };
        }
        private void SaveFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                // Set filter for file types
                saveDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                saveDialog.Title = "Save Drawing";

                // Set default filename based on the active tab's name
                saveDialog.FileName = $"{tabControl.SelectedTab.Text}.png";

                // Show save dialog and proceed if user selects a file location
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    PictureBox pictureBox = GetActivePictureBox();
                    if (pictureBox != null && pictureBox.Image != null)
                    {
                        // Save image in selected format
                        pictureBox.Image.Save(saveDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
            }
        }



        private void OpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                openDialog.Title = "Open Drawing";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    // Call NewButton_Click to create a new TabPage and TabControl if needed
                    NewButton_Click(sender, e);

                    // Get the newly created TabPage and PictureBox
                    TabPage newTabPage = tabControl.SelectedTab;
                    PictureBox pictureBox = newTabPage.Controls.OfType<PictureBox>().FirstOrDefault();

                    // Load the image into the PictureBox
                    Bitmap loadedImage = new Bitmap(openDialog.FileName);
                    pictureBox.Image = loadedImage;
                    pictureBox.Tag = loadedImage; // Store reference for future editing

                    // Set the name of the new TabPage to the file name (without extension)
                    newTabPage.Text = Path.GetFileNameWithoutExtension(openDialog.FileName);
                }
            }
        }



        private PictureBox GetActivePictureBox()
        {
            // Get the active TabPage from the TabControl
            TabPage activeTab = tabControl.SelectedTab;

            // Find the PictureBox control on the active TabPage
            PictureBox pictureBox = activeTab?.Controls.OfType<PictureBox>().FirstOrDefault();

            return pictureBox;
        }



    }
}

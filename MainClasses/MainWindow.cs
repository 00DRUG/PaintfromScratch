using PaintfromScratch.DrawingClasses;
using PaintfromScratch.Extensions;
using PaintfromScratch.FiguresClasses;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace PaintfromScratch
{

    public partial class MainWindow : Form
    {

        public MainWindow()
        {
            InitializeComponent();
            InitializeHistoryPanelCurtain();
            CustomParameters();
        }
        private TabControl? tabControl;
        private ColorCircle colorCircle;
        private PictureBox colorPreview;
        private ShapeType selectedShape = ShapeType.None;
        private bool isPainting = false;
        private bool isErasing = false;
        private Point? lastPoint;
        private bool isPushed_Brush = false;
        private bool isPushed_Erase = false;
        private bool isPushed_Background = false;
        private Color brushColor = Color.Black;
        private float brushThickness = 3f;
        private Point startShapePoint;
        // private List<Shape> shapes = new List<Shape>();
        private Dictionary<TabPage, List<Shape>> tabShapes = new();

        private CustomBrush currentBrush = new CustomBrush();
        private EraserTool eraser = new EraserTool();
        private Shape? selectedShapeForManipulation = null;
        private bool skipNextPreviewShapeAdd = false;
        private bool isManipulatingShape = false;
        private Point lastMousePoint;
        private enum ManipulationMode { None, Move, Resize }

        private ResizeHandle activeResizeHandle = ResizeHandle.None;

        private ManipulationMode currentManipulationMode = ManipulationMode.None;
        private Shape? previewShape = null; //for the preview of the shape being drawn

        //Buttons related functions
        private void BrushButton_Click(object sender, EventArgs e)
        {
            isPushed_Brush = !isPushed_Brush;
            UnclickAllTools(sender);
            BrushButton.BackColor = isPushed_Brush ? Color.LightGray : Color.Transparent;
            selectedShape = ShapeType.None;
        }
        private void EraseButton_Click(object sender, EventArgs e)
        {

            isPushed_Erase = !isPushed_Erase;
            UnclickAllTools(sender);
            EraseButton.BackColor = isPushed_Erase ? Color.LightGreen : Color.Transparent;
            selectedShape = ShapeType.None;
        }
        private void BackgroundTool_Click(object sender, EventArgs e)
        {

            isPushed_Background = !isPushed_Background;
            UnclickAllTools(sender);
            BackgroundToolButton.BackColor = isPushed_Background ? Color.LightGreen : Color.Transparent;
            selectedShape = ShapeType.None;
        }
        private void ManipulateButton_Click(object sender, EventArgs e)
        {
            isManipulatingShape = !isManipulatingShape;
            UnclickAllTools(sender);
            if (isManipulatingShape)
            {
                ManipulateButton.BackColor = Color.LightGreen;
                if (selectedShapeForManipulation != null)
                {
                    ApplyButton.Visible = true;
                }
            }
            else
            {
                ManipulateButton.BackColor = Color.Transparent;
                ApplyButton.Visible = false;
            }
            selectedShape = ShapeType.None;
        }
        private void UnclickAllTools(object sender)
        {
            object[] tools = { BrushButton, EraseButton, BackgroundToolButton, ManipulateButton, rectItem, ellipseItem, CleanButton };

            foreach (object tool in tools)
            {
                if (tool == sender) continue;

                switch (tool)
                {
                    case ToolStripButton btn:
                        btn.BackColor = Color.Transparent;
                        if (btn == BrushButton) isPushed_Brush = false;
                        if (btn == EraseButton) isPushed_Erase = false;
                        if (btn == ManipulateButton) isManipulatingShape = false;
                        if (btn == BackgroundToolButton) isPushed_Background = false;
                        break;

                    case ToolStripMenuItem menuItem:
                        menuItem.Checked = false;
                        break;
                }
            }
        }
        private void NewButton_Click(object sender, EventArgs e)
        {
            using (Form inputForm = new Form())
            {
                inputForm.Text = "Enter Canvas Size";
                inputForm.Size = new Size(250, 180);
                inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputForm.StartPosition = FormStartPosition.CenterScreen;

                Label labelWidth = new Label { Text = "Width:", Left = 10, Top = 20, Width = 50 };
                TextBox inputWidth = new TextBox { Left = 70, Top = 18, Width = 100 };

                Label labelHeight = new Label { Text = "Height:", Left = 10, Top = 50, Width = 50 };
                TextBox inputHeight = new TextBox { Left = 70, Top = 48, Width = 100 };

                Button okButton = new Button { Text = "OK", Left = 70, Top = 80, Width = 100, DialogResult = DialogResult.OK };
                inputForm.Controls.Add(labelWidth);
                inputForm.Controls.Add(inputWidth);
                inputForm.Controls.Add(labelHeight);
                inputForm.Controls.Add(inputHeight);
                inputForm.Controls.Add(okButton);
                inputForm.AcceptButton = okButton;

                bool validInput = false;
                int canvasWidth = 0, canvasHeight = 0;
                while (!validInput)
                {
                    if (inputForm.ShowDialog() != DialogResult.OK)
                        return;

                    if (!int.TryParse(inputWidth.Text, out canvasWidth) || canvasWidth <= 0)
                    {
                        MessageBox.Show("Please enter a valid width.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    if (!int.TryParse(inputHeight.Text, out canvasHeight) || canvasHeight <= 0)
                    {
                        MessageBox.Show("Please enter a valid height.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    validInput = true;
                }

                TabControl_creation(sender, e);

                // Create new tab and canvas
                TabPage newTabPage = new TabPage($"Tab {tabControl.TabPages.Count + 1}");

                Bitmap canvasBitmap = new Bitmap(canvasWidth, canvasHeight);

                PictureBox_creation_and_add(newTabPage, canvasBitmap);
                AddToHistory(canvasBitmap, $"New canvas created: {canvasWidth} x {canvasHeight}");
            }
        }
        private void CleanButton_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = GetActivePictureBox();
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
                pictureBox.Tag = null;
                pictureBox.Image = null;
            }

            Bitmap newBitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                g.Clear(Color.Transparent);
            }
            GetCurrentShapes().Clear();
            pictureBox.Image = newBitmap;
            pictureBox.Tag = newBitmap;
            AddToHistory(newBitmap, "Canvas cleared");
        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Do you want to exit?",
                "Exit Application",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            if (tabControl == null || tabControl.TabPages.Count == 0)
            {
                this.Close();
                return;
            }

            for (int i = tabControl.TabPages.Count - 1; i >= 0; i--)
            {
                TabPage tabPage = tabControl.TabPages[i];
                PictureBox pictureBox = tabPage.Controls.OfType<PictureBox>().FirstOrDefault();

                if (pictureBox != null && pictureBox.Image != null)
                {
                    DialogResult saveResult = MessageBox.Show(
                        $"Do you want to save '{tabPage.Text}' before closing?",
                        "Save Tab",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (saveResult == DialogResult.Yes)
                    {
                        using (SaveFileDialog saveDialog = new SaveFileDialog())
                        {
                            saveDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                            saveDialog.Title = "Save Drawing";
                            saveDialog.FileName = $"{tabPage.Text}.png";

                            if (saveDialog.ShowDialog() == DialogResult.OK)
                            {
                                pictureBox.Image.Save(saveDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            }
                        }
                    }
                    else if (saveResult == DialogResult.Cancel)
                    {
                        // Abort closing all tabs and exit
                        return;
                    }
                }

                tabControl.TabPages.RemoveAt(i);
            }

            this.Close();
        }
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = GetActivePictureBox();
            if (pictureBox == null || pictureBox.Image == null)
                return;

            Bitmap canvasBitmap = pictureBox.Tag as Bitmap;
            if (canvasBitmap == null)
                return;

            List<Shape> shapes = GetCurrentShapes();

            // If manipulating a shape
            if (isManipulatingShape)
            {
                if (selectedShapeForManipulation != null)
                {
                    using (Graphics g = Graphics.FromImage(canvasBitmap))
                    {
                        selectedShapeForManipulation.Draw(g);
                    }
                    shapes.Remove(selectedShapeForManipulation);
                    AddToHistory(canvasBitmap, $"Shape manipulated: {selectedShapeForManipulation.Type}");
                    selectedShapeForManipulation = null;
                    isManipulatingShape = false;
                    
                }
                else
                {
                    MessageBox.Show("No shape selected for manipulation. Please click on a shape first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            // Just to apply the last created shape
            else if (selectedShape != ShapeType.None && GetCurrentShapes().Count > 0)
            {
                var lastShape = shapes[^1];

                using (Graphics g = Graphics.FromImage(canvasBitmap))
                {
                    lastShape.Draw(g);
                }
                shapes.RemoveAt(shapes.Count - 1);
                previewShape = null;
                pictureBox.Invalidate();
                AddToHistory(canvasBitmap, $"Shape Created: {selectedShape}");
            }
            currentManipulationMode = ManipulationMode.None;
            activeResizeHandle = ResizeHandle.None;
            Cursor = Cursors.Default;

            pictureBox.Invalidate();
            ManipulateButton_Click(sender, e);
        }
        private void ShapeSelectButton_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            if (clickedItem == null)
            {
                ApplyButton.Visible = false;
                return;
            }
            UnclickAllTools(sender);
            if (clickedItem.Checked)
            {
                ApplyButton.Visible = false;
                clickedItem.Checked = false;
                selectedShape = ShapeType.None;
                return;
            }

            rectItem.Checked = false;
            ellipseItem.Checked = false;

            clickedItem.Checked = true;

            if (clickedItem == rectItem)
            {
                ApplyButton.Visible = true;
                selectedShape = ShapeType.Rectangle;

            }
            else if (clickedItem == ellipseItem)
            {
                ApplyButton.Visible = true;
                selectedShape = ShapeType.Ellipse;
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
                    TabControl_creation(sender, e);
                    // Load the image
                    Bitmap loadedImage;
                    try
                    {
                        using (var temp = new Bitmap(openDialog.FileName))
                        {
                            loadedImage = new Bitmap(temp.Width, temp.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                            using (Graphics g = Graphics.FromImage(loadedImage))
                            {
                                g.DrawImage(temp, 0, 0, temp.Width, temp.Height);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to load image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Create new tab and canvas
                    TabPage newTabPage = new TabPage(Path.GetFileNameWithoutExtension(openDialog.FileName));
                    PictureBox_creation_and_add(newTabPage, loadedImage);
                    AddToHistory(loadedImage, "Image Loaded");
                }
            }
        }
        private void SaveFile_Click(object sender, EventArgs e)
        {

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                saveDialog.Title = "Save Drawing";
                if (tabControl.TabCount != 0 || tabControl is not null)
                {
                    saveDialog.FileName = $"{tabControl.SelectedTab.Text}.png";
                }
                else
                {
                    MessageBox.Show(
                   "You have 0 files active!");
                }

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    PictureBox pictureBox = GetActivePictureBox();
                    Bitmap bitmapToSave = new Bitmap(pictureBox.Image.Width, pictureBox.Image.Height);

                    using (Graphics g = Graphics.FromImage(bitmapToSave))
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;

                        g.DrawImageUnscaled((Bitmap)pictureBox.Image, 0, 0);

                        foreach (var shape in GetCurrentShapes())
                            shape.Draw(g);
                    }

                    bitmapToSave.Save(saveDialog.FileName);
                }
            }
        }

        //Helper related functions
        private Rectangle GetImageDisplayRectangle(PictureBox pb)
        {
            if (pb.Image == null)
                return Rectangle.Empty;

            int imgWidth = pb.Image.Width;
            int imgHeight = pb.Image.Height;
            int boxWidth = pb.ClientSize.Width;
            int boxHeight = pb.ClientSize.Height;

            switch (pb.SizeMode)
            {
                case PictureBoxSizeMode.Normal:
                case PictureBoxSizeMode.AutoSize:
                    return new Rectangle(0, 0, imgWidth, imgHeight);

                case PictureBoxSizeMode.StretchImage:
                    return new Rectangle(0, 0, boxWidth, boxHeight);

                case PictureBoxSizeMode.CenterImage:
                    return new Rectangle(
                        (boxWidth - imgWidth) / 2,
                        (boxHeight - imgHeight) / 2,
                        imgWidth,
                        imgHeight);

                case PictureBoxSizeMode.Zoom:
                    float imageAspect = (float)imgWidth / imgHeight;
                    float boxAspect = (float)boxWidth / boxHeight;
                    int drawWidth, drawHeight, offsetX, offsetY;

                    if (imageAspect > boxAspect)
                    {
                        drawWidth = boxWidth;
                        drawHeight = (int)(boxWidth / imageAspect);
                        offsetX = 0;
                        offsetY = (boxHeight - drawHeight) / 2;
                    }
                    else
                    {
                        drawHeight = boxHeight;
                        drawWidth = (int)(boxHeight * imageAspect);
                        offsetX = (boxWidth - drawWidth) / 2;
                        offsetY = 0;
                    }
                    return new Rectangle(offsetX, offsetY, drawWidth, drawHeight);

                default:
                    return new Rectangle(0, 0, imgWidth, imgHeight);
            }
        }
        private List<Shape> GetCurrentShapes()
        {
            if (tabControl?.SelectedTab != null && tabShapes.ContainsKey(tabControl.SelectedTab))
                return tabShapes[tabControl.SelectedTab];
            return new List<Shape>();
        }
        private ResizeHandle GetResizeHandle(Shape shape, Point point)
        {
            int handleSize = 10;
            Rectangle bounds = shape.Bounds;

            if (IsNear(point, bounds.Left, bounds.Top, handleSize)) return ResizeHandle.TopLeft;
            if (IsNear(point, bounds.Right, bounds.Top, handleSize)) return ResizeHandle.TopRight;
            if (IsNear(point, bounds.Left, bounds.Bottom, handleSize)) return ResizeHandle.BottomLeft;
            if (IsNear(point, bounds.Right, bounds.Bottom, handleSize)) return ResizeHandle.BottomRight;
            if (IsNear(point, bounds.Left, bounds.Top + bounds.Height / 2, handleSize)) return ResizeHandle.Left;
            if (IsNear(point, bounds.Right, bounds.Top + bounds.Height / 2, handleSize)) return ResizeHandle.Right;
            if (IsNear(point, bounds.Left + bounds.Width / 2, bounds.Top, handleSize)) return ResizeHandle.Top;
            if (IsNear(point, bounds.Left + bounds.Width / 2, bounds.Bottom, handleSize)) return ResizeHandle.Bottom;

            return ResizeHandle.None;
        }
        private bool IsNear(Point p, int x, int y, int threshold)
        {
            return Math.Abs(p.X - x) < threshold && Math.Abs(p.Y - y) < threshold;
        }
        private void FloodFill(Bitmap bmp, Point pt, Color targetColor, Color fillColor)
        {
            if (targetColor.ToArgb() == fillColor.ToArgb()) return;

            Stack<Point> pixels = new Stack<Point>();
            pixels.Push(pt);

            while (pixels.Count > 0)
            {
                Point temp = pixels.Pop();
                if (temp.X < 0 || temp.Y < 0 || temp.X >= bmp.Width || temp.Y >= bmp.Height)
                    continue;

                if (bmp.GetPixel(temp.X, temp.Y) == targetColor)
                {
                    bmp.SetPixel(temp.X, temp.Y, fillColor);
                    pixels.Push(new Point(temp.X - 1, temp.Y));
                    pixels.Push(new Point(temp.X + 1, temp.Y));
                    pixels.Push(new Point(temp.X, temp.Y - 1));
                    pixels.Push(new Point(temp.X, temp.Y + 1));
                }
            }
        }
        private void ColorCircle_ColorSelected(object sender, Color selectedColor)
        {
            redSwitch.Value = selectedColor.R;
            greenSwitch.Value = selectedColor.G;
            blueSwitch.Value = selectedColor.B;
            colorPreview.BackColor = selectedColor;
            currentBrush.Color = selectedColor;
        }
        private void RGB_ValueChanged(object sender, EventArgs e)
        {
            Color newColor = Color.FromArgb((int)redSwitch.Value, (int)greenSwitch.Value, (int)blueSwitch.Value);
            colorPreview.BackColor = newColor;
            currentBrush.Color = colorPreview.BackColor;
        }
        private void ThicknessNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            thicknessNumericUpDown.Value = thicknessNumericUpDown.Value;
            currentBrush.Size = (int)thicknessNumericUpDown.Value;
            this.Focus();
        }
        private void LineStyleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LineStyleComboBox.SelectedItem != null)
            {
                currentBrush.Shape = LineStyleComboBox.SelectedItem.ToString() switch
                {
                    "Circle" => CustomBrush.BrushShape.Circle,
                    "Square" => CustomBrush.BrushShape.Square,
                    "Triangle" => CustomBrush.BrushShape.Triangle,
                    "Star" => CustomBrush.BrushShape.Star,
                    _ => CustomBrush.BrushShape.Circle  // Default to Circle
                };
            }
        }

        //TabControl related functions
        private void DrawCheckerboard(Graphics g, Rectangle area, int tileSize = 10)
        {
            using (Brush lightBrush = new SolidBrush(Color.LightGray))
            using (Brush darkBrush = new SolidBrush(Color.White))
            {
                for (int y = 0; y < area.Height; y += tileSize)
                {
                    for (int x = 0; x < area.Width; x += tileSize)
                    {
                        bool isLight = ((x / tileSize) + (y / tileSize)) % 2 == 0;
                        Brush brush = isLight ? lightBrush : darkBrush;
                        g.FillRectangle(brush, x, y, tileSize, tileSize);
                    }
                }
            }
        }
        private void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tabControl = (TabControl)sender;
            TabPage tabPage = tabControl.TabPages[e.Index];
            Rectangle tabRect = tabControl.GetTabRect(e.Index);

            TextRenderer.DrawText(e.Graphics, tabPage.Text, tabControl.Font, tabRect, tabControl.ForeColor, TextFormatFlags.Left);

            Rectangle closeButtonRect = GetCloseButtonRect(tabControl, e.Index);

            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.DrawLine(pen, closeButtonRect.Left, closeButtonRect.Top, closeButtonRect.Right, closeButtonRect.Bottom);
                e.Graphics.DrawLine(pen, closeButtonRect.Left, closeButtonRect.Bottom, closeButtonRect.Right, closeButtonRect.Top);
            }
        }
        private Rectangle GetCloseButtonRect(TabControl tabControl, int tabIndex)
        {
            Rectangle tabRect = tabControl.GetTabRect(tabIndex);
            int closeButtonSize = 15;
            return new Rectangle(
                tabRect.Right - closeButtonSize - 5,
                tabRect.Top + (tabRect.Height - closeButtonSize),
                closeButtonSize,
                closeButtonSize
            );
        }
        private void TabControl_MouseDown(object sender, MouseEventArgs e)
        {
            TabControl tabControl = (TabControl)sender;

            for (int i = 0; i < tabControl.TabPages.Count; i++)
            {
                Rectangle closeButtonRect = GetCloseButtonRect(tabControl, i);



                if (closeButtonRect.Contains(e.Location))
                {
                    TabPage tabPage = tabControl.TabPages[i];
                    DialogResult result = MessageBox.Show("Do you want to save this tab before closing?", "Save Tab", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
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
                        tabControl.TabPages.RemoveAt(i);
                        tabHistoryEntries.Remove(tabPage);
                        tabShapes.Remove(tabPage);
                    }

                    break;
                }
            }
        }
        private void TabControl_creation(object sender, EventArgs e)
        {
            // Ensure TabControl exists 
            if (tabControl == null)
            {
                tabControl = new TabControl
                {
                    Dock = DockStyle.Fill,
                    DrawMode = TabDrawMode.OwnerDrawFixed,
                };
                splitContainer1.Panel2.Controls.Add(tabControl);

                tabControl.DrawItem += TabControl_DrawItem;
                tabControl.MouseDown += TabControl_MouseDown;
                tabControl.MouseUp += TabControl_MouseUp;
                tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            }

        }
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebuildHistoryPanel(-1); 
        }
        private void TabControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TabControl tabControl = sender as TabControl;
                if (tabControl == null) return;

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

        //PictureBox related functions
        private void PictureBox_creation_and_add(TabPage newTabPage, Bitmap canvasBitmap)
        {
            PictureBox pictureBox = new PictureBox
            {
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.AutoSize,

                Image = canvasBitmap,
                Tag = canvasBitmap,
                Width = canvasBitmap.Width,
                Height = canvasBitmap.Height
            };

            // Add event handlers
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseUp += PictureBox_MouseUp;
            pictureBox.Paint += PictureBox_Paint;

            newTabPage.Controls.Add(pictureBox);

            tabControl.TabPages.Add(newTabPage);
            tabControl.SelectedTab = newTabPage;

            tabShapes[newTabPage] = new List<Shape>();
            tabHistoryEntries[newTabPage] = new List<HistoryEntry>();
            // refresh history panel
            RebuildHistoryPanel(-1);
        }
        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox == null || pictureBox.Image == null) return;

            Bitmap bmp = (Bitmap)pictureBox.Image;
            if (e.Button == MouseButtons.Left)
            {
                if (isPushed_Brush)
                {
                    isPainting = true;
                    currentBrush.ResetLastPoint();
                    Bitmap canvasBitmap = pictureBox.Tag as Bitmap;
                    if (canvasBitmap != null)
                    {
                        using (Graphics g = Graphics.FromImage(canvasBitmap))
                        {
                            currentBrush.Draw(g, e.Location);
                        }
                        pictureBox.Invalidate();
                    }
                }
                else if (isPushed_Erase)
                {
                    isErasing = true;
                    if (pictureBox.Tag is Bitmap canvas)
                    {
                        eraser.Erase(canvas, e.Location);
                        pictureBox.Invalidate();
                    }
                }
                if (selectedShape != ShapeType.None && e.Button == MouseButtons.Left)
                {
                    startShapePoint = e.Location;
                    previewShape = ShapeFactory.CreateShape(selectedShape,
                                                            startShapePoint,
                                                            startShapePoint,
                                                            currentBrush.Color,
                                                            brushThickness);
                    pictureBox.Invalidate();
                }

                else if (isPushed_Background)
                {

                    Color clickedColor = bmp.GetPixel(e.X, e.Y);

                    FloodFill(bmp, e.Location, clickedColor, currentBrush.Color);
                    pictureBox.Invalidate();
                    AddToHistory(bmp, "Filled Area");
                }
                else if (isManipulatingShape)
                {
                    selectedShapeForManipulation = GetCurrentShapes().FirstOrDefault(shape => shape.Contains(e.Location));
                    if (selectedShapeForManipulation != null)
                    {
                        lastMousePoint = e.Location;
                        activeResizeHandle = GetResizeHandle(selectedShapeForManipulation, e.Location);
                        currentManipulationMode = activeResizeHandle == ResizeHandle.None ? ManipulationMode.Move : ManipulationMode.Resize;

                        Cursor = activeResizeHandle switch
                        {
                            ResizeHandle.TopLeft or ResizeHandle.BottomRight => Cursors.SizeNWSE,
                            ResizeHandle.TopRight or ResizeHandle.BottomLeft => Cursors.SizeNESW,
                            ResizeHandle.Top or ResizeHandle.Bottom => Cursors.SizeNS,
                            ResizeHandle.Left or ResizeHandle.Right => Cursors.SizeWE,
                            _ => Cursors.SizeAll
                        };
                        ApplyButton.Visible = true;
                    }
                    else
                    {
                        selectedShapeForManipulation = null;
                        currentManipulationMode = ManipulationMode.None;
                        activeResizeHandle = ResizeHandle.None;
                        Cursor = Cursors.Default;
                        ApplyButton.Visible = false;
                        pictureBox.Invalidate();
                    }
                }
            }
        }
        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox == null) return;

            Bitmap canvasBitmap = pictureBox.Tag as Bitmap;
            if (canvasBitmap == null) return;

            if (isPainting && isPushed_Brush)
            {
                currentBrush.Spacing = (int)spacingUpDown.Value;
                using (Graphics g = Graphics.FromImage(canvasBitmap))
                {
                    currentBrush.Draw(g, e.Location);
                }
                pictureBox.Invalidate();
            }
            else if (isErasing && isPushed_Erase)
            {
                eraser.Size = (int)thicknessNumericUpDown.Value;
                if (pictureBox.Tag is Bitmap canvas)
                {
                    eraser.Erase(canvas, e.Location);
                    pictureBox.Invalidate();
                }
            }
            else if (isManipulatingShape && selectedShapeForManipulation != null)
            {
                int deltaX = e.X - lastMousePoint.X;
                int deltaY = e.Y - lastMousePoint.Y;

                if (currentManipulationMode == ManipulationMode.Move)
                {
                    selectedShapeForManipulation.Move(deltaX, deltaY);
                }
                else if (currentManipulationMode == ManipulationMode.Resize)
                {
                    selectedShapeForManipulation.ResizeFromHandle(activeResizeHandle, deltaX, deltaY);
                }

                lastMousePoint = e.Location;
                pictureBox.Invalidate();
            }
            else if (selectedShape != ShapeType.None && e.Button == MouseButtons.Left)
            {
                if (previewShape != null)
                {
                    previewShape.Resize(e.Location.X - previewShape.Bounds.Right,
                                        e.Location.Y - previewShape.Bounds.Bottom);
                    pictureBox.Invalidate();
                }
            }


        }
        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (isPainting)
            {
                isPainting = false;
                currentBrush.ResetLastPoint();
                PictureBox pictureBox = sender as PictureBox;
                if (pictureBox != null && pictureBox.Tag is Bitmap canvas)
                {
                    AddToHistory(canvas, "Brush Stroke");
                }
            }
            if (isErasing)
            {
                isErasing = false;
                eraser.ResetLastPoint();
                PictureBox pictureBox = sender as PictureBox;
                if (pictureBox != null && pictureBox.Tag is Bitmap canvas)
                {
                    AddToHistory(canvas, "Erased Area");
                }
            }
            if (selectedShape != ShapeType.None && previewShape != null)
            {
                if (!skipNextPreviewShapeAdd)
                {
                    PictureBox pictureBox = sender as PictureBox;
                    if (pictureBox == null) return;
                    GetCurrentShapes().Add(previewShape);
                    previewShape = null;
                    pictureBox.Invalidate();
                    // Add history for shape creation
                    AddToHistory(pictureBox.Tag as Bitmap, $"Shape Created: {selectedShape}");
                }
                skipNextPreviewShapeAdd = false;
            }
            if (isManipulatingShape && selectedShapeForManipulation !=null )
            {

                PictureBox pictureBox = sender as PictureBox;
                if (pictureBox != null && pictureBox.Tag is Bitmap canvas)
                {
                    AddToHistory(canvas, $"Shape {currentManipulationMode}: {selectedShapeForManipulation.Type}");
                }
                //selectedShapeForManipulation = null;
                currentManipulationMode = ManipulationMode.None;
                activeResizeHandle = ResizeHandle.None;
                Cursor = Cursors.Default;
            }


        }
        /*private void RedrawPictureBox(PictureBox pictureBox, TabPage tabPage)
        {
            if (pictureBox.Tag is Bitmap canvasBitmap)
            {

                Bitmap tempBitmap = new Bitmap(canvasBitmap);

                using (Graphics g = Graphics.FromImage(tempBitmap))
                {
                    foreach (var shape in GetCurrentShapes())
                    {
                        shape.Draw(g);
                    }

                    if (previewShape != null)
                    {
                        previewShape.Draw(g);
                    }
                }

                pictureBox.Image = tempBitmap;
                pictureBox.Tag = tempBitmap;
            }
        }
        */
        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox == null || pictureBox.Image == null) return;

            Rectangle imageRect = GetImageDisplayRectangle(pictureBox);

            // Draw checkerboard in the displayed image area
            if (!imageRect.IsEmpty)
                DrawCheckerboard(e.Graphics, imageRect);

            // Draw the image in the displayed image area
            e.Graphics.DrawImage(pictureBox.Image, imageRect);

            foreach (var shape in GetCurrentShapes())
            {
                shape.Draw(e.Graphics);
            }
            if (previewShape != null)
            {
                using Pen p = new Pen(previewShape.Color, previewShape.Thickness)
                {
                    DashStyle = DashStyle.Dash
                };
                if (previewShape.Type == ShapeType.Rectangle)
                    e.Graphics.DrawRectangle(p, previewShape.Bounds);
                else
                    e.Graphics.DrawEllipse(p, previewShape.Bounds);
            }
            if (isManipulatingShape && selectedShapeForManipulation != null)
                selectedShapeForManipulation.DrawBoundingBox(e.Graphics);
        }
        private PictureBox? GetActivePictureBox()
        {
            TabPage activeTab = tabControl.SelectedTab;
            return activeTab?.Controls.OfType<PictureBox>().FirstOrDefault();
        }


        // history related functions
        private Dictionary<TabPage, List<HistoryEntry>> tabHistoryEntries = new();
        private void AddToHistory(Bitmap canvasBitmap, string description)
        {
            Bitmap snapshot = new Bitmap(canvasBitmap);
            var shapesCopy = GetCurrentShapes().Select(s => new Shape(s.Type, s.Bounds.Location, new Point(s.Bounds.Right, s.Bounds.Bottom), s.Color, s.Thickness)).ToList();
            var tab = tabControl?.SelectedTab;
            if (tab == null) return;
            if (!tabHistoryEntries.ContainsKey(tab))
                tabHistoryEntries[tab] = new List<HistoryEntry>();
            var historyEntries = tabHistoryEntries[tab];
            historyEntries.Add(new HistoryEntry { Snapshot = snapshot, Description = description, ShapesSnapshot = shapesCopy });
            AddTextBoxToHistoryView(description, historyEntries.Count - 1);
        }
        private void GoToHistoryState(int index)
        {
            var pictureBox = GetActivePictureBox();
            var tab = tabControl?.SelectedTab;
            if (tab == null || !tabHistoryEntries.ContainsKey(tab)) return;
            var historyEntries = tabHistoryEntries[tab];
            if (pictureBox != null && index >= 0 && index < historyEntries.Count)
            {
                pictureBox.Image?.Dispose();
                pictureBox.Image = new Bitmap(historyEntries[index].Snapshot);
                pictureBox.Tag = pictureBox.Image;

                tabShapes[tab] = historyEntries[index].ShapesSnapshot
                    .Select(s => new Shape(s.Type, s.Bounds.Location, new Point(s.Bounds.Right, s.Bounds.Bottom), s.Color, s.Thickness))
                    .ToList();
                // Reset manipulation state
                selectedShapeForManipulation = null;
                isManipulatingShape = false;
                currentManipulationMode = ManipulationMode.None;
                activeResizeHandle = ResizeHandle.None;
                previewShape = null;
                Cursor = Cursors.Default;
                ApplyButton.Visible = false;

                int removeCount = historyEntries.Count - (index + 1);
                if (removeCount > 0)
                {
                    historyEntries.RemoveRange(index + 1, removeCount);
                }
                RebuildHistoryPanel(index);
                pictureBox.Invalidate();
            }
        }
        private void RebuildHistoryPanel(int selectedIndex)
        {
            var tab = tabControl?.SelectedTab;
            if (tab == null || !tabHistoryEntries.ContainsKey(tab)) return;
            var historyEntries = tabHistoryEntries[tab];
            historyPanel.Controls.Clear();
            for (int i = historyEntries.Count - 1; i >= 0; i--)
            {
                Panel box = new Panel
                {
                    Height = 40,
                    Margin = new Padding(2),
                    BackColor = (i == selectedIndex) ? Color.LightBlue : Color.White,
                    Dock = DockStyle.Top,
                    Tag = i
                };
                Label label = new Label
                {
                    Text = $"{i + 1}: {historyEntries[i].Description}",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    Tag = i
                };
                box.Controls.Add(label);

                box.Click += Box_Click;
                label.Click += Box_Click;
                //Ensure connection
                box.MouseEnter += HistoryBox_MouseEnter;
                box.MouseLeave += HistoryBox_MouseLeave;
                label.MouseEnter += HistoryBox_MouseEnter;
                label.MouseLeave += HistoryBox_MouseLeave;

                historyPanel.Controls.Add(box);
            }
        }
        // Helper for click event
        private void Box_Click(object sender, EventArgs e)
        {
            int index = (sender as Control)?.Tag is int idx ? idx : 0;
            GoToHistoryState(index);
        }
        private void AddTextBoxToHistoryView(string description, int index)
        {
            Panel box = new Panel
            {
                Height = 40,
                Margin = new Padding(2),
                BackColor = Color.White,
                Dock = DockStyle.Top,
                Tag = index
            };
            Label label = new Label
            {
                Text = $"{index + 1}: {description}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Tag = index
            };
            box.Controls.Add(label);

            box.Click += Box_Click;
            label.Click += Box_Click;
            //Ensure connection
            box.MouseEnter += HistoryBox_MouseEnter;
            box.MouseLeave += HistoryBox_MouseLeave;
            label.MouseEnter += HistoryBox_MouseEnter;
            label.MouseLeave += HistoryBox_MouseLeave;
            historyPanel.Controls.Add(box);
            historyPanel.Controls.SetChildIndex(box, 0);
        }

        // Panel closing/opening related functions
        private System.Windows.Forms.Timer historyPanelTimer = new System.Windows.Forms.Timer();
        private bool historyPanelVisible = true;
        private int historyPanelTargetWidth = 200; 
        private int historyPanelMinWidth = 0;
        private System.Windows.Forms.Timer historyPreviewTimer = new System.Windows.Forms.Timer();
        private int hoveredHistoryIndex = -1;
        private Form? historyPreviewForm = null;

        private void InitializeHistoryPanelCurtain()
        {
            historyPanelTimer.Interval = 10;
            historyPanelTimer.Tick += HistoryPanelTimer_Tick;
            historyPreviewTimer.Interval = 600; 
            historyPreviewTimer.Tick += HistoryPreviewTimer_Tick;
        }

        private void historyToggleButton_Click(object sender, EventArgs e)
        {
            if (historyPanelVisible)
            {
                historyPanelTargetWidth = historyPanelMinWidth;
                historyPanelVisible = false; 
            }
            else
            {
                historyPanelTargetWidth = 200;
                historyPanelVisible = true;
                historyPanel.Visible = true;
            }
            historyPanelTimer.Start();
        }

        private void HistoryPanelTimer_Tick(object sender, EventArgs e)
        {
            if (historyPanelVisible)
            {
                if (historyPanel.Width < historyPanelTargetWidth)
                {
                    historyPanel.Width += 10;
                    if (historyPanel.Width >= historyPanelTargetWidth)
                    {
                        historyPanel.Width = historyPanelTargetWidth;
                        historyPanelTimer.Stop();
                    }
                }
                else
                {
                    historyPanelTimer.Stop();
                }
            }
            else
            {
                if (historyPanel.Width > historyPanelTargetWidth)
                {
                    historyPanel.Width -= 10;
                    if (historyPanel.Width <= historyPanelTargetWidth)
                    {
                        historyPanel.Width = historyPanelTargetWidth;
                        historyPanelTimer.Stop();
                    }
                }
                else
                {
                    historyPanelTimer.Stop();
                }
            }
        }
        private void HistoryBox_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Control ctrl && ctrl.Tag is int idx)
            {
                hoveredHistoryIndex = idx;
                historyPreviewTimer.Start();
            }
        }

        private void HistoryBox_MouseLeave(object sender, EventArgs e)
        {
            historyPreviewTimer.Stop();
            hoveredHistoryIndex = -1;
            if (historyPreviewForm != null)
            {
                historyPreviewForm.Close();
                historyPreviewForm = null;
            }
        }
        private void HistoryPreviewTimer_Tick(object? sender, EventArgs e)
        {
            historyPreviewTimer.Stop();
            var tab = tabControl?.SelectedTab;
            if (tab == null || !tabHistoryEntries.ContainsKey(tab)) return;
            var historyEntries = tabHistoryEntries[tab];
            if (hoveredHistoryIndex < 0 || hoveredHistoryIndex >= historyEntries.Count)
                return;

            if (historyPreviewForm != null)
            {
                historyPreviewForm.Close();
                historyPreviewForm = null;
            }

            var entry = historyEntries[hoveredHistoryIndex];

            Bitmap previewBitmap = new Bitmap(entry.Snapshot.Width, entry.Snapshot.Height);
            using (Graphics g = Graphics.FromImage(previewBitmap))
            {
                g.DrawImage(entry.Snapshot, 0, 0);
                foreach (var shape in entry.ShapesSnapshot)
                {
                    shape.Draw(g);
                }
            }
            Bitmap scaledPreview = new Bitmap(previewBitmap, 160, 120);

            historyPreviewForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                Size = new Size(160, 120),
                TopMost = true,
                ShowInTaskbar = false,
                BackColor = Color.White
            };

            var pb = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = scaledPreview,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            historyPreviewForm.Controls.Add(pb);

            var mousePos = Cursor.Position;
            historyPreviewForm.Location = new Point(mousePos.X + 10, mousePos.Y + 10);

            historyPreviewForm.Show();
        }
    }
}

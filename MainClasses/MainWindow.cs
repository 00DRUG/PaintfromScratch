using PaintfromScratch.DrawingClasses;
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
        private bool isManipulatingShape = false;
        private Point lastMousePoint;
        private enum ManipulationMode { None, Move, Resize }

        private ResizeHandle activeResizeHandle = ResizeHandle.None;

        private ManipulationMode currentManipulationMode = ManipulationMode.None;
        private Shape? previewShape = null; //for the preview of the shape being drawn


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
            BackgroundTool.BackColor = isPushed_Background ? Color.LightGreen : Color.Transparent;
            selectedShape = ShapeType.None;
        }
        private void ManipulateButton_Click(object sender, EventArgs e)
        {
            isManipulatingShape = !isManipulatingShape;
            UnclickAllTools(sender);
            ManipulateButton.BackColor = isManipulatingShape ? Color.LightGreen : Color.Transparent;
            selectedShape = ShapeType.None;
        }
        private void UnclickAllTools(object sender)
        {
            object[] tools = { BrushButton, EraseButton, BackgroundTool, ManipulateButton, rectItem, ellipseItem };

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
                        if (btn == BackgroundTool) isPushed_Background = false;
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

                // Ensure TabControl exists and is added to the UI
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
                }

                // Create new tab and canvas
                TabPage newTabPage = new TabPage($"Tab {tabControl.TabPages.Count + 1}");

                PictureBox pictureBox = new PictureBox
                {
                    BackColor = Color.Transparent,
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Width = canvasWidth,
                    Height = canvasHeight
                };

                Bitmap canvasBitmap = new Bitmap(canvasWidth, canvasHeight);
                pictureBox.Image = canvasBitmap;
                pictureBox.Tag = canvasBitmap;

                // Add event handlers
                pictureBox.MouseDown += PictureBox_MouseDown;
                pictureBox.MouseMove += PictureBox_MouseMove;
                pictureBox.MouseUp += PictureBox_MouseUp;
                pictureBox.Paint += PictureBox_Paint;

                // Add PictureBox to TabPage
                newTabPage.Controls.Add(pictureBox);

                // Add TabPage to TabControl
                tabControl.TabPages.Add(newTabPage);
                tabControl.SelectedTab = newTabPage; // Select the new tab

                // Initialize shapes for this tab
                tabShapes[newTabPage] = new List<Shape>();

                // Center PictureBox in TabPage
                CenterPictureBox(pictureBox, newTabPage);

                MessageBox.Show(
                    $"PictureBox Size: {pictureBox.Width}x{pictureBox.Height}\n" +
                    $"Bitmap Size: {canvasBitmap.Width}x{canvasBitmap.Height}",
                    "Canvas Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CenterPictureBox(PictureBox pictureBox, TabPage tabPage)
        {
            pictureBox.Left = (tabPage.ClientSize.Width - pictureBox.Width) / 2;
            pictureBox.Top = (tabPage.ClientSize.Height - pictureBox.Height) / 2;
        }


        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
       
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox == null) return;
            DrawCheckerboard(e.Graphics, pictureBox.ClientRectangle);
            if (pictureBox.Image != null)
            {
                e.Graphics.DrawImage(pictureBox.Image, 0, 0);
            }
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

            int closeButtonSize = 9;
            Rectangle closeButtonRect = new Rectangle(
                tabRect.Right - closeButtonSize - 1,
                tabRect.Top + 1,
                closeButtonSize,
                closeButtonSize
            );

            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.DrawLine(pen, closeButtonRect.Left, closeButtonRect.Top, closeButtonRect.Right, closeButtonRect.Bottom);
                e.Graphics.DrawLine(pen, closeButtonRect.Left, closeButtonRect.Bottom, closeButtonRect.Right, closeButtonRect.Top);
            }
        }

        private void TabControl_MouseDown(object sender, MouseEventArgs e)
        {
            TabControl tabControl = (TabControl)sender;

            for (int i = 0; i < tabControl.TabPages.Count; i++)
            {
                Rectangle tabRect = tabControl.GetTabRect(i);

                int closeButtonSize = 15;
                Rectangle closeButtonRect = new Rectangle(
                    tabRect.Right - closeButtonSize - 5,
                    tabRect.Top + (tabRect.Height - closeButtonSize) / 2,
                    closeButtonSize,
                    closeButtonSize
                );

                if (closeButtonRect.Contains(e.Location))
                {
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
                    }

                    break;
                }
            }
        }

        private void ShapeSelectButton_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            if (clickedItem == null) return;
            UnclickAllTools(sender);
            if (clickedItem.Checked)
            {
                clickedItem.Checked = false;
                selectedShape = ShapeType.None;
                return;
            }

            rectItem.Checked = false;
            ellipseItem.Checked = false;

            clickedItem.Checked = true;

            if (clickedItem == rectItem)
            {
                selectedShape = ShapeType.Rectangle;
            }
            else if (clickedItem == ellipseItem)
            {
                selectedShape = ShapeType.Ellipse;
            }
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
                                                            brushColor,
                                                            brushThickness);
                    pictureBox.Invalidate();
                }

                else if (isPushed_Background)
                {

                    Color clickedColor = bmp.GetPixel(e.X, e.Y);
                    Color fillColor = brushColor;

                    FloodFill(bmp, e.Location, clickedColor, fillColor);
                    pictureBox.Invalidate();
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
                    }
                    else
                    {
                        selectedShapeForManipulation = null;
                        currentManipulationMode = ManipulationMode.None;
                        activeResizeHandle = ResizeHandle.None;
                        Cursor = Cursors.Default;
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
        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (isPainting)
            {
                isPainting = false;
                currentBrush.ResetLastPoint();
            }
            if (isErasing)
            {
                isErasing = false;
                eraser.ResetLastPoint();
            }
            if (selectedShape != ShapeType.None && previewShape != null)
            {
           
                PictureBox pictureBox = sender as PictureBox;
                if (pictureBox == null) return;
                GetCurrentShapes().Add(previewShape);
                previewShape = null;
                pictureBox.Invalidate();

            }
            if (isManipulatingShape)
            {
                selectedShapeForManipulation = null;
                currentManipulationMode = ManipulationMode.None;
                activeResizeHandle = ResizeHandle.None;
                Cursor = Cursors.Default;
            }

            if (isPainting || isErasing || previewShape != null || isManipulatingShape)
            {
                PictureBox pictureBox = sender as PictureBox;
                if (pictureBox != null && pictureBox.Tag is Bitmap canvas)
                {
                    AddToHistory(canvas);
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



        private void OpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                openDialog.Title = "Open Drawing";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    NewButton_Click(sender, e);

                    TabPage newTabPage = tabControl.SelectedTab;
                    PictureBox pictureBox = newTabPage.Controls.OfType<PictureBox>().FirstOrDefault();


                    Bitmap loadedImage = new Bitmap(openDialog.FileName);
                    pictureBox.Image = loadedImage;
                    pictureBox.Tag = loadedImage;

                    newTabPage.Text = Path.GetFileNameWithoutExtension(openDialog.FileName);
                }
            }
        }


        private PictureBox? GetActivePictureBox()
        {
            TabPage activeTab = tabControl.SelectedTab;
            return activeTab?.Controls.OfType<PictureBox>().FirstOrDefault();
        }

        // history part
        private List<Bitmap> historySnapshots = new List<Bitmap>();
        private void AddToHistory(Bitmap canvasBitmap)
        {
            Bitmap snapshot = new Bitmap(canvasBitmap);
            historySnapshots.Add(snapshot);
            AddThumbnailToHistoryView(snapshot, historySnapshots.Count - 1);
        }

        private void AddThumbnailToHistoryView(Bitmap snapshot, int index)
        {
            PictureBox thumbnail = new PictureBox
            {
                Width = 100,
                Height = 70,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = new Bitmap(snapshot),
                Margin = new Padding(2)
            };

            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(thumbnail, $"Step #{index + 1}");

            thumbnail.Click += (s, e) => GoToHistoryState(index);

            historyPanel.Controls.Add(thumbnail);
        }

        private void GoToHistoryState(int index)
        {
            var pictureBox = GetActivePictureBox();
            if (pictureBox != null && index >= 0 && index < historySnapshots.Count)
            {
                pictureBox.Image?.Dispose();
                pictureBox.Image = new Bitmap(historySnapshots[index]);
                pictureBox.Tag = pictureBox.Image;
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

        }
        private List<Shape> GetCurrentShapes()
        {
            if (tabControl?.SelectedTab != null && tabShapes.ContainsKey(tabControl.SelectedTab))
                return tabShapes[tabControl.SelectedTab];
            return new List<Shape>();
        }
    }
}

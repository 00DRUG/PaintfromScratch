using PaintfromScratch.FiguresClasses;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace PaintfromScratch
{
    partial class MainWindow
    {
  
        private System.ComponentModel.IContainer components = null;
   
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        /// 

        private void CustomParameters()
        {
            switchParameters();
            InitializeShapeSelection();
           
            rectItem.TextImageRelation = TextImageRelation.ImageBeforeText;
            ellipseItem.BackgroundImageLayout = ImageLayout.Zoom;
            ellipseItem.TextImageRelation = TextImageRelation.ImageBeforeText;
            LineStyleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            LineStyleComboBox.SelectedIndex = 0;
            // Dock menu and toolstrip to top
            menuStrip1.Dock = DockStyle.Top;
            toolStrip1.Dock = DockStyle.Top;
            // Dock splitContainer to fill the form
            splitContainer1.Dock = DockStyle.Fill;
            // Panel1: tools/colors (keep as is, or anchor controls as needed)
            colorCircle.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            colorPreview.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            thicknessNumericUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            LineStyleComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            spacingUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // Dock historyPanel to the right
            historyPanel.Dock = DockStyle.Right;
            historyPanel.Width = 120;

            //  historyPanel
            splitContainer1.Panel2.Controls.SetChildIndex(historyPanel, 0);


        }
        private void switchParameters() { 
            redSwitch.Minimum = 0;
            redSwitch.Maximum = 255;
            greenSwitch.Minimum = 0;
            greenSwitch.Maximum = 255;
            blueSwitch.Minimum = 0; 
            blueSwitch.Maximum = 255;
        }
        private void InitializeShapeSelection()
        {
            rectItem.Click += (s, e) => selectedShape = ShapeType.Rectangle;
            ellipseItem.Click += (s, e) => selectedShape = ShapeType.Ellipse;
            rectItem.Click += ShapeSelectButton_Click;
            ellipseItem.Click += ShapeSelectButton_Click;

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            NewButton = new ToolStripMenuItem();
            SaveButton = new ToolStripMenuItem();
            OpenButton = new ToolStripMenuItem();
            ExitButton = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            spacingUpDown = new NumericUpDown();
            textBox_R = new TextBox();
            textBox_B = new TextBox();
            textBox_G = new TextBox();
            redSwitch = new NumericUpDown();
            blueSwitch = new NumericUpDown();
            greenSwitch = new NumericUpDown();
            colorPreview = new PictureBox();
            colorCircle = new ColorCircle();
            thicknessNumericUpDown = new NumericUpDown();
            LineStyleComboBox = new ComboBox();
            historyPanel = new Panel();
            BrushButton = new ToolStripButton();
            EraseButton = new ToolStripButton();
            BackgroundToolButton = new ToolStripButton();
            ManipulateButton = new ToolStripButton();
            toolStrip1 = new ToolStrip();
            FiguresToolButton = new ToolStripDropDownButton();
            rectItem = new ToolStripMenuItem();
            ellipseItem = new ToolStripMenuItem();
            CleanButton = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            historyToggleButton = new ToolStripButton();
            ApplyButton = new ToolStripButton();
            UndoButton = new ToolStripButton();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spacingUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)redSwitch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)blueSwitch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)greenSwitch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)colorPreview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)thicknessNumericUpDown).BeginInit();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(766, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { NewButton, SaveButton, OpenButton, ExitButton });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(40, 20);
            fileToolStripMenuItem.Text = " File";
            // 
            // NewButton
            // 
            NewButton.Name = "NewButton";
            NewButton.Size = new Size(103, 22);
            NewButton.Text = "New";
            NewButton.Click += NewButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(103, 22);
            SaveButton.Text = "Save";
            SaveButton.Click += SaveFile_Click;
            // 
            // OpenButton
            // 
            OpenButton.Name = "OpenButton";
            OpenButton.Size = new Size(103, 22);
            OpenButton.Text = "Open";
            OpenButton.Click += OpenFile_Click;
            // 
            // ExitButton
            // 
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(103, 22);
            ExitButton.Text = "Exit";
            ExitButton.Click += ExitButton_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 57);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(spacingUpDown);
            splitContainer1.Panel1.Controls.Add(textBox_R);
            splitContainer1.Panel1.Controls.Add(textBox_B);
            splitContainer1.Panel1.Controls.Add(textBox_G);
            splitContainer1.Panel1.Controls.Add(redSwitch);
            splitContainer1.Panel1.Controls.Add(blueSwitch);
            splitContainer1.Panel1.Controls.Add(greenSwitch);
            splitContainer1.Panel1.Controls.Add(colorPreview);
            splitContainer1.Panel1.Controls.Add(colorCircle);
            splitContainer1.Panel1.Controls.Add(thicknessNumericUpDown);
            splitContainer1.Panel1.Controls.Add(LineStyleComboBox);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(historyPanel);
            splitContainer1.Size = new Size(766, 504);
            splitContainer1.SplitterDistance = 76;
            splitContainer1.TabIndex = 2;
            // 
            // spacingUpDown
            // 
            spacingUpDown.Location = new Point(3, 255);
            spacingUpDown.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            spacingUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            spacingUpDown.Name = "spacingUpDown";
            spacingUpDown.Size = new Size(70, 23);
            spacingUpDown.TabIndex = 9;
            spacingUpDown.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // textBox_R
            // 
            textBox_R.BackColor = SystemColors.Menu;
            textBox_R.BorderStyle = BorderStyle.None;
            textBox_R.Location = new Point(3, 134);
            textBox_R.Name = "textBox_R";
            textBox_R.Size = new Size(10, 16);
            textBox_R.TabIndex = 8;
            textBox_R.Text = "R";
            // 
            // textBox_B
            // 
            textBox_B.BackColor = SystemColors.Menu;
            textBox_B.BorderStyle = BorderStyle.None;
            textBox_B.Location = new Point(3, 192);
            textBox_B.Name = "textBox_B";
            textBox_B.Size = new Size(10, 16);
            textBox_B.TabIndex = 7;
            textBox_B.Text = "B";
            // 
            // textBox_G
            // 
            textBox_G.BackColor = SystemColors.Menu;
            textBox_G.BorderStyle = BorderStyle.None;
            textBox_G.Location = new Point(3, 163);
            textBox_G.Name = "textBox_G";
            textBox_G.Size = new Size(10, 16);
            textBox_G.TabIndex = 6;
            textBox_G.Text = "G";
            // 
            // redSwitch
            // 
            redSwitch.Location = new Point(19, 132);
            redSwitch.Name = "redSwitch";
            redSwitch.Size = new Size(56, 23);
            redSwitch.TabIndex = 3;
            redSwitch.ValueChanged += RGB_ValueChanged;
            // 
            // blueSwitch
            // 
            blueSwitch.Location = new Point(19, 190);
            blueSwitch.Name = "blueSwitch";
            blueSwitch.Size = new Size(56, 23);
            blueSwitch.TabIndex = 1;
            blueSwitch.ValueChanged += RGB_ValueChanged;
            // 
            // greenSwitch
            // 
            greenSwitch.Location = new Point(19, 161);
            greenSwitch.Name = "greenSwitch";
            greenSwitch.Size = new Size(56, 23);
            greenSwitch.TabIndex = 2;
            greenSwitch.ValueChanged += RGB_ValueChanged;
            // 
            // colorPreview
            // 
            colorPreview.Location = new Point(0, 220);
            colorPreview.Name = "colorPreview";
            colorPreview.Size = new Size(75, 29);
            colorPreview.TabIndex = 1;
            colorPreview.TabStop = false;
            // 
            // colorCircle
            // 
            colorCircle.Location = new Point(3, 58);
            colorCircle.Name = "colorCircle";
            colorCircle.Size = new Size(72, 68);
            colorCircle.TabIndex = 0;
            colorCircle.ColorSelected += ColorCircle_ColorSelected;
            // 
            // thicknessNumericUpDown
            // 
            thicknessNumericUpDown.ImeMode = ImeMode.Off;
            thicknessNumericUpDown.Location = new Point(3, 32);
            thicknessNumericUpDown.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            thicknessNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            thicknessNumericUpDown.Name = "thicknessNumericUpDown";
            thicknessNumericUpDown.Size = new Size(70, 23);
            thicknessNumericUpDown.TabIndex = 2;
            thicknessNumericUpDown.TabStop = false;
            thicknessNumericUpDown.Value = new decimal(new int[] { 10, 0, 0, 0 });
            thicknessNumericUpDown.ValueChanged += ThicknessNumericUpDown_ValueChanged;
            // 
            // LineStyleComboBox
            // 
            LineStyleComboBox.ImeMode = ImeMode.NoControl;
            LineStyleComboBox.Items.AddRange(new object[] { "Circle", "Square", "Star", "Triangle" });
            LineStyleComboBox.Location = new Point(3, 3);
            LineStyleComboBox.Name = "LineStyleComboBox";
            LineStyleComboBox.Size = new Size(70, 23);
            LineStyleComboBox.Sorted = true;
            LineStyleComboBox.TabIndex = 1;
            LineStyleComboBox.SelectedIndexChanged += LineStyleComboBox_SelectedIndexChanged;
            // 
            // historyPanel
            // 
            historyPanel.AutoScroll = true;
            historyPanel.BorderStyle = BorderStyle.FixedSingle;
            historyPanel.Location = new Point(601, 3);
            historyPanel.Name = "historyPanel";
            historyPanel.Size = new Size(85, 701);
            historyPanel.TabIndex = 0;
            historyPanel.Visible = false;
            // 
            // BrushButton
            // 
            BrushButton.AutoSize = false;
            BrushButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            BrushButton.Image = PublicResXFileCodeGenerator.Resources.brush;
            BrushButton.ImageTransparentColor = Color.Magenta;
            BrushButton.Name = "BrushButton";
            BrushButton.Size = new Size(30, 30);
            BrushButton.Text = "Brush tool";
            BrushButton.Click += BrushButton_Click;
            // 
            // EraseButton
            // 
            EraseButton.AutoSize = false;
            EraseButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            EraseButton.Image = (Image)resources.GetObject("EraseButton.Image");
            EraseButton.ImageTransparentColor = Color.Magenta;
            EraseButton.Name = "EraseButton";
            EraseButton.Size = new Size(30, 30);
            EraseButton.Text = "Erasing tool";
            EraseButton.Click += EraseButton_Click;
            // 
            // BackgroundToolButton
            // 
            BackgroundToolButton.AutoSize = false;
            BackgroundToolButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            BackgroundToolButton.Image = PublicResXFileCodeGenerator.Resources.bucket;
            BackgroundToolButton.ImageTransparentColor = Color.Magenta;
            BackgroundToolButton.Name = "BackgroundToolButton";
            BackgroundToolButton.Size = new Size(30, 30);
            BackgroundToolButton.Text = "Background filling tool";
            BackgroundToolButton.Click += BackgroundTool_Click;
            // 
            // ManipulateButton
            // 
            ManipulateButton.AutoSize = false;
            ManipulateButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ManipulateButton.Image = PublicResXFileCodeGenerator.Resources.resize;
            ManipulateButton.ImageTransparentColor = Color.Magenta;
            ManipulateButton.Name = "ManipulateButton";
            ManipulateButton.Size = new Size(30, 30);
            ManipulateButton.Text = "Resizing tool";
            ManipulateButton.ToolTipText = "ManipulateButton";
            ManipulateButton.Click += ManipulateButton_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { EraseButton, BrushButton, FiguresToolButton, BackgroundToolButton, ManipulateButton, CleanButton, toolStripSeparator1, historyToggleButton, ApplyButton, UndoButton });
            toolStrip1.Location = new Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(766, 33);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // FiguresToolButton
            // 
            FiguresToolButton.AutoSize = false;
            FiguresToolButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            FiguresToolButton.DropDownItems.AddRange(new ToolStripItem[] { rectItem, ellipseItem });
            FiguresToolButton.Image = PublicResXFileCodeGenerator.Resources.figures;
            FiguresToolButton.ImageTransparentColor = Color.Magenta;
            FiguresToolButton.Name = "FiguresToolButton";
            FiguresToolButton.Size = new Size(30, 30);
            FiguresToolButton.Text = "Figure tool";
            // 
            // rectItem
            // 
            rectItem.Image = PublicResXFileCodeGenerator.Resources.rectangle;
            rectItem.ImageAlign = ContentAlignment.MiddleLeft;
            rectItem.Name = "rectItem";
            rectItem.Size = new Size(126, 22);
            rectItem.Text = "Rectangle";
            // 
            // ellipseItem
            // 
            ellipseItem.Image = PublicResXFileCodeGenerator.Resources.ellipse;
            ellipseItem.ImageAlign = ContentAlignment.MiddleLeft;
            ellipseItem.Name = "ellipseItem";
            ellipseItem.Size = new Size(126, 22);
            ellipseItem.Text = "Ellipse";
            // 
            // CleanButton
            // 
            CleanButton.AutoSize = false;
            CleanButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            CleanButton.Image = PublicResXFileCodeGenerator.Resources.clean3_com;
            CleanButton.ImageTransparentColor = Color.Magenta;
            CleanButton.Name = "CleanButton";
            CleanButton.Size = new Size(30, 30);
            CleanButton.Text = "Cleaning tool";
            CleanButton.Click += CleanButton_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 33);
            // 
            // historyToggleButton
            // 
            historyToggleButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            historyToggleButton.Image = PublicResXFileCodeGenerator.Resources.history;
            historyToggleButton.ImageTransparentColor = Color.Magenta;
            historyToggleButton.Name = "historyToggleButton";
            historyToggleButton.Size = new Size(23, 30);
            historyToggleButton.Text = "History Tool";
            historyToggleButton.ToolTipText = "History Tool";
            historyToggleButton.Click += historyToggleButton_Click;
            // 
            // ApplyButton
            // 
            ApplyButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ApplyButton.Image = PublicResXFileCodeGenerator.Resources.check_mark;
            ApplyButton.ImageTransparentColor = Color.Magenta;
            ApplyButton.Name = "ApplyButton";
            ApplyButton.Size = new Size(23, 30);
            ApplyButton.Text = "ApplyButton";
            ApplyButton.ToolTipText = "Apply Button";
            ApplyButton.Visible = false;
            ApplyButton.Click += ApplyButton_Click;
            // 
            // UndoButton
            // 
            UndoButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            UndoButton.Image = (Image)resources.GetObject("UndoButton.Image");
            UndoButton.ImageTransparentColor = Color.Magenta;
            UndoButton.Name = "UndoButton";
            UndoButton.Size = new Size(23, 30);
            UndoButton.Text = "UndoButton";
            UndoButton.ToolTipText = "UndoButton";
            UndoButton.Visible = false;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(766, 561);
            Controls.Add(splitContainer1);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            MinimumSize = new Size(600, 600);
            Name = "MainWindow";
            Text = "Paint";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spacingUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)redSwitch).EndInit();
            ((System.ComponentModel.ISupportInitialize)blueSwitch).EndInit();
            ((System.ComponentModel.ISupportInitialize)greenSwitch).EndInit();
            ((System.ComponentModel.ISupportInitialize)colorPreview).EndInit();
            ((System.ComponentModel.ISupportInitialize)thicknessNumericUpDown).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private SplitContainer splitContainer1;
        private ToolStripMenuItem NewButton;
        private ToolStripMenuItem SaveButton;
        private ToolStripMenuItem ExitButton;
        private ToolStripMenuItem OpenButton;
        private ToolStripButton BrushButton;
        private ToolStripButton EraseButton;
        private ToolStripButton BackgroundToolButton;
        private ToolStripButton ManipulateButton;
        private ToolStrip toolStrip1;
        private ToolStripSeparator toolStripSeparator1;
        private NumericUpDown thicknessNumericUpDown;
        private ComboBox LineStyleComboBox;
        private NumericUpDown blueSwitch;
        private NumericUpDown greenSwitch;
        private NumericUpDown redSwitch;
        private TextBox textBox_B;
        private TextBox textBox_G;
        private TextBox textBox_R;
        private ToolStripDropDownButton FiguresToolButton;
        private ToolStripMenuItem rectItem;
        private ToolStripMenuItem ellipseItem;
        private NumericUpDown spacingUpDown;
        private ToolStripButton CleanButton;
        private Panel historyPanel;
        private ToolStripButton ApplyButton;
        private ToolStripButton historyToggleButton;
        private ToolStripButton UndoButton;
    }
}

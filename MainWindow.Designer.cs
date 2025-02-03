namespace PaintfromScratch
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            NewButton = new ToolStripMenuItem();
            SaveButton = new ToolStripMenuItem();
            CloseButton = new ToolStripMenuItem();
            OpenButton = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            redSwitch = new NumericUpDown();
            blueSwitch = new NumericUpDown();
            okButton = new Button();
            greenSwitch = new NumericUpDown();
            colorPreview = new PictureBox();
            colorCircle = new ColorCircle();
            thicknessNumericUpDown = new NumericUpDown();
            LineStyleComboBox = new ComboBox();
            BrushButton = new ToolStripButton();
            toolStripButton2 = new ToolStripButton();
            toolStripButton3 = new ToolStripButton();
            toolStripButton4 = new ToolStripButton();
            toolStrip1 = new ToolStrip();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripButton5 = new ToolStripButton();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
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
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(766, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { NewButton, SaveButton, CloseButton, OpenButton });
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
            // 
            // CloseButton
            // 
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(103, 22);
            CloseButton.Text = "Close";
            // 
            // OpenButton
            // 
            OpenButton.Name = "OpenButton";
            OpenButton.Size = new Size(103, 22);
            OpenButton.Text = "Open";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Location = new Point(0, 49);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(redSwitch);
            splitContainer1.Panel1.Controls.Add(blueSwitch);
            splitContainer1.Panel1.Controls.Add(okButton);
            splitContainer1.Panel1.Controls.Add(greenSwitch);
            splitContainer1.Panel1.Controls.Add(colorPreview);
            splitContainer1.Panel1.Controls.Add(colorCircle);
            splitContainer1.Panel1.Controls.Add(thicknessNumericUpDown);
            splitContainer1.Panel1.Controls.Add(LineStyleComboBox);
            splitContainer1.Size = new Size(766, 488);
            splitContainer1.SplitterDistance = 76;
            splitContainer1.TabIndex = 2;
            // 
            // redSwitch
            // 
            redSwitch.Location = new Point(3, 301);
            redSwitch.Name = "redSwitch";
            redSwitch.Size = new Size(72, 23);
            redSwitch.TabIndex = 3;
            redSwitch.ValueChanged += RGB_ValueChanged;
            // 
            // blueSwitch
            // 
            blueSwitch.Location = new Point(3, 272);
            blueSwitch.Name = "blueSwitch";
            blueSwitch.Size = new Size(72, 23);
            blueSwitch.TabIndex = 1;
            blueSwitch.ValueChanged += RGB_ValueChanged;
            // 
            // okButton
            // 
            okButton.Location = new Point(0, 214);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 0;
            okButton.Click += OkButton_Click;
            // 
            // greenSwitch
            // 
            greenSwitch.Location = new Point(3, 243);
            greenSwitch.Name = "greenSwitch";
            greenSwitch.Size = new Size(72, 23);
            greenSwitch.TabIndex = 2;
            greenSwitch.ValueChanged += RGB_ValueChanged;
            // 
            // colorPreview
            // 
            colorPreview.Location = new Point(0, 179);
            colorPreview.Name = "colorPreview";
            colorPreview.Size = new Size(75, 29);
            colorPreview.TabIndex = 1;
            colorPreview.TabStop = false;
            // 
            // colorCircle
            // 
            colorCircle.Location = new Point(3, 90);
            colorCircle.Name = "colorCircle";
            colorCircle.Size = new Size(70, 83);
            colorCircle.TabIndex = 0;
            colorCircle.ColorSelected += ColorCircle_ColorSelected;
            // 
            // thicknessNumericUpDown
            // 
            thicknessNumericUpDown.Location = new Point(3, 61);
            thicknessNumericUpDown.Name = "thicknessNumericUpDown";
            thicknessNumericUpDown.Size = new Size(70, 23);
            thicknessNumericUpDown.TabIndex = 2;
            thicknessNumericUpDown.Minimum = 1;
            thicknessNumericUpDown.Maximum = 20;
            thicknessNumericUpDown.Value = 3;
            thicknessNumericUpDown.ValueChanged += ThicknessNumericUpDown_ValueChanged;
            // 
            // LineStyleComboBox
            // 
            LineStyleComboBox.FormattingEnabled = false;
            LineStyleComboBox.Location = new Point(3, 32);
            LineStyleComboBox.Name = "LineStyleComboBox";
            LineStyleComboBox.Size = new Size(70, 23);
            LineStyleComboBox.TabIndex = 1;
            LineStyleComboBox.Items.AddRange(new object[] { "Solid", "Dash", "Dot", "DashDot", "DashDotDot" });
            LineStyleComboBox.SelectedIndex = 0;
            LineStyleComboBox.SelectedIndexChanged += LineStyleComboBox_SelectedIndexChanged;
            // 
            // BrushButton
            // 
            BrushButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            BrushButton.Image = (Image)resources.GetObject("BrushButton.Image");
            BrushButton.ImageTransparentColor = Color.Magenta;
            BrushButton.Name = "BrushButton";
            BrushButton.Size = new Size(23, 22);
            BrushButton.Text = "toolStripButton1";
            BrushButton.Click += BrushButton_Click;
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton2.Image = (Image)resources.GetObject("toolStripButton2.Image");
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(23, 22);
            toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton3.Image = (Image)resources.GetObject("toolStripButton3.Image");
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(23, 22);
            toolStripButton3.Text = "toolStripButton3";
            // 
            // toolStripButton4
            // 
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton4.Image = (Image)resources.GetObject("toolStripButton4.Image");
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(23, 22);
            toolStripButton4.Text = "toolStripButton4";
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { BrushButton, toolStripButton2, toolStripButton3, toolStripButton4, toolStripSeparator1, toolStripButton5 });
            toolStrip1.Location = new Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(766, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // toolStripButton5
            // 
            toolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton5.Image = (Image)resources.GetObject("toolStripButton5.Image");
            toolStripButton5.ImageTransparentColor = Color.Magenta;
            toolStripButton5.Name = "toolStripButton5";
            toolStripButton5.Size = new Size(23, 22);
            toolStripButton5.Text = "toolStripButton5";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(766, 537);
            Controls.Add(splitContainer1);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            MinimumSize = new Size(400, 400);
            Name = "MainWindow";
            Text = "Paint";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
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
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private SplitContainer splitContainer1;
        private ToolStripMenuItem NewButton;
        private ToolStripMenuItem SaveButton;
        private ToolStripMenuItem CloseButton;
        private ToolStripMenuItem OpenButton;
        private ToolStripButton BrushButton;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton4;
        private ToolStrip toolStrip1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButton5;
        private NumericUpDown thicknessNumericUpDown;
        private ComboBox LineStyleComboBox;
        private Button okButton;
        private NumericUpDown blueSwitch;
        private NumericUpDown greenSwitch;
        private NumericUpDown redSwitch;
    }
}

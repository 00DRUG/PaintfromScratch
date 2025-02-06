﻿using System.Numerics;
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
        }
        private void switchParameters() { 
            redSwitch.Minimum = 0;
            redSwitch.Maximum = 255;
            greenSwitch.Minimum = 0;
            greenSwitch.Maximum = 255;
            blueSwitch.Minimum = 0; 
            blueSwitch.Maximum = 255;
        }

        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            NewButton = new ToolStripMenuItem();
            SaveButton = new ToolStripMenuItem();
            CloseButton = new ToolStripMenuItem();
            OpenButton = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            textBox_R = new TextBox();
            textBox_B = new TextBox();
            textBox_G = new TextBox();
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
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 49);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(textBox_R);
            splitContainer1.Panel1.Controls.Add(textBox_B);
            splitContainer1.Panel1.Controls.Add(textBox_G);
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
            // okButton
            // 
            okButton.Location = new Point(3, 255);
            okButton.Name = "okButton";
            okButton.Size = new Size(72, 23);
            okButton.TabIndex = 0;
            okButton.Text = "Apply";
            okButton.Click += OkButton_Click;
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
            thicknessNumericUpDown.Location = new Point(3, 32);
            thicknessNumericUpDown.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            thicknessNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            thicknessNumericUpDown.Name = "thicknessNumericUpDown";
            thicknessNumericUpDown.Size = new Size(70, 23);
            thicknessNumericUpDown.TabIndex = 2;
            thicknessNumericUpDown.Value = new decimal(new int[] { 3, 0, 0, 0 });
            thicknessNumericUpDown.ValueChanged += ThicknessNumericUpDown_ValueChanged;
            // 
            // LineStyleComboBox
            // 
            LineStyleComboBox.Items.AddRange(new object[] { "Solid", "Dash", "Dot", "DashDot", "DashDotDot" });
            LineStyleComboBox.Location = new Point(3, 3);
            LineStyleComboBox.Name = "LineStyleComboBox";
            LineStyleComboBox.Size = new Size(70, 23);
            LineStyleComboBox.TabIndex = 1;
            LineStyleComboBox.SelectedIndexChanged += LineStyleComboBox_SelectedIndexChanged;
            // 
            // BrushButton
            // 
            BrushButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            BrushButton.ImageTransparentColor = Color.Magenta;
            BrushButton.Name = "BrushButton";
            BrushButton.Size = new Size(23, 22);
            BrushButton.Text = "toolStripButton1";
            BrushButton.Click += BrushButton_Click;
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(23, 22);
            toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(23, 22);
            toolStripButton3.Text = "toolStripButton3";
            // 
            // toolStripButton4
            // 
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
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
            splitContainer1.Panel1.PerformLayout();
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
        private TextBox textBox_B;
        private TextBox textBox_G;
        private TextBox textBox_R;
    }
}

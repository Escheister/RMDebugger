namespace RMDebugger
{
    partial class HexUpdate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.HexPathBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.HexPageSize = new System.Windows.Forms.NumericUpDown();
            this.BytesEnd = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.BytesStart = new System.Windows.Forms.Label();
            this.UpdateBar = new System.Windows.Forms.ProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.InfoStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.SignaturePanel = new System.Windows.Forms.Panel();
            this.ThroughSignID = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.TargetSignID = new System.Windows.Forms.NumericUpDown();
            this.NeedThrough = new System.Windows.Forms.CheckBox();
            this.HexUploadButton = new System.Windows.Forms.Button();
            this.HexPathButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.HexPageSize)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SignaturePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThroughSignID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetSignID)).BeginInit();
            this.SuspendLayout();
            // 
            // HexPathBox
            // 
            this.HexPathBox.AllowDrop = true;
            this.HexPathBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HexPathBox.FormattingEnabled = true;
            this.HexPathBox.Location = new System.Drawing.Point(4, 35);
            this.HexPathBox.Name = "HexPathBox";
            this.HexPathBox.Size = new System.Drawing.Size(269, 21);
            this.HexPathBox.TabIndex = 33;
            this.HexPathBox.TextChanged += new System.EventHandler(this.HexPathBox_TextChanged);
            this.HexPathBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.HexPathBox_DragDrop);
            this.HexPathBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.HexPathBox_DragEnter);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Page Size:";
            // 
            // HexPageSize
            // 
            this.HexPageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.HexPageSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HexPageSize.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.HexPageSize.Location = new System.Drawing.Point(259, 59);
            this.HexPageSize.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.HexPageSize.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.HexPageSize.Name = "HexPageSize";
            this.HexPageSize.Size = new System.Drawing.Size(40, 20);
            this.HexPageSize.TabIndex = 30;
            this.HexPageSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.HexPageSize.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // BytesEnd
            // 
            this.BytesEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BytesEnd.BackColor = System.Drawing.Color.Transparent;
            this.BytesEnd.Location = new System.Drawing.Point(167, 107);
            this.BytesEnd.Name = "BytesEnd";
            this.BytesEnd.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.BytesEnd.Size = new System.Drawing.Size(50, 15);
            this.BytesEnd.TabIndex = 28;
            this.BytesEnd.Text = "0";
            this.BytesEnd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(144, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "//";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BytesStart
            // 
            this.BytesStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BytesStart.BackColor = System.Drawing.Color.Transparent;
            this.BytesStart.Location = new System.Drawing.Point(88, 107);
            this.BytesStart.Name = "BytesStart";
            this.BytesStart.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.BytesStart.Size = new System.Drawing.Size(50, 15);
            this.BytesStart.TabIndex = 26;
            this.BytesStart.Text = "0";
            this.BytesStart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UpdateBar
            // 
            this.UpdateBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateBar.BackColor = System.Drawing.Color.White;
            this.UpdateBar.ForeColor = System.Drawing.Color.Transparent;
            this.UpdateBar.Location = new System.Drawing.Point(4, 82);
            this.UpdateBar.Margin = new System.Windows.Forms.Padding(1);
            this.UpdateBar.Name = "UpdateBar";
            this.UpdateBar.Size = new System.Drawing.Size(295, 22);
            this.UpdateBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.UpdateBar.TabIndex = 29;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.White;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.InfoStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 135);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(304, 22);
            this.statusStrip1.TabIndex = 31;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(73, 17);
            this.toolStripStatusLabel1.Text = "Information:";
            // 
            // InfoStatus
            // 
            this.InfoStatus.Name = "InfoStatus";
            this.InfoStatus.Size = new System.Drawing.Size(13, 17);
            this.InfoStatus.Text = "0";
            // 
            // SignaturePanel
            // 
            this.SignaturePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SignaturePanel.BackColor = System.Drawing.Color.White;
            this.SignaturePanel.Controls.Add(this.ThroughSignID);
            this.SignaturePanel.Controls.Add(this.label1);
            this.SignaturePanel.Controls.Add(this.TargetSignID);
            this.SignaturePanel.Controls.Add(this.NeedThrough);
            this.SignaturePanel.Location = new System.Drawing.Point(0, 10);
            this.SignaturePanel.Margin = new System.Windows.Forms.Padding(1);
            this.SignaturePanel.Name = "SignaturePanel";
            this.SignaturePanel.Size = new System.Drawing.Size(304, 25);
            this.SignaturePanel.TabIndex = 36;
            // 
            // ThroughSignID
            // 
            this.ThroughSignID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ThroughSignID.Enabled = false;
            this.ThroughSignID.Location = new System.Drawing.Point(247, 2);
            this.ThroughSignID.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ThroughSignID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ThroughSignID.Name = "ThroughSignID";
            this.ThroughSignID.Size = new System.Drawing.Size(52, 20);
            this.ThroughSignID.TabIndex = 2;
            this.ThroughSignID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ThroughSignID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Signature ID:";
            // 
            // TargetSignID
            // 
            this.TargetSignID.Location = new System.Drawing.Point(74, 2);
            this.TargetSignID.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.TargetSignID.Name = "TargetSignID";
            this.TargetSignID.Size = new System.Drawing.Size(52, 20);
            this.TargetSignID.TabIndex = 0;
            this.TargetSignID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TargetSignID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // NeedThrough
            // 
            this.NeedThrough.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NeedThrough.AutoSize = true;
            this.NeedThrough.BackColor = System.Drawing.Color.Transparent;
            this.NeedThrough.Location = new System.Drawing.Point(142, 4);
            this.NeedThrough.Margin = new System.Windows.Forms.Padding(0);
            this.NeedThrough.Name = "NeedThrough";
            this.NeedThrough.Size = new System.Drawing.Size(107, 17);
            this.NeedThrough.TabIndex = 4;
            this.NeedThrough.Text = "Through RM485:";
            this.NeedThrough.UseVisualStyleBackColor = false;
            this.NeedThrough.CheckedChanged += new System.EventHandler(this.NeedThrough_CheckedChanged);
            // 
            // HexUploadButton
            // 
            this.HexUploadButton.Image = global::RMDebugger.Properties.Resources.StatusRunning;
            this.HexUploadButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.HexUploadButton.Location = new System.Drawing.Point(3, 58);
            this.HexUploadButton.Name = "HexUploadButton";
            this.HexUploadButton.Size = new System.Drawing.Size(81, 22);
            this.HexUploadButton.TabIndex = 37;
            this.HexUploadButton.Text = "Upload";
            this.HexUploadButton.UseVisualStyleBackColor = true;
            this.HexUploadButton.Click += new System.EventHandler(this.HexUploadButton_Click);
            // 
            // HexPathButton
            // 
            this.HexPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.HexPathButton.BackColor = System.Drawing.Color.Transparent;
            this.HexPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HexPathButton.Image = global::RMDebugger.Properties.Resources.OpenFolder;
            this.HexPathButton.Location = new System.Drawing.Point(277, 35);
            this.HexPathButton.Margin = new System.Windows.Forms.Padding(0);
            this.HexPathButton.Name = "HexPathButton";
            this.HexPathButton.Size = new System.Drawing.Size(22, 21);
            this.HexPathButton.TabIndex = 34;
            this.HexPathButton.UseVisualStyleBackColor = false;
            this.HexPathButton.Click += new System.EventHandler(this.HexPathButton_Click);
            // 
            // HexUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(304, 157);
            this.Controls.Add(this.HexUploadButton);
            this.Controls.Add(this.SignaturePanel);
            this.Controls.Add(this.HexPathButton);
            this.Controls.Add(this.HexPathBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HexPageSize);
            this.Controls.Add(this.BytesEnd);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.BytesStart);
            this.Controls.Add(this.UpdateBar);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(320, 196);
            this.Name = "HexUpdate";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Uploader";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HexUpdate_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.HexPageSize)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.SignaturePanel.ResumeLayout(false);
            this.SignaturePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThroughSignID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetSignID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button HexPathButton;
        private System.Windows.Forms.ComboBox HexPathBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown HexPageSize;
        private System.Windows.Forms.Label BytesEnd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label BytesStart;
        private System.Windows.Forms.ProgressBar UpdateBar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel InfoStatus;
        private System.Windows.Forms.Panel SignaturePanel;
        private System.Windows.Forms.NumericUpDown ThroughSignID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown TargetSignID;
        private System.Windows.Forms.CheckBox NeedThrough;
        private System.Windows.Forms.Button HexUploadButton;
    }
}
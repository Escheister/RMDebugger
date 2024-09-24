namespace RMDebugger
{
    partial class DataDebuggerForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataDebuggerForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statesStrip = new System.Windows.Forms.ToolStripDropDownButton();
            this.DEBUGState = new System.Windows.Forms.ToolStripMenuItem();
            this.ERRORState = new System.Windows.Forms.ToolStripMenuItem();
            this.stateLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bufferStrip = new System.Windows.Forms.ToolStripDropDownButton();
            this.lowestBuffer = new System.Windows.Forms.ToolStripMenuItem();
            this.smallBuffer = new System.Windows.Forms.ToolStripMenuItem();
            this.normalBuffer = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumBuffer = new System.Windows.Forms.ToolStripMenuItem();
            this.largeBuffer = new System.Windows.Forms.ToolStripMenuItem();
            this.bufferLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ClearLogger = new System.Windows.Forms.ToolStripButton();
            this.SaveLogger = new System.Windows.Forms.ToolStripButton();
            this.LogBox = new System.Windows.Forms.RichTextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.AppendTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statesStrip,
            this.stateLabel,
            this.toolStripSeparator1,
            this.bufferStrip,
            this.bufferLabel,
            this.toolStripSeparator2,
            this.ClearLogger,
            this.SaveLogger});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(582, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statesStrip
            // 
            this.statesStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statesStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DEBUGState,
            this.ERRORState});
            this.statesStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.statesStrip.Name = "statesStrip";
            this.statesStrip.Size = new System.Drawing.Size(46, 24);
            this.statesStrip.Text = "State";
            // 
            // DEBUGState
            // 
            this.DEBUGState.CheckOnClick = true;
            this.DEBUGState.Name = "DEBUGState";
            this.DEBUGState.Size = new System.Drawing.Size(111, 22);
            this.DEBUGState.Text = "DEBUG";
            // 
            // ERRORState
            // 
            this.ERRORState.Checked = true;
            this.ERRORState.CheckOnClick = true;
            this.ERRORState.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ERRORState.Name = "ERRORState";
            this.ERRORState.Size = new System.Drawing.Size(111, 22);
            this.ERRORState.Text = "ERROR";
            // 
            // stateLabel
            // 
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(0, 24);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // bufferStrip
            // 
            this.bufferStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bufferStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lowestBuffer,
            this.smallBuffer,
            this.normalBuffer,
            this.mediumBuffer,
            this.largeBuffer});
            this.bufferStrip.Image = ((System.Drawing.Image)(resources.GetObject("bufferStrip.Image")));
            this.bufferStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bufferStrip.Name = "bufferStrip";
            this.bufferStrip.Size = new System.Drawing.Size(75, 24);
            this.bufferStrip.Text = "Buffer Size";
            // 
            // lowestBuffer
            // 
            this.lowestBuffer.CheckOnClick = true;
            this.lowestBuffer.Name = "lowestBuffer";
            this.lowestBuffer.Size = new System.Drawing.Size(119, 22);
            this.lowestBuffer.Text = "Lowest";
            // 
            // smallBuffer
            // 
            this.smallBuffer.Checked = true;
            this.smallBuffer.CheckOnClick = true;
            this.smallBuffer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.smallBuffer.Name = "smallBuffer";
            this.smallBuffer.Size = new System.Drawing.Size(119, 22);
            this.smallBuffer.Text = "Small";
            // 
            // normalBuffer
            // 
            this.normalBuffer.CheckOnClick = true;
            this.normalBuffer.Name = "normalBuffer";
            this.normalBuffer.Size = new System.Drawing.Size(119, 22);
            this.normalBuffer.Text = "Normal";
            // 
            // mediumBuffer
            // 
            this.mediumBuffer.CheckOnClick = true;
            this.mediumBuffer.Name = "mediumBuffer";
            this.mediumBuffer.Size = new System.Drawing.Size(119, 22);
            this.mediumBuffer.Text = "Medium";
            // 
            // largeBuffer
            // 
            this.largeBuffer.CheckOnClick = true;
            this.largeBuffer.Name = "largeBuffer";
            this.largeBuffer.Size = new System.Drawing.Size(119, 22);
            this.largeBuffer.Text = "Large";
            // 
            // bufferLabel
            // 
            this.bufferLabel.Name = "bufferLabel";
            this.bufferLabel.Size = new System.Drawing.Size(0, 24);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // ClearLogger
            // 
            this.ClearLogger.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ClearLogger.AutoSize = false;
            this.ClearLogger.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearLogger.Image = global::RMDebugger.Properties.Resources.Eraser;
            this.ClearLogger.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearLogger.Name = "ClearLogger";
            this.ClearLogger.Size = new System.Drawing.Size(24, 24);
            this.ClearLogger.Text = "toolStripButton1";
            this.ClearLogger.ToolTipText = "Clear logs";
            // 
            // SaveLogger
            // 
            this.SaveLogger.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.SaveLogger.AutoSize = false;
            this.SaveLogger.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveLogger.Image = global::RMDebugger.Properties.Resources.SaveAs;
            this.SaveLogger.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.SaveLogger.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveLogger.Name = "SaveLogger";
            this.SaveLogger.Size = new System.Drawing.Size(24, 24);
            this.SaveLogger.Text = "toolStripButton1";
            this.SaveLogger.ToolTipText = "Save logs to";
            // 
            // LogBox
            // 
            this.LogBox.BackColor = System.Drawing.Color.Black;
            this.LogBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogBox.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.LogBox.ForeColor = System.Drawing.Color.White;
            this.LogBox.HideSelection = false;
            this.LogBox.Location = new System.Drawing.Point(0, 27);
            this.LogBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.Size = new System.Drawing.Size(582, 372);
            this.LogBox.TabIndex = 1;
            this.LogBox.Text = "";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "Logger";
            this.saveFileDialog.Filter = "Log files (*.log)|*.log|All files (*.*)|*.*";
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // AppendTimer
            // 
            this.AppendTimer.Interval = 200;
            this.AppendTimer.Tick += new System.EventHandler(this.AppendTimer_Tick);
            // 
            // DataDebuggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(582, 399);
            this.Controls.Add(this.LogBox);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Location = new System.Drawing.Point(100, 100);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(598, 438);
            this.Name = "DataDebuggerForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Logger";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.RichTextBox LogBox;
        private System.Windows.Forms.ToolStripButton ClearLogger;
        private System.Windows.Forms.ToolStripDropDownButton statesStrip;
        private System.Windows.Forms.ToolStripButton SaveLogger;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton bufferStrip;
        private System.Windows.Forms.ToolStripMenuItem lowestBuffer;
        private System.Windows.Forms.ToolStripMenuItem smallBuffer;
        private System.Windows.Forms.ToolStripMenuItem normalBuffer;
        private System.Windows.Forms.ToolStripMenuItem mediumBuffer;
        private System.Windows.Forms.ToolStripMenuItem largeBuffer;
        private System.Windows.Forms.ToolStripMenuItem DEBUGState;
        private System.Windows.Forms.ToolStripMenuItem ERRORState;
        private System.Windows.Forms.ToolStripLabel stateLabel;
        private System.Windows.Forms.ToolStripLabel bufferLabel;
        private System.Windows.Forms.Timer AppendTimer;
    }
}
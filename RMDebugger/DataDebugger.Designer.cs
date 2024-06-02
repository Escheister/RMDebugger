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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.flagStates = new System.Windows.Forms.ToolStripDropDownButton();
            this.allFlagToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.errorsFlagToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveLogger = new System.Windows.Forms.ToolStripButton();
            this.ClearLogger = new System.Windows.Forms.ToolStripButton();
            this.LogBox = new System.Windows.Forms.RichTextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.flagStates,
            this.SaveLogger,
            this.ClearLogger});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(582, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // flagStates
            // 
            this.flagStates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.flagStates.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allFlagToolStrip,
            this.errorsFlagToolStrip});
            this.flagStates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.flagStates.Name = "flagStates";
            this.flagStates.Size = new System.Drawing.Size(47, 22);
            this.flagStates.Text = "Flags";
            // 
            // allFlagToolStrip
            // 
            this.allFlagToolStrip.CheckOnClick = true;
            this.allFlagToolStrip.Name = "allFlagToolStrip";
            this.allFlagToolStrip.Size = new System.Drawing.Size(180, 22);
            this.allFlagToolStrip.Text = "All";
            // 
            // errorsFlagToolStrip
            // 
            this.errorsFlagToolStrip.Checked = true;
            this.errorsFlagToolStrip.CheckOnClick = true;
            this.errorsFlagToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.errorsFlagToolStrip.Name = "errorsFlagToolStrip";
            this.errorsFlagToolStrip.Size = new System.Drawing.Size(180, 22);
            this.errorsFlagToolStrip.Text = "Errors";
            // 
            // SaveLogger
            // 
            this.SaveLogger.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveLogger.Image = global::RMDebugger.Properties.Resources.SaveAs;
            this.SaveLogger.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.SaveLogger.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveLogger.Name = "SaveLogger";
            this.SaveLogger.Size = new System.Drawing.Size(23, 22);
            this.SaveLogger.Text = "toolStripButton1";
            this.SaveLogger.ToolTipText = "Save logs to";
            // 
            // ClearLogger
            // 
            this.ClearLogger.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ClearLogger.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearLogger.Image = global::RMDebugger.Properties.Resources.Eraser;
            this.ClearLogger.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearLogger.Name = "ClearLogger";
            this.ClearLogger.Size = new System.Drawing.Size(23, 22);
            this.ClearLogger.Text = "toolStripButton1";
            this.ClearLogger.ToolTipText = "Clear logs";
            // 
            // LogBox
            // 
            this.LogBox.BackColor = System.Drawing.Color.Black;
            this.LogBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogBox.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.LogBox.ForeColor = System.Drawing.Color.White;
            this.LogBox.Location = new System.Drawing.Point(0, 25);
            this.LogBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.Size = new System.Drawing.Size(582, 374);
            this.LogBox.TabIndex = 1;
            this.LogBox.Text = "";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "Logger";
            this.saveFileDialog.Filter = "Log files (*.log)|*.log|All files (*.*)|*.*";
            this.saveFileDialog.RestoreDirectory = true;
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
        private System.Windows.Forms.ToolStripDropDownButton flagStates;
        public System.Windows.Forms.ToolStripMenuItem allFlagToolStrip;
        public System.Windows.Forms.ToolStripMenuItem errorsFlagToolStrip;
        private System.Windows.Forms.ToolStripButton SaveLogger;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}
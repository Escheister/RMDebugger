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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataDebuggerForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SaveLogger = new System.Windows.Forms.ToolStripButton();
            this.ClearLogger = new System.Windows.Forms.ToolStripButton();
            this.Flags = new System.Windows.Forms.ToolStripDropDownButton();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.SaveLogger,
            this.ClearLogger,
            this.Flags});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // SaveLogger
            // 
            this.SaveLogger.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveLogger.Image = global::RMDebugger.Properties.Resources.SaveAs;
            resources.ApplyResources(this.SaveLogger, "SaveLogger");
            this.SaveLogger.Name = "SaveLogger";
            this.SaveLogger.Click += new System.EventHandler(this.SaveLogger_Click);
            // 
            // ClearLogger
            // 
            this.ClearLogger.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearLogger.Image = global::RMDebugger.Properties.Resources.Eraser;
            resources.ApplyResources(this.ClearLogger, "ClearLogger");
            this.ClearLogger.Name = "ClearLogger";
            this.ClearLogger.Click += new System.EventHandler(this.ClearLogger_Click);
            // 
            // Flags
            // 
            this.Flags.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Flags.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.errorsToolStripMenuItem});
            resources.ApplyResources(this.Flags, "Flags");
            this.Flags.Name = "Flags";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.CheckOnClick = true;
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            resources.ApplyResources(this.allToolStripMenuItem, "allToolStripMenuItem");
            this.allToolStripMenuItem.Click += new System.EventHandler(this.allToolStripMenuItem_Click);
            // 
            // errorsToolStripMenuItem
            // 
            this.errorsToolStripMenuItem.Checked = true;
            this.errorsToolStripMenuItem.CheckOnClick = true;
            this.errorsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.errorsToolStripMenuItem.Name = "errorsToolStripMenuItem";
            resources.ApplyResources(this.errorsToolStripMenuItem, "errorsToolStripMenuItem");
            this.errorsToolStripMenuItem.Click += new System.EventHandler(this.errorsToolStripMenuItem_Click);
            // 
            // LogBox
            // 
            this.LogBox.BackColor = System.Drawing.Color.Black;
            this.LogBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.LogBox, "LogBox");
            this.LogBox.ForeColor = System.Drawing.Color.White;
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "Logger";
            resources.ApplyResources(this.saveFileDialog, "saveFileDialog");
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // DataDebuggerForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.LogBox);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DataDebuggerForm";
            this.Opacity = 0.95D;
            this.ShowIcon = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DataDebuggerForm_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.RichTextBox LogBox;
        private System.Windows.Forms.ToolStripButton ClearLogger;
        private System.Windows.Forms.ToolStripDropDownButton Flags;
        public System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem errorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton SaveLogger;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}
namespace File_Verifier
{
    partial class AboutInfo
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
            this.crcBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.verBox = new System.Windows.Forms.TextBox();
            this.whatsNewBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // crcBox
            // 
            this.crcBox.Location = new System.Drawing.Point(125, 24);
            this.crcBox.MaxLength = 10;
            this.crcBox.Name = "crcBox";
            this.crcBox.ReadOnly = true;
            this.crcBox.Size = new System.Drawing.Size(249, 20);
            this.crcBox.TabIndex = 0;
            this.crcBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(229, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "CRC32";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(228, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Version";
            // 
            // verBox
            // 
            this.verBox.Location = new System.Drawing.Point(125, 66);
            this.verBox.MaxLength = 10;
            this.verBox.Name = "verBox";
            this.verBox.ReadOnly = true;
            this.verBox.Size = new System.Drawing.Size(249, 20);
            this.verBox.TabIndex = 3;
            this.verBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // whatsNewBox
            // 
            this.whatsNewBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.whatsNewBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.whatsNewBox.DetectUrls = false;
            this.whatsNewBox.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.whatsNewBox.Location = new System.Drawing.Point(9, 90);
            this.whatsNewBox.Margin = new System.Windows.Forms.Padding(0);
            this.whatsNewBox.Name = "whatsNewBox";
            this.whatsNewBox.ReadOnly = true;
            this.whatsNewBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.whatsNewBox.ShortcutsEnabled = false;
            this.whatsNewBox.Size = new System.Drawing.Size(490, 256);
            this.whatsNewBox.TabIndex = 4;
            this.whatsNewBox.TabStop = false;
            this.whatsNewBox.Text = "";
            // 
            // AboutInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 346);
            this.Controls.Add(this.whatsNewBox);
            this.Controls.Add(this.verBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.crcBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutInfo";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.crc_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox crcBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox verBox;
        private System.Windows.Forms.RichTextBox whatsNewBox;
    }
}
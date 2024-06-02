using System.Collections.Generic;
using System.Windows.Forms;
using StaticSettings;
using System.Linq;

namespace RMDebugger
{
    public partial class DataDebuggerForm : Form
    {
        public DataDebuggerForm()
        {
            InitializeComponent();
            AddEvents();
        }
        private void AddEvents()
        {
            this.FormClosed += (s, e) => Options.debugForm = null;
            LogBox.TextChanged += (s, e) => {
                if (LogBox.Lines.Length > 512)
                {
                    string[] lineArray = LogBox.Lines;
                    List<string> lineList = lineArray.ToList();
                    lineList.RemoveRange(0, 25);
                    LogBox.Lines = lineList.ToArray();
                }
            };
            SaveLogger.Click += (s, e) => {
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
                    System.IO.File.WriteAllText(saveFileDialog.FileName, LogBox.Text);
            };
            ClearLogger.Click += (s, e) => LogBox.Clear();
            errorsFlagToolStrip.Click += (s, e) => CheckFlags(s);
            allFlagToolStrip.Click += (s, e) => CheckFlags(s);
        }
        private void CheckFlags(object sender)
        {
            allFlagToolStrip.Checked = errorsFlagToolStrip.Checked = false;
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            item.Checked = true;
        }
    }
}

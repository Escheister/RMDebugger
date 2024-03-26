using StaticSettings;
using System.Windows.Forms;

namespace RMDebugger
{
    public partial class DataDebuggerForm : Form
    {
        public DataDebuggerForm()
        {
            InitializeComponent();
        }
        private void ClearLogger_Click(object sender, System.EventArgs e)
            => BeginInvoke((MethodInvoker)(() => LogBox.Clear() ));
        private void errorsToolStripMenuItem_Click(object sender, System.EventArgs e) => CheckFlags(sender);
        private void allToolStripMenuItem_Click(object sender, System.EventArgs e) => CheckFlags(sender);
        private void CheckFlags(object sender)
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                allToolStripMenuItem.Checked = errorsToolStripMenuItem.Checked = false;
                ToolStripMenuItem item = (ToolStripMenuItem)sender;
                item.Checked = true;
            }));
        }
        private void SaveLogger_Click(object sender, System.EventArgs e)
        {
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            System.IO.File.WriteAllText(saveFileDialog.FileName, LogBox.Text);
        }

        private void DataDebuggerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Options.debugForm = null;
        }
    }
}

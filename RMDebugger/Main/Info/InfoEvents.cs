using System;
using System.Windows.Forms;

namespace RMDebugger.Main
{
    public partial class MainForm : Form
    {
        private void AddInfoEvents()
        {
            InfoTree.NodeMouseClick += (s, e) =>
                TargetSignID.Value = UInt16.TryParse(e.Node.Text, out ushort newSignTarget)
                    ? newSignTarget
                    : TargetSignID.Value;
            InfoGetInfoButton.Click += InfoTreeNodeClick;
            saveToCsvInfoMenuStrip.Click += InfoSaveToCSVButtonClick;
            InfoAutoCheckBox.CheckedChanged += (s, e) => InfoTimeout.Visible = label3.Visible = InfoAutoCheckBox.Checked;
        }
    }
}

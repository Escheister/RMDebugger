using System.Windows.Forms;
using Microsoft.Win32;
using System;

namespace RMDebugger
{
    public partial class WriteHere : Form
    {
        MainDebugger debugger;
        public WriteHere(MainDebugger _debugger)
        {
            InitializeComponent();
            debugger = _debugger;
        }
        private void RegEditSaveParams()
        {
            try
            {
                string mainName = "RM Debugger";
                RegistryKey currentUserKey = Registry.CurrentUser;
                if (!debugger.checkMainFolder(currentUserKey)) currentUserKey.CreateSubKey(mainName);
                currentUserKey.OpenSubKey(mainName, true);
                RegistryKey rKey = currentUserKey.OpenSubKey(mainName, true);
                if (!debugger.checkInMainFolder(rKey, FolderNameBox.Text)) rKey.CreateSubKey(FolderNameBox.Text);
                RegistryKey sKey = rKey.OpenSubKey(FolderNameBox.Text, true);

                sKey.SetValue("ThroughRM485", $"{debugger.NeedThrough.Checked}");
                sKey.SetValue("MainSignatureID", $"{debugger.TargetSignID.Value}");
                sKey.SetValue("ThroughSignatureID", $"{debugger.ThroughSignID.Value}");
                sKey.SetValue("UDPGateIP", $"{debugger.IPaddressBox.Text}");
                sKey.SetValue("UDPGatePort", $"{debugger.numericPort.Value}");
                sKey.SetValue("LastPageSize", $"{debugger.HexPageSize.Value}");
                sKey.SetValue("LastPathToHex", $"{debugger.HexPathBox.Text}");
                sKey.SetValue("LastComPort", $"{debugger.comPort.Text}");
                sKey.SetValue("LastBaudrate", $"{debugger.BaudRate.Text}");
                sKey.Close();
                rKey.Close();
                currentUserKey.Close();
            }
            catch
            {
                MessageBox.Show(this, "Registry Error.");
            }
        }
        private void CancelButton_Click(object sender, EventArgs e) => Close();
        private void FolderNameBox_TextChanged(object sender, EventArgs e)
        {
            if (FolderNameBox.Text.Length > 0) saveButton.Enabled = true; 
            else saveButton.Enabled = false;
        }
        
        private void saveButton_Click(object sender, EventArgs e)
        {
            RegEditSaveParams();
            debugger.AddSettingsToStrip(FolderNameBox.Text);
            debugger.AddNewEvents();
            Close();
        }
    }
}

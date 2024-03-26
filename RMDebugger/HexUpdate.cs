using System.Threading.Tasks;
using System.Windows.Forms;
using BootloaderProtocol;
using System.Net.Sockets;
using System.IO.Ports;
using ProtocolEnums;
using StaticMethods;
using System.IO;
using System;

using RMDebugger.Properties;
using StaticSettings;

namespace RMDebugger
{
    public partial class HexUpdate : Form
    {
        public HexUpdate()
        {
            InitializeComponent();
            HexUploadButton.Enabled = false;
            NeedThrough.Checked = Options.debugger.NeedThrough.Checked;
            ThroughSignID.Value = Options.debugger.ThroughSignID.Value;
            TargetSignID.Value = Options.debugger.TargetSignID.Value;
            foreach(string item in Options.debugger.HexPathBox.Items) HexPathBox.Items.Add(item);
            HexPathBox.Text = Options.debugger.HexPathBox.Text;
        }

        private byte[] GET_RM_NUMBER(NumericUpDown updn) => BitConverter.GetBytes(Convert.ToUInt16(updn.Value));
        private void After_Uploaded()
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                HexUploadButton.Text = "Upload";
                HexUploadButton.Image = Resources.StatusRunning;
                UpdateBar.Value = 0;
                BytesStart.Text = 0.ToString();
                HexPathBox.Enabled = true;
                HexPageSize.Enabled = true;
                HexPathButton.Enabled = true;
                HexUploadButton.Enabled = true;
                SignaturePanel.Enabled = true;
                Options.debugger.RMData.Enabled = true;
                Options.debugger.SerUdpPages.Enabled = true;
            }));
        }
        private void NeedThrough_CheckedChanged(object sender, EventArgs e)
        {
            decimal targetID;

            if (NeedThrough.Checked)
            {
                targetID = ThroughSignID.Value;
                ThroughSignID.Value = TargetSignID.Value;
                TargetSignID.Value = targetID;
                ThroughSignID.Enabled = true;
            }
            else
            {
                targetID = TargetSignID.Value;
                TargetSignID.Value = ThroughSignID.Value;
                ThroughSignID.Value = targetID;
                ThroughSignID.Enabled = false;
            }
        }
        private string[] FileReader(string path)
        {
            StreamReader file = new StreamReader(path);
            string data = file.ReadToEnd();
            file.Close();
            return data.Trim().Split('\n');
        }
        private void HexPathBox_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(HexPathBox.Text))
            {
                HexUploadButton.Enabled = true;
                string[] len = FileReader(HexPathBox.Text);
                UpdateBar.Maximum = len.Length;
                BytesEnd.Text = len.Length.ToString();
                UpdateBar.Value = 0;
                BytesStart.Text = 0.ToString();
            }
            else HexUploadButton.Enabled = false;
        }
        private bool CheckInsidePaths(string path)
        {
            foreach (string select in HexPathBox.Items)
            {
                if (select == path) return true;
            }
            return false;
        }
        private void HexPathBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] FilePath = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string path in FilePath)
            {
                if (CheckInsidePaths(path)) continue;
                if (new FileInfo(path).Extension == ".hex") HexPathBox.Items.Add(path);
            }
            HexPathBox.SelectedItem = FilePath[0];
        }
        private void HexPathBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
        private void HexPathButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog
            {
                Filter = "Hex файл (*.hex)|*.hex",
                Title = "Укажите файл с расширением *.hex"
            };
            if (HexPathBox.Text != string.Empty) file.InitialDirectory = Path.GetDirectoryName(HexPathBox.Text);
            if (file.ShowDialog() == DialogResult.OK)
            {
                if (!CheckInsidePaths(file.FileName))
                {
                    HexPathBox.Items.Add(file.FileName);
                    HexPathBox.SelectedItem = file.FileName;
                }
            }
        }
        async private void HexUploadButton_Click(object sender, EventArgs e)
        {
            if (HexUploadButton.Text == "Stop")
                HexUploadButton.Text = "Upload";
            else
            {
                Options.debugger.RMData.Enabled = false;
                Options.debugger.SerUdpPages.Enabled = false;
                HexUploadButton.Text = "Stop";
                HexUploadButton.Image = Resources.StatusStopped;
                await Task.Run(() => UploadDataToDevice(new Bootloader(Options.deviceInterface)));
            }
        }
        private byte[] GetCmdThroughOrNot(Bootloader boot, byte[] rmSign, byte[] rmThrough, CmdOutput cmdOutput)
        {
            if (!NeedThrough.Checked) return boot.SendCommand(rmSign, cmdOutput);
            else return boot.SendCommand(rmSign, rmThrough, cmdOutput);
        }
        private byte[] GetCmdThroughOrNot(Bootloader boot, byte[] rmSign, byte[] rmThrough, byte[] data)
        {
            if (!NeedThrough.Checked) return boot.SendCommand(rmSign, data);
            else return boot.SendCommand(rmSign, rmThrough, data);
        }
        async private Task<bool> SendCommand(Bootloader boot, byte[] cmdOut, bool delay = false, int delayMs = 25)
        {
            Tuple<byte[], ProtocolReply> replyes = null;
            DateTime t0 = DateTime.Now;
            TimeSpan tstop = DateTime.Now - t0;
            do
            {
                if (HexUploadButton.Text == "Upload")
                {
                    After_Uploaded();
                    return false;
                }
                try
                {
                    replyes = await boot.GetData(cmdOut, cmdOut.Length, 100);
                    BeginInvoke((MethodInvoker)(() => { InfoStatus.Text = replyes.Item2.ToString(); }));
                    if (replyes.Item2 == ProtocolReply.Ok) break;
                }
                catch (Exception ex)
                {
                    BeginInvoke((MethodInvoker)(() => { InfoStatus.Text = ex.Message; }));
                    tstop = DateTime.Now - t0;
                    if (tstop.Seconds >= 20)
                    {
                        DialogResult message = MessageBox.Show(this, "Timeout", "Something wrong...", MessageBoxButtons.RetryCancel);
                        if (message == DialogResult.Cancel)
                        {
                            After_Uploaded();
                            return false;
                        }
                        else
                        {
                            t0 = DateTime.Now;
                            tstop = DateTime.Now - t0;
                        }
                    }
                    if (delay) await Task.Delay(delayMs);
                }
            }
            while (tstop.Seconds < 20);
            return true;
        }
        async private Task<bool> SendDataCommand(Bootloader boot, byte[] cmdOut)
        {
            Tuple<byte[], ProtocolReply> replyes = null;
            DateTime t0 = DateTime.Now;
            TimeSpan tstop = DateTime.Now - t0;
            do
            {
                if (HexUploadButton.Text == "Upload")
                {
                    After_Uploaded();
                    return false;
                }
                try
                {
                    replyes = await boot.GetData(cmdOut, cmdOut.Length, 150);
                    ProtocolReply reply = Methods.GetDataReply(cmdOut, replyes.Item1, NeedThrough.Checked);
                    BeginInvoke((MethodInvoker)(() => { InfoStatus.Text = reply.ToString(); }));
                    if (reply == ProtocolReply.Ok) break;
                }
                catch (Exception ex)
                {
                    BeginInvoke((MethodInvoker)(() => { InfoStatus.Text = ex.Message; }));
                    tstop = DateTime.Now - t0;
                    if (tstop.Seconds >= 20)
                    {
                        DialogResult message = MessageBox.Show(this, "Timeout", "Something wrong...", MessageBoxButtons.RetryCancel);
                        if (message == DialogResult.Cancel)
                        {
                            After_Uploaded();
                            return false;
                        }
                        else
                        {
                            t0 = DateTime.Now;
                            tstop = DateTime.Now - t0;
                        }
                    }
                }
            }
            while (tstop.Seconds < 20);
            return true;
        }
        async private Task UploadDataToDevice(Bootloader boot)
        {
            byte[][] hex;
            try { hex = boot.GetByteDataFromFile(HexPathBox.Text); }
            catch (Exception ex)
            {
                BeginInvoke((MethodInvoker)(() => { InfoStatus.Text = ex.Message; }));
                After_Uploaded();
                return;
            }
            BeginInvoke((MethodInvoker)(() =>
            {
                HexPathBox.Enabled = false;
                HexPageSize.Enabled = false;
                HexPathButton.Enabled = false;
                SignaturePanel.Enabled = false;
            }));
            byte[] rmSign = GET_RM_NUMBER(TargetSignID);
            byte[] rmThrough = GET_RM_NUMBER(ThroughSignID);

            byte[] cmdBootStart = GetCmdThroughOrNot(boot, rmSign, rmThrough, CmdOutput.START_BOOTLOADER);
            if (!await SendCommand(boot, cmdBootStart, true, 50)) return;
            byte[] cmdUpdateData = GetCmdThroughOrNot(boot, rmSign, rmThrough, CmdOutput.UPDATE_DATA_PAGE);
            byte[] cmdBootStop = GetCmdThroughOrNot(boot, rmSign, rmThrough, CmdOutput.STOP_BOOTLOADER);

            DateTime t0 = DateTime.Now;
            TimeSpan tstop;
            Tuple<byte[], int> tuple;
            for (int i = 0; i <= hex.Length - 1;)
            {
                tuple = boot.GetDataForUpload(hex, (int)HexPageSize.Value, i);
                i = tuple.Item2;
                if (tuple.Item1 is null) continue;
                else
                {
                    byte[] cmdLoadData = GetCmdThroughOrNot(boot, rmSign, rmThrough, tuple.Item1);
                    if (!await SendDataCommand(boot, cmdLoadData)) return;
                    if (!await SendCommand(boot, cmdUpdateData)) return;
                    BeginInvoke((MethodInvoker)(() => { UpdateBar.Value = i; BytesStart.Text = i.ToString(); }));
                }
            }
            if (!await SendCommand(boot, cmdBootStop, true, 50)) return;
            tstop = DateTime.Now - t0;
            BeginInvoke((MethodInvoker)(() => {
                InfoStatus.Text = $"Uploaded for {tstop.Minutes}:{tstop.Seconds}:{tstop.Milliseconds}";
            }));
            After_Uploaded();
        }
        private void HexUpdate_FormClosed(object sender, FormClosedEventArgs e)
        {
            Options.debugger.windowUpdate = null;
            Options.debugger.HexUpdatePage.Enabled = true;
        }
    }
}

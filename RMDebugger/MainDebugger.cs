using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Threading.Tasks;
using RMDebugger.Properties;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.Sockets;
using System.Reflection;
using Microsoft.Win32;
using System.IO.Ports;
using System.Drawing;
using System.Linq;
using System.Net;
using System.IO;
using System;

using ConfigurationProtocol;
using BootloaderProtocol;
using SearchProtocol;
using ProtocolEnums;
using StaticMethods;
using File_Verifier;
using CSV;

namespace RMDebugger
{
    public partial class MainDebugger : Form
    {
        Socket udpGate;
        public HexUpdate windowUpdate = null;
        Color mirClr = Color.PaleGreen;
        const string mainName = "RM Debugger";
        string ver;
        private readonly Dictionary<string, ConfigCheckList> fieldsDict = new Dictionary<string, ConfigCheckList>()
        {
            ["addr"] = ConfigCheckList.uInt16,
            ["fio"] = ConfigCheckList.len16,
            ["lamp"] = ConfigCheckList.len4,
            ["puid"] = ConfigCheckList.uInt16,
            ["rmb"] = ConfigCheckList.uInt16,
        };
        private int setTimerTest;
        private int realTimeWorkingTest = 0;

        public MainDebugger()
        {
            InitializeComponent();
            NotifyMessage.Text = this.Text = $"{Assembly.GetEntryAssembly().GetName().Name} {Assembly.GetEntryAssembly().GetName().Version}";
            AddEvents();
        }
        private void AddEvents()
        {
            Load += MainFormLoad;
            FormClosed += MainFormClosed;
            comPort.SelectedIndexChanged += (s, e) => mainPort.PortName = comPort.SelectedItem.ToString();
            BaudRate.SelectedIndexChanged += (s, e) => BaudRateSelectedIndexChanged(s, e);
            RefreshSerial.Click += (s, e) => AddPorts(comPort);
            foreach (ToolStripDropDownItem item in dataBits.DropDownItems) item.Click += dataBitsForSerial;
            foreach (ToolStripDropDownItem item in Parity.DropDownItems) item.Click += ParityForSerial;
            foreach (ToolStripDropDownItem item in stopBits.DropDownItems) item.Click += StopBitsForSerial;
            OpenCom.Click += OpenComClick;
            PingButton.Click += PingButtonClick;
            Connect.Click += ConnectClick;
        }



        //********************
        private void MainFormLoad(object sender, EventArgs e)
        {
            CheckUpdates();
            ComDefault();
            AddPorts(comPort);
            CheckReg();
            DefaultInfoGrid();
            DefaultConfigGrid();
            SetProperties();
        }
        //********************
        private void MainFormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.ThroughRM485 = NeedThrough.Checked;
            Settings.Default.MainSignatureID = (ushort)TargetSignID.Value;
            Settings.Default.ThroughSignatureID = (ushort)ThroughSignID.Value;
            Settings.Default.UDPGatePort = (ushort)numericPort.Value;
            Settings.Default.LastPageSize = (int)HexPageSize.Value;
            Settings.Default.UDPGateIP = IPaddressBox.Text;
            Settings.Default.LastPathToHex = HexPathBox.Text;
            Settings.Default.LastPortName = comPort.Text;
            Settings.Default.LastBaudRate = Int32.TryParse(BaudRate.Text, out int digit)
                ? digit
                : 38400;
            Settings.Default.Save();
        }

        async private void CheckUpdates()
        {
            if (Internet())
            {
                WebClient wc = new WebClient();
                ver = await wc.DownloadStringTaskAsync(
                    "https://drive.usercontent.google.com/download?id=1ip-kWdbtBA2Mpb1RAwTSdFMXD3MRHRvB&export=download&authuser=0&confirm=t&uuid=6096c5af-c408-4423-bffb-6b030dc50e09&at=APZUnTWfPp_1h-SJ001eKAdS_4Cf:1694263652561");
                Version curVer = Assembly.GetEntryAssembly().GetName().Version;
                if (Version.TryParse(ver, out Version driveVer) && driveVer.CompareTo(curVer) > 0)
                {
                    UpdateButton.Visible = true;
                    UpdateButton.ToolTipText = $"Доступна новая версия: {ver}";
                    NotifyMessage.BalloonTipTitle = "Обновление";
                    NotifyMessage.BalloonTipText = $"Доступна новая версия: {ver}";
                    NotifyMessage.ShowBalloonTip(10);
                }
            }
        }
        async private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, $"Доступна новая версия программы: {ver}\nПосле завершения обновления программа откроется самостоятельно",
                "Обновить?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                if (Internet())
                    await Task.Run(() =>
                    {
                        string exeFile = AppDomain.CurrentDomain.FriendlyName;
                        string exePath = Assembly.GetEntryAssembly().Location;
                        string newFile = "new" + exeFile;
                        WebClient wc = new WebClient();
                        wc.DownloadFile(
                                "https://drive.usercontent.google.com/download?id=1BpGT3HkD_YgYZDKbpFMuYx-Lwns8ZmZ0&export=download&authuser=0&confirm=t&uuid=e7a57cef-d2af-4976-b199-159b39b27d65&at=APZUnTVXMZcSBq-MHX2N0AfCypsi:1694263317080",
                                newFile);
                        CMD($"/c taskkill /f /im \"{exeFile}\" && " +
                            $"timeout /t 1 && " +
                            $"del \"{exePath}\" && " +
                            $"ren \"{newFile}\" \"{exeFile}\" &&" +
                            $"\"{exeFile}\"");
                    });
                else UpdateButton.Visible = false;
        }
        private void CMD(string cmd) => 
            Process.Start(new ProcessStartInfo { 
                FileName = "cmd.exe", 
                Arguments = cmd, 
                WindowStyle = ProcessWindowStyle.Hidden, 
            });
        private bool Internet()
        {
            try { Dns.GetHostEntry("drive.google.com");
                return true; }
            catch { return false; }
        }


        private void ComDefault()
        {
            mainPort.WriteTimeout =
                mainPort.ReadTimeout = 500;
            BaudRate.SelectedItem = "38400";
            dataBitsForSerial(dataBits8, null);
            ParityForSerial(ParityNone, null);
            StopBitsForSerial(stopBits1, null);
            BaudRateSelectedIndexChanged(null, null);
        }
        private void AddPorts(ComboBox box)
        {
            box.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length > 0)
            {
                box.Items.AddRange(ports);
                box.SelectedItem = box.Items[0];
            }
            OpenCom.Enabled = ports.Length > 0;
        }
        private void dataBitsForSerial(object sender, EventArgs e)
        {
            ToolStripMenuItem databits = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem item in dataBits.DropDownItems) item.CheckState = CheckState.Unchecked;
            mainPort.DataBits = Convert.ToInt32(databits.Text);
            databits.CheckState = CheckState.Checked;
            dataBitsInfo.Text = mainPort.DataBits.ToString();
        }
        private void ParityForSerial(object sender, EventArgs e)
        {
            ToolStripMenuItem parity = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem item in Parity.DropDownItems) item.CheckState = CheckState.Unchecked;
            switch (parity.Text)
            {
                case "None":
                    mainPort.Parity = System.IO.Ports.Parity.None;
                    break;
                case "Odd":
                    mainPort.Parity = System.IO.Ports.Parity.Odd;
                    break;
                case "Even":
                    mainPort.Parity = System.IO.Ports.Parity.Even;
                    break;
                case "Mark":
                    mainPort.Parity = System.IO.Ports.Parity.Mark;
                    break;
                case "Space":
                    mainPort.Parity = System.IO.Ports.Parity.Space;
                    break;
            }
            parity.CheckState = CheckState.Checked;
            ParityInfo.Text = mainPort.Parity.ToString();
        }
        private void StopBitsForSerial(object sender, EventArgs e)
        {
            ToolStripMenuItem stopbits = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem item in stopBits.DropDownItems) item.CheckState = CheckState.Unchecked;
            Enum.TryParse(Enum.GetName(typeof(StopBits), Convert.ToInt32(stopbits.Text)), out StopBits bits);
            mainPort.StopBits = bits;
            stopbits.CheckState = CheckState.Checked;
            StopBitsInfo.Text = stopbits.Text;
        }
        private void OpenComClick(object sender, EventArgs e)
        {
            if (OpenCom.Text == "Open")
            {
                try { mainPort.Open(); }
                catch (Exception ex) { ToInfoStatus(ex.Message); }
            }
            else mainPort.Close();
            OpenCom.Text = mainPort.IsOpen ? "Close" : "Open";
            AfterComEvent(mainPort.IsOpen);
            AfterAnyInterfaceEvent(mainPort.IsOpen);
        }
        private void AfterComEvent(bool sw)
            => comPort.Enabled = 
                RefreshSerial.Enabled = 
                UdpPage.Enabled = !sw;
        async private void PingButtonClick(object sender, EventArgs e)
        {
            if (PingButton.BackColor == Color.Green)
                PingButton.BackColor = Color.Red;
            else await check_ip();
        }
        async private Task check_ip()
        {
            int timeout = 250;
            using Ping ping = new Ping();
            byte[] buffer = new byte[32];
            PingOptions pingOptions = new PingOptions(buffer.Length, true);
            if (!IPAddress.TryParse(IPaddressBox.Text, out IPAddress ip)) return;
            PingReply reply = await ping.SendPingAsync(ip, timeout, buffer, pingOptions);
            if (reply.Status != IPStatus.Success) return;
            try
            {
                Invoke((MethodInvoker)(() => { Connect.Enabled = true; PingButton.BackColor = Color.Green; }));
                for (int reconnect = 0; reconnect < 5 && PingButton.BackColor == Color.Green;)
                {
                    reply = await ping.SendPingAsync(ip, timeout, buffer, pingOptions);
                    if (reply.Status != IPStatus.Success) reconnect++;
                    await Task.Delay(timeout);
                }
            }
            catch (Exception ex) { ToInfoStatus(ex.Message.ToString()); }
            finally
            {
                Invoke((MethodInvoker)(() => {
                    PingButton.BackColor = Color.Red;
                    if (udpGate.Connected) ConnectClick(null, null);
                    Connect.Enabled = PingButton.BackColor == Color.Green;
                }));
            }
        }
        private void ConnectClick(object sender, EventArgs e)
        {
            if (Connect.Text == "Connect")
            {
                udpGate = new Socket(SocketType.Dgram, ProtocolType.Udp);
                udpGate.Connect(IPaddressBox.Text, Convert.ToUInt16(numericPort.Text));
            }
            else
            {
                udpGate.Shutdown(SocketShutdown.Both);
                udpGate.Close();
            }
            Connect.Text = udpGate.Connected ? "Close" : "Connect";
            AfterUdpEvent(udpGate.Connected);
            AfterAnyInterfaceEvent(udpGate.Connected);
        }
        private void AfterUdpEvent(bool sw)
            => SerialPage.Enabled = !sw;
        private void AfterAnyInterfaceEvent(bool sw)
        {
            RMData.Enabled =
                    ExtraButtonsGroup.Enabled = sw;
            loadFromPCToolStripMenuItem.Enabled = 
                clearSettingsToolStrip.Enabled = !sw;
        }
        private string[] FileReader(string path)
        {
            using StreamReader file = new StreamReader(path);
            Task<string> data = file.ReadToEndAsync();
            return data.Result.Trim().Split('\n');
        }















        private byte[] GET_RM_NUMBER(NumericUpDown updn) => BitConverter.GetBytes(Convert.ToUInt16(updn.Value));
        private byte[] GET_RM_NUMBER(int num) => BitConverter.GetBytes(Convert.ToUInt16(num));
        private void TaskForChangedRows()
        {
            StartTestRMButton.Enabled =
            SaveLogTestRS485.Enabled =
            StatusRM485GridView.Rows.Count > 0;
            ToInfoStatus($"{StatusRM485GridView.Rows.Count} devices on RM Test.");
        }
        private void offTabsExcept(TabControl tab, TabPage exc)
        {
            Action action = () =>
            {
                foreach (TabPage t in tab.TabPages)
                {
                    if (t == exc)
                    {
                        t.Text = $"↓{t.Text}↓";
                        continue;
                    }
                    t.Enabled = false;
                }
                BaudRate.Enabled = false;
                dataBits.Enabled = false;
                Parity.Enabled = false;
                stopBits.Enabled = false;
                if (ExtraButtonsGroup.Enabled)
                    ExtraButtonsGroup.Enabled = false;
            };
            if (InvokeRequired) BeginInvoke(action);
            else action();
        }
        private void onTabPages(TabControl tab)
        {
            Action action = () =>
            {
                foreach (TabPage page in tab.TabPages)
                {
                    if (page.Enabled == false)
                        page.Enabled = true;
                    page.Text = page.Text.Replace("↓", "");
                }
            };
            if (InvokeRequired) BeginInvoke(action);
            else action();
        }
        private bool CheckInsidePaths(string path) => HexPathBox.Items.Contains(path);
        private void BackToDefaults()
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                if (!RMData.Enabled) RMData.Enabled = true;
                HexUploadButton.Text = "Upload";
                HexUploadButton.Image = Resources.StatusRunning;
                AutoDistTof.Text = "Auto";
                AutoDistTof.Image = Resources.StatusRunning;
                AutoDistTof.Enabled = true;
                AutoGetNear.Text = "Auto";
                AutoGetNear.Image = Resources.StatusRunning;
                AutoGetNear.Enabled = true;
                ScanTestRM.Text = "Manual Scan";
                UpdateBar.Value = 0;
                BytesStart.Text = 0.ToString();
                onTabPages(RMData);
                onTabPages(TestPages);
                GetNearPage.Text = "Get Near";
                DistTofPage.Text = "Dist Tof";
                ManualGetNear.Enabled = true;
                ManualDistTof.Enabled = true;
                HexPathBox.Enabled = true;
                HexPageSize.Enabled = true;
                HexPathButton.Enabled = true;
                OpenCom.Enabled = true;
                Connect.Enabled = true;
                HexUploadButton.Enabled = true;
                SerUdpPages.Enabled = true;
                AutoScanTestRM.Enabled = true;
                StatusRM485GridView.Enabled = true;
                StatusRM485GridView.AllowUserToDeleteRows = true;
                ClearDataTestRS485.Enabled = true;
                SignaturePanel.Enabled = true;
                NeedThrough.Enabled = true;
                ClearInfoTestRS485.Enabled = true;
                RMPTimeout.Enabled = true;
                AddSigTestRM.Enabled = true;
                InfoTree.Enabled = true;
                ExtraButtonsGroup.Enabled = true;
                ButtonsPanel.Enabled = true;
                AutoScanTestRM.Enabled = true;
                scanGroupBox.Enabled = true;
                settingsGroupBox.Enabled = true;
                ClearDataTestRS485.Enabled = true;
                timerPanelTest.Enabled = true;
                minSigToScan.Enabled = true;
                maxSigToScan.Enabled = true;
                ConfigDataGrid.Enabled = true;
                WorkTestTimer.Stop();
                BaudRate.Enabled = true;
                dataBits.Enabled = true;
                Parity.Enabled = true;
                stopBits.Enabled = true;
                if (RMPStatusBar.Value != (int)RMPTimeout.Value)
                {
                    RMPStatusBar.Value = (int)RMPTimeout.Value;
                    timerLabel.Text = RMPStatusBar.Value.ToString();
                }
                if (windowUpdate != null)
                {
                    windowUpdate.Enabled = true;
                    HexUpdatePage.Enabled = false;
                }
                else
                {
                    HexUpdateInAWindow.Enabled = true;
                }
                ThroughOrNot(NeedThrough.Checked);
                timerRmp.Stop();
            }));
        }
        private void SetProperties()
        {
            Invoke((MethodInvoker)(() =>
            {
                NeedThrough.Checked = Settings.Default.ThroughRM485;
                TargetSignID.Value = Settings.Default.MainSignatureID;
                ThroughSignID.Value = Settings.Default.ThroughSignatureID;
                numericPort.Value = Settings.Default.UDPGatePort;
                HexPageSize.Value = Settings.Default.LastPageSize;
                IPaddressBox.Text = Settings.Default.UDPGateIP;
                HexPathBox.Items.Add(Settings.Default.LastPathToHex);
                if (HexPathBox.Items.Count != 0) HexPathBox.SelectedItem = HexPathBox.Items[0];
                ThroughOrNot(NeedThrough.Checked);
                TypeFilterBox.SelectedIndex = 0;
                if (Settings.Default.LastPortName != string.Empty && comPort.Items.Contains(Settings.Default.LastPortName))
                    comPort.Text = Settings.Default.LastPortName;
                BaudRate.Text = Settings.Default.LastBaudRate.ToString();
            }));

        }
        private void ToInfoStatus(string msg) => BeginInvoke((MethodInvoker)(() => { InfoStatus.Text = msg; }));
        private void BaudRateSelectedIndexChanged(object sender, EventArgs e)
            => mainPort.BaudRate = Convert.ToInt32(BaudRate.SelectedItem);

        private void DefaultConfigGrid()
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                foreach (string key in fieldsDict.Keys)
                {
                    int row = ConfigDataGrid.Rows.Add(key);
                    ConfigDataGrid.Rows[row].Cells[0].ReadOnly = true;
                    ConfigDataGrid.Rows[row].Cells[3].ReadOnly = false;
                    ConfigDataGrid.Rows[row].Cells[3].ToolTipText = GetDescription(fieldsDict[key]);
                }
                ConfigDataGrid.Rows.Add();
                ConfigDataGrid.Rows[ConfigDataGrid.Rows.Count - 1].Cells[1].ReadOnly = true;
                ConfigDataGrid.Rows[ConfigDataGrid.Rows.Count - 1].Cells[3].ReadOnly = true;
            }));
        }
        private string GetDescription(ConfigCheckList ccl)
        {
            switch (ccl)
            {
                case ConfigCheckList.uInt16: return "0-65535\n";
                case ConfigCheckList.len4: return "4 any symbols length\n";
                case ConfigCheckList.len16: return "16 any symbols length\n";
            }
            return null;
        }
        private void DefaultInfoGrid()
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                InfoFieldsGrid.Rows.Clear();
                foreach (string data in Enum.GetNames(typeof(InfoGrid))) InfoFieldsGrid.Rows.Add(data);
                foreach (TreeNode node in InfoTree.Nodes) node.Nodes.Clear();
            }));
        }
        
        private void BaudRate_SelectedIndexChanged(object sender, EventArgs e) => mainPort.BaudRate = Convert.ToInt32(BaudRate.Text);
        private void IPaddressBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                IPAddress addr = IPAddress.Parse(IPaddressBox.Text);
                PingButton.Enabled = true;
                ErrorMessage.Clear();
            }
            catch (Exception ex)
            {
                PingButton.Enabled = false;
                ErrorMessage.SetError(label13, ex.Message);
            }
            finally
            {
                PingButton.BackColor = Color.Red;
                Connect.Text = "Connect";
                Connect.Enabled = false;
            }
        }
        private void numericPort_ValueChanged(object sender, EventArgs e)
        {
            PingButton.BackColor = Color.Red;
            Connect.Text = "Connect";
        }

        private void DistTofTimeout_Scroll(object sender, EventArgs e) => Invoke((MethodInvoker)(() => { TimeForDistTof.Text = DistToftimeout.Value.ToString() + " ms"; }));
        private void GetNearTimeout_Scroll(object sender, EventArgs e) => Invoke((MethodInvoker)(() => { TimeForGetNear.Text = GetNeartimeout.Value.ToString() + " ms"; }));
        async private void ManualDistTof_Click(object sender, EventArgs e) => await Task.Run(() => AsyncDistTof());
        async private void ManualGetNear_Click(object sender, EventArgs e) => await Task.Run(() => AsyncGetNear());
        private void AutoDistTof_Click(object sender, EventArgs e)
        {
            if (AutoDistTof.Text == "Stop")
            {
                AutoDistTof.Enabled = false;
                AutoDistTof.Text = "Auto";
            }
            else
            {
                AutoDistTof.Text = "Stop";
                AutoDistTof.Image = Resources.StatusStopped;
                ManualDistTof_Click(null, null);
            }
        }
        private void AutoGetNear_Click(object sender, EventArgs e)
        {
            if (AutoGetNear.Text == "Stop")
            {
                AutoGetNear.Enabled = false;
                AutoGetNear.Text = "Auto";
            }
            else
            {
                AutoGetNear.Text = "Stop";
                AutoGetNear.Image = Resources.StatusStopped;
                ManualGetNear_Click(null, null);
            }
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
                    HexPathBox.Items.Add(file.FileName);
                HexPathBox.SelectedItem = file.FileName;
            }
        }
        private void Hex_Box_DragDrop(object sender, DragEventArgs e)
        {
            string[] FilePath = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string path in FilePath)
            {
                if (CheckInsidePaths(path)) continue;
                if (new FileInfo(path).Extension == ".hex") HexPathBox.Items.Add(path);
            }
            HexPathBox.SelectedItem = FilePath[0];
        }
        private void Hex_Box_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
        private void Hex_Box_TextChanged(object sender, EventArgs e)
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                if (File.Exists(HexPathBox.Text))
                {
                    HexUploadFilename.Text = $"Filename: {Path.GetFileName(HexPathBox.Text)}";
                    HexUploadButton.Enabled = true;
                    string[] len = FileReader(HexPathBox.Text);
                    UpdateBar.Maximum = len.Length;
                    BytesEnd.Text = len.Length.ToString();
                    UpdateBar.Value = 0;
                    BytesStart.Text = 0.ToString();
                }
                else
                {
                    HexUploadButton.Enabled = false;
                    HexUploadFilename.Text = string.Empty;
                }
            }));
        }
        private void TargetSignID_ValueChanged(object sender, EventArgs e) => NeedThrough.Enabled = TargetSignID.Value == 0 ? false : true;
        async private void Upload_button_Click(object sender, EventArgs e)
        {
            if (HexUploadButton.Text == "Stop")
                HexUploadButton.Text = "Upload";
            else
            {
                offTabsExcept(RMData, HexUpdatePage);
                HexUploadButton.Text = "Stop";
                HexUploadButton.Image = Resources.StatusStopped;
                if (windowUpdate == null) HexUpdateInAWindow.Enabled = false;
                await Task.Run(() => UploadDevice(new Bootloader(mainPort, udpGate)));
            }
        }
        async private void RefreshRMButton_Click(object sender, EventArgs e)
        {
            if (windowUpdate != null) windowUpdate.Enabled = false;
            await Task.Run(() => AddToStatusGrid(new ForTests(mainPort, udpGate)));
        }
        
        private void StatusGridView_DoubleClick(object sender, EventArgs e) => StatusRM485GridView.ClearSelection();
        private void StatusGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) => TaskForChangedRows();
        private void StatusGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) => TaskForChangedRows();
        private void GetNearGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) => ToInfoStatus($"{GetNearGrid.Rows.Count} devices on Get Near.");
        private void GetNearGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) => ToInfoStatus($"{GetNearGrid.Rows.Count} devices on Get Near.");
        private void DistTofGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) => ToInfoStatus($"{DistTofGrid.Rows.Count} devices on Dist tof.");
        private void DistTofGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) => ToInfoStatus($"{DistTofGrid.Rows.Count} devices on Dist tof.");
        private void ClearDataStatusRM_Click(object sender, EventArgs e)
        {
            StatusRM485GridView.Rows.Clear();
            realTimeWorkingTest = 0;
            ChangeWorkTestTime(realTimeWorkingTest);
        }
        private void ThroughOrNot(bool through)
        {
            BeginInvoke((MethodInvoker)(() => {
                MirrorBox.Enabled = !through;
                MirrorColorButton.Enabled = !through;
                ThroughSignID.Enabled = through;
                RS485Page.Enabled = !through;
                ExtFind.Enabled = !through;
            }));
        }
        private void NeedThrough_CheckedChanged(object sender, EventArgs e)
        {
            ThroughOrNot(NeedThrough.Checked);

            decimal targetID;

            if (NeedThrough.Checked)
            {
                targetID = ThroughSignID.Value;
                ThroughSignID.Value = TargetSignID.Value;
                TargetSignID.Value = targetID;
            }
            else
            {
                targetID = TargetSignID.Value;
                TargetSignID.Value = ThroughSignID.Value;
                ThroughSignID.Value = targetID;
            }
        }
        private void AboutButton_Click(object sender, EventArgs e) => new AboutInfo().ShowDialog();
        async private Task<Tuple<List<int>, Dictionary<int, int>>> GetMoreDevices(Searching search, Dictionary<int, int> data)
        {
            Dictionary<int, int> extData = new Dictionary<int, int>();
            List<int> exception = new List<int>();
            search.AddKeys(extData, data);
            int devices = 0;
            Tuple<byte[], ProtocolReply> replyes;
            foreach (int key in data.Keys)
            {
                try
                {
                    replyes = await search.GetData(search.FormatCmdOut(GET_RM_NUMBER(key), CmdOutput.STATUS, 0xff), (int)CmdMaxSize.STATUS, 50);
                    Dictionary<int, int> tempData = new Dictionary<int, int>();
                    tempData = await GetDeviceListInfo(search, CmdOutput.GRAPH_GET_NEAR, GET_RM_NUMBER(key));
                    search.AddKeys(extData, tempData);
                    devices++;
                }
                catch { exception.Add(key); continue; }
            }
            data = extData.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            return new Tuple<List<int>, Dictionary<int, int>>(exception, data);
        }
        private Dictionary<int, int> RemoveOnType(Dictionary<int, int> data)
        {
            if (TypeFilterBox.SelectedItem.ToString() == "<Any>") return data;
            Dictionary<int, int> newData = new Dictionary<int, int>();
            foreach (int key in data.Keys)
            {
                DevType type = (DevType)data[key];
                if (type.ToString() == TypeFilterBox.SelectedItem.ToString())
                    newData.Add(key, data[key]);
            }
            return newData;
        }
        async private Task AsyncGetNear()
        {
            if (!udpGate.Connected && !mainPort.IsOpen) return;
            BeginInvoke((MethodInvoker)(() => {
                SerUdpPages.Enabled = false;
                ManualGetNear.Enabled = false;
                if (AutoGetNear.Text == "Auto") AutoGetNear.Enabled = false;
                if (windowUpdate != null) windowUpdate.Enabled = false;
            }));
            offTabsExcept(RMData, GetNearPage);

            Searching search = new Searching(mainPort, udpGate);

            do
            {
                if (!udpGate.Connected && !mainPort.IsOpen && PingButton.BackColor == Color.Red) break;
                Dictionary<int, int> data = await GetDeviceListInfo(search, CmdOutput.GRAPH_GET_NEAR, GET_RM_NUMBER(TargetSignID));
                BeginInvoke((MethodInvoker)(() => { GetNearGrid.Rows.Clear(); }));
                if (data != null)
                {
                    List<int> exception = new List<int>();

                    if (ExtFind.Checked == true && ExtFind.Enabled)
                    {
                        Tuple<List<int>, Dictionary<int, int>> temp = await GetMoreDevices(search, data);
                        exception = temp.Item1;
                        data = temp.Item2;
                    }

                    data = RemoveOnType(data);

                    foreach (int key in data.Keys)
                    {
                        BeginInvoke((MethodInvoker)(() => { GetNearGrid.ClearSelection(); }));
                        if (key == (int)TargetSignID.Value) continue;
                        if (MirrorBox.Checked && MirrorBox.Enabled)
                        {
                            Dictionary<int, int> mirror = null;
                            try { mirror = await GetDeviceListInfo(search, CmdOutput.GRAPH_GET_NEAR, GET_RM_NUMBER(key)); }
                            catch
                            {
                                BeginInvoke((MethodInvoker)(() => { GetNearGrid.Rows.Add($"{key}", (DevType)data[key]); }));
                                continue;
                            }
                            if (mirror != null && !exception.Contains(key))
                            {
                                if (mirror.ContainsKey((int)TargetSignID.Value))
                                {
                                    BeginInvoke((MethodInvoker)(() =>
                                    {
                                        int row = GetNearGrid.Rows.Add(key, (DevType)data[key]);
                                        GetNearGrid.Rows[row].DefaultCellStyle.BackColor = mirClr;
                                    }));
                                    continue;
                                }
                            }
                        }
                        BeginInvoke((MethodInvoker)(() => { GetNearGrid.Rows.Add($"{key}", (DevType)data[key]); }));
                    }
                }
                if (KnockBox.Checked && data != null && data.Count != 0 && AutoGetNear.Text == "Stop")
                {
                    NotifyMessage.BalloonTipTitle = "Тук-тук!";
                    NotifyMessage.BalloonTipText = $"Ответ получен!";
                    NotifyMessage.ShowBalloonTip(10);
                    break;
                }
                await Task.Delay(AutoGetNear.Text == "Stop" ? GetNeartimeout.Value : 50);
            }
            while (AutoGetNear.Text == "Stop");
            BackToDefaults();
        }
        async private Task AsyncDistTof()
        {
            if (!udpGate.Connected && !mainPort.IsOpen) return;
            BeginInvoke((MethodInvoker)(() => {
                SerUdpPages.Enabled = false;
                ManualDistTof.Enabled = false;
                if (AutoDistTof.Text == "Auto") AutoDistTof.Enabled = false;
                if (windowUpdate != null) windowUpdate.Enabled = false;
            }));
            offTabsExcept(RMData, DistTofPage);

            Searching search = new Searching(mainPort, udpGate);
            do
            {
                if (!udpGate.Connected && !mainPort.IsOpen && PingButton.BackColor == Color.Red) break;
                Dictionary<int, int> data = await GetDeviceListInfo(search, CmdOutput.ONLINE_DIST_TOF, GET_RM_NUMBER(TargetSignID));
                BeginInvoke((MethodInvoker)(() => { DistTofGrid.Rows.Clear(); }));

                if (data != null)
                    foreach (int key in data.Keys)
                        BeginInvoke((MethodInvoker)(() => { DistTofGrid.Rows.Add(key, data[key]); }));
                await Task.Delay(AutoDistTof.Text == "Stop" ? DistToftimeout.Value : 50);
            }
            while (AutoDistTof.Text == "Stop");
            BackToDefaults();
        }
        async private Task<Dictionary<int, int>> GetDeviceListInfo(Searching search, CmdOutput cmdOutput, byte[] rmSign)
        {
            byte ix = 0x00;
            int iteration = 1;
            byte[] rmThrough = GET_RM_NUMBER(ThroughSignID);
            bool through = NeedThrough.Checked;
            Dictionary<int, int> dataReturn = new Dictionary<int, int>();
            try
            {
                do
                {
                    Tuple<byte, Dictionary<int, int>> data = await search.RequestAndParseNew(cmdOutput, ix, rmSign, rmThrough, through) 
                        ?? throw new Exception(ProtocolReply.Null.ToString());
                    ix = data.Item1;
                    iteration++;
                    search.AddKeys(dataReturn, data.Item2);
                }
                while (ix != 0x00 && iteration <= 5);
            }
            catch (Exception ex) { ToInfoStatus(ex.Message); }
            return dataReturn.Count == 0
                ? null
                : dataReturn.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }
        private byte[] GetCmdThroughOrNot(Bootloader boot, byte[] rmSign, byte[] rmThrough, CmdOutput cmdOutput)
            => !NeedThrough.Checked ? boot.SendCommand(rmSign, cmdOutput) : boot.SendCommand(rmSign, rmThrough, cmdOutput);
        private byte[] GetCmdThroughOrNot(Bootloader boot, byte[] rmSign, byte[] rmThrough, byte[] data)
            => !NeedThrough.Checked ? boot.SendCommand(rmSign, data) : boot.SendCommand(rmSign, rmThrough, data);
        async private Task<bool> GetRequestUpload(Bootloader boot, byte[] cmdOut, bool uploadData = false, int aWait = 100, bool delay = false, int delayMs = 25)
        {
            Tuple<byte[], ProtocolReply> replyes = null;
            DateTime t0 = DateTime.Now;
            TimeSpan tstop = DateTime.Now - t0;
            do
            {
                if (HexUploadButton.Text == "Upload")
                {
                    BackToDefaults();
                    return false;
                }
                try
                {
                    replyes = await boot.GetData(cmdOut, cmdOut.Length, aWait);
                    if (uploadData)
                    {
                        ProtocolReply reply = Methods.GetDataReply(cmdOut, replyes.Item1, NeedThrough.Checked);
                        ToInfoStatus(reply.ToString());
                        if (reply == ProtocolReply.Ok) break;
                    }
                    else
                    {
                        ToInfoStatus(replyes.Item2.ToString());
                        if (replyes.Item2 == ProtocolReply.Ok) break;
                    }
                }
                catch (Exception ex)
                {
                    ToInfoStatus(ex.Message);
                    tstop = DateTime.Now - t0;
                    if (tstop.Seconds >= 20)
                    {
                        DialogResult message = MessageBox.Show(this, "Timeout", "Something wrong...", MessageBoxButtons.RetryCancel);
                        if (message == DialogResult.Cancel)
                        {
                            BackToDefaults();
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
        async private Task UploadDevice(Bootloader boot)
        {
            byte[][] hex;
            try { hex = boot.GetByteDataFromFile(HexPathBox.Text); }
            catch (Exception ex)
            {
                ToInfoStatus(ex.Message);
                BackToDefaults();
                return;
            }
            Invoke((MethodInvoker)(() =>
            {
                HexPathBox.Enabled = false;
                HexPageSize.Enabled = false;
                HexPathButton.Enabled = false;
                SignaturePanel.Enabled = false;
            }));
            byte[] rmSign = GET_RM_NUMBER(TargetSignID);
            byte[] rmThrough = GET_RM_NUMBER(ThroughSignID);

            byte[] cmdBootStart = GetCmdThroughOrNot(boot, rmSign, rmThrough, CmdOutput.START_BOOTLOADER);
            if (!await GetRequestUpload(boot, cmdBootStart, uploadData: false, aWait: 50, delay: true, delayMs: 25)) return;
            BeginInvoke((MethodInvoker)(() => { SerUdpPages.Enabled = false; }));
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
                    if (!await GetRequestUpload(boot, cmdLoadData, uploadData: true, aWait: 200)) return;
                    if (!await GetRequestUpload(boot, cmdUpdateData, delay: true, delayMs: 50)) return;
                    BeginInvoke((MethodInvoker)(() => { UpdateBar.Value = i; BytesStart.Text = i.ToString(); }));
                }
            }
            if (!await GetRequestUpload(boot, cmdBootStop, delay: true, delayMs: 25)) return;
            tstop = DateTime.Now - t0;
            BeginInvoke((MethodInvoker)(() => {
                InfoStatus.Text = $"Uploaded for {tstop.Minutes}:{tstop.Seconds}:{tstop.Milliseconds}";
                NotifyMessage.BalloonTipTitle = "Прошивка устройства";
                NotifyMessage.BalloonTipText = $"Файл {Path.GetFileName(HexPathBox.Text)} успешно загружен на устройство за " +
                $"{tstop.Minutes}:{tstop.Seconds}:{tstop.Milliseconds}";
                NotifyMessage.ShowBalloonTip(10);
            }));

            BackToDefaults();
        }
        private void ClearStatusStatusRM_Click(object sender, EventArgs e)
            => BeginInvoke((MethodInvoker)(() =>
            {
                for (int r = 0; r < StatusRM485GridView.Rows.Count; r++)
                {
                    for (int i = 3; i < 11; i++) StatusRM485GridView[i, r].Value = 0;
                    StatusRM485GridView[(int)RS485Columns.Status, r].Value = "-";
                    StatusRM485GridView.Rows[r].DefaultCellStyle.BackColor = Color.White;
                }
                realTimeWorkingTest = 0;
                ChangeWorkTestTime(realTimeWorkingTest);
            }));
        private void FillGridResults(int code, int index)
        {
            Invoke((MethodInvoker)(() =>
            {
                StatusRM485GridView[(int)RS485Columns.Tx, index].Value = Convert.ToInt32(StatusRM485GridView[(int)RS485Columns.Tx, index].Value) + 1;
                switch (code)
                {
                    case 1:
                        {
                            StatusRM485GridView[(int)RS485Columns.NoReply, index].Value = Convert.ToInt32(StatusRM485GridView[(int)RS485Columns.NoReply, index].Value) + 1;
                            break;
                        }
                    case 2:
                        {
                            StatusRM485GridView[(int)RS485Columns.BadCrc, index].Value = Convert.ToInt32(StatusRM485GridView[(int)RS485Columns.BadCrc, index].Value) + 1;
                            break;
                        }
                    case 3:
                        {
                            StatusRM485GridView[(int)RS485Columns.BadReply, index].Value = Convert.ToInt32(StatusRM485GridView[(int)RS485Columns.BadReply, index].Value) + 1;
                            break;
                        }
                    case 4:
                        {
                            StatusRM485GridView[(int)RS485Columns.BadRadio, index].Value = Convert.ToInt32(StatusRM485GridView[(int)RS485Columns.BadRadio, index].Value) + 1;
                            break;
                        }
                    case 10:
                        {
                            StatusRM485GridView[(int)RS485Columns.Rx, index].Value = Convert.ToInt32(StatusRM485GridView[(int)RS485Columns.Rx, index].Value) + 1;
                            break;
                        }
                }

                int Errors = Convert.ToInt32(StatusRM485GridView[(int)RS485Columns.NoReply, index].Value) +
                    Convert.ToInt32(StatusRM485GridView[(int)RS485Columns.BadCrc, index].Value) +
                    Convert.ToInt32(StatusRM485GridView[(int)RS485Columns.BadReply, index].Value) +
                    Convert.ToInt32(StatusRM485GridView[(int)RS485Columns.BadRadio, index].Value);

                StatusRM485GridView[(int)RS485Columns.Errors, index].Value = Errors;
                int txGrid = Convert.ToInt32(StatusRM485GridView[(int)RS485Columns.Tx, index].Value);
                int rxGrid = Convert.ToInt32(StatusRM485GridView[(int)RS485Columns.Rx, index].Value);

                StatusRM485GridView[(int)RS485Columns.PercentErrors, index].Value = 100.00 - (100.00 * rxGrid / txGrid);

                double PercentErrors = Convert.ToDouble(StatusRM485GridView[(int)RS485Columns.PercentErrors, index].Value);

                Color statusColor = GetErrorColor(PercentErrors);
                StatusRM485GridView.Rows[index].DefaultCellStyle.BackColor = statusColor;
                if (statusColor == Color.White) StatusRM485GridView[(int)RS485Columns.Status, index].Value = "Good";
                else StatusRM485GridView[(int)RS485Columns.Status, index].Value = "Bad";
            }));
        }
        private Color GetErrorColor(double percent)
        {
            if (percent >= 5) return Color.Red;
            else if (percent >= 3) return Color.Yellow;
            else if (percent >= 1) return Color.GreenYellow;
            else return Color.White;
        }
        private bool CheckButtonAndTime()
        {
            if (StartTestRMButton.Text == "&Start Test") return false;
            if (TimerTestBox.Checked && setTimerTest == 0)
            {
                StartTestRMButton.Text = "&Start Test";
                if (GetMessageTestBox.Checked)
                {
                    NotifyMessage.BalloonTipTitle = "Время истекло!";
                    NotifyMessage.BalloonTipText = $"Время истекло, тест завершен";
                    NotifyMessage.ShowBalloonTip(10);
                }
                return false;
            }
            return true;
        }


        async private void StartStatusRMTestButton_Click(object sender, EventArgs e)
        {
            if (StartTestRMButton.Text == "&Stop Test")
            {
                StartTestRMButton.Text = "&Start Test";
                StartTestRMButton.Enabled = false;
            }
            else
            {
                StartTestRMButton.Text = "&Stop Test";
                StartTestRMButton.Image = Resources.StatusStopped;
                if (windowUpdate != null) windowUpdate.Enabled = false;
                await Task.Run(() => RMStatusTest());
            }
        }

        async private void RMStatusTest()
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                StatusRM485GridView.ClearSelection();
                StatusRM485GridView.AllowUserToDeleteRows = false;
                SerUdpPages.Enabled = false;
                SignaturePanel.Enabled = false;
                AutoScanTestRM.Enabled = false;
                scanGroupBox.Enabled = false;
                settingsGroupBox.Enabled = false;
                ClearDataTestRS485.Enabled = false;
                timerPanelTest.Enabled = false;
                WorkTestTimer.Start();
            }));
            offTabsExcept(RMData, TestPage);
            offTabsExcept(TestPages, RS485Page);

            if (TimerTestBox.Checked)
                setTimerTest = (int)new TimeSpan(
                    (int)numericDaysTest.Value,
                    (int)numericHoursTest.Value,
                    (int)numericMinutesTest.Value,
                    (int)numericSecondsTest.Value).TotalSeconds;

            Dictionary<string, Dictionary<int, Tuple<int, int, DevType>>> dataFromGrid = GetGridInfo();

            List<Task> tasks = new List<Task>();
            foreach(string devInterface in dataFromGrid.Keys)
            {
                Task task;
                if (UInt16.TryParse(devInterface, out ushort port))
                {
                    if (!udpGate.Connected || (udpGate.Connected && (ushort)((IPEndPoint)udpGate.RemoteEndPoint).Port != port))
                    {
                        task = SocketForRMTest(port, dataFromGrid[devInterface]);
                        tasks.Add(task);
                        continue;
                    }
                }
                else
                {
                    if (!mainPort.IsOpen || (mainPort.IsOpen && mainPort.PortName != devInterface))
                    {
                        task = SerialForRMTest(devInterface, dataFromGrid[devInterface]);
                        tasks.Add(task);
                        continue;
                    }
                }
                task = Task.Run(() => RMStatusTestTask(new ForTests(mainPort, udpGate), dataFromGrid[devInterface]));
                tasks.Add(task);
            }
            ToInfoStatus($"Devices on test: {StatusRM485GridView.Rows.Count}");
            await Task.WhenAll(tasks);
            StartTestRMButton.Text = "&Start Test";
            StartTestRMButton.Image = Resources.StatusRunning;
            StartTestRMButton.Enabled = true;
            BackToDefaults();
        }

        async private Task SerialForRMTest(string portName, Dictionary<int, Tuple<int, int, DevType>> data)
        {
            try
            {
                using (SerialPort port = new SerialPort(
                portName, mainPort.BaudRate, mainPort.Parity,
                mainPort.DataBits, mainPort.StopBits))
                {
                    port.Open();
                    await RMStatusTestTask(new ForTests(port, new Socket(SocketType.Dgram, ProtocolType.Udp)), data);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        async private Task SocketForRMTest(ushort port, Dictionary<int, Tuple<int, int, DevType>> data)
        {
            try
            {
                using (Socket sock = new Socket(SocketType.Dgram, ProtocolType.Udp))
                if (IPAddress.TryParse(IPaddressBox.Text, out IPAddress address))
                {
                    sock.Connect(address, port);
                    await RMStatusTestTask(new ForTests(new SerialPort(), sock), data);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        async private Task RMStatusTestTask(ForTests test, Dictionary<int, Tuple<int, int, DevType>> dataGrid)
        {
            do
            {
                for (int i = 0; i < 5; i++)
                {
                    foreach (int key in dataGrid.Keys)
                    {
                        if (!CheckButtonAndTime()) return;
                        byte[] rmSign = GET_RM_NUMBER(key);
                        Enum.TryParse(StatusRM485GridView[(int)RS485Columns.Type, dataGrid[key].Item1].Value.ToString(), out DevType devType);
                        List<int> replyCodes;

                        if (BufferTestBox.Checked)
                        {
                            if (test.Sock.Connected) Methods.FlushBuffer(test.Sock);
                            if (test.Port.IsOpen) Methods.FlushBuffer(test.Port);
                        }
                        
                        switch (i)
                        {
                            case 0:
                                FillGridResults(await test.RS485Test(rmSign, CmdOutput.WHO_ARE_YOU, devType), dataGrid[key].Item1);
                                break;
                            case 1:
                                if (RadioCheckTestBox.Checked)
                                {
                                    replyCodes = await test.RadioTest(rmSign, CmdOutput.GRAPH_GET_NEAR, dataGrid);
                                    foreach (int code in replyCodes)
                                        FillGridResults(code, dataGrid[key].Item1);
                                }
                                break;
                            case 2:
                                FillGridResults(await test.RS485Test(rmSign, CmdOutput.ROUTING_GET, devType), dataGrid[key].Item1);
                                break;
                            case 3:
                                if (RadioCheckTestBox.Checked)
                                {
                                    replyCodes = await test.RadioTest(rmSign, CmdOutput.ONLINE_DIST_TOF, dataGrid);
                                    foreach (int code in replyCodes)
                                        FillGridResults(code, dataGrid[key].Item1);
                                }
                                break;
                            case 4:
                                Tuple<int, TimeSpan?, int?> data = await test.GetWorkTimeAndVersion(rmSign, devType);
                                FillGridResults(data.Item1, dataGrid[key].Item1);
                                if (data.Item2 is null || data.Item3 is null) break;
                                else
                                {
                                    TimeSpan time = (TimeSpan)data.Item2;
                                    Invoke((MethodInvoker)(() =>
                                    {
                                        StatusRM485GridView[(int)RS485Columns.WorkTime, dataGrid[key].Item1].Value =
                                        $"{time.Days}d " +
                                        $"{time.Hours}h " +
                                        $": {time.Minutes}m " +
                                        $": {time.Seconds}s";
                                        StatusRM485GridView[(int)RS485Columns.Version, dataGrid[key].Item1].Value = data.Item3;
                                    }));
                                }
                                break;
                        }
                    }
                }
            }
            while (StartTestRMButton.Text == "&Stop Test"
                && (test.Sock.Connected || test.Port.IsOpen)
                && CheckButtonAndTime());
        }
        private Dictionary<string, Dictionary<int, Tuple<int, int, DevType>>> GetGridInfo()
        {
            // interface :
            // Sig : 
            // <index, ver, type>
            Dictionary<string, Dictionary<int, Tuple<int, int, DevType>>> _data = new Dictionary<string, Dictionary<int, Tuple<int, int, DevType>>>();
            if (StatusRM485GridView.Rows.Count == 0) return null;

            for (int i = 0; i < StatusRM485GridView.Rows.Count; i++)
                _data[StatusRM485GridView[(int)RS485Columns.Interface, i].Value.ToString()] = new Dictionary<int, Tuple<int, int, DevType>>();

            try
            {
                for (int i = 0; i < StatusRM485GridView.Rows.Count; i++)
                {
                    if (Enum.TryParse(StatusRM485GridView[(int)RS485Columns.Type, i].Value.ToString(), out DevType devType))
                    {
                        Dictionary<int, Tuple<int, int, DevType>> extData = new Dictionary<int, Tuple<int, int, DevType>>()
                        {
                            [Convert.ToUInt16(StatusRM485GridView[(int)RS485Columns.Sign, i].Value)] = new Tuple<int, int, DevType>
                            (i, Convert.ToInt32(StatusRM485GridView[(int)RS485Columns.Version, i].Value), devType)
                        };
                        if(_data.ContainsKey(StatusRM485GridView[(int)RS485Columns.Interface, i].Value.ToString()))
                        {
                            _data[StatusRM485GridView[(int)RS485Columns.Interface, i].Value.ToString()]
                                .Add(Convert.ToUInt16(StatusRM485GridView[(int)RS485Columns.Sign, i].Value), 
                                new Tuple<int, int, DevType> (i, Convert.ToInt32(StatusRM485GridView[(int)RS485Columns.Version, i].Value), devType));
                        }
                        else _data.Add(StatusRM485GridView[(int)RS485Columns.Interface, i].Value.ToString(), extData);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString());}
            
            return _data;
        }
        private async Task<Dictionary<string, Dictionary<int, Tuple<int, DevType>>>> GetDevicesInfo(ForTests search, string devInterface, Dictionary<int, int> data)
        {
            Dictionary<string, Dictionary<int, Tuple<int, DevType>>> _data = new Dictionary<string, Dictionary<int, Tuple<int, DevType>>>();
            Dictionary<int, Tuple<int, DevType>> extData = new Dictionary<int, Tuple<int, DevType>>();
            //sign, <ver, type>
            foreach (int key in data.Keys)
            {
                Tuple<byte[], ProtocolReply> replyes;
                byte[] cmdOut = search.FormatCmdOut(GET_RM_NUMBER(key), CmdOutput.STATUS, 0xff);
                try
                {
                    replyes = await search.GetData(cmdOut, (int)CmdMaxSize.STATUS);
                    extData.Add(
                        key,
                        new Tuple<int, DevType>(
                            search.GetVersion(replyes.Item1),
                            search.GetType(replyes.Item1)));
                }
                catch { }
            }
            if (extData.Count > 0)
            {
                _data.Add(devInterface, extData);
                return _data;
            }
            return null;
        }
        async private Task AddToStatusGrid(ForTests search)
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                SerUdpPages.Enabled = false;
                RS485Page.Enabled = false;
                SignaturePanel.Enabled = false;
            }));
            offTabsExcept(RMData, TestPage);
            offTabsExcept(TestPages, RS485Page);

            Dictionary<int, int> data = await GetDeviceListInfo(search, CmdOutput.GRAPH_GET_NEAR, GET_RM_NUMBER(TargetSignID));
            if (data is null)
            {
                BackToDefaults();
                return;
            }

            string devInterface;
            if (mainPort.IsOpen) devInterface = mainPort.PortName;
            else if (udpGate.Connected) devInterface = ((IPEndPoint)udpGate.RemoteEndPoint).Port.ToString();
            else devInterface = "None";

            data.Add((int)TargetSignID.Value, 1);

            Dictionary<string, Dictionary<int, Tuple<int, DevType>>> dataPassed = await GetDevicesInfo(search, devInterface, data);
            Dictionary<string, Dictionary<int, Tuple<int, int, DevType>>> dataFromGrid = GetGridInfo();

            int devicesAdded = 0;

            foreach (int key in dataPassed[devInterface].Keys)
            {
                if (dataFromGrid != null && dataFromGrid.ContainsKey(devInterface) && dataFromGrid[devInterface].ContainsKey(key)) continue;
                else AddToGridTest(devInterface, key, dataPassed[devInterface][key].Item2, dataPassed[devInterface][key].Item1);
                devicesAdded++;
            }
            ToInfoStatus($"Added:{devicesAdded}");
            BackToDefaults();
        }
        async private Task AddRangeToStatusGrid(ForTests search)
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                SerUdpPages.Enabled = false;
                StartTestRMButton.Enabled = false;
                AutoScanTestRM.Enabled = false;
                ClearDataTestRS485.Enabled = false;
                settingsGroupBox.Enabled = false;
                AddSigTestRM.Enabled = false;
                minSigToScan.Enabled = false;
                maxSigToScan.Enabled = false;
                StartTestRMButton.Enabled = false;
                SignaturePanel.Enabled = false;
            }));
            offTabsExcept(RMData, TestPage);
            offTabsExcept(TestPages, RS485Page);

            string devInterface;
            if (mainPort.IsOpen) devInterface = mainPort.PortName;
            else if (udpGate.Connected) devInterface = ((IPEndPoint)udpGate.RemoteEndPoint).Port.ToString();
            else devInterface = "None";

            Dictionary<string, Dictionary<int, Tuple<int, int, DevType>>> dataFromGrid = GetGridInfo();
            for (int i = (int)minSigToScan.Value; i <= maxSigToScan.Value; i++)
            {
                if (ScanTestRM.Text == "Manual Scan") break;
                Tuple<byte[], ProtocolReply> replyes = null;
                try
                {
                    ToInfoStatus($"Signature: {i}");
                    replyes = await search.GetData(
                        search.FormatCmdOut(GET_RM_NUMBER(i),
                        CmdOutput.STATUS, 0xff), (int)CmdMaxSize.STATUS, 25);
                    if (dataFromGrid is null || !dataFromGrid.ContainsKey(devInterface) || !dataFromGrid[devInterface].ContainsKey(i))
                        AddToGridTest(devInterface, i,
                        search.GetType(replyes.Item1),
                        search.GetVersion(replyes.Item1));
                }
                catch { }
            }
            BackToDefaults();
            TaskForChangedRows();
        }
        async private void AddSigTestRM_Click(object sender, EventArgs e)
        {
            string devInterface;
            if (mainPort.IsOpen) devInterface = mainPort.PortName;
            else if (udpGate.Connected) devInterface = ((IPEndPoint)udpGate.RemoteEndPoint).Port.ToString();
            else devInterface = "None";
            Dictionary<string, Dictionary<int, Tuple<int, int, DevType>>> dataFromGrid = GetGridInfo();
            if (dataFromGrid is null || !dataFromGrid.ContainsKey(devInterface) || !dataFromGrid[devInterface].ContainsKey((int)TargetSignID.Value))
            {
                Searching search = new Searching(mainPort, udpGate);
                byte[] cmdOut = search.FormatCmdOut(GET_RM_NUMBER(TargetSignID), CmdOutput.STATUS, 0xff);
                Tuple<byte[], ProtocolReply> replyes = null;
                try
                {
                    replyes = await search.GetData(cmdOut, (int)CmdMaxSize.STATUS, 50);
                    AddToGridTest(devInterface, (int)TargetSignID.Value,
                        search.GetType(replyes.Item1),
                        search.GetVersion(replyes.Item1));
                }
                catch (Exception ex) { ToInfoStatus(ex.Message); }
            }
        }

        private void AddToGridTest(string devInterface, int signature, DevType type, int version) =>
            Invoke((MethodInvoker)(() => { StatusRM485GridView.Rows.Add(
                devInterface, signature, type, "-", 0, 0, 0, 0, 0, 0, 0, 0, 0, version);
            }));

        private void HexUpdateInAWindow_Click(object sender, EventArgs e)
        {
            windowUpdate = new HexUpdate(this, udpGate, mainPort);
            HexUpdatePage.Enabled = false;
            windowUpdate.Show();
        }
        private void MirrorColorButton_Click(object sender, EventArgs e)
        {
            if (MirrorColor.ShowDialog() == DialogResult.OK)
                mirClr = MirrorColor.Color;
        }
        private void clearSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Reset();
            SetProperties();
            Settings.Default.Save();
        }
        private void saveToRegToolStripMenuItem_Click(object sender, EventArgs e) => new WriteHere(this).ShowDialog();
        public bool checkMainFolder(RegistryKey cuKey) => cuKey.GetSubKeyNames().Contains(mainName);
        public bool checkInMainFolder(RegistryKey rKey, string subKey) => rKey.GetSubKeyNames().Contains(subKey);
        private void loadSaveFrom(object sender, EventArgs e)
        {
            bool removeError = false;
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            RegistryKey currentUserKey = Registry.CurrentUser;
            if (checkMainFolder(currentUserKey))
            {
                RegistryKey rKey = currentUserKey.OpenSubKey(mainName, true);
                if (checkInMainFolder(rKey, item.Text))
                {
                    RegistryKey sKey = rKey.OpenSubKey(item.Text, true);
                    if (!RegEditLoadParams(sKey)) removeError = true;
                    sKey.Close();
                    rKey.Close();
                    if (removeError) RegEditDeleteParams(currentUserKey, item);
                }
            }
            currentUserKey.Close();
        }
        private bool RegEditLoadParams(RegistryKey rKey)
        {
            try
            {
                Invoke((MethodInvoker)(() => {
                    NeedThrough.Checked = Convert.ToBoolean(rKey.GetValue("ThroughRM485"));
                    TargetSignID.Value = Convert.ToUInt16(rKey.GetValue("MainSignatureID"));
                    HexPageSize.Value = Convert.ToInt32(rKey.GetValue("LastPageSize"));
                    IPaddressBox.Text = rKey.GetValue("UDPGateIP").ToString();
                    numericPort.Value = Convert.ToUInt16(rKey.GetValue("UDPGatePort"));
                    HexPathBox.Text = rKey.GetValue("LastPathToHex").ToString();
                    ThroughSignID.Value = Convert.ToUInt16(rKey.GetValue("ThroughSignatureID"));
                    comPort.Text = rKey.GetValue("LastComPort").ToString();
                    BaudRate.Text = rKey.GetValue("LastBaudrate").ToString();
                }));
            }
            catch
            {
                if (MessageBox.Show(this, $"Неверный формат одного или нескольких значений.\nУдалить настройку?", "Format Error",
                    MessageBoxButtons.YesNo) == DialogResult.Yes) return false;
            }
            return true;
        }
        private void deleteSaveFrom(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            RegistryKey currentUserKey = Registry.CurrentUser;
            if (checkMainFolder(currentUserKey))
            {
                RegistryKey rKey = currentUserKey.OpenSubKey(mainName, true);
                if (checkInMainFolder(rKey, item.Text))
                    RegEditDeleteParams(rKey, item);
                if (rKey.GetSubKeyNames().Length == 0)
                {
                    loadFromPCToolStripMenuItem.Visible = false;
                    deleteSaveFromPCToolStripMenuItem.Visible = false;
                }
            }
            currentUserKey.Close();
        }
        private void RegEditDeleteParams(RegistryKey cuKey, ToolStripMenuItem item)
        {
            try
            {
                Invoke((MethodInvoker)(() => {
                    this.Enabled = false;
                    for (int i = 0; i < loadFromPCToolStripMenuItem.DropDownItems.Count; i++)
                        if (loadFromPCToolStripMenuItem.DropDownItems[i].Text == item.Text)
                        {
                            loadFromPCToolStripMenuItem.DropDownItems.RemoveAt(i);
                            break;
                        }
                    for (int i = 0; i < deleteSaveFromPCToolStripMenuItem.DropDownItems.Count; i++)
                        if (deleteSaveFromPCToolStripMenuItem.DropDownItems[i].Text == item.Text)
                        {
                            deleteSaveFromPCToolStripMenuItem.DropDownItems.RemoveAt(i);
                            break;
                        }
                    this.Enabled = true;
                }));
                cuKey.DeleteSubKey(item.Text);
            }
            catch
            {
                MessageBox.Show(this, $"Ошибка при удалении сохранения");
                this.Enabled = true;
            }
        }
        private bool checkInStripMenuItems(ToolStripItemCollection collection, string subKey)
        {
            foreach (ToolStripMenuItem item in collection)
                if (item.Text == subKey) return true;
            return false;
        }
        public void AddNewEvents()
        {
            Invoke((MethodInvoker)(() => {
                foreach (ToolStripMenuItem item in loadFromPCToolStripMenuItem.DropDownItems)
                    item.Click += new EventHandler(loadSaveFrom);
                foreach (ToolStripMenuItem item in deleteSaveFromPCToolStripMenuItem.DropDownItems)
                    item.Click += new EventHandler(deleteSaveFrom);
            }));
        }
        public void AddSettingsToStrip(string subKey)
        {
            if (!checkInStripMenuItems(loadFromPCToolStripMenuItem.DropDownItems, subKey) &&
                !checkInStripMenuItems(deleteSaveFromPCToolStripMenuItem.DropDownItems, subKey))
            {
                Invoke((MethodInvoker)(() =>
                {
                    loadFromPCToolStripMenuItem.Visible = true;
                    loadFromPCToolStripMenuItem.DropDownItems.Add(subKey);
                    deleteSaveFromPCToolStripMenuItem.Visible = true;
                    deleteSaveFromPCToolStripMenuItem.DropDownItems.Add(subKey);
                }));
            }
        }
        private void CheckReg()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            if (checkMainFolder(currentUserKey))
            {
                RegistryKey rKey = currentUserKey.OpenSubKey(mainName, true);
                string[] keys = rKey.GetSubKeyNames();
                if (keys.Length > 0)
                {
                    this.Enabled = false;
                    foreach (string key in keys) AddSettingsToStrip(key);
                    AddNewEvents();
                }
                else
                {
                    loadFromPCToolStripMenuItem.Visible = false;
                    deleteSaveFromPCToolStripMenuItem.Visible = false;
                }
                rKey.Close();
                this.Enabled = true;
            }
            currentUserKey.Close();
        }
        private void timerRmp_Tick(object sender, EventArgs e)
        {
            if (RMPStatusBar.Value != 0) BeginInvoke((MethodInvoker)(() => {
                RMPStatusBar.Value -= 1;
                timerLabel.Text = RMPStatusBar.Value.ToString();
            }));
        }
        private void RMPTimeout_ValueChanged(object sender, EventArgs e) {
            RMPStatusBar.Value =  RMPStatusBar.Maximum = (int)RMPTimeout.Value;
            timerLabel.Text = RMPTimeout.Value.ToString();
        }
        private void NotifyMessage_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Normal;
        private void PinButton_Click(object sender, EventArgs e)
        {
            this.TopMost = PinButton.Checked;
            PinButton.ToolTipText = PinButton.Checked ? "Поверх других окон." : "Обычное состояние окна.";
            PinButton.Image = PinButton.Checked ? Resources.Pin : Resources.Unpin;
        }
        async private void InfoTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Dictionary<string, CmdOutput> dict = new Dictionary<string, CmdOutput>()
            {
                ["WhoAreYouInfo"] = CmdOutput.WHO_ARE_YOU,
                ["StatusInfo"] = CmdOutput.STATUS,
                ["GetNearInfo"] = CmdOutput.GRAPH_GET_NEAR,
            };
            try { TargetSignID.Value = Convert.ToUInt16(e.Node.Text); }
            catch { }
            if (!dict.ContainsKey(e.Node.Name)) return;
            Information info = new Information(mainPort, udpGate);
            Invoke((MethodInvoker)(() => {
                if (mainPort.IsOpen) Methods.FlushBuffer(mainPort);
                if (udpGate.Connected) Methods.FlushBuffer(udpGate);
                SerUdpPages.Enabled = false;
                InfoTree.Enabled = false;
                e.Node.Nodes.Clear();
            }));
            offTabsExcept(RMData, InfoPage);
            Tuple<byte[], ProtocolReply> reply;

            CmdMaxSize cmdSize;
            Enum.TryParse(Enum.GetName(typeof(CmdOutput), dict[e.Node.Name]), out cmdSize);
            int size = !NeedThrough.Checked ? (int)cmdSize : (int)cmdSize + 6;

            try
            {
                if (dict[e.Node.Name] == CmdOutput.GRAPH_GET_NEAR)
                {
                    Dictionary<int, int> data = await GetDeviceListInfo(info, dict[e.Node.Name], GET_RM_NUMBER(TargetSignID));
                    BeginInvoke((MethodInvoker)(() => {
                        string radio = data is null || data.Count == 0 ? "Error" : "Ok";
                        TreeNode getnear = new TreeNode($"Radio: {radio}");
                        InfoFieldsGrid.Rows[(int)InfoGrid.Radio].Cells[1].Value = radio;
                        e.Node.Nodes.Add(getnear);
                        Dictionary<string, List<int>> newData = new Dictionary<string, List<int>>();
                        if (radio != "Error")
                            foreach (int key in data.Keys)
                            {
                                if (!newData.ContainsKey($"{(DevType)data[key]}")) newData[$"{(DevType)data[key]}"] = new List<int>();
                                newData[$"{(DevType)data[key]}"].Add(key);
                            }
                        foreach (string key in newData.Keys)
                        {
                            TreeNode type = new TreeNode($"{key}: {newData[key].Count}");
                            getnear.Nodes.Add(type);
                            foreach (int i in newData[key])
                            {
                                TreeNode signature = new TreeNode($"{i}");
                                type.Nodes.Add(signature);
                                signature.ToolTipText = $"Нажмите на сигнатуру {i}, что бы опросить";
                            }
                        }
                        e.Node.ExpandAll();
                    }));
                }
                else
                {
                    byte[] cmdOut = !NeedThrough.Checked ? info.GetInfo(GET_RM_NUMBER(TargetSignID), dict[e.Node.Name]) :
                    info.GetInfo(GET_RM_NUMBER(TargetSignID), GET_RM_NUMBER(ThroughSignID), dict[e.Node.Name]);
                    reply = await info.GetData(cmdOut, size, 100);
                    byte[] cmdIn = !NeedThrough.Checked ? reply.Item1 : info.ReturnWithoutThrough(reply.Item1);
                    Dictionary<string, string> data = info.CmdInParse(cmdIn);
                    ToInfoStatus(reply.Item2.ToString());
                    BeginInvoke((MethodInvoker)(() => {
                        data.Add("Date", DateTime.Now.ToString("dd-MM-yy HH:mm"));
                        foreach (string str in data.Keys)
                        {
                            e.Node.Nodes.Add($"{str}: {data[str]}");
                            if (Enum.GetNames(typeof(InfoGrid)).Contains(str))
                                InfoFieldsGrid.Rows[(int)Enum.Parse(typeof(InfoGrid), str)].Cells[1].Value = data[str];
                        }
                        e.Node.Expand();
                    }));
                }
            }
            catch (Exception ex) { ToInfoStatus(ex.Message); }
            finally { BackToDefaults(); }
        }
        private void OpenCloseMenuInfoTree_Click(object sender, EventArgs e)
        {
            if (OpenCloseMenuInfoTree.Text == "<")
            {
                OpenCloseMenuInfoTree.Text = ">";
                InfoTreePanel.Location = new Point(InfoTreePanel.Location.X - 162, InfoTreePanel.Location.Y);
            }
            else
            {
                OpenCloseMenuInfoTree.Text = "<";
                InfoTreePanel.Location = new Point(InfoTreePanel.Location.X + 162, InfoTreePanel.Location.Y);
            }
        }
        private void InfoClearGrid_Click(object sender, EventArgs e) => DefaultInfoGrid();
        private void InfoSaveToCSVButton_Click(object sender, EventArgs e)
        {
            if ((string)InfoFieldsGrid.Rows[0].Cells[1].Value == string.Empty) return;
            string path = Environment.CurrentDirectory + $"\\InformationAboutDevice.csv";
            CSVLib csv = new CSVLib(path);
            string columns = "";
            string[] columnsEnum = Enum.GetNames(typeof(InfoGrid));
            foreach (string column in columnsEnum)
                columns += column != "Date" ? $"{column};" : column;
            csv.AddFields(columns);
            csv.MainIndexes = new int[] { 0, 1, columnsEnum.Length - 1 };
            List<string> list = new List<string>();
            for (int i = 0; i < InfoFieldsGrid.Rows.Count; i++)
                list.Add((string)InfoFieldsGrid.Rows[i].Cells[1].Value);
            csv.WriteCsv(string.Join(";", list));
        }
        async private void SetOnlineButton_Click(object sender, EventArgs e) =>
             await Task.Run(() => SendCommandFromButton(new CommandsOutput(mainPort, udpGate), CmdOutput.ONLINE));
        async private void ResetButton_Click(object sender, EventArgs e) =>
             await Task.Run(() => SendCommandFromButton(new CommandsOutput(mainPort, udpGate), CmdOutput.RESET));
        async private void SetBootloaderStartButton_Click(object sender, EventArgs e) =>
             await Task.Run(() => SendCommandFromButton(new CommandsOutput(mainPort, udpGate), CmdOutput.START_BOOTLOADER));
        async private void SetBootloaderStopButton_Click(object sender, EventArgs e) =>
             await Task.Run(() => SendCommandFromButton(new CommandsOutput(mainPort, udpGate), CmdOutput.STOP_BOOTLOADER));
        async private Task SendCommandFromButton(CommandsOutput CO, CmdOutput cmdOutput)
        {
            byte[] cmdOut;
            switch (cmdOutput)
            {
                case CmdOutput.ONLINE:
                    {
                        cmdOut = CO.FormatCmdOut(GET_RM_NUMBER(TargetSignID), cmdOutput, (byte)SetOnlineFreqNumeric.Value);
                        if (NeedThrough.Checked) cmdOut = CO.CmdThroughRm(cmdOut, GET_RM_NUMBER(ThroughSignID), CmdOutput.ROUTING_THROUGH);
                        break;
                    }
                case CmdOutput.START_BOOTLOADER:
                case CmdOutput.STOP_BOOTLOADER:
                    {
                        cmdOut = CO.FormatCmdOut(GET_RM_NUMBER(TargetSignID), cmdOutput, 0xff);
                        if (NeedThrough.Checked) cmdOut = CO.CmdThroughRm(cmdOut, GET_RM_NUMBER(ThroughSignID), CmdOutput.ROUTING_PROG);
                        break;
                    }
                default:
                    {
                        cmdOut = CO.FormatCmdOut(GET_RM_NUMBER(TargetSignID), cmdOutput, 0xff);
                        if (NeedThrough.Checked) cmdOut = CO.CmdThroughRm(cmdOut, GET_RM_NUMBER(ThroughSignID), CmdOutput.ROUTING_THROUGH);
                        break;
                    }
            }
            BeginInvoke((MethodInvoker)(() =>
            {
                SerUdpPages.Enabled = false;
                ButtonsPanel.Enabled = false;
                SignaturePanel.Enabled = false;
                RMData.Enabled = false;
            }));
            Tuple<byte[], ProtocolReply> reply = null;
            CmdMaxSize cmdSize;
            Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOutput), out cmdSize);
            int size = !NeedThrough.Checked ? (int)cmdSize : (int)cmdSize + 6;
            do
            {
                try
                {
                    reply = await CO.GetData(cmdOut, size, 50);
                    ToInfoStatus($"{reply.Item2}");
                    if (cmdOutput == CmdOutput.ONLINE
                        && Methods.CheckResult(reply.Item1) != RmResult.Ok)
                    {
                        ToInfoStatus($"{Methods.CheckResult(reply.Item1)}");
                        continue;
                    }

                    NotifyMessage.BalloonTipTitle = "Кнопка отработана";
                    NotifyMessage.BalloonTipText = $"Успешно отправлена команда {cmdOutput}";
                    NotifyMessage.ShowBalloonTip(10);

                    break;
                }
                catch (Exception ex) { ToInfoStatus(ex.Message); }
                finally { await Task.Delay((int)AutoExtraButtonsTimeout.Value); }
            }
            while (AutoExtraButtons.Checked);
            BackToDefaults();
        }
        private void PasswordBox_TextChanged(object sender, EventArgs e)
            => BeginInvoke((MethodInvoker)(() => {
                ResetButton.Visible =
                SetBootloaderStopButton.Visible =
                SetBootloaderStartButton.Visible = PasswordBox.Text == "198237645"; }));
        private void HexUploadFilename_DoubleClick(object sender, EventArgs e) => PasswordBox.Visible = !PasswordBox.Visible;
        private void minSigToScan_ValueChanged(object sender, EventArgs e)
            => BeginInvoke((MethodInvoker)(() => {
                maxSigToScan.Minimum = minSigToScan.Value;
                if (minSigToScan.Value >= maxSigToScan.Value)
                    maxSigToScan.Value = minSigToScan.Value;
            }));
        async private void ScanTestRM_Click(object sender, EventArgs e)
        {
            if (ScanTestRM.Text == "Cancel") ScanTestRM.Text = "Manual Scan";
            else
            {
                ScanTestRM.Text = "Cancel";
                if (windowUpdate != null) windowUpdate.Enabled = false;
                await Task.Run(() => AddRangeToStatusGrid(new ForTests(mainPort, udpGate)));
            }
        }
        private ConfigCheckList GetFieldDict(string key) => fieldsDict.ContainsKey(key) ? fieldsDict[key] : ConfigCheckList.None;
        private void ConfigDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int emptyRow = e.RowIndex + 1;
            string field = (string)ConfigDataGrid[(int)ConfigColumns.fColumn, e.RowIndex].Value;
            string value = (string)ConfigDataGrid[(int)ConfigColumns.fUpload, e.RowIndex].Value;
            if (field is null) return;
            switch (e.ColumnIndex)
            {
                case (int)ConfigColumns.fColumn: //field column
                    {
                        ConfigDataGrid.Rows[e.RowIndex].Cells[0].ReadOnly = true;
                        ConfigDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = false;
                        ConfigDataGrid.Rows[e.RowIndex].Cells[3].ReadOnly = false;
                        if (fieldsDict.ContainsKey(field))
                            ConfigDataGrid.Rows[e.RowIndex].Cells[3].ToolTipText = GetDescription(fieldsDict[field]);
                        ConfigDataGrid.Rows.Add();
                        ConfigDataGrid.Rows[emptyRow].Cells[1].ReadOnly = true;
                        ConfigDataGrid.Rows[emptyRow].Cells[3].ReadOnly = true;
                        break;
                    }
                /*case (int)ConfigColumns.enabled: //enabled
                    break;*/
                case (int)ConfigColumns.fUpload: //upload
                    {
                        try
                        {
                            ConfigCheckList eValue = GetFieldDict(field);
                            if (value != null)
                                switch (eValue)
                                {
                                    case ConfigCheckList.uInt16:
                                        {
                                            Convert.ToUInt16(value);
                                            break;
                                        }
                                    case ConfigCheckList.len4:
                                    case ConfigCheckList.len16:
                                        {
                                            if (value.Length > (int)eValue)
                                            {
                                                string newValue = "";
                                                for (int i = 0; i < (int)eValue; i++) newValue += value[i];
                                                ConfigDataGrid[(int)ConfigColumns.fUpload, e.RowIndex].Value = newValue;
                                            }
                                            break;
                                        }
                                }
                            ConfigDataGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                            ConfigDataGrid.Rows[e.RowIndex].Cells[(int)ConfigColumns.enabled].ReadOnly = false;
                        }
                        catch
                        {
                            ConfigDataGrid[1, e.RowIndex].Value = false;
                            ConfigDataGrid.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                            ConfigDataGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                        }
                        break;
                    }
            }
        }
        private void GetNearGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
            => TargetSignID.Value = Convert.ToDecimal(GetNearGrid[0, e.RowIndex].Value);
        async private void LoadConfigButton_Click(object sender, EventArgs e)
        {
            if (windowUpdate != null) windowUpdate.Enabled = false;
            Dictionary<string, int> dict = GetEnabledFields();
            if (dict is null) return;
            offTabsExcept(RMData, null);
            OffControlsForConfig();
            await Task.Run(() => LoadField(new Configuration(mainPort, udpGate), dict));
            BackToDefaults();
        }
        async private void UploadConfigButton_Click(object sender, EventArgs e)
        {
            if (windowUpdate != null) windowUpdate.Enabled = false;
            Dictionary<string, int> dict = GetEnabledFields();
            if (dict is null) return;
            offTabsExcept(RMData, null);
            OffControlsForConfig();
            await Task.Run(() => UploadField(new Configuration(mainPort, udpGate), dict));
            BackToDefaults();
        }
        private Dictionary<string, int> GetEnabledFields()
        {
            Dictionary<string, int> fields = new Dictionary<string, int>();
            for (int i = 0; i < ConfigDataGrid.Rows.Count; i++)
                if (ConfigDataGrid[(int)ConfigColumns.fColumn, i].Value != null &&
                    Convert.ToBoolean(ConfigDataGrid[(int)ConfigColumns.enabled, i].Value) == true &&
                    !fields.ContainsKey((string)ConfigDataGrid[(int)ConfigColumns.fColumn, i].Value))
                    fields.Add((string)ConfigDataGrid[(int)ConfigColumns.fColumn, i].Value, i);
            return fields.Count > 0 ? fields : null;
        }
        private void OffControlsForConfig()
            => BeginInvoke((MethodInvoker)(() =>
            {
                SerUdpPages.Enabled = false;
                SignaturePanel.Enabled = false;
                timerRmp.Start();
                RMPTimeout.Enabled = false;
            }));
        async private Task LoadField(Configuration config, Dictionary<string, int> fields)
        {
            DateTime t0 = DateTime.Now;
            TimeSpan tstop = DateTime.Now - t0;
            foreach (string key in fields.Keys)
            {
                byte[] cmdOut = NeedThrough.Checked ?
                    config.ConfigLoad(GET_RM_NUMBER(TargetSignID), GET_RM_NUMBER(ThroughSignID), key) :
                    config.ConfigLoad(GET_RM_NUMBER(TargetSignID), key);

                int valueLength = fieldsDict.ContainsKey(key) ? (int)fieldsDict[key] + 1 : 17;
                int fieldLength = key.Length + 1;
                int dataCount = valueLength + fieldLength + 6;
                dataCount = NeedThrough.Checked ? dataCount + 4 : dataCount;

                Tuple<byte[], ProtocolReply> replyes = null;
                byte[] cmdIn = null;

                while (tstop.Seconds < RMPTimeout.Value)
                {
                    tstop = DateTime.Now - t0;
                    try
                    {
                        replyes = await config.GetData(cmdOut, dataCount);
                        cmdIn = replyes.Item1;
                        ToInfoStatus($"{key} : {replyes.Item2}");
                        break;
                    }
                    catch (Exception ex)
                    {
                        ToInfoStatus(ex.Message);
                        if (ex.Message == "No interface")
                        {
                            BackToDefaults();
                            return;
                        }
                    } finally { await Task.Delay(50); }
                }
                try
                {
                    byte[] data = new byte[cmdIn.Length - (NeedThrough.Checked ? 10 : 6) - fieldLength];
                    Array.Copy(cmdIn, 9 + key.Length, data, 0, data.Length);
                    BeginInvoke((MethodInvoker)(() => {
                        ConfigDataGrid[(int)ConfigColumns.fLoad, fields[key]].Value = Methods.CheckSymbols(data);
                        ColoredRow(fields[key], ConfigDataGrid, Color.GreenYellow);
                    }));
                }
                catch { BeginInvoke((MethodInvoker)(() => {
                    ConfigDataGrid[(int)ConfigColumns.fLoad, fields[key]].Value = "Error";
                    ColoredRow(fields[key], ConfigDataGrid, Color.Red);
                })); }
            }
        }
        async private Task UploadField(Configuration config, Dictionary<string, int> fields)
        {
            DateTime t0 = DateTime.Now;
            TimeSpan tstop = DateTime.Now - t0;
            bool factory = ConfigFactoryCheck.Checked;
            foreach (string key in fields.Keys)
            {
                string value = (string)ConfigDataGrid[(int)ConfigColumns.fUpload, fields[key]].Value;
                int maxSize = fieldsDict.ContainsKey(key) ? (int)fieldsDict[key] + 1 : 17;

                if (factory)
                    if (key == "addr") continue;
                if (value is null && !factory) continue;
                byte[] cmdOut = NeedThrough.Checked ?
                    config.ConfigUploadNew(GET_RM_NUMBER(TargetSignID), GET_RM_NUMBER(ThroughSignID), key, value, maxSize, factory) :
                    config.ConfigUploadNew(GET_RM_NUMBER(TargetSignID), key, value, maxSize, factory);

                Tuple<RmResult, ProtocolReply> replyes = null;

                while (tstop.Seconds < RMPTimeout.Value)
                {
                    tstop = DateTime.Now - t0;
                    try
                    {
                        replyes = await config.GetResult(cmdOut, NeedThrough.Checked ? (int)CmdMaxSize.ONLINE + 4 : (int)CmdMaxSize.ONLINE);
                        ToInfoStatus($"{key} : {replyes.Item1}");
                        if (replyes.Item1 == RmResult.Ok)
                        {
                            ColoredRow(fields[key], ConfigDataGrid, Color.GreenYellow);
                            if (key == "addr")
                                Invoke((MethodInvoker)(() => {
                                    TargetSignID.Value = Convert.ToUInt16(ConfigDataGrid[(int)ConfigColumns.fUpload, fields[key]].Value);
                                }));
                            break;
                        }
                        else ColoredRow(fields[key], ConfigDataGrid, Color.Red);
                    }
                    catch (Exception ex)
                    {
                        ToInfoStatus(ex.Message);
                        if (ex.Message == "No interface")
                        {
                            BackToDefaults();
                            return;
                        }
                    } finally { await Task.Delay(50); }
                }
                if (tstop.Seconds >= RMPTimeout.Value)
                    ColoredRow(fields[key], ConfigDataGrid, Color.Red);
            }
        }
        private void ColoredRow(int index, DataGridView dgv, Color color)
            => Invoke((MethodInvoker)(async () => {
                dgv.Rows[index].DefaultCellStyle.BackColor = color;
                await Task.Delay(500);
                dgv.Rows[index].DefaultCellStyle.BackColor = Color.White;
            }));
        private void ConfigDataGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
            => e.Cancel = ConfigDataGrid[0, e.Row.Index].Value is null;
        private void ClearGridButton_Click(object sender, EventArgs e)
            => BeginInvoke((MethodInvoker)(() => {
                for (int i = 0; i < ConfigDataGrid.Rows.Count - 1; i++)
                    if (Convert.ToBoolean(ConfigDataGrid[(int)ConfigColumns.enabled, i].Value))
                    {
                        ConfigDataGrid[(int)ConfigColumns.fLoad, i].Value = null;
                        ConfigDataGrid[(int)ConfigColumns.fUpload, i].Value = null;
                    }
            }));
        private void ShowExtendedMenu_Click(object sender, EventArgs e)
        {
            if (ShowExtendedMenu.Text == "Show &menu")
            {
                ShowExtendedMenu.Text = "Hide &menu";
                extendedMenuPanel.Location = new Point(extendedMenuPanel.Location.X, extendedMenuPanel.Location.Y - 147);
                ShowExtendedMenu.Image = Resources.Unhide;
                ToolTipHelper.SetToolTip(ShowExtendedMenu, "Скрыть расширенное меню");
            }
            else
            {
                ShowExtendedMenu.Text = "Show &menu";
                extendedMenuPanel.Location = new Point(extendedMenuPanel.Location.X, extendedMenuPanel.Location.Y + 147);
                ShowExtendedMenu.Image = Resources.Hide;
                ToolTipHelper.SetToolTip(ShowExtendedMenu, "Показать расширенное меню");
            }
        }
        private void WorkTestTimer_Tick(object sender, EventArgs e)
        {
            setTimerTest -= 1;
            realTimeWorkingTest += 1;
            ChangeWorkTestTime(realTimeWorkingTest);
        }

        private void ChangeWorkTestTime(int seconds) 
            => BeginInvoke((MethodInvoker)(() => {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            WorkingTimeLabel.Text = $"{time.Days}d " +
                                    $"{time.Hours}h " +
                                    $": {time.Minutes}m " +
                                    $": {time.Seconds}s";
        }));

        private void SaveLogTestRS485_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            File.WriteAllText(saveFileDialog.FileName, GetLogFromTestRM485());
        }
        private string GetLogFromTestRM485()
        {
            if (StatusRM485GridView.Rows.Count > 0)
            {
                DateTime time = DateTime.Now;
                int tX = 0;
                int rX = 0;
                int errors = 0;
                for (int i = 0; i < StatusRM485GridView.Rows.Count; i++)
                {
                    tX += (int)StatusRM485GridView[(int)RS485Columns.Tx, i].Value;
                    rX += (int)StatusRM485GridView[(int)RS485Columns.Rx, i].Value;
                    errors += (int)StatusRM485GridView[(int)RS485Columns.Errors, i].Value;
                }
                string log =
                    $"Время создания лога\t\t\t{time}\n" +
                    $"Общее время тестирования\t\t{WorkingTimeLabel.Text}\n\n" +
                    $"\nОбщее количество отправленных пакетов: {tX}." +
                    $"\nОбщее количество принятых пакетов: {rX}." +
                    $"\nОбщее количество ошибок: {errors}\n\n";


                //"Испытуемые: sig, sig, sig... sig"
                Dictionary<ushort, int> signDataIndex = new Dictionary<ushort, int>();
                for (int i = 0; i < StatusRM485GridView.Rows.Count; i++)
                    signDataIndex[Convert.ToUInt16(StatusRM485GridView[(int)RS485Columns.Sign, i].Value)] = i;

                signDataIndex = signDataIndex.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                log += $"Количество испытуемых: {signDataIndex.Count}\n";
                log += $"Сигнатуры испытуемых: {String.Join(", ", signDataIndex.Keys)}.\n\n";

                if (errors > 0)
                    foreach (ushort key in signDataIndex.Keys)
                    {
                        int i = signDataIndex[key];
                        if ((int)StatusRM485GridView[(int)RS485Columns.Errors, i].Value > 0)
                        {
                            ushort sign = Convert.ToUInt16(StatusRM485GridView[(int)RS485Columns.Sign, i].Value);
                            string type = Convert.ToString(StatusRM485GridView[(int)RS485Columns.Type, i].Value);
                            int noReply = (int)StatusRM485GridView[(int)RS485Columns.NoReply, i].Value;
                            int badReply = (int)StatusRM485GridView[(int)RS485Columns.BadReply, i].Value;
                            int badCRC = (int)StatusRM485GridView[(int)RS485Columns.BadCrc, i].Value;
                            int badRadio = (int)StatusRM485GridView[(int)RS485Columns.BadRadio, i].Value;
                            log += $"Информация об устройстве типа {type} с сигнатурой {sign}:\n" +
                                   $"Состояние: {StatusRM485GridView[(int)RS485Columns.Status, i].Value}\n" +
                                   $"Версия прошивки устройства: {StatusRM485GridView[(int)RS485Columns.Version, i].Value}\n"+
                                   $"Количество отправленных пакетов устройству: {Convert.ToInt32(StatusRM485GridView[(int)RS485Columns.Tx, i].Value)}\n" +
                                   $"Количество принятых пакетов от устройства: {Convert.ToInt32(StatusRM485GridView[(int)RS485Columns.Rx, i].Value)}\n";
                            log += noReply > 0 ? $"Пропустил опрос: {noReply}\n" : "";
                            log += badReply > 0 ? $"Проблемы с ответом: {badReply}\n" : "";
                            log += badCRC > 0 ? $"Проблемы с CRC: {badCRC}\n" : "";
                            log += badRadio > 0 ? $"Проблемы с Radio: {badRadio}\n" : "";
                            log += $"Процент ошибок: {Math.Round(Convert.ToDouble(StatusRM485GridView[(int)RS485Columns.PercentErrors, i].Value), 3)}\n\n\n";
                        }
                    }
                else log += "В следствии прогона ошибок не обнаружено.\n\n";
                return log;
            }
            else return "Empty";
        }
        private void TimerTestBox_CheckedChanged(object sender, EventArgs e)
            => GetMessageTestBox.Visible = timerPanelTest.Visible = TimerTestBox.Checked;
        private void numericSecondsTest_ValueChanged(object sender, EventArgs e) => BeginInvoke((MethodInvoker)(() => {
                if (numericSecondsTest.Value == 60)
                {
                    numericSecondsTest.Value = 0;
                    numericMinutesTest.Value += 1;
                }
            }));
        private void numericMinutesTest_ValueChanged(object sender, EventArgs e) => BeginInvoke((MethodInvoker)(() => {
            if (numericMinutesTest.Value == 60)
            {
                numericMinutesTest.Value = 0;
                numericHoursTest.Value += 1;
            }
        }));
        private void numericHoursTest_ValueChanged(object sender, EventArgs e) => BeginInvoke((MethodInvoker)(() => {
            if (numericHoursTest.Value == 24)
            {
                numericHoursTest.Value = 0;
                numericDaysTest.Value += 1;
            }
        }));

        private void openDebugWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (StaticSettings.debugForm is null)
            {
                StaticSettings.debugForm = new DataDebuggerForm();
                StaticSettings.debugForm.Show();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e) 
            => this.Close();

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) => new AboutInfo().ShowDialog();

    }
}
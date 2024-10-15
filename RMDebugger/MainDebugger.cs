using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationProtocol;
using RMDebugger.Properties;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.Sockets;
using System.Reflection;
using Microsoft.Win32;
using System.IO.Ports;
using System.Drawing;
using StaticSettings;
using System.Linq;
using System.Net;
using System.IO;
using System;

using BootloaderProtocol;
using SearchProtocol;
using File_Verifier;
using ProtocolEnums;
using CRC16;
using CSV;

namespace RMDebugger
{
    public partial class MainDebugger : Form
    {
        Socket udpGate = new Socket(SocketType.Dgram, ProtocolType.Udp);
        Color mirClr = Color.PaleGreen;
        private readonly string mainName = Assembly.GetEntryAssembly().GetName().Name;
        string ver;
        private string[] fieldsFounded = new string[5] { "addr", "fio", "lamp", "puid", "rmb" };

        BindingList<FieldConfiguration> fieldsData = new BindingList<FieldConfiguration>();
        BindingList<DeviceClass> testerData = new BindingList<DeviceClass>();
        BindingList<DeviceData> deviceData = new BindingList<DeviceData>();

        private int setTimerTest;
        private int realTimeWorkingTest = 0;

        public MainDebugger()
        {
            InitializeComponent();
            Options.debugger = this;
            NotifyMessage.Text = 
                this.Text = 
                $"{Assembly.GetEntryAssembly().GetName().Name} {Assembly.GetEntryAssembly().GetName().Version}";
            AddEvents();
        }
        private void AddEvents()
        {
            Load += MainFormLoad;
            FormClosed += MainFormClosed;

            KeyDown += (s, e) =>
            {
                if (e.KeyValue == (char)Keys.F11)
                {
                    if (this.WindowState == FormWindowState.Normal)
                        this.WindowState = FormWindowState.Maximized;
                    else
                        this.WindowState = FormWindowState.Normal;
                }
            };
            StatusRS485GridView.DoubleBuffered(true);
            ConfigDataGrid.DoubleBuffered(true);
            SearchGrid.DoubleBuffered(true);

            windowPinToolStrip.CheckedChanged += (s, e) =>
            {
                this.TopMost = windowPinToolStrip.Checked;
                windowPinToolStrip.ToolTipText = windowPinToolStrip.Checked ? "Поверх других окон." : "Обычное состояние окна.";
            };
            transparentToolStrip.CheckedChanged += (s, e) => this.Opacity = transparentToolStrip.Checked ? 0.95 : 1;
            messagesToolStrip.CheckedChanged += (s, e) => Options.showMessages = messagesToolStrip.Checked;
            HexTimeout.ValueChanged += (s, e) => Options.hexTimeout = (int)HexTimeout.Value;
            extendedButtonsToolStrip.CheckedChanged += (s, e) =>
            {
                ResetButton.Visible =
                SetBootloaderStopButton.Visible =
                SetBootloaderStartButton.Visible = extendedButtonsToolStrip.Checked;
            };

            comPort.SelectedIndexChanged += (s, e) => mainPort.PortName = comPort.SelectedItem.ToString();
            BaudRate.SelectedIndexChanged += (s, e) => BaudRateSelectedIndexChanged(s, e);
            RefreshSerial.Click += (s, e) => AddPorts(comPort);
            foreach (ToolStripDropDownItem item in dataBits.DropDownItems) item.Click += DataBitsForSerial;
            foreach (ToolStripDropDownItem item in Parity.DropDownItems) item.Click += ParityForSerial;
            foreach (ToolStripDropDownItem item in stopBits.DropDownItems) item.Click += StopBitsForSerial;
            OpenCom.Click += OpenComClick;
            PingButton.Click += PingButtonClick;
            numericPort.ValueChanged += (s, e) => { if (Options.pingOk) Options.pingOk = !Options.pingOk; };
            Connect.Click += ConnectClick;
            NeedThrough.CheckedChanged += NeedThroughCheckedChanged;
            TargetSignID.ValueChanged += (s, e) => NeedThrough.Enabled = TargetSignID.Value != 0;
            

            deviceData.ListChanged += (s, e) => ToMessageStatus($"{deviceData.Count} devices in Search.");
            SearchAutoButton.Click += SearchButtonClick;
            SearchManualButton.Click += SearchButtonClick;
            SearchTimeout.ValueChanged += (s, e) => Options.timeoutSearch = (int)SearchTimeout.Value;
            SearchChangeColorMenuItem.Click += (s, e) => {
                if (MirrorColor.ShowDialog() == DialogResult.OK)
                    mirClr = MirrorColor.Color;
            };
            SearchGetNear.CheckedChanged += (s, e) =>
            {
                SearchFilterMode.Enabled =
                    SearchGetNear.Checked;
                SearchFindSignatireMode.Enabled =
                    SearchExtendedFindMode.Enabled = !Options.through && SearchGetNear.Checked;
            };
            foreach (ToolStripMenuItem item in SearchFilterMenuStrip.Items) item.Click += SearchFilterClick;
            SearchGrid.DataSource = deviceData;
            SearchGrid.CellContentClick += (s, e) => TargetSignID.Value = Convert.ToDecimal(SearchGrid[0, e.RowIndex].Value);


            HexCheckCrc.CheckedChanged += (s, e) => Options.checkCrc = HexCheckCrc.Checked;
            HexUploadButton.Click += HexUploadButtonClick;

            CloseFromToolStrip.Click += (s, e) => this.Close();
            OpenDebugFromToolStrip.Click += (s, e) =>
            {
                if (Options.debugForm is null)
                {
                    Options.debugForm = new DataDebuggerForm();
                    Options.debugForm.Show();
                }
            };

            AboutButton.Click += (s, e) => new AboutInfo().ShowDialog();
            AboutFromToolStrip.Click += (s, e) => new AboutInfo().ShowDialog();

            ConfigDataGrid.DataSource = fieldsData;
            ConfigDataGrid.CellToolTipTextNeeded += (s, e) =>
            {
                if (e.RowIndex > -1 && e.ColumnIndex == (int)ConfigColumns.ConfigUpload)
                    e.ToolTipText = ((FieldConfiguration)ConfigDataGrid.Rows[e.RowIndex].DataBoundItem).toolTip;
            };


            LoadConfigButton.Click += LoadConfigButtonClick;
            UploadConfigButton.Click += UploadConfigButtonClick;

            ConfigClearLoad.Click += (s, e) => { 
                foreach (FieldConfiguration fc in fieldsData) 
                    fc.loadValue = string.Empty; 
                ConfigDataGrid.Refresh(); };
            ConfigClearUpload.Click += (s, e) => { 
                foreach (FieldConfiguration fc in fieldsData) 
                    fc.uploadValue = string.Empty; 
                ConfigDataGrid.Refresh(); };
            ConfigClearAll.Click += (s, e) => { 
                foreach (FieldConfiguration fc in fieldsData) 
                    fc.ClearValues(); 
                ConfigDataGrid.Refresh(); };
            ConfigEnableAllMenuItem.CheckedChanged += (s, e) =>
            {
                foreach (FieldConfiguration fc in fieldsData) 
                    fc.fieldActive = ConfigEnableAllMenuItem.Checked; 
                ConfigDataGrid.Refresh(); 
            };

            ConfigAddField.Click += (s, e) =>
            {
                if (ConfigFieldTextBox.TextLength > 0)
                    fieldsData.Add(new FieldConfiguration() {
                        fieldName = ConfigFieldTextBox.Text
                    });
            };

            RMLRModeCheck.CheckedChanged += (s, e) => RMLRRepeatCount.Visible = RMLRModeCheck.Checked;

            InfoTree.NodeMouseClick += InfoTreeNodeClick;

            StartTestRSButton.Click += StartTestRSButtonClick;
            AddSignatureIDToTest.Click += AddTargetSignID;
            ManualScanToTest.Click += ManualScanToTestClick;
            AutoScanToTest.Click += AutoScanToTestClick;

            ClearInfoTestRS485.Click += ClearInfoTestRS485Click;
            MoreInfoTestRS485.Click += MoreInfoTestRS485Click;

            TimerSettingsTestBox.CheckedChanged += (s, e) => timerPanelTest.Visible = TimerSettingsTestBox.Checked;
            StatusRS485GridView.DataSource = testerData;
            StatusRS485GridView.RowPrePaint += CellValueChangedRS485;
            foreach (ToolStripDropDownItem item in RS485SortMenuStrip.Items) item.Click += ChooseSortedBy;
            SortByButton.Click += (s, e) => StatusRS485Sort();
            SortedColumnCombo.SelectedIndex = 0;


            SetOnlineButton.Click += (s, e) => SendCommandFromExtraButton(CmdOutput.ONLINE);
            ResetButton.Click += (s, e) => SendCommandFromExtraButton(CmdOutput.RESET);
            SetBootloaderStartButton.Click += (s, e) => SendCommandFromExtraButton(CmdOutput.START_BOOTLOADER);
            SetBootloaderStopButton.Click += (s, e) => SendCommandFromExtraButton(CmdOutput.STOP_BOOTLOADER);
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
        private void SetProperties()
        {
            NeedThrough.Checked = Settings.Default.ThroughRM485;
            TargetSignID.Value = Settings.Default.MainSignatureID;
            ThroughSignID.Value = Settings.Default.ThroughSignatureID;
            numericPort.Value = Settings.Default.UDPGatePort;
            HexPageSize.Value = Settings.Default.LastPageSize;
            IPaddressBox.Text = Settings.Default.UDPGateIP;
            HexPathBox.Items.Add(Settings.Default.LastPathToHex);
            if (HexPathBox.Items.Count != 0) HexPathBox.SelectedItem = HexPathBox.Items[0];
            ThroughOrNot();
            if (Settings.Default.LastPortName != string.Empty && comPort.Items.Contains(Settings.Default.LastPortName))
                comPort.Text = Settings.Default.LastPortName;
            BaudRate.Text = Settings.Default.LastBaudRate.ToString();
            transparentToolStrip.Checked = Settings.Default.LastTransparent;
            Options.showMessages = messagesToolStrip.Checked = Settings.Default.LastShowMessages;
            Options.checkCrc = HexCheckCrc.Checked = Settings.Default.LastCheckCrc;
            HexTimeout.Value = Settings.Default.LastHexTimeout;
            Options.hexTimeout = (int)HexTimeout.Value;
        }
        private void NotifyMessage_Click(object sender, EventArgs e)
            => this.WindowState = FormWindowState.Normal;
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
            Settings.Default.LastTransparent = transparentToolStrip.Checked;
            Settings.Default.LastShowMessages = messagesToolStrip.Checked;
            Settings.Default.LastCheckCrc = HexCheckCrc.Checked;
            Settings.Default.LastHexTimeout = (int)HexTimeout.Value;
            Settings.Default.Save();
        }
        private void ToMessageStatus(string msg) => BeginInvoke((MethodInvoker)(() => MessageStatus.Text = msg ));
        private void ToReplyStatus(string msg) => BeginInvoke((MethodInvoker)(() => ReplyStatus.Text = msg));
        private void ToDebuggerWindow(string msg, ProtocolReply reply)
        {
            if ((Options.logState == LogState.ERRORState && reply != ProtocolReply.Ok)
                || Options.logState == LogState.DEBUGState)
                Options.debugForm?.AddToQueue(msg);
        }

        //Update
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
                    if (Options.showMessages)
                        NotifyMessage.ShowBalloonTip(5, "Обновление", $"Доступна новая версия: {ver}", ToolTipIcon.Info);
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

        /// <summary>
        /// Переделать чтение реестра
        /// Публичные контролы вернуть в приватные, перенести в статический класс
        /// </summary>
        private void CheckReg()
        {
            using (RegistryKey curUK = Registry.CurrentUser)
            {
                if (checkMainFolder(curUK))
                    using (RegistryKey rKey = curUK.OpenSubKey(mainName, true))
                    {
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
                        this.Enabled = true;
                    }
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
                    item.Click += loadSaveFrom;
                foreach (ToolStripMenuItem item in deleteSaveFromPCToolStripMenuItem.DropDownItems)
                    item.Click += deleteSaveFrom;
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


        //Serial config
        private void ComDefault()
        {
            mainPort.WriteTimeout =
                mainPort.ReadTimeout = 500;
            BaudRate.SelectedItem = "38400";
            DataBitsForSerial(dataBits8, null);
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
        private void BaudRateSelectedIndexChanged(object sender, EventArgs e)
            => mainPort.BaudRate = Convert.ToInt32(BaudRate.SelectedItem);
        private void DataBitsForSerial(object sender, EventArgs e)
        {
            ToolStripMenuItem databits = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem item in dataBits.DropDownItems) item.CheckState = CheckState.Unchecked;
            mainPort.DataBits = Convert.ToByte(databits.Text);
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
                catch (Exception ex) { ToMessageStatus(ex.Message); }
            }
            else mainPort.Close();
            OpenCom.Text = mainPort.IsOpen ? "Close" : "Open";
            AfterComEvent(mainPort.IsOpen);
            AfterAnyInterfaceEvent(mainPort.IsOpen);
        }
        private void AfterComEvent(bool sw)
        {
            comPort.Enabled =
                RefreshSerial.Enabled =
                UdpPage.Enabled = !sw;
            Options.mainInterface = sw ? mainPort : null;
        }
        
        //UDP config
        async private void PingButtonClick(object sender, EventArgs e)
        {
            if (Options.pingOk) PingSettings(!Options.pingOk);
            else await check_ip();
        }
        private void IPaddressBox_TextChanged(object sender, EventArgs e)
        {
            Options.pingOk =
                Connect.Enabled = false;
            bool ipCorrect = IPAddress.TryParse(IPaddressBox.Text, out IPAddress ipAddr) && IPaddressBox.Text.Split('.').Length == 4;
            PingButton.Enabled = ipCorrect;
            if (ipCorrect) ErrorMessage.Clear();
            else ErrorMessage.SetError(label13, $"Неверно задан параметр IP Address");
        }
        private void PingSettings(bool sw)
        {
            Options.pingOk = 
                Connect.Enabled = sw;
            PingButton.BackColor = sw ? Color.Green : Color.Red;
        }
        async private Task check_ip()
        {
            using Ping ping = new Ping();
            byte[] buffer = new byte[32];
            PingOptions pingOptions = new PingOptions(buffer.Length, true);
            if (!IPAddress.TryParse(IPaddressBox.Text, out IPAddress ip)) return;
            PingReply reply = await ping.SendPingAsync(ip, 255, buffer, pingOptions);
            PingSettings(reply.Status == IPStatus.Success);
            if (reply.Status != IPStatus.Success) return;
            try
            {
                for (int reconnect = 0; reconnect <= 5 && Options.pingOk;)
                {
                    reply = await ping.SendPingAsync(ip, 1000, buffer, pingOptions);

                    if (reply.Status != IPStatus.Success)
                    {
                        reconnect++;
                        continue;
                    }
                    else if (reconnect > 0) reconnect = 0;
                    await Task.Delay(250);
                }
            }
            catch (Exception ex) { ToMessageStatus(ex.Message.ToString()); }
            finally {
                if (udpGate.Connected) ConnectClick(null, null);
                PingSettings(udpGate.Connected);
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
        {
            SerialPage.Enabled = !sw;
            Options.mainInterface = sw ? udpGate : null;
        }
        
        // //after any interface
        private void AfterAnyInterfaceEvent(bool sw)
        {
            RMData.Enabled =
                    ExtraButtonsGroup.Enabled = 
                    Options.mainIsAvailable = sw;
            loadFromPCToolStripMenuItem.Enabled = 
                clearSettingsToolStrip.Enabled = !sw;
        }
        
        //Through settings
        private void NeedThroughCheckedChanged(object sender, EventArgs e)
        {
            ThroughOrNot();
            (TargetSignID.Value, ThroughSignID.Value) = (ThroughSignID.Value, TargetSignID.Value);
        }
        private void ThroughOrNot()
        {
            Options.through = NeedThrough.Checked;
            RS485Page.Enabled =
                RMLRModeCheck.Enabled =
                RMLRRepeatCount.Enabled = !Options.through;
            SearchFindSignatireMode.Enabled =
                SearchExtendedFindMode.Enabled = !Options.through && SearchGetNear.Checked;
            ThroughSignID.Enabled = Options.through;
        }

        // Off\On tabs
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

        //Search
        private void SearchFilterClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            Enum.TryParse(item.Text, out DevType type);
            switch (item.Checked)
            {
                case true: 
                    if (!Options.devTypesSearch.Contains(type)) Options.devTypesSearch.Add(type);
                    break;
                case false:
                    if (Options.devTypesSearch.Contains(type)) Options.devTypesSearch.Remove(type);
                    break;
            }
        }
        async private void SearchButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            bool manual = btn == SearchManualButton;
            if (btn == SearchAutoButton)
            {
                if (Options.autoSearch)
                {
                    Options.autoSearch = false;
                    SearchAutoButton.Enabled = false;
                    return;
                }
                else Options.autoSearch = true;
            }
            AfterSearchEvent(true, manual);
            offTabsExcept(RMData, SearchPage);
            await Task.Run(() => StartSearchAsync());
            AfterSearchEvent(false, manual);
            onTabPages(RMData);
        }
        private void AfterSearchEvent(bool sw, bool manual)
        {
            AfterAnyAutoEvent(sw);
            if (!SearchAutoButton.Enabled) SearchAutoButton.Enabled = true;
            if (manual) SearchAutoButton.Enabled = !sw;
            else
            {
                SearchAutoButton.Text = sw ? "Stop" : "Auto";
                SearchAutoButton.Image = sw ? Resources.StatusStopped : Resources.StatusRunning;
            }
            SearchManualButton.Enabled = !sw;
        }
        
        async private Task StartSearchAsync()
        {
            using (Searching search = new Searching(Options.mainInterface))
            {
                search.ToReply += ToReplyStatus;
                search.ToDebug += ToDebuggerWindow;
                do
                {
                    if (!Options.mainIsAvailable) break;
                    List<DeviceData> data = new List<DeviceData>();
                    if (SearchGetNear.Checked)
                    {
                        data = await search.GetDataFromDevice(CmdOutput.GRAPH_GET_NEAR, TargetSignID.GetBytes(), ThroughSignID.GetBytes());
                        if ((SearchFindSignatireMode.Checked && SearchFindSignatireMode.Enabled) 
                            || (SearchExtendedFindMode.Checked && SearchExtendedFindMode.Enabled))
                        {
                            List<DeviceData> dataNew = new List<DeviceData>();
                            foreach (DeviceData device in data)
                            {
                                device.inOneBus = await ThisDeviceInOneBus(search, device);
                                if (SearchExtendedFindMode.Checked && SearchExtendedFindMode.Enabled && device.inOneBus)
                                {
                                    device.iSee = await search.GetDataFromDevice(CmdOutput.GRAPH_GET_NEAR, device.devSign.GetBytes(), 0.GetBytes());
                                    if (device.iSee.Count > 0)
                                        dataNew.AddRange(device.iSee);
                                }
                            }
                            data.AddRange(dataNew);
                        }  
                    }
                    if (SearchDistTof.Checked)
                    {
                        if (data.Count == 0) data = await search.GetDataFromDevice(CmdOutput.ONLINE_DIST_TOF, TargetSignID.GetBytes(), ThroughSignID.GetBytes());
                        else
                        {
                            List<DeviceData> dataDistTof = await search.GetDataFromDevice(CmdOutput.ONLINE_DIST_TOF, TargetSignID.GetBytes(), ThroughSignID.GetBytes());
                            foreach(DeviceData dDt in dataDistTof)
                            {
                                foreach (DeviceData dGn in data)
                                    if (dDt.devSign == dGn.devSign)
                                    {
                                        dGn.devDist = dDt.devDist;
                                        dGn.devRSSI = dDt.devRSSI;
                                        break;
                                    }
                            }
                        }
                    }
                    data = data.OrderBy(s => s.devSign).GroupBy(a => a.devSign).Select(g => g.First()).ToList();

                    if (SearchFilterMode.Checked && SearchFilterMode.Enabled && Options.devTypesSearch.Count > 0)
                        data = data.Where(x => Options.devTypesSearch.Contains(x.devType)).ToList();

                    Action action = () =>
                    {
                        deviceData.Clear();
                        foreach (DeviceData dData in data)
                        {
                            deviceData.Add(dData);
                            if (SearchFindSignatireMode.Checked && SearchFindSignatireMode.Enabled)
                                SearchGrid.Rows[data.IndexOf(dData)].DefaultCellStyle.BackColor = dData.inOneBus ? mirClr : Color.White;
                        }
                        
                    };
                    if (InvokeRequired) Invoke(action);
                    else action();

                    if (SearchKnockMode.Checked && data.Count > 0)
                    {
                        Options.autoSearch = false;
                        if (Options.showMessages)
                            NotifyMessage.ShowBalloonTip(5, "Тук-тук!", $"Ответ получен!", ToolTipIcon.Info);
                    }
                    await Task.Delay(Options.autoSearch ? Options.timeoutSearch : 50);
                }
                while (Options.autoSearch);
                Options.autoSearch = false;
            }
        }
        async private Task<bool> ThisDeviceInOneBus(CommandsOutput search, DeviceData device)
        {
            try {
                await Task.Delay(1);
                await search.GetData(search.FormatCmdOut(device.devSign.GetBytes(), CmdOutput.STATUS, 0xff), (int)CmdMaxSize.STATUS, 50);
                return true;
            }
            catch { return false; }
        }

        //Any events
        private void AfterAnyAutoEvent(bool sw)
        {
            SerUdpPages.Enabled =
                ExtraButtonsGroup.Enabled = 
                BaudRate.Enabled = 
                dataBits.Enabled = 
                Parity.Enabled = 
                stopBits.Enabled = !sw;
            DinoRunningProcessOk.Enabled =
                DinoRunningProcessOk.Visible = sw;
        }

        //Hex uploader
        async private void HexUploadButtonClick(object sender, EventArgs e)
        {
            Options.HexUploadState = !Options.HexUploadState;
            if (Options.HexUploadState)
            {
                AfterHexUploadEvent(true);
                offTabsExcept(RMData, HexUpdatePage);
                await Task.Run(() => HexUploadAsyncNew());
                Options.HexUploadState = false;
                AfterHexUploadEvent(false);
                onTabPages(RMData);
            }
        }
        private void AfterHexUploadEvent(bool sw)
        {
            AfterAnyAutoEvent(sw);
            HexPathBox.Enabled = 
                HexPageSize.Enabled = 
                HexPathButton.Enabled =
                HexTimeout.Enabled =
                SignaturePanel.Enabled = !sw;
            HexUploadButton.Text = sw ? "Stop" : "Upload";
            HexUploadButton.Image = sw ? Resources.StatusStopped : Resources.StatusRunning;
            UpdateBar.Value = 0;
            BytesStart.Text = "0";
        }
        private void SetHexUploadProgress(int progress = 0)
        {
            UpdateBar.Value = progress;
            BytesStart.Text = progress.ToString();
        }
        async private Task HexUploadAsyncNew()
        {
            using (Bootloader boot = NeedThrough.Checked
                ? new Bootloader(Options.mainInterface, TargetSignID.GetBytes(), ThroughSignID.GetBytes())
                : new Bootloader(Options.mainInterface, TargetSignID.GetBytes()))
            {
                boot.ToReply += ToReplyStatus;
                boot.ToDebug += ToDebuggerWindow;
                boot.SetProgress += SetHexUploadProgress;
                try { boot.SetQueueFromHex(Options.hexPath); }
                catch (Exception ex) { ToMessageStatus(ex.Message); return; }
                //async method2
                async Task<bool> GetReplyFromDevice(byte[] cmdOut, int receiveDelay = 50, bool taskDelay = false, int delayMs = 10)
                {
                    DateTime t0 = DateTime.Now;
                    TimeSpan tstop = DateTime.Now - t0;
                    while (tstop.Seconds < Options.hexTimeout && Options.HexUploadState)
                    {
                        try
                        {
                            Tuple<byte[], ProtocolReply> replyes = await boot.GetData(cmdOut, cmdOut.Length, receiveDelay);
                            return true;
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message == "devNull") return false;
                            if ((DateTime.Now - t0).Seconds >= Options.hexTimeout)
                            {
                                DialogResult message = MessageBox.Show(this, "Timeout", "Something wrong...", MessageBoxButtons.RetryCancel);
                                if (message == DialogResult.Cancel) break;
                                else t0 = DateTime.Now;
                            }
                            if (taskDelay) await Task.Delay(delayMs);
                        }
                    }
                    return false;
                }

                byte[] cmdBootStart = boot.buildCmdDelegate(CmdOutput.START_BOOTLOADER);
                byte[] cmdBootStop = boot.buildCmdDelegate(CmdOutput.STOP_BOOTLOADER);
                byte[] cmdConfirmData = boot.buildCmdDelegate(CmdOutput.UPDATE_DATA_PAGE);

                if (!await GetReplyFromDevice(cmdBootStart, taskDelay: true)) return;

                ToMessageStatus("Bootload OK");

                boot.PageSize = (int)HexPageSize.Value;

                Stopwatch stopwatchQueue = Stopwatch.StartNew();

                while (boot.HexQueue.Count() > 0)
                {
                    boot.GetDataForUpload(out byte[] dataOutput); 
                    if (!await GetReplyFromDevice(boot.buildDataCmdDelegate(dataOutput), receiveDelay: 250)) return;
                    if (!await GetReplyFromDevice(cmdConfirmData, taskDelay: true, delayMs: 10)) return;
                }
                await GetReplyFromDevice(cmdBootStop, taskDelay: true);
                stopwatchQueue.Stop();
                string timeUplod = $"{stopwatchQueue.Elapsed.Minutes:00}:{stopwatchQueue.Elapsed.Seconds:00}:{stopwatchQueue.Elapsed.Milliseconds:000}";
                if (Options.HexUploadState)
                {
                    ToMessageStatus($"Firmware OK | Uploaded for " + timeUplod);
                    if (Options.showMessages)
                        NotifyMessage.ShowBalloonTip(5,
                            "Прошивка устройства",
                            $"Файл {Path.GetFileName(Options.hexPath)} успешно загружен на устройство за " + timeUplod,
                            ToolTipIcon.Info);
                }
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
                if (!HexPathBox.Items.Contains(file.FileName))
                    HexPathBox.Items.Add(file.FileName);
                HexPathBox.SelectedItem = file.FileName;
            }
        }
        private void Hex_Box_DragDrop(object sender, DragEventArgs e)
        {
            string[] FilePath = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string path in FilePath)
                if (!HexPathBox.Items.Contains(path) && new FileInfo(path).Extension == ".hex")
                    HexPathBox.Items.Add(path);
            HexPathBox.SelectedItem = FilePath[0];
        }
        private void Hex_Box_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
        private void Hex_Box_TextChanged(object sender, EventArgs e)
        {
            bool exists = File.Exists(HexPathBox.Text);
            if (exists)
            {
                Options.hexPath = HexPathBox.Text;
                int linesCount = File.ReadLines(HexPathBox.Text).Count();
                BytesEnd.Text = linesCount.ToString();
                UpdateBar.Maximum = linesCount;
            }
            HexUploadButton.Enabled = exists;
            HexUploadFilename.Text = exists ? $"Filename: {Path.GetFileName(HexPathBox.Text)}" : string.Empty;
        }


        //Config
        private void DefaultConfigGrid() {
            foreach (string key in fieldsFounded)
                fieldsData.Add(new FieldConfiguration() { fieldName = key });
        }
        private void ColoredRow(int index, DataGridView dgv, Color color)
                => Invoke((MethodInvoker)(async () => {
                    dgv.Rows[index].DefaultCellStyle.BackColor = color;
                    await Task.Delay(500);
                    dgv.Rows[index].DefaultCellStyle.BackColor = Color.White;
                }));

        // //Load
        async private void LoadConfigButtonClick(object sender, EventArgs e)
        {
            bool CheckFields() {
                foreach (FieldConfiguration field in fieldsData)
                    if (field.fieldActive) return true;
                return false;
            }
            Options.ConfigLoadState = !Options.ConfigLoadState;
            if ((Options.ConfigLoadState && CheckFields()) || RMLRModeCheck.Checked)
            {
                AfterLoadConfigEvent(true);
                offTabsExcept(RMData, ConfigPage);
                await Task.Run(() => LoadField());
                Options.ConfigLoadState = false;
                AfterLoadConfigEvent(false);
                onTabPages(RMData);
            }
        }
        private void AfterLoadConfigEvent(bool sw)
        {
            AfterAnyAutoEvent(sw);
            ConfigDataGrid.Enabled =
                SignaturePanel.Enabled =
                UploadConfigButton.Enabled =
                RMLRModeCheck.Enabled =
                RMLRRepeatCount.Enabled =
                ConfigAddField.Enabled =
                ConfigFieldTextBox.Enabled =
                ConfigFactoryCheck.Enabled = !sw;
            LoadConfigButton.Text = sw ? "Stop" : "Load from device";
            LoadConfigButton.Image = sw ? Resources.StatusStopped : Resources.CloudDownload;
        }

        async private Task<bool> RMLRMode(Configuration config)
        {
            byte[] RMLRRgbFormat(byte[] rmSign, byte count, CmdOutput cmd)
            {
                List<byte> data = new List<byte>();
                data.AddRange(rmSign);
                data.AddRange(BitConverter.GetBytes((ushort)cmd).Reverse());
                data.Add(RMLRRed.Checked ? count : (byte)0);
                data.Add(RMLRGreen.Checked ? count : (byte)0);
                data.Add(RMLRBlue.Checked ? count : (byte)0);
                data.Add(RMLRBuzzer.Checked ? count : (byte)0);
                return new CRC16_CCITT_FALSE().CrcCalc(data.ToArray());
            }

            Tuple<byte[], ProtocolReply> reply;
            byte[] cmdOut = config.FormatCmdOut(config._targetSign, CmdOutput.RMLR_REGISTRATION, 0xff);
            byte[] rgbBuzz = RMLRRgbFormat(config._targetSign, (byte)RMLRRepeatCount.Value, CmdOutput.RMLR_RGB);
            while (true)
            {
                if (!Options.ConfigLoadState && !Options.ConfigUploadState) return false;
                try
                {
                    reply = await config.GetData(cmdOut, (int)CmdMaxSize.RMLR_REGISTRATION);
                    ToReplyStatus(reply.Item2.ToString());
                    if (reply.Item1.Length == 10)
                    {
                        await config.GetData(rgbBuzz, (int)CmdMaxSize.RMLR_RGB);
                        config._targetSign = new byte[2] { reply.Item1[4], reply.Item1[5] };
                        if (Options.showMessages)
                            NotifyMessage.ShowBalloonTip(
                                5, "Найдена метка RMP", 
                                $"Найдена метка RMP с сигнатурой: {reply.Item1[5] << 8 | reply.Item1[4]}", 
                                ToolTipIcon.Info);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    ToReplyStatus(ex.Message);
                    if (ex.Message == "devNull") return false;
                }
                await Task.Delay(50);
            }
            return true;
        }
        async private Task LoadField()
        {
            Configuration config = NeedThrough.Checked
                ? new Configuration(Options.mainInterface, TargetSignID.GetBytes(), ThroughSignID.GetBytes())
                : new Configuration(Options.mainInterface, TargetSignID.GetBytes());
            config.ToReply += ToReplyStatus;
            config.ToDebug += ToDebuggerWindow;


            if (RMLRModeCheck.Checked && !NeedThrough.Checked)
            {
                if (await RMLRMode(config))
                {
                    config = new Configuration(Options.mainInterface, config._targetSign, TargetSignID.GetBytes());
                    config.ToReply += ToReplyStatus;
                    config.ToDebug += ToDebuggerWindow;
                }
                else return;
            }

            foreach (FieldConfiguration field in fieldsData)
            {
                if (field.fieldActive && Options.ConfigLoadState)
                {
                    byte[] cmdOut = config.buildCmdLoadDelegate(field.fieldName);
                    int fieldLen = fieldsFounded.Contains(field.fieldName) ? (int)field.rule + 1 : 17;
                    int sizeData = fieldLen + (field.fieldName.Length + 1) + 6; // 6 = 2 addr + 2 cmd + 2 crc
                    if (config.through) sizeData += 4;

                    Tuple<byte[], ProtocolReply> reply;
                    byte[] cmdIn = new byte[0];
                    while (Options.ConfigLoadState)
                    {
                        try
                        {
                            ToMessageStatus($"{field.fieldName}");
                            reply = await config.GetData(cmdOut, sizeData);
                            cmdIn = config.ReturnWithoutThrough(reply.Item1);
                            break;
                        }
                        catch (Exception ex) 
                        {
                            if (ex.Message == "devNull") return;
                        }
                        await Task.Delay(50);
                    }
                    try
                    {
                        byte[] tempData = new byte[cmdIn.Length - 6 - field.fieldName.Length];
                        Array.Copy(cmdIn, 4 + field.fieldName.Length, tempData, 0, tempData.Length);
                        field.loadValue = config.GetSymbols(tempData);
                        ColoredRow(fieldsData.IndexOf(field), ConfigDataGrid, Color.GreenYellow);
                    }
                    catch
                    {
                        field.loadValue = "Error";
                        ColoredRow(fieldsData.IndexOf(field), ConfigDataGrid, Color.Red);
                    }
                }
            }
        }

        // //Upload
        async private void UploadConfigButtonClick(object sender, EventArgs e)
        {
            bool CheckFields() {
                foreach (FieldConfiguration field in fieldsData)
                    if (field.fieldActive && Options.ConfigUploadState)
                        if (!string.IsNullOrEmpty(field.uploadValue) || ConfigFactoryCheck.Checked) return true;
                return false;
            }
            Options.ConfigUploadState = !Options.ConfigUploadState;
            if ((Options.ConfigUploadState && CheckFields()) || RMLRModeCheck.Checked)
            {
                AfterUploadConfigEvent(true);
                offTabsExcept(RMData, ConfigPage);
                await Task.Run(() => UploadField());
                Options.ConfigUploadState = false;
                AfterUploadConfigEvent(false);
                onTabPages(RMData);
            }
        }
        private void AfterUploadConfigEvent(bool sw)
        {
            AfterAnyAutoEvent(sw);
            ConfigDataGrid.Enabled =
                SignaturePanel.Enabled =
                LoadConfigButton.Enabled =
                RMLRModeCheck.Enabled =
                RMLRRepeatCount.Enabled =
                ConfigAddField.Enabled =
                ConfigFieldTextBox.Enabled =
                ConfigFactoryCheck.Enabled = !sw;
            UploadConfigButton.Text = sw ? "Stop" : "Upload to device";
            UploadConfigButton.Image = sw ? Resources.StatusStopped : Resources.CloudUpload;
        }
        async private Task UploadField()
        {
            Configuration config = NeedThrough.Checked
                ? new Configuration(Options.mainInterface, TargetSignID.GetBytes(), ThroughSignID.GetBytes())
                : new Configuration(Options.mainInterface, TargetSignID.GetBytes());

            if (RMLRModeCheck.Checked && !NeedThrough.Checked)
            {
                if (await RMLRMode(config))
                    config = new Configuration(Options.mainInterface, config._targetSign, TargetSignID.GetBytes());
                else return;
            }

            config.ToReply += ToReplyStatus;
            config.ToDebug += ToDebuggerWindow;

            config.factory = ConfigFactoryCheck.Checked;

            int sizeData = (int)CmdMaxSize.ONLINE;
            if (config.through) sizeData += 4; // +2 addr, +2 cmd

            foreach (FieldConfiguration field in fieldsData)
            {
                if (field.fieldActive && Options.ConfigUploadState)
                    if (!string.IsNullOrEmpty(field.uploadValue) || config.factory)
                    {
                        if (config.factory && field.fieldName == "addr") continue;
                        Tuple<RmResult, ProtocolReply> reply;
                        byte[] cmdOut = config.buildCmdUploadDelegate(
                                            field.fieldName, 
                                            field.uploadValue, 
                                            fieldsFounded.Contains(field.fieldName) ? (int)field.rule + 1 : 17);
                        while (Options.ConfigUploadState)
                        {
                            try
                            {
                                reply = await config.GetResult(cmdOut, sizeData);
                                ToMessageStatus($"{field.fieldName} : {reply.Item1}");
                                if (reply.Item1 == RmResult.Ok)
                                {
                                    if (field.fieldName == "addr" && !RMLRModeCheck.Checked) TargetSignID.Value = Convert.ToInt32(field.uploadValue);
                                    ColoredRow(fieldsData.IndexOf(field), ConfigDataGrid, Color.GreenYellow);
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message == "devNull") return;
                            }
                            await Task.Delay(50);
                        }
                    }
            }
        }

        //Get info
        private void DefaultInfoGrid()
        {
            InfoFieldsGrid.Rows.Clear();
            foreach (string data in Enum.GetNames(typeof(InfoGrid))) InfoFieldsGrid.Rows.Add(data);
            foreach (TreeNode node in InfoTree.Nodes) node.Nodes.Clear();
        }
        async private void InfoTreeNodeClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (UInt16.TryParse(e.Node.Text, out ushort newSignTarget))
            {
                TargetSignID.Value = newSignTarget;
                return;
            }
            switch (e.Node.Name)
            {
                case "GetNearInfo":
                    using (Searching search = new Searching(Options.mainInterface))
                    {
                        search.ToReply += ToReplyStatus;
                        search.ToDebug += ToDebuggerWindow;
                        List<DeviceData> data = await search.GetDataFromDevice(CmdOutput.GRAPH_GET_NEAR, TargetSignID.GetBytes(), ThroughSignID.GetBytes());
                        string radio = data.Count > 0 ? "Ok" : "Null";
                        e.Node.Nodes.Clear();
                        TreeNode getnear = new TreeNode($"Radio: {radio}");
                        e.Node.Nodes.Add(getnear);
                        InfoFieldsGrid.Rows[(int)InfoGrid.Radio].Cells[1].Value = radio;
                        if (data.Count > 0)
                        {
                            e.Node.Expand();
                            Dictionary<string, List<int>> typeNodesData = new Dictionary<string, List<int>>();
                            foreach(DeviceData device in data)
                            {
                                string type = $"{device.devType}";
                                if (!typeNodesData.ContainsKey(type)) typeNodesData[type] = new List<int>();
                                typeNodesData[type].Add(device.devSign);
                            }

                            List<TreeNode> typeNodesList = new List<TreeNode>();
                            foreach (string key in typeNodesData.Keys)
                            {
                                TreeNode typeNode = new TreeNode($"{key}: {typeNodesData[key].Count}");
                                foreach (int sign in typeNodesData[key])
                                    typeNode.Nodes.Add(
                                        new TreeNode($"{sign}")
                                        {
                                            ToolTipText = $"Нажмите на сигнатуру {sign}, что бы опросить",
                                            ForeColor = SystemColors.HotTrack
                                        });
                                typeNodesList.Add(typeNode);
                            }
                            getnear.Nodes.AddRange(typeNodesList.ToArray());
                            getnear.Expand();
                        }
                        e.Node.Expand();
                    };
                    return;
                case "WhoAreYouInfo":
                    GetInfoAboutDevice(CmdOutput.GRAPH_WHO_ARE_YOU, e.Node);
                    break;
                case "StatusInfo":
                    GetInfoAboutDevice(CmdOutput.STATUS, e.Node);
                    break;
                default: return;
            }
        }
        async private void GetInfoAboutDevice(CmdOutput cmdOutput, TreeNode treeNode)
        {
            using (Information info = NeedThrough.Checked
                ? new Information(Options.mainInterface, TargetSignID.GetBytes(), ThroughSignID.GetBytes())
                : new Information(Options.mainInterface, TargetSignID.GetBytes()))
            {
                info.ToReply += ToReplyStatus;
                info.ToDebug += ToDebuggerWindow;
                byte[] cmdOut = info.buildCmdDelegate(cmdOutput);
                int size = !NeedThrough.Checked ? (int)cmdOutput : (int)cmdOutput + 4;
                treeNode.Nodes.Clear();
                try
                {
                    Tuple<byte[], ProtocolReply> reply = await info.GetData(cmdOut, size);
                    byte[] cmdIn = info.ReturnWithoutThrough(reply.Item1);
                    Dictionary<string, string> data = info.CmdInParse(cmdIn);
                    data.Add("Date", DateTime.Now.ToString("dd-MM-yy HH:mm"));
                    foreach (string str in data.Keys)
                    {
                        treeNode.Nodes.Add($"{str}: {data[str]}");
                        if (Enum.GetNames(typeof(InfoGrid)).Contains(str))
                            InfoFieldsGrid.Rows[(int)Enum.Parse(typeof(InfoGrid), str)].Cells[1].Value = data[str];
                    }
                    treeNode.Expand();
                }
                catch { }
            };
        }
        private void OpenCloseMenuInfoTree_Click(object sender, EventArgs e)
        {
            InfoTreePanel.Location = OpenCloseMenuInfoTree.Text == "<"
                ? new Point(InfoTreePanel.Location.X - 162, InfoTreePanel.Location.Y)
                : new Point(InfoTreePanel.Location.X + 162, InfoTreePanel.Location.Y);
            OpenCloseMenuInfoTree.Text = OpenCloseMenuInfoTree.Text == "<" ? ">" : "<";
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


        //RS485 test
        private void StatusGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) => TaskForChangedRows();
        private void StatusGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) => TaskForChangedRows();
        private void TaskForChangedRows() 
            => StartTestRSButton.Enabled =
                SaveLogTestRS485.Enabled =
                StatusRS485GridView.Rows.Count > 0;
        private void CellValueChangedRS485(object sender, DataGridViewRowPrePaintEventArgs e)
            => StatusRS485GridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = testerData[e.RowIndex].statusColor;
        private bool GetDevices(out Dictionary<string, List<DeviceClass>> interfacesDict)
        {
            interfacesDict = new Dictionary<string, List<DeviceClass>>();
            if (StatusRS485GridView.Rows.Count == 0) return false;
            foreach(DeviceClass device in testerData)
            {
                if (!interfacesDict.ContainsKey(device.devInterface)) interfacesDict[device.devInterface] = new List<DeviceClass>();
                interfacesDict[device.devInterface].Add(device);
            }
            return true;
        }
        private string GetConnectedInterfaceString()
        {
            string deviceInterface = "";
            string deviceOption = "";
            if (Options.mainInterface is Socket Sock)
            {
                deviceInterface = ((IPEndPoint)Sock.RemoteEndPoint).Address.MapToIPv4().ToString();
                deviceOption = ((IPEndPoint)Sock.RemoteEndPoint).Port.ToString();
            }
            else if (Options.mainInterface is SerialPort Port)
            {
                deviceInterface = Port.PortName;
                deviceOption = Port.BaudRate.ToString();
            }
            return $"{deviceInterface}:{deviceOption}";
        }
        private void AddToGridDevice(DeviceClass device)
        {
            GetDevices(out Dictionary<string, List<DeviceClass>> devicesInGrid);
            string connectedInterface = GetConnectedInterfaceString();

            if (devicesInGrid.ContainsKey(connectedInterface))
            {
                List<int> signaturesInGrid = new List<int>();

                foreach (DeviceClass deviceInGrid in devicesInGrid[connectedInterface])
                    signaturesInGrid.Add(deviceInGrid.devSign);

                if (signaturesInGrid.Contains(device.devSign)) return;
            }
            device.devInterface = connectedInterface;
            testerData.Add(device);
            ToMessageStatus($"Device count: {StatusRS485GridView.RowCount}");
        }
        private void AddToGridDevices(List<DeviceClass> devices)
        {
            GetDevices(out Dictionary<string, List<DeviceClass>> devicesInGrid);
            string connectedInterface = GetConnectedInterfaceString();

            List<DeviceClass> newDevicesForGrid = new List<DeviceClass>();
            if (devicesInGrid.ContainsKey(connectedInterface))
            {
                List<int> signaturesInGrid = new List<int>();
                foreach (DeviceClass device in devicesInGrid[connectedInterface])
                    signaturesInGrid.Add(device.devSign);

                foreach (DeviceClass device in devices)
                    if (!signaturesInGrid.Contains(device.devSign))
                        newDevicesForGrid.Add(device);
            }
            else newDevicesForGrid = devices;

            foreach (DeviceClass device in newDevicesForGrid)
            {
                device.devInterface = connectedInterface;
                testerData.Add(device);
            }
            ToMessageStatus($"Device count: {StatusRS485GridView.RowCount}");
        }


        async private void StartTestRSButtonClick(object sender, EventArgs e)
        {
            Options.RS485TestState = !Options.RS485TestState;
            if (Options.RS485TestState && StatusRS485GridView.Rows.Count > 0)
            {
                AfterStartTestRSEvent(true);
                offTabsExcept(RMData, TestPage);
                offTabsExcept(TestPages, RS485Page);
                ToMessageStatus($"Device count: {StatusRS485GridView.RowCount}");
                await Task.Run(() => StartTaskRS485Test());
                AfterStartTestRSEvent(false);
                Options.RS485TestState = false;
                WorkTestTimer.Stop();
                onTabPages(RMData);
                onTabPages(TestPages);
            }
            else StartTestRSButton.Enabled = false;
        }
        private void AfterStartTestRSEvent(bool sw)
        {
            AfterAnyAutoEvent(sw);
            StatusRS485GridView.AllowUserToDeleteRows =
                SignaturePanel.Enabled = 
                AutoScanToTest.Enabled = 
                scanGroupBox.Enabled = 
                settingsGroupBox.Enabled = 
                ClearDataTestRS485.Enabled =
                timerPanelTest.Enabled = !sw;
            StatusRS485GridView.Cursor = sw ? Cursors.AppStarting : Cursors.Default;
            StartTestRSButton.Text = sw ? "&Stop" : "&Start Test";
            StartTestRSButton.Image = sw ? Resources.StatusStopped : Resources.StatusRunning;
            if (!sw) StartTestRSButton.Enabled = true;
        }

        async private Task StartTaskRS485Test()
        {
            GetDevices(out Dictionary<string, List<DeviceClass>> devices);
            
            List<Task> tasks = new List<Task>();
            string connectedInterface = GetConnectedInterfaceString();

            if (TimerSettingsTestBox.Checked)
                setTimerTest = (int)new TimeSpan(
                               (int)numericDaysTest.Value,
                               (int)numericHoursTest.Value,
                               (int)numericMinutesTest.Value,
                               (int)numericSecondsTest.Value).TotalSeconds;

            Invoke((MethodInvoker)(() => WorkTestTimer.Start()));

            try
            {
                foreach (string key in devices.Keys)
                {
                    if (key == connectedInterface)
                        tasks.Add(StartTest(new ForTests(Options.mainInterface, devices[key])));
                    else tasks.Add(ConnectInterfaceAndStartTest(key.Split(':'), devices[key]));
                }
                await Task.WhenAll(tasks.ToArray());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally {
                RefreshGridTask();
                ToMessageStatus($"Device count: {StatusRS485GridView.RowCount} | Завершено");
            }
        }
        private void RefreshGridTask()
            => BeginInvoke((MethodInvoker)(() => {
                testerData.ResetBindings();
                StatusRS485GridView.ClearSelection();
                StatusRS485GridView.Refresh();
            }));

        private void WorkTestTimer_Tick(object sender, EventArgs e)
        {
            setTimerTest -= 1;
            realTimeWorkingTest += 1;
            RefreshGridTask();
            ChangeWorkTestTime(realTimeWorkingTest);
            if (TimerSettingsTestBox.Checked && setTimerTest == 0)
            {
                Options.RS485TestState = false;
                if (Options.showMessages)
                    NotifyMessage.ShowBalloonTip(5,
                        "Время истекло!",
                        "Время истекло, тест завершен",
                        ToolTipIcon.Info);
            }
            
        }

        async private Task ConnectInterfaceAndStartTest(string[] interfaceSettings, List<DeviceClass> devices)
        {
            if (IPAddress.TryParse(interfaceSettings[0], out IPAddress ipAddr))
            {
                using (Socket sockTest = new Socket(SocketType.Dgram, ProtocolType.Udp))
                {
                    sockTest.Connect(ipAddr, Convert.ToUInt16(interfaceSettings[1]));
                    await StartTest(new ForTests(sockTest, devices));
                }
            }   
            else
            {
                using (SerialPort serialTest = new SerialPort(interfaceSettings[0], Convert.ToInt32(interfaceSettings[1])))
                {
                    serialTest.Open();
                    await StartTest(new ForTests(serialTest, devices));
                };
            }
        }
        async private Task StartTest(ForTests forTests)
        {
            forTests.TestDebug += ToDebuggerWindow;
            forTests.clearAfterError = ClearBufferSettingsTestBox.Checked;
            Random random = new Random();
            do
            {
                int getTest = random.Next(0, 101);
                foreach (DeviceClass device in forTests.ListDeviceClass)
                {
                    if (!Options.RS485TestState || !Options.mainIsAvailable) break;
                    if      (getTest == 0 && RadioSettingsTestBox.Checked) 
                            await forTests.GetRadioDataFromDevice(device, CmdOutput.GRAPH_GET_NEAR);
                    else if (getTest == 100) 
                            await forTests.GetRadioDataFromDevice(device, CmdOutput.ONLINE_DIST_TOF);
                    else if (getTest == 99 && RadioSettingsTestBox.Checked) 
                            await forTests.GetDataFromDevice(device, CmdOutput.GRAPH_WHO_ARE_YOU);
                    else if (getTest >= 75) 
                            await forTests.GetDataFromDevice(device, CmdOutput.STATUS);
                    else    await forTests.GetDataFromDevice(device, CmdOutput.ROUTING_GET);
                }
            }
            while (Options.RS485TestState && Options.mainIsAvailable);
        }
        async Task<DeviceClass> GetDeviceInfo(Searching search, ushort sign)
        {
            Tuple<byte[], ProtocolReply> replyes =
                    await search.GetData(search.FormatCmdOut(sign.GetBytes(), CmdOutput.STATUS, 0xff),
                        (int)CmdMaxSize.STATUS, 35);
            return new DeviceClass()
            {
                devSign = sign,
                devType = search.GetType(replyes.Item1),
                devVer = search.GetVersion(replyes.Item1),
            };
        }

        // //Add signature from Target numeric
        async private void AddTargetSignID(object sender, EventArgs e)
        {
            using (Searching search = new Searching(Options.mainInterface))
            {
                search.ToReply += ToReplyStatus;                                                                                                                                                        
                search.ToDebug += ToDebuggerWindow;
                try { AddToGridDevice(await GetDeviceInfo(search, (ushort)TargetSignID.Value)); }
                catch { }
            }
        }

        // //Manual scan
        async private void ManualScanToTestClick(object sender, EventArgs e)
        {
            Options.RS485ManualScanState = !Options.RS485ManualScanState;
            if (Options.RS485ManualScanState)
            {
                AfterRS485ManualScanEvent(true);
                offTabsExcept(RMData, TestPage);
                offTabsExcept(TestPages, RS485Page);
                await ManualScanRange();
                Options.RS485ManualScanState = false;
                AfterRS485ManualScanEvent(false);
                onTabPages(RMData);
                onTabPages(TestPages);
            }
        }
        private void AfterRS485ManualScanEvent(bool sw)
        {
            AfterAnyAutoEvent(sw);
            StatusRS485GridView.Cursor = sw ? Cursors.WaitCursor : Cursors.Default;
            StatusRS485GridView.AllowUserToDeleteRows =
                minSigToScan.Enabled = 
                maxSigToScan.Enabled =
                StartTestRSButton.Enabled =
                SignaturePanel.Enabled =
                AutoScanToTest.Enabled =
                settingsGroupBox.Enabled =
                ClearDataTestRS485.Enabled =
                AddSignatureIDToTest.Enabled =
                timerPanelTest.Enabled = !sw;
            ManualScanToTest.Text = sw ? "Stop" : "Manual Scan";
            ManualScanToTest.Image = sw ? Resources.StatusStopped : Resources.Search;
        }
        async private Task ManualScanRange()
        {
            List<DeviceClass> devices = await Task.Run(async () =>
            {
                using (Searching search = new Searching(Options.mainInterface))
                {
                    search.ToReply += ToReplyStatus;
                    search.ToDebug += ToDebuggerWindow;
                    List<DeviceClass> devices = new List<DeviceClass>();
                    int newDevices = 0;
                    for (int i = (int)minSigToScan.Value; i <= maxSigToScan.Value && Options.RS485ManualScanState; i++)
                    {
                        string deviceCount = newDevices > 0 ? $" (Catched: {newDevices})" : "";
                        try
                        {
                            ToMessageStatus($"Signature: {i} {deviceCount}");
                            devices.Add(await GetDeviceInfo(search, (ushort)i));
                            newDevices++;
                        }
                        catch { }
                    }
                    return devices;
                };
            });
            AddToGridDevices(devices);
        }

        // //Auto scan
        async private void AutoScanToTestClick(object sender, EventArgs e)
        {
            Options.RS485ManualScanState = !Options.RS485ManualScanState;
            if (Options.RS485ManualScanState)
            {
                AfterRS485AutoScanEvent(true);
                offTabsExcept(RMData, TestPage);
                offTabsExcept(TestPages, RS485Page);
                await AutoScanAdd();
                Options.RS485ManualScanState = false;
                AfterRS485AutoScanEvent(false);
                onTabPages(RMData);
                onTabPages(TestPages);
            }
        }
        private void AfterRS485AutoScanEvent(bool sw)
        {
            AfterAnyAutoEvent(sw);
            StatusRS485GridView.Cursor = sw ? Cursors.WaitCursor : Cursors.Default;
            StatusRS485GridView.AllowUserToDeleteRows =
                extendedMenuPanel.Enabled = !sw;
        }
        async private Task AutoScanAdd()
        {
            List<DeviceClass> devices = await Task.Run(async () =>
            {
                using (Searching search = new Searching(Options.mainInterface))
                {
                    search.ToReply += ToReplyStatus;
                    search.ToDebug += ToDebuggerWindow;
                    List<DeviceClass> devices = new List<DeviceClass>();
                    try
                    {
                        DeviceClass mainDevice = await GetDeviceInfo(search, (ushort)TargetSignID.Value);
                        devices.Add(mainDevice);
                        List<DeviceData> data = await search.GetDataFromDevice(CmdOutput.GRAPH_GET_NEAR, TargetSignID.GetBytes(), null);
                        foreach (DeviceData device in data)
                        {
                            try
                            {
                                devices.Add(await GetDeviceInfo(search, device.devSign));
                                ToMessageStatus($"Signature: {device.devSign}");
                            }
                            catch { }
                        }
                    }
                    catch { }
                    return devices;
                }
            });
            AddToGridDevices(devices);
        }
        private void minSigToScan_ValueChanged(object sender, EventArgs e)
        {
            maxSigToScan.Minimum = minSigToScan.Value;
            if (minSigToScan.Value >= maxSigToScan.Value)
                maxSigToScan.Value = minSigToScan.Value;
        }

        // //ExtendedMenu
        private void ShowExtendedMenu_Click(object sender, EventArgs e)
        {
            bool hideMenu = ShowExtendedMenu.Text == "Show &menu";
            StatusRS485GridView.Columns[0].Visible = hideMenu;
            extendedMenuPanel.Location = hideMenu
                ? new Point(extendedMenuPanel.Location.X, extendedMenuPanel.Location.Y - 147)
                : new Point(extendedMenuPanel.Location.X, extendedMenuPanel.Location.Y + 147);
            ShowExtendedMenu.Image = hideMenu ? Resources.Unhide : Resources.Hide;
            ToolTipHelper.SetToolTip(ShowExtendedMenu, hideMenu ? "Скрыть расширенное меню" : "Показать расширенное меню");
            ShowExtendedMenu.Text = hideMenu ? "Hide &menu" : "Show &menu";
        }
        private void ClearDataStatusRM_Click(object sender, EventArgs e) => testerData.Clear();
        private void ClearInfoTestRS485Click(object sender, EventArgs e)
        {
            foreach (DeviceClass device in testerData)
                device.Reset();
            RefreshGridTask();
            realTimeWorkingTest = 0;
            ChangeWorkTestTime(realTimeWorkingTest);
        }
        private void MoreInfoTestRS485Click(object sender, EventArgs e)
        {
            bool sw = MoreInfoTestRS485.Text == "More info";
            StatusRS485GridView.Columns[(int)RS485Columns.NoReply].Visible = 
                StatusRS485GridView.Columns[(int)RS485Columns.BadReply].Visible = 
                StatusRS485GridView.Columns[(int)RS485Columns.BadCrc].Visible = 
                StatusRS485GridView.Columns[(int)RS485Columns.BadRadio].Visible = 
                StatusRS485GridView.Columns[(int)RS485Columns.Nearby].Visible = sw;
            MoreInfoTestRS485.Text = sw ? "Hide info" : "More info";
        }
        private BindingList<DeviceClass> GetSortedList()
        {
            string property = StatusRS485GridView.GetPropertyByHeader(SortedColumnCombo.SelectedItem);
            if (byAscMenuItem.Checked)
                return new BindingList<DeviceClass>(testerData
                    .OrderBy(x => x[property])
                    .ToList());
            else if (byDescMenuItem.Checked)
                return new BindingList<DeviceClass>(testerData
                    .OrderByDescending(x => x[property])
                    .ToList());
            return null;
        }
        private void StatusRS485Sort()
        {
            BindingList<DeviceClass> testerDataNew = GetSortedList();
            testerData.Clear();
            foreach (DeviceClass device in testerDataNew)
                testerData.Add(device);
        }
        private void ChooseSortedBy(object sender, EventArgs e)
        {
            ToolStripMenuItem items = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem item in RS485SortMenuStrip.Items) item.CheckState = CheckState.Unchecked;
            switch (items.Name)
            {
                case "byAscMenuItem":
                    SortByButton.Image = Resources.SortAscending;
                    byAscMenuItem.Checked = true;
                    break;
                case "byDescMenuItem":
                    SortByButton.Image = Resources.SortDescending;
                    byDescMenuItem.Checked = true;
                    break;
            }
        }
        private void ChangeWorkTestTime(int seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            WorkingTimeLabel.Text = $"{(time.Days > 0 ? $"{time.Days}d " : "")}" +
                                    $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}";
        }
        private void SaveLogTestRS485_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            File.WriteAllText(saveFileDialog.FileName, GetLogFromTestRM485());
        }
        private string GetLogFromTestRM485()
        {
            if (StatusRS485GridView.Rows.Count > 0)
            {
                DateTime time = DateTime.Now;
                int tX = 0;
                int rX = 0;
                int errors = 0;
                for (int i = 0; i < StatusRS485GridView.Rows.Count; i++)
                {
                    tX += (int)StatusRS485GridView[(int)RS485Columns.Tx, i].Value;
                    rX += (int)StatusRS485GridView[(int)RS485Columns.Rx, i].Value;
                    errors += (int)StatusRS485GridView[(int)RS485Columns.Errors, i].Value;
                }
                string log =
                    $"Время создания лога\t\t\t{time}\n" +
                    $"Общее время тестирования\t\t{WorkingTimeLabel.Text}\n\n" +
                    $"\nОбщее количество отправленных пакетов: {tX}." +
                    $"\nОбщее количество принятых пакетов: {rX}." +
                    $"\nОбщее количество ошибок: {errors}\n\n";

                //"Испытуемые: sig, sig, sig... sig"
                Dictionary<ushort, int> signDataIndex = new Dictionary<ushort, int>();
                for (int i = 0; i < StatusRS485GridView.Rows.Count; i++)
                    signDataIndex[Convert.ToUInt16(StatusRS485GridView[(int)RS485Columns.Sign, i].Value)] = i;

                signDataIndex = signDataIndex.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                log += $"Количество испытуемых: {signDataIndex.Count}\n";
                log += $"Сигнатуры испытуемых: {String.Join(", ", signDataIndex.Keys)}.\n\n";

                if (errors > 0)
                    foreach (ushort key in signDataIndex.Keys)
                    {
                        int i = signDataIndex[key];
                        if ((int)StatusRS485GridView[(int)RS485Columns.Errors, i].Value > 0)
                        {
                            ushort sign = Convert.ToUInt16(StatusRS485GridView[(int)RS485Columns.Sign, i].Value);
                            string type = Convert.ToString(StatusRS485GridView[(int)RS485Columns.Type, i].Value);
                            int noReply = (int)StatusRS485GridView[(int)RS485Columns.NoReply, i].Value;
                            int badReply = (int)StatusRS485GridView[(int)RS485Columns.BadReply, i].Value;
                            int badCRC = (int)StatusRS485GridView[(int)RS485Columns.BadCrc, i].Value;
                            int badRadio = (int)StatusRS485GridView[(int)RS485Columns.BadRadio, i].Value;
                            log += $"Информация об устройстве типа {type} с сигнатурой {sign}:\n" +
                                   $"Состояние: {StatusRS485GridView[(int)RS485Columns.Status, i].Value}\n" +
                                   $"Версия прошивки устройства: {StatusRS485GridView[(int)RS485Columns.Version, i].Value}\n" +
                                   $"Количество отправленных пакетов устройству: {Convert.ToInt32(StatusRS485GridView[(int)RS485Columns.Tx, i].Value)}\n" +
                                   $"Количество принятых пакетов от устройства: {Convert.ToInt32(StatusRS485GridView[(int)RS485Columns.Rx, i].Value)}\n";
                            log += noReply > 0 ? $"Пропустил опрос: {noReply}\n" : "";
                            log += badReply > 0 ? $"Проблемы с ответом: {badReply}\n" : "";
                            log += badCRC > 0 ? $"Проблемы с CRC: {badCRC}\n" : "";
                            log += badRadio > 0 ? $"Проблемы с Radio: {badRadio}\n" : "";
                            log += $"Процент ошибок: {Math.Round(Convert.ToDouble(StatusRS485GridView[(int)RS485Columns.PercentErrors, i].Value), 3)}\n\n\n";
                        }
                    }
                else log += "В следствии прогона ошибок не обнаружено.\n\n";
                return log;
            }
            else return "Empty";
        }
        private void numericSecondsTest_ValueChanged(object sender, EventArgs e)
        {
            if (numericSecondsTest.Value == 60)
            {
                numericSecondsTest.Value = 0;
                numericMinutesTest.Value += 1;
            }
        }
        private void numericMinutesTest_ValueChanged(object sender, EventArgs e)
        {
            if (numericMinutesTest.Value == 60)
            {
                numericMinutesTest.Value = 0;
                numericHoursTest.Value += 1;
            }
        }
        private void numericHoursTest_ValueChanged(object sender, EventArgs e)
        {
            if (numericHoursTest.Value == 24)
            {
                numericHoursTest.Value = 0;
                numericDaysTest.Value += 1;
            }
        }


        //Extra Buttons
        private void AfterSendExtraButtonEvent(bool sw)
        {
            SerUdpPages.Enabled =
                BaudRate.Enabled =
                dataBits.Enabled =
                Parity.Enabled =
                stopBits.Enabled =
                ButtonsPanel.Enabled =
                SignaturePanel.Enabled =
                RMData.Enabled = !sw;
            DinoRunningProcessOk.Enabled =
                DinoRunningProcessOk.Visible = sw;
        }
        async private void SendCommandFromExtraButton(CmdOutput cmdOutput)
        {
            AfterSendExtraButtonEvent(true);
            await Task.Run(() => SendCommandFromButtonTask(cmdOutput));
            AfterSendExtraButtonEvent(false);
        }
        async private Task SendCommandFromButtonTask(CmdOutput cmdOutput)
        {
            using (CommandsOutput cOutput = new CommandsOutput(Options.mainInterface))
            {
                cOutput.ToReply += ToReplyStatus;
                cOutput.ToDebug += ToDebuggerWindow;
                byte[] cmdOut;
                switch (cmdOutput)
                {
                    case CmdOutput.ONLINE:
                        {
                            cmdOut = cOutput.FormatCmdOut(TargetSignID.GetBytes(), cmdOutput, (byte)SetOnlineFreqNumeric.Value);
                            if (NeedThrough.Checked) cmdOut = cOutput.CmdThroughRm(cmdOut, ThroughSignID.GetBytes(), CmdOutput.ROUTING_THROUGH);
                            break;
                        }
                    case CmdOutput.START_BOOTLOADER:
                    case CmdOutput.STOP_BOOTLOADER:
                        {
                            cmdOut = cOutput.FormatCmdOut(TargetSignID.GetBytes(), cmdOutput, 0xff);
                            if (NeedThrough.Checked) cmdOut = cOutput.CmdThroughRm(cmdOut, ThroughSignID.GetBytes(), CmdOutput.ROUTING_PROG);
                            break;
                        }
                    default:
                        {
                            cmdOut = cOutput.FormatCmdOut(TargetSignID.GetBytes(), cmdOutput, 0xff);
                            if (NeedThrough.Checked) cmdOut = cOutput.CmdThroughRm(cmdOut, ThroughSignID.GetBytes(), CmdOutput.ROUTING_THROUGH);
                            break;
                        }
                }
                Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOutput), out CmdMaxSize cmdSize);
                int size = !NeedThrough.Checked ? (int)cmdSize : (int)cmdSize + 6;
                do
                {
                    try
                    {
                        Tuple<byte[], ProtocolReply> reply = await cOutput.GetData(cmdOut, size, 50);
                        if (cmdOutput == CmdOutput.ONLINE
                            && cOutput.CheckResult(reply.Item1) != RmResult.Ok)
                            continue;

                        ToMessageStatus($"Успешно отправлена команда {cmdOutput}");
                        if (Options.showMessages)
                            NotifyMessage.ShowBalloonTip(5,
                                "Кнопка отработана",
                                $"Успешно отправлена команда {cmdOutput}",
                                ToolTipIcon.Info);
                        break;
                    }
                    catch { }
                    finally { await Task.Delay((int)AutoExtraButtonsTimeout.Value); }
                }
                while (AutoExtraButtons.Checked);
            };
        }
    }
}
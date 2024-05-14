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
using StaticSettings;
using File_Verifier;
using CSV;
using System.ComponentModel;

namespace RMDebugger
{
    public partial class MainDebugger : Form
    {
        Socket udpGate = new Socket(SocketType.Dgram, ProtocolType.Udp);
        Color mirClr = Color.PaleGreen;
        private readonly string mainName = Assembly.GetEntryAssembly().GetName().Name;
        string ver;
        private readonly Dictionary<string, ConfigCheckList> fieldsDict = new Dictionary<string, ConfigCheckList>()
        {
            ["addr"] = ConfigCheckList.uInt16,
            ["fio"] = ConfigCheckList.len16,
            ["lamp"] = ConfigCheckList.len4,
            ["puid"] = ConfigCheckList.uInt16,
            ["rmb"] = ConfigCheckList.uInt16,
        };

        BindingList<DeviceClass> testerData = new BindingList<DeviceClass>();

        private int setTimerTest;
        private int realTimeWorkingTest = 0;

        public MainDebugger()
        {
            InitializeComponent();
            Options.debugger = this;
            NotifyMessage.Text = this.Text = $"{Assembly.GetEntryAssembly().GetName().Name} {Assembly.GetEntryAssembly().GetName().Version}";
            AddEvents();
        }
        private void AddEvents()
        {
            Load += MainFormLoad;
            FormClosed += MainFormClosed;
            PasswordBox.TextChanged += (s, e) => ResetButton.Visible =
                                                 SetBootloaderStopButton.Visible =
                                                 SetBootloaderStartButton.Visible = 
                                                 PasswordBox.Text == "198237645";
            PinButton.Click += (s, e) =>
            {
                this.TopMost = PinButton.Checked;
                PinButton.ToolTipText = PinButton.Checked ? "Поверх других окон." : "Обычное состояние окна.";
                PinButton.Image = PinButton.Checked ? Resources.Pin : Resources.Unpin;
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
            DistToftimeout.Scroll += (s, e) => {
                Options.timeoutDistTof = DistToftimeout.Value;
                TimeForDistTof.Text = $"{Options.timeoutDistTof} ms";
            };
            GetNeartimeout.Scroll += (s, e) => {
                Options.timeoutGetNear = GetNeartimeout.Value;
                TimeForGetNear.Text = $"{Options.timeoutGetNear} ms";
            };
            ManualDistTof.Click += DistTofClick;
            AutoDistTof.Click += DistTofClick;
            DistTofGrid.RowsAdded += (s, e) => ToMessageStatus($"{e.RowCount} devices on Dist Tof.");

            ManualGetNear.Click += GetNearClick;
            AutoGetNear.Click += GetNearClick;
            GetNearGrid.RowsAdded += (s, e) => ToMessageStatus($"{e.RowCount} devices on Get Near.");
            GetNearGrid.CellContentClick += (s, e) => TargetSignID.Value = Convert.ToDecimal(GetNearGrid[0, e.RowIndex].Value);


            MirrorColorButton.Click += (s, e) =>
            {
                if (MirrorColor.ShowDialog() == DialogResult.OK)
                    mirClr = MirrorColor.Color;
            };
            TypeFilterBox.SelectedIndexChanged += (s, e) => Options.typeOfGetNear = TypeFilterBox.Text;
            Options.timeoutDistTof = DistToftimeout.Value;
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

            void AboutButtonClick(object sender, EventArgs e) => new AboutInfo().ShowDialog();
            AboutButton.Click += AboutButtonClick;
            AboutFromToolStrip.Click += AboutButtonClick;

            LoadConfigButton.Click += LoadConfigButtonClick;
            UploadConfigButton.Click += UploadConfigButtonClick;

            InfoTree.NodeMouseClick += InfoTreeNodeClick;

            StartTestRSButton.Click += StartTestRSButtonClick;
            AddSignatureIDToTest.Click += AddTargetSignID;
            ManualScanToTest.Click += ManualScanToTestClick;
            AutoScanToTest.Click += AutoScanToTestClick;

            ClearInfoTestRS485.Click += ClearInfoTestRS485Click;
            MoreInfoTestRS485.Click += MoreInfoTestRS485Click;
            StatusRM485GridView.DataSource = testerData;
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
        private void ToMessageStatus(string msg) => BeginInvoke((MethodInvoker)(() => MessageStatus.Text = msg ));
        private void ToReplyStatus(string msg) => BeginInvoke((MethodInvoker)(() => ReplyStatus.Text = msg));


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
            Options.pingOk = false;
            bool ipCorrect = IPAddress.TryParse(IPaddressBox.Text, out IPAddress ipAddr);
            PingButton.Enabled =
                Connect.Enabled = ipCorrect;
            if (ipCorrect) ErrorMessage.Clear();
            else ErrorMessage.SetError(label13, $"Неверно задан параметр IP Address");
            Connect.Text = "Connect";
        }
        private void PingSettings(bool sw)
        {
            Options.pingOk = 
                Connect.Enabled = sw;
            PingButton.BackColor = sw ? Color.Green : Color.Red;
        }
        async private Task check_ip()
        {
            int timeout = 500;
            using Ping ping = new Ping();
            byte[] buffer = new byte[32];
            PingOptions pingOptions = new PingOptions(buffer.Length, true);
            if (!IPAddress.TryParse(IPaddressBox.Text, out IPAddress ip)) return;
            PingReply reply = await ping.SendPingAsync(ip, timeout, buffer, pingOptions);
            PingSettings(reply.Status == IPStatus.Success);
            if (reply.Status != IPStatus.Success) return;
            try
            {
                for (int reconnect = 0; reconnect < 100 && Options.pingOk;)
                {
                    reply = await ping.SendPingAsync(ip, timeout, buffer, pingOptions);
                    if (reply.Status != IPStatus.Success) reconnect++;
                    await Task.Delay(timeout);
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
        
        //after any interface
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
            MirrorBox.Enabled = 
                MirrorColorButton.Enabled = 
                RS485Page.Enabled = 
                ExtendedBox.Enabled = !Options.through;
            ThroughSignID.Enabled = Options.through;
        }

        // method for Get near & Dist Tof
        async private Task<Dictionary<int, int>> GetDeviceListInfo(Searching search, CmdOutput cmdOutput, byte[] rmSign)
        {
            byte ix = 0x00;
            int iteration = 1;
            byte[] rmThrough = ThroughSignID.GetBytes();
            Dictionary<int, int> dataReturn = new Dictionary<int, int>();
            try
            {
                do
                {
                    Tuple<byte, Dictionary<int, int>> data = await search.RequestAndParseNew(cmdOutput, ix, rmSign, rmThrough);
                    ToReplyStatus("Ok");
                    ix = data.Item1;
                    iteration++;
                    search.AddKeys(dataReturn, data.Item2);
                }
                while (ix != 0x00 && iteration <= 5);
            }
            catch (Exception ex) { ToReplyStatus(ex.Message); }
            return dataReturn.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        //DistTof
        async private void DistTofClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            bool auto = btn == AutoDistTof;
            if (auto)
            {
                Options.autoDistTof = !Options.autoDistTof;
                if (Options.autoDistTof)
                {
                    AfterDistTofEvent(auto);
                    offTabsExcept(RMData, DistTofPage);
                    await Task.Run(() => DistTofAsync(auto));
                    AfterDistTofEvent(!auto);
                    onTabPages(RMData);
                }
                else AutoDistTof.Enabled = false;
            }
            else await DistTofAsync(auto);
        }
        private void AfterDistTofEvent(bool sw)
        {
            AfterAnyAutoEvent(sw);
            ManualDistTof.Enabled = !sw;
            AutoDistTof.Text = sw ? "Stop" : "Auto";
            AutoDistTof.Image = sw ? Resources.StatusStopped : Resources.StatusRunning;
            if (!AutoDistTof.Enabled) AutoDistTof.Enabled = true;
        }
        async private Task DistTofAsync(bool auto)
        {
            Searching search = new Searching(Options.mainInterface);
            do
            {
                if (!Options.mainIsAvailable) break;
                List<DataGridViewRow> rows = new List<DataGridViewRow>();
                Dictionary<int, int> data = await GetDeviceListInfo(search, CmdOutput.ONLINE_DIST_TOF, TargetSignID.GetBytes());
                if (data != null)
                    foreach (int key in data.Keys)
                    {
                        rows.Add(new DataGridViewRow());
                        rows[rows.Count - 1].CreateCells(DistTofGrid, key, data[key]);
                        rows[rows.Count - 1].Height = 18;
                    }
                Action action = () => {
                    DistTofGrid.Rows.Clear();
                    DistTofGrid.Rows.AddRange(rows.ToArray());
                };
                if (InvokeRequired) Invoke(action);
                else action();
                await Task.Delay(auto ? Options.timeoutDistTof : 50);
            }
            while (Options.autoDistTof);
        }

        //GetNear
        async private void GetNearClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            bool auto = btn == AutoGetNear;
            if (auto)
            {
                Options.autoGetNear = !Options.autoGetNear;
                if (Options.autoGetNear)
                {
                    AfterGetNearEvent(auto);
                    offTabsExcept(RMData, GetNearPage);
                    await Task.Run(() => GetNearAsync(auto));
                    AfterGetNearEvent(!auto);
                    onTabPages(RMData);
                }
                else AutoGetNear.Enabled = false;
            }
            else await Task.Run(() => GetNearAsync(auto));
        }
        private void AfterGetNearEvent(bool sw)
        {
            AfterAnyAutoEvent(sw);
            ManualGetNear.Enabled = !sw;
            AutoGetNear.Text = sw ? "Stop" : "Auto";
            AutoGetNear.Image = sw ? Resources.StatusStopped : Resources.StatusRunning;
            if (!AutoGetNear.Enabled) AutoGetNear.Enabled = true;
        }
        async private Task GetNearAsync(bool auto)
        {
            Searching search = new Searching(Options.mainInterface);
            do
            {
                if (!Options.mainIsAvailable) break;
                List<DataGridViewRow> rows = new List<DataGridViewRow>();
                Dictionary<int, int> data = await GetDeviceListInfo(search, CmdOutput.GRAPH_GET_NEAR, TargetSignID.GetBytes());
                List<int> inOneBus = new List<int>();
                List<int> outOfBus = new List<int>();
                List<int> mirrorList = new List<int>();

                if ((ExtendedBox.Checked && ExtendedBox.Enabled)
                    || (MirrorBox.Checked && MirrorBox.Enabled))
                {
                    foreach (int key in data.Keys)
                        if (ThisDeviceInOneBus(search, key)) inOneBus.Add(key);
                        else outOfBus.Add(key);

                    foreach (int key in inOneBus)
                    {
                        Dictionary<int, int> tempData = await GetDeviceListInfo(search, CmdOutput.GRAPH_GET_NEAR, key.GetBytes());
                        if (MirrorBox.Checked && tempData.ContainsKey((int)TargetSignID.Value)) mirrorList.Add(key);
                        if (ExtendedBox.Checked) search.AddKeys(data, tempData);
                    }
                    data = data.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                }
                foreach (int key in data.Keys)
                {
                    if (Options.typeOfGetNear != "<Any>"
                        && ((DevType)data[key]).ToString() != Options.typeOfGetNear || key == TargetSignID.Value) continue;
                    rows.Add(new DataGridViewRow());
                    rows[rows.Count - 1].CreateCells(DistTofGrid, key, (DevType)data[key]);
                    rows[rows.Count - 1].Height = 18;
                    if (mirrorList.Count > 0)
                        rows[rows.Count - 1].DefaultCellStyle.BackColor = 
                            mirrorList.Contains(key) ? mirClr : Color.White;
                }
                Action action = () =>
                {
                    GetNearGrid.Rows.Clear();
                    GetNearGrid.Rows.AddRange(rows.ToArray());
                };
                if (InvokeRequired) Invoke(action);
                else action();

                if (auto && rows.Count > 0 && KnockKnockBox.Checked)
                {
                    Options.autoGetNear = false;
                    NotifyMessage.BalloonTipTitle = "Тук-тук!";
                    NotifyMessage.BalloonTipText = $"Ответ получен!";
                    NotifyMessage.ShowBalloonTip(10);
                }

                await Task.Delay(auto ? Options.timeoutGetNear : 50);
            }
            while (Options.autoGetNear);
        }
        private bool ThisDeviceInOneBus(Searching search, int device) {
            try {
                return 
                    search.GetData(
                        search.FormatCmdOut(device.GetBytes(), CmdOutput.STATUS, 0xff), (int)CmdMaxSize.STATUS, 50) != null;
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
        }

        //Hex uploader
        async private void HexUploadButtonClick(object sender, EventArgs e)
        {
            Options.HexUploadState = !Options.HexUploadState;
            if (Options.HexUploadState)
            {
                AfterHexUploadEvent(true);
                offTabsExcept(RMData, HexUpdatePage);
                await Task.Run(() => HexUploadAsync());
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
                SignaturePanel.Enabled = !sw;
            HexUploadButton.Text = sw ? "Stop" : "Upload";
            HexUploadButton.Image = sw ? Resources.StatusStopped : Resources.StatusRunning;
            UpdateBar.Value = 0; 
            BytesStart.Text = "0";
        }
        async private Task HexUploadAsync()
        {
            Bootloader boot = NeedThrough.Checked 
                ? new Bootloader(Options.mainInterface, TargetSignID.GetBytes(), ThroughSignID.GetBytes()) 
                : new Bootloader(Options.mainInterface, TargetSignID.GetBytes());

            byte[] cmdBootStart = boot.buildCmdDelegate(CmdOutput.START_BOOTLOADER);
            byte[] cmdBootStop = boot.buildCmdDelegate(CmdOutput.STOP_BOOTLOADER);
            byte[] cmdConfirmData = boot.buildCmdDelegate(CmdOutput.UPDATE_DATA_PAGE);

            byte[][] hex;
            try { hex = boot.GetByteDataFromFile(Options.hexPath); }
            catch (Exception ex) { ToMessageStatus(ex.Message); return; }

            async Task<bool> GetReplyFromDevice(byte[] cmdOut, int receiveDelay = 50, bool taskDelay = false, int delayMs = 25)
            {
                DateTime t0 = DateTime.Now;
                TimeSpan tstop = DateTime.Now - t0;
                while (tstop.Seconds < Options.awaitCorrectHexUpload && Options.HexUploadState)
                {
                    try
                    {
                        Tuple<byte[], ProtocolReply> replyes = await boot.GetData(cmdOut, cmdOut.Length, receiveDelay);
                        ToReplyStatus(replyes.Item2.ToString());
                        return true;
                    }
                    catch (Exception ex)
                    {
                        ToReplyStatus(ex.Message);
                        if (ex.Message == "devNull") return false;
                        if ((DateTime.Now - t0).Seconds >= Options.awaitCorrectHexUpload)
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
            if (await GetReplyFromDevice(cmdBootStart, taskDelay: true)) ToMessageStatus("Bootload OK");
            else return;

            DateTime t0 = DateTime.Now;
            TimeSpan tstop;
            Tuple<byte[], int> tuple;
            for (int i = 0; i <= hex.Length - 1 && Options.HexUploadState;)
            {
                tuple = boot.GetDataForUpload(hex, (int)HexPageSize.Value, i);
                i = tuple.Item2;
                if (tuple.Item1 is null) continue;
                else
                {
                    if (!await GetReplyFromDevice(boot.buildDataCmdDelegate(tuple.Item1), receiveDelay: 200)) return;
                    if (!await GetReplyFromDevice(cmdConfirmData, taskDelay:true, delayMs:10)) return;
                    BeginInvoke((MethodInvoker)(() => {
                        UpdateBar.Value = 100 * i / hex.Length;
                        BytesStart.Text = i.ToString();
                    }));
                }
            }
            await GetReplyFromDevice(cmdBootStop, taskDelay: true);
            tstop = DateTime.Now - t0;
            string timeUplod = $" {tstop.Minutes}:{tstop.Seconds}:{tstop.Milliseconds}";
            if (Options.HexUploadState)
            {
                Invoke((MethodInvoker)(() => { MessageStatus.Text = $"Firmware OK | Uploaded for" + timeUplod; }));
                NotifyMessage.ShowBalloonTip(5, "Прошивка устройства", 
                    $"Файл {Path.GetFileName(Options.hexPath)} успешно загружен на устройство за" + timeUplod, ToolTipIcon.Info);
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
            {
                if (HexPathBox.Items.Contains(path)) continue;
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
            bool exists = File.Exists(HexPathBox.Text);
            if (exists)
            {
                Options.hexPath = HexPathBox.Text;
                int linesCount = File.ReadLines(HexPathBox.Text).Count();
                BytesEnd.Text = linesCount.ToString();
            }
            HexUploadButton.Enabled = exists;
            HexUploadFilename.Text = exists ? $"Filename: {Path.GetFileName(HexPathBox.Text)}" : string.Empty;
        }


        //Config

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
            InfoFieldsGrid.Rows.Clear();
            foreach (string data in Enum.GetNames(typeof(InfoGrid))) InfoFieldsGrid.Rows.Add(data);
            foreach (TreeNode node in InfoTree.Nodes) node.Nodes.Clear();
        }

        private ConfigCheckList GetFieldDict(string key) => fieldsDict.ContainsKey(key) ? fieldsDict[key] : ConfigCheckList.None;
        private void ConfigDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int emptyRow = e.RowIndex + 1;
            string field = (string)ConfigDataGrid[(int)ConfigColumns.ConfigColumn, e.RowIndex].Value;
            string value = (string)ConfigDataGrid[(int)ConfigColumns.ConfigUpload, e.RowIndex].Value;
            if (field is null) return;
            switch (e.ColumnIndex)
            {
                case (int)ConfigColumns.ConfigColumn: //field column
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
                case (int)ConfigColumns.ConfigUpload: //upload
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
                                                ConfigDataGrid[(int)ConfigColumns.ConfigUpload, e.RowIndex].Value = newValue;
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
        private void ColoredRow(int index, DataGridView dgv, Color color)
                => Invoke((MethodInvoker)(async () => {
                    dgv.Rows[index].DefaultCellStyle.BackColor = color;
                    await Task.Delay(500);
                    dgv.Rows[index].DefaultCellStyle.BackColor = Color.White;
                }));

        // //Load
        async private void LoadConfigButtonClick(object sender, EventArgs e)
        {
            bool GetEnabledFields(out Dictionary<string, int> fields) {
                fields = new Dictionary<string, int>();
                foreach (DataGridViewRow row in ConfigDataGrid.Rows)
                    if (row.Cells[(int)ConfigColumns.ConfigColumn].Value != null)
                        if (Convert.ToBoolean(row.Cells[(int)ConfigColumns.enabled].Value) == true
                            && !fields.ContainsKey((string)row.Cells[(int)ConfigColumns.ConfigColumn].Value))
                            fields.Add((string)row.Cells[(int)ConfigColumns.ConfigColumn].Value, row.Index);
                return fields.Count > 0;
            }

            Options.ConfigLoadState = !Options.ConfigLoadState;
            if (Options.ConfigLoadState && GetEnabledFields(out Dictionary<string, int> fields))
            {
                AfterLoadConfigEvent(true);
                offTabsExcept(RMData, ConfigPage);
                await Task.Run(() => LoadField(fields));
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
                ClearGridButton.Enabled =
                ConfigFactoryCheck.Enabled = !sw;
            LoadConfigButton.Text = sw ? "Stop" : "Load from device";
            LoadConfigButton.Image = sw ? Resources.StatusStopped : Resources.CloudDownload;
        }
        async private Task LoadField(Dictionary<string, int> fields)
        {
            Configuration config = NeedThrough.Checked
                ? new Configuration(Options.mainInterface, TargetSignID.GetBytes(), ThroughSignID.GetBytes())
                : new Configuration(Options.mainInterface, TargetSignID.GetBytes());

            Dictionary<string, (byte[], int)> cmdsOut = new Dictionary<string, (byte[], int)>();
            foreach(string key in fields.Keys)
            {
                byte[] cmdOut = config.buildCmdLoadDelegate(key);
                int valueLen = fieldsDict.ContainsKey(key) ? (int)fieldsDict[key] + 1 : 17;
                int dataCount = valueLen + (key.Length + 1) + 6;
                if (Options.through) dataCount += 4;
                cmdsOut[key] = (cmdOut, dataCount);
            }

            void TryParseData(byte[] data, int fieldLen, out string dataValue, out Color clr)
            {
                try
                {
                    byte[] tempData = new byte[data.Length - 6 - fieldLen];
                    Array.Copy(data, 4+fieldLen, tempData, 0, tempData.Length);
                    dataValue = Methods.CheckSymbols(tempData);
                    clr = Color.GreenYellow;
                }
                catch
                {
                    dataValue = "Error";
                    clr = Color.Red;
                }
            }

            foreach (string key in cmdsOut.Keys)
            {
                ToMessageStatus($"{key}");
                Tuple<byte[], ProtocolReply> reply;
                while (true)
                {
                    if (!Options.ConfigLoadState) return;
                    try
                    {
                        reply = await config.GetData(cmdsOut[key].Item1, cmdsOut[key].Item2);
                        ToReplyStatus(reply.Item2.ToString());
                        break;
                    }
                    catch (Exception ex)
                    {
                        ToReplyStatus(ex.Message);
                        if (ex.Message == "devNull") return;
                    }
                    await Task.Delay(50);
                }
                TryParseData(Options.through 
                        ? config.ReturnWithoutThrough(reply.Item1) 
                        : reply.Item1, key.Length + 1, 
                    out string dataValue, out Color clr);
                ColoredRow(fields[key], ConfigDataGrid, clr);
                ConfigDataGrid[(int)ConfigColumns.ConfigLoad, fields[key]].Value = dataValue;
            }
        }

        // //Upload
        async private void UploadConfigButtonClick(object sender, EventArgs e)
        {
            bool GetEnabledFieldsAndValues(out Dictionary<string, (int, string)> fields)
            {
                fields = new Dictionary<string, (int, string)>();
                foreach (DataGridViewRow row in ConfigDataGrid.Rows)
                    if (row.Cells[(int)ConfigColumns.ConfigColumn].Value != null
                        && (row.Cells[(int)ConfigColumns.ConfigUpload].Value != null || ConfigFactoryCheck.Checked))
                        if (Convert.ToBoolean(row.Cells[(int)ConfigColumns.enabled].Value) == true
                            && !fields.ContainsKey((string)row.Cells[(int)ConfigColumns.ConfigColumn].Value))
                            fields.Add((string)row.Cells[(int)ConfigColumns.ConfigColumn].Value, 
                                (row.Index, (string)row.Cells[(int)ConfigColumns.ConfigUpload].Value));
                return fields.Count > 0;
            }

            Options.ConfigUploadState = !Options.ConfigUploadState;
            if (Options.ConfigUploadState && GetEnabledFieldsAndValues(out Dictionary<string, (int, string)> fields))
            {
                AfterUploadConfigEvent(true);
                offTabsExcept(RMData, ConfigPage);
                await Task.Run(() => UploadField(fields));
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
                ClearGridButton.Enabled =
                ConfigFactoryCheck.Enabled = !sw;
            UploadConfigButton.Text = sw ? "Stop" : "Upload to device";
            UploadConfigButton.Image = sw ? Resources.StatusStopped : Resources.CloudUpload;
        }
        async private Task UploadField(Dictionary<string, (int, string)> fields)
        {
            Configuration config = NeedThrough.Checked
                ? new Configuration(Options.mainInterface, TargetSignID.GetBytes(), ThroughSignID.GetBytes())
                : new Configuration(Options.mainInterface, TargetSignID.GetBytes());

            int sizeAwaitData = Options.through 
                ? (int)CmdMaxSize.ONLINE + 4 
                : (int)CmdMaxSize.ONLINE;

            bool factory = ConfigFactoryCheck.Checked;

            Dictionary<string, byte[]> cmdsOut = new Dictionary<string, byte[]>();
            foreach(string key in fields.Keys)
            {
                if (factory && key == "addr") continue;
                int maxSize = fieldsDict.ContainsKey(key) ? (int)fieldsDict[key] + 1 : 17;
                cmdsOut[key] = config.buildCmdUploadDelegate(key, fields[key].Item2, fieldsDict.ContainsKey(key)
                                                            ? (int)fieldsDict[key] + 1
                                                            : 17, factory);
                if (key == "addr") config._targetSign = Convert.ToInt32(fields[key].Item2).GetBytes();
            }

            foreach (string key in cmdsOut.Keys)
            {
                Tuple<RmResult, ProtocolReply> reply;
                while (true)
                {
                    if (!Options.ConfigUploadState) return;
                    try
                    {
                        reply = await config.GetResult(cmdsOut[key], sizeAwaitData);
                        ToReplyStatus(reply.Item2.ToString());
                        ToMessageStatus($"{key} : {reply.Item1}");
                        if (reply.Item1 == RmResult.Ok)
                        {
                            if (key == "addr") TargetSignID.Value = Convert.ToInt32(fields[key].Item2);
                            ColoredRow(fields[key].Item1, ConfigDataGrid, Color.GreenYellow);
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ToReplyStatus(ex.Message);
                        if (ex.Message == "devNull") return;
                    }
                    await Task.Delay(50);
                }
            }
        }


        //Get info
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
                    Searching search = new Searching(Options.mainInterface);
                    Dictionary<int, int> data = await GetDeviceListInfo(search, CmdOutput.GRAPH_GET_NEAR, TargetSignID.GetBytes());
                    string radio = data.Count > 0 ? "Ok" : "Null";
                    e.Node.Nodes.Clear();
                    TreeNode getnear = new TreeNode($"Radio: {radio}");
                    e.Node.Nodes.Add(getnear);
                    InfoFieldsGrid.Rows[(int)InfoGrid.Radio].Cells[1].Value = radio;
                    if (data.Count > 0)
                    {
                        e.Node.Expand();
                        Dictionary<string, List<int>> typeNodesData = new Dictionary<string, List<int>>();
                        foreach (int key in data.Keys)
                        {
                            string type = $"{(DevType)data[key]}";
                            if (!typeNodesData.ContainsKey(type)) typeNodesData[type] = new List<int>();
                            typeNodesData[type].Add(key);
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
                    return;
                case "WhoAreYouInfo":
                    GetInfoAboutDevice(CmdOutput.WHO_ARE_YOU, e.Node);
                    break;
                case "StatusInfo":
                    GetInfoAboutDevice(CmdOutput.STATUS, e.Node);
                    break;
                default: return;
            }
        }
        async private void GetInfoAboutDevice(CmdOutput cmdOutput, TreeNode treeNode)
        {
            Information info = NeedThrough.Checked 
                ? new Information(Options.mainInterface, TargetSignID.GetBytes(), ThroughSignID.GetBytes())
                : new Information(Options.mainInterface, TargetSignID.GetBytes());

            byte[] cmdOut = info.buildCmdDelegate(cmdOutput);
            int size = !NeedThrough.Checked ? (int)cmdOutput : (int)cmdOutput + 6;
            treeNode.Nodes.Clear();
            try
            {
                Tuple<byte[], ProtocolReply> reply = await info.GetData(cmdOut, size);
                ToReplyStatus(reply.Item2.ToString());
                byte[] cmdIn = !NeedThrough.Checked ? reply.Item1 : info.ReturnWithoutThrough(reply.Item1);
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
            catch (Exception ex) { ToReplyStatus(ex.Message); }
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
        private bool GetDevices(out Dictionary<string, List<DeviceClass>> interfacesDict)
        {
            interfacesDict = new Dictionary<string, List<DeviceClass>>();
            if (StatusRM485GridView.Rows.Count == 0) return false;
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
            ToMessageStatus($"Device count: {StatusRM485GridView.RowCount}");
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
            ToMessageStatus($"Device count: {StatusRM485GridView.RowCount}");
        }


        async private void StartTestRSButtonClick(object sender, EventArgs e)
        {
            Options.RS485TestState = !Options.RS485TestState;
            if (Options.RS485TestState && StatusRM485GridView.Rows.Count > 0)
            {
                AfterStartTestRSEvent(true);
                ToMessageStatus($"Device count: {StatusRM485GridView.RowCount}");
                await Task.Run(() => StartTaskRS485Test());
                AfterStartTestRSEvent(false);
                Options.RS485TestState = false;
                WorkTestTimer.Stop();
            }
            else StartTestRSButton.Enabled = false;
        }
        private void AfterStartTestRSEvent(bool sw)
        {
            AfterAnyAutoEvent(sw);
            StatusRM485GridView.AllowUserToDeleteRows =
                SignaturePanel.Enabled = 
                AutoScanToTest.Enabled = 
                scanGroupBox.Enabled = 
                settingsGroupBox.Enabled = 
                ClearDataTestRS485.Enabled =
                timerPanelTest.Enabled = !sw;
            StatusRM485GridView.Cursor = sw ? Cursors.WaitCursor : Cursors.Default;
            StartTestRSButton.Text = sw ? "&Stop" : "&Start Test";
            StartTestRSButton.Image = sw ? Resources.StatusStopped : Resources.StatusRunning;
            if (!sw) StartTestRSButton.Enabled = true;
        }

        async private Task StartTaskRS485Test()
        {
            GetDevices(out Dictionary<string, List<DeviceClass>> devices);
            
            List<Task> tasks = new List<Task>();
            string connectedInterface = GetConnectedInterfaceString();

            if (TimerTestBox.Checked)
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
                        tasks.Add(StartTestNew(new ForTests(Options.mainInterface, devices[key])));
                    else tasks.Add(ConnectInterfaceAndStartTest(key.Split(':'), devices[key]));
                }
                await Task.WhenAll(tasks.ToArray());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally {
                RefreshGridTask();
                ToMessageStatus($"Device count: {StatusRM485GridView.RowCount} | Завершено");
            }
        }



        private void RefreshGridTask()
            => Invoke((MethodInvoker)(() => {
                testerData.ResetBindings();
                StatusRM485GridView.Refresh();
            }));

        private void WorkTestTimer_Tick(object sender, EventArgs e)
        {
            setTimerTest -= 1;
            realTimeWorkingTest += 1;
            RefreshGridTask();
            ChangeWorkTestTime(realTimeWorkingTest);
            if (TimerTestBox.Checked && setTimerTest == 0)
            {
                Options.RS485TestState = false;
                if (GetMessageTestBox.Checked)
                {
                    NotifyMessage.BalloonTipTitle = "Время истекло!";
                    NotifyMessage.BalloonTipText = $"Время истекло, тест завершен";
                    NotifyMessage.ShowBalloonTip(10);
                }
            }
        }

        async private Task ConnectInterfaceAndStartTest(string[] interfaceSettings, List<DeviceClass> devices)
        {
            if (IPAddress.TryParse(interfaceSettings[0], out IPAddress ipAddr))
            {
                using (Socket sockTest = new Socket(SocketType.Dgram, ProtocolType.Udp))
                {
                    sockTest.Connect(ipAddr, Convert.ToUInt16(interfaceSettings[1]));
                    await StartTestNew(new ForTests(sockTest, devices));
                }
            }   
            else
            {
                using (SerialPort serialTest = new SerialPort(interfaceSettings[0], Convert.ToInt32(interfaceSettings[1])))
                {
                    serialTest.Open();
                    await StartTestNew(new ForTests(serialTest, devices));
                };
            }
        }

        async private Task StartTestNew(ForTests forTests)
        {
            do
            {
                for (int i = 0; i < 2 && Options.RS485TestState && Options.mainIsAvailable; i++)
                {
                    foreach (DeviceClass device in forTests.ListDeviceClass)
                    {
                        /*await Task.Delay(1);*/
                        if (!Options.RS485TestState || !Options.mainIsAvailable) break;
                        device.devTx += 1;
                        switch (i)
                        {
                            case 0:
                                await forTests.GetDataFromDevice(device, CmdOutput.WHO_ARE_YOU);
                                break;
                            /*case 1:
                                break;*/
                            case 1:
                                await forTests.GetDataFromDevice(device, CmdOutput.STATUS);
                                break;
                            /*case 3:
                                break;
                            case 4:
                                break;*/
                        }
                    }
                }
            }
            while (Options.RS485TestState && Options.mainIsAvailable);
        }

        /*async private Task StartTest(ForTests forTests)
        {
            do
            {
                for (int i = 0; i < 5; i++)
                {
                    foreach (DeviceClass device in forTests.ListDeviceClass)
                    {
                        if (!Options.RS485TestState || !Options.mainIsAvailable) return;
                        byte[] rmSign = device.devSign.GetBytes();
                        List<int> replyCodes;
                        switch (i)
                        {
                            case 0:
                                FillGridResults(await forTests.RS485Test(rmSign, CmdOutput.WHO_ARE_YOU, device.devType), device.devIndexRow);
                                break;
                            *//*case 1:
                                if (RadioCheckTestBox.Checked)
                                {
                                    replyCodes = await forTests.RadioTest(rmSign, CmdOutput.GRAPH_GET_NEAR, dataGrid);
                                    foreach (int code in replyCodes)
                                        FillGridResults(code, device.devIndexRow);
                                }
                                break;*/
                            /*case 3:
                                if (RadioCheckTestBox.Checked)
                                {
                                    replyCodes = await forTests.RadioTest(rmSign, CmdOutput.ONLINE_DIST_TOF, dataGrid);
                                    foreach (int code in replyCodes)
                                        FillGridResults(code, device.devIndexRow);
                                }
                                break;*//*
                            case 4:
                                Tuple<int, TimeSpan?, int?> data = await forTests.GetWorkTimeAndVersion(rmSign, device.devType);
                                FillGridResults(data.Item1, device.devIndexRow);
                                if (data.Item2 is null || data.Item3 is null) break;
                                else
                                {
                                    TimeSpan time = (TimeSpan)data.Item2;
                                    Invoke((MethodInvoker)(() =>
                                    {
                                        StatusRM485GridView[(int)RS485Columns.WorkTime, device.devIndexRow].Value =
                                        $"{time.Days}d " +
                                        $"{time.Hours}h " +
                                        $": {time.Minutes}m " +
                                        $": {time.Seconds}s";
                                        StatusRM485GridView[(int)RS485Columns.Version, device.devIndexRow].Value = data.Item3;
                                    }));
                                }
                                break;
                        }
                    }
                }
            }
            while (Options.RS485TestState && Options.mainIsAvailable);
        }*/
        // //Add signature from Target numeric

        async private void AddTargetSignID(object sender, EventArgs e)
        {
            Searching search = new Searching(Options.mainInterface);
            byte[] cmdOut = search.FormatCmdOut(TargetSignID.GetBytes(), CmdOutput.STATUS, 0xff);
            Tuple<byte[], ProtocolReply> replyes;
            try
            {
                replyes = await search.GetData(cmdOut, (int)CmdMaxSize.STATUS, 50);
                DeviceClass device = new DeviceClass
                {
                    devSign = (int)TargetSignID.Value,
                    devType = search.GetType(replyes.Item1),
                    devVer = search.GetVersion(replyes.Item1)
                };
                AddToGridDevice(device);
            }
            catch (Exception ex) { ToReplyStatus(ex.Message); }
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
            StatusRM485GridView.Cursor = sw ? Cursors.WaitCursor : Cursors.Default;
            StatusRM485GridView.AllowUserToDeleteRows =
                minSigToScan.Enabled = 
                maxSigToScan.Enabled =
                StartTestRSButton.Enabled =  // <----------
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
            List<DeviceClass> devices = await Task.Run( async () =>
            {
                Searching search = new Searching(Options.mainInterface);
                List<DeviceClass> devices = new List<DeviceClass>();
                for (int i = (int)minSigToScan.Value; i <= maxSigToScan.Value && Options.RS485ManualScanState; i++)
                {
                    Tuple<byte[], ProtocolReply> replyes;
                    try
                    {
                        ToMessageStatus($"Signature: {i}");
                        replyes = await search.GetData(
                            search.FormatCmdOut(i.GetBytes(),
                            CmdOutput.STATUS, 0xff), (int)CmdMaxSize.STATUS, 25);
                        devices.Add(new DeviceClass
                        {
                            devSign = i,
                            devType = search.GetType(replyes.Item1),
                            devVer = search.GetVersion(replyes.Item1)
                        });
                    }
                    catch { }
                }
                return devices;
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
            StatusRM485GridView.Cursor = sw ? Cursors.WaitCursor : Cursors.Default;
            StatusRM485GridView.AllowUserToDeleteRows =
                extendedMenuPanel.Enabled = !sw;
        }
        async private Task AutoScanAdd()
        {
            List<DeviceClass> devices = await Task.Run(async () =>
            {
                Searching search = new Searching(Options.mainInterface);
                List<DeviceClass> devices = new List<DeviceClass>();
                Dictionary<int, int> data = await GetDeviceListInfo(search, CmdOutput.GRAPH_GET_NEAR, TargetSignID.GetBytes());
                if (data.Count > 0)
                {
                    data[(int)TargetSignID.Value] = 0;
                    foreach (int sign in data.Keys)
                    {
                        Tuple<byte[], ProtocolReply> replyes;
                        try
                        {
                            ToMessageStatus($"Signature: {sign}");
                            replyes = await search.GetData(
                                search.FormatCmdOut(sign.GetBytes(),
                                CmdOutput.STATUS, 0xff), (int)CmdMaxSize.STATUS, 25);
                            devices.Add(new DeviceClass
                            {
                                devSign = sign,
                                devType = search.GetType(replyes.Item1),
                                devVer = search.GetVersion(replyes.Item1)
                            });
                        }
                        catch { }
                    }
                }
                return devices;
            });
            AddToGridDevices(devices);
        }
        private void minSigToScan_ValueChanged(object sender, EventArgs e)
        {
            maxSigToScan.Minimum = minSigToScan.Value;
            if (minSigToScan.Value >= maxSigToScan.Value)
                maxSigToScan.Value = minSigToScan.Value;
        }
        private void ShowExtendedMenu_Click(object sender, EventArgs e)
        {
            bool hideMenu = ShowExtendedMenu.Text == "Show &menu";
            StatusRM485GridView.Columns[0].Visible = hideMenu;
            extendedMenuPanel.Location = hideMenu
                ? new Point(extendedMenuPanel.Location.X, extendedMenuPanel.Location.Y - 147)
                : new Point(extendedMenuPanel.Location.X, extendedMenuPanel.Location.Y + 147);
            ShowExtendedMenu.Image = hideMenu ? Resources.Unhide : Resources.Hide;
            ToolTipHelper.SetToolTip(ShowExtendedMenu, hideMenu ? "Скрыть расширенное меню" : "Показать расширенное меню");
            ShowExtendedMenu.Text = hideMenu ? "Hide &menu" : "Show &menu";
        }
        private void MoreInfoTestRS485Click(object sender, EventArgs e)
        {
            bool sw = MoreInfoTestRS485.Text == "More info";
            StatusRM485GridView.Columns[(int)RS485Columns.NoReply].Visible = sw;
            StatusRM485GridView.Columns[(int)RS485Columns.BadReply].Visible = sw;
            StatusRM485GridView.Columns[(int)RS485Columns.BadCrc].Visible = sw;
            StatusRM485GridView.Columns[(int)RS485Columns.BadRadio].Visible = sw;
            MoreInfoTestRS485.Text = sw ? "Hide info" : "More info";
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
                                   $"Версия прошивки устройства: {StatusRM485GridView[(int)RS485Columns.Version, i].Value}\n" +
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





















































        private void TaskForChangedRows()
        {
            StartTestRSButton.Enabled =
                SaveLogTestRS485.Enabled =
                StatusRM485GridView.Rows.Count > 0;
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
                ThroughOrNot();
                TypeFilterBox.SelectedIndex = 0;
                if (Settings.Default.LastPortName != string.Empty && comPort.Items.Contains(Settings.Default.LastPortName))
                    comPort.Text = Settings.Default.LastPortName;
                BaudRate.Text = Settings.Default.LastBaudRate.ToString();
            }));
        }

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

        
        private void StatusGridView_DoubleClick(object sender, EventArgs e) => StatusRM485GridView.ClearSelection();
        private void StatusGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) => TaskForChangedRows();
        private void StatusGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) => TaskForChangedRows();
        private void ClearDataStatusRM_Click(object sender, EventArgs e) => testerData.Clear();







        private void ClearInfoTestRS485Click(object sender, EventArgs e)
        {
            foreach (DeviceClass device in testerData)
                device.Reset();
            RefreshGridTask();
            realTimeWorkingTest = 0;
            ChangeWorkTestTime(realTimeWorkingTest);
        }


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


        



        


        
        private void NotifyMessage_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Normal;














        async private void SetOnlineButton_Click(object sender, EventArgs e) =>
             await Task.Run(() => SendCommandFromButton(new CommandsOutput(Options.mainInterface), CmdOutput.ONLINE));
        async private void ResetButton_Click(object sender, EventArgs e) =>
             await Task.Run(() => SendCommandFromButton(new CommandsOutput(Options.mainInterface), CmdOutput.RESET));
        async private void SetBootloaderStartButton_Click(object sender, EventArgs e) =>
             await Task.Run(() => SendCommandFromButton(new CommandsOutput(Options.mainInterface), CmdOutput.START_BOOTLOADER));
        async private void SetBootloaderStopButton_Click(object sender, EventArgs e) =>
             await Task.Run(() => SendCommandFromButton(new CommandsOutput(Options.mainInterface), CmdOutput.STOP_BOOTLOADER));
        async private Task SendCommandFromButton(CommandsOutput CO, CmdOutput cmdOutput)
        {
            byte[] cmdOut;
            switch (cmdOutput)
            {
                case CmdOutput.ONLINE:
                    {
                        cmdOut = CO.FormatCmdOut(TargetSignID.GetBytes(), cmdOutput, (byte)SetOnlineFreqNumeric.Value);
                        if (NeedThrough.Checked) cmdOut = CO.CmdThroughRm(cmdOut, ThroughSignID.GetBytes(), CmdOutput.ROUTING_THROUGH);
                        break;
                    }
                case CmdOutput.START_BOOTLOADER:
                case CmdOutput.STOP_BOOTLOADER:
                    {
                        cmdOut = CO.FormatCmdOut(TargetSignID.GetBytes(), cmdOutput, 0xff);
                        if (NeedThrough.Checked) cmdOut = CO.CmdThroughRm(cmdOut, ThroughSignID.GetBytes(), CmdOutput.ROUTING_PROG);
                        break;
                    }
                default:
                    {
                        cmdOut = CO.FormatCmdOut(TargetSignID.GetBytes(), cmdOutput, 0xff);
                        if (NeedThrough.Checked) cmdOut = CO.CmdThroughRm(cmdOut, ThroughSignID.GetBytes(), CmdOutput.ROUTING_THROUGH);
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
                    ToMessageStatus($"{reply.Item2}");
                    if (cmdOutput == CmdOutput.ONLINE
                        && Methods.CheckResult(reply.Item1) != RmResult.Ok)
                    {
                        ToMessageStatus($"{Methods.CheckResult(reply.Item1)}");
                        continue;
                    }

                    NotifyMessage.BalloonTipTitle = "Кнопка отработана";
                    NotifyMessage.BalloonTipText = $"Успешно отправлена команда {cmdOutput}";
                    NotifyMessage.ShowBalloonTip(10);

                    break;
                }
                catch (Exception ex) { ToMessageStatus(ex.Message); }
                finally { await Task.Delay((int)AutoExtraButtonsTimeout.Value); }
            }
            while (AutoExtraButtons.Checked);
        }

        private void HexUploadFilename_DoubleClick(object sender, EventArgs e) => PasswordBox.Visible = !PasswordBox.Visible;
        private void ConfigDataGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
            => e.Cancel = ConfigDataGrid[0, e.Row.Index].Value is null;
        private void ClearGridButton_Click(object sender, EventArgs e)
            => BeginInvoke((MethodInvoker)(() => {
                for (int i = 0; i < ConfigDataGrid.Rows.Count - 1; i++)
                    if (Convert.ToBoolean(ConfigDataGrid[(int)ConfigColumns.enabled, i].Value))
                    {
                        ConfigDataGrid[(int)ConfigColumns.ConfigLoad, i].Value = null;
                        ConfigDataGrid[(int)ConfigColumns.ConfigUpload, i].Value = null;
                    }
            }));

    }
}
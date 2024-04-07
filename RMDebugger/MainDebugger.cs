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

namespace RMDebugger
{
    public partial class MainDebugger : Form
    {
        Socket udpGate = new Socket(SocketType.Dgram, ProtocolType.Udp);
        public HexUpdate windowUpdate = null;
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
            ManualGetNear.Click += GetNearClick;
            AutoGetNear.Click += GetNearClick;
            MirrorColorButton.Click += (s, e) =>
            {
                if (MirrorColor.ShowDialog() == DialogResult.OK)
                    mirClr = MirrorColor.Color;
            };
            TypeFilterBox.SelectedIndexChanged += (s, e) => Options.typeOfGetNear = TypeFilterBox.Text;
            Options.timeoutDistTof = DistToftimeout.Value;
            HexUploadButton.Click += HexUploadButtonClick;
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
            int timeout = 250;
            using Ping ping = new Ping();
            byte[] buffer = new byte[32];
            PingOptions pingOptions = new PingOptions(buffer.Length, true);
            if (!IPAddress.TryParse(IPaddressBox.Text, out IPAddress ip)) return;
            PingReply reply = await ping.SendPingAsync(ip, timeout, buffer, pingOptions);
            PingSettings(reply.Status == IPStatus.Success);
            if (reply.Status != IPStatus.Success) return;
            try
            {
                for (int reconnect = 0; reconnect < 5 && Options.pingOk;)
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
            if (windowUpdate != null) windowUpdate.Enabled = !sw;
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
                        rows[rows.Count - 1].Height = 17;
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
            if (windowUpdate != null) windowUpdate.Enabled = !sw;
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
                    rows[rows.Count - 1].Height = 17;
                    rows[rows.Count - 1].DefaultCellStyle.BackColor = mirrorList.Contains(key) ? mirClr : Color.White;
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
            Options.HexUploadStarted = !Options.HexUploadStarted;
            if (Options.HexUploadStarted)
            {
                AfterHexUploadEvent(true);
                offTabsExcept(RMData, HexUpdatePage);
                await Task.Run(() => HexUploadAsync());
                Options.HexUploadStarted = false;
                AfterHexUploadEvent(false);
                onTabPages(RMData);
            }
            else return;
        }
        private void AfterHexUploadEvent(bool sw)
        {
            AfterAnyAutoEvent(sw);
            HexPathBox.Enabled = 
                HexPageSize.Enabled = 
                HexPathButton.Enabled =
                HexUpdateInAWindow.Enabled = 
                SignaturePanel.Enabled = !sw;
            HexUploadButton.Text = sw ? "Stop" : "Upload";
            HexUploadButton.Image = sw ? Resources.StatusStopped : Resources.StatusRunning;
            UpdateBar.Value = 0; 
            BytesStart.Text = "0";
            if (windowUpdate != null) windowUpdate.Enabled = !sw;

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
                while (tstop.Seconds < Options.awaitCorrectHexUpload && Options.HexUploadStarted)
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
            for (int i = 0; i <= hex.Length - 1 && Options.HexUploadStarted;)
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
            if (Options.HexUploadStarted)
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


































        private void TaskForChangedRows()
        {
            StartTestRMButton.Enabled =
                SaveLogTestRS485.Enabled =
                StatusRM485GridView.Rows.Count > 0;
            ToMessageStatus($"{StatusRM485GridView.Rows.Count} devices on RM Test.");
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
        private void BackToDefaults()
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                RMData.Enabled = Options.mainIsAvailable;
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
                ThroughOrNot();
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
        

        async private void RefreshRMButton_Click(object sender, EventArgs e)
        {
            if (windowUpdate != null) windowUpdate.Enabled = false;
            await Task.Run(() => AddToStatusGrid(new Searching(Options.mainInterface)));
        }
        
        private void StatusGridView_DoubleClick(object sender, EventArgs e) => StatusRM485GridView.ClearSelection();
        private void StatusGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) => TaskForChangedRows();
        private void StatusGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) => TaskForChangedRows();
        private void GetNearGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) => ToMessageStatus($"{GetNearGrid.Rows.Count} devices on Get Near.");
        private void GetNearGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) => ToMessageStatus($"{GetNearGrid.Rows.Count} devices on Get Near.");
        private void DistTofGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) => ToMessageStatus($"{DistTofGrid.Rows.Count} devices on Dist tof.");
        private void DistTofGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) => ToMessageStatus($"{DistTofGrid.Rows.Count} devices on Dist tof.");
        private void ClearDataStatusRM_Click(object sender, EventArgs e)
        {
            StatusRM485GridView.Rows.Clear();
            realTimeWorkingTest = 0;
            ChangeWorkTestTime(realTimeWorkingTest);
        }

        private void AboutButton_Click(object sender, EventArgs e) => new AboutInfo().ShowDialog();


        

        async private Task<Dictionary<int, int>> GetDeviceListInfo(Searching search, CmdOutput cmdOutput, byte[] rmSign)
        {
            byte ix = 0x00;
            int iteration = 1;
            byte[] rmThrough = ThroughSignID.GetBytes();
            bool through = NeedThrough.Checked;
            Dictionary<int, int> dataReturn = new Dictionary<int, int>();
            try
            {
                do
                {
                    Tuple<byte, Dictionary<int, int>> data = await search.RequestAndParseNew(cmdOutput, ix, rmSign, rmThrough, through);
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
            if (StartTestRMButton.Text == "&Start Test" || !Options.mainIsAvailable) return false;
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
                task = Task.Run(() => RMStatusTestTask(new ForTests(Options.mainInterface), dataFromGrid[devInterface]));
                tasks.Add(task);
            }
            ToMessageStatus($"Devices on test: {StatusRM485GridView.Rows.Count}");
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
                    await RMStatusTestTask(new ForTests(port), data);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        async private Task SocketForRMTest(ushort port, Dictionary<int, Tuple<int, int, DevType>> data)
        {
            try
            {
                using Socket sock = new Socket(SocketType.Dgram, ProtocolType.Udp);
                if (IPAddress.TryParse(IPaddressBox.Text, out IPAddress address))
                {
                    sock.Connect(address, port);
                    await RMStatusTestTask(new ForTests(sock), data);
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
                        byte[] rmSign = key.GetBytes();
                        Enum.TryParse(StatusRM485GridView[(int)RS485Columns.Type, dataGrid[key].Item1].Value.ToString(), out DevType devType);
                        List<int> replyCodes;

                        /*if (BufferTestBox.Checked)
                        {
                            if (test.Sock.Connected) Methods.FlushBuffer(test.Sock);
                            if (test.Port.IsOpen) Methods.FlushBuffer(test.Port);
                        }*/

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
                /*&& (test.Sock.Connected || test.Port.IsOpen)*/
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
        private async Task<Dictionary<string, Dictionary<int, Tuple<int, DevType>>>> GetDevicesInfo(Searching search, string devInterface, Dictionary<int, int> data)
        {
            Dictionary<string, Dictionary<int, Tuple<int, DevType>>> _data = new Dictionary<string, Dictionary<int, Tuple<int, DevType>>>();
            Dictionary<int, Tuple<int, DevType>> extData = new Dictionary<int, Tuple<int, DevType>>();
            //sign, <ver, type>
            foreach (int key in data.Keys)
            {
                Tuple<byte[], ProtocolReply> replyes;
                byte[] cmdOut = search.FormatCmdOut(key.GetBytes(), CmdOutput.STATUS, 0xff);
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
        async private Task AddToStatusGrid(Searching search)
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                SerUdpPages.Enabled = false;
                RS485Page.Enabled = false;
                SignaturePanel.Enabled = false;
            }));
            offTabsExcept(RMData, TestPage);
            offTabsExcept(TestPages, RS485Page);

            Dictionary<int, int> data = await GetDeviceListInfo(search, CmdOutput.GRAPH_GET_NEAR, TargetSignID.GetBytes());
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
            ToMessageStatus($"Added:{devicesAdded}");
            BackToDefaults();
        }
        async private Task AddRangeToStatusGrid(Searching search)
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
                    ToMessageStatus($"Signature: {i}");
                    replyes = await search.GetData(
                        search.FormatCmdOut(i.GetBytes(),
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
                Searching search = new Searching(Options.mainInterface);
                byte[] cmdOut = search.FormatCmdOut(TargetSignID.GetBytes(), CmdOutput.STATUS, 0xff);
                Tuple<byte[], ProtocolReply> replyes = null;
                try
                {
                    replyes = await search.GetData(cmdOut, (int)CmdMaxSize.STATUS, 50);
                    AddToGridTest(devInterface, (int)TargetSignID.Value,
                        search.GetType(replyes.Item1),
                        search.GetVersion(replyes.Item1));
                }
                catch (Exception ex) { ToMessageStatus(ex.Message); }
            }
        }

        private void AddToGridTest(string devInterface, int signature, DevType type, int version) =>
            Invoke((MethodInvoker)(() => { StatusRM485GridView.Rows.Add(
                devInterface, signature, type, "-", 0, 0, 0, 0, 0, 0, 0, 0, 0, version);
            }));

        private void HexUpdateInAWindow_Click(object sender, EventArgs e)
        {
            windowUpdate = new HexUpdate();
            HexUpdatePage.Enabled = false;
            windowUpdate.Show();
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
            Information info = new Information(Options.mainInterface);
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
                    Dictionary<int, int> data = await GetDeviceListInfo(info, dict[e.Node.Name], TargetSignID.GetBytes());
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
                    byte[] cmdOut = !NeedThrough.Checked ? info.GetInfo(TargetSignID.GetBytes(), dict[e.Node.Name]) :
                    info.GetInfo(TargetSignID.GetBytes(), ThroughSignID.GetBytes(), dict[e.Node.Name]);
                    reply = await info.GetData(cmdOut, size, 100);
                    byte[] cmdIn = !NeedThrough.Checked ? reply.Item1 : info.ReturnWithoutThrough(reply.Item1);
                    Dictionary<string, string> data = info.CmdInParse(cmdIn);
                    ToMessageStatus(reply.Item2.ToString());
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
            catch (Exception ex) { ToMessageStatus(ex.Message); }
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
                await Task.Run(() => AddRangeToStatusGrid(new Searching(Options.mainInterface)));
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
            await Task.Run(() => LoadField(new Configuration(Options.mainInterface), dict));
            BackToDefaults();
        }
        async private void UploadConfigButton_Click(object sender, EventArgs e)
        {
            if (windowUpdate != null) windowUpdate.Enabled = false;
            Dictionary<string, int> dict = GetEnabledFields();
            if (dict is null) return;
            offTabsExcept(RMData, null);
            OffControlsForConfig();
            await Task.Run(() => UploadField(new Configuration(Options.mainInterface), dict));
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
                    config.ConfigLoad(TargetSignID.GetBytes(), ThroughSignID.GetBytes(), key) :
                    config.ConfigLoad(TargetSignID.GetBytes(), key);

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
                        ToMessageStatus($"{key} : {replyes.Item2}");
                        break;
                    }
                    catch (Exception ex)
                    {
                        ToMessageStatus(ex.Message);
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
                    config.ConfigUploadNew(TargetSignID.GetBytes(), ThroughSignID.GetBytes(), key, value, maxSize, factory) :
                    config.ConfigUploadNew(TargetSignID.GetBytes(), key, value, maxSize, factory);

                Tuple<RmResult, ProtocolReply> replyes = null;

                while (tstop.Seconds < RMPTimeout.Value)
                {
                    tstop = DateTime.Now - t0;
                    try
                    {
                        replyes = await config.GetResult(cmdOut, NeedThrough.Checked ? (int)CmdMaxSize.ONLINE + 4 : (int)CmdMaxSize.ONLINE);
                        ToMessageStatus($"{key} : {replyes.Item1}");
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
                        ToMessageStatus(ex.Message);
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
            if (Options.debugForm is null)
            {
                Options.debugForm = new DataDebuggerForm();
                Options.debugForm.Show();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e) 
            => this.Close();

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) => new AboutInfo().ShowDialog();

    }
}
using Debugger;
using Enums;
using FileVerifier;
using Microsoft.Win32;
using RMDebugger.Main.Properties;
using StaticSettings;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMDebugger.Main
{
    public partial class MainForm : Form
    {
        Socket udpGate = new Socket(SocketType.Dgram, ProtocolType.Udp);
        private readonly string mainName = Assembly.GetEntryAssembly().GetName().Name;
        string ver;

        CancellationTokenSource pingToken;

        public MainForm()
        {
            InitializeComponent();
            Options.debugger = this;
            NotifyMessage.Text =
                this.Text =
                $"{Assembly.GetEntryAssembly().GetName().Name} v{Assembly.GetEntryAssembly().GetName().Version}";
            AddEvents();
        }
        private void AddEvents()
        {
            AddSearchEvents();
            AddUploaderEvents();
            AddInfoEvents();
            AddConfigEvents();
            AddSettingsRMLREvents();
            AddTestRS485Events();
            AddExtraButtonsEvents();

            Load += MainFormLoad;
            FormClosed += MainFormClosed;
            DinoRunningProcessOk.Click += (s, e) => Options.activeToken?.Cancel();

            KeyDown += (s, e) =>
            {
                if (e.KeyValue == (char)Keys.F11)
                {
                    if (this.WindowState == FormWindowState.Normal)
                        this.WindowState = FormWindowState.Maximized;
                    else
                        this.WindowState = FormWindowState.Normal;
                }
                else if (e.KeyValue == (char)Keys.Escape) Options.activeToken?.Cancel();
            };

            windowPinToolStrip.CheckedChanged += (s, e) =>
            {
                this.TopMost = windowPinToolStrip.Checked;
                windowPinToolStrip.ToolTipText = windowPinToolStrip.Checked ? "Поверх других окон." : "Обычное состояние окна.";
            };
            transparentToolStrip.CheckedChanged += (s, e) => this.Opacity = transparentToolStrip.Checked ? 0.95 : 1;
            messagesToolStrip.CheckedChanged += (s, e) => Options.showMessages = messagesToolStrip.Checked;
            extendedSettingsToolStrip.CheckedChanged += (s, e) =>
            {
                ResetButton.Visible =
                SleepButton.Visible =
                SetBootloaderStopButton.Visible =
                SetBootloaderStartButton.Visible =
                HexExtendedPanel.Visible = extendedSettingsToolStrip.Checked;
            };

            comPort.SelectedIndexChanged += (s, e) => mainPort.PortName = comPort.SelectedItem.ToString();
            BaudRate.SelectedIndexChanged += (s, e) => BaudRateSelectedIndexChanged(s, e);
            RefreshSerial.Click += (s, e) => AddPorts(comPort);

            foreach (ToolStripMenuItem item in PriorityToolStripMenuItem.DropDownItems) item.Click += SetPriority;
            foreach (ToolStripDropDownItem item in dataBits.DropDownItems) item.Click += DataBitsForSerial;
            foreach (ToolStripDropDownItem item in Parity.DropDownItems) item.Click += ParityForSerial;
            foreach (ToolStripDropDownItem item in stopBits.DropDownItems) item.Click += StopBitsForSerial;
            OpenCom.Click += OpenComClick;
            PingButton.Click += PingButtonClick;
            numericPort.ValueChanged += (s, e) => { if (Options.pingOk) pingToken?.Cancel(); };
            Connect.Click += ConnectClick;
            NeedThrough.CheckedChanged += NeedThroughCheckedChanged;
            TargetSignID.ValueChanged += (s, e) => NeedThrough.Enabled = TargetSignID.Value != 0;

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
        }

        private void HideConfigPanelClick(bool sw)
        {
            HideConfigPanel.Tag = sw ? "Unhided" : "Hided";
            HideConfigPanel.Image = sw ? Resources.Unhide : Resources.Hide;
            ConfigPanel.Location = new Point(0, sw ? ConfigPanel.Location.Y - 113 : ConfigPanel.Location.Y + 113);
        }
        //********************
        private void MainFormLoad(object sender, EventArgs e)
        {
            CheckUpdates();
            ComDefault();
            AddPorts(comPort);
            CheckReg();
            SetProperties();
            HideConfigPanelClick((string)HideConfigPanel.Tag == "Hided");
            SettingsRmlrGridDefault();
        }
        private void SetProperties()
        {
            NeedThrough.Checked = Settings.Default.ThroughRM485;
            TargetSignID.Value = Settings.Default.MainSignatureID;
            ThroughSignID.Value = Settings.Default.ThroughSignatureID;
            numericPort.Value = Settings.Default.UDPGatePort;
            HexPageSize.Value = Settings.Default.LastPageSize;
            IPaddressBox.Text = Settings.Default.UDPGateIP;
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
        private void SetPriority(object sender, EventArgs e)
        {
            ToolStripMenuItem toolstrip = (ToolStripMenuItem)sender;
            Process proc = Process.GetCurrentProcess();
            ProcessPriorityClass thisPriority = proc.PriorityClass;
            if (Enum.TryParse(toolstrip.Tag.ToString(), out ProcessPriorityClass newPriority))
            {
                foreach (ToolStripMenuItem item in PriorityToolStripMenuItem.DropDownItems)
                    item.CheckState = CheckState.Unchecked;
                if (toolstrip.Name == "RealTimeToolItem" && thisPriority != ProcessPriorityClass.RealTime)
                {
                    DialogResult message =
                        MessageBox.Show(this,
                        "Вы уверены что хотите этого? Возможны проблемы в работе с устройствами USB.",
                        "Set Priority", MessageBoxButtons.YesNo);
                    newPriority = message == DialogResult.No
                        ? thisPriority
                        : ProcessPriorityClass.RealTime;
                }
                proc.PriorityClass = proc.PriorityClass != newPriority ? newPriority : proc.PriorityClass;
                foreach (ToolStripMenuItem item in PriorityToolStripMenuItem.DropDownItems)
                    if (item.Tag.ToString() == proc.PriorityClass.ToString())
                    {
                        item.CheckState = CheckState.Checked;
                        break;
                    }
            }
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
        private void ToMessageStatus(string msg)
            => BeginInvoke((MethodInvoker)(() => MessageStatus.Text = msg));
        private void ToReplyStatus(ProtocolReply reply)
            => BeginInvoke((MethodInvoker)(() =>
        {

            ReplyStatus.Text = reply.ToString();
            DinoRunningProcessOk.Enabled = reply == ProtocolReply.Ok;
        }));
        private void ToReplyStatus()
            => BeginInvoke((MethodInvoker)(() => ReplyStatus.Text = ""));
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
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = cmd,
                WindowStyle = ProcessWindowStyle.Hidden,
            });
        private bool Internet()
        {
            try
            {
                Dns.GetHostEntry("drive.google.com");
                return true;
            }
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
            Invoke((MethodInvoker)(() =>
            {
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
                Invoke((MethodInvoker)(() =>
                {
                    NeedThrough.Checked = Convert.ToBoolean(rKey.GetValue("ThroughRM485"));
                    TargetSignID.Value = Convert.ToUInt16(rKey.GetValue("MainSignatureID"));
                    HexPageSize.Value = Convert.ToInt32(rKey.GetValue("LastPageSize"));
                    IPaddressBox.Text = rKey.GetValue("UDPGateIP").ToString();
                    numericPort.Value = Convert.ToUInt16(rKey.GetValue("UDPGatePort"));
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
                Invoke((MethodInvoker)(() =>
                {
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
            if (Options.pingOk) { pingToken?.Cancel(); return; }
            pingToken = new CancellationTokenSource();
            await check_ip(pingToken);
        }
        private void IPaddressBox_TextChanged(object sender, EventArgs e)
        {
            if (Options.pingOk)
            {
                pingToken?.Cancel();
                Connect.Enabled = false;
            }
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
        async private Task check_ip(CancellationTokenSource cts)
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
                for (int reconnect = 0; reconnect <= 5 && Options.pingOk/* && !cts.IsCancellationRequested*/;)
                {
                    reply = await ping.SendPingAsync(ip, 1000, buffer, pingOptions);

                    if (reply.Status != IPStatus.Success)
                    {
                        reconnect++;
                        continue;
                    }
                    else if (reconnect > 0) reconnect = 0;
                    await Task.Delay(1000, cts.Token);
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex) { ToMessageStatus(ex.Message.ToString()); }
            finally
            {
                if (udpGate.Connected) ConnectClick(null, null);
                Options.activeToken?.Cancel();
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
                rmlrPage.Enabled = !Options.through;
            SearchFindSignatireMode.Enabled =
                SearchExtendedFindMode.Enabled = !Options.through && SearchGetNear.Checked;
            ThroughSignID.Enabled = Options.through;
        }

        // Off\On tabs
        private void offTabsExcept(TabControl tab, TabPage exc)
        {
            Action action = () =>
            {
                ToMessageStatus("");
                ToReplyStatus();
                foreach (TabPage t in tab.TabPages)
                {
                    if (t == exc)
                    {
                        t.Text = $"▶ {t.Text}";
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
                    page.Text = page.Text.Replace("▶ ", "");
                }
            };
            if (InvokeRequired) BeginInvoke(action);
            else action();
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
                Options.activeProgress =
                DinoRunningProcessOk.Visible = sw;
        }
    }
}
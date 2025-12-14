using Enums;
using RMDebugger.Main.Properties;
using SearchProtocol;
using StaticSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMDebugger.Main
{
    public partial class MainForm : Form
    {
        BindingList<DeviceClass> testerData = new BindingList<DeviceClass>();

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
            foreach (DeviceClass device in testerData)
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
            void AfterStartTestRSEvent(bool sw)
            {
                AfterAnyAutoEvent(sw);
                StatusRS485GridView.AllowUserToDeleteRows =
                    SignaturePanel.Enabled =
                    AutoScanToTest.Enabled =
                    scanGroupBox.Enabled =
                    settingsGroupBox.Enabled =
                    ClearDataTestRS485.Enabled =
                    timerPanelTest.Enabled =
                    SortByButton.Enabled = !sw;
                StatusRS485GridView.Cursor = sw ? Cursors.AppStarting : Cursors.Default;
                StartTestRSButton.Text = sw ? "&Stop" : "&Start Test";
                StartTestRSButton.Image = sw ? Resources.StatusStopped : Resources.StatusRunning;
            }
            if (Options.activeProgress) { Options.activeToken?.Cancel(); return; }
            Options.activeToken = new CancellationTokenSource();
            Options.activeTask = Task.Run(() => StartTaskRS485Test());
            AfterStartTestRSEvent(true);
            offTabsExcept(RMData, TestPage);
            offTabsExcept(TestPages, RS485Page);
            ToMessageStatus($"Device count: {testerData.Count}");
            try { await Options.activeTask; } catch { }
            RefreshGridTask();
            WorkTestTimer.Stop();
            onTabPages(RMData);
            onTabPages(TestPages);
            AfterStartTestRSEvent(false);
            Options.activeTask = null;
        }

        async private Task StartTaskRS485Test()
        {
            GetDevices(out Dictionary<string, List<DeviceClass>> devices);

            List<Task> tasks = new List<Task>();
            string connectedInterface = GetConnectedInterfaceString();

            if (TimerSettingsTestBox.Checked)
                Options.easyTimer = new TimeSpan(
                               (int)numericHoursTest.Value,
                               (int)numericMinutesTest.Value,
                               (int)numericSecondsTest.Value);
            Options.TesterTimer.Start();
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
            catch (OperationCanceledException) { MessageBox.Show("Задача остановлена"); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                Options.TesterTimer.Stop();
                ToMessageStatus($"Device count: {testerData.Count} | Завершено");
            }
        }

        private void ElapsedWorkTime()
        {
            TimeSpan time = TimeSpan.FromTicks(Options.TesterTimer.ElapsedTicks);
            WorkingTimeLabel.Text = $"Total: {(time.Days > 0 ? $"{time.Days}d, " : "")}" +
                                    $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}.{time.Milliseconds:000}";
            if (TimerSettingsTestBox.Checked
                && TimeSpan.FromTicks(Options.TesterTimer.ElapsedTicks) > Options.easyTimer)
            {
                Options.activeToken?.Cancel();
                if (Options.showMessages)
                    NotifyMessage.ShowBalloonTip(5,
                        "Время истекло!",
                        "Время истекло, тест завершен",
                        ToolTipIcon.Info);
            }
        }
        private void RefreshGridTask()
        {
            int ScrollLastPos = StatusRS485GridView.FirstDisplayedScrollingRowIndex;
            testerData.ResetBindings();
            StatusRS485GridView.ClearSelection();
            StatusRS485GridView.Refresh();
            if (testerData.Count > ScrollLastPos && ScrollLastPos >= 0)
                StatusRS485GridView.FirstDisplayedScrollingRowIndex = ScrollLastPos;
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
                }
                ;
            }
        }
        async private Task StartTest(ForTests forTests)
        {
            forTests.TestDebug += ToDebuggerWindow;
            forTests.TimeRefresh += ElapsedWorkTime;
            forTests.clearAfterError = ClearBufferSettingsTestBox.Checked;
            Random random = new Random();
            do
            {
                int getTest = random.Next(0, 101);
                foreach (DeviceClass device in forTests.ListDeviceClass)
                {
                    if (Options.activeToken.IsCancellationRequested || !Options.mainIsAvailable) return;
                    if (getTest == 0 && RadioSettingsTestBox.Checked)
                        await forTests.GetRadioDataFromDevice(device, CmdOutput.GRAPH_GET_NEAR);
                    else if (getTest == 100)
                        await forTests.GetRadioDataFromDevice(device, CmdOutput.ONLINE_DIST_TOF);
                    else if (getTest == 99 && RadioSettingsTestBox.Checked)
                        await forTests.GetDataFromDevice(device, CmdOutput.GRAPH_WHO_ARE_YOU);
                    else if (getTest >= 75)
                        await forTests.GetDataFromDevice(device, CmdOutput.STATUS);
                    else await forTests.GetDataFromDevice(device, CmdOutput.ROUTING_GET);
                }
            }
            while (Options.activeProgress && !Options.activeToken.IsCancellationRequested);
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
            if (Options.activeProgress) { Options.activeToken?.Cancel(); return; }
            Options.activeToken = new CancellationTokenSource();
            using (Searching search = new Searching(Options.mainInterface))
            {
                Options.activeToken = new CancellationTokenSource();
                search.ToReply += ToReplyStatus;
                search.ToDebug += ToDebuggerWindow;
                try { AddToGridDevice(await GetDeviceInfo(search, (ushort)TargetSignID.Value)); }
                catch { }
            }
        }

        // //Manual scan
        async private void ManualScanToTestClick(object sender, EventArgs e)
        {
            void AfterRS485ManualScanEvent(bool sw)
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
            if (Options.activeProgress) { Options.activeToken?.Cancel(); return; }
            Options.activeToken = new CancellationTokenSource();
            AfterRS485ManualScanEvent(true);
            offTabsExcept(RMData, TestPage);
            offTabsExcept(TestPages, RS485Page);
            await ManualScanRange();
            onTabPages(RMData);
            onTabPages(TestPages);
            AfterRS485ManualScanEvent(false);
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
                    for (int i = (int)minSigToScan.Value, devs = 0; i <= maxSigToScan.Value && !Options.activeToken.IsCancellationRequested; i++)
                    {
                        string deviceCount = devs > 0 ? $" (Catched: {devs})" : "";
                        try
                        {
                            ToMessageStatus($"Signature: {i} {deviceCount}");
                            devices.Add(await GetDeviceInfo(search, (ushort)i));
                            devs++;
                        }
                        catch { }
                    }
                    return devices;
                }
                ;
            });
            AddToGridDevices(devices);
        }
        private void minSigToScan_ValueChanged(object sender, EventArgs e)
        {
            maxSigToScan.Minimum = minSigToScan.Value;
            if (minSigToScan.Value > maxSigToScan.Value)
                maxSigToScan.Value = minSigToScan.Value;
        }

        // //Auto scan
        async private void AutoScanToTestClick(object sender, EventArgs e)
        {
            void AfterRS485AutoScanEvent(bool sw)
            {
                AfterAnyAutoEvent(sw);
                StatusRS485GridView.Cursor = sw ? Cursors.WaitCursor : Cursors.Default;
                StatusRS485GridView.AllowUserToDeleteRows =
                    extendedMenuPanel.Enabled = !sw;
            }
            if (Options.activeProgress) { Options.activeToken?.Cancel(); return; }
            Options.activeToken = new CancellationTokenSource();
            AfterRS485AutoScanEvent(true);
            offTabsExcept(RMData, TestPage);
            offTabsExcept(TestPages, RS485Page);
            await AutoScanAdd();
            onTabPages(RMData);
            onTabPages(TestPages);
            AfterRS485AutoScanEvent(false);
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
                            if (!Enum.IsDefined(typeof(RS485Type), device.devType.ToString())) continue;
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
        // //ExtendedMenu
        private void ClearDataStatusRM_Click(object sender, EventArgs e) => testerData?.Clear();
        private void ClearInfoTestRS485Click(object sender, EventArgs e)
        {
            if (Options.TesterTimer.IsRunning)
                Options.TesterTimer.Restart();
            else Options.TesterTimer.Reset();
            foreach (DeviceClass device in testerData)
                device.Reset();
            ElapsedWorkTime();
            RefreshGridTask();
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
    }
}

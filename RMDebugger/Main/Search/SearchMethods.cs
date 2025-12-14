using Enums;
using RMDebugger.Main.Properties;
using SearchProtocol;
using StaticSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMDebugger.Main
{
    public partial class MainForm : Form
    {
        Color mirClr = Color.PaleGreen;
        BindingList<DeviceData> deviceData = new BindingList<DeviceData>();

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
            void AfterSearchEvent(bool sw, bool manual)
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
            if (Options.activeProgress) { Options.activeToken?.Cancel(); return; }
            Button btn = (Button)sender;
            bool manual = btn == SearchManualButton;
            Options.activeToken = new CancellationTokenSource();
            Options.activeTask = Task.Run(() => StartSearchAsync(!manual));
            AfterSearchEvent(true, manual);
            offTabsExcept(RMData, SearchPage);
            try { await Options.activeTask; } catch { }
            onTabPages(RMData);
            AfterSearchEvent(false, manual);
            Options.activeTask = null;
        }

        async private Task StartSearchAsync(bool auto)
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
                                if (!Enum.IsDefined(typeof(RS485Type), device.devType.ToString())) continue;
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
                        List<DeviceData> dataDistTof = await search.GetDataFromDevice(CmdOutput.ONLINE_DIST_TOF, TargetSignID.GetBytes(), ThroughSignID.GetBytes());
                        if (data.Count == 0)
                            data.AddRange(dataDistTof);
                        else
                        {
                            foreach (DeviceData device in dataDistTof)
                            {
                                DeviceData devIn = data.OfType<DeviceData>().FirstOrDefault(x => x.devSign == device.devSign);
                                if (devIn != null)
                                {
                                    devIn.devDist = device.devDist;
                                    devIn.devRSSI = device.devRSSI;
                                }
                            }
                        }
                    }
                    data = data.OrderBy(s => s.devSign).GroupBy(a => a.devSign).Select(g => g.First()).ToList();

                    if (SearchFilterMode.Checked && SearchFilterMode.Enabled && Options.devTypesSearch.Count > 0)
                        data = data.Where(x => Options.devTypesSearch.Contains(x.devType)).ToList();

                    Action action = () =>
                    {
                        int ScrollLastPos = SearchGrid.FirstDisplayedScrollingRowIndex;
                        deviceData.Clear();
                        bool find = SearchFindSignatireMode.Checked && SearchFindSignatireMode.Enabled;
                        foreach (DeviceData dData in data)
                        {
                            deviceData.Add(dData);
                            if (find)
                                SearchGrid.Rows[data.IndexOf(dData)].DefaultCellStyle.BackColor =
                                    dData.inOneBus ? mirClr : Color.White;
                        }

                        if (deviceData.Count > ScrollLastPos && ScrollLastPos >= 0)
                            SearchGrid.FirstDisplayedScrollingRowIndex = ScrollLastPos;
                    };
                    if (InvokeRequired) Invoke(action);
                    else action();

                    if (SearchKnockMode.Checked && data.Count > 0)
                    {
                        Options.activeToken?.Cancel();
                        if (Options.showMessages)
                            NotifyMessage.ShowBalloonTip(5, "Тук-тук!", $"Ответ получен!", ToolTipIcon.Info);
                    }
                    await Task.Delay(auto ? Options.timeoutSearch : 50, Options.activeToken.Token);
                }
                while (auto && Options.activeProgress && !Options.activeToken.IsCancellationRequested);
            }
        }
        async private Task<bool> ThisDeviceInOneBus(CommandsOutput search, DeviceData device)
        {
            try
            {
                await Task.Delay(1);
                await search.GetData(search.FormatCmdOut(device.devSign.GetBytes(), CmdOutput.STATUS, 0xff), (int)CmdMaxSize.STATUS, 50);
                return true;
            }
            catch { return false; }
        }
    }
}

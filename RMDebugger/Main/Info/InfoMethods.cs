using CSV;
using Enums;
using SearchProtocol;
using StaticSettings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMDebugger.Main
{
    public partial class MainForm : Form
    {
        async private void InfoTreeNodeClick(object sender, EventArgs e)
        {
            void AfterInfoEvent(bool sw)
            {
                AfterAnyAutoEvent(sw);
                InfoGetInfoButton.Text = sw ? "Stop" : "Get Info";
            }
            if (Options.activeProgress) { Options.activeToken?.Cancel(); return; }
            Options.activeToken = new CancellationTokenSource();
            AfterInfoEvent(true);
            offTabsExcept(RMData, InfoPage);
            do
            {
                try
                {
                    InfoTree.BeginUpdate();
                    foreach (TreeNode node in InfoTree.Nodes)
                    {
                        if (node.Name == InfoWhoAreYouToolStrip.Tag && InfoWhoAreYouToolStrip.Checked)
                            await Task.Run(() => GetInfoAboutDevice(CmdOutput.GRAPH_WHO_ARE_YOU, node));
                        else if (node.Name == InfoStatusToolStrip.Tag && InfoStatusToolStrip.Checked)
                            await Task.Run(() => GetInfoAboutDevice(CmdOutput.STATUS, node));
                        else if (node.Name == InfoGetNearToolStrip.Tag && InfoGetNearToolStrip.Checked)
                            await Task.Run(() => GetInfoNearFromDevice(node));
                        await Task.Delay(1);
                    }
                    InfoTree.EndUpdate();
                    await Task.Delay(InfoAutoCheckBox.Checked ? (int)InfoTimeout.Value : 50, Options.activeToken.Token);
                }
                catch { }
            }
            while (!Options.activeToken.IsCancellationRequested && InfoAutoCheckBox.Checked);
            if (Options.activeToken.IsCancellationRequested)
                InfoTree.EndUpdate();
            AfterInfoEvent(false);
            onTabPages(RMData);
        }
        async private Task GetInfoAboutDevice(CmdOutput cmdOutput, TreeNode treeNode)
        {
            using (Information info = NeedThrough.Checked
                ? new Information(Options.mainInterface, TargetSignID.GetBytes(), ThroughSignID.GetBytes())
                : new Information(Options.mainInterface, TargetSignID.GetBytes()))
            {
                info.ToReply += ToReplyStatus;
                info.ToDebug += ToDebuggerWindow;
                ToMessageStatus($"Sign:{TargetSignID.Value}, {cmdOutput}");
                byte[] cmdOut = info.buildCmdDelegate(cmdOutput);
                int size = !NeedThrough.Checked ? (int)cmdOutput : (int)cmdOutput + 4;
                async Task<List<TreeNode>> GetInfo()
                {
                    List<TreeNode> typeNodesList;
                    CancellationTokenSource timeoutToken = new CancellationTokenSource(500);
                    do
                    {
                        typeNodesList = new List<TreeNode>();
                        try
                        {
                            Tuple<byte[], ProtocolReply> reply = await info.GetData(cmdOut, size);
                            byte[] cmdIn = info.ReturnWithoutThrough(reply.Item1);
                            Dictionary<string, string> data = info.CmdInParse(cmdIn);
                            data.Add($"{infoEnum.Date}", DateTime.Now.ToString("dd-MM-yy HH:mm"));

                            foreach (PropertyInfo info in Options.infoData.GetType().GetProperties())
                                if (data.ContainsKey(info.Name))
                                    info.SetValue(Options.infoData, data[info.Name], null);

                            foreach (string str in data.Keys)
                            {
                                bool csvInfo = Enum.IsDefined(typeof(infoEnum), str);
                                typeNodesList.Add(
                                    new TreeNode($"{str}: {data[str]}")
                                    {
                                        ToolTipText = csvInfo ? "Данные для csv" : "",
                                        ForeColor = csvInfo ? Color.ForestGreen : Color.Black,
                                    });
                            }
                            break;
                        }
                        catch { await Task.Delay(10); }
                    }
                    while (!Options.activeToken.IsCancellationRequested && !timeoutToken.IsCancellationRequested);
                    return typeNodesList;
                }
                List<TreeNode> newNodes = await GetInfo();
                Invoke((MethodInvoker)(() =>
                {
                    treeNode.Nodes.Clear();
                    treeNode.Nodes.AddRange(newNodes.ToArray());
                }));
            }
            ;
        }
        async private Task GetInfoNearFromDevice(TreeNode treeNode)
        {
            using (Searching search = new Searching(Options.mainInterface))
            {
                search.ToReply += ToReplyStatus;
                search.ToDebug += ToDebuggerWindow;
                ToMessageStatus($"Sign:{TargetSignID.Value}, GRAPH_GET_NEAR");
                async Task<TreeNode> GetNear()
                {
                    List<DeviceData> data = await search.GetDataFromDevice(CmdOutput.GRAPH_GET_NEAR, TargetSignID.GetBytes(), ThroughSignID.GetBytes());
                    Options.infoData.Radio = data.Count > 0 ? "Ok" : "Empty";
                    TreeNode getnear = new TreeNode($"{(infoEnum)3}: {Options.infoData.Radio}")
                    {
                        ToolTipText = "Данные для csv",
                        ForeColor = Color.ForestGreen
                    };

                    if (data.Count > 0)
                    {
                        data = data.OrderBy(s => s.devSign).GroupBy(a => a.devSign).Select(g => g.First()).ToList();
                        Dictionary<string, List<int>> typeNodesData = new Dictionary<string, List<int>>();
                        foreach (DeviceData device in data)
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
                    return getnear;
                }
                TreeNode newNode = await GetNear();
                Invoke((MethodInvoker)(() =>
                {
                    treeNode.Nodes.Clear();
                    treeNode.Nodes.Add(newNode);
                }));
            }
            ;
        }
        private void InfoSaveToCSVButtonClick(object sender, EventArgs e)
        {
            if (Options.infoData.haveDataForCSV)
            {
                string path = Environment.CurrentDirectory + $"\\InfoAbout_{Options.infoData.Type}.csv";
                CSVLib csv = new CSVLib(path);
                string columns = "";
                string[] columnsEnum = Enum.GetNames(typeof(infoEnum));
                foreach (string column in columnsEnum)
                    columns += column != "Date" ? $"{column};" : column;
                csv.AddFields(columns);
                csv.MainIndexes = new int[] { 0, 1, columnsEnum.Length - 1 };
                csv.WriteCsv(string.Join(";", Options.infoData.GetFieldsValue()));
            }
        }
    }
}

using ConfigurationProtocol;
using Enums;
using RMDebugger.Main.Properties;
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
        BindingList<FieldConfiguration> fieldsData = new BindingList<FieldConfiguration>();
        private string[] fieldsFounded = new string[5] { "addr", "fio", "lamp", "puid", "rmb" };

        private void ColoredRow(int index, DataGridView dgv, Color color)
                => Invoke((MethodInvoker)(async () =>
                {
                    dgv.Rows[index].DefaultCellStyle.BackColor = color;
                    await Task.Delay(1000);
                    dgv.Rows[index].DefaultCellStyle.BackColor = Color.White;
                }));

        // //Load
        async private void LoadConfigButtonClick(object sender, EventArgs e)
        {
            void AfterLoadConfigEvent(bool sw)
            {
                AfterAnyAutoEvent(sw);
                ConfigDataGrid.Enabled =
                    SignaturePanel.Enabled =
                    LoadFieldsConfigButton.Enabled =
                    UploadConfigButton.Enabled =
                    settingsBoxConfig.Enabled = !sw;
                LoadConfigButton.Text = sw ? "Stop" : "Load from fields";
                LoadConfigButton.Image = sw ? Resources.StatusStopped : Resources.CloudDownload;
            }
            bool CheckFields()
            {
                foreach (FieldConfiguration field in fieldsData)
                    if (field.fieldActive) return true;
                return false;
            }

            if (Options.activeProgress) { Options.activeToken?.Cancel(); return; }
            if (CheckFields())
            {
                Options.activeToken = new CancellationTokenSource();
                AfterLoadConfigEvent(true);
                offTabsExcept(RMData, ConfigPage);
                try { await Task.Run(() => LoadField()); } catch { }
                AfterLoadConfigEvent(false);
                onTabPages(RMData);
            }
        }
        async private Task LoadField()
        {
            Configuration config = NeedThrough.Checked
                ? new Configuration(Options.mainInterface, TargetSignID.GetBytes(), ThroughSignID.GetBytes())
                : new Configuration(Options.mainInterface, TargetSignID.GetBytes());
            config.ToReply += ToReplyStatus;
            config.ToDebug += ToDebuggerWindow;

            foreach (FieldConfiguration field in fieldsData)
            {
                if (field.fieldActive && !Options.activeToken.IsCancellationRequested)
                {
                    byte[] cmdOut = config.buildCmdLoadDelegate(field.fieldName);
                    int fieldLen = fieldsFounded.Contains(field.fieldName) ? (int)field.rule + 1 : 17;
                    int sizeData = fieldLen + (field.fieldName.Length + 1) + 6; // 6 = 2 addr + 2 cmd + 2 crc
                    if (config.through) sizeData += 4;

                    Tuple<byte[], ProtocolReply> reply;
                    byte[] cmdIn = new byte[0];
                    while (!Options.activeToken.IsCancellationRequested)
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
                        await Task.Delay((int)ConfigButtonsTimeout.Value, Options.activeToken.Token);
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
            void AfterUploadConfigEvent(bool sw)
            {
                AfterAnyAutoEvent(sw);
                ConfigDataGrid.Enabled =
                    SignaturePanel.Enabled =
                    LoadConfigButton.Enabled =
                    LoadFieldsConfigButton.Enabled =
                    settingsBoxConfig.Enabled = !sw;
                UploadConfigButton.Text = sw ? "Stop" : "Upload to fields";
                UploadConfigButton.Image = sw ? Resources.StatusStopped : Resources.CloudUpload;
            }
            bool CheckFields()
            {
                foreach (FieldConfiguration field in fieldsData)
                    if (field.fieldActive)
                        if (!string.IsNullOrEmpty(field.uploadValue)) return true;
                return false;
            }

            if (Options.activeProgress) { Options.activeToken?.Cancel(); return; }
            if (CheckFields())
            {
                Options.activeToken = new CancellationTokenSource();
                AfterUploadConfigEvent(true);
                offTabsExcept(RMData, ConfigPage);
                try { await Task.Run(() => UploadField()); } catch { }
                onTabPages(RMData);
                AfterUploadConfigEvent(false);
            }
        }
        async private Task UploadField()
        {
            Configuration config = NeedThrough.Checked
                ? new Configuration(Options.mainInterface, TargetSignID.GetBytes(), ThroughSignID.GetBytes())
                : new Configuration(Options.mainInterface, TargetSignID.GetBytes());

            config.ToReply += ToReplyStatus;
            config.ToDebug += ToDebuggerWindow;

            int sizeData = (int)CmdMaxSize.ONLINE;
            if (config.through) sizeData += 4; // +2 addr, +2 cmd

            foreach (FieldConfiguration field in fieldsData)
            {
                if (field.fieldActive && !Options.activeToken.IsCancellationRequested)
                    if (!string.IsNullOrEmpty(field.uploadValue) || config.factory)
                    {
                        if (config.factory && field.fieldName == "addr") continue;
                        Tuple<RmResult, ProtocolReply> reply;
                        byte[] cmdOut = config.buildCmdUploadDelegate(
                                            field.fieldName,
                                            field.uploadValue,
                                            fieldsFounded.Contains(field.fieldName) ? (int)field.rule + 1 : 17);
                        while (!Options.activeToken.IsCancellationRequested)
                        {
                            try
                            {
                                reply = await config.GetResult(cmdOut, sizeData);
                                ToMessageStatus($"{field.fieldName} : {reply.Item1}");
                                if (reply.Item1 == RmResult.Ok)
                                {
                                    if (field.fieldName == "addr") TargetSignID.Value = Convert.ToInt32(field.uploadValue);
                                    ColoredRow(fieldsData.IndexOf(field), ConfigDataGrid, Color.GreenYellow);
                                    break;
                                }
                                else
                                {
                                    ColoredRow(fieldsData.IndexOf(field), ConfigDataGrid, Color.Red);
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message == "devNull") return;
                            }
                            await Task.Delay((int)ConfigButtonsTimeout.Value, Options.activeToken.Token);
                        }
                    }
            }
        }

        async private void LoadFieldsConfigButtonClick(object sender, EventArgs e)
        {
            void AfterLoadFieldsEvent(bool sw)
            {
                AfterAnyAutoEvent(sw);
                ConfigDataGrid.Enabled =
                    SignaturePanel.Enabled =
                    LoadConfigButton.Enabled =
                    UploadConfigButton.Enabled =
                    settingsBoxConfig.Enabled = !sw;
                LoadFieldsConfigButton.Text = sw ? "Stop" : "Scan fields";
                LoadFieldsConfigButton.Image = sw ? Resources.StatusStopped : Resources.DatabaseSource;
            }
            if (Options.activeProgress) { Options.activeToken?.Cancel(); return; }
            Options.activeToken = new CancellationTokenSource();
            AfterLoadFieldsEvent(true);
            offTabsExcept(RMData, ConfigPage);
            using (Configuration config = NeedThrough.Checked
                ? new Configuration(Options.mainInterface, TargetSignID.GetBytes(), ThroughSignID.GetBytes())
                : new Configuration(Options.mainInterface, TargetSignID.GetBytes()))
            {
                try { await Task.Run(() => LoadFields(config)); } catch { }
            }
            AfterLoadFieldsEvent(false);
            onTabPages(RMData);
        }
        async private Task LoadFields(Configuration config)
        {
            config.ToReply += ToReplyStatus;
            config.ToDebug += ToDebuggerWindow;
            List<FieldConfiguration> newFields = new List<FieldConfiguration>();
            for (byte i = (byte)minIxScanConfig.Value; i <= (byte)maxIxScanConfig.Value && !Options.activeToken.IsCancellationRequested; i++)
            {
                byte[] cmdOut = config.buildCmdGetFieldsDelegate(i);
                Tuple<byte[], ProtocolReply> reply;
                byte[] cmdIn;
                do
                {
                    try
                    {
                        await Task.Delay((int)ConfigButtonsTimeout.Value, Options.activeToken.Token);
                        reply = await config.GetData(cmdOut, 50);
                        cmdIn = config.ReturnWithoutThrough(reply.Item1);
                        byte[] data = new byte[cmdIn.Length - 6];
                        Array.Copy(cmdIn, 4, data, 0, data.Length);
                        if (data.Length > 0)
                        {
                            List<byte[]> splited = data.SplitBytteArrayBy(0x00);
                            ToMessageStatus($"Field added: {config.GetSymbols(splited[0])} | ID: {i}");
                            newFields.Add(new FieldConfiguration()
                            {
                                fieldName = config.GetSymbols(splited[0]),
                                loadValue = splited.Count > 1 ? config.GetSymbols(splited[1]) : "",
                                fieldActive = true,
                            });
                        }
                        break;
                    }
                    catch { }
                }
                while (!Options.activeToken.IsCancellationRequested);
            }
            if (newFields.Count == 0) return;
            Action action = () =>
            {
                if (clearAfterScanCheckBox.Checked) fieldsData.Clear();
                foreach (FieldConfiguration field in newFields)
                    fieldsData.Add(field);
            };
            if (InvokeRequired) Invoke(action);
            else action();
        }
    }
}

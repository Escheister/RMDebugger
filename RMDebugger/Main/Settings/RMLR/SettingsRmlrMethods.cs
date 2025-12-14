using ConfigurationProtocol;
using Enums;
using RMDebugger.Main.Properties;
using StaticSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMDebugger.Main
{
    public partial class MainForm : Form
    {
        BindingList<FieldConfiguration> settingsRMLRData = new BindingList<FieldConfiguration>();

        private void SettingsRmlrGridDefault()
        {
            settingsRMLRData.Add(new FieldConfiguration("tag_min_pwr"));
            settingsRMLRData.Add(new FieldConfiguration("tag_pack_cnt"));
            settingsRMLRData.Add(new FieldConfiguration("tag_ttl"));
            settingsRMLRData.Add(new FieldConfiguration("ps_als_avg_cnt"));
            settingsRMLRData.Add(new FieldConfiguration("ps_rise_threshold"));
            settingsRMLRData.Add(new FieldConfiguration("als_drop_threshold"));
            settingsRMLRData.Add(new FieldConfiguration("als_ps_delay"));
        }
        private async void SettingRMLRButtonClick(object sender, EventArgs e)
        {
            bool GetUploadDataFromSettingsRmlrTable(out List<byte> data)
            {
                data = new List<byte>();
                foreach (FieldConfiguration rmlrData in settingsRMLRData)
                {
                    if (string.IsNullOrEmpty(rmlrData.uploadValue)) return false;
                    switch (rmlrData.rule)
                    {
                        case ConfigRule.uInt8:
                            if (Byte.TryParse(rmlrData.uploadValue, out byte vByte))
                                data.Add(vByte);
                            break;
                        case ConfigRule.uInt16:
                            if (UInt16.TryParse(rmlrData.uploadValue, out ushort vUshort))
                                data.AddRange(vUshort.GetBytes());
                            break;
                    }
                }
                return true;
            }
            void SetLoadDataToSettingsRmlrTable(byte[] data)
            {
                for (int i = 0, d = 0; i < settingsRMLRData.Count; i++)
                {
                    string value = "";
                    switch (settingsRMLRData[i].rule)
                    {
                        case ConfigRule.uInt8:
                            value = data[d].ToString();
                            d++;
                            break;
                        case ConfigRule.uInt16:
                            value = (data[d + 1] << 8 | data[d]).ToString();
                            d += 2;
                            break;
                    }
                    settingsRMLRData[i].loadValue = settingsRMLRData[i].uploadValue = value;
                }

                Invoke((MethodInvoker)(() =>
                {
                    settingsRMLRData.ResetBindings();
                    SettingsRMLRGrid.Refresh();
                }));
            }
            using (RMLR cOutput = new RMLR(Options.mainInterface, TargetSignID.GetBytes()))
            {
                Options.activeToken = new CancellationTokenSource(500);
                cOutput.ToReply += ToReplyStatus;
                cOutput.ToDebug += ToDebuggerWindow;

                Button btn = (Button)sender;
                byte[] cmdOut;
                bool buzzer = false;
                if (btn == LoadSettingRMLRButton)
                    cmdOut = cOutput.GetCmdLoad();
                else
                {
                    buzzer = resetSettingsRmlrToolStrip.Checked;
                    if (resetSettingsRmlrToolStrip.Checked)
                        cmdOut = cOutput.GetCmdReset();
                    else
                    {
                        if (GetUploadDataFromSettingsRmlrTable(out List<byte> data))
                            cmdOut = cOutput.GetCmdUpload(data.ToArray());
                        else
                        {
                            MessageBox.Show(this, "Введены не все данные в столбце Upload");
                            return;
                        }
                    }
                }
                try
                {
                    Tuple<byte[], ProtocolReply> reply = await cOutput.GetData(cmdOut, 15);
                    byte[] data = new byte[8];
                    Array.Copy(reply.Item1, 5, data, 0, data.Length);
                    SetLoadDataToSettingsRmlrTable(data);
                    if (buzzer)
                    {
                        await Task.Delay(25);
                        await cOutput.GetData(cOutput.GetCmdRGBB(false, false, false, true, 4), 6);
                    }
                }
                catch { }
            }
        }
        private async void SettingRMLRTestClick(object sender, EventArgs e)
        {
            void AfterStartRMLRTest(bool sw)
            {
                AfterAnyAutoEvent(sw);
                SignaturePanel.Enabled =
                    LoadSettingRMLRButton.Enabled =
                    UploadSettingRMLRButton.Enabled =
                    SettingsRMLRGrid.Enabled = !sw;
                TestSettingRMLRButton.Text = sw ? "Stop" : "Test";
                TestSettingRMLRButton.Image = sw ? Resources.StatusStopped : Resources.StatusRunning;
            }
            if (Options.activeProgress) { Options.activeToken?.Cancel(); return; }
            Options.activeToken = new CancellationTokenSource();
            Options.activeTask = Task.Run(() => StartTestSettingsRMLR());
            AfterStartRMLRTest(true);
            offTabsExcept(RMData, SettingsPages);
            offTabsExcept(ModeDeviceTabs, rmlrPage);
            try { await Options.activeTask; } catch { }
            onTabPages(RMData);
            onTabPages(ModeDeviceTabs);
            AfterStartRMLRTest(false);
            Options.activeTask = null;
        }
        private async Task StartTestSettingsRMLR()
        {
            using (RMLR cOutput = new RMLR(Options.mainInterface, TargetSignID.GetBytes()))
            {
                cOutput.ToReply += ToReplyStatus;
                cOutput.ToDebug += ToDebuggerWindow;
                byte[] cmdOut = cOutput.GetCmdRegistration();
                Tuple<byte[], ProtocolReply> reply;
                do
                {
                    try
                    {
                        reply = await cOutput.GetData(cmdOut, (int)CmdMaxSize.RMLR_REGISTRATION);
                        if (reply.Item1.Length > 6)
                        {
                            if (SettingsRMLRCounter.Value > 0)
                                await cOutput.GetData(cOutput.GetCmdRGBB(
                                    RedSettingsRMLR.Checked,
                                    GreenSettingsRMLR.Checked,
                                    BlueSettingsRMLR.Checked,
                                    BuzzerSettingsRMLR.Checked,
                                    (byte)SettingsRMLRCounter.Value), 6);
                            byte[] data = new byte[4];
                            Array.Copy(reply.Item1, 4, data, 0, data.Length);
                            linkSettingsRMLR_RMP_Signature.Text = $"{data[1] << 8 | data[0]}:{data[3] << 8 | data[2]} hz";
                            if (Options.showMessages)
                                NotifyMessage.ShowBalloonTip(5, "Найдена метка RMP",
                                    $"Найдена метка RMP с сигнатурой: {data[1] << 8 | data[0]}",
                                    ToolTipIcon.Info);
                            if (RmpDataReadSettingsRMLR.Checked)
                                await LoadFields(new Configuration(Options.mainInterface, (data[1] << 8 | data[0]).GetBytes(), TargetSignID.GetBytes()));
                            if (!RepeatSettingsRMLR.Checked) break;
                            await Task.Delay(1000, Options.activeToken.Token);
                        }
                        else linkSettingsRMLR_RMP_Signature.Text = "0:0 hz";
                    }
                    catch (OperationCanceledException) { return; }
                    catch { }
                    await Task.Delay(25, Options.activeToken.Token);
                }
                while (!Options.activeToken.IsCancellationRequested);
            }
        }
    }
}

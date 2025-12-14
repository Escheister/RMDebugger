using BootloaderProtocol;
using Enums;
using RMDebugger.Main.Properties;
using StaticSettings;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMDebugger.Main
{
    public partial class MainForm : Form
    {
        BindingList<FileInfoClass> MainFileObjects = new BindingList<FileInfoClass>();

        async private void HexUploadButtonClick(object sender, EventArgs e)
        {
            void AfterHexUploadEvent(bool sw)
            {
                AfterAnyAutoEvent(sw);
                HexPageSize.Enabled =
                    ListOfMainFiles.Enabled =
                    HexUploadDownFile.Enabled =
                    HexUploadUpFile.Enabled =
                    HexUploadPathButton.Enabled =
                    HexTimeout.Enabled =
                    SignaturePanel.Enabled = !sw;
                HexUploadButton.Text = sw ? "Stop" : "Upload";
                HexUploadButton.Image = sw ? Resources.StatusStopped : Resources.StatusRunning;
            }
            if (Options.activeProgress) { Options.activeToken?.Cancel(); return; }
            Options.activeToken = new CancellationTokenSource();
            AfterHexUploadEvent(true);
            offTabsExcept(RMData, HexUpdatePage);
            try
            {
                do
                {
                    if (Options.checkQueue)
                    {
                        FileInfoClass mainFile = Options.checkFirstMain
                            ? (FileInfoClass)ListOfMainFiles.Items[0]
                            : (FileInfoClass)ListOfMainFiles.SelectedItem;
                        for (int i = Options.checkFirstMain ? 1 : 0; i < ListOfMainFiles.Items.Count && Options.checkQueue; i++)
                        {
                            ListOfMainFiles.SetSelected(i, true);
                            if (Options.checkFirstMain)
                            {
                                await Task.Run(() => HexUploadAsync(mainFile));
                                await Task.Delay((int)HexPauseAfterUpload.Value * 1000, Options.activeToken.Token);
                            }
                            FileInfoClass file = (FileInfoClass)ListOfMainFiles.SelectedItem;
                            await Task.Run(() => HexUploadAsync(file));
                            await Task.Delay(Options.checkFirstMain
                                ? (int)HexPauseAfterUploadMainAndSec.Value * 1000
                                : (int)HexPauseAfterUpload.Value * 1000, Options.activeToken.Token);
                        }
                    }
                    else
                    {
                        if (Options.checkFirstMain && ListOfMainFiles.SelectedIndex != 0)
                        {
                            await Task.Run(() => HexUploadAsync((FileInfoClass)ListOfMainFiles.Items[0]));
                            await Task.Delay((int)HexPauseAfterUpload.Value * 1000, Options.activeToken.Token);
                        }
                        FileInfoClass file = (FileInfoClass)ListOfMainFiles.SelectedItem;
                        await Task.Run(() => HexUploadAsync(file));
                        await Task.Delay(HexUploadRepeatToolItem.Checked
                            ? (int)HexPauseAfterUpload.Value * 1000
                            : 0, Options.activeToken.Token);
                    }
                }
                while (HexUploadRepeatToolItem.Checked && !Options.activeToken.IsCancellationRequested);
            }
            catch { }
            AfterHexUploadEvent(false);
            onTabPages(RMData);
        }
        private void SetHexUploadProgress(string filename, int start = 0, int end = 0)
        {
            UpdateBar.Maximum = end;
            UpdateBar.Value = start;
            if (end > 0) HexFirmwareProgressLabel.Text = $"{start}/{end,-5}";
            else HexFirmwareProgressLabel.Text = $"{0}/{0}";

            HexFirmwareFilename.Text = end > 0 ? filename : "";
        }
        async private Task HexUploadAsync(FileInfoClass fileInfo)
        {
            using (Bootloader boot = NeedThrough.Checked
                ? new Bootloader(Options.mainInterface, TargetSignID.GetBytes(), ThroughSignID.GetBytes())
                : new Bootloader(Options.mainInterface, TargetSignID.GetBytes()))
            {
                boot.ToReply += ToReplyStatus;
                boot.ToDebug += ToDebuggerWindow;
                boot.SetProgress += SetHexUploadProgress;
                try { boot.SetQueueFromHex(fileInfo.Filepath); }
                catch (Exception ex) { ToMessageStatus(ex.Message); return; }
                //async method
                async Task<bool> GetReplyFromDevice(byte[] cmdOut)
                {
                    DateTime t0 = DateTime.Now;
                    TimeSpan tstop = DateTime.Now - t0;
                    while (tstop.Seconds < Options.hexTimeout && !Options.activeToken.IsCancellationRequested)
                    {
                        try
                        {
                            Tuple<byte[], ProtocolReply> replyes = await boot.GetData(cmdOut, cmdOut.Length, (int)HexTimeoutCmdAwait.Value);
                            return true;
                        }
                        catch (OperationCanceledException) { return false; }
                        catch (Exception ex)
                        {
                            if (ex.Message == "devNull") return false;
                            if ((DateTime.Now - t0).Seconds >= Options.hexTimeout)
                            {
                                DialogResult message = MessageBox.Show(this, "Timeout", "Something wrong...", MessageBoxButtons.RetryCancel);
                                if (message == DialogResult.Cancel) Options.activeToken?.Cancel();
                                else t0 = DateTime.Now;
                            }
                            await Task.Delay((int)HexTimeoutCmdRepeat.Value, Options.activeToken.Token);
                        }
                    }
                    return false;
                }

                byte[] cmdBootStart = boot.buildCmdDelegate(CmdOutput.START_BOOTLOADER);
                byte[] cmdBootStop = boot.buildCmdDelegate(CmdOutput.STOP_BOOTLOADER);
                byte[] cmdConfirmData = boot.buildCmdDelegate(CmdOutput.UPDATE_DATA_PAGE);

                if (!await GetReplyFromDevice(cmdBootStart)) return;

                ToMessageStatus("Bootload OK");

                boot.PageSize = (int)HexPageSize.Value;

                Stopwatch stopwatchQueue = Stopwatch.StartNew();

                while (boot.HexQueue.Count() > 0)
                {
                    boot.GetDataForUpload(out byte[] dataOutput);
                    if (!await GetReplyFromDevice(boot.buildDataCmdDelegate(dataOutput))) return;
                    if (!await GetReplyFromDevice(cmdConfirmData)) return;
                }
                await GetReplyFromDevice(cmdBootStop);
                stopwatchQueue.Stop();
                string timeUplod = $"{stopwatchQueue.Elapsed.Minutes:00}:{stopwatchQueue.Elapsed.Seconds:00}:{stopwatchQueue.Elapsed.Milliseconds:000}";
                if (!Options.activeToken.IsCancellationRequested)
                {
                    ToMessageStatus($"Firmware OK | Uploaded for " + timeUplod);
                    if (Options.showMessages)
                        NotifyMessage.ShowBalloonTip(5,
                            "Прошивка устройства",
                            $"Файл {fileInfo.Filename} успешно загружен на устройство за " + timeUplod,
                            ToolTipIcon.Info);
                }

            }
        }
    }
}

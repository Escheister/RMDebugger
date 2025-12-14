using Enums;
using StaticSettings;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMDebugger.Main
{
    public partial class MainForm : Form
    {
        async private void SendCommandFromExtraButton(CmdOutput cmdOutput)
        {
            void AfterSendExtraButtonEvent(bool sw)
            {
                SerUdpPages.Enabled =
                    BaudRate.Enabled =
                    dataBits.Enabled =
                    Parity.Enabled =
                    stopBits.Enabled =
                    SignaturePanel.Enabled =
                    RMData.Enabled =
                    SetOnlineFreqNumeric.Enabled = !sw;
                DinoRunningProcessOk.Enabled =
                    DinoRunningProcessOk.Visible =
                    Options.activeProgress = sw;
            }
            if (Options.activeProgress) { Options.activeToken?.Cancel(); return; }
            Options.activeToken = new CancellationTokenSource();
            AfterSendExtraButtonEvent(true);
            try { await Task.Run(() => SendCommandFromButtonTask(cmdOutput)); } catch { }
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
                        return;
                    }
                    catch { }
                    await Task.Delay((int)AutoExtraButtonsTimeout.Value, Options.activeToken.Token);
                }
                while (ExtendedRepeatMenuItem.Checked && !Options.activeToken.IsCancellationRequested);
            }
            ;
        }
    }
}

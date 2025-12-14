using Enums;
using System.Windows.Forms;

namespace RMDebugger.Main
{
    public partial class MainForm : Form
    {
        private void AddExtraButtonsEvents()
        {
            SetOnlineButton.Click += (s, e) => SendCommandFromExtraButton(CmdOutput.ONLINE);
            ResetButton.Click += (s, e) => SendCommandFromExtraButton(CmdOutput.RESET);
            SleepButton.Click += (s, e) => SendCommandFromExtraButton(CmdOutput.SLEEP);
            SetBootloaderStartButton.Click += (s, e) => SendCommandFromExtraButton(CmdOutput.START_BOOTLOADER);
            SetBootloaderStopButton.Click += (s, e) => SendCommandFromExtraButton(CmdOutput.STOP_BOOTLOADER);
            ExtendedRepeatMenuItem.CheckedChanged += (s, e)
                => AutoExtraButtonsTimeout.Visible = label24.Visible = ExtendedRepeatMenuItem.Checked;
        }
    }
}

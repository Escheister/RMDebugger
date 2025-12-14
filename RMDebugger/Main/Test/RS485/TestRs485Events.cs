using RMDebugger.Main.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace RMDebugger.Main
{
    public partial class MainForm : Form
    {
        private void AddTestRS485Events()
        {
            StatusRS485GridView.DoubleBuffered(true);

            StartTestRSButton.Click += StartTestRSButtonClick;
            AddSignatureIDToTest.Click += AddTargetSignID;
            ManualScanToTest.Click += ManualScanToTestClick;
            AutoScanToTest.Click += AutoScanToTestClick;

            ClearInfoTestRS485.Click += ClearInfoTestRS485Click;
            MoreInfoTestRS485.Click += MoreInfoTestRS485Click;
            ShowExtendedMenu.Click += (s, e) =>
            {
                bool hideMenu = ShowExtendedMenu.Text == "Show &menu";
                StatusRS485GridView.Columns[0].Visible = hideMenu;
                extendedMenuPanel.Location = hideMenu
                    ? new Point(extendedMenuPanel.Location.X, extendedMenuPanel.Location.Y - 147)
                    : new Point(extendedMenuPanel.Location.X, extendedMenuPanel.Location.Y + 147);
                ShowExtendedMenu.Image = hideMenu ? Resources.Unhide : Resources.Hide;
                ToolTipHelper.SetToolTip(ShowExtendedMenu, hideMenu ? "Скрыть расширенное меню" : "Показать расширенное меню");
                ShowExtendedMenu.Text = hideMenu ? "Hide &menu" : "Show &menu";
            };

            TimerSettingsTestBox.CheckedChanged += (s, e) => timerPanelTest.Visible = TimerSettingsTestBox.Checked;
            WorkTestTimer.Tick += (s, e) => RefreshGridTask();
            FreqTestRSNumeric.ValueChanged += (s, e) => WorkTestTimer.Interval = (int)FreqTestRSNumeric.Value;
            StatusRS485GridView.DataSource = testerData;
            StatusRS485GridView.RowPrePaint += CellValueChangedRS485;
            foreach (ToolStripDropDownItem item in RS485SortMenuStrip.Items) item.Click += ChooseSortedBy;
            SortByButton.Click += (s, e) => StatusRS485Sort();
            SortedColumnCombo.SelectedIndex = 0;
        }
    }
}

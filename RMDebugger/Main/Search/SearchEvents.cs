using StaticSettings;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RMDebugger.Main
{
    public partial class MainForm : Form
    {
        private void AddSearchEvents()
        {
            SearchGrid.DoubleBuffered(true);

            deviceData.ListChanged += (s, e) => ToMessageStatus($"{deviceData.Count} devices in Search.");
            SearchAutoButton.Click += SearchButtonClick;
            SearchManualButton.Click += SearchButtonClick;
            SearchTimeout.ValueChanged += (s, e) => Options.timeoutSearch = (int)SearchTimeout.Value;
            SearchChangeColorMenuItem.Click += (s, e) =>
            {
                if (MirrorColor.ShowDialog() == DialogResult.OK)
                    mirClr = MirrorColor.Color;
            };

            void FontChanging(bool check)
            {
                if (check)
                {
                    SearchGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    SearchGrid.ColumnHeadersHeight = SearchGrid.RowTemplate.Height = (int)(FontDialog.Font.Size * 2) + 2;
                    SearchGrid.ColumnHeadersDefaultCellStyle.Font = FontDialog.Font;
                    SearchGrid.DefaultCellStyle.Font = FontDialog.Font;
                }
                else
                {
                    SearchGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    SearchGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Consolas", 9);
                    SearchGrid.DefaultCellStyle.Font = new Font("Consolas", 11);
                    SearchGrid.ColumnHeadersHeight = SearchGrid.RowTemplate.Height = 18;
                }
            }

            SearchChangeFontMenuItem.Click += (s, e) =>
            {
                if (FontDialog.ShowDialog() == DialogResult.OK)
                    FontChanging(SearchAnotherFontMode.Checked);
            };
            SearchAnotherFontMode.CheckedChanged += (s, e) =>
            {
                FontChanging(SearchAnotherFontMode.Checked);
            };

            SearchGetNear.CheckedChanged += (s, e) =>
            {
                SearchFilterMode.Enabled =
                    SearchGetNear.Checked;
                SearchFindSignatireMode.Enabled =
                    SearchExtendedFindMode.Enabled = !Options.through && SearchGetNear.Checked;
            };
            foreach (ToolStripMenuItem item in SearchFilterMenuStrip.Items) item.Click += SearchFilterClick;
            SearchGrid.DataSource = deviceData;
            SearchGrid.CellContentClick += (s, e) =>
            {
                if (e.RowIndex > -1)
                    TargetSignID.Value = Convert.ToDecimal(SearchGrid[0, e.RowIndex].Value);
            };
        }
    }
}

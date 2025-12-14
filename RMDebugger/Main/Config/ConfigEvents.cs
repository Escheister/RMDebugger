using ConfigurationProtocol;
using Enums;
using System.Windows.Forms;

namespace RMDebugger.Main
{
    public partial class MainForm : Form
    {
        private void AddConfigEvents()
        {
            ConfigDataGrid.DoubleBuffered(true);

            ConfigDataGrid.DataSource = fieldsData;
            ConfigDataGrid.CellToolTipTextNeeded += (s, e) =>
            {
                if (e.RowIndex > -1 && e.ColumnIndex == (int)ConfigColumns.ConfigUpload)
                    e.ToolTipText = ((FieldConfiguration)ConfigDataGrid.Rows[e.RowIndex].DataBoundItem).uploadToolTip;
            };

            LoadConfigButton.Click += LoadConfigButtonClick;
            UploadConfigButton.Click += UploadConfigButtonClick;

            ConfigClearLoad.Click += (s, e) =>
            {
                foreach (FieldConfiguration fc in fieldsData)
                    fc.loadValue = string.Empty;
                ConfigDataGrid.Refresh();
            };
            ConfigClearUpload.Click += (s, e) =>
            {
                foreach (FieldConfiguration fc in fieldsData)
                    fc.uploadValue = string.Empty;
                ConfigDataGrid.Refresh();
            };
            ConfigClearAll.Click += (s, e) =>
            {
                foreach (FieldConfiguration fc in fieldsData)
                    fc.ClearValues();
                ConfigDataGrid.Refresh();
            };
            ConfigEnableAllMenuItem.CheckedChanged += (s, e) =>
            {
                foreach (FieldConfiguration fc in fieldsData)
                    fc.fieldActive = ConfigEnableAllMenuItem.Checked;
                ConfigDataGrid.Refresh();
            };
            clearAllFieldsMenuItem.Click += (s, e) => fieldsData.Clear();
            LoadFieldsConfigButton.Click += LoadFieldsConfigButtonClick;
            HideConfigPanel.Click += (s, e) => HideConfigPanelClick((string)HideConfigPanel.Tag == "Hided");
            minIxScanConfig.ValueChanged += (s, e) =>
            {
                maxIxScanConfig.Minimum = minIxScanConfig.Value;
                if (minIxScanConfig.Value > maxIxScanConfig.Value)
                    maxIxScanConfig.Value = minIxScanConfig.Value;
            };
        }
    }
}

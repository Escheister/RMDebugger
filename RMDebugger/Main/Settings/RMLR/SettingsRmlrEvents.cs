using ConfigurationProtocol;
using Enums;
using RMDebugger.Main.Properties;
using System.Windows.Forms;

namespace RMDebugger.Main
{
    public partial class MainForm : Form
    {
        private void AddSettingsRMLREvents()
        {
            SettingsRMLRGrid.AutoGenerateColumns = false;
            SettingsRMLRGrid.DoubleBuffered(true);

            SettingsRMLRGrid.DataSource = settingsRMLRData;
            SettingsRMLRGrid.CellToolTipTextNeeded += (s, e) =>
            {
                if (e.RowIndex > -1)
                {
                    if (e.ColumnIndex == (int)SettingsRmlrColumns.RmlrField)
                        e.ToolTipText = ((FieldConfiguration)SettingsRMLRGrid.Rows[e.RowIndex].DataBoundItem).GetFieldToolTip();
                    else if (e.ColumnIndex == (int)SettingsRmlrColumns.RmlrUpload)
                        e.ToolTipText = ((FieldConfiguration)SettingsRMLRGrid.Rows[e.RowIndex].DataBoundItem).uploadToolTip;
                }

            };
            ClearGridSettingsRMLR.Click += (s, e) =>
            {
                foreach (FieldConfiguration field in settingsRMLRData) field.ClearValues();
                SettingsRMLRGrid.Refresh();
            };
            resetSettingsRmlrToolStrip.CheckedChanged += (s, e)
                => UploadSettingRMLRButton.Image = resetSettingsRmlrToolStrip.Checked
                                                    ? Resources.CloudError
                                                    : Resources.CloudUpload;

            LoadSettingRMLRButton.Click += SettingRMLRButtonClick;
            UploadSettingRMLRButton.Click += SettingRMLRButtonClick;
            TestSettingRMLRButton.Click += SettingRMLRTestClick;


            linkSettingsRMLR_RMP_Signature.Click += (s, e) =>
            {
                if (ushort.TryParse(linkSettingsRMLR_RMP_Signature.Text.Split(':')[0], out ushort value))
                    TargetSignID.Value = value;
            };
        }
    }
}

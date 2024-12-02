namespace RMDebugger
{
    partial class MainDebugger
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDebugger));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Who are you");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Status");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Get Near");
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TargetSignID = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.mainPort = new System.IO.Ports.SerialPort(this.components);
            this.RMData = new System.Windows.Forms.TabControl();
            this.SearchPage = new System.Windows.Forms.TabPage();
            this.SearchExtraGroup = new System.Windows.Forms.GroupBox();
            this.SearchFilterMode = new System.Windows.Forms.CheckBox();
            this.SearchFilterMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rM485ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rMPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rMGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rMHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rMTAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchExtendedFindMode = new System.Windows.Forms.CheckBox();
            this.SearchKnockMode = new System.Windows.Forms.CheckBox();
            this.SearchFindSignatireMode = new System.Windows.Forms.CheckBox();
            this.SearchFindSignatureColorMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SearchChangeColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchModesPanel = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.SearchGetNear = new System.Windows.Forms.CheckBox();
            this.SearchTimeout = new System.Windows.Forms.NumericUpDown();
            this.SearchDistTof = new System.Windows.Forms.CheckBox();
            this.SearchManualButton = new System.Windows.Forms.Button();
            this.SearchAutoButton = new System.Windows.Forms.Button();
            this.SearchGrid = new System.Windows.Forms.DataGridView();
            this.signSearch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeSearch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.distSearch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rssiSearch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HexUpdatePage = new System.Windows.Forms.TabPage();
            this.HexUploadFilename = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.HexCheckCrc = new System.Windows.Forms.CheckBox();
            this.HexTimeout = new System.Windows.Forms.NumericUpDown();
            this.HexPageSize = new System.Windows.Forms.NumericUpDown();
            this.HexPathBox = new System.Windows.Forms.ComboBox();
            this.BytesEnd = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.BytesStart = new System.Windows.Forms.Label();
            this.UpdateBar = new System.Windows.Forms.ProgressBar();
            this.HexUploadButton = new System.Windows.Forms.Button();
            this.HexPathButton = new System.Windows.Forms.Button();
            this.ConfigPage = new System.Windows.Forms.TabPage();
            this.ConfigAddField = new System.Windows.Forms.Button();
            this.ConfigFieldTextBox = new System.Windows.Forms.TextBox();
            this.RMLRRepeatCount = new System.Windows.Forms.NumericUpDown();
            this.RMLRModeCheck = new System.Windows.Forms.CheckBox();
            this.RMLRMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RMLRRed = new System.Windows.Forms.ToolStripMenuItem();
            this.RMLRGreen = new System.Windows.Forms.ToolStripMenuItem();
            this.RMLRBlue = new System.Windows.Forms.ToolStripMenuItem();
            this.RMLRBuzzer = new System.Windows.Forms.ToolStripMenuItem();
            this.RMLRSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.RMLRRepeat = new System.Windows.Forms.ToolStripMenuItem();
            this.ConfigFactoryCheck = new System.Windows.Forms.CheckBox();
            this.ConfigDataGrid = new System.Windows.Forms.DataGridView();
            this.ConfigClearMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ConfigEnableAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConfigClearMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConfigClearLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.ConfigClearUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.ConfigClearAll = new System.Windows.Forms.ToolStripMenuItem();
            this.UploadConfigButton = new System.Windows.Forms.Button();
            this.LoadConfigButton = new System.Windows.Forms.Button();
            this.InfoPage = new System.Windows.Forms.TabPage();
            this.numericInfoSeconds = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonInfoStop = new System.Windows.Forms.Button();
            this.InfoTree = new System.Windows.Forms.TreeView();
            this.infoMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearInfoMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToCsvInfoMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.TestPage = new System.Windows.Forms.TabPage();
            this.TestPages = new System.Windows.Forms.TabControl();
            this.RS485Page = new System.Windows.Forms.TabPage();
            this.extendedMenuPanel = new System.Windows.Forms.Panel();
            this.SortedColumnCombo = new System.Windows.Forms.ComboBox();
            this.SortByButton = new System.Windows.Forms.Button();
            this.RS485SortMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.byAscMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byDescMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoreInfoTestRS485 = new System.Windows.Forms.Button();
            this.timerPanelTest = new System.Windows.Forms.Panel();
            this.numericHoursTest = new System.Windows.Forms.NumericUpDown();
            this.numericSecondsTest = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericMinutesTest = new System.Windows.Forms.NumericUpDown();
            this.numericDaysTest = new System.Windows.Forms.NumericUpDown();
            this.WorkingTimeLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SaveLogTestRS485 = new System.Windows.Forms.Button();
            this.ClearDataTestRS485 = new System.Windows.Forms.Button();
            this.ClearInfoTestRS485 = new System.Windows.Forms.Button();
            this.settingsGroupBox = new System.Windows.Forms.GroupBox();
            this.ClearBufferSettingsTestBox = new System.Windows.Forms.CheckBox();
            this.TimerSettingsTestBox = new System.Windows.Forms.CheckBox();
            this.RadioSettingsTestBox = new System.Windows.Forms.CheckBox();
            this.scanGroupBox = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ManualScanToTest = new System.Windows.Forms.Button();
            this.maxSigToScan = new System.Windows.Forms.NumericUpDown();
            this.AddSignatureIDToTest = new System.Windows.Forms.Button();
            this.minSigToScan = new System.Windows.Forms.NumericUpDown();
            this.ShowExtendedMenu = new System.Windows.Forms.Button();
            this.StartTestRSButton = new System.Windows.Forms.Button();
            this.AutoScanToTest = new System.Windows.Forms.Button();
            this.StatusRS485GridView = new System.Windows.Forms.DataGridView();
            this.InterfaceStatusRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SignStatusRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeStatusRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusStatusRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TxStatusRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RxStatusRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorStatusRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PercentErrorStatusRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisconnectedStatusRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BadReplyStatusRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BadCrcStatusRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RadioErrorStatusRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RadioNearbyStatusRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkTimeStatusRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VerStatusRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.MessageStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.ReplyStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.SerUdpPages = new System.Windows.Forms.TabControl();
            this.SerialPage = new System.Windows.Forms.TabPage();
            this.comPort = new System.Windows.Forms.ComboBox();
            this.RefreshSerial = new System.Windows.Forms.Button();
            this.BaudRate = new System.Windows.Forms.ComboBox();
            this.OpenCom = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.UdpPage = new System.Windows.Forms.TabPage();
            this.PingButton = new System.Windows.Forms.Button();
            this.numericPort = new System.Windows.Forms.NumericUpDown();
            this.Connect = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.IPaddressBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.settingsToolStrip = new System.Windows.Forms.ToolStripDropDownButton();
            this.saveToRegToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromPCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSaveFromPCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.transparentToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.messagesToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.windowPinToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.extendedButtonsToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.clearSettingsToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dataBits = new System.Windows.Forms.ToolStripDropDownButton();
            this.dataBits7 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataBits8 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataBitsInfo = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.Parity = new System.Windows.Forms.ToolStripDropDownButton();
            this.ParityNone = new System.Windows.Forms.ToolStripMenuItem();
            this.ParityOdd = new System.Windows.Forms.ToolStripMenuItem();
            this.ParityEven = new System.Windows.Forms.ToolStripMenuItem();
            this.ParityMark = new System.Windows.Forms.ToolStripMenuItem();
            this.ParitySpace = new System.Windows.Forms.ToolStripMenuItem();
            this.ParityInfo = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.stopBits = new System.Windows.Forms.ToolStripDropDownButton();
            this.stopBits1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stopBits2 = new System.Windows.Forms.ToolStripMenuItem();
            this.StopBitsInfo = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.AboutButton = new System.Windows.Forms.ToolStripButton();
            this.UpdateButton = new System.Windows.Forms.ToolStripButton();
            this.DinoRunningProcessOk = new System.Windows.Forms.ToolStripButton();
            this.SignaturePanel = new System.Windows.Forms.Panel();
            this.ThroughSignID = new System.Windows.Forms.NumericUpDown();
            this.NeedThrough = new System.Windows.Forms.CheckBox();
            this.ExtraButtonsGroup = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.AutoExtraButtonsTimeout = new System.Windows.Forms.NumericUpDown();
            this.AutoExtraButtons = new System.Windows.Forms.CheckBox();
            this.ButtonsPanel = new System.Windows.Forms.Panel();
            this.SetBootloaderStopButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.SetBootloaderStartButton = new System.Windows.Forms.Button();
            this.SetOnlineFreqNumeric = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.SetOnlineButton = new System.Windows.Forms.Button();
            this.ToolTipHelper = new System.Windows.Forms.ToolTip(this.components);
            this.MirrorColor = new System.Windows.Forms.ColorDialog();
            this.ErrorMessage = new System.Windows.Forms.ErrorProvider(this.components);
            this.NotifyMessage = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyMessageStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenDebugFromToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.AboutFromToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseFromToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.WorkTestTimer = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FieldsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnabledColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LoadFieldColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UploadFieldColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.TargetSignID)).BeginInit();
            this.RMData.SuspendLayout();
            this.SearchPage.SuspendLayout();
            this.SearchExtraGroup.SuspendLayout();
            this.SearchFilterMenuStrip.SuspendLayout();
            this.SearchFindSignatureColorMenuStrip.SuspendLayout();
            this.SearchModesPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SearchGrid)).BeginInit();
            this.HexUpdatePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HexTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HexPageSize)).BeginInit();
            this.ConfigPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RMLRRepeatCount)).BeginInit();
            this.RMLRMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConfigDataGrid)).BeginInit();
            this.ConfigClearMenuStrip.SuspendLayout();
            this.InfoPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericInfoSeconds)).BeginInit();
            this.infoMenuStrip.SuspendLayout();
            this.TestPage.SuspendLayout();
            this.TestPages.SuspendLayout();
            this.RS485Page.SuspendLayout();
            this.extendedMenuPanel.SuspendLayout();
            this.RS485SortMenuStrip.SuspendLayout();
            this.timerPanelTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericHoursTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSecondsTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinutesTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDaysTest)).BeginInit();
            this.settingsGroupBox.SuspendLayout();
            this.scanGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxSigToScan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minSigToScan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusRS485GridView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SerUdpPages.SuspendLayout();
            this.SerialPage.SuspendLayout();
            this.UdpPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPort)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SignaturePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThroughSignID)).BeginInit();
            this.ExtraButtonsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AutoExtraButtonsTimeout)).BeginInit();
            this.ButtonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SetOnlineFreqNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMessage)).BeginInit();
            this.notifyMessageStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TargetSignID
            // 
            this.TargetSignID.Location = new System.Drawing.Point(109, 2);
            this.TargetSignID.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.TargetSignID.Name = "TargetSignID";
            this.TargetSignID.Size = new System.Drawing.Size(52, 20);
            this.TargetSignID.TabIndex = 0;
            this.TargetSignID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TargetSignID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Signature ID:";
            // 
            // mainPort
            // 
            this.mainPort.ReadBufferSize = 512;
            this.mainPort.WriteBufferSize = 512;
            // 
            // RMData
            // 
            this.RMData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RMData.Controls.Add(this.SearchPage);
            this.RMData.Controls.Add(this.HexUpdatePage);
            this.RMData.Controls.Add(this.ConfigPage);
            this.RMData.Controls.Add(this.InfoPage);
            this.RMData.Controls.Add(this.TestPage);
            this.RMData.Cursor = System.Windows.Forms.Cursors.Default;
            this.RMData.Enabled = false;
            this.RMData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RMData.Location = new System.Drawing.Point(174, 27);
            this.RMData.Margin = new System.Windows.Forms.Padding(0);
            this.RMData.Name = "RMData";
            this.RMData.SelectedIndex = 0;
            this.RMData.Size = new System.Drawing.Size(327, 292);
            this.RMData.TabIndex = 16;
            // 
            // SearchPage
            // 
            this.SearchPage.BackColor = System.Drawing.Color.White;
            this.SearchPage.Controls.Add(this.SearchExtraGroup);
            this.SearchPage.Controls.Add(this.SearchModesPanel);
            this.SearchPage.Controls.Add(this.SearchManualButton);
            this.SearchPage.Controls.Add(this.SearchAutoButton);
            this.SearchPage.Controls.Add(this.SearchGrid);
            this.SearchPage.Location = new System.Drawing.Point(4, 22);
            this.SearchPage.Margin = new System.Windows.Forms.Padding(1);
            this.SearchPage.Name = "SearchPage";
            this.SearchPage.Size = new System.Drawing.Size(319, 266);
            this.SearchPage.TabIndex = 9;
            this.SearchPage.Text = "Search";
            // 
            // SearchExtraGroup
            // 
            this.SearchExtraGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchExtraGroup.Controls.Add(this.SearchFilterMode);
            this.SearchExtraGroup.Controls.Add(this.SearchExtendedFindMode);
            this.SearchExtraGroup.Controls.Add(this.SearchKnockMode);
            this.SearchExtraGroup.Controls.Add(this.SearchFindSignatireMode);
            this.SearchExtraGroup.Location = new System.Drawing.Point(210, 106);
            this.SearchExtraGroup.Margin = new System.Windows.Forms.Padding(1);
            this.SearchExtraGroup.Name = "SearchExtraGroup";
            this.SearchExtraGroup.Size = new System.Drawing.Size(107, 159);
            this.SearchExtraGroup.TabIndex = 37;
            this.SearchExtraGroup.TabStop = false;
            this.SearchExtraGroup.Text = "Extra";
            this.ToolTipHelper.SetToolTip(this.SearchExtraGroup, "Режимы");
            // 
            // SearchFilterMode
            // 
            this.SearchFilterMode.ContextMenuStrip = this.SearchFilterMenuStrip;
            this.SearchFilterMode.Location = new System.Drawing.Point(2, 65);
            this.SearchFilterMode.Margin = new System.Windows.Forms.Padding(1);
            this.SearchFilterMode.Name = "SearchFilterMode";
            this.SearchFilterMode.Size = new System.Drawing.Size(104, 17);
            this.SearchFilterMode.TabIndex = 37;
            this.SearchFilterMode.Text = "Filter";
            this.SearchFilterMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipHelper.SetToolTip(this.SearchFilterMode, "Фильтр по типу устройств\r\nНажмите правой кнопкой мыши, что бы выделить нужные уст" +
        "ройства");
            this.SearchFilterMode.UseVisualStyleBackColor = true;
            // 
            // SearchFilterMenuStrip
            // 
            this.SearchFilterMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rM485ToolStripMenuItem,
            this.rMPToolStripMenuItem,
            this.rMGToolStripMenuItem,
            this.rMHToolStripMenuItem,
            this.rMTAToolStripMenuItem});
            this.SearchFilterMenuStrip.Name = "SearchFilterMenuStrip";
            this.SearchFilterMenuStrip.Size = new System.Drawing.Size(111, 114);
            // 
            // rM485ToolStripMenuItem
            // 
            this.rM485ToolStripMenuItem.CheckOnClick = true;
            this.rM485ToolStripMenuItem.Name = "rM485ToolStripMenuItem";
            this.rM485ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.rM485ToolStripMenuItem.Text = "RM485";
            // 
            // rMPToolStripMenuItem
            // 
            this.rMPToolStripMenuItem.CheckOnClick = true;
            this.rMPToolStripMenuItem.Name = "rMPToolStripMenuItem";
            this.rMPToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.rMPToolStripMenuItem.Text = "RMP";
            // 
            // rMGToolStripMenuItem
            // 
            this.rMGToolStripMenuItem.CheckOnClick = true;
            this.rMGToolStripMenuItem.Name = "rMGToolStripMenuItem";
            this.rMGToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.rMGToolStripMenuItem.Text = "RMG";
            // 
            // rMHToolStripMenuItem
            // 
            this.rMHToolStripMenuItem.CheckOnClick = true;
            this.rMHToolStripMenuItem.Name = "rMHToolStripMenuItem";
            this.rMHToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.rMHToolStripMenuItem.Text = "RMH";
            // 
            // rMTAToolStripMenuItem
            // 
            this.rMTAToolStripMenuItem.CheckOnClick = true;
            this.rMTAToolStripMenuItem.Name = "rMTAToolStripMenuItem";
            this.rMTAToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.rMTAToolStripMenuItem.Text = "RMTA";
            // 
            // SearchExtendedFindMode
            // 
            this.SearchExtendedFindMode.Location = new System.Drawing.Point(2, 33);
            this.SearchExtendedFindMode.Margin = new System.Windows.Forms.Padding(1);
            this.SearchExtendedFindMode.Name = "SearchExtendedFindMode";
            this.SearchExtendedFindMode.Size = new System.Drawing.Size(104, 17);
            this.SearchExtendedFindMode.TabIndex = 35;
            this.SearchExtendedFindMode.Text = "Extended Find";
            this.SearchExtendedFindMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipHelper.SetToolTip(this.SearchExtendedFindMode, "Расширенный поиск");
            this.SearchExtendedFindMode.UseVisualStyleBackColor = true;
            // 
            // SearchKnockMode
            // 
            this.SearchKnockMode.Location = new System.Drawing.Point(2, 49);
            this.SearchKnockMode.Margin = new System.Windows.Forms.Padding(1);
            this.SearchKnockMode.Name = "SearchKnockMode";
            this.SearchKnockMode.Size = new System.Drawing.Size(104, 17);
            this.SearchKnockMode.TabIndex = 36;
            this.SearchKnockMode.Text = "Knock-Knock";
            this.SearchKnockMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipHelper.SetToolTip(this.SearchKnockMode, "Ожидание ответа");
            this.SearchKnockMode.UseVisualStyleBackColor = true;
            // 
            // SearchFindSignatireMode
            // 
            this.SearchFindSignatireMode.ContextMenuStrip = this.SearchFindSignatureColorMenuStrip;
            this.SearchFindSignatireMode.Location = new System.Drawing.Point(2, 17);
            this.SearchFindSignatireMode.Margin = new System.Windows.Forms.Padding(1);
            this.SearchFindSignatireMode.Name = "SearchFindSignatireMode";
            this.SearchFindSignatireMode.Size = new System.Drawing.Size(104, 17);
            this.SearchFindSignatireMode.TabIndex = 34;
            this.SearchFindSignatireMode.Text = "Find Signature";
            this.SearchFindSignatireMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ToolTipHelper.SetToolTip(this.SearchFindSignatireMode, "Поиск устройств, подключенных к одной шине RS485\r\nНажмите правой кнопкой мыши, чт" +
        "о бы настроить цвет выделения");
            this.SearchFindSignatireMode.UseVisualStyleBackColor = true;
            // 
            // SearchFindSignatureColorMenuStrip
            // 
            this.SearchFindSignatureColorMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SearchChangeColorMenuItem});
            this.SearchFindSignatureColorMenuStrip.Name = "SearchMirrorColorMenuStrip";
            this.SearchFindSignatureColorMenuStrip.Size = new System.Drawing.Size(146, 26);
            // 
            // SearchChangeColorMenuItem
            // 
            this.SearchChangeColorMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("SearchChangeColorMenuItem.Image")));
            this.SearchChangeColorMenuItem.Name = "SearchChangeColorMenuItem";
            this.SearchChangeColorMenuItem.Size = new System.Drawing.Size(145, 22);
            this.SearchChangeColorMenuItem.Text = "Change color";
            this.SearchChangeColorMenuItem.ToolTipText = "Выбрать цвет выделения";
            // 
            // SearchModesPanel
            // 
            this.SearchModesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchModesPanel.Controls.Add(this.label12);
            this.SearchModesPanel.Controls.Add(this.SearchGetNear);
            this.SearchModesPanel.Controls.Add(this.SearchTimeout);
            this.SearchModesPanel.Controls.Add(this.SearchDistTof);
            this.SearchModesPanel.Location = new System.Drawing.Point(210, 47);
            this.SearchModesPanel.Margin = new System.Windows.Forms.Padding(1);
            this.SearchModesPanel.Name = "SearchModesPanel";
            this.SearchModesPanel.Size = new System.Drawing.Size(107, 59);
            this.SearchModesPanel.TabIndex = 26;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(83, 5);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(20, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "ms";
            // 
            // SearchGetNear
            // 
            this.SearchGetNear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchGetNear.Checked = true;
            this.SearchGetNear.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SearchGetNear.Location = new System.Drawing.Point(2, 23);
            this.SearchGetNear.Margin = new System.Windows.Forms.Padding(1);
            this.SearchGetNear.Name = "SearchGetNear";
            this.SearchGetNear.Size = new System.Drawing.Size(101, 17);
            this.SearchGetNear.TabIndex = 24;
            this.SearchGetNear.Text = "Get Near";
            this.ToolTipHelper.SetToolTip(this.SearchGetNear, "Отправка команды для определения устройств по близости, а так же их типов");
            this.SearchGetNear.UseVisualStyleBackColor = true;
            // 
            // SearchTimeout
            // 
            this.SearchTimeout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchTimeout.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.SearchTimeout.Location = new System.Drawing.Point(2, 1);
            this.SearchTimeout.Margin = new System.Windows.Forms.Padding(1);
            this.SearchTimeout.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.SearchTimeout.Name = "SearchTimeout";
            this.SearchTimeout.Size = new System.Drawing.Size(79, 20);
            this.SearchTimeout.TabIndex = 27;
            this.SearchTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTipHelper.SetToolTip(this.SearchTimeout, "Время опроса, мс");
            this.SearchTimeout.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // SearchDistTof
            // 
            this.SearchDistTof.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchDistTof.Location = new System.Drawing.Point(2, 39);
            this.SearchDistTof.Margin = new System.Windows.Forms.Padding(1);
            this.SearchDistTof.Name = "SearchDistTof";
            this.SearchDistTof.Size = new System.Drawing.Size(101, 17);
            this.SearchDistTof.TabIndex = 25;
            this.SearchDistTof.Text = "Dist Tof";
            this.ToolTipHelper.SetToolTip(this.SearchDistTof, "Отправка команды для измерения дистанции и мощности сигнала\r\nНе рекомендуется ста" +
        "вить частоту опроса ниже 1 000 мс");
            this.SearchDistTof.UseVisualStyleBackColor = true;
            // 
            // SearchManualButton
            // 
            this.SearchManualButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchManualButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SearchManualButton.Location = new System.Drawing.Point(211, 1);
            this.SearchManualButton.Margin = new System.Windows.Forms.Padding(1);
            this.SearchManualButton.Name = "SearchManualButton";
            this.SearchManualButton.Size = new System.Drawing.Size(107, 23);
            this.SearchManualButton.TabIndex = 22;
            this.SearchManualButton.Text = "Manual";
            this.SearchManualButton.UseVisualStyleBackColor = true;
            // 
            // SearchAutoButton
            // 
            this.SearchAutoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchAutoButton.Image = ((System.Drawing.Image)(resources.GetObject("SearchAutoButton.Image")));
            this.SearchAutoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SearchAutoButton.Location = new System.Drawing.Point(211, 24);
            this.SearchAutoButton.Margin = new System.Windows.Forms.Padding(1);
            this.SearchAutoButton.Name = "SearchAutoButton";
            this.SearchAutoButton.Size = new System.Drawing.Size(107, 23);
            this.SearchAutoButton.TabIndex = 23;
            this.SearchAutoButton.Text = "Auto";
            this.SearchAutoButton.UseVisualStyleBackColor = true;
            // 
            // SearchGrid
            // 
            this.SearchGrid.AllowUserToAddRows = false;
            this.SearchGrid.AllowUserToDeleteRows = false;
            this.SearchGrid.AllowUserToResizeColumns = false;
            this.SearchGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SearchGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.SearchGrid.BackgroundColor = System.Drawing.Color.White;
            this.SearchGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SearchGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.SearchGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.SearchGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SearchGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.SearchGrid.ColumnHeadersHeight = 18;
            this.SearchGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.SearchGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.signSearch,
            this.typeSearch,
            this.distSearch,
            this.rssiSearch});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SearchGrid.DefaultCellStyle = dataGridViewCellStyle7;
            this.SearchGrid.Dock = System.Windows.Forms.DockStyle.Left;
            this.SearchGrid.GridColor = System.Drawing.Color.White;
            this.SearchGrid.Location = new System.Drawing.Point(0, 0);
            this.SearchGrid.Margin = new System.Windows.Forms.Padding(0);
            this.SearchGrid.MultiSelect = false;
            this.SearchGrid.Name = "SearchGrid";
            this.SearchGrid.ReadOnly = true;
            this.SearchGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SearchGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.SearchGrid.RowHeadersVisible = false;
            this.SearchGrid.RowHeadersWidth = 25;
            this.SearchGrid.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SearchGrid.RowTemplate.Height = 18;
            this.SearchGrid.RowTemplate.ReadOnly = true;
            this.SearchGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SearchGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SearchGrid.Size = new System.Drawing.Size(209, 266);
            this.SearchGrid.TabIndex = 1;
            this.SearchGrid.VirtualMode = true;
            // 
            // signSearch
            // 
            this.signSearch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.signSearch.DataPropertyName = "devSign";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.signSearch.DefaultCellStyle = dataGridViewCellStyle3;
            this.signSearch.FillWeight = 50F;
            this.signSearch.HeaderText = "Sign";
            this.signSearch.MaxInputLength = 5;
            this.signSearch.MinimumWidth = 50;
            this.signSearch.Name = "signSearch";
            this.signSearch.ReadOnly = true;
            this.signSearch.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.signSearch.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.signSearch.Width = 50;
            // 
            // typeSearch
            // 
            this.typeSearch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.typeSearch.DataPropertyName = "devType";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.typeSearch.DefaultCellStyle = dataGridViewCellStyle4;
            this.typeSearch.FillWeight = 50F;
            this.typeSearch.HeaderText = "Type";
            this.typeSearch.MaxInputLength = 5;
            this.typeSearch.MinimumWidth = 50;
            this.typeSearch.Name = "typeSearch";
            this.typeSearch.ReadOnly = true;
            this.typeSearch.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.typeSearch.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.typeSearch.Width = 50;
            // 
            // distSearch
            // 
            this.distSearch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.distSearch.DataPropertyName = "devDist";
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.distSearch.DefaultCellStyle = dataGridViewCellStyle5;
            this.distSearch.FillWeight = 40F;
            this.distSearch.HeaderText = "Dist";
            this.distSearch.MaxInputLength = 5;
            this.distSearch.MinimumWidth = 40;
            this.distSearch.Name = "distSearch";
            this.distSearch.ReadOnly = true;
            this.distSearch.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.distSearch.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.distSearch.Width = 40;
            // 
            // rssiSearch
            // 
            this.rssiSearch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.rssiSearch.DataPropertyName = "devRSSI";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.rssiSearch.DefaultCellStyle = dataGridViewCellStyle6;
            this.rssiSearch.FillWeight = 40F;
            this.rssiSearch.HeaderText = "RSSI";
            this.rssiSearch.MaxInputLength = 3;
            this.rssiSearch.MinimumWidth = 40;
            this.rssiSearch.Name = "rssiSearch";
            this.rssiSearch.ReadOnly = true;
            this.rssiSearch.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.rssiSearch.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.rssiSearch.Width = 40;
            // 
            // HexUpdatePage
            // 
            this.HexUpdatePage.BackColor = System.Drawing.Color.White;
            this.HexUpdatePage.Controls.Add(this.HexUploadFilename);
            this.HexUpdatePage.Controls.Add(this.label4);
            this.HexUpdatePage.Controls.Add(this.label2);
            this.HexUpdatePage.Controls.Add(this.HexCheckCrc);
            this.HexUpdatePage.Controls.Add(this.HexTimeout);
            this.HexUpdatePage.Controls.Add(this.HexPageSize);
            this.HexUpdatePage.Controls.Add(this.HexPathBox);
            this.HexUpdatePage.Controls.Add(this.BytesEnd);
            this.HexUpdatePage.Controls.Add(this.label8);
            this.HexUpdatePage.Controls.Add(this.BytesStart);
            this.HexUpdatePage.Controls.Add(this.UpdateBar);
            this.HexUpdatePage.Controls.Add(this.HexUploadButton);
            this.HexUpdatePage.Controls.Add(this.HexPathButton);
            this.HexUpdatePage.Location = new System.Drawing.Point(4, 22);
            this.HexUpdatePage.Name = "HexUpdatePage";
            this.HexUpdatePage.Size = new System.Drawing.Size(319, 266);
            this.HexUpdatePage.TabIndex = 3;
            this.HexUpdatePage.Text = "Hex Update";
            // 
            // HexUploadFilename
            // 
            this.HexUploadFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HexUploadFilename.Location = new System.Drawing.Point(0, 226);
            this.HexUploadFilename.Name = "HexUploadFilename";
            this.HexUploadFilename.Size = new System.Drawing.Size(316, 13);
            this.HexUploadFilename.TabIndex = 29;
            this.HexUploadFilename.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(332, 50);
            this.label4.Margin = new System.Windows.Forms.Padding(1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Timeout:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(322, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Page Size:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HexCheckCrc
            // 
            this.HexCheckCrc.Checked = true;
            this.HexCheckCrc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HexCheckCrc.Location = new System.Drawing.Point(93, 27);
            this.HexCheckCrc.Margin = new System.Windows.Forms.Padding(1);
            this.HexCheckCrc.Name = "HexCheckCrc";
            this.HexCheckCrc.Size = new System.Drawing.Size(48, 17);
            this.HexCheckCrc.TabIndex = 26;
            this.HexCheckCrc.Text = "CRC";
            this.HexCheckCrc.UseVisualStyleBackColor = true;
            // 
            // HexTimeout
            // 
            this.HexTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.HexTimeout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HexTimeout.Location = new System.Drawing.Point(277, 47);
            this.HexTimeout.Margin = new System.Windows.Forms.Padding(1);
            this.HexTimeout.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.HexTimeout.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.HexTimeout.Name = "HexTimeout";
            this.HexTimeout.Size = new System.Drawing.Size(40, 20);
            this.HexTimeout.TabIndex = 27;
            this.HexTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTipHelper.SetToolTip(this.HexTimeout, "Время ожидания ответа. Seconds");
            this.HexTimeout.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // HexPageSize
            // 
            this.HexPageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.HexPageSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HexPageSize.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.HexPageSize.Location = new System.Drawing.Point(277, 25);
            this.HexPageSize.Margin = new System.Windows.Forms.Padding(1);
            this.HexPageSize.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.HexPageSize.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.HexPageSize.Name = "HexPageSize";
            this.HexPageSize.Size = new System.Drawing.Size(40, 20);
            this.HexPageSize.TabIndex = 7;
            this.HexPageSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTipHelper.SetToolTip(this.HexPageSize, "Размер пакета для отправки. Byte");
            this.HexPageSize.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // HexPathBox
            // 
            this.HexPathBox.AllowDrop = true;
            this.HexPathBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HexPathBox.BackColor = System.Drawing.Color.White;
            this.HexPathBox.FormattingEnabled = true;
            this.HexPathBox.ItemHeight = 13;
            this.HexPathBox.Location = new System.Drawing.Point(0, 2);
            this.HexPathBox.Name = "HexPathBox";
            this.HexPathBox.Size = new System.Drawing.Size(293, 21);
            this.HexPathBox.Sorted = true;
            this.HexPathBox.TabIndex = 21;
            this.ToolTipHelper.SetToolTip(this.HexPathBox, "Перетащите *.hex файл в это поле\r\n");
            this.HexPathBox.TextChanged += new System.EventHandler(this.Hex_Box_TextChanged);
            this.HexPathBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.Hex_Box_DragDrop);
            this.HexPathBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.Hex_Box_DragEnter);
            // 
            // BytesEnd
            // 
            this.BytesEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BytesEnd.BackColor = System.Drawing.Color.Transparent;
            this.BytesEnd.Location = new System.Drawing.Point(171, 249);
            this.BytesEnd.Name = "BytesEnd";
            this.BytesEnd.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.BytesEnd.Size = new System.Drawing.Size(50, 15);
            this.BytesEnd.TabIndex = 5;
            this.BytesEnd.Text = "0";
            this.BytesEnd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(151, 249);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "//";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BytesStart
            // 
            this.BytesStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BytesStart.BackColor = System.Drawing.Color.Transparent;
            this.BytesStart.Location = new System.Drawing.Point(99, 249);
            this.BytesStart.Name = "BytesStart";
            this.BytesStart.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.BytesStart.Size = new System.Drawing.Size(50, 15);
            this.BytesStart.TabIndex = 3;
            this.BytesStart.Text = "0";
            this.BytesStart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UpdateBar
            // 
            this.UpdateBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateBar.Location = new System.Drawing.Point(0, 239);
            this.UpdateBar.Margin = new System.Windows.Forms.Padding(0);
            this.UpdateBar.Name = "UpdateBar";
            this.UpdateBar.Size = new System.Drawing.Size(317, 10);
            this.UpdateBar.Step = 1;
            this.UpdateBar.TabIndex = 6;
            // 
            // HexUploadButton
            // 
            this.HexUploadButton.Enabled = false;
            this.HexUploadButton.Image = ((System.Drawing.Image)(resources.GetObject("HexUploadButton.Image")));
            this.HexUploadButton.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.HexUploadButton.Location = new System.Drawing.Point(-1, 24);
            this.HexUploadButton.Name = "HexUploadButton";
            this.HexUploadButton.Size = new System.Drawing.Size(90, 22);
            this.HexUploadButton.TabIndex = 2;
            this.HexUploadButton.Text = "Upload";
            this.HexUploadButton.UseVisualStyleBackColor = true;
            // 
            // HexPathButton
            // 
            this.HexPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.HexPathButton.BackColor = System.Drawing.Color.Transparent;
            this.HexPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HexPathButton.Image = ((System.Drawing.Image)(resources.GetObject("HexPathButton.Image")));
            this.HexPathButton.Location = new System.Drawing.Point(295, 2);
            this.HexPathButton.Margin = new System.Windows.Forms.Padding(0);
            this.HexPathButton.Name = "HexPathButton";
            this.HexPathButton.Size = new System.Drawing.Size(22, 21);
            this.HexPathButton.TabIndex = 23;
            this.ToolTipHelper.SetToolTip(this.HexPathButton, "Открывает проводник для выбора файла");
            this.HexPathButton.UseVisualStyleBackColor = false;
            this.HexPathButton.Click += new System.EventHandler(this.HexPathButton_Click);
            // 
            // ConfigPage
            // 
            this.ConfigPage.BackColor = System.Drawing.Color.White;
            this.ConfigPage.Controls.Add(this.ConfigAddField);
            this.ConfigPage.Controls.Add(this.ConfigFieldTextBox);
            this.ConfigPage.Controls.Add(this.RMLRRepeatCount);
            this.ConfigPage.Controls.Add(this.RMLRModeCheck);
            this.ConfigPage.Controls.Add(this.ConfigFactoryCheck);
            this.ConfigPage.Controls.Add(this.ConfigDataGrid);
            this.ConfigPage.Controls.Add(this.UploadConfigButton);
            this.ConfigPage.Controls.Add(this.LoadConfigButton);
            this.ConfigPage.Location = new System.Drawing.Point(4, 22);
            this.ConfigPage.Name = "ConfigPage";
            this.ConfigPage.Size = new System.Drawing.Size(319, 266);
            this.ConfigPage.TabIndex = 8;
            this.ConfigPage.Text = "Config";
            // 
            // ConfigAddField
            // 
            this.ConfigAddField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConfigAddField.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ConfigAddField.Image = ((System.Drawing.Image)(resources.GetObject("ConfigAddField.Image")));
            this.ConfigAddField.Location = new System.Drawing.Point(110, 202);
            this.ConfigAddField.Margin = new System.Windows.Forms.Padding(1);
            this.ConfigAddField.Name = "ConfigAddField";
            this.ConfigAddField.Size = new System.Drawing.Size(20, 20);
            this.ConfigAddField.TabIndex = 31;
            this.ToolTipHelper.SetToolTip(this.ConfigAddField, "Добавить поле");
            this.ConfigAddField.UseVisualStyleBackColor = true;
            // 
            // ConfigFieldTextBox
            // 
            this.ConfigFieldTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConfigFieldTextBox.Location = new System.Drawing.Point(0, 202);
            this.ConfigFieldTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.ConfigFieldTextBox.MaxLength = 32;
            this.ConfigFieldTextBox.Name = "ConfigFieldTextBox";
            this.ConfigFieldTextBox.Size = new System.Drawing.Size(108, 20);
            this.ConfigFieldTextBox.TabIndex = 30;
            this.ConfigFieldTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTipHelper.SetToolTip(this.ConfigFieldTextBox, "Введите название поля");
            // 
            // RMLRRepeatCount
            // 
            this.RMLRRepeatCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RMLRRepeatCount.Location = new System.Drawing.Point(199, 225);
            this.RMLRRepeatCount.Margin = new System.Windows.Forms.Padding(1);
            this.RMLRRepeatCount.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.RMLRRepeatCount.Name = "RMLRRepeatCount";
            this.RMLRRepeatCount.Size = new System.Drawing.Size(28, 20);
            this.RMLRRepeatCount.TabIndex = 29;
            this.ToolTipHelper.SetToolTip(this.RMLRRepeatCount, "Количество миганий светодиода");
            this.RMLRRepeatCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.RMLRRepeatCount.Visible = false;
            // 
            // RMLRModeCheck
            // 
            this.RMLRModeCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RMLRModeCheck.AutoSize = true;
            this.RMLRModeCheck.ContextMenuStrip = this.RMLRMenuStrip;
            this.RMLRModeCheck.Location = new System.Drawing.Point(110, 227);
            this.RMLRModeCheck.Name = "RMLRModeCheck";
            this.RMLRModeCheck.Size = new System.Drawing.Size(87, 17);
            this.RMLRModeCheck.TabIndex = 28;
            this.RMLRModeCheck.Text = "RMLR Mode";
            this.ToolTipHelper.SetToolTip(this.RMLRModeCheck, resources.GetString("RMLRModeCheck.ToolTip"));
            this.RMLRModeCheck.UseVisualStyleBackColor = true;
            // 
            // RMLRMenuStrip
            // 
            this.RMLRMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RMLRRed,
            this.RMLRGreen,
            this.RMLRBlue,
            this.RMLRBuzzer,
            this.RMLRSeparator,
            this.RMLRRepeat});
            this.RMLRMenuStrip.Name = "RGBBMenuStrip";
            this.RMLRMenuStrip.Size = new System.Drawing.Size(111, 120);
            // 
            // RMLRRed
            // 
            this.RMLRRed.Checked = true;
            this.RMLRRed.CheckOnClick = true;
            this.RMLRRed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RMLRRed.Name = "RMLRRed";
            this.RMLRRed.Size = new System.Drawing.Size(110, 22);
            this.RMLRRed.Text = "Red";
            // 
            // RMLRGreen
            // 
            this.RMLRGreen.Checked = true;
            this.RMLRGreen.CheckOnClick = true;
            this.RMLRGreen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RMLRGreen.Name = "RMLRGreen";
            this.RMLRGreen.Size = new System.Drawing.Size(110, 22);
            this.RMLRGreen.Text = "Green";
            // 
            // RMLRBlue
            // 
            this.RMLRBlue.Checked = true;
            this.RMLRBlue.CheckOnClick = true;
            this.RMLRBlue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RMLRBlue.Name = "RMLRBlue";
            this.RMLRBlue.Size = new System.Drawing.Size(110, 22);
            this.RMLRBlue.Text = "Blue";
            // 
            // RMLRBuzzer
            // 
            this.RMLRBuzzer.Checked = true;
            this.RMLRBuzzer.CheckOnClick = true;
            this.RMLRBuzzer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RMLRBuzzer.Name = "RMLRBuzzer";
            this.RMLRBuzzer.Size = new System.Drawing.Size(110, 22);
            this.RMLRBuzzer.Text = "Buzzer";
            // 
            // RMLRSeparator
            // 
            this.RMLRSeparator.Name = "RMLRSeparator";
            this.RMLRSeparator.Size = new System.Drawing.Size(107, 6);
            // 
            // RMLRRepeat
            // 
            this.RMLRRepeat.CheckOnClick = true;
            this.RMLRRepeat.Name = "RMLRRepeat";
            this.RMLRRepeat.Size = new System.Drawing.Size(110, 22);
            this.RMLRRepeat.Text = "Repeat";
            // 
            // ConfigFactoryCheck
            // 
            this.ConfigFactoryCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConfigFactoryCheck.AutoSize = true;
            this.ConfigFactoryCheck.Location = new System.Drawing.Point(110, 248);
            this.ConfigFactoryCheck.Name = "ConfigFactoryCheck";
            this.ConfigFactoryCheck.Size = new System.Drawing.Size(61, 17);
            this.ConfigFactoryCheck.TabIndex = 26;
            this.ConfigFactoryCheck.Text = "Factory";
            this.ConfigFactoryCheck.UseVisualStyleBackColor = true;
            // 
            // ConfigDataGrid
            // 
            this.ConfigDataGrid.AllowUserToAddRows = false;
            this.ConfigDataGrid.AllowUserToOrderColumns = true;
            this.ConfigDataGrid.AllowUserToResizeRows = false;
            this.ConfigDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.ConfigDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConfigDataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ConfigDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.ConfigDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ConfigDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FieldsColumn,
            this.EnabledColumn,
            this.LoadFieldColumn,
            this.UploadFieldColumn});
            this.ConfigDataGrid.ContextMenuStrip = this.ConfigClearMenuStrip;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ConfigDataGrid.DefaultCellStyle = dataGridViewCellStyle12;
            this.ConfigDataGrid.EnableHeadersVisualStyles = false;
            this.ConfigDataGrid.GridColor = System.Drawing.Color.DarkGray;
            this.ConfigDataGrid.Location = new System.Drawing.Point(0, 0);
            this.ConfigDataGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ConfigDataGrid.Name = "ConfigDataGrid";
            this.ConfigDataGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ConfigDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.ConfigDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.ConfigDataGrid.ShowEditingIcon = false;
            this.ConfigDataGrid.Size = new System.Drawing.Size(319, 201);
            this.ConfigDataGrid.TabIndex = 0;
            this.ConfigDataGrid.TabStop = false;
            this.ConfigDataGrid.VirtualMode = true;
            // 
            // ConfigClearMenuStrip
            // 
            this.ConfigClearMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConfigEnableAllMenuItem,
            this.ConfigClearMenuItem});
            this.ConfigClearMenuStrip.Name = "ConfigClearMenuStrip";
            this.ConfigClearMenuStrip.Size = new System.Drawing.Size(125, 48);
            // 
            // ConfigEnableAllMenuItem
            // 
            this.ConfigEnableAllMenuItem.CheckOnClick = true;
            this.ConfigEnableAllMenuItem.Name = "ConfigEnableAllMenuItem";
            this.ConfigEnableAllMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ConfigEnableAllMenuItem.Text = "Enable all";
            // 
            // ConfigClearMenuItem
            // 
            this.ConfigClearMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConfigClearLoad,
            this.ConfigClearUpload,
            this.ConfigClearAll});
            this.ConfigClearMenuItem.Name = "ConfigClearMenuItem";
            this.ConfigClearMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ConfigClearMenuItem.Text = "Clear";
            // 
            // ConfigClearLoad
            // 
            this.ConfigClearLoad.Name = "ConfigClearLoad";
            this.ConfigClearLoad.Size = new System.Drawing.Size(112, 22);
            this.ConfigClearLoad.Text = "Load";
            this.ConfigClearLoad.ToolTipText = "Очистка колонки Load Value";
            // 
            // ConfigClearUpload
            // 
            this.ConfigClearUpload.Name = "ConfigClearUpload";
            this.ConfigClearUpload.Size = new System.Drawing.Size(112, 22);
            this.ConfigClearUpload.Text = "Upload";
            this.ConfigClearUpload.ToolTipText = "Очистка колонки Upload Value";
            // 
            // ConfigClearAll
            // 
            this.ConfigClearAll.Name = "ConfigClearAll";
            this.ConfigClearAll.Size = new System.Drawing.Size(112, 22);
            this.ConfigClearAll.Text = "All";
            this.ConfigClearAll.ToolTipText = "Очистка колонок Load Value и Upload Value";
            // 
            // UploadConfigButton
            // 
            this.UploadConfigButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UploadConfigButton.Image = ((System.Drawing.Image)(resources.GetObject("UploadConfigButton.Image")));
            this.UploadConfigButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UploadConfigButton.Location = new System.Drawing.Point(-1, 245);
            this.UploadConfigButton.Margin = new System.Windows.Forms.Padding(0);
            this.UploadConfigButton.Name = "UploadConfigButton";
            this.UploadConfigButton.Size = new System.Drawing.Size(110, 22);
            this.UploadConfigButton.TabIndex = 2;
            this.UploadConfigButton.Text = "Upload to device";
            this.UploadConfigButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolTipHelper.SetToolTip(this.UploadConfigButton, "Загрузка конфигурации на устройства");
            this.UploadConfigButton.UseVisualStyleBackColor = true;
            // 
            // LoadConfigButton
            // 
            this.LoadConfigButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LoadConfigButton.Image = ((System.Drawing.Image)(resources.GetObject("LoadConfigButton.Image")));
            this.LoadConfigButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LoadConfigButton.Location = new System.Drawing.Point(-1, 223);
            this.LoadConfigButton.Margin = new System.Windows.Forms.Padding(0);
            this.LoadConfigButton.Name = "LoadConfigButton";
            this.LoadConfigButton.Size = new System.Drawing.Size(110, 22);
            this.LoadConfigButton.TabIndex = 1;
            this.LoadConfigButton.Text = "Load from device";
            this.LoadConfigButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolTipHelper.SetToolTip(this.LoadConfigButton, "Загрузка конфигурации из устройства");
            this.LoadConfigButton.UseVisualStyleBackColor = true;
            // 
            // InfoPage
            // 
            this.InfoPage.BackColor = System.Drawing.Color.White;
            this.InfoPage.Controls.Add(this.numericInfoSeconds);
            this.InfoPage.Controls.Add(this.label3);
            this.InfoPage.Controls.Add(this.buttonInfoStop);
            this.InfoPage.Controls.Add(this.InfoTree);
            this.InfoPage.Location = new System.Drawing.Point(4, 22);
            this.InfoPage.Name = "InfoPage";
            this.InfoPage.Padding = new System.Windows.Forms.Padding(3);
            this.InfoPage.Size = new System.Drawing.Size(319, 266);
            this.InfoPage.TabIndex = 7;
            this.InfoPage.Text = "Info";
            // 
            // numericInfoSeconds
            // 
            this.numericInfoSeconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericInfoSeconds.Location = new System.Drawing.Point(44, 246);
            this.numericInfoSeconds.Margin = new System.Windows.Forms.Padding(1);
            this.numericInfoSeconds.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericInfoSeconds.Name = "numericInfoSeconds";
            this.numericInfoSeconds.Size = new System.Drawing.Size(34, 20);
            this.numericInfoSeconds.TabIndex = 5;
            this.numericInfoSeconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTipHelper.SetToolTip(this.numericInfoSeconds, "Время ожидания ответа, в секундах");
            this.numericInfoSeconds.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 249);
            this.label3.Margin = new System.Windows.Forms.Padding(1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Timeout";
            // 
            // buttonInfoStop
            // 
            this.buttonInfoStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonInfoStop.BackColor = System.Drawing.Color.White;
            this.buttonInfoStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonInfoStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonInfoStop.Location = new System.Drawing.Point(80, 245);
            this.buttonInfoStop.Margin = new System.Windows.Forms.Padding(1);
            this.buttonInfoStop.Name = "buttonInfoStop";
            this.buttonInfoStop.Size = new System.Drawing.Size(37, 22);
            this.buttonInfoStop.TabIndex = 44;
            this.buttonInfoStop.Text = "Stop";
            this.buttonInfoStop.UseVisualStyleBackColor = false;
            this.buttonInfoStop.Visible = false;
            // 
            // InfoTree
            // 
            this.InfoTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoTree.BackColor = System.Drawing.Color.White;
            this.InfoTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InfoTree.ContextMenuStrip = this.infoMenuStrip;
            this.InfoTree.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InfoTree.Indent = 15;
            this.InfoTree.ItemHeight = 17;
            this.InfoTree.Location = new System.Drawing.Point(0, 0);
            this.InfoTree.Margin = new System.Windows.Forms.Padding(0);
            this.InfoTree.Name = "InfoTree";
            treeNode1.BackColor = System.Drawing.Color.White;
            treeNode1.ForeColor = System.Drawing.SystemColors.HotTrack;
            treeNode1.Name = "WhoAreYouInfo";
            treeNode1.NodeFont = new System.Drawing.Font("Consolas", 9F);
            treeNode1.Text = "Who are you";
            treeNode1.ToolTipText = "Нажмите, для отправки команды";
            treeNode2.BackColor = System.Drawing.Color.White;
            treeNode2.ForeColor = System.Drawing.SystemColors.HotTrack;
            treeNode2.Name = "StatusInfo";
            treeNode2.NodeFont = new System.Drawing.Font("Consolas", 9F);
            treeNode2.Text = "Status";
            treeNode2.ToolTipText = "Нажмите, для отправки команды";
            treeNode3.BackColor = System.Drawing.Color.White;
            treeNode3.ForeColor = System.Drawing.SystemColors.HotTrack;
            treeNode3.Name = "GetNearInfo";
            treeNode3.NodeFont = new System.Drawing.Font("Consolas", 9F);
            treeNode3.Text = "Get Near";
            treeNode3.ToolTipText = "Нажмите, для отправки команды";
            this.InfoTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.InfoTree.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.InfoTree.ShowNodeToolTips = true;
            this.InfoTree.ShowRootLines = false;
            this.InfoTree.Size = new System.Drawing.Size(319, 244);
            this.InfoTree.TabIndex = 1;
            this.ToolTipHelper.SetToolTip(this.InfoTree, "Нажмите правую кнопку мыши для дополнительного меню");
            // 
            // infoMenuStrip
            // 
            this.infoMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearInfoMenuStrip,
            this.toolStripSeparator8,
            this.saveToCsvInfoMenuStrip});
            this.infoMenuStrip.Name = "infoMenuStrip";
            this.infoMenuStrip.Size = new System.Drawing.Size(156, 54);
            // 
            // clearInfoMenuStrip
            // 
            this.clearInfoMenuStrip.Name = "clearInfoMenuStrip";
            this.clearInfoMenuStrip.Size = new System.Drawing.Size(155, 22);
            this.clearInfoMenuStrip.Text = "Clear";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(152, 6);
            // 
            // saveToCsvInfoMenuStrip
            // 
            this.saveToCsvInfoMenuStrip.Name = "saveToCsvInfoMenuStrip";
            this.saveToCsvInfoMenuStrip.Size = new System.Drawing.Size(155, 22);
            this.saveToCsvInfoMenuStrip.Text = "Save to CSV file";
            // 
            // TestPage
            // 
            this.TestPage.BackColor = System.Drawing.Color.White;
            this.TestPage.Controls.Add(this.TestPages);
            this.TestPage.Location = new System.Drawing.Point(4, 22);
            this.TestPage.Name = "TestPage";
            this.TestPage.Padding = new System.Windows.Forms.Padding(3);
            this.TestPage.Size = new System.Drawing.Size(319, 266);
            this.TestPage.TabIndex = 6;
            this.TestPage.Text = "Test";
            // 
            // TestPages
            // 
            this.TestPages.Controls.Add(this.RS485Page);
            this.TestPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestPages.Location = new System.Drawing.Point(3, 3);
            this.TestPages.Name = "TestPages";
            this.TestPages.SelectedIndex = 0;
            this.TestPages.Size = new System.Drawing.Size(313, 260);
            this.TestPages.TabIndex = 0;
            // 
            // RS485Page
            // 
            this.RS485Page.BackColor = System.Drawing.Color.White;
            this.RS485Page.Controls.Add(this.extendedMenuPanel);
            this.RS485Page.Controls.Add(this.StatusRS485GridView);
            this.RS485Page.Location = new System.Drawing.Point(4, 22);
            this.RS485Page.Name = "RS485Page";
            this.RS485Page.Padding = new System.Windows.Forms.Padding(3);
            this.RS485Page.Size = new System.Drawing.Size(305, 234);
            this.RS485Page.TabIndex = 0;
            this.RS485Page.Text = "RS485";
            // 
            // extendedMenuPanel
            // 
            this.extendedMenuPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.extendedMenuPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.extendedMenuPanel.Controls.Add(this.SortedColumnCombo);
            this.extendedMenuPanel.Controls.Add(this.SortByButton);
            this.extendedMenuPanel.Controls.Add(this.MoreInfoTestRS485);
            this.extendedMenuPanel.Controls.Add(this.timerPanelTest);
            this.extendedMenuPanel.Controls.Add(this.WorkingTimeLabel);
            this.extendedMenuPanel.Controls.Add(this.label9);
            this.extendedMenuPanel.Controls.Add(this.SaveLogTestRS485);
            this.extendedMenuPanel.Controls.Add(this.ClearDataTestRS485);
            this.extendedMenuPanel.Controls.Add(this.ClearInfoTestRS485);
            this.extendedMenuPanel.Controls.Add(this.settingsGroupBox);
            this.extendedMenuPanel.Controls.Add(this.scanGroupBox);
            this.extendedMenuPanel.Controls.Add(this.ShowExtendedMenu);
            this.extendedMenuPanel.Controls.Add(this.StartTestRSButton);
            this.extendedMenuPanel.Controls.Add(this.AutoScanToTest);
            this.extendedMenuPanel.Location = new System.Drawing.Point(0, 65);
            this.extendedMenuPanel.Margin = new System.Windows.Forms.Padding(0);
            this.extendedMenuPanel.Name = "extendedMenuPanel";
            this.extendedMenuPanel.Size = new System.Drawing.Size(303, 169);
            this.extendedMenuPanel.TabIndex = 31;
            // 
            // SortedColumnCombo
            // 
            this.SortedColumnCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SortedColumnCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SortedColumnCombo.FormattingEnabled = true;
            this.SortedColumnCombo.Items.AddRange(new object[] {
            "Interface",
            "Sign",
            "Tx",
            "%Error"});
            this.SortedColumnCombo.Location = new System.Drawing.Point(212, 97);
            this.SortedColumnCombo.Margin = new System.Windows.Forms.Padding(1);
            this.SortedColumnCombo.Name = "SortedColumnCombo";
            this.SortedColumnCombo.Size = new System.Drawing.Size(88, 21);
            this.SortedColumnCombo.TabIndex = 46;
            // 
            // SortByButton
            // 
            this.SortByButton.ContextMenuStrip = this.RS485SortMenuStrip;
            this.SortByButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SortByButton.Image = ((System.Drawing.Image)(resources.GetObject("SortByButton.Image")));
            this.SortByButton.Location = new System.Drawing.Point(190, 97);
            this.SortByButton.Margin = new System.Windows.Forms.Padding(0);
            this.SortByButton.Name = "SortByButton";
            this.SortByButton.Size = new System.Drawing.Size(21, 21);
            this.SortByButton.TabIndex = 44;
            this.SortByButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolTipHelper.SetToolTip(this.SortByButton, "Нажмите правой кнопкой мыши, для выбора сортировки");
            this.SortByButton.UseVisualStyleBackColor = true;
            // 
            // RS485SortMenuStrip
            // 
            this.RS485SortMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.byAscMenuItem,
            this.byDescMenuItem});
            this.RS485SortMenuStrip.Name = "RS485SortMenuStrip";
            this.RS485SortMenuStrip.Size = new System.Drawing.Size(153, 48);
            // 
            // byAscMenuItem
            // 
            this.byAscMenuItem.Checked = true;
            this.byAscMenuItem.CheckOnClick = true;
            this.byAscMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.byAscMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("byAscMenuItem.Image")));
            this.byAscMenuItem.Name = "byAscMenuItem";
            this.byAscMenuItem.Size = new System.Drawing.Size(152, 22);
            this.byAscMenuItem.Text = "By Ascending";
            // 
            // byDescMenuItem
            // 
            this.byDescMenuItem.CheckOnClick = true;
            this.byDescMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("byDescMenuItem.Image")));
            this.byDescMenuItem.Name = "byDescMenuItem";
            this.byDescMenuItem.Size = new System.Drawing.Size(152, 22);
            this.byDescMenuItem.Text = "By Descending";
            // 
            // MoreInfoTestRS485
            // 
            this.MoreInfoTestRS485.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MoreInfoTestRS485.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.MoreInfoTestRS485.Image = ((System.Drawing.Image)(resources.GetObject("MoreInfoTestRS485.Image")));
            this.MoreInfoTestRS485.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.MoreInfoTestRS485.Location = new System.Drawing.Point(190, 74);
            this.MoreInfoTestRS485.Margin = new System.Windows.Forms.Padding(0);
            this.MoreInfoTestRS485.Name = "MoreInfoTestRS485";
            this.MoreInfoTestRS485.Size = new System.Drawing.Size(110, 22);
            this.MoreInfoTestRS485.TabIndex = 43;
            this.MoreInfoTestRS485.Text = "More info";
            this.MoreInfoTestRS485.UseVisualStyleBackColor = true;
            // 
            // timerPanelTest
            // 
            this.timerPanelTest.Controls.Add(this.numericHoursTest);
            this.timerPanelTest.Controls.Add(this.numericSecondsTest);
            this.timerPanelTest.Controls.Add(this.label7);
            this.timerPanelTest.Controls.Add(this.numericMinutesTest);
            this.timerPanelTest.Controls.Add(this.numericDaysTest);
            this.timerPanelTest.Location = new System.Drawing.Point(0, 125);
            this.timerPanelTest.Margin = new System.Windows.Forms.Padding(0);
            this.timerPanelTest.Name = "timerPanelTest";
            this.timerPanelTest.Size = new System.Drawing.Size(189, 22);
            this.timerPanelTest.TabIndex = 40;
            this.timerPanelTest.Visible = false;
            // 
            // numericHoursTest
            // 
            this.numericHoursTest.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericHoursTest.Location = new System.Drawing.Point(87, 3);
            this.numericHoursTest.Margin = new System.Windows.Forms.Padding(0);
            this.numericHoursTest.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericHoursTest.Name = "numericHoursTest";
            this.numericHoursTest.Size = new System.Drawing.Size(30, 16);
            this.numericHoursTest.TabIndex = 37;
            this.numericHoursTest.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTipHelper.SetToolTip(this.numericHoursTest, "Hours");
            this.numericHoursTest.ValueChanged += new System.EventHandler(this.numericHoursTest_ValueChanged);
            // 
            // numericSecondsTest
            // 
            this.numericSecondsTest.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericSecondsTest.Location = new System.Drawing.Point(147, 3);
            this.numericSecondsTest.Margin = new System.Windows.Forms.Padding(0);
            this.numericSecondsTest.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericSecondsTest.Name = "numericSecondsTest";
            this.numericSecondsTest.Size = new System.Drawing.Size(30, 16);
            this.numericSecondsTest.TabIndex = 39;
            this.numericSecondsTest.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTipHelper.SetToolTip(this.numericSecondsTest, "Seconds");
            this.numericSecondsTest.ValueChanged += new System.EventHandler(this.numericSecondsTest_ValueChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(2, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 20);
            this.label7.TabIndex = 40;
            this.label7.Text = "Timer:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericMinutesTest
            // 
            this.numericMinutesTest.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericMinutesTest.Location = new System.Drawing.Point(117, 3);
            this.numericMinutesTest.Margin = new System.Windows.Forms.Padding(0);
            this.numericMinutesTest.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericMinutesTest.Name = "numericMinutesTest";
            this.numericMinutesTest.Size = new System.Drawing.Size(30, 16);
            this.numericMinutesTest.TabIndex = 36;
            this.numericMinutesTest.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTipHelper.SetToolTip(this.numericMinutesTest, "Minutes");
            this.numericMinutesTest.ValueChanged += new System.EventHandler(this.numericMinutesTest_ValueChanged);
            // 
            // numericDaysTest
            // 
            this.numericDaysTest.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericDaysTest.Location = new System.Drawing.Point(36, 3);
            this.numericDaysTest.Margin = new System.Windows.Forms.Padding(0);
            this.numericDaysTest.Maximum = new decimal(new int[] {
            168,
            0,
            0,
            0});
            this.numericDaysTest.Name = "numericDaysTest";
            this.numericDaysTest.Size = new System.Drawing.Size(40, 16);
            this.numericDaysTest.TabIndex = 38;
            this.numericDaysTest.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTipHelper.SetToolTip(this.numericDaysTest, "Days");
            // 
            // WorkingTimeLabel
            // 
            this.WorkingTimeLabel.Location = new System.Drawing.Point(36, 146);
            this.WorkingTimeLabel.Name = "WorkingTimeLabel";
            this.WorkingTimeLabel.Size = new System.Drawing.Size(81, 21);
            this.WorkingTimeLabel.TabIndex = 41;
            this.WorkingTimeLabel.Text = "00:00:00";
            this.WorkingTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(2, 146);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 21);
            this.label9.TabIndex = 42;
            this.label9.Text = "Total";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SaveLogTestRS485
            // 
            this.SaveLogTestRS485.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveLogTestRS485.Enabled = false;
            this.SaveLogTestRS485.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveLogTestRS485.Image = ((System.Drawing.Image)(resources.GetObject("SaveLogTestRS485.Image")));
            this.SaveLogTestRS485.Location = new System.Drawing.Point(280, -1);
            this.SaveLogTestRS485.Margin = new System.Windows.Forms.Padding(0);
            this.SaveLogTestRS485.Name = "SaveLogTestRS485";
            this.SaveLogTestRS485.Size = new System.Drawing.Size(22, 22);
            this.SaveLogTestRS485.TabIndex = 34;
            this.SaveLogTestRS485.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolTipHelper.SetToolTip(this.SaveLogTestRS485, "Сохранить текущие данные из таблицы в log файл");
            this.SaveLogTestRS485.UseVisualStyleBackColor = true;
            this.SaveLogTestRS485.Click += new System.EventHandler(this.SaveLogTestRS485_Click);
            // 
            // ClearDataTestRS485
            // 
            this.ClearDataTestRS485.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearDataTestRS485.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ClearDataTestRS485.Image = ((System.Drawing.Image)(resources.GetObject("ClearDataTestRS485.Image")));
            this.ClearDataTestRS485.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ClearDataTestRS485.Location = new System.Drawing.Point(190, 28);
            this.ClearDataTestRS485.Margin = new System.Windows.Forms.Padding(0);
            this.ClearDataTestRS485.Name = "ClearDataTestRS485";
            this.ClearDataTestRS485.Size = new System.Drawing.Size(110, 22);
            this.ClearDataTestRS485.TabIndex = 25;
            this.ClearDataTestRS485.Text = "Clear all";
            this.ClearDataTestRS485.UseVisualStyleBackColor = true;
            this.ClearDataTestRS485.Click += new System.EventHandler(this.ClearDataStatusRM_Click);
            // 
            // ClearInfoTestRS485
            // 
            this.ClearInfoTestRS485.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearInfoTestRS485.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ClearInfoTestRS485.Image = ((System.Drawing.Image)(resources.GetObject("ClearInfoTestRS485.Image")));
            this.ClearInfoTestRS485.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ClearInfoTestRS485.Location = new System.Drawing.Point(190, 51);
            this.ClearInfoTestRS485.Margin = new System.Windows.Forms.Padding(0);
            this.ClearInfoTestRS485.Name = "ClearInfoTestRS485";
            this.ClearInfoTestRS485.Size = new System.Drawing.Size(110, 22);
            this.ClearInfoTestRS485.TabIndex = 24;
            this.ClearInfoTestRS485.Text = "Clear info";
            this.ClearInfoTestRS485.UseVisualStyleBackColor = true;
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.Controls.Add(this.ClearBufferSettingsTestBox);
            this.settingsGroupBox.Controls.Add(this.TimerSettingsTestBox);
            this.settingsGroupBox.Controls.Add(this.RadioSettingsTestBox);
            this.settingsGroupBox.Location = new System.Drawing.Point(104, 22);
            this.settingsGroupBox.Margin = new System.Windows.Forms.Padding(0);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Padding = new System.Windows.Forms.Padding(0);
            this.settingsGroupBox.Size = new System.Drawing.Size(85, 104);
            this.settingsGroupBox.TabIndex = 33;
            this.settingsGroupBox.TabStop = false;
            this.settingsGroupBox.Text = "Settings";
            // 
            // ClearBufferSettingsTestBox
            // 
            this.ClearBufferSettingsTestBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClearBufferSettingsTestBox.Checked = true;
            this.ClearBufferSettingsTestBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ClearBufferSettingsTestBox.Location = new System.Drawing.Point(3, 13);
            this.ClearBufferSettingsTestBox.Margin = new System.Windows.Forms.Padding(0);
            this.ClearBufferSettingsTestBox.Name = "ClearBufferSettingsTestBox";
            this.ClearBufferSettingsTestBox.Size = new System.Drawing.Size(80, 17);
            this.ClearBufferSettingsTestBox.TabIndex = 29;
            this.ClearBufferSettingsTestBox.Text = "Clear buffer";
            this.ToolTipHelper.SetToolTip(this.ClearBufferSettingsTestBox, "Очистка входного буфера после ошибки");
            this.ClearBufferSettingsTestBox.UseVisualStyleBackColor = true;
            // 
            // TimerSettingsTestBox
            // 
            this.TimerSettingsTestBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TimerSettingsTestBox.AutoSize = true;
            this.TimerSettingsTestBox.Location = new System.Drawing.Point(3, 45);
            this.TimerSettingsTestBox.Margin = new System.Windows.Forms.Padding(0);
            this.TimerSettingsTestBox.Name = "TimerSettingsTestBox";
            this.TimerSettingsTestBox.Size = new System.Drawing.Size(52, 17);
            this.TimerSettingsTestBox.TabIndex = 28;
            this.TimerSettingsTestBox.Text = "Timer";
            this.ToolTipHelper.SetToolTip(this.TimerSettingsTestBox, "Установка времени тестирования\r\n");
            this.TimerSettingsTestBox.UseVisualStyleBackColor = true;
            // 
            // RadioSettingsTestBox
            // 
            this.RadioSettingsTestBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RadioSettingsTestBox.AutoSize = true;
            this.RadioSettingsTestBox.Checked = true;
            this.RadioSettingsTestBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RadioSettingsTestBox.Location = new System.Drawing.Point(3, 29);
            this.RadioSettingsTestBox.Margin = new System.Windows.Forms.Padding(0);
            this.RadioSettingsTestBox.Name = "RadioSettingsTestBox";
            this.RadioSettingsTestBox.Size = new System.Drawing.Size(54, 17);
            this.RadioSettingsTestBox.TabIndex = 27;
            this.RadioSettingsTestBox.Text = "Radio";
            this.ToolTipHelper.SetToolTip(this.RadioSettingsTestBox, "Включение в тестирование таких команд как Get Near и Dist Tof\r\nПри отправке коман" +
        "ды Get Near, если кол-во устройств меньше 0, то засчитывается BadRadio");
            this.RadioSettingsTestBox.UseVisualStyleBackColor = true;
            // 
            // scanGroupBox
            // 
            this.scanGroupBox.Controls.Add(this.label6);
            this.scanGroupBox.Controls.Add(this.label5);
            this.scanGroupBox.Controls.Add(this.ManualScanToTest);
            this.scanGroupBox.Controls.Add(this.maxSigToScan);
            this.scanGroupBox.Controls.Add(this.AddSignatureIDToTest);
            this.scanGroupBox.Controls.Add(this.minSigToScan);
            this.scanGroupBox.Location = new System.Drawing.Point(0, 22);
            this.scanGroupBox.Margin = new System.Windows.Forms.Padding(0);
            this.scanGroupBox.Name = "scanGroupBox";
            this.scanGroupBox.Padding = new System.Windows.Forms.Padding(0);
            this.scanGroupBox.Size = new System.Drawing.Size(103, 104);
            this.scanGroupBox.TabIndex = 32;
            this.scanGroupBox.TabStop = false;
            this.scanGroupBox.Text = "Scan";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(55, 64);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "max";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 64);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "min";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ManualScanToTest
            // 
            this.ManualScanToTest.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ManualScanToTest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ManualScanToTest.Image = ((System.Drawing.Image)(resources.GetObject("ManualScanToTest.Image")));
            this.ManualScanToTest.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ManualScanToTest.Location = new System.Drawing.Point(6, 39);
            this.ManualScanToTest.Margin = new System.Windows.Forms.Padding(0);
            this.ManualScanToTest.Name = "ManualScanToTest";
            this.ManualScanToTest.Size = new System.Drawing.Size(90, 22);
            this.ManualScanToTest.TabIndex = 30;
            this.ManualScanToTest.Text = "Manual Scan";
            this.ManualScanToTest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ManualScanToTest.UseVisualStyleBackColor = true;
            // 
            // maxSigToScan
            // 
            this.maxSigToScan.Location = new System.Drawing.Point(51, 78);
            this.maxSigToScan.Margin = new System.Windows.Forms.Padding(0);
            this.maxSigToScan.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.maxSigToScan.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxSigToScan.Name = "maxSigToScan";
            this.maxSigToScan.Size = new System.Drawing.Size(50, 20);
            this.maxSigToScan.TabIndex = 29;
            this.maxSigToScan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTipHelper.SetToolTip(this.maxSigToScan, "Maximum addr");
            this.maxSigToScan.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // AddSignatureIDToTest
            // 
            this.AddSignatureIDToTest.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.AddSignatureIDToTest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddSignatureIDToTest.Image = ((System.Drawing.Image)(resources.GetObject("AddSignatureIDToTest.Image")));
            this.AddSignatureIDToTest.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AddSignatureIDToTest.Location = new System.Drawing.Point(6, 16);
            this.AddSignatureIDToTest.Margin = new System.Windows.Forms.Padding(0);
            this.AddSignatureIDToTest.Name = "AddSignatureIDToTest";
            this.AddSignatureIDToTest.Size = new System.Drawing.Size(90, 22);
            this.AddSignatureIDToTest.TabIndex = 26;
            this.AddSignatureIDToTest.Text = "Signature ID";
            this.AddSignatureIDToTest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AddSignatureIDToTest.UseVisualStyleBackColor = true;
            // 
            // minSigToScan
            // 
            this.minSigToScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.minSigToScan.Location = new System.Drawing.Point(2, 78);
            this.minSigToScan.Margin = new System.Windows.Forms.Padding(0);
            this.minSigToScan.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.minSigToScan.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.minSigToScan.Name = "minSigToScan";
            this.minSigToScan.Size = new System.Drawing.Size(50, 20);
            this.minSigToScan.TabIndex = 28;
            this.minSigToScan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTipHelper.SetToolTip(this.minSigToScan, "Minimum addr");
            this.minSigToScan.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.minSigToScan.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.minSigToScan.ValueChanged += new System.EventHandler(this.minSigToScan_ValueChanged);
            // 
            // ShowExtendedMenu
            // 
            this.ShowExtendedMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowExtendedMenu.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ShowExtendedMenu.Image = ((System.Drawing.Image)(resources.GetObject("ShowExtendedMenu.Image")));
            this.ShowExtendedMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ShowExtendedMenu.Location = new System.Drawing.Point(147, -1);
            this.ShowExtendedMenu.Margin = new System.Windows.Forms.Padding(0);
            this.ShowExtendedMenu.Name = "ShowExtendedMenu";
            this.ShowExtendedMenu.Size = new System.Drawing.Size(134, 22);
            this.ShowExtendedMenu.TabIndex = 31;
            this.ShowExtendedMenu.Text = "Hide &menu";
            this.ToolTipHelper.SetToolTip(this.ShowExtendedMenu, "Показать расширенное меню");
            this.ShowExtendedMenu.UseVisualStyleBackColor = true;
            this.ShowExtendedMenu.Click += new System.EventHandler(this.ShowExtendedMenu_Click);
            // 
            // StartTestRSButton
            // 
            this.StartTestRSButton.Enabled = false;
            this.StartTestRSButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StartTestRSButton.Image = ((System.Drawing.Image)(resources.GetObject("StartTestRSButton.Image")));
            this.StartTestRSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.StartTestRSButton.Location = new System.Drawing.Point(-1, -1);
            this.StartTestRSButton.Margin = new System.Windows.Forms.Padding(0);
            this.StartTestRSButton.Name = "StartTestRSButton";
            this.StartTestRSButton.Size = new System.Drawing.Size(75, 22);
            this.StartTestRSButton.TabIndex = 23;
            this.StartTestRSButton.Text = "&Start Test";
            this.StartTestRSButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolTipHelper.SetToolTip(this.StartTestRSButton, "Запуск \\ Остановка теста");
            this.StartTestRSButton.UseVisualStyleBackColor = true;
            // 
            // AutoScanToTest
            // 
            this.AutoScanToTest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AutoScanToTest.Image = ((System.Drawing.Image)(resources.GetObject("AutoScanToTest.Image")));
            this.AutoScanToTest.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AutoScanToTest.Location = new System.Drawing.Point(73, -1);
            this.AutoScanToTest.Margin = new System.Windows.Forms.Padding(0);
            this.AutoScanToTest.Name = "AutoScanToTest";
            this.AutoScanToTest.Size = new System.Drawing.Size(75, 22);
            this.AutoScanToTest.TabIndex = 22;
            this.AutoScanToTest.Text = "&Auto Scan";
            this.AutoScanToTest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolTipHelper.SetToolTip(this.AutoScanToTest, "Сканирование путём соединения расширенного и зеркального поисков, что бы найти вс" +
        "е RS485 устройства в одной шине");
            this.AutoScanToTest.UseVisualStyleBackColor = true;
            // 
            // StatusRS485GridView
            // 
            this.StatusRS485GridView.AllowUserToAddRows = false;
            this.StatusRS485GridView.AllowUserToOrderColumns = true;
            this.StatusRS485GridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StatusRS485GridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle14;
            this.StatusRS485GridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusRS485GridView.BackgroundColor = System.Drawing.Color.White;
            this.StatusRS485GridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatusRS485GridView.CausesValidation = false;
            this.StatusRS485GridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.StatusRS485GridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.StatusRS485GridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.StatusRS485GridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.StatusRS485GridView.ColumnHeadersHeight = 18;
            this.StatusRS485GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.StatusRS485GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InterfaceStatusRM,
            this.SignStatusRM,
            this.TypeStatusRM,
            this.StatusStatusRM,
            this.TxStatusRM,
            this.RxStatusRM,
            this.ErrorStatusRM,
            this.PercentErrorStatusRM,
            this.DisconnectedStatusRM,
            this.BadReplyStatusRM,
            this.BadCrcStatusRM,
            this.RadioErrorStatusRM,
            this.RadioNearbyStatusRM,
            this.WorkTimeStatusRM,
            this.VerStatusRM});
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle31.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle31.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle31.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.StatusRS485GridView.DefaultCellStyle = dataGridViewCellStyle31;
            this.StatusRS485GridView.GridColor = System.Drawing.Color.White;
            this.StatusRS485GridView.Location = new System.Drawing.Point(2, 2);
            this.StatusRS485GridView.Margin = new System.Windows.Forms.Padding(1);
            this.StatusRS485GridView.Name = "StatusRS485GridView";
            this.StatusRS485GridView.ReadOnly = true;
            this.StatusRS485GridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle32.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle32.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle32.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.StatusRS485GridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle32;
            this.StatusRS485GridView.RowHeadersVisible = false;
            this.StatusRS485GridView.RowHeadersWidth = 25;
            this.StatusRS485GridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StatusRS485GridView.RowsDefaultCellStyle = dataGridViewCellStyle33;
            this.StatusRS485GridView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StatusRS485GridView.RowTemplate.ErrorText = "???";
            this.StatusRS485GridView.RowTemplate.Height = 12;
            this.StatusRS485GridView.RowTemplate.ReadOnly = true;
            this.StatusRS485GridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.StatusRS485GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.StatusRS485GridView.ShowCellToolTips = false;
            this.StatusRS485GridView.Size = new System.Drawing.Size(301, 209);
            this.StatusRS485GridView.TabIndex = 21;
            this.StatusRS485GridView.TabStop = false;
            this.StatusRS485GridView.VirtualMode = true;
            this.StatusRS485GridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.StatusGridView_RowsAdded);
            this.StatusRS485GridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.StatusGridView_RowsRemoved);
            // 
            // InterfaceStatusRM
            // 
            this.InterfaceStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.InterfaceStatusRM.DataPropertyName = "devInterface";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.InterfaceStatusRM.DefaultCellStyle = dataGridViewCellStyle16;
            this.InterfaceStatusRM.HeaderText = "Interface";
            this.InterfaceStatusRM.MaxInputLength = 5;
            this.InterfaceStatusRM.MinimumWidth = 50;
            this.InterfaceStatusRM.Name = "InterfaceStatusRM";
            this.InterfaceStatusRM.ReadOnly = true;
            this.InterfaceStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InterfaceStatusRM.Width = 67;
            // 
            // SignStatusRM
            // 
            this.SignStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.SignStatusRM.DataPropertyName = "devSign";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SignStatusRM.DefaultCellStyle = dataGridViewCellStyle17;
            this.SignStatusRM.FillWeight = 50F;
            this.SignStatusRM.HeaderText = "Sign";
            this.SignStatusRM.MaxInputLength = 5;
            this.SignStatusRM.MinimumWidth = 35;
            this.SignStatusRM.Name = "SignStatusRM";
            this.SignStatusRM.ReadOnly = true;
            this.SignStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SignStatusRM.Width = 37;
            // 
            // TypeStatusRM
            // 
            this.TypeStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.TypeStatusRM.DataPropertyName = "devType";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.TypeStatusRM.DefaultCellStyle = dataGridViewCellStyle18;
            this.TypeStatusRM.FillWeight = 45F;
            this.TypeStatusRM.HeaderText = "Type";
            this.TypeStatusRM.MaxInputLength = 5;
            this.TypeStatusRM.MinimumWidth = 35;
            this.TypeStatusRM.Name = "TypeStatusRM";
            this.TypeStatusRM.ReadOnly = true;
            this.TypeStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TypeStatusRM.Width = 37;
            // 
            // StatusStatusRM
            // 
            this.StatusStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.StatusStatusRM.DataPropertyName = "devStatus";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StatusStatusRM.DefaultCellStyle = dataGridViewCellStyle19;
            this.StatusStatusRM.FillWeight = 45F;
            this.StatusStatusRM.HeaderText = "Status";
            this.StatusStatusRM.MaxInputLength = 4;
            this.StatusStatusRM.MinimumWidth = 40;
            this.StatusStatusRM.Name = "StatusStatusRM";
            this.StatusStatusRM.ReadOnly = true;
            this.StatusStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StatusStatusRM.Width = 49;
            // 
            // TxStatusRM
            // 
            this.TxStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.TxStatusRM.DataPropertyName = "devTx";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.TxStatusRM.DefaultCellStyle = dataGridViewCellStyle20;
            this.TxStatusRM.FillWeight = 75F;
            this.TxStatusRM.HeaderText = "Tx";
            this.TxStatusRM.MaxInputLength = 7;
            this.TxStatusRM.MinimumWidth = 25;
            this.TxStatusRM.Name = "TxStatusRM";
            this.TxStatusRM.ReadOnly = true;
            this.TxStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TxStatusRM.Width = 25;
            // 
            // RxStatusRM
            // 
            this.RxStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.RxStatusRM.DataPropertyName = "devRx";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RxStatusRM.DefaultCellStyle = dataGridViewCellStyle21;
            this.RxStatusRM.FillWeight = 75F;
            this.RxStatusRM.HeaderText = "Rx";
            this.RxStatusRM.MaxInputLength = 7;
            this.RxStatusRM.MinimumWidth = 25;
            this.RxStatusRM.Name = "RxStatusRM";
            this.RxStatusRM.ReadOnly = true;
            this.RxStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RxStatusRM.Width = 25;
            // 
            // ErrorStatusRM
            // 
            this.ErrorStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ErrorStatusRM.DataPropertyName = "devErrors";
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ErrorStatusRM.DefaultCellStyle = dataGridViewCellStyle22;
            this.ErrorStatusRM.FillWeight = 75F;
            this.ErrorStatusRM.HeaderText = "Error";
            this.ErrorStatusRM.MaxInputLength = 7;
            this.ErrorStatusRM.MinimumWidth = 35;
            this.ErrorStatusRM.Name = "ErrorStatusRM";
            this.ErrorStatusRM.ReadOnly = true;
            this.ErrorStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ErrorStatusRM.Width = 43;
            // 
            // PercentErrorStatusRM
            // 
            this.PercentErrorStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.PercentErrorStatusRM.DataPropertyName = "devPercentErrors";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle23.Format = "N3";
            dataGridViewCellStyle23.NullValue = null;
            this.PercentErrorStatusRM.DefaultCellStyle = dataGridViewCellStyle23;
            this.PercentErrorStatusRM.FillWeight = 75F;
            this.PercentErrorStatusRM.HeaderText = "%Error";
            this.PercentErrorStatusRM.MaxInputLength = 7;
            this.PercentErrorStatusRM.MinimumWidth = 35;
            this.PercentErrorStatusRM.Name = "PercentErrorStatusRM";
            this.PercentErrorStatusRM.ReadOnly = true;
            this.PercentErrorStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PercentErrorStatusRM.Width = 49;
            // 
            // DisconnectedStatusRM
            // 
            this.DisconnectedStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DisconnectedStatusRM.DataPropertyName = "devNoReply";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DisconnectedStatusRM.DefaultCellStyle = dataGridViewCellStyle24;
            this.DisconnectedStatusRM.FillWeight = 75F;
            this.DisconnectedStatusRM.HeaderText = "No Reply";
            this.DisconnectedStatusRM.MaxInputLength = 7;
            this.DisconnectedStatusRM.MinimumWidth = 35;
            this.DisconnectedStatusRM.Name = "DisconnectedStatusRM";
            this.DisconnectedStatusRM.ReadOnly = true;
            this.DisconnectedStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DisconnectedStatusRM.Visible = false;
            this.DisconnectedStatusRM.Width = 61;
            // 
            // BadReplyStatusRM
            // 
            this.BadReplyStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.BadReplyStatusRM.DataPropertyName = "devBadReply";
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.BadReplyStatusRM.DefaultCellStyle = dataGridViewCellStyle25;
            this.BadReplyStatusRM.FillWeight = 75F;
            this.BadReplyStatusRM.HeaderText = "Bad Reply";
            this.BadReplyStatusRM.MaxInputLength = 7;
            this.BadReplyStatusRM.MinimumWidth = 35;
            this.BadReplyStatusRM.Name = "BadReplyStatusRM";
            this.BadReplyStatusRM.ReadOnly = true;
            this.BadReplyStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BadReplyStatusRM.Visible = false;
            this.BadReplyStatusRM.Width = 67;
            // 
            // BadCrcStatusRM
            // 
            this.BadCrcStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.BadCrcStatusRM.DataPropertyName = "devBadCRC";
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.BadCrcStatusRM.DefaultCellStyle = dataGridViewCellStyle26;
            this.BadCrcStatusRM.FillWeight = 75F;
            this.BadCrcStatusRM.HeaderText = "Bad CRC";
            this.BadCrcStatusRM.MaxInputLength = 7;
            this.BadCrcStatusRM.MinimumWidth = 35;
            this.BadCrcStatusRM.Name = "BadCrcStatusRM";
            this.BadCrcStatusRM.ReadOnly = true;
            this.BadCrcStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BadCrcStatusRM.Visible = false;
            this.BadCrcStatusRM.Width = 55;
            // 
            // RadioErrorStatusRM
            // 
            this.RadioErrorStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.RadioErrorStatusRM.DataPropertyName = "devBadRadio";
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RadioErrorStatusRM.DefaultCellStyle = dataGridViewCellStyle27;
            this.RadioErrorStatusRM.FillWeight = 75F;
            this.RadioErrorStatusRM.HeaderText = "Bad Radio";
            this.RadioErrorStatusRM.MaxInputLength = 7;
            this.RadioErrorStatusRM.MinimumWidth = 35;
            this.RadioErrorStatusRM.Name = "RadioErrorStatusRM";
            this.RadioErrorStatusRM.ReadOnly = true;
            this.RadioErrorStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RadioErrorStatusRM.Visible = false;
            this.RadioErrorStatusRM.Width = 67;
            // 
            // RadioNearbyStatusRM
            // 
            this.RadioNearbyStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.RadioNearbyStatusRM.DataPropertyName = "devNearbyDevs";
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RadioNearbyStatusRM.DefaultCellStyle = dataGridViewCellStyle28;
            this.RadioNearbyStatusRM.HeaderText = "Nearby";
            this.RadioNearbyStatusRM.MaxInputLength = 2;
            this.RadioNearbyStatusRM.Name = "RadioNearbyStatusRM";
            this.RadioNearbyStatusRM.ReadOnly = true;
            this.RadioNearbyStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RadioNearbyStatusRM.Visible = false;
            this.RadioNearbyStatusRM.Width = 49;
            // 
            // WorkTimeStatusRM
            // 
            this.WorkTimeStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.WorkTimeStatusRM.DataPropertyName = "devWorkTime";
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle29.NullValue = null;
            this.WorkTimeStatusRM.DefaultCellStyle = dataGridViewCellStyle29;
            this.WorkTimeStatusRM.HeaderText = "Work Time";
            this.WorkTimeStatusRM.MaxInputLength = 32;
            this.WorkTimeStatusRM.MinimumWidth = 70;
            this.WorkTimeStatusRM.Name = "WorkTimeStatusRM";
            this.WorkTimeStatusRM.ReadOnly = true;
            this.WorkTimeStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.WorkTimeStatusRM.Width = 70;
            // 
            // VerStatusRM
            // 
            this.VerStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.VerStatusRM.DataPropertyName = "devVer";
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.VerStatusRM.DefaultCellStyle = dataGridViewCellStyle30;
            this.VerStatusRM.FillWeight = 35F;
            this.VerStatusRM.HeaderText = "Ver";
            this.VerStatusRM.MaxInputLength = 4;
            this.VerStatusRM.MinimumWidth = 30;
            this.VerStatusRM.Name = "VerStatusRM";
            this.VerStatusRM.ReadOnly = true;
            this.VerStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.VerStatusRM.Width = 30;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.White;
            this.statusStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.MessageStatus,
            this.ReplyStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 322);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(512, 25);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 17;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(31, 25);
            this.toolStripStatusLabel1.Text = "Info:";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MessageStatus
            // 
            this.MessageStatus.AutoSize = false;
            this.MessageStatus.Margin = new System.Windows.Forms.Padding(0);
            this.MessageStatus.Name = "MessageStatus";
            this.MessageStatus.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.MessageStatus.Size = new System.Drawing.Size(413, 25);
            this.MessageStatus.Spring = true;
            this.MessageStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ReplyStatus
            // 
            this.ReplyStatus.AutoSize = false;
            this.ReplyStatus.Margin = new System.Windows.Forms.Padding(0);
            this.ReplyStatus.Name = "ReplyStatus";
            this.ReplyStatus.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.ReplyStatus.Size = new System.Drawing.Size(44, 25);
            this.ReplyStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SerUdpPages
            // 
            this.SerUdpPages.Controls.Add(this.SerialPage);
            this.SerUdpPages.Controls.Add(this.UdpPage);
            this.SerUdpPages.Location = new System.Drawing.Point(9, 27);
            this.SerUdpPages.Margin = new System.Windows.Forms.Padding(0);
            this.SerUdpPages.Multiline = true;
            this.SerUdpPages.Name = "SerUdpPages";
            this.SerUdpPages.SelectedIndex = 0;
            this.SerUdpPages.Size = new System.Drawing.Size(165, 148);
            this.SerUdpPages.TabIndex = 19;
            // 
            // SerialPage
            // 
            this.SerialPage.BackColor = System.Drawing.Color.White;
            this.SerialPage.Controls.Add(this.comPort);
            this.SerialPage.Controls.Add(this.RefreshSerial);
            this.SerialPage.Controls.Add(this.BaudRate);
            this.SerialPage.Controls.Add(this.OpenCom);
            this.SerialPage.Controls.Add(this.label10);
            this.SerialPage.Controls.Add(this.label11);
            this.SerialPage.Location = new System.Drawing.Point(4, 22);
            this.SerialPage.Name = "SerialPage";
            this.SerialPage.Padding = new System.Windows.Forms.Padding(3);
            this.SerialPage.Size = new System.Drawing.Size(157, 122);
            this.SerialPage.TabIndex = 0;
            this.SerialPage.Text = "Serial";
            // 
            // comPort
            // 
            this.comPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comPort.FormattingEnabled = true;
            this.comPort.Location = new System.Drawing.Point(19, 26);
            this.comPort.Name = "comPort";
            this.comPort.Size = new System.Drawing.Size(121, 21);
            this.comPort.Sorted = true;
            this.comPort.TabIndex = 32;
            // 
            // RefreshSerial
            // 
            this.RefreshSerial.Location = new System.Drawing.Point(76, 7);
            this.RefreshSerial.Name = "RefreshSerial";
            this.RefreshSerial.Size = new System.Drawing.Size(65, 21);
            this.RefreshSerial.TabIndex = 43;
            this.RefreshSerial.Text = "Refresh";
            this.RefreshSerial.UseVisualStyleBackColor = true;
            // 
            // BaudRate
            // 
            this.BaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BaudRate.FormattingEnabled = true;
            this.BaudRate.Items.AddRange(new object[] {
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "1000000"});
            this.BaudRate.Location = new System.Drawing.Point(19, 66);
            this.BaudRate.MaxLength = 7;
            this.BaudRate.Name = "BaudRate";
            this.BaudRate.Size = new System.Drawing.Size(121, 21);
            this.BaudRate.TabIndex = 33;
            // 
            // OpenCom
            // 
            this.OpenCom.BackColor = System.Drawing.Color.Transparent;
            this.OpenCom.Location = new System.Drawing.Point(19, 93);
            this.OpenCom.Name = "OpenCom";
            this.OpenCom.Size = new System.Drawing.Size(121, 23);
            this.OpenCom.TabIndex = 42;
            this.OpenCom.Text = "Open";
            this.OpenCom.UseVisualStyleBackColor = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "PortName";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 37;
            this.label11.Text = "BaudRate";
            // 
            // UdpPage
            // 
            this.UdpPage.BackColor = System.Drawing.Color.White;
            this.UdpPage.Controls.Add(this.PingButton);
            this.UdpPage.Controls.Add(this.numericPort);
            this.UdpPage.Controls.Add(this.Connect);
            this.UdpPage.Controls.Add(this.label14);
            this.UdpPage.Controls.Add(this.label13);
            this.UdpPage.Controls.Add(this.IPaddressBox);
            this.UdpPage.Controls.Add(this.label16);
            this.UdpPage.Location = new System.Drawing.Point(4, 22);
            this.UdpPage.Name = "UdpPage";
            this.UdpPage.Padding = new System.Windows.Forms.Padding(3);
            this.UdpPage.Size = new System.Drawing.Size(157, 122);
            this.UdpPage.TabIndex = 1;
            this.UdpPage.Text = "UDP";
            // 
            // PingButton
            // 
            this.PingButton.BackColor = System.Drawing.Color.Red;
            this.PingButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PingButton.Enabled = false;
            this.PingButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.PingButton.Location = new System.Drawing.Point(102, 70);
            this.PingButton.Name = "PingButton";
            this.PingButton.Size = new System.Drawing.Size(51, 21);
            this.PingButton.TabIndex = 32;
            this.PingButton.Text = "Ping";
            this.PingButton.UseVisualStyleBackColor = false;
            // 
            // numericPort
            // 
            this.numericPort.Location = new System.Drawing.Point(102, 47);
            this.numericPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericPort.Name = "numericPort";
            this.numericPort.Size = new System.Drawing.Size(51, 20);
            this.numericPort.TabIndex = 29;
            // 
            // Connect
            // 
            this.Connect.Enabled = false;
            this.Connect.Location = new System.Drawing.Point(5, 69);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(90, 23);
            this.Connect.TabIndex = 29;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(99, 31);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(26, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Port";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 31);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "IP address";
            // 
            // IPaddressBox
            // 
            this.IPaddressBox.Location = new System.Drawing.Point(6, 47);
            this.IPaddressBox.MaxLength = 15;
            this.IPaddressBox.Name = "IPaddressBox";
            this.IPaddressBox.Size = new System.Drawing.Size(88, 20);
            this.IPaddressBox.TabIndex = 0;
            this.IPaddressBox.TextChanged += new System.EventHandler(this.IPaddressBox_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(94, 49);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(10, 13);
            this.label16.TabIndex = 31;
            this.label16.Text = ":";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStrip,
            this.toolStripSeparator2,
            this.dataBits,
            this.dataBitsInfo,
            this.toolStripSeparator3,
            this.Parity,
            this.ParityInfo,
            this.toolStripSeparator5,
            this.stopBits,
            this.StopBitsInfo,
            this.toolStripSeparator1,
            this.AboutButton,
            this.UpdateButton,
            this.DinoRunningProcessOk});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(512, 25);
            this.toolStrip1.TabIndex = 20;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // settingsToolStrip
            // 
            this.settingsToolStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToRegToolStripMenuItem,
            this.loadFromPCToolStripMenuItem,
            this.deleteSaveFromPCToolStripMenuItem,
            this.toolStripSeparator4,
            this.toolStripMenuItem1,
            this.toolStripSeparator7,
            this.clearSettingsToolStrip});
            this.settingsToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsToolStrip.Margin = new System.Windows.Forms.Padding(0);
            this.settingsToolStrip.Name = "settingsToolStrip";
            this.settingsToolStrip.Size = new System.Drawing.Size(51, 25);
            this.settingsToolStrip.Text = "Menu";
            // 
            // saveToRegToolStripMenuItem
            // 
            this.saveToRegToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToRegToolStripMenuItem.Image")));
            this.saveToRegToolStripMenuItem.Name = "saveToRegToolStripMenuItem";
            this.saveToRegToolStripMenuItem.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.saveToRegToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.saveToRegToolStripMenuItem.Text = "Save in PC";
            this.saveToRegToolStripMenuItem.ToolTipText = "Выполняет сохранение настройки пользователя в реестр";
            this.saveToRegToolStripMenuItem.Click += new System.EventHandler(this.saveToRegToolStripMenuItem_Click);
            // 
            // loadFromPCToolStripMenuItem
            // 
            this.loadFromPCToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("loadFromPCToolStripMenuItem.Image")));
            this.loadFromPCToolStripMenuItem.Name = "loadFromPCToolStripMenuItem";
            this.loadFromPCToolStripMenuItem.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.loadFromPCToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.loadFromPCToolStripMenuItem.Text = "Load from PC";
            this.loadFromPCToolStripMenuItem.ToolTipText = "Выполняет загрузку настройки пользователя из реестра";
            // 
            // deleteSaveFromPCToolStripMenuItem
            // 
            this.deleteSaveFromPCToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteSaveFromPCToolStripMenuItem.Image")));
            this.deleteSaveFromPCToolStripMenuItem.Name = "deleteSaveFromPCToolStripMenuItem";
            this.deleteSaveFromPCToolStripMenuItem.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.deleteSaveFromPCToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.deleteSaveFromPCToolStripMenuItem.Text = "Delete from PC";
            this.deleteSaveFromPCToolStripMenuItem.ToolTipText = "Выполняет удаление настройки пользователя из реестра\r\n";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(151, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transparentToolStrip,
            this.messagesToolStrip,
            this.windowPinToolStrip,
            this.extendedButtonsToolStrip});
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.toolStripMenuItem1.Text = "Settings";
            // 
            // transparentToolStrip
            // 
            this.transparentToolStrip.BackColor = System.Drawing.Color.White;
            this.transparentToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.transparentToolStrip.Checked = true;
            this.transparentToolStrip.CheckOnClick = true;
            this.transparentToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.transparentToolStrip.Name = "transparentToolStrip";
            this.transparentToolStrip.Size = new System.Drawing.Size(167, 22);
            this.transparentToolStrip.Text = "Transparent";
            this.transparentToolStrip.ToolTipText = "Прозрачность основного окна";
            // 
            // messagesToolStrip
            // 
            this.messagesToolStrip.BackColor = System.Drawing.Color.White;
            this.messagesToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.messagesToolStrip.Checked = true;
            this.messagesToolStrip.CheckOnClick = true;
            this.messagesToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.messagesToolStrip.Name = "messagesToolStrip";
            this.messagesToolStrip.Size = new System.Drawing.Size(167, 22);
            this.messagesToolStrip.Text = "Messages";
            this.messagesToolStrip.ToolTipText = "Всплывающие сообщения";
            // 
            // windowPinToolStrip
            // 
            this.windowPinToolStrip.BackColor = System.Drawing.Color.White;
            this.windowPinToolStrip.CheckOnClick = true;
            this.windowPinToolStrip.Name = "windowPinToolStrip";
            this.windowPinToolStrip.Size = new System.Drawing.Size(167, 22);
            this.windowPinToolStrip.Text = "Window pin";
            this.windowPinToolStrip.ToolTipText = "Обычное состояние окна";
            // 
            // extendedButtonsToolStrip
            // 
            this.extendedButtonsToolStrip.CheckOnClick = true;
            this.extendedButtonsToolStrip.Name = "extendedButtonsToolStrip";
            this.extendedButtonsToolStrip.Size = new System.Drawing.Size(167, 22);
            this.extendedButtonsToolStrip.Text = "Extended buttons";
            this.extendedButtonsToolStrip.ToolTipText = "Дополнительные кнопки";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(151, 6);
            // 
            // clearSettingsToolStrip
            // 
            this.clearSettingsToolStrip.Image = ((System.Drawing.Image)(resources.GetObject("clearSettingsToolStrip.Image")));
            this.clearSettingsToolStrip.Name = "clearSettingsToolStrip";
            this.clearSettingsToolStrip.Size = new System.Drawing.Size(154, 22);
            this.clearSettingsToolStrip.Text = "Clear settings";
            this.clearSettingsToolStrip.ToolTipText = "Выполняет сброс всех настроек, которые были сохранены пользователем ранее";
            this.clearSettingsToolStrip.Click += new System.EventHandler(this.clearSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // dataBits
            // 
            this.dataBits.AutoToolTip = false;
            this.dataBits.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.dataBits.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataBits7,
            this.dataBits8});
            this.dataBits.Image = ((System.Drawing.Image)(resources.GetObject("dataBits.Image")));
            this.dataBits.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dataBits.Margin = new System.Windows.Forms.Padding(0);
            this.dataBits.Name = "dataBits";
            this.dataBits.ShowDropDownArrow = false;
            this.dataBits.Size = new System.Drawing.Size(57, 25);
            this.dataBits.Text = "Data Bits";
            // 
            // dataBits7
            // 
            this.dataBits7.Name = "dataBits7";
            this.dataBits7.Size = new System.Drawing.Size(80, 22);
            this.dataBits7.Text = "7";
            // 
            // dataBits8
            // 
            this.dataBits8.Name = "dataBits8";
            this.dataBits8.Size = new System.Drawing.Size(80, 22);
            this.dataBits8.Text = "8";
            // 
            // dataBitsInfo
            // 
            this.dataBitsInfo.Enabled = false;
            this.dataBitsInfo.Name = "dataBitsInfo";
            this.dataBitsInfo.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // Parity
            // 
            this.Parity.AutoToolTip = false;
            this.Parity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Parity.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ParityNone,
            this.ParityOdd,
            this.ParityEven,
            this.ParityMark,
            this.ParitySpace});
            this.Parity.Image = ((System.Drawing.Image)(resources.GetObject("Parity.Image")));
            this.Parity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Parity.Margin = new System.Windows.Forms.Padding(0);
            this.Parity.Name = "Parity";
            this.Parity.ShowDropDownArrow = false;
            this.Parity.Size = new System.Drawing.Size(41, 25);
            this.Parity.Text = "Parity";
            // 
            // ParityNone
            // 
            this.ParityNone.Name = "ParityNone";
            this.ParityNone.Size = new System.Drawing.Size(105, 22);
            this.ParityNone.Text = "None";
            // 
            // ParityOdd
            // 
            this.ParityOdd.Name = "ParityOdd";
            this.ParityOdd.Size = new System.Drawing.Size(105, 22);
            this.ParityOdd.Text = "Odd";
            // 
            // ParityEven
            // 
            this.ParityEven.Name = "ParityEven";
            this.ParityEven.Size = new System.Drawing.Size(105, 22);
            this.ParityEven.Text = "Even";
            // 
            // ParityMark
            // 
            this.ParityMark.Name = "ParityMark";
            this.ParityMark.Size = new System.Drawing.Size(105, 22);
            this.ParityMark.Text = "Mark";
            // 
            // ParitySpace
            // 
            this.ParitySpace.Name = "ParitySpace";
            this.ParitySpace.Size = new System.Drawing.Size(105, 22);
            this.ParitySpace.Text = "Space";
            // 
            // ParityInfo
            // 
            this.ParityInfo.Enabled = false;
            this.ParityInfo.Name = "ParityInfo";
            this.ParityInfo.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // stopBits
            // 
            this.stopBits.AutoToolTip = false;
            this.stopBits.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stopBits.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stopBits1,
            this.stopBits2});
            this.stopBits.Image = ((System.Drawing.Image)(resources.GetObject("stopBits.Image")));
            this.stopBits.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopBits.Margin = new System.Windows.Forms.Padding(0);
            this.stopBits.Name = "stopBits";
            this.stopBits.ShowDropDownArrow = false;
            this.stopBits.Size = new System.Drawing.Size(57, 25);
            this.stopBits.Text = "Stop Bits";
            // 
            // stopBits1
            // 
            this.stopBits1.Name = "stopBits1";
            this.stopBits1.Size = new System.Drawing.Size(80, 22);
            this.stopBits1.Text = "1";
            // 
            // stopBits2
            // 
            this.stopBits2.Name = "stopBits2";
            this.stopBits2.Size = new System.Drawing.Size(80, 22);
            this.stopBits2.Text = "2";
            // 
            // StopBitsInfo
            // 
            this.StopBitsInfo.Enabled = false;
            this.StopBitsInfo.Name = "StopBitsInfo";
            this.StopBitsInfo.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // AboutButton
            // 
            this.AboutButton.AutoSize = false;
            this.AboutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AboutButton.Image = ((System.Drawing.Image)(resources.GetObject("AboutButton.Image")));
            this.AboutButton.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.AboutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AboutButton.Margin = new System.Windows.Forms.Padding(0);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(25, 25);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.UpdateButton.AutoSize = false;
            this.UpdateButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UpdateButton.Image = ((System.Drawing.Image)(resources.GetObject("UpdateButton.Image")));
            this.UpdateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UpdateButton.Margin = new System.Windows.Forms.Padding(0);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(25, 25);
            this.UpdateButton.Text = "Update";
            this.UpdateButton.Visible = false;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // DinoRunningProcessOk
            // 
            this.DinoRunningProcessOk.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.DinoRunningProcessOk.AutoSize = false;
            this.DinoRunningProcessOk.BackColor = System.Drawing.Color.White;
            this.DinoRunningProcessOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DinoRunningProcessOk.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DinoRunningProcessOk.Enabled = false;
            this.DinoRunningProcessOk.Image = ((System.Drawing.Image)(resources.GetObject("DinoRunningProcessOk.Image")));
            this.DinoRunningProcessOk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DinoRunningProcessOk.Margin = new System.Windows.Forms.Padding(0);
            this.DinoRunningProcessOk.Name = "DinoRunningProcessOk";
            this.DinoRunningProcessOk.Size = new System.Drawing.Size(25, 25);
            this.DinoRunningProcessOk.ToolTipText = "Dino goes ROAR\r\nAnd he can stop any task!";
            this.DinoRunningProcessOk.Visible = false;
            // 
            // SignaturePanel
            // 
            this.SignaturePanel.BackColor = System.Drawing.Color.White;
            this.SignaturePanel.Controls.Add(this.ThroughSignID);
            this.SignaturePanel.Controls.Add(this.label1);
            this.SignaturePanel.Controls.Add(this.TargetSignID);
            this.SignaturePanel.Controls.Add(this.NeedThrough);
            this.SignaturePanel.Location = new System.Drawing.Point(9, 177);
            this.SignaturePanel.Name = "SignaturePanel";
            this.SignaturePanel.Size = new System.Drawing.Size(163, 46);
            this.SignaturePanel.TabIndex = 21;
            // 
            // ThroughSignID
            // 
            this.ThroughSignID.Enabled = false;
            this.ThroughSignID.Location = new System.Drawing.Point(109, 23);
            this.ThroughSignID.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ThroughSignID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ThroughSignID.Name = "ThroughSignID";
            this.ThroughSignID.Size = new System.Drawing.Size(52, 20);
            this.ThroughSignID.TabIndex = 2;
            this.ThroughSignID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ThroughSignID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // NeedThrough
            // 
            this.NeedThrough.AutoSize = true;
            this.NeedThrough.BackColor = System.Drawing.Color.Transparent;
            this.NeedThrough.Location = new System.Drawing.Point(3, 25);
            this.NeedThrough.Margin = new System.Windows.Forms.Padding(0);
            this.NeedThrough.Name = "NeedThrough";
            this.NeedThrough.Size = new System.Drawing.Size(107, 17);
            this.NeedThrough.TabIndex = 4;
            this.NeedThrough.Text = "Through RM485:";
            this.NeedThrough.UseVisualStyleBackColor = false;
            // 
            // ExtraButtonsGroup
            // 
            this.ExtraButtonsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ExtraButtonsGroup.Controls.Add(this.label15);
            this.ExtraButtonsGroup.Controls.Add(this.AutoExtraButtonsTimeout);
            this.ExtraButtonsGroup.Controls.Add(this.AutoExtraButtons);
            this.ExtraButtonsGroup.Controls.Add(this.ButtonsPanel);
            this.ExtraButtonsGroup.Enabled = false;
            this.ExtraButtonsGroup.Location = new System.Drawing.Point(9, 223);
            this.ExtraButtonsGroup.Margin = new System.Windows.Forms.Padding(0);
            this.ExtraButtonsGroup.Name = "ExtraButtonsGroup";
            this.ExtraButtonsGroup.Size = new System.Drawing.Size(163, 96);
            this.ExtraButtonsGroup.TabIndex = 3;
            this.ExtraButtonsGroup.TabStop = false;
            this.ExtraButtonsGroup.Text = "Extra buttons";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(121, 76);
            this.label15.Margin = new System.Windows.Forms.Padding(0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(20, 13);
            this.label15.TabIndex = 22;
            this.label15.Text = "ms";
            // 
            // AutoExtraButtonsTimeout
            // 
            this.AutoExtraButtonsTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AutoExtraButtonsTimeout.Location = new System.Drawing.Point(70, 73);
            this.AutoExtraButtonsTimeout.Margin = new System.Windows.Forms.Padding(0);
            this.AutoExtraButtonsTimeout.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.AutoExtraButtonsTimeout.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.AutoExtraButtonsTimeout.Name = "AutoExtraButtonsTimeout";
            this.AutoExtraButtonsTimeout.Size = new System.Drawing.Size(51, 20);
            this.AutoExtraButtonsTimeout.TabIndex = 21;
            this.AutoExtraButtonsTimeout.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // AutoExtraButtons
            // 
            this.AutoExtraButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AutoExtraButtons.AutoSize = true;
            this.AutoExtraButtons.Location = new System.Drawing.Point(22, 76);
            this.AutoExtraButtons.Margin = new System.Windows.Forms.Padding(0);
            this.AutoExtraButtons.Name = "AutoExtraButtons";
            this.AutoExtraButtons.Size = new System.Drawing.Size(48, 17);
            this.AutoExtraButtons.TabIndex = 2;
            this.AutoExtraButtons.Text = "Auto";
            this.ToolTipHelper.SetToolTip(this.AutoExtraButtons, resources.GetString("AutoExtraButtons.ToolTip"));
            this.AutoExtraButtons.UseVisualStyleBackColor = true;
            // 
            // ButtonsPanel
            // 
            this.ButtonsPanel.Controls.Add(this.SetBootloaderStopButton);
            this.ButtonsPanel.Controls.Add(this.ResetButton);
            this.ButtonsPanel.Controls.Add(this.SetBootloaderStartButton);
            this.ButtonsPanel.Controls.Add(this.SetOnlineFreqNumeric);
            this.ButtonsPanel.Controls.Add(this.label18);
            this.ButtonsPanel.Controls.Add(this.SetOnlineButton);
            this.ButtonsPanel.Location = new System.Drawing.Point(1, 16);
            this.ButtonsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonsPanel.Name = "ButtonsPanel";
            this.ButtonsPanel.Size = new System.Drawing.Size(159, 56);
            this.ButtonsPanel.TabIndex = 20;
            // 
            // SetBootloaderStopButton
            // 
            this.SetBootloaderStopButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SetBootloaderStopButton.Image = ((System.Drawing.Image)(resources.GetObject("SetBootloaderStopButton.Image")));
            this.SetBootloaderStopButton.Location = new System.Drawing.Point(43, 22);
            this.SetBootloaderStopButton.Margin = new System.Windows.Forms.Padding(0);
            this.SetBootloaderStopButton.Name = "SetBootloaderStopButton";
            this.SetBootloaderStopButton.Size = new System.Drawing.Size(20, 20);
            this.SetBootloaderStopButton.TabIndex = 3;
            this.ToolTipHelper.SetToolTip(this.SetBootloaderStopButton, "Отправляет команду для остановки Bootloader");
            this.SetBootloaderStopButton.UseVisualStyleBackColor = true;
            this.SetBootloaderStopButton.Visible = false;
            // 
            // ResetButton
            // 
            this.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ResetButton.Image = ((System.Drawing.Image)(resources.GetObject("ResetButton.Image")));
            this.ResetButton.Location = new System.Drawing.Point(1, 22);
            this.ResetButton.Margin = new System.Windows.Forms.Padding(0);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(20, 20);
            this.ResetButton.TabIndex = 1;
            this.ToolTipHelper.SetToolTip(this.ResetButton, "Отправляет команду для перезагрузки устройства");
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Visible = false;
            // 
            // SetBootloaderStartButton
            // 
            this.SetBootloaderStartButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SetBootloaderStartButton.Image = ((System.Drawing.Image)(resources.GetObject("SetBootloaderStartButton.Image")));
            this.SetBootloaderStartButton.Location = new System.Drawing.Point(22, 22);
            this.SetBootloaderStartButton.Margin = new System.Windows.Forms.Padding(0);
            this.SetBootloaderStartButton.Name = "SetBootloaderStartButton";
            this.SetBootloaderStartButton.Size = new System.Drawing.Size(20, 20);
            this.SetBootloaderStartButton.TabIndex = 2;
            this.ToolTipHelper.SetToolTip(this.SetBootloaderStartButton, "Отправляет команду для перехода в Bootloader");
            this.SetBootloaderStartButton.UseVisualStyleBackColor = true;
            this.SetBootloaderStartButton.Visible = false;
            // 
            // SetOnlineFreqNumeric
            // 
            this.SetOnlineFreqNumeric.Location = new System.Drawing.Point(22, 1);
            this.SetOnlineFreqNumeric.Margin = new System.Windows.Forms.Padding(0);
            this.SetOnlineFreqNumeric.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.SetOnlineFreqNumeric.Name = "SetOnlineFreqNumeric";
            this.SetOnlineFreqNumeric.Size = new System.Drawing.Size(41, 20);
            this.SetOnlineFreqNumeric.TabIndex = 4;
            this.ToolTipHelper.SetToolTip(this.SetOnlineFreqNumeric, "Канал отправки для входа устройства в Online режим");
            this.SetOnlineFreqNumeric.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(63, 5);
            this.label18.Margin = new System.Windows.Forms.Padding(0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(25, 13);
            this.label18.TabIndex = 5;
            this.label18.Text = "freq";
            // 
            // SetOnlineButton
            // 
            this.SetOnlineButton.BackColor = System.Drawing.Color.White;
            this.SetOnlineButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SetOnlineButton.Image = ((System.Drawing.Image)(resources.GetObject("SetOnlineButton.Image")));
            this.SetOnlineButton.Location = new System.Drawing.Point(1, 1);
            this.SetOnlineButton.Margin = new System.Windows.Forms.Padding(0);
            this.SetOnlineButton.Name = "SetOnlineButton";
            this.SetOnlineButton.Size = new System.Drawing.Size(20, 20);
            this.SetOnlineButton.TabIndex = 0;
            this.ToolTipHelper.SetToolTip(this.SetOnlineButton, "Отправляет команду для входа устройства в Online режим");
            this.SetOnlineButton.UseVisualStyleBackColor = false;
            // 
            // ToolTipHelper
            // 
            this.ToolTipHelper.BackColor = System.Drawing.Color.White;
            this.ToolTipHelper.ForeColor = System.Drawing.Color.Black;
            this.ToolTipHelper.UseAnimation = false;
            // 
            // ErrorMessage
            // 
            this.ErrorMessage.BlinkRate = 500;
            this.ErrorMessage.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.ErrorMessage.ContainerControl = this;
            // 
            // NotifyMessage
            // 
            this.NotifyMessage.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.NotifyMessage.ContextMenuStrip = this.notifyMessageStrip;
            this.NotifyMessage.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyMessage.Icon")));
            this.NotifyMessage.Text = "RM Debugger";
            this.NotifyMessage.Visible = true;
            this.NotifyMessage.Click += new System.EventHandler(this.NotifyMessage_Click);
            // 
            // notifyMessageStrip
            // 
            this.notifyMessageStrip.BackColor = System.Drawing.Color.White;
            this.notifyMessageStrip.DropShadowEnabled = false;
            this.notifyMessageStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.notifyMessageStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenDebugFromToolStrip,
            this.toolStripSeparator6,
            this.AboutFromToolStrip,
            this.CloseFromToolStrip});
            this.notifyMessageStrip.Name = "notifyMessageStrip";
            this.notifyMessageStrip.Size = new System.Drawing.Size(190, 88);
            // 
            // OpenDebugFromToolStrip
            // 
            this.OpenDebugFromToolStrip.Image = ((System.Drawing.Image)(resources.GetObject("OpenDebugFromToolStrip.Image")));
            this.OpenDebugFromToolStrip.Name = "OpenDebugFromToolStrip";
            this.OpenDebugFromToolStrip.Size = new System.Drawing.Size(189, 26);
            this.OpenDebugFromToolStrip.Text = "Open debug window";
            this.OpenDebugFromToolStrip.ToolTipText = "Открыть окно отладки";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.ForeColor = System.Drawing.Color.Black;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(186, 6);
            // 
            // AboutFromToolStrip
            // 
            this.AboutFromToolStrip.Image = ((System.Drawing.Image)(resources.GetObject("AboutFromToolStrip.Image")));
            this.AboutFromToolStrip.Name = "AboutFromToolStrip";
            this.AboutFromToolStrip.Size = new System.Drawing.Size(189, 26);
            this.AboutFromToolStrip.Text = "About";
            // 
            // CloseFromToolStrip
            // 
            this.CloseFromToolStrip.Name = "CloseFromToolStrip";
            this.CloseFromToolStrip.Size = new System.Drawing.Size(189, 26);
            this.CloseFromToolStrip.Text = "Close";
            // 
            // WorkTestTimer
            // 
            this.WorkTestTimer.Interval = 1000;
            this.WorkTestTimer.Tick += new System.EventHandler(this.WorkTestTimer_Tick);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "RS485Log";
            this.saveFileDialog.Filter = "Log files (*.log)|*.log|All files (*.*)|*.*";
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 175);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(150, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(150, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightToolStripPanel.Location = new System.Drawing.Point(283, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(25, 172);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 175);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(283, 172);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "devType";
            this.dataGridViewTextBoxColumn1.FillWeight = 60F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Type";
            this.dataGridViewTextBoxColumn1.MaxInputLength = 6;
            this.dataGridViewTextBoxColumn1.MinimumWidth = 60;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // FieldsColumn
            // 
            this.FieldsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.FieldsColumn.DataPropertyName = "fieldName";
            this.FieldsColumn.FillWeight = 200F;
            this.FieldsColumn.Frozen = true;
            this.FieldsColumn.HeaderText = "Field";
            this.FieldsColumn.MaxInputLength = 16;
            this.FieldsColumn.Name = "FieldsColumn";
            this.FieldsColumn.ReadOnly = true;
            this.FieldsColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FieldsColumn.Width = 35;
            // 
            // EnabledColumn
            // 
            this.EnabledColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.EnabledColumn.DataPropertyName = "fieldActive";
            this.EnabledColumn.FillWeight = 20F;
            this.EnabledColumn.Frozen = true;
            this.EnabledColumn.HeaderText = "";
            this.EnabledColumn.MinimumWidth = 20;
            this.EnabledColumn.Name = "EnabledColumn";
            this.EnabledColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EnabledColumn.Width = 20;
            // 
            // LoadFieldColumn
            // 
            this.LoadFieldColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.LoadFieldColumn.DataPropertyName = "loadValue";
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LoadFieldColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.LoadFieldColumn.FillWeight = 200F;
            this.LoadFieldColumn.HeaderText = "Load Value";
            this.LoadFieldColumn.MaxInputLength = 16;
            this.LoadFieldColumn.MinimumWidth = 80;
            this.LoadFieldColumn.Name = "LoadFieldColumn";
            this.LoadFieldColumn.ReadOnly = true;
            this.LoadFieldColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LoadFieldColumn.Width = 80;
            // 
            // UploadFieldColumn
            // 
            this.UploadFieldColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.UploadFieldColumn.DataPropertyName = "uploadValue";
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.UploadFieldColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.UploadFieldColumn.FillWeight = 200F;
            this.UploadFieldColumn.HeaderText = "Upload Value";
            this.UploadFieldColumn.MaxInputLength = 16;
            this.UploadFieldColumn.MinimumWidth = 80;
            this.UploadFieldColumn.Name = "UploadFieldColumn";
            this.UploadFieldColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UploadFieldColumn.Width = 80;
            // 
            // MainDebugger
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(512, 347);
            this.Controls.Add(this.ExtraButtonsGroup);
            this.Controls.Add(this.SignaturePanel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.SerUdpPages);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.RMData);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(528, 386);
            this.Name = "MainDebugger";
            this.Opacity = 0.95D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RM Debugger";
            ((System.ComponentModel.ISupportInitialize)(this.TargetSignID)).EndInit();
            this.RMData.ResumeLayout(false);
            this.SearchPage.ResumeLayout(false);
            this.SearchExtraGroup.ResumeLayout(false);
            this.SearchFilterMenuStrip.ResumeLayout(false);
            this.SearchFindSignatureColorMenuStrip.ResumeLayout(false);
            this.SearchModesPanel.ResumeLayout(false);
            this.SearchModesPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SearchGrid)).EndInit();
            this.HexUpdatePage.ResumeLayout(false);
            this.HexUpdatePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HexTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HexPageSize)).EndInit();
            this.ConfigPage.ResumeLayout(false);
            this.ConfigPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RMLRRepeatCount)).EndInit();
            this.RMLRMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ConfigDataGrid)).EndInit();
            this.ConfigClearMenuStrip.ResumeLayout(false);
            this.InfoPage.ResumeLayout(false);
            this.InfoPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericInfoSeconds)).EndInit();
            this.infoMenuStrip.ResumeLayout(false);
            this.TestPage.ResumeLayout(false);
            this.TestPages.ResumeLayout(false);
            this.RS485Page.ResumeLayout(false);
            this.extendedMenuPanel.ResumeLayout(false);
            this.RS485SortMenuStrip.ResumeLayout(false);
            this.timerPanelTest.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericHoursTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSecondsTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinutesTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDaysTest)).EndInit();
            this.settingsGroupBox.ResumeLayout(false);
            this.settingsGroupBox.PerformLayout();
            this.scanGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.maxSigToScan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minSigToScan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusRS485GridView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.SerUdpPages.ResumeLayout(false);
            this.SerialPage.ResumeLayout(false);
            this.SerialPage.PerformLayout();
            this.UdpPage.ResumeLayout(false);
            this.UdpPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPort)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.SignaturePanel.ResumeLayout(false);
            this.SignaturePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThroughSignID)).EndInit();
            this.ExtraButtonsGroup.ResumeLayout(false);
            this.ExtraButtonsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AutoExtraButtonsTimeout)).EndInit();
            this.ButtonsPanel.ResumeLayout(false);
            this.ButtonsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SetOnlineFreqNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMessage)).EndInit();
            this.notifyMessageStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.IO.Ports.SerialPort mainPort;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel MessageStatus;
        private System.Windows.Forms.Button HexUploadButton;
        private System.Windows.Forms.Label BytesEnd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label BytesStart;
        private System.Windows.Forms.ProgressBar UpdateBar;
        private System.Windows.Forms.TabPage SerialPage;
        private System.Windows.Forms.Button RefreshSerial;
        private System.Windows.Forms.Button OpenCom;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabPage UdpPage;
        private System.Windows.Forms.Button PingButton;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton dataBits;
        private System.Windows.Forms.ToolStripMenuItem dataBits7;
        private System.Windows.Forms.ToolStripMenuItem dataBits8;
        private System.Windows.Forms.ToolStripLabel dataBitsInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton Parity;
        private System.Windows.Forms.ToolStripMenuItem ParityNone;
        private System.Windows.Forms.ToolStripMenuItem ParityOdd;
        private System.Windows.Forms.ToolStripMenuItem ParityEven;
        private System.Windows.Forms.ToolStripMenuItem ParityMark;
        private System.Windows.Forms.ToolStripMenuItem ParitySpace;
        private System.Windows.Forms.ToolStripLabel ParityInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripDropDownButton stopBits;
        private System.Windows.Forms.ToolStripMenuItem stopBits1;
        private System.Windows.Forms.ToolStripMenuItem stopBits2;
        private System.Windows.Forms.ToolStripLabel StopBitsInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton AboutButton;
        private System.Windows.Forms.Panel SignaturePanel;
        private System.Windows.Forms.ToolTip ToolTipHelper;
        private System.Windows.Forms.Button HexPathButton;
        public System.Windows.Forms.NumericUpDown TargetSignID;
        public System.Windows.Forms.NumericUpDown ThroughSignID;
        public System.Windows.Forms.ComboBox HexPathBox;
        public System.Windows.Forms.TabControl RMData;
        public System.Windows.Forms.CheckBox NeedThrough;
        public System.Windows.Forms.TabControl SerUdpPages;
        public System.Windows.Forms.TabPage HexUpdatePage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton UpdateButton;
        private System.Windows.Forms.ColorDialog MirrorColor;
        private System.Windows.Forms.ToolStripMenuItem clearSettingsToolStrip;
        public System.Windows.Forms.NumericUpDown HexPageSize;
        private System.Windows.Forms.ToolStripDropDownButton settingsToolStrip;
        public System.Windows.Forms.ToolStripMenuItem saveToRegToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem loadFromPCToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem deleteSaveFromPCToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.TabPage TestPage;
        private System.Windows.Forms.TabControl TestPages;
        private System.Windows.Forms.TabPage RS485Page;
        private System.Windows.Forms.Button ClearDataTestRS485;
        private System.Windows.Forms.Button ClearInfoTestRS485;
        private System.Windows.Forms.Button StartTestRSButton;
        private System.Windows.Forms.Button AutoScanToTest;
        private System.Windows.Forms.Button AddSignatureIDToTest;
        private System.Windows.Forms.ErrorProvider ErrorMessage;
        private System.Windows.Forms.NotifyIcon NotifyMessage;
        private System.Windows.Forms.TabPage InfoPage;
        private System.Windows.Forms.TreeView InfoTree;
        private System.Windows.Forms.GroupBox ExtraButtonsGroup;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button SetOnlineButton;
        private System.Windows.Forms.Panel ButtonsPanel;
        private System.Windows.Forms.CheckBox AutoExtraButtons;
        private System.Windows.Forms.NumericUpDown AutoExtraButtonsTimeout;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button SetBootloaderStopButton;
        private System.Windows.Forms.Button SetBootloaderStartButton;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown SetOnlineFreqNumeric;
        private System.Windows.Forms.CheckBox RadioSettingsTestBox;
        public System.Windows.Forms.NumericUpDown maxSigToScan;
        public System.Windows.Forms.NumericUpDown minSigToScan;
        private System.Windows.Forms.Button ManualScanToTest;
        private System.Windows.Forms.TabPage ConfigPage;
        private System.Windows.Forms.DataGridView ConfigDataGrid;
        private System.Windows.Forms.Button LoadConfigButton;
        private System.Windows.Forms.Button UploadConfigButton;
        private System.Windows.Forms.CheckBox ConfigFactoryCheck;
        private System.Windows.Forms.Panel extendedMenuPanel;
        private System.Windows.Forms.GroupBox scanGroupBox;
        private System.Windows.Forms.Button ShowExtendedMenu;
        private System.Windows.Forms.GroupBox settingsGroupBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button SaveLogTestRS485;
        private System.Windows.Forms.CheckBox TimerSettingsTestBox;
        private System.Windows.Forms.Timer WorkTestTimer;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.NumericUpDown numericHoursTest;
        private System.Windows.Forms.NumericUpDown numericMinutesTest;
        private System.Windows.Forms.NumericUpDown numericDaysTest;
        private System.Windows.Forms.NumericUpDown numericSecondsTest;
        private System.Windows.Forms.Panel timerPanelTest;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label WorkingTimeLabel;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.ComboBox comPort;
        public System.Windows.Forms.ComboBox BaudRate;
        public System.Windows.Forms.NumericUpDown numericPort;
        public System.Windows.Forms.TextBox IPaddressBox;
        private System.Windows.Forms.DataGridView StatusRS485GridView;
        private System.Windows.Forms.ContextMenuStrip notifyMessageStrip;
        private System.Windows.Forms.ToolStripMenuItem OpenDebugFromToolStrip;
        private System.Windows.Forms.ToolStripMenuItem CloseFromToolStrip;
        private System.Windows.Forms.ToolStripMenuItem AboutFromToolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripStatusLabel ReplyStatus;
        private System.Windows.Forms.Button MoreInfoTestRS485;
        private System.Windows.Forms.ToolStripButton DinoRunningProcessOk;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem transparentToolStrip;
        private System.Windows.Forms.ToolStripMenuItem messagesToolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem windowPinToolStrip;
        private System.Windows.Forms.CheckBox HexCheckCrc;
        public System.Windows.Forms.NumericUpDown HexTimeout;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ClearBufferSettingsTestBox;
        private System.Windows.Forms.ToolStripMenuItem extendedButtonsToolStrip;
        private System.Windows.Forms.Label HexUploadFilename;
        private System.Windows.Forms.CheckBox RMLRModeCheck;
        private System.Windows.Forms.NumericUpDown RMLRRepeatCount;
        private System.Windows.Forms.ContextMenuStrip RMLRMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem RMLRRed;
        private System.Windows.Forms.ToolStripMenuItem RMLRGreen;
        private System.Windows.Forms.ToolStripMenuItem RMLRBlue;
        private System.Windows.Forms.ToolStripMenuItem RMLRBuzzer;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ContextMenuStrip ConfigClearMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ConfigClearMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ConfigClearLoad;
        private System.Windows.Forms.ToolStripMenuItem ConfigClearUpload;
        private System.Windows.Forms.ToolStripMenuItem ConfigClearAll;
        private System.Windows.Forms.TextBox ConfigFieldTextBox;
        private System.Windows.Forms.Button ConfigAddField;
        private System.Windows.Forms.ToolStripMenuItem ConfigEnableAllMenuItem;
        private System.Windows.Forms.ContextMenuStrip SearchFindSignatureColorMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SearchChangeColorMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.ToolStripSeparator RMLRSeparator;
        private System.Windows.Forms.ToolStripMenuItem RMLRRepeat;
        private System.Windows.Forms.ContextMenuStrip RS485SortMenuStrip;
        private System.Windows.Forms.ComboBox SortedColumnCombo;
        private System.Windows.Forms.Button SortByButton;
        private System.Windows.Forms.ToolStripMenuItem byAscMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byDescMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn InterfaceStatusRM;
        private System.Windows.Forms.DataGridViewTextBoxColumn SignStatusRM;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeStatusRM;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusStatusRM;
        private System.Windows.Forms.DataGridViewTextBoxColumn TxStatusRM;
        private System.Windows.Forms.DataGridViewTextBoxColumn RxStatusRM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorStatusRM;
        private System.Windows.Forms.DataGridViewTextBoxColumn PercentErrorStatusRM;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisconnectedStatusRM;
        private System.Windows.Forms.DataGridViewTextBoxColumn BadReplyStatusRM;
        private System.Windows.Forms.DataGridViewTextBoxColumn BadCrcStatusRM;
        private System.Windows.Forms.DataGridViewTextBoxColumn RadioErrorStatusRM;
        private System.Windows.Forms.DataGridViewTextBoxColumn RadioNearbyStatusRM;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkTimeStatusRM;
        private System.Windows.Forms.DataGridViewTextBoxColumn VerStatusRM;
        private System.Windows.Forms.TabPage SearchPage;
        private System.Windows.Forms.DataGridView SearchGrid;
        private System.Windows.Forms.Button SearchManualButton;
        private System.Windows.Forms.Button SearchAutoButton;
        private System.Windows.Forms.CheckBox SearchDistTof;
        private System.Windows.Forms.CheckBox SearchGetNear;
        private System.Windows.Forms.Panel SearchModesPanel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown SearchTimeout;
        private System.Windows.Forms.GroupBox SearchExtraGroup;
        private System.Windows.Forms.CheckBox SearchExtendedFindMode;
        private System.Windows.Forms.CheckBox SearchKnockMode;
        private System.Windows.Forms.CheckBox SearchFindSignatireMode;
        private System.Windows.Forms.CheckBox SearchFilterMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn signSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn distSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn rssiSearch;
        private System.Windows.Forms.ContextMenuStrip SearchFilterMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem rM485ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rMPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rMGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rMHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rMTAToolStripMenuItem;
        private System.Windows.Forms.Button buttonInfoStop;
        public System.Windows.Forms.NumericUpDown numericInfoSeconds;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip infoMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem clearInfoMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem saveToCsvInfoMenuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldsColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EnabledColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoadFieldColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UploadFieldColumn;
    }
}
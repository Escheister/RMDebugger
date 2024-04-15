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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDebugger));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle48 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle49 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle47 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle50 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle51 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle52 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle53 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle54 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle57 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle58 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle55 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle56 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Who are you");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Status");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Get Near");
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle59 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle60 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle74 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle75 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle76 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle61 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle62 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle63 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle64 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle65 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle66 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle67 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle68 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle69 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle70 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle71 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle72 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle73 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TargetSignID = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.mainPort = new System.IO.Ports.SerialPort(this.components);
            this.DistTofGrid = new System.Windows.Forms.DataGridView();
            this.SignDistTof = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RSSIDistTof = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RMData = new System.Windows.Forms.TabControl();
            this.DistTofPage = new System.Windows.Forms.TabPage();
            this.TimeForDistTof = new System.Windows.Forms.Label();
            this.DistToftimeout = new System.Windows.Forms.TrackBar();
            this.AutoDistTof = new System.Windows.Forms.Button();
            this.ManualDistTof = new System.Windows.Forms.Button();
            this.GetNearPage = new System.Windows.Forms.TabPage();
            this.KnockKnockBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.TypeFilterBox = new System.Windows.Forms.ComboBox();
            this.MirrorColorButton = new System.Windows.Forms.Button();
            this.ExtendedBox = new System.Windows.Forms.CheckBox();
            this.MirrorBox = new System.Windows.Forms.CheckBox();
            this.TimeForGetNear = new System.Windows.Forms.Label();
            this.GetNeartimeout = new System.Windows.Forms.TrackBar();
            this.ManualGetNear = new System.Windows.Forms.Button();
            this.GetNearGrid = new System.Windows.Forms.DataGridView();
            this.SignGetNear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeGetNear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AutoGetNear = new System.Windows.Forms.Button();
            this.HexUpdatePage = new System.Windows.Forms.TabPage();
            this.HexUploadFilename = new System.Windows.Forms.Label();
            this.HexPathBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.HexPageSize = new System.Windows.Forms.NumericUpDown();
            this.BytesEnd = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.BytesStart = new System.Windows.Forms.Label();
            this.UpdateBar = new System.Windows.Forms.ProgressBar();
            this.HexPathButton = new System.Windows.Forms.Button();
            this.HexUploadButton = new System.Windows.Forms.Button();
            this.ConfigPage = new System.Windows.Forms.TabPage();
            this.ClearGridButton = new System.Windows.Forms.Button();
            this.ConfigFactoryCheck = new System.Windows.Forms.CheckBox();
            this.UploadConfigButton = new System.Windows.Forms.Button();
            this.LoadConfigButton = new System.Windows.Forms.Button();
            this.ConfigDataGrid = new System.Windows.Forms.DataGridView();
            this.FieldsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnabledColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LoadFieldColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UploadFieldColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InfoPage = new System.Windows.Forms.TabPage();
            this.InfoTreePanel = new System.Windows.Forms.Panel();
            this.InfoClearGrid = new System.Windows.Forms.Button();
            this.InfoSaveToCSVButton = new System.Windows.Forms.Button();
            this.InfoFieldsGrid = new System.Windows.Forms.DataGridView();
            this.InfoFieldRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InfoValueRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenCloseMenuInfoTree = new System.Windows.Forms.Button();
            this.InfoTree = new System.Windows.Forms.TreeView();
            this.TestPage = new System.Windows.Forms.TabPage();
            this.TestPages = new System.Windows.Forms.TabControl();
            this.RS485Page = new System.Windows.Forms.TabPage();
            this.extendedMenuPanel = new System.Windows.Forms.Panel();
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
            this.GetMessageTestBox = new System.Windows.Forms.CheckBox();
            this.BufferTestBox = new System.Windows.Forms.CheckBox();
            this.TimerTestBox = new System.Windows.Forms.CheckBox();
            this.RadioCheckTestBox = new System.Windows.Forms.CheckBox();
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
            this.StatusRM485GridView = new System.Windows.Forms.DataGridView();
            this.PasswordBox = new System.Windows.Forms.TextBox();
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
            this.PinButton = new System.Windows.Forms.ToolStripButton();
            this.SignaturePanel = new System.Windows.Forms.Panel();
            this.ThroughSignID = new System.Windows.Forms.NumericUpDown();
            this.NeedThrough = new System.Windows.Forms.CheckBox();
            this.ExtraButtonsGroup = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.AutoExtraButtonsTimeout = new System.Windows.Forms.NumericUpDown();
            this.ButtonsPanel = new System.Windows.Forms.Panel();
            this.SetOnlineFreqNumeric = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.SetBootloaderStopButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.SetOnlineButton = new System.Windows.Forms.Button();
            this.SetBootloaderStartButton = new System.Windows.Forms.Button();
            this.AutoExtraButtons = new System.Windows.Forms.CheckBox();
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
            this.Interface = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.WorkTimeStatusRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VerStatusRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.TargetSignID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistTofGrid)).BeginInit();
            this.RMData.SuspendLayout();
            this.DistTofPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DistToftimeout)).BeginInit();
            this.GetNearPage.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GetNeartimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GetNearGrid)).BeginInit();
            this.HexUpdatePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HexPageSize)).BeginInit();
            this.ConfigPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConfigDataGrid)).BeginInit();
            this.InfoPage.SuspendLayout();
            this.InfoTreePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InfoFieldsGrid)).BeginInit();
            this.TestPage.SuspendLayout();
            this.TestPages.SuspendLayout();
            this.RS485Page.SuspendLayout();
            this.extendedMenuPanel.SuspendLayout();
            this.timerPanelTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericHoursTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSecondsTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMinutesTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDaysTest)).BeginInit();
            this.settingsGroupBox.SuspendLayout();
            this.scanGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxSigToScan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minSigToScan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusRM485GridView)).BeginInit();
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
            // DistTofGrid
            // 
            this.DistTofGrid.AllowUserToAddRows = false;
            this.DistTofGrid.AllowUserToDeleteRows = false;
            this.DistTofGrid.AllowUserToResizeColumns = false;
            this.DistTofGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DistTofGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle39;
            this.DistTofGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DistTofGrid.BackgroundColor = System.Drawing.Color.White;
            this.DistTofGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DistTofGrid.CausesValidation = false;
            this.DistTofGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.DistTofGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.DistTofGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle40.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle40.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle40.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle40.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle40.SelectionForeColor = System.Drawing.Color.Black;
            this.DistTofGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle40;
            this.DistTofGrid.ColumnHeadersHeight = 18;
            this.DistTofGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DistTofGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SignDistTof,
            this.RSSIDistTof});
            dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle43.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle43.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle43.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle43.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle43.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle43.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DistTofGrid.DefaultCellStyle = dataGridViewCellStyle43;
            this.DistTofGrid.GridColor = System.Drawing.Color.Black;
            this.DistTofGrid.Location = new System.Drawing.Point(0, 0);
            this.DistTofGrid.Margin = new System.Windows.Forms.Padding(0);
            this.DistTofGrid.MultiSelect = false;
            this.DistTofGrid.Name = "DistTofGrid";
            this.DistTofGrid.ReadOnly = true;
            this.DistTofGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DistTofGrid.RowHeadersVisible = false;
            this.DistTofGrid.RowHeadersWidth = 25;
            this.DistTofGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DistTofGrid.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DistTofGrid.RowTemplate.Height = 18;
            this.DistTofGrid.RowTemplate.ReadOnly = true;
            this.DistTofGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DistTofGrid.Size = new System.Drawing.Size(182, 269);
            this.DistTofGrid.TabIndex = 15;
            this.DistTofGrid.TabStop = false;
            // 
            // SignDistTof
            // 
            this.SignDistTof.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle41.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle41.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle41.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle41.SelectionForeColor = System.Drawing.Color.Black;
            this.SignDistTof.DefaultCellStyle = dataGridViewCellStyle41;
            this.SignDistTof.FillWeight = 50F;
            this.SignDistTof.HeaderText = "Sign";
            this.SignDistTof.MaxInputLength = 5;
            this.SignDistTof.MinimumWidth = 50;
            this.SignDistTof.Name = "SignDistTof";
            this.SignDistTof.ReadOnly = true;
            this.SignDistTof.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SignDistTof.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SignDistTof.Width = 50;
            // 
            // RSSIDistTof
            // 
            this.RSSIDistTof.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.RSSIDistTof.DataPropertyName = "(нет)";
            dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle42.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle42.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle42.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle42.SelectionForeColor = System.Drawing.Color.Black;
            this.RSSIDistTof.DefaultCellStyle = dataGridViewCellStyle42;
            this.RSSIDistTof.FillWeight = 60F;
            this.RSSIDistTof.HeaderText = "RSSI";
            this.RSSIDistTof.MaxInputLength = 4;
            this.RSSIDistTof.MinimumWidth = 60;
            this.RSSIDistTof.Name = "RSSIDistTof";
            this.RSSIDistTof.ReadOnly = true;
            this.RSSIDistTof.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.RSSIDistTof.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RSSIDistTof.Width = 60;
            // 
            // RMData
            // 
            this.RMData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RMData.Controls.Add(this.DistTofPage);
            this.RMData.Controls.Add(this.GetNearPage);
            this.RMData.Controls.Add(this.HexUpdatePage);
            this.RMData.Controls.Add(this.ConfigPage);
            this.RMData.Controls.Add(this.InfoPage);
            this.RMData.Controls.Add(this.TestPage);
            this.RMData.Cursor = System.Windows.Forms.Cursors.Default;
            this.RMData.Enabled = false;
            this.RMData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RMData.Location = new System.Drawing.Point(174, 25);
            this.RMData.Margin = new System.Windows.Forms.Padding(0);
            this.RMData.Name = "RMData";
            this.RMData.SelectedIndex = 0;
            this.RMData.Size = new System.Drawing.Size(316, 295);
            this.RMData.TabIndex = 16;
            // 
            // DistTofPage
            // 
            this.DistTofPage.BackColor = System.Drawing.Color.White;
            this.DistTofPage.Controls.Add(this.TimeForDistTof);
            this.DistTofPage.Controls.Add(this.DistToftimeout);
            this.DistTofPage.Controls.Add(this.AutoDistTof);
            this.DistTofPage.Controls.Add(this.ManualDistTof);
            this.DistTofPage.Controls.Add(this.DistTofGrid);
            this.DistTofPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DistTofPage.Location = new System.Drawing.Point(4, 22);
            this.DistTofPage.Name = "DistTofPage";
            this.DistTofPage.Size = new System.Drawing.Size(308, 269);
            this.DistTofPage.TabIndex = 0;
            this.DistTofPage.Text = "Dist Tof";
            // 
            // TimeForDistTof
            // 
            this.TimeForDistTof.AutoSize = true;
            this.TimeForDistTof.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TimeForDistTof.Location = new System.Drawing.Point(225, 26);
            this.TimeForDistTof.Name = "TimeForDistTof";
            this.TimeForDistTof.Size = new System.Drawing.Size(47, 13);
            this.TimeForDistTof.TabIndex = 19;
            this.TimeForDistTof.Text = "1000 ms";
            this.TimeForDistTof.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DistToftimeout
            // 
            this.DistToftimeout.AutoSize = false;
            this.DistToftimeout.BackColor = System.Drawing.SystemColors.Window;
            this.DistToftimeout.LargeChange = 10;
            this.DistToftimeout.Location = new System.Drawing.Point(185, 42);
            this.DistToftimeout.Maximum = 10000;
            this.DistToftimeout.Minimum = 1000;
            this.DistToftimeout.Name = "DistToftimeout";
            this.DistToftimeout.Size = new System.Drawing.Size(121, 30);
            this.DistToftimeout.SmallChange = 10;
            this.DistToftimeout.TabIndex = 18;
            this.DistToftimeout.TickFrequency = 1000;
            this.DistToftimeout.Value = 1000;
            // 
            // AutoDistTof
            // 
            this.AutoDistTof.BackColor = System.Drawing.Color.Transparent;
            this.AutoDistTof.Image = global::RMDebugger.Properties.Resources.StatusRunning;
            this.AutoDistTof.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AutoDistTof.Location = new System.Drawing.Point(185, 78);
            this.AutoDistTof.Name = "AutoDistTof";
            this.AutoDistTof.Size = new System.Drawing.Size(121, 23);
            this.AutoDistTof.TabIndex = 17;
            this.AutoDistTof.Text = "Auto";
            this.AutoDistTof.UseVisualStyleBackColor = false;
            // 
            // ManualDistTof
            // 
            this.ManualDistTof.Location = new System.Drawing.Point(185, 2);
            this.ManualDistTof.Name = "ManualDistTof";
            this.ManualDistTof.Size = new System.Drawing.Size(121, 23);
            this.ManualDistTof.TabIndex = 16;
            this.ManualDistTof.Text = "Manual";
            this.ManualDistTof.UseVisualStyleBackColor = true;
            // 
            // GetNearPage
            // 
            this.GetNearPage.BackColor = System.Drawing.Color.White;
            this.GetNearPage.Controls.Add(this.KnockKnockBox);
            this.GetNearPage.Controls.Add(this.panel1);
            this.GetNearPage.Controls.Add(this.MirrorColorButton);
            this.GetNearPage.Controls.Add(this.ExtendedBox);
            this.GetNearPage.Controls.Add(this.MirrorBox);
            this.GetNearPage.Controls.Add(this.TimeForGetNear);
            this.GetNearPage.Controls.Add(this.GetNeartimeout);
            this.GetNearPage.Controls.Add(this.ManualGetNear);
            this.GetNearPage.Controls.Add(this.GetNearGrid);
            this.GetNearPage.Controls.Add(this.AutoGetNear);
            this.GetNearPage.Location = new System.Drawing.Point(4, 22);
            this.GetNearPage.Name = "GetNearPage";
            this.GetNearPage.Size = new System.Drawing.Size(308, 269);
            this.GetNearPage.TabIndex = 2;
            this.GetNearPage.Text = "Get Near";
            // 
            // KnockKnockBox
            // 
            this.KnockKnockBox.AutoSize = true;
            this.KnockKnockBox.Location = new System.Drawing.Point(186, 151);
            this.KnockKnockBox.Name = "KnockKnockBox";
            this.KnockKnockBox.Size = new System.Drawing.Size(91, 17);
            this.KnockKnockBox.TabIndex = 30;
            this.KnockKnockBox.Text = "Knock-Knock";
            this.ToolTipHelper.SetToolTip(this.KnockKnockBox, "В прямом смысле \"Достучаться до устройства\" в авто режиме");
            this.KnockKnockBox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.TypeFilterBox);
            this.panel1.Location = new System.Drawing.Point(185, 174);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(123, 95);
            this.panel1.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Device Type Filter";
            // 
            // TypeFilterBox
            // 
            this.TypeFilterBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeFilterBox.FormattingEnabled = true;
            this.TypeFilterBox.Items.AddRange(new object[] {
            "<Any>",
            "RM485",
            "RMP",
            "RMG",
            "RMH",
            "RMTA"});
            this.TypeFilterBox.Location = new System.Drawing.Point(1, 18);
            this.TypeFilterBox.Name = "TypeFilterBox";
            this.TypeFilterBox.Size = new System.Drawing.Size(120, 21);
            this.TypeFilterBox.TabIndex = 27;
            // 
            // MirrorColorButton
            // 
            this.MirrorColorButton.Image = ((System.Drawing.Image)(resources.GetObject("MirrorColorButton.Image")));
            this.MirrorColorButton.Location = new System.Drawing.Point(281, 102);
            this.MirrorColorButton.Name = "MirrorColorButton";
            this.MirrorColorButton.Size = new System.Drawing.Size(25, 23);
            this.MirrorColorButton.TabIndex = 26;
            this.ToolTipHelper.SetToolTip(this.MirrorColorButton, "Выбрать цвет выделения");
            this.MirrorColorButton.UseVisualStyleBackColor = true;
            // 
            // ExtendedBox
            // 
            this.ExtendedBox.AutoSize = true;
            this.ExtendedBox.Location = new System.Drawing.Point(186, 128);
            this.ExtendedBox.Name = "ExtendedBox";
            this.ExtendedBox.Size = new System.Drawing.Size(94, 17);
            this.ExtendedBox.TabIndex = 25;
            this.ExtendedBox.Text = "Extended Find";
            this.ToolTipHelper.SetToolTip(this.ExtendedBox, "Расширенный поиск");
            this.ExtendedBox.UseVisualStyleBackColor = true;
            // 
            // MirrorBox
            // 
            this.MirrorBox.AutoSize = true;
            this.MirrorBox.Location = new System.Drawing.Point(186, 105);
            this.MirrorBox.Name = "MirrorBox";
            this.MirrorBox.Size = new System.Drawing.Size(94, 17);
            this.MirrorBox.TabIndex = 24;
            this.MirrorBox.Text = "Find Signature";
            this.ToolTipHelper.SetToolTip(this.MirrorBox, "Поиск сигнатур устройств, подключенных к одной шине RS485, которые видят основную" +
        " сигнатуру в радиоэфире");
            this.MirrorBox.UseVisualStyleBackColor = true;
            // 
            // TimeForGetNear
            // 
            this.TimeForGetNear.AutoSize = true;
            this.TimeForGetNear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TimeForGetNear.Location = new System.Drawing.Point(225, 26);
            this.TimeForGetNear.Name = "TimeForGetNear";
            this.TimeForGetNear.Size = new System.Drawing.Size(41, 13);
            this.TimeForGetNear.TabIndex = 23;
            this.TimeForGetNear.Text = "500 ms";
            this.TimeForGetNear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GetNeartimeout
            // 
            this.GetNeartimeout.AutoSize = false;
            this.GetNeartimeout.BackColor = System.Drawing.SystemColors.Window;
            this.GetNeartimeout.LargeChange = 10;
            this.GetNeartimeout.Location = new System.Drawing.Point(185, 42);
            this.GetNeartimeout.Maximum = 10000;
            this.GetNeartimeout.Minimum = 200;
            this.GetNeartimeout.Name = "GetNeartimeout";
            this.GetNeartimeout.Size = new System.Drawing.Size(121, 30);
            this.GetNeartimeout.SmallChange = 10;
            this.GetNeartimeout.TabIndex = 22;
            this.GetNeartimeout.TickFrequency = 1000;
            this.GetNeartimeout.Value = 500;
            // 
            // ManualGetNear
            // 
            this.ManualGetNear.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ManualGetNear.Location = new System.Drawing.Point(185, 2);
            this.ManualGetNear.Name = "ManualGetNear";
            this.ManualGetNear.Size = new System.Drawing.Size(121, 23);
            this.ManualGetNear.TabIndex = 20;
            this.ManualGetNear.Text = "Manual";
            this.ManualGetNear.UseVisualStyleBackColor = true;
            // 
            // GetNearGrid
            // 
            this.GetNearGrid.AllowUserToAddRows = false;
            this.GetNearGrid.AllowUserToDeleteRows = false;
            this.GetNearGrid.AllowUserToResizeColumns = false;
            this.GetNearGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.GetNearGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle44;
            this.GetNearGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GetNearGrid.BackgroundColor = System.Drawing.Color.White;
            this.GetNearGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GetNearGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.GetNearGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.GetNearGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle45.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle45.Font = new System.Drawing.Font("Consolas", 9F);
            dataGridViewCellStyle45.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle45.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle45.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle45.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GetNearGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle45;
            this.GetNearGrid.ColumnHeadersHeight = 18;
            this.GetNearGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.GetNearGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SignGetNear,
            this.TypeGetNear});
            dataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle48.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle48.Font = new System.Drawing.Font("Consolas", 11F);
            dataGridViewCellStyle48.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle48.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle48.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle48.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GetNearGrid.DefaultCellStyle = dataGridViewCellStyle48;
            this.GetNearGrid.GridColor = System.Drawing.Color.White;
            this.GetNearGrid.Location = new System.Drawing.Point(0, 0);
            this.GetNearGrid.Margin = new System.Windows.Forms.Padding(0);
            this.GetNearGrid.MultiSelect = false;
            this.GetNearGrid.Name = "GetNearGrid";
            this.GetNearGrid.ReadOnly = true;
            this.GetNearGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle49.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle49.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle49.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle49.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle49.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle49.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle49.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GetNearGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle49;
            this.GetNearGrid.RowHeadersVisible = false;
            this.GetNearGrid.RowHeadersWidth = 25;
            this.GetNearGrid.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.GetNearGrid.RowTemplate.Height = 18;
            this.GetNearGrid.RowTemplate.ReadOnly = true;
            this.GetNearGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GetNearGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.GetNearGrid.Size = new System.Drawing.Size(182, 269);
            this.GetNearGrid.TabIndex = 0;
            // 
            // SignGetNear
            // 
            this.SignGetNear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle46.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle46.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle46.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle46.SelectionForeColor = System.Drawing.Color.Black;
            this.SignGetNear.DefaultCellStyle = dataGridViewCellStyle46;
            this.SignGetNear.FillWeight = 50F;
            this.SignGetNear.HeaderText = "Sign";
            this.SignGetNear.MaxInputLength = 5;
            this.SignGetNear.MinimumWidth = 50;
            this.SignGetNear.Name = "SignGetNear";
            this.SignGetNear.ReadOnly = true;
            this.SignGetNear.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SignGetNear.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SignGetNear.Width = 50;
            // 
            // TypeGetNear
            // 
            this.TypeGetNear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle47.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle47.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle47.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle47.SelectionForeColor = System.Drawing.Color.Black;
            this.TypeGetNear.DefaultCellStyle = dataGridViewCellStyle47;
            this.TypeGetNear.FillWeight = 60F;
            this.TypeGetNear.HeaderText = "Type";
            this.TypeGetNear.MaxInputLength = 6;
            this.TypeGetNear.MinimumWidth = 60;
            this.TypeGetNear.Name = "TypeGetNear";
            this.TypeGetNear.ReadOnly = true;
            this.TypeGetNear.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TypeGetNear.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TypeGetNear.Width = 60;
            // 
            // AutoGetNear
            // 
            this.AutoGetNear.Image = global::RMDebugger.Properties.Resources.StatusRunning;
            this.AutoGetNear.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AutoGetNear.Location = new System.Drawing.Point(185, 78);
            this.AutoGetNear.Name = "AutoGetNear";
            this.AutoGetNear.Size = new System.Drawing.Size(121, 23);
            this.AutoGetNear.TabIndex = 21;
            this.AutoGetNear.Text = "Auto";
            this.AutoGetNear.UseVisualStyleBackColor = true;
            // 
            // HexUpdatePage
            // 
            this.HexUpdatePage.BackColor = System.Drawing.Color.White;
            this.HexUpdatePage.Controls.Add(this.HexUploadFilename);
            this.HexUpdatePage.Controls.Add(this.HexPathBox);
            this.HexUpdatePage.Controls.Add(this.label2);
            this.HexUpdatePage.Controls.Add(this.HexPageSize);
            this.HexUpdatePage.Controls.Add(this.BytesEnd);
            this.HexUpdatePage.Controls.Add(this.label8);
            this.HexUpdatePage.Controls.Add(this.BytesStart);
            this.HexUpdatePage.Controls.Add(this.UpdateBar);
            this.HexUpdatePage.Controls.Add(this.HexPathButton);
            this.HexUpdatePage.Controls.Add(this.HexUploadButton);
            this.HexUpdatePage.Location = new System.Drawing.Point(4, 22);
            this.HexUpdatePage.Name = "HexUpdatePage";
            this.HexUpdatePage.Size = new System.Drawing.Size(308, 269);
            this.HexUpdatePage.TabIndex = 3;
            this.HexUpdatePage.Text = "Hex Update";
            // 
            // HexUploadFilename
            // 
            this.HexUploadFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HexUploadFilename.Location = new System.Drawing.Point(1, 223);
            this.HexUploadFilename.Name = "HexUploadFilename";
            this.HexUploadFilename.Size = new System.Drawing.Size(304, 13);
            this.HexUploadFilename.TabIndex = 25;
            this.HexUploadFilename.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.HexUploadFilename.DoubleClick += new System.EventHandler(this.HexUploadFilename_DoubleClick);
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
            this.HexPathBox.Size = new System.Drawing.Size(282, 21);
            this.HexPathBox.Sorted = true;
            this.HexPathBox.TabIndex = 21;
            this.ToolTipHelper.SetToolTip(this.HexPathBox, "Перетащите *.hex файл в это поле\r\n");
            this.HexPathBox.TextChanged += new System.EventHandler(this.Hex_Box_TextChanged);
            this.HexPathBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.Hex_Box_DragDrop);
            this.HexPathBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.Hex_Box_DragEnter);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Page Size:";
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
            this.HexPageSize.Location = new System.Drawing.Point(266, 25);
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
            this.HexPageSize.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // BytesEnd
            // 
            this.BytesEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BytesEnd.BackColor = System.Drawing.Color.Transparent;
            this.BytesEnd.Location = new System.Drawing.Point(190, 252);
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
            this.label8.Location = new System.Drawing.Point(146, 253);
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
            this.BytesStart.Location = new System.Drawing.Point(68, 252);
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
            this.UpdateBar.BackColor = System.Drawing.Color.White;
            this.UpdateBar.ForeColor = System.Drawing.Color.White;
            this.UpdateBar.Location = new System.Drawing.Point(0, 239);
            this.UpdateBar.Margin = new System.Windows.Forms.Padding(0);
            this.UpdateBar.MarqueeAnimationSpeed = 2500;
            this.UpdateBar.Name = "UpdateBar";
            this.UpdateBar.Size = new System.Drawing.Size(306, 10);
            this.UpdateBar.Step = 1;
            this.UpdateBar.TabIndex = 6;
            // 
            // HexPathButton
            // 
            this.HexPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.HexPathButton.BackColor = System.Drawing.Color.Transparent;
            this.HexPathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HexPathButton.Image = ((System.Drawing.Image)(resources.GetObject("HexPathButton.Image")));
            this.HexPathButton.Location = new System.Drawing.Point(284, 2);
            this.HexPathButton.Margin = new System.Windows.Forms.Padding(0);
            this.HexPathButton.Name = "HexPathButton";
            this.HexPathButton.Size = new System.Drawing.Size(22, 21);
            this.HexPathButton.TabIndex = 23;
            this.ToolTipHelper.SetToolTip(this.HexPathButton, "Открывает проводник для выбора файла");
            this.HexPathButton.UseVisualStyleBackColor = false;
            this.HexPathButton.Click += new System.EventHandler(this.HexPathButton_Click);
            // 
            // HexUploadButton
            // 
            this.HexUploadButton.Enabled = false;
            this.HexUploadButton.Image = global::RMDebugger.Properties.Resources.StatusRunning;
            this.HexUploadButton.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.HexUploadButton.Location = new System.Drawing.Point(-1, 24);
            this.HexUploadButton.Name = "HexUploadButton";
            this.HexUploadButton.Size = new System.Drawing.Size(81, 22);
            this.HexUploadButton.TabIndex = 2;
            this.HexUploadButton.Text = "Upload";
            this.HexUploadButton.UseVisualStyleBackColor = true;
            // 
            // ConfigPage
            // 
            this.ConfigPage.Controls.Add(this.ClearGridButton);
            this.ConfigPage.Controls.Add(this.ConfigFactoryCheck);
            this.ConfigPage.Controls.Add(this.UploadConfigButton);
            this.ConfigPage.Controls.Add(this.LoadConfigButton);
            this.ConfigPage.Controls.Add(this.ConfigDataGrid);
            this.ConfigPage.Location = new System.Drawing.Point(4, 22);
            this.ConfigPage.Name = "ConfigPage";
            this.ConfigPage.Size = new System.Drawing.Size(308, 269);
            this.ConfigPage.TabIndex = 8;
            this.ConfigPage.Text = "Config";
            this.ConfigPage.UseVisualStyleBackColor = true;
            // 
            // ClearGridButton
            // 
            this.ClearGridButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClearGridButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ClearGridButton.Image = global::RMDebugger.Properties.Resources.Eraser;
            this.ClearGridButton.Location = new System.Drawing.Point(220, 249);
            this.ClearGridButton.Margin = new System.Windows.Forms.Padding(0);
            this.ClearGridButton.Name = "ClearGridButton";
            this.ClearGridButton.Size = new System.Drawing.Size(20, 20);
            this.ClearGridButton.TabIndex = 27;
            this.ToolTipHelper.SetToolTip(this.ClearGridButton, "Очистка содержимого столбцов Load и Upload помеченных галочкой");
            this.ClearGridButton.UseVisualStyleBackColor = true;
            this.ClearGridButton.Click += new System.EventHandler(this.ClearGridButton_Click);
            // 
            // ConfigFactoryCheck
            // 
            this.ConfigFactoryCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConfigFactoryCheck.AutoSize = true;
            this.ConfigFactoryCheck.Location = new System.Drawing.Point(242, 251);
            this.ConfigFactoryCheck.Name = "ConfigFactoryCheck";
            this.ConfigFactoryCheck.Size = new System.Drawing.Size(61, 17);
            this.ConfigFactoryCheck.TabIndex = 26;
            this.ConfigFactoryCheck.Text = "Factory";
            this.ConfigFactoryCheck.UseVisualStyleBackColor = true;
            // 
            // UploadConfigButton
            // 
            this.UploadConfigButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UploadConfigButton.Image = global::RMDebugger.Properties.Resources.CloudUpload;
            this.UploadConfigButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UploadConfigButton.Location = new System.Drawing.Point(109, 248);
            this.UploadConfigButton.Margin = new System.Windows.Forms.Padding(0);
            this.UploadConfigButton.Name = "UploadConfigButton";
            this.UploadConfigButton.Size = new System.Drawing.Size(110, 22);
            this.UploadConfigButton.TabIndex = 2;
            this.UploadConfigButton.Text = "Upload to device";
            this.UploadConfigButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UploadConfigButton.UseVisualStyleBackColor = true;
            // 
            // LoadConfigButton
            // 
            this.LoadConfigButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LoadConfigButton.Image = global::RMDebugger.Properties.Resources.CloudDownload;
            this.LoadConfigButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LoadConfigButton.Location = new System.Drawing.Point(-1, 248);
            this.LoadConfigButton.Margin = new System.Windows.Forms.Padding(0);
            this.LoadConfigButton.Name = "LoadConfigButton";
            this.LoadConfigButton.Size = new System.Drawing.Size(110, 22);
            this.LoadConfigButton.TabIndex = 1;
            this.LoadConfigButton.Text = "Load from device";
            this.LoadConfigButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LoadConfigButton.UseVisualStyleBackColor = true;
            // 
            // ConfigDataGrid
            // 
            this.ConfigDataGrid.AllowUserToAddRows = false;
            this.ConfigDataGrid.AllowUserToOrderColumns = true;
            this.ConfigDataGrid.AllowUserToResizeRows = false;
            this.ConfigDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ConfigDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.ConfigDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConfigDataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle50.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle50.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle50.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle50.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle50.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle50.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle50.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ConfigDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle50;
            this.ConfigDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConfigDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FieldsColumn,
            this.EnabledColumn,
            this.LoadFieldColumn,
            this.UploadFieldColumn});
            dataGridViewCellStyle51.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle51.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle51.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle51.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle51.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle51.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle51.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ConfigDataGrid.DefaultCellStyle = dataGridViewCellStyle51;
            this.ConfigDataGrid.EnableHeadersVisualStyles = false;
            this.ConfigDataGrid.GridColor = System.Drawing.Color.DarkGray;
            this.ConfigDataGrid.Location = new System.Drawing.Point(0, 0);
            this.ConfigDataGrid.Margin = new System.Windows.Forms.Padding(0);
            this.ConfigDataGrid.MultiSelect = false;
            this.ConfigDataGrid.Name = "ConfigDataGrid";
            this.ConfigDataGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle52.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle52.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle52.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle52.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle52.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle52.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle52.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ConfigDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle52;
            this.ConfigDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.ConfigDataGrid.Size = new System.Drawing.Size(308, 248);
            this.ConfigDataGrid.TabIndex = 0;
            this.ConfigDataGrid.TabStop = false;
            this.ConfigDataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.ConfigDataGrid_CellEndEdit);
            this.ConfigDataGrid.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.ConfigDataGrid_UserDeletingRow);
            // 
            // FieldsColumn
            // 
            this.FieldsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.FieldsColumn.Frozen = true;
            this.FieldsColumn.HeaderText = "Field";
            this.FieldsColumn.MaxInputLength = 16;
            this.FieldsColumn.Name = "FieldsColumn";
            this.FieldsColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FieldsColumn.Width = 35;
            // 
            // EnabledColumn
            // 
            this.EnabledColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
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
            this.LoadFieldColumn.Frozen = true;
            this.LoadFieldColumn.HeaderText = "Load Value";
            this.LoadFieldColumn.MaxInputLength = 16;
            this.LoadFieldColumn.Name = "LoadFieldColumn";
            this.LoadFieldColumn.ReadOnly = true;
            this.LoadFieldColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LoadFieldColumn.Width = 67;
            // 
            // UploadFieldColumn
            // 
            this.UploadFieldColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.UploadFieldColumn.HeaderText = "Upload Value";
            this.UploadFieldColumn.MaxInputLength = 16;
            this.UploadFieldColumn.Name = "UploadFieldColumn";
            this.UploadFieldColumn.ReadOnly = true;
            this.UploadFieldColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UploadFieldColumn.Width = 77;
            // 
            // InfoPage
            // 
            this.InfoPage.Controls.Add(this.InfoTreePanel);
            this.InfoPage.Controls.Add(this.InfoTree);
            this.InfoPage.Location = new System.Drawing.Point(4, 22);
            this.InfoPage.Name = "InfoPage";
            this.InfoPage.Padding = new System.Windows.Forms.Padding(3);
            this.InfoPage.Size = new System.Drawing.Size(308, 269);
            this.InfoPage.TabIndex = 7;
            this.InfoPage.Text = "Info";
            this.InfoPage.UseVisualStyleBackColor = true;
            // 
            // InfoTreePanel
            // 
            this.InfoTreePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoTreePanel.BackColor = System.Drawing.Color.White;
            this.InfoTreePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InfoTreePanel.Controls.Add(this.InfoClearGrid);
            this.InfoTreePanel.Controls.Add(this.InfoSaveToCSVButton);
            this.InfoTreePanel.Controls.Add(this.InfoFieldsGrid);
            this.InfoTreePanel.Controls.Add(this.OpenCloseMenuInfoTree);
            this.InfoTreePanel.Location = new System.Drawing.Point(129, 0);
            this.InfoTreePanel.Margin = new System.Windows.Forms.Padding(0);
            this.InfoTreePanel.Name = "InfoTreePanel";
            this.InfoTreePanel.Size = new System.Drawing.Size(179, 269);
            this.InfoTreePanel.TabIndex = 2;
            // 
            // InfoClearGrid
            // 
            this.InfoClearGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoClearGrid.Location = new System.Drawing.Point(97, 244);
            this.InfoClearGrid.Margin = new System.Windows.Forms.Padding(0);
            this.InfoClearGrid.Name = "InfoClearGrid";
            this.InfoClearGrid.Size = new System.Drawing.Size(80, 23);
            this.InfoClearGrid.TabIndex = 6;
            this.InfoClearGrid.Text = "Clear";
            this.InfoClearGrid.UseVisualStyleBackColor = true;
            this.InfoClearGrid.Click += new System.EventHandler(this.InfoClearGrid_Click);
            // 
            // InfoSaveToCSVButton
            // 
            this.InfoSaveToCSVButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoSaveToCSVButton.Location = new System.Drawing.Point(16, 244);
            this.InfoSaveToCSVButton.Margin = new System.Windows.Forms.Padding(0);
            this.InfoSaveToCSVButton.Name = "InfoSaveToCSVButton";
            this.InfoSaveToCSVButton.Size = new System.Drawing.Size(80, 23);
            this.InfoSaveToCSVButton.TabIndex = 5;
            this.InfoSaveToCSVButton.Text = "To CSV";
            this.InfoSaveToCSVButton.UseVisualStyleBackColor = true;
            this.InfoSaveToCSVButton.Click += new System.EventHandler(this.InfoSaveToCSVButton_Click);
            // 
            // InfoFieldsGrid
            // 
            this.InfoFieldsGrid.AllowUserToAddRows = false;
            this.InfoFieldsGrid.AllowUserToDeleteRows = false;
            this.InfoFieldsGrid.AllowUserToResizeColumns = false;
            this.InfoFieldsGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle53.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.InfoFieldsGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle53;
            this.InfoFieldsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.InfoFieldsGrid.BackgroundColor = System.Drawing.Color.White;
            this.InfoFieldsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InfoFieldsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.InfoFieldsGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.InfoFieldsGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle54.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle54.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle54.Font = new System.Drawing.Font("Consolas", 8.25F);
            dataGridViewCellStyle54.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle54.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle54.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle54.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InfoFieldsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle54;
            this.InfoFieldsGrid.ColumnHeadersHeight = 18;
            this.InfoFieldsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.InfoFieldsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InfoFieldRow,
            this.InfoValueRow});
            dataGridViewCellStyle57.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle57.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle57.Font = new System.Drawing.Font("Consolas", 8.25F);
            dataGridViewCellStyle57.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle57.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle57.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle57.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.InfoFieldsGrid.DefaultCellStyle = dataGridViewCellStyle57;
            this.InfoFieldsGrid.GridColor = System.Drawing.Color.White;
            this.InfoFieldsGrid.Location = new System.Drawing.Point(17, -1);
            this.InfoFieldsGrid.MultiSelect = false;
            this.InfoFieldsGrid.Name = "InfoFieldsGrid";
            this.InfoFieldsGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle58.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle58.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle58.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle58.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle58.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle58.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle58.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InfoFieldsGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle58;
            this.InfoFieldsGrid.RowHeadersVisible = false;
            this.InfoFieldsGrid.RowHeadersWidth = 25;
            this.InfoFieldsGrid.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.InfoFieldsGrid.RowTemplate.Height = 18;
            this.InfoFieldsGrid.RowTemplate.ReadOnly = true;
            this.InfoFieldsGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.InfoFieldsGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.InfoFieldsGrid.Size = new System.Drawing.Size(162, 242);
            this.InfoFieldsGrid.TabIndex = 4;
            // 
            // InfoFieldRow
            // 
            this.InfoFieldRow.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle55.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle55.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle55.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle55.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle55.SelectionForeColor = System.Drawing.Color.Black;
            this.InfoFieldRow.DefaultCellStyle = dataGridViewCellStyle55;
            this.InfoFieldRow.HeaderText = "Field";
            this.InfoFieldRow.MaxInputLength = 5;
            this.InfoFieldRow.MinimumWidth = 60;
            this.InfoFieldRow.Name = "InfoFieldRow";
            this.InfoFieldRow.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.InfoFieldRow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // InfoValueRow
            // 
            this.InfoValueRow.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle56.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle56.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle56.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle56.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle56.SelectionForeColor = System.Drawing.Color.Black;
            this.InfoValueRow.DefaultCellStyle = dataGridViewCellStyle56;
            this.InfoValueRow.HeaderText = "Value";
            this.InfoValueRow.MaxInputLength = 6;
            this.InfoValueRow.MinimumWidth = 60;
            this.InfoValueRow.Name = "InfoValueRow";
            this.InfoValueRow.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.InfoValueRow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InfoValueRow.Width = 60;
            // 
            // OpenCloseMenuInfoTree
            // 
            this.OpenCloseMenuInfoTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.OpenCloseMenuInfoTree.FlatAppearance.BorderSize = 0;
            this.OpenCloseMenuInfoTree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenCloseMenuInfoTree.Location = new System.Drawing.Point(-1, -1);
            this.OpenCloseMenuInfoTree.Margin = new System.Windows.Forms.Padding(0);
            this.OpenCloseMenuInfoTree.Name = "OpenCloseMenuInfoTree";
            this.OpenCloseMenuInfoTree.Size = new System.Drawing.Size(17, 269);
            this.OpenCloseMenuInfoTree.TabIndex = 0;
            this.OpenCloseMenuInfoTree.Text = ">";
            this.OpenCloseMenuInfoTree.UseVisualStyleBackColor = true;
            this.OpenCloseMenuInfoTree.Click += new System.EventHandler(this.OpenCloseMenuInfoTree_Click);
            // 
            // InfoTree
            // 
            this.InfoTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoTree.BackColor = System.Drawing.Color.White;
            this.InfoTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InfoTree.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InfoTree.HotTracking = true;
            this.InfoTree.Indent = 15;
            this.InfoTree.ItemHeight = 17;
            this.InfoTree.Location = new System.Drawing.Point(0, 0);
            this.InfoTree.Margin = new System.Windows.Forms.Padding(0);
            this.InfoTree.Name = "InfoTree";
            treeNode4.BackColor = System.Drawing.Color.White;
            treeNode4.ForeColor = System.Drawing.SystemColors.HotTrack;
            treeNode4.Name = "WhoAreYouInfo";
            treeNode4.NodeFont = new System.Drawing.Font("Consolas", 9F);
            treeNode4.Text = "Who are you";
            treeNode4.ToolTipText = "Нажмите, для отправки команды";
            treeNode5.BackColor = System.Drawing.Color.White;
            treeNode5.ForeColor = System.Drawing.SystemColors.HotTrack;
            treeNode5.Name = "StatusInfo";
            treeNode5.NodeFont = new System.Drawing.Font("Consolas", 9F);
            treeNode5.Text = "Status";
            treeNode5.ToolTipText = "Нажмите, для отправки команды";
            treeNode6.BackColor = System.Drawing.Color.White;
            treeNode6.ForeColor = System.Drawing.SystemColors.HotTrack;
            treeNode6.Name = "GetNearInfo";
            treeNode6.NodeFont = new System.Drawing.Font("Consolas", 9F);
            treeNode6.Text = "Get Near";
            treeNode6.ToolTipText = "Нажмите, для отправки команды";
            this.InfoTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6});
            this.InfoTree.ShowNodeToolTips = true;
            this.InfoTree.Size = new System.Drawing.Size(291, 269);
            this.InfoTree.TabIndex = 1;
            // 
            // TestPage
            // 
            this.TestPage.BackColor = System.Drawing.Color.White;
            this.TestPage.Controls.Add(this.TestPages);
            this.TestPage.Location = new System.Drawing.Point(4, 22);
            this.TestPage.Name = "TestPage";
            this.TestPage.Padding = new System.Windows.Forms.Padding(3);
            this.TestPage.Size = new System.Drawing.Size(308, 269);
            this.TestPage.TabIndex = 6;
            this.TestPage.Text = "Test";
            // 
            // TestPages
            // 
            this.TestPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TestPages.Controls.Add(this.RS485Page);
            this.TestPages.Location = new System.Drawing.Point(3, 3);
            this.TestPages.Name = "TestPages";
            this.TestPages.SelectedIndex = 0;
            this.TestPages.Size = new System.Drawing.Size(302, 263);
            this.TestPages.TabIndex = 0;
            // 
            // RS485Page
            // 
            this.RS485Page.BackColor = System.Drawing.Color.White;
            this.RS485Page.Controls.Add(this.extendedMenuPanel);
            this.RS485Page.Controls.Add(this.StatusRM485GridView);
            this.RS485Page.Location = new System.Drawing.Point(4, 22);
            this.RS485Page.Name = "RS485Page";
            this.RS485Page.Padding = new System.Windows.Forms.Padding(3);
            this.RS485Page.Size = new System.Drawing.Size(294, 237);
            this.RS485Page.TabIndex = 0;
            this.RS485Page.Text = "RS485";
            // 
            // extendedMenuPanel
            // 
            this.extendedMenuPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.extendedMenuPanel.Location = new System.Drawing.Point(0, 67);
            this.extendedMenuPanel.Margin = new System.Windows.Forms.Padding(0);
            this.extendedMenuPanel.Name = "extendedMenuPanel";
            this.extendedMenuPanel.Size = new System.Drawing.Size(294, 170);
            this.extendedMenuPanel.TabIndex = 31;
            // 
            // timerPanelTest
            // 
            this.timerPanelTest.Controls.Add(this.numericHoursTest);
            this.timerPanelTest.Controls.Add(this.numericSecondsTest);
            this.timerPanelTest.Controls.Add(this.label7);
            this.timerPanelTest.Controls.Add(this.numericMinutesTest);
            this.timerPanelTest.Controls.Add(this.numericDaysTest);
            this.timerPanelTest.Location = new System.Drawing.Point(0, 126);
            this.timerPanelTest.Margin = new System.Windows.Forms.Padding(0);
            this.timerPanelTest.Name = "timerPanelTest";
            this.timerPanelTest.Size = new System.Drawing.Size(208, 22);
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
            this.WorkingTimeLabel.Location = new System.Drawing.Point(53, 147);
            this.WorkingTimeLabel.Name = "WorkingTimeLabel";
            this.WorkingTimeLabel.Size = new System.Drawing.Size(155, 21);
            this.WorkingTimeLabel.TabIndex = 41;
            this.WorkingTimeLabel.Text = "0d 0h : 0m : 0s";
            this.WorkingTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(2, 147);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 21);
            this.label9.TabIndex = 42;
            this.label9.Text = "Test time:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SaveLogTestRS485
            // 
            this.SaveLogTestRS485.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.SaveLogTestRS485.Enabled = false;
            this.SaveLogTestRS485.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveLogTestRS485.Image = global::RMDebugger.Properties.Resources.SaveAs;
            this.SaveLogTestRS485.Location = new System.Drawing.Point(272, 0);
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
            this.ClearDataTestRS485.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ClearDataTestRS485.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ClearDataTestRS485.Image = global::RMDebugger.Properties.Resources.DeleteTable;
            this.ClearDataTestRS485.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ClearDataTestRS485.Location = new System.Drawing.Point(209, 29);
            this.ClearDataTestRS485.Margin = new System.Windows.Forms.Padding(0);
            this.ClearDataTestRS485.Name = "ClearDataTestRS485";
            this.ClearDataTestRS485.Size = new System.Drawing.Size(83, 22);
            this.ClearDataTestRS485.TabIndex = 25;
            this.ClearDataTestRS485.Text = "Clear all";
            this.ClearDataTestRS485.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ClearDataTestRS485.UseVisualStyleBackColor = true;
            this.ClearDataTestRS485.Click += new System.EventHandler(this.ClearDataStatusRM_Click);
            // 
            // ClearInfoTestRS485
            // 
            this.ClearInfoTestRS485.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ClearInfoTestRS485.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ClearInfoTestRS485.Image = global::RMDebugger.Properties.Resources.DeleteTaskList;
            this.ClearInfoTestRS485.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ClearInfoTestRS485.Location = new System.Drawing.Point(209, 52);
            this.ClearInfoTestRS485.Margin = new System.Windows.Forms.Padding(0);
            this.ClearInfoTestRS485.Name = "ClearInfoTestRS485";
            this.ClearInfoTestRS485.Size = new System.Drawing.Size(83, 22);
            this.ClearInfoTestRS485.TabIndex = 24;
            this.ClearInfoTestRS485.Text = "Clear info";
            this.ClearInfoTestRS485.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ClearInfoTestRS485.UseVisualStyleBackColor = true;
            this.ClearInfoTestRS485.Click += new System.EventHandler(this.ClearStatusStatusRM_Click);
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.Controls.Add(this.GetMessageTestBox);
            this.settingsGroupBox.Controls.Add(this.BufferTestBox);
            this.settingsGroupBox.Controls.Add(this.TimerTestBox);
            this.settingsGroupBox.Controls.Add(this.RadioCheckTestBox);
            this.settingsGroupBox.Location = new System.Drawing.Point(104, 23);
            this.settingsGroupBox.Margin = new System.Windows.Forms.Padding(0);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Padding = new System.Windows.Forms.Padding(0);
            this.settingsGroupBox.Size = new System.Drawing.Size(104, 104);
            this.settingsGroupBox.TabIndex = 33;
            this.settingsGroupBox.TabStop = false;
            this.settingsGroupBox.Text = "Settings";
            // 
            // GetMessageTestBox
            // 
            this.GetMessageTestBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GetMessageTestBox.AutoSize = true;
            this.GetMessageTestBox.Location = new System.Drawing.Point(55, 45);
            this.GetMessageTestBox.Margin = new System.Windows.Forms.Padding(0);
            this.GetMessageTestBox.Name = "GetMessageTestBox";
            this.GetMessageTestBox.Size = new System.Drawing.Size(45, 17);
            this.GetMessageTestBox.TabIndex = 30;
            this.GetMessageTestBox.Text = "msg";
            this.ToolTipHelper.SetToolTip(this.GetMessageTestBox, "Получение уведомления о завершении");
            this.GetMessageTestBox.UseVisualStyleBackColor = true;
            this.GetMessageTestBox.Visible = false;
            // 
            // BufferTestBox
            // 
            this.BufferTestBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BufferTestBox.AutoSize = true;
            this.BufferTestBox.Location = new System.Drawing.Point(3, 13);
            this.BufferTestBox.Margin = new System.Windows.Forms.Padding(0);
            this.BufferTestBox.Name = "BufferTestBox";
            this.BufferTestBox.Size = new System.Drawing.Size(80, 17);
            this.BufferTestBox.TabIndex = 29;
            this.BufferTestBox.Text = "Clear buffer";
            this.ToolTipHelper.SetToolTip(this.BufferTestBox, "Очистка входного и выходного буфера перед отправкой новой команды");
            this.BufferTestBox.UseVisualStyleBackColor = true;
            // 
            // TimerTestBox
            // 
            this.TimerTestBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TimerTestBox.AutoSize = true;
            this.TimerTestBox.Location = new System.Drawing.Point(3, 45);
            this.TimerTestBox.Margin = new System.Windows.Forms.Padding(0);
            this.TimerTestBox.Name = "TimerTestBox";
            this.TimerTestBox.Size = new System.Drawing.Size(52, 17);
            this.TimerTestBox.TabIndex = 28;
            this.TimerTestBox.Text = "Timer";
            this.ToolTipHelper.SetToolTip(this.TimerTestBox, "Установка времени тестирования\r\n");
            this.TimerTestBox.UseVisualStyleBackColor = true;
            this.TimerTestBox.CheckedChanged += new System.EventHandler(this.TimerTestBox_CheckedChanged);
            // 
            // RadioCheckTestBox
            // 
            this.RadioCheckTestBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RadioCheckTestBox.AutoSize = true;
            this.RadioCheckTestBox.Location = new System.Drawing.Point(3, 29);
            this.RadioCheckTestBox.Margin = new System.Windows.Forms.Padding(0);
            this.RadioCheckTestBox.Name = "RadioCheckTestBox";
            this.RadioCheckTestBox.Size = new System.Drawing.Size(54, 17);
            this.RadioCheckTestBox.TabIndex = 27;
            this.RadioCheckTestBox.Text = "Radio";
            this.ToolTipHelper.SetToolTip(this.RadioCheckTestBox, resources.GetString("RadioCheckTestBox.ToolTip"));
            this.RadioCheckTestBox.UseVisualStyleBackColor = true;
            // 
            // scanGroupBox
            // 
            this.scanGroupBox.Controls.Add(this.label6);
            this.scanGroupBox.Controls.Add(this.label5);
            this.scanGroupBox.Controls.Add(this.ManualScanToTest);
            this.scanGroupBox.Controls.Add(this.maxSigToScan);
            this.scanGroupBox.Controls.Add(this.AddSignatureIDToTest);
            this.scanGroupBox.Controls.Add(this.minSigToScan);
            this.scanGroupBox.Location = new System.Drawing.Point(0, 23);
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
            this.ManualScanToTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
            this.minSigToScan.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.minSigToScan.ValueChanged += new System.EventHandler(this.minSigToScan_ValueChanged);
            // 
            // ShowExtendedMenu
            // 
            this.ShowExtendedMenu.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ShowExtendedMenu.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ShowExtendedMenu.Image = global::RMDebugger.Properties.Resources.Unhide;
            this.ShowExtendedMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ShowExtendedMenu.Location = new System.Drawing.Point(148, 0);
            this.ShowExtendedMenu.Margin = new System.Windows.Forms.Padding(0);
            this.ShowExtendedMenu.Name = "ShowExtendedMenu";
            this.ShowExtendedMenu.Size = new System.Drawing.Size(125, 22);
            this.ShowExtendedMenu.TabIndex = 31;
            this.ShowExtendedMenu.Text = "Hide &menu";
            this.ToolTipHelper.SetToolTip(this.ShowExtendedMenu, "Показать расширенное меню");
            this.ShowExtendedMenu.UseVisualStyleBackColor = true;
            this.ShowExtendedMenu.Click += new System.EventHandler(this.ShowExtendedMenu_Click);
            // 
            // StartTestRSButton
            // 
            this.StartTestRSButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.StartTestRSButton.Enabled = false;
            this.StartTestRSButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StartTestRSButton.Image = global::RMDebugger.Properties.Resources.StatusRunning;
            this.StartTestRSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.StartTestRSButton.Location = new System.Drawing.Point(0, 0);
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
            this.AutoScanToTest.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.AutoScanToTest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AutoScanToTest.Image = ((System.Drawing.Image)(resources.GetObject("AutoScanToTest.Image")));
            this.AutoScanToTest.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AutoScanToTest.Location = new System.Drawing.Point(74, 0);
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
            // StatusRM485GridView
            // 
            this.StatusRM485GridView.AllowUserToAddRows = false;
            this.StatusRM485GridView.AllowUserToOrderColumns = true;
            this.StatusRM485GridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle59.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StatusRM485GridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle59;
            this.StatusRM485GridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusRM485GridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.StatusRM485GridView.BackgroundColor = System.Drawing.Color.White;
            this.StatusRM485GridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatusRM485GridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.StatusRM485GridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.StatusRM485GridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle60.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle60.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle60.Font = new System.Drawing.Font("Consolas", 8.25F);
            dataGridViewCellStyle60.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle60.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle60.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle60.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.StatusRM485GridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle60;
            this.StatusRM485GridView.ColumnHeadersHeight = 18;
            this.StatusRM485GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.StatusRM485GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Interface,
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
            this.WorkTimeStatusRM,
            this.VerStatusRM});
            dataGridViewCellStyle74.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle74.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle74.Font = new System.Drawing.Font("Consolas", 9F);
            dataGridViewCellStyle74.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle74.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle74.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle74.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.StatusRM485GridView.DefaultCellStyle = dataGridViewCellStyle74;
            this.StatusRM485GridView.GridColor = System.Drawing.Color.White;
            this.StatusRM485GridView.Location = new System.Drawing.Point(3, 3);
            this.StatusRM485GridView.Margin = new System.Windows.Forms.Padding(1);
            this.StatusRM485GridView.Name = "StatusRM485GridView";
            this.StatusRM485GridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle75.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle75.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle75.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle75.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle75.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle75.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle75.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.StatusRM485GridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle75;
            this.StatusRM485GridView.RowHeadersVisible = false;
            this.StatusRM485GridView.RowHeadersWidth = 25;
            dataGridViewCellStyle76.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StatusRM485GridView.RowsDefaultCellStyle = dataGridViewCellStyle76;
            this.StatusRM485GridView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StatusRM485GridView.RowTemplate.ErrorText = "???";
            this.StatusRM485GridView.RowTemplate.Height = 12;
            this.StatusRM485GridView.RowTemplate.ReadOnly = true;
            this.StatusRM485GridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.StatusRM485GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.StatusRM485GridView.Size = new System.Drawing.Size(288, 207);
            this.StatusRM485GridView.TabIndex = 21;
            this.StatusRM485GridView.TabStop = false;
            this.StatusRM485GridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.StatusGridView_RowsAdded);
            this.StatusRM485GridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.StatusGridView_RowsRemoved);
            this.StatusRM485GridView.DoubleClick += new System.EventHandler(this.StatusGridView_DoubleClick);
            // 
            // PasswordBox
            // 
            this.PasswordBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PasswordBox.BackColor = System.Drawing.Color.White;
            this.PasswordBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PasswordBox.Location = new System.Drawing.Point(3, 62);
            this.PasswordBox.Margin = new System.Windows.Forms.Padding(0);
            this.PasswordBox.MaxLength = 20;
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.Size = new System.Drawing.Size(157, 13);
            this.PasswordBox.TabIndex = 26;
            this.PasswordBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PasswordBox.UseSystemPasswordChar = true;
            this.PasswordBox.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.White;
            this.statusStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.MessageStatus,
            this.ReplyStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 324);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(499, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 17;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(31, 22);
            this.toolStripStatusLabel1.Text = "Info:";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MessageStatus
            // 
            this.MessageStatus.AutoSize = false;
            this.MessageStatus.Margin = new System.Windows.Forms.Padding(0);
            this.MessageStatus.Name = "MessageStatus";
            this.MessageStatus.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.MessageStatus.Size = new System.Drawing.Size(405, 22);
            this.MessageStatus.Spring = true;
            this.MessageStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ReplyStatus
            // 
            this.ReplyStatus.AutoSize = false;
            this.ReplyStatus.Margin = new System.Windows.Forms.Padding(0);
            this.ReplyStatus.Name = "ReplyStatus";
            this.ReplyStatus.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.ReplyStatus.Size = new System.Drawing.Size(48, 22);
            this.ReplyStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SerUdpPages
            // 
            this.SerUdpPages.Controls.Add(this.SerialPage);
            this.SerUdpPages.Controls.Add(this.UdpPage);
            this.SerUdpPages.Location = new System.Drawing.Point(9, 25);
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
            this.PinButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(499, 25);
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
            this.clearSettingsToolStrip});
            this.settingsToolStrip.Image = ((System.Drawing.Image)(resources.GetObject("settingsToolStrip.Image")));
            this.settingsToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsToolStrip.Name = "settingsToolStrip";
            this.settingsToolStrip.Size = new System.Drawing.Size(78, 22);
            this.settingsToolStrip.Text = "Settings";
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
            this.dataBits.Name = "dataBits";
            this.dataBits.ShowDropDownArrow = false;
            this.dataBits.Size = new System.Drawing.Size(57, 22);
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
            this.Parity.Name = "Parity";
            this.Parity.ShowDropDownArrow = false;
            this.Parity.Size = new System.Drawing.Size(41, 22);
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
            this.stopBits.Name = "stopBits";
            this.stopBits.ShowDropDownArrow = false;
            this.stopBits.Size = new System.Drawing.Size(57, 22);
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
            this.AboutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(22, 22);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.UpdateButton.AutoSize = false;
            this.UpdateButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UpdateButton.Image = ((System.Drawing.Image)(resources.GetObject("UpdateButton.Image")));
            this.UpdateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(22, 22);
            this.UpdateButton.Text = "Update";
            this.UpdateButton.Visible = false;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // PinButton
            // 
            this.PinButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.PinButton.AutoSize = false;
            this.PinButton.CheckOnClick = true;
            this.PinButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PinButton.Image = ((System.Drawing.Image)(resources.GetObject("PinButton.Image")));
            this.PinButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PinButton.Name = "PinButton";
            this.PinButton.Size = new System.Drawing.Size(22, 22);
            this.PinButton.ToolTipText = "Обычное состояние окна.";
            // 
            // SignaturePanel
            // 
            this.SignaturePanel.BackColor = System.Drawing.Color.White;
            this.SignaturePanel.Controls.Add(this.ThroughSignID);
            this.SignaturePanel.Controls.Add(this.label1);
            this.SignaturePanel.Controls.Add(this.TargetSignID);
            this.SignaturePanel.Controls.Add(this.NeedThrough);
            this.SignaturePanel.Location = new System.Drawing.Point(9, 175);
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
            this.ExtraButtonsGroup.Controls.Add(this.PasswordBox);
            this.ExtraButtonsGroup.Controls.Add(this.label15);
            this.ExtraButtonsGroup.Controls.Add(this.AutoExtraButtonsTimeout);
            this.ExtraButtonsGroup.Controls.Add(this.ButtonsPanel);
            this.ExtraButtonsGroup.Controls.Add(this.AutoExtraButtons);
            this.ExtraButtonsGroup.Enabled = false;
            this.ExtraButtonsGroup.Location = new System.Drawing.Point(9, 221);
            this.ExtraButtonsGroup.Margin = new System.Windows.Forms.Padding(0);
            this.ExtraButtonsGroup.Name = "ExtraButtonsGroup";
            this.ExtraButtonsGroup.Size = new System.Drawing.Size(163, 99);
            this.ExtraButtonsGroup.TabIndex = 3;
            this.ExtraButtonsGroup.TabStop = false;
            this.ExtraButtonsGroup.Text = "Extra buttons";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(121, 79);
            this.label15.Margin = new System.Windows.Forms.Padding(0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(20, 13);
            this.label15.TabIndex = 22;
            this.label15.Text = "ms";
            // 
            // AutoExtraButtonsTimeout
            // 
            this.AutoExtraButtonsTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AutoExtraButtonsTimeout.Location = new System.Drawing.Point(70, 76);
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
            // ButtonsPanel
            // 
            this.ButtonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonsPanel.Controls.Add(this.SetOnlineFreqNumeric);
            this.ButtonsPanel.Controls.Add(this.label18);
            this.ButtonsPanel.Controls.Add(this.SetBootloaderStopButton);
            this.ButtonsPanel.Controls.Add(this.ResetButton);
            this.ButtonsPanel.Controls.Add(this.SetOnlineButton);
            this.ButtonsPanel.Controls.Add(this.SetBootloaderStartButton);
            this.ButtonsPanel.Location = new System.Drawing.Point(1, 16);
            this.ButtonsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonsPanel.Name = "ButtonsPanel";
            this.ButtonsPanel.Size = new System.Drawing.Size(159, 45);
            this.ButtonsPanel.TabIndex = 20;
            // 
            // SetOnlineFreqNumeric
            // 
            this.SetOnlineFreqNumeric.Location = new System.Drawing.Point(22, 2);
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
            this.label18.Location = new System.Drawing.Point(63, 6);
            this.label18.Margin = new System.Windows.Forms.Padding(0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(25, 13);
            this.label18.TabIndex = 5;
            this.label18.Text = "freq";
            // 
            // SetBootloaderStopButton
            // 
            this.SetBootloaderStopButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SetBootloaderStopButton.Image = ((System.Drawing.Image)(resources.GetObject("SetBootloaderStopButton.Image")));
            this.SetBootloaderStopButton.Location = new System.Drawing.Point(43, 23);
            this.SetBootloaderStopButton.Margin = new System.Windows.Forms.Padding(0);
            this.SetBootloaderStopButton.Name = "SetBootloaderStopButton";
            this.SetBootloaderStopButton.Size = new System.Drawing.Size(20, 20);
            this.SetBootloaderStopButton.TabIndex = 3;
            this.ToolTipHelper.SetToolTip(this.SetBootloaderStopButton, "Отправляет команду для остановки Bootloader");
            this.SetBootloaderStopButton.UseVisualStyleBackColor = true;
            this.SetBootloaderStopButton.Visible = false;
            this.SetBootloaderStopButton.Click += new System.EventHandler(this.SetBootloaderStopButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ResetButton.Image = ((System.Drawing.Image)(resources.GetObject("ResetButton.Image")));
            this.ResetButton.Location = new System.Drawing.Point(1, 23);
            this.ResetButton.Margin = new System.Windows.Forms.Padding(0);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(20, 20);
            this.ResetButton.TabIndex = 1;
            this.ToolTipHelper.SetToolTip(this.ResetButton, "Отправляет команду для перезагрузки устройства");
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Visible = false;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // SetOnlineButton
            // 
            this.SetOnlineButton.BackColor = System.Drawing.Color.White;
            this.SetOnlineButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SetOnlineButton.Image = ((System.Drawing.Image)(resources.GetObject("SetOnlineButton.Image")));
            this.SetOnlineButton.Location = new System.Drawing.Point(1, 2);
            this.SetOnlineButton.Margin = new System.Windows.Forms.Padding(0);
            this.SetOnlineButton.Name = "SetOnlineButton";
            this.SetOnlineButton.Size = new System.Drawing.Size(20, 20);
            this.SetOnlineButton.TabIndex = 0;
            this.ToolTipHelper.SetToolTip(this.SetOnlineButton, "Отправляет команду для входа устройства в Online режим");
            this.SetOnlineButton.UseVisualStyleBackColor = false;
            this.SetOnlineButton.Click += new System.EventHandler(this.SetOnlineButton_Click);
            // 
            // SetBootloaderStartButton
            // 
            this.SetBootloaderStartButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SetBootloaderStartButton.Image = ((System.Drawing.Image)(resources.GetObject("SetBootloaderStartButton.Image")));
            this.SetBootloaderStartButton.Location = new System.Drawing.Point(22, 23);
            this.SetBootloaderStartButton.Margin = new System.Windows.Forms.Padding(0);
            this.SetBootloaderStartButton.Name = "SetBootloaderStartButton";
            this.SetBootloaderStartButton.Size = new System.Drawing.Size(20, 20);
            this.SetBootloaderStartButton.TabIndex = 2;
            this.ToolTipHelper.SetToolTip(this.SetBootloaderStartButton, "Отправляет команду для перехода в Bootloader");
            this.SetBootloaderStartButton.UseVisualStyleBackColor = true;
            this.SetBootloaderStartButton.Visible = false;
            this.SetBootloaderStartButton.Click += new System.EventHandler(this.SetBootloaderStartButton_Click);
            // 
            // AutoExtraButtons
            // 
            this.AutoExtraButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AutoExtraButtons.AutoSize = true;
            this.AutoExtraButtons.Location = new System.Drawing.Point(22, 79);
            this.AutoExtraButtons.Margin = new System.Windows.Forms.Padding(0);
            this.AutoExtraButtons.Name = "AutoExtraButtons";
            this.AutoExtraButtons.Size = new System.Drawing.Size(48, 17);
            this.AutoExtraButtons.TabIndex = 2;
            this.AutoExtraButtons.Text = "Auto";
            this.ToolTipHelper.SetToolTip(this.AutoExtraButtons, resources.GetString("AutoExtraButtons.ToolTip"));
            this.AutoExtraButtons.UseVisualStyleBackColor = true;
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
            this.notifyMessageStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenDebugFromToolStrip,
            this.toolStripSeparator6,
            this.AboutFromToolStrip,
            this.CloseFromToolStrip});
            this.notifyMessageStrip.Name = "notifyMessageStrip";
            this.notifyMessageStrip.Size = new System.Drawing.Size(186, 76);
            // 
            // OpenDebugFromToolStrip
            // 
            this.OpenDebugFromToolStrip.Image = global::RMDebugger.Properties.Resources.Writeable;
            this.OpenDebugFromToolStrip.Name = "OpenDebugFromToolStrip";
            this.OpenDebugFromToolStrip.Size = new System.Drawing.Size(185, 22);
            this.OpenDebugFromToolStrip.Text = "Open debug window";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.ForeColor = System.Drawing.Color.Black;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(182, 6);
            // 
            // AboutFromToolStrip
            // 
            this.AboutFromToolStrip.Image = global::RMDebugger.Properties.Resources.StatusInformationOutlineNoColor;
            this.AboutFromToolStrip.Name = "AboutFromToolStrip";
            this.AboutFromToolStrip.Size = new System.Drawing.Size(185, 22);
            this.AboutFromToolStrip.Text = "About";
            // 
            // CloseFromToolStrip
            // 
            this.CloseFromToolStrip.Name = "CloseFromToolStrip";
            this.CloseFromToolStrip.Size = new System.Drawing.Size(185, 22);
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
            // Interface
            // 
            this.Interface.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Interface.FillWeight = 50F;
            this.Interface.HeaderText = "Interface";
            this.Interface.MaxInputLength = 5;
            this.Interface.MinimumWidth = 50;
            this.Interface.Name = "Interface";
            this.Interface.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Interface.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Interface.Width = 67;
            // 
            // SignStatusRM
            // 
            this.SignStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle61.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SignStatusRM.DefaultCellStyle = dataGridViewCellStyle61;
            this.SignStatusRM.FillWeight = 40F;
            this.SignStatusRM.HeaderText = "Sign";
            this.SignStatusRM.MaxInputLength = 5;
            this.SignStatusRM.MinimumWidth = 40;
            this.SignStatusRM.Name = "SignStatusRM";
            this.SignStatusRM.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SignStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SignStatusRM.Width = 40;
            // 
            // TypeStatusRM
            // 
            this.TypeStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle62.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.TypeStatusRM.DefaultCellStyle = dataGridViewCellStyle62;
            this.TypeStatusRM.FillWeight = 40F;
            this.TypeStatusRM.HeaderText = "Type";
            this.TypeStatusRM.MaxInputLength = 5;
            this.TypeStatusRM.MinimumWidth = 40;
            this.TypeStatusRM.Name = "TypeStatusRM";
            this.TypeStatusRM.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TypeStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TypeStatusRM.Width = 40;
            // 
            // StatusStatusRM
            // 
            this.StatusStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle63.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StatusStatusRM.DefaultCellStyle = dataGridViewCellStyle63;
            this.StatusStatusRM.FillWeight = 40F;
            this.StatusStatusRM.HeaderText = "Status";
            this.StatusStatusRM.MaxInputLength = 4;
            this.StatusStatusRM.MinimumWidth = 60;
            this.StatusStatusRM.Name = "StatusStatusRM";
            this.StatusStatusRM.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.StatusStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StatusStatusRM.Width = 60;
            // 
            // TxStatusRM
            // 
            this.TxStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle64.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.TxStatusRM.DefaultCellStyle = dataGridViewCellStyle64;
            this.TxStatusRM.HeaderText = "Tx";
            this.TxStatusRM.MaxInputLength = 7;
            this.TxStatusRM.MinimumWidth = 25;
            this.TxStatusRM.Name = "TxStatusRM";
            this.TxStatusRM.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TxStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TxStatusRM.Width = 25;
            // 
            // RxStatusRM
            // 
            this.RxStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle65.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RxStatusRM.DefaultCellStyle = dataGridViewCellStyle65;
            this.RxStatusRM.HeaderText = "Rx";
            this.RxStatusRM.MaxInputLength = 7;
            this.RxStatusRM.MinimumWidth = 25;
            this.RxStatusRM.Name = "RxStatusRM";
            this.RxStatusRM.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.RxStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RxStatusRM.Width = 25;
            // 
            // ErrorStatusRM
            // 
            this.ErrorStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle66.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ErrorStatusRM.DefaultCellStyle = dataGridViewCellStyle66;
            this.ErrorStatusRM.HeaderText = "Error";
            this.ErrorStatusRM.MaxInputLength = 7;
            this.ErrorStatusRM.MinimumWidth = 40;
            this.ErrorStatusRM.Name = "ErrorStatusRM";
            this.ErrorStatusRM.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ErrorStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ErrorStatusRM.Width = 40;
            // 
            // PercentErrorStatusRM
            // 
            this.PercentErrorStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle67.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle67.Format = "N3";
            dataGridViewCellStyle67.NullValue = null;
            this.PercentErrorStatusRM.DefaultCellStyle = dataGridViewCellStyle67;
            this.PercentErrorStatusRM.HeaderText = "%Error";
            this.PercentErrorStatusRM.MaxInputLength = 7;
            this.PercentErrorStatusRM.MinimumWidth = 40;
            this.PercentErrorStatusRM.Name = "PercentErrorStatusRM";
            this.PercentErrorStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PercentErrorStatusRM.Width = 49;
            // 
            // DisconnectedStatusRM
            // 
            this.DisconnectedStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle68.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DisconnectedStatusRM.DefaultCellStyle = dataGridViewCellStyle68;
            this.DisconnectedStatusRM.FillWeight = 70F;
            this.DisconnectedStatusRM.HeaderText = "No Reply";
            this.DisconnectedStatusRM.MaxInputLength = 7;
            this.DisconnectedStatusRM.MinimumWidth = 70;
            this.DisconnectedStatusRM.Name = "DisconnectedStatusRM";
            this.DisconnectedStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DisconnectedStatusRM.Width = 70;
            // 
            // BadReplyStatusRM
            // 
            this.BadReplyStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle69.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.BadReplyStatusRM.DefaultCellStyle = dataGridViewCellStyle69;
            this.BadReplyStatusRM.FillWeight = 70F;
            this.BadReplyStatusRM.HeaderText = "Bad Reply";
            this.BadReplyStatusRM.MaxInputLength = 7;
            this.BadReplyStatusRM.MinimumWidth = 70;
            this.BadReplyStatusRM.Name = "BadReplyStatusRM";
            this.BadReplyStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BadReplyStatusRM.Width = 70;
            // 
            // BadCrcStatusRM
            // 
            this.BadCrcStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle70.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.BadCrcStatusRM.DefaultCellStyle = dataGridViewCellStyle70;
            this.BadCrcStatusRM.FillWeight = 70F;
            this.BadCrcStatusRM.HeaderText = "Bad CRC";
            this.BadCrcStatusRM.MaxInputLength = 7;
            this.BadCrcStatusRM.MinimumWidth = 70;
            this.BadCrcStatusRM.Name = "BadCrcStatusRM";
            this.BadCrcStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BadCrcStatusRM.Width = 70;
            // 
            // RadioErrorStatusRM
            // 
            this.RadioErrorStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle71.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RadioErrorStatusRM.DefaultCellStyle = dataGridViewCellStyle71;
            this.RadioErrorStatusRM.FillWeight = 70F;
            this.RadioErrorStatusRM.HeaderText = "Bad Radio";
            this.RadioErrorStatusRM.MaxInputLength = 7;
            this.RadioErrorStatusRM.MinimumWidth = 70;
            this.RadioErrorStatusRM.Name = "RadioErrorStatusRM";
            this.RadioErrorStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RadioErrorStatusRM.Width = 70;
            // 
            // WorkTimeStatusRM
            // 
            this.WorkTimeStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle72.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle72.NullValue = null;
            this.WorkTimeStatusRM.DefaultCellStyle = dataGridViewCellStyle72;
            this.WorkTimeStatusRM.HeaderText = "Work Time";
            this.WorkTimeStatusRM.MinimumWidth = 70;
            this.WorkTimeStatusRM.Name = "WorkTimeStatusRM";
            this.WorkTimeStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.WorkTimeStatusRM.Width = 70;
            // 
            // VerStatusRM
            // 
            this.VerStatusRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle73.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.VerStatusRM.DefaultCellStyle = dataGridViewCellStyle73;
            this.VerStatusRM.HeaderText = "Ver";
            this.VerStatusRM.MaxInputLength = 4;
            this.VerStatusRM.MinimumWidth = 30;
            this.VerStatusRM.Name = "VerStatusRM";
            this.VerStatusRM.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.VerStatusRM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.VerStatusRM.Width = 31;
            // 
            // MainDebugger
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(499, 346);
            this.Controls.Add(this.ExtraButtonsGroup);
            this.Controls.Add(this.SignaturePanel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.SerUdpPages);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.RMData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(515, 385);
            this.Name = "MainDebugger";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RM Debugger";
            ((System.ComponentModel.ISupportInitialize)(this.TargetSignID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistTofGrid)).EndInit();
            this.RMData.ResumeLayout(false);
            this.DistTofPage.ResumeLayout(false);
            this.DistTofPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DistToftimeout)).EndInit();
            this.GetNearPage.ResumeLayout(false);
            this.GetNearPage.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GetNeartimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GetNearGrid)).EndInit();
            this.HexUpdatePage.ResumeLayout(false);
            this.HexUpdatePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HexPageSize)).EndInit();
            this.ConfigPage.ResumeLayout(false);
            this.ConfigPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConfigDataGrid)).EndInit();
            this.InfoPage.ResumeLayout(false);
            this.InfoTreePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InfoFieldsGrid)).EndInit();
            this.TestPage.ResumeLayout(false);
            this.TestPages.ResumeLayout(false);
            this.RS485Page.ResumeLayout(false);
            this.extendedMenuPanel.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.StatusRM485GridView)).EndInit();
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
        private System.Windows.Forms.TabPage DistTofPage;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel MessageStatus;
        private System.Windows.Forms.TrackBar DistToftimeout;
        private System.Windows.Forms.Button AutoDistTof;
        private System.Windows.Forms.Button ManualDistTof;
        private System.Windows.Forms.Label TimeForDistTof;
        private System.Windows.Forms.TabPage GetNearPage;
        private System.Windows.Forms.Label TimeForGetNear;
        private System.Windows.Forms.TrackBar GetNeartimeout;
        private System.Windows.Forms.Button AutoGetNear;
        private System.Windows.Forms.Button ManualGetNear;
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
        private System.Windows.Forms.CheckBox MirrorBox;
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
        private System.Windows.Forms.CheckBox ExtendedBox;
        private System.Windows.Forms.Button MirrorColorButton;
        private System.Windows.Forms.ColorDialog MirrorColor;
        private System.Windows.Forms.ToolStripMenuItem clearSettingsToolStrip;
        public System.Windows.Forms.NumericUpDown HexPageSize;
        private System.Windows.Forms.ToolStripDropDownButton settingsToolStrip;
        public System.Windows.Forms.ToolStripMenuItem saveToRegToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem loadFromPCToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem deleteSaveFromPCToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox TypeFilterBox;
        private System.Windows.Forms.TabPage TestPage;
        private System.Windows.Forms.TabControl TestPages;
        private System.Windows.Forms.TabPage RS485Page;
        private System.Windows.Forms.Button ClearDataTestRS485;
        private System.Windows.Forms.Button ClearInfoTestRS485;
        private System.Windows.Forms.Button StartTestRSButton;
        private System.Windows.Forms.Button AutoScanToTest;
        private System.Windows.Forms.DataGridViewTextBoxColumn SignGetNear;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeGetNear;
        private System.Windows.Forms.Button AddSignatureIDToTest;
        private System.Windows.Forms.ErrorProvider ErrorMessage;
        private System.Windows.Forms.NotifyIcon NotifyMessage;
        private System.Windows.Forms.Label HexUploadFilename;
        private System.Windows.Forms.ToolStripButton PinButton;
        private System.Windows.Forms.TabPage InfoPage;
        private System.Windows.Forms.TreeView InfoTree;
        private System.Windows.Forms.Panel InfoTreePanel;
        private System.Windows.Forms.Button OpenCloseMenuInfoTree;
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
        private System.Windows.Forms.Button InfoClearGrid;
        private System.Windows.Forms.Button InfoSaveToCSVButton;
        private System.Windows.Forms.DataGridView InfoFieldsGrid;
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.CheckBox RadioCheckTestBox;
        public System.Windows.Forms.NumericUpDown maxSigToScan;
        public System.Windows.Forms.NumericUpDown minSigToScan;
        private System.Windows.Forms.Button ManualScanToTest;
        private System.Windows.Forms.TabPage ConfigPage;
        private System.Windows.Forms.DataGridView ConfigDataGrid;
        private System.Windows.Forms.Button LoadConfigButton;
        private System.Windows.Forms.Button UploadConfigButton;
        private System.Windows.Forms.CheckBox ConfigFactoryCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldsColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EnabledColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoadFieldColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UploadFieldColumn;
        private System.Windows.Forms.Button ClearGridButton;
        private System.Windows.Forms.CheckBox KnockKnockBox;
        private System.Windows.Forms.Panel extendedMenuPanel;
        private System.Windows.Forms.GroupBox scanGroupBox;
        private System.Windows.Forms.Button ShowExtendedMenu;
        private System.Windows.Forms.GroupBox settingsGroupBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button SaveLogTestRS485;
        private System.Windows.Forms.CheckBox TimerTestBox;
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
        private System.Windows.Forms.DataGridView StatusRM485GridView;
        private System.Windows.Forms.CheckBox BufferTestBox;
        private System.Windows.Forms.CheckBox GetMessageTestBox;
        private System.Windows.Forms.ContextMenuStrip notifyMessageStrip;
        private System.Windows.Forms.ToolStripMenuItem OpenDebugFromToolStrip;
        private System.Windows.Forms.ToolStripMenuItem CloseFromToolStrip;
        private System.Windows.Forms.ToolStripMenuItem AboutFromToolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.DataGridViewTextBoxColumn SignDistTof;
        private System.Windows.Forms.DataGridViewTextBoxColumn RSSIDistTof;
        private System.Windows.Forms.ToolStripStatusLabel ReplyStatus;
        private System.Windows.Forms.DataGridView DistTofGrid;
        private System.Windows.Forms.DataGridView GetNearGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn InfoFieldRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn InfoValueRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn Interface;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkTimeStatusRM;
        private System.Windows.Forms.DataGridViewTextBoxColumn VerStatusRM;
    }
}
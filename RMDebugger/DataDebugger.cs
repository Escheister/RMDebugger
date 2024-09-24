using System.Collections.Concurrent;
using System.Windows.Forms;
using StaticSettings;
using System.Linq;
using System;

using ProtocolEnums;

namespace RMDebugger
{
    public partial class DataDebuggerForm : Form
    {
        private ConcurrentQueue<string> linesQueue;
        public void AddToQueue(string line) => linesQueue.Enqueue(line);
        public DataDebuggerForm()
        {
            InitializeComponent();
            linesQueue = new ConcurrentQueue<string>();
            AppendTimer.Start();
            AddEvents();
            SetState(GetItemFromStripMenu(statesStrip, Options.logState.ToString()), null);
            SetSize(GetItemFromStripMenu(bufferStrip, Options.logSize.ToString()), null);
        }
        private void AddEvents()
        {
            this.FormClosed += (s, e) => { AppendTimer.Stop(); Options.debugForm = null; };
            LogBox.TextChanged += (s, e) => {
                if (LogBox.Lines.Length > (int)Options.logSize)
                    LogBox.Lines = LogBox.Lines.Skip(Options.linesRemove).ToArray();
            };
            SaveLogger.Click += (s, e) => {
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
                    LogBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
            };
            ClearLogger.Click += (s, e) => LogBox.Clear();
            foreach (ToolStripDropDownItem item in statesStrip.DropDownItems) item.Click += SetState;
            foreach (ToolStripDropDownItem item in bufferStrip.DropDownItems) item.Click += SetSize;
        }
        private object GetItemFromStripMenu(ToolStripDropDownButton menu, string enumChoice)
            => menu.DropDownItems.Find(enumChoice, false).First();
        private void SetState(object sender, EventArgs e)
        {
            ToolStripMenuItem state = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem item in statesStrip.DropDownItems) item.CheckState = CheckState.Unchecked;
            switch (state.Text)
            {
                case "DEBUG":
                    Options.logState = LogState.DEBUGState;
                    break;
                case "ERROR":
                    Options.logState = LogState.ERRORState;
                    break;
            }
            state.CheckState = CheckState.Checked;
            stateLabel.Text = state.Text;
        }
        private void SetSize(object sender, EventArgs e)
        {
            ToolStripMenuItem bufSize = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem item in bufferStrip.DropDownItems) item.CheckState = CheckState.Unchecked;
            switch (bufSize.Text)
            {
                case "Lowest":
                    Options.logSize = LogSize.lowestBuffer;
                    break;
                case "Small":
                    Options.logSize = LogSize.smallBuffer;
                    break;
                case "Normal":
                    Options.logSize = LogSize.normalBuffer;
                    break;
                case "Medium":
                    Options.logSize = LogSize.mediumBuffer;
                    break;
                case "Large":
                    Options.logSize = LogSize.largeBuffer;
                    break;
            }
            Enum.TryParse(Enum.GetName(typeof(LogSize), Options.logSize), out LogLinesRemove result);
            Options.linesRemove = (int)result;
            bufSize.CheckState = CheckState.Checked;
            bufferLabel.Text = bufSize.Text;
        }
        private void AppendTimer_Tick(object sender, EventArgs e)
        {
            while (linesQueue.Count > 0 && linesQueue.TryDequeue(out string line))
                LogBox.AppendText(line);
        }
    }
}

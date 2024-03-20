using CRC16;
using RMDebugger.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace File_Verifier
{
    public partial class AboutInfo : Form
    {
        public AboutInfo()
        {
            InitializeComponent();
            whatsNewBox.Text = Resources.WhatsNew;
        }

        private void crc_Load(object sender, EventArgs e)
        {
            using (FileStream fs = File.OpenRead(Assembly.GetExecutingAssembly().Location))
            {
                byte[] fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length);
                crcBox.Text = CheckSumCRC32(fileData);
            }
            verBox.Text = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.ToString();
        }
        private string CheckSumCRC32(byte[] buffer)
        {
            CRC32 crc = new CRC32();
            byte[] checkSum = crc.CRC_calc(buffer);
            string result = BitConverter.ToString(checkSum).Replace("-", string.Empty);
            return result;
        }
    }
}

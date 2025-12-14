using CRC32;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace FileVerifier
{
    public partial class AboutInfo : Form
    {
        public AboutInfo()
        {
            InitializeComponent();
            AddEvents();
        }

        private void AddEvents() =>
            Load += (s, e) =>
                {
                    using (FileStream fs = File.OpenRead(Assembly.GetExecutingAssembly().Location))
                    {
                        byte[] fileData = new byte[fs.Length];
                        fs.Read(fileData, 0, (int)fs.Length);
                        crcBox.Text = BitConverter.ToString(CRC32_ISO_HDLC.CrcCalc(fileData)).Replace("-", string.Empty);
                    }
                    verBox.Text = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
                };
    }
}

using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System;

using CRC16;

namespace File_Verifier
{
    public partial class AboutInfo : Form
    {
        public AboutInfo()
        {
            InitializeComponent();
            AddEvents();
        }

        private void AddEvents() => 
            this.Load += (s, e) =>
                {
                    using (FileStream fs = File.OpenRead(Assembly.GetExecutingAssembly().Location))
                    {
                        byte[] fileData = new byte[fs.Length];
                        fs.Read(fileData, 0, (int)fs.Length);
                        crcBox.Text = BitConverter.ToString(new CRC32().CRC_calc(fileData)).Replace("-", string.Empty);
                    }
                    verBox.Text = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.ToString();
                };
    }
}

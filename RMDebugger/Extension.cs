using System.Windows.Forms;
using System;

namespace RMDebugger
{
    public static class Extension
    {
        public static byte[] GetBytes(this NumericUpDown updown) => BitConverter.GetBytes(Convert.ToUInt16(updown.Value));
        public static byte[] GetBytes(this int intNum) => BitConverter.GetBytes(Convert.ToUInt16(intNum));
    }
}

using System.Windows.Forms;
using System.Linq;
using System;

namespace RMDebugger
{
    public static class Extension
    {
        public static byte[] GetBytes(this NumericUpDown updown) => BitConverter.GetBytes(Convert.ToUInt16(updown.Value));
        public static byte[] GetBytes(this int intNum) => BitConverter.GetBytes(Convert.ToUInt16(intNum));
        public static byte[] GetReverseBytes(this ushort ushortNum) => BitConverter.GetBytes(ushortNum).Reverse().ToArray();
        public static string GetStringOfBytes(this byte[] byteArray) => BitConverter.ToString(byteArray).Replace("-", " ");
    }
}

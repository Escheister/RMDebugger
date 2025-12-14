using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace RMDebugger.Main
{
    public static class Extension
    {
        public static byte[] GetBytes(this NumericUpDown updown) => BitConverter.GetBytes(Convert.ToUInt16(updown.Value));
        public static byte[] GetBytes(this int intNum) => BitConverter.GetBytes(Convert.ToUInt16(intNum));
        public static byte[] GetBytes(this ushort intNum) => BitConverter.GetBytes(Convert.ToUInt16(intNum));
        public static byte[] GetReverseBytes(this ushort ushortNum) => BitConverter.GetBytes(ushortNum).Reverse().ToArray();
        public static string GetStringOfBytes(this byte[] byteArray) => BitConverter.ToString(byteArray).Replace("-", " ");
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
        public static List<byte[]> SplitBytteArrayBy(this byte[] source, byte separator)
        {
            List<byte[]> arrays = new List<byte[]>();
            for (int i = 0, s = 0, d = 0; i < source.Length; i++, d++)
                if (source[i] == 0x00)
                {
                    arrays.Add(source.Skip(s).Take(d).ToArray());
                    s = i + 1;
                    d = 0;
                }
            return arrays;
        }
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int count)
        {
            return source.Select((x, y) => new { Index = y, Value = x })
                         .GroupBy(x => x.Index / count)
                         .Select(x => x.Select(y => y.Value).ToList())
                         .ToList();
        }
        public static string GetPropertyByHeader(this DataGridView dgv, object header)
        {
            foreach (DataGridViewColumn column in dgv.Columns)
                if (column.HeaderText == header.ToString()) return column.DataPropertyName;
            return null;
        }
    }
}

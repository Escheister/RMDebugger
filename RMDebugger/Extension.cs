using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.Linq;
using System;

namespace RMDebugger
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
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int count)
        {
            return source.Select((x, y) => new { Index = y, Value = x })
                         .GroupBy(x => x.Index / count)
                         .Select(x => x.Select(y => y.Value).ToList())
                         .ToList();
        }
        public static string GetPropertyByHeader(this DataGridView dgv, object header)
        {
            foreach(DataGridViewColumn column in dgv.Columns)
                if (column.HeaderText == header.ToString()) return column.DataPropertyName;
            return null;
        }
        public static byte[] TrimTailingZeros(this byte[] arr)
        {
            if (arr == null || arr.Length == 0)
                return arr;
            return arr.Reverse().SkipWhile(x => x == 0).Reverse().ToArray();
        }
    }
}

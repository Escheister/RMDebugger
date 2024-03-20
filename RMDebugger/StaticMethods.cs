using System.Net.Sockets;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System;

using ProtocolEnums;
using CRC16;

namespace StaticMethods
{
    static class Methods
    {
        public static string CheckSymbols(byte[] array)
        {
            string text = "";
            foreach (byte b in array)
            {
                if (b == 0xff) { text += string.Empty; continue; }
                text += Encoding.GetEncoding("koi8r").GetString(new byte[] { b });
            }
            if (text == string.Empty) return "Empty";
            return text;
        }
        public static ProtocolReply GetReply(byte[] bufferIn, byte[] rmSign, CmdInput cmdMain, bool crc=false)
        {
            if (bufferIn is null) return ProtocolReply.Null;
            if (crc) //Убрана временная проверка контрольной суммы
                if (!CRC16_CCITT_FALSE.CRC_check(bufferIn)) return ProtocolReply.WCrc; 
            if (!SignatureEqual(bufferIn, rmSign)) return ProtocolReply.WSign;
            if (!CmdInputEqual(bufferIn, cmdMain)) return ProtocolReply.WCmd;
            return ProtocolReply.Ok;
        }
        public static ProtocolReply GetReply(byte[] bufferIn, byte[] rmThrough, CmdInput cmdThrough, byte[] rmSign, CmdInput cmdMain)
        {
            if (bufferIn is null) return ProtocolReply.Null;
            /*if (!CRC16_CCITT_FALSE.CRC_check(bufferIn)) return ProtocolReply.WCrc;*/ //Убрана временная проверка контрольной суммы
            if (!SignatureEqual(bufferIn, rmThrough, rmSign)) return ProtocolReply.WSign;
            if (!CmdInputEqual(bufferIn, cmdThrough, cmdMain)) return ProtocolReply.WCmd;
            return ProtocolReply.Ok;
        }
        public static ProtocolReply GetDataReply(byte[] bufferIn, byte[] bufferOut, bool through)
        {
            if (!DataEqual(bufferIn, bufferOut, through)) return ProtocolReply.WData;
            return ProtocolReply.Ok;
        }
        private static bool SignatureEqual(byte[] bufferIn, byte[] rmSign)
        {
            try
            {
                byte[] targetSign = new byte[2] { bufferIn[0], bufferIn[1] };
                return Enumerable.SequenceEqual(rmSign, targetSign);
            }
            catch { return false; }
        }
        private static bool SignatureEqual(byte[] bufferIn, byte[] rmThrough, byte[] rmSign)
        {
            try
            {
                byte[] throughSign = new byte[2] { bufferIn[0], bufferIn[1] };
                byte[] targetSign = new byte[2] { bufferIn[4], bufferIn[5] };
                return Enumerable.SequenceEqual(rmSign, targetSign) 
                    && Enumerable.SequenceEqual(rmThrough, throughSign);
            }
            catch { return false; }
        }
        private static bool CmdInputEqual(byte[] bufferIn, CmdInput cmdMain)
        {
            try { return cmdMain == (CmdInput)((bufferIn[2] << 8) | bufferIn[3]); }
            catch { return false; }
        }
        private static bool CmdInputEqual(byte[] bufferIn, CmdInput cmdThrough, CmdInput cmdMain)
        {
            try 
            { 
                return cmdMain == (CmdInput)((bufferIn[6] << 8) | bufferIn[7]) 
                    && cmdThrough == (CmdInput)((bufferIn[2] << 8) | bufferIn[3]); 
            }
            catch { return false; }
        }
        private static bool DataEqual(byte[] bufferIn, byte[] bufferOut, bool through = false)
        {
            try
            {
                int start = 4;
                int crap = 6;
                if (through)
                {
                    start += 8;
                    crap += 12;
                }
                byte[] data_out = new byte[bufferOut.Length - crap];
                byte[] data_in = new byte[bufferOut.Length - crap];
                Array.Copy(bufferOut, start, data_out, 0, data_out.Length);
                Array.Copy(bufferIn, start, data_in, 0, data_in.Length);
                return Enumerable.SequenceEqual(data_out, data_in);
            }
            catch { return false; }
        }
        public static RmResult CheckResult(byte[] bufferIn)
        {
            try { return (RmResult)bufferIn[bufferIn.Length - 3]; }
            catch { return RmResult.Error; }
        }
        public static byte[] ReceiveData(Socket socket, int length, int ms = 250)
        {
            if (!socket.Connected) return null;
            DateTime t0 = DateTime.Now;
            TimeSpan tstop;
            int bytes;
            do
            {
                bytes = socket.Available;
                tstop = DateTime.Now - t0;
            }
            while (bytes < length && tstop.Milliseconds <= ms);
            if (bytes > 0) 
            { 
                byte[] buffer = new byte[bytes]; 
                socket.Receive(buffer);
                return buffer;
            }
            return null;
        }
        public static byte[] ReceiveData(SerialPort serial, int length, int ms = 250)
        {
            if (!serial.IsOpen) return null;
            DateTime t0 = DateTime.Now;
            TimeSpan tstop;
            int bytes;
            do
            {
                bytes = serial.BytesToRead;
                tstop = DateTime.Now - t0;
            }
            while (bytes < length && tstop.Milliseconds <= ms);
            if (bytes > 0) { 
                byte[] buffer = new byte[bytes]; 
                serial.Read(buffer, 0, bytes); 
                return buffer;
            }
            return null;
        }
        public static void FlushBuffer(SerialPort serial)
        {
            if (!serial.IsOpen) return;
            if (serial.BytesToRead > 0)
                serial.DiscardInBuffer();
        }
        public static void FlushBuffer(Socket socket)
        {
            if (!socket.Connected) return;
            if (socket.Available > 0)
                socket.Receive(new byte[socket.Available]);
        }
        public static byte[] uShortToTwoBytes(ushort cmd) => BitConverter.GetBytes(cmd).Reverse().ToArray();
    }
}

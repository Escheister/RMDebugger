using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO.Ports;
using System.Text;
using System.Linq;
using System;

using ProtocolEnums;
using StaticMethods;
using CRC16;

namespace RMDebugger
{
    internal class CommandsOutput
    {
        public CommandsOutput(SerialPort com, Socket sock)
        {
            Port = com;
            Sock = sock;
        }
        private SerialPort Port;
        private Socket Sock;

        public byte[] ReturnWithoutThrough(byte[] cmdIn)
        {
            byte[] _buffer = new byte[cmdIn.Length - 4];
            Array.Copy(cmdIn, 4, _buffer, 0, _buffer.Length);
            return _buffer;
        }
        protected byte[] EncodeToKOI8R(string text)
        {
            byte[] koi8 = new byte[text.Length + 1];
            Encoding.GetEncoding("koi8r").GetBytes(text).CopyTo(koi8, 0);
            return koi8;
        }
        public byte[] FormatCmdOut(byte[] rmSign, CmdOutput cmd, byte ix = 0x00, bool crc = true)
        {
            List<byte> data = new List<byte>();
            foreach (byte b in rmSign) data.Add(b);
            foreach (byte b in BitConverter.GetBytes((ushort)cmd).Reverse().ToArray()) data.Add(b);
            if (ix != 0xff) data.Add(ix);
            if (crc) return new CRC16_CCITT_FALSE().CRC_calc(data.ToArray());
            return data.ToArray();
        }
        public byte[] CmdThroughRm(byte[] cmdIn, byte[] rmThrough, CmdOutput cmd)
        {
            byte[] cmdOut = new byte[cmdIn.Length + 4];
            rmThrough.CopyTo(cmdOut, 0);
            Methods.uShortToTwoBytes((ushort)cmd).CopyTo(cmdOut, 2);
            cmdIn.CopyTo(cmdOut, 4);
            return new CRC16_CCITT_FALSE().CRC_calc(cmdOut);
        }
        async protected Task SendData(byte[] data) => await Task.Run(() => {
            if (Port.IsOpen) Port.Write(data, 0, data.Length);
            if (Sock.Connected) Sock.Send(data);
        });
        protected byte[] ReceiveData(int length, int ms = 250)
        {
            if (Sock.Connected) return Methods.ReceiveData(Sock, length, ms);
            else if (Port.IsOpen) return Methods.ReceiveData(Port, length, ms);
            else return null;
        }
        protected Tuple<CmdInput, byte[], CmdInput?, byte[]> ParseCmdSign(byte[] cmdOut)
        {
            CmdOutput cmdOne = (CmdOutput)((cmdOut[2] << 8) | cmdOut[3]);

            if (cmdOne != CmdOutput.ROUTING_THROUGH && cmdOne != CmdOutput.ROUTING_PROG)
            {
                Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOne), out CmdInput cmdMain);
                byte[] rmSign = new byte[2] { cmdOut[0], cmdOut[1] };
                return new Tuple<CmdInput, byte[], CmdInput?, byte[]>(cmdMain, rmSign, null, null );
            }
            else
            {
                CmdOutput cmdTwo = (CmdOutput)((cmdOut[6] << 8) | cmdOut[7]);
                Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOne), out CmdInput cmdThrough);
                Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdTwo), out CmdInput cmdMain);
                byte[] rmSign = new byte[2] { cmdOut[4], cmdOut[5] };
                byte[] rmThrough = new byte[2] { cmdOut[0], cmdOut[1] };
                return new Tuple<CmdInput, byte[], CmdInput?, byte[]>(cmdThrough, rmThrough, cmdMain, rmSign);
            }
        }
        protected void ToLogger(byte[] cmdOut, byte[] cmdIn, ProtocolReply reply)
        {
            if (StaticSettings.debugForm is null) return;
            try
            {
                StaticSettings.debugForm.BeginInvoke((MethodInvoker) delegate
                {
                    string stringOut = BitConverter.ToString(cmdOut).Replace("-", " ");
                    string stringIn = cmdIn is null ? "" : BitConverter.ToString(cmdIn).Replace("-", " ");
                    string msg = $"Send\t->  {stringOut}\n{reply}\t<-  {stringIn}\n";
                    if (StaticSettings.debugForm.allToolStripMenuItem.Checked)
                    {
                        StaticSettings.debugForm.LogBox.AppendText(msg);
                        StaticSettings.debugForm.LogBox.ScrollToCaret();
                        return;
                    }
                    if (StaticSettings.debugForm.errorsToolStripMenuItem.Checked)
                        if (reply != ProtocolReply.Ok)
                        {
                            StaticSettings.debugForm.LogBox.AppendText(msg);
                            StaticSettings.debugForm.LogBox.ScrollToCaret();
                            return;
                        }
                });
            }
            catch { }
        }
        async public Task<Tuple<RmResult, ProtocolReply>> GetResult(byte[] cmdOut, int size, int ms = 50)
        {
            if (!Sock.Connected && !Port.IsOpen) throw new Exception("No interface");
            await SendData(cmdOut);
            Tuple<CmdInput, byte[], CmdInput?, byte[]> insideCmd = ParseCmdSign(cmdOut);
            if (insideCmd.Item3 != null && insideCmd.Item4 != null) ms *= 2;
            byte[] cmdIn = ReceiveData(size, ms);
            ProtocolReply reply;
            if (insideCmd.Item3 is null && insideCmd.Item4 is null) 
                reply = Methods.GetReply(cmdIn, insideCmd.Item2, insideCmd.Item1);
            else
                reply = Methods.GetReply(cmdIn, insideCmd.Item2, insideCmd.Item1, insideCmd.Item4, (CmdInput)insideCmd.Item3);
            ToLogger(cmdOut, cmdIn, reply);
            if (reply != ProtocolReply.Ok) throw new Exception(reply.ToString());
            return new Tuple<RmResult, ProtocolReply>(Methods.CheckResult(cmdIn), reply);
        }
        async public Task<Tuple<byte[], ProtocolReply>> GetData(byte[] cmdOut, int size, int ms = 50)
        {
            if (!Sock.Connected && !Port.IsOpen) throw new Exception("No interface");
            await SendData(cmdOut);
            Tuple<CmdInput, byte[], CmdInput?, byte[]> insideCmd = ParseCmdSign(cmdOut);
            if (insideCmd.Item3 != null && insideCmd.Item4 != null) ms *= 2;
            byte[] cmdIn = ReceiveData(size, ms);
            ProtocolReply reply;
            if (insideCmd.Item3 is null && insideCmd.Item4 is null)
                reply = Methods.GetReply(cmdIn, insideCmd.Item2, insideCmd.Item1);
            else
                reply = Methods.GetReply(cmdIn, insideCmd.Item2, insideCmd.Item1, insideCmd.Item4, (CmdInput)insideCmd.Item3);
            ToLogger(cmdOut, cmdIn, reply);
            if (reply != ProtocolReply.Ok) throw new Exception(reply.ToString());
            return new Tuple<byte[], ProtocolReply>(cmdIn, reply);
        }
    }
}

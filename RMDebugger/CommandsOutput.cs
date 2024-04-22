using System.Collections.Generic;
using System.Net.Sockets;
using System.IO.Ports;
using System.Text;
using System.Linq;
using System;

using System.Threading.Tasks;
using StaticSettings;
using ProtocolEnums;
using StaticMethods;
using CRC16;

namespace RMDebugger
{
    internal class CommandsOutput
    {
        protected delegate void SendDataDelegate(byte[] cmdOut);
        protected delegate Task<byte[]> ReceiveDataDelegate(int length, int ms = 250);
        public CommandsOutput(object sender) { GetTypeDevice(sender); }

        protected SerialPort Port;
        protected Socket Sock;
        protected SendDataDelegate sendData;
        protected ReceiveDataDelegate receiveData;

        protected void GetTypeDevice(object sender)
        {
            if (sender is SerialPort ser)
            {
                Port = ser;
                sendData += (byte[] data) => Port.Write(data, 0, data.Length);
                receiveData = SerialReceiveData;
            }
            else if (sender is Socket sock)
            {
                Sock = sock;
                sendData += (byte[] data) => Sock.Send(data);
                receiveData = SocketReceiveData;
            }
        }
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
            data.AddRange(rmSign);
            data.AddRange(BitConverter.GetBytes((ushort)cmd).Reverse());
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
        async private Task<byte[]> SocketReceiveData(int length, int ms = 250)
        {
            DateTime t0 = DateTime.Now;
            TimeSpan tstop;
            int bytes;
            do
            {
                bytes = Sock.Connected ? Sock.Available : 0;
                tstop = DateTime.Now - t0;
            }
            while (bytes < length && tstop.Milliseconds <= ms);
            ArraySegment<Byte> buffer = new ArraySegment<byte>(new byte[bytes]);
            if (bytes > 0) await Sock.ReceiveAsync(buffer, SocketFlags.None);
            return buffer.ToArray();
        }
        async private Task<byte[]> SerialReceiveData(int length, int ms = 250)
        {
            DateTime t0 = DateTime.Now;
            TimeSpan tstop;
            int bytes;
            do
            {
                bytes = Port.IsOpen ? Port.BytesToRead : 0;
                tstop = DateTime.Now - t0;
            }
            while (bytes < length && tstop.Milliseconds <= ms);
            byte[] buffer = new byte[bytes];
            if (buffer.Length > 0) await Port.BaseStream.ReadAsync(buffer, 0, bytes);
            return buffer;
        }
        protected void PraseCmd(byte[] cmdOut, out CmdInput cmdMain, out CmdInput cmdThrough)
        {
            CmdOutput cmdOne = (CmdOutput)((cmdOut[2] << 8) | cmdOut[3]);
            switch (cmdOne)
            {
                case CmdOutput.ROUTING_THROUGH:
                case CmdOutput.ROUTING_PROG:
                    CmdOutput cmdTwo = (CmdOutput)((cmdOut[6] << 8) | cmdOut[7]);
                    Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOne), out cmdThrough);
                    Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdTwo), out cmdMain);
                    break;
                default:
                    Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOne), out cmdMain);
                    cmdThrough = CmdInput.ROUTING_THROUGH;
                    break;
            }
        }
        async public Task<Tuple<RmResult, ProtocolReply>> GetResult(byte[] cmdOut, int size, int ms = 50)
        {
            if (!Options.mainIsAvailable) throw new Exception("devNull");
            sendData(cmdOut);

            Task<byte[]> receiveTask = receiveData(size, Options.through ? ms * 2 : ms);

            PraseCmd(cmdOut, out CmdInput cmdMain, out CmdInput cmdThrough);

            await Task.WhenAll(receiveTask);

            byte[] cmdIn = receiveTask.Result;

            ProtocolReply reply = Options.through
                ? Methods.GetReply(cmdIn, new byte[2] { cmdOut[0], cmdOut[1] }, cmdThrough,
                                          new byte[2] { cmdOut[4], cmdOut[5] }, cmdMain)
                : Methods.GetReply(cmdIn, new byte[2] { cmdOut[0], cmdOut[1] }, cmdMain);

            Methods.ToLogger(cmdOut, cmdIn, reply);

            if (reply != ProtocolReply.Ok) throw new Exception(reply.ToString());

            return new Tuple<RmResult, ProtocolReply>(Methods.CheckResult(cmdIn), reply);
        }

        async public Task<Tuple<byte[], ProtocolReply>> GetData(byte[] cmdOut, int size, int ms = 50)
        {
            if (!Options.mainIsAvailable) throw new Exception("devNull");
            sendData(cmdOut);

            Task<byte[]> receiveTask = receiveData(size, Options.through ? ms * 2 : ms);

            PraseCmd(cmdOut, out CmdInput cmdMain, out CmdInput cmdThrough);

            await Task.WhenAll(receiveTask);

            byte[] cmdIn = receiveTask.Result;

            ProtocolReply reply = Options.through 
                ? Methods.GetReply(cmdIn, new byte[2] { cmdOut[0], cmdOut[1] }, cmdThrough,
                                          new byte[2] { cmdOut[4], cmdOut[5] }, cmdMain)
                : Methods.GetReply(cmdIn, new byte[2] { cmdOut[0], cmdOut[1] }, cmdMain);

            reply = cmdMain == CmdInput.LOAD_DATA_PAGE ? Methods.GetDataReply(cmdIn, cmdOut) : reply;

            Methods.ToLogger(cmdOut, cmdIn, reply);

            if (reply != ProtocolReply.Ok) throw new Exception(reply.ToString());

            return new Tuple<byte[], ProtocolReply>(cmdIn, reply);
        }
    }
}

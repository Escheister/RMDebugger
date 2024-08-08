﻿using System.Collections.Generic;
using System.Net.Sockets;
using System.IO.Ports;
using System.Text;
using System.Linq;
using System;

using System.Threading.Tasks;
using StaticSettings;
using ProtocolEnums;
using CRC16;

namespace RMDebugger
{
    internal class CommandsOutput : IDisposable
    {
        protected delegate void SendDataDelegate(byte[] cmdOut);
        protected delegate Task<byte[]> ReceiveDataDelegate(int length, int ms = 250);
        public delegate void ClearBufferDelegate();
        public CommandsOutput(object sender) { GetTypeDevice(sender); }
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
        }

        protected SerialPort Port;
        protected Socket Sock;
        protected SendDataDelegate sendData;
        protected ReceiveDataDelegate receiveData;
        public ClearBufferDelegate clearBuffer;
        public bool through = true;

        protected void GetTypeDevice(object sender)
        {
            if (sender is SerialPort ser)
            {
                Port = ser;
                sendData += (byte[] data) => Port.Write(data, 0, data.Length);
                receiveData = SerialReceiveData;
                clearBuffer += () => 
                { 
                    if (Port.IsOpen && Port.BytesToRead > 0) 
                        Port.DiscardInBuffer(); 
                };
            }
            else if (sender is Socket sock)
            {
                Sock = sock;
                sendData += (byte[] data) => Sock.Send(data);
                receiveData = SocketReceiveData;
                clearBuffer += () => 
                { 
                    if (Sock.Connected && Sock.Available > 0) 
                        Sock.Receive(new byte[Sock.Available]); 
                };
            }
        }
        public byte[] ReturnWithoutThrough(byte[] cmdIn)
        {
            if (Enum.TryParse(Enum.GetName(typeof(CmdInput), 
                (cmdIn[2] << 8) | cmdIn[3]), out CmdInput cmdThrough) 
                && cmdThrough == CmdInput.ROUTING_THROUGH)
            {
                byte[] _buffer = new byte[cmdIn.Length - 4];
                Array.Copy(cmdIn, 4, _buffer, 0, _buffer.Length);
                return _buffer;
            }
            else return cmdIn;
        }
        protected byte[] EncodeToKOI8R(string text)
        {
            byte[] koi8 = new byte[text.Length + 1];
            Encoding.GetEncoding("koi8r").GetBytes(text).CopyTo(koi8, 0);
            return koi8;
        }
        protected string CheckSymbols(byte[] array)
        {
            string text = "";
            foreach (byte b in array)
                text += b == 0xff ? string.Empty : Encoding.GetEncoding("koi8r").GetString(new byte[] { b });
            return text == string.Empty ? "Empty" : text;
        }
        protected virtual ProtocolReply GetReply(byte[] bufferIn, byte[] rmSign, CmdInput cmdMain)
        {
            if (bufferIn.Length == 0) return ProtocolReply.Null;
            if (Options.checkCrc && !CRC16_CCITT_FALSE.CRC_check(bufferIn)) return ProtocolReply.WCrc;
            if (!SignatureEqual(bufferIn, rmSign)) return ProtocolReply.WSign;
            if (!CmdInputEqual(bufferIn, cmdMain)) return ProtocolReply.WCmd;
            return ProtocolReply.Ok;
        }
        protected ProtocolReply GetReply(byte[] bufferIn, byte[] rmThrough, CmdInput cmdThrough, byte[] rmSign, CmdInput cmdMain)
        {
            if (bufferIn.Length == 0) return ProtocolReply.Null;
            if (Options.checkCrc && !CRC16_CCITT_FALSE.CRC_check(bufferIn)) return ProtocolReply.WCrc;
            if (!SignatureEqual(bufferIn, rmThrough, rmSign)) return ProtocolReply.WSign;
            if (!CmdInputEqual(bufferIn, cmdThrough, cmdMain)) return ProtocolReply.WCmd;
            return ProtocolReply.Ok;
        }
        protected ProtocolReply GetDataReply(byte[] bufferIn, byte[] bufferOut)
        {
            if (!DataEqual(bufferIn, bufferOut)) return ProtocolReply.WData;
            return ProtocolReply.Ok;
        }
        protected bool SignatureEqual(byte[] bufferIn, byte[] rmSign)
        {
            try
            {
                byte[] targetSign = new byte[2] { bufferIn[0], bufferIn[1] };
                return Enumerable.SequenceEqual(rmSign, targetSign);
            }
            catch { return false; }
        }
        protected bool SignatureEqual(byte[] bufferIn, byte[] rmThrough, byte[] rmSign)
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
        protected bool CmdInputEqual(byte[] bufferIn, CmdInput cmdMain)
        {
            try { return cmdMain == (CmdInput)((bufferIn[2] << 8) | bufferIn[3]); }
            catch { return false; }
        }
        protected bool CmdInputEqual(byte[] bufferIn, CmdInput cmdThrough, CmdInput cmdMain)
        {
            try
            {
                return cmdMain == (CmdInput)((bufferIn[6] << 8) | bufferIn[7])
                    && cmdThrough == (CmdInput)((bufferIn[2] << 8) | bufferIn[3]);
            }
            catch { return false; }
        }
        protected bool DataEqual(byte[] bufferIn, byte[] bufferOut)
        {
            try
            {
                int start = 4;
                int crap = 6;
                if (Options.through)
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
        public RmResult CheckResult(byte[] bufferIn)
        {
            try { return (RmResult)bufferIn[bufferIn.Length - 3]; }
            catch { return RmResult.Error; }
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
            ((ushort)cmd).GetReverseBytes().CopyTo(cmdOut, 2);
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
                bytes = Sock.Available;
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
                bytes = Port.BytesToRead;
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
                    through = true;
                    break;
                default:
                    Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOne), out cmdMain);
                    cmdThrough = CmdInput.NONE;
                    through = false;
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

            ProtocolReply reply = through
                ? GetReply(cmdIn, new byte[2] { cmdOut[0], cmdOut[1] }, cmdThrough,
                                          new byte[2] { cmdOut[4], cmdOut[5] }, cmdMain)
                : GetReply(cmdIn, new byte[2] { cmdOut[0], cmdOut[1] }, cmdMain);

            ToLogger(cmdOut, cmdIn, reply);

            if (reply != ProtocolReply.Ok) throw new Exception(reply.ToString());

            return new Tuple<RmResult, ProtocolReply>(CheckResult(cmdIn), reply);
        }
        async public Task<Tuple<byte[], ProtocolReply>> GetData(byte[] cmdOut, int size, int ms = 50)
        {
            if (!Options.mainIsAvailable) throw new Exception("devNull");
            sendData(cmdOut);

            Task<byte[]> receiveTask = receiveData(size, Options.through ? ms * 2 : ms);

            PraseCmd(cmdOut, out CmdInput cmdMain, out CmdInput cmdThrough);

            await Task.WhenAll(receiveTask);

            byte[] cmdIn = receiveTask.Result;

            ProtocolReply reply = through
                ? GetReply(cmdIn, new byte[2] { cmdOut[0], cmdOut[1] }, cmdThrough,
                                          new byte[2] { cmdOut[4], cmdOut[5] }, cmdMain)
                : GetReply(cmdIn, new byte[2] { cmdOut[0], cmdOut[1] }, cmdMain);

            reply = (reply == ProtocolReply.Ok && cmdMain == CmdInput.LOAD_DATA_PAGE)
                    ? GetDataReply(cmdIn, cmdOut) 
                    : reply;

            ToLogger(cmdOut, cmdIn, reply);

            if (reply != ProtocolReply.Ok) throw new Exception(reply.ToString());

            return new Tuple<byte[], ProtocolReply>(cmdIn, reply);
        }
        protected void ToLogger(byte[] cmdOut, byte[] cmdIn, ProtocolReply reply)
        {
            if (Options.debugForm is null) return;
            if ((Options.logState == LogState.ERRORState && reply != ProtocolReply.Ok)
                    || Options.logState == LogState.DEBUGState)
                Options.debugForm?.AddToQueue(
                    $"{Options.workTimer.Elapsed.TotalMilliseconds:0000000000}:{"send",-6}->  {cmdOut.GetStringOfBytes()}\n" +
                    $"{Options.workTimer.Elapsed.TotalMilliseconds:0000000000}:{reply,-6}<-  {cmdIn.GetStringOfBytes()}\n");
        }
    }
}

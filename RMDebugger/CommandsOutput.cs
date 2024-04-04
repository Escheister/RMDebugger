﻿using System.Collections.Generic;
using System.Net.Sockets;
using System.IO.Ports;
using System.Text;
using System.Linq;
using System;

using ProtocolEnums;
using StaticMethods;
using CRC16;
using StaticSettings;

namespace RMDebugger
{
    internal class CommandsOutput
    {
        protected delegate void SendDataDelegate(byte[] cmdOut);
        protected delegate byte[] ReceiveDataDelegate(int length, int ms = 250);
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
                sendData = SendSerialData;
                receiveData = SerialReceiveData;
            }
            else if (sender is Socket sock)
            {
                Sock = sock;
                sendData = SendSocketData;
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
        private void SendSerialData(byte[] data) => Port.Write(data, 0, data.Length);
        private void SendSocketData(byte[] data) => Sock.Send(data);
        private byte[] SocketReceiveData(int length, int ms = 250)
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
            byte[] buffer = new byte[bytes];
            if (buffer.Length > 0) Sock.Receive(buffer);
            return buffer;
        }
        private byte[] SerialReceiveData(int length, int ms = 250)
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
            if (buffer.Length > 0) Port.Read(buffer, 0, bytes); 
            return buffer;
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

        public byte[] GetDataNew(byte[] cmdOut, int size, out ProtocolReply reply, int ms = 50)
        {
            if (!Options.mainIsAvailable) throw new Exception("devNull");
            sendData(cmdOut);
            CmdOutput cmdOne = (CmdOutput)((cmdOut[2] << 8) | cmdOut[3]);
            CmdInput cmdThrough;
            CmdInput cmdMain;
            byte[] cmdIn = receiveData(size, ms);
            switch (cmdOne)
            {
                case CmdOutput.ROUTING_THROUGH: 
                case CmdOutput.ROUTING_PROG:
                    CmdOutput cmdTwo = (CmdOutput)((cmdOut[6] << 8) | cmdOut[7]);
                    Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOne), out cmdThrough);
                    Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdTwo), out cmdMain);
                    reply = Methods.GetReply(cmdIn,
                        new byte[2] { cmdOut[0], cmdOut[1] }, cmdThrough,
                        new byte[2] { cmdOut[4], cmdOut[5] }, cmdMain);
                    reply = cmdMain == CmdInput.LOAD_DATA_PAGE ? Methods.GetDataReply(cmdIn, cmdOut, true) : reply;
                    break;
                default:
                    Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOne), out cmdMain);
                    reply = Methods.GetReply(receiveData(size, ms),
                        new byte[2] { cmdOut[0], cmdOut[1] }, cmdMain);
                    reply = cmdMain == CmdInput.LOAD_DATA_PAGE ? Methods.GetDataReply(cmdIn, cmdOut, false) : reply;
                    break;
            }
            return cmdOut;
        }




        public Tuple<RmResult, ProtocolReply> GetResult(byte[] cmdOut, int size, int ms = 50)
        {
            if (!Options.mainIsAvailable) throw new Exception("No interface");
            sendData(cmdOut);
            Tuple<CmdInput, byte[], CmdInput?, byte[]> insideCmd = ParseCmdSign(cmdOut);
            if (insideCmd.Item3 != null && insideCmd.Item4 != null) ms *= 2;
            byte[] cmdIn = receiveData(size, ms);
            ProtocolReply reply;
            if (insideCmd.Item3 is null && insideCmd.Item4 is null) 
                reply = Methods.GetReply(cmdIn, insideCmd.Item2, insideCmd.Item1);
            else
                reply = Methods.GetReply(cmdIn, insideCmd.Item2, insideCmd.Item1, insideCmd.Item4, (CmdInput)insideCmd.Item3);
            Methods.ToLogger(cmdOut, cmdIn, reply);
            if (reply != ProtocolReply.Ok) throw new Exception(reply.ToString());
            return new Tuple<RmResult, ProtocolReply>(Methods.CheckResult(cmdIn), reply);
        }
        public Tuple<byte[], ProtocolReply> GetData(byte[] cmdOut, int size, int ms = 50)
        {
            if (!Options.mainIsAvailable) throw new Exception("No interface");
            sendData(cmdOut);
            Tuple<CmdInput, byte[], CmdInput?, byte[]> insideCmd = ParseCmdSign(cmdOut);
            if (insideCmd.Item3 != null && insideCmd.Item4 != null) ms *= 2;
            byte[] cmdIn = receiveData(size, ms);
            ProtocolReply reply;
            if (insideCmd.Item3 is null && insideCmd.Item4 is null)
                reply = Methods.GetReply(cmdIn, insideCmd.Item2, insideCmd.Item1);
            else
                reply = Methods.GetReply(cmdIn, insideCmd.Item2, insideCmd.Item1, insideCmd.Item4, (CmdInput)insideCmd.Item3);
            Methods.ToLogger(cmdOut, cmdIn, reply);
            if (reply != ProtocolReply.Ok) throw new Exception(reply.ToString());
            return new Tuple<byte[], ProtocolReply>(cmdIn, reply);
        }
    }
}

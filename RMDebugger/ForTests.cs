﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System;

using SearchProtocol;
using StaticMethods;
using ProtocolEnums;
using StaticSettings;
using CRC16;

namespace RMDebugger
{
    internal class ForTests : Searching
    {
        public ForTests(object sender, List<DeviceClass> _listDeviceClass) : base (sender) => listDeviceClass = _listDeviceClass;
        private List<DeviceClass> listDeviceClass;
        public List<DeviceClass> ListDeviceClass { get { return listDeviceClass; } }

        async private Task<Tuple<byte[], ProtocolReply>> GetDataTest(byte[] cmdOut, int size, int ms = 250)
        {
            if (!Options.mainIsAvailable) throw new Exception("No interface");
            sendData(cmdOut);
            Enum.TryParse(Enum.GetName(typeof(CmdOutput), (CmdOutput)((cmdOut[2] << 8) | cmdOut[3])), out CmdInput cmdMain);
            byte[] cmdIn = await receiveData(size, ms);
            ProtocolReply reply = Methods.GetReply(cmdIn, new byte[2] { cmdOut[0], cmdOut[1] }, cmdMain);
            if (!CRC16_CCITT_FALSE.CRC_check(cmdIn)) reply = ProtocolReply.WCrc;
            Methods.ToLogger(cmdOut, cmdIn, reply);
            return new Tuple<byte[], ProtocolReply>(cmdIn, reply);
        }

        private bool CheckIn(Dictionary<int, Tuple<int, int, DevType>> mainDict, Dictionary<int, int> secondDict)
        {
            foreach (int key in secondDict.Keys)
                if (mainDict.ContainsKey(key)) return true;
            return false;
        }
        private int GetCode(ProtocolReply reply)
        {
            switch (reply)
            {
                case ProtocolReply.Ok: return 10;
                case ProtocolReply.Null: return 1;
                case ProtocolReply.WCrc: return 2;
                default: return 3;
            }
        }
        async public Task<List<int>> RadioTest(byte[] rmSign, CmdOutput cmdOutput, Dictionary<int, Tuple<int, int, DevType>> rmGrid)
        {
            Dictionary<int, int> radioData = new Dictionary<int, int>();
            List<int> replyCodes = new List<int>();
            Tuple<byte[], ProtocolReply> reply;

            Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOutput), out CmdMaxSize cmdInSize);

            byte ix = 0x00;
            do
            {
                byte[] cmdOut = FormatCmdOut(rmSign, cmdOutput, ix);
                reply = await GetDataTest(cmdOut, (int)cmdInSize, 50);
                replyCodes.Add(GetCode(reply.Item2));
                if (reply.Item2 != ProtocolReply.Ok) return replyCodes;
                ix = reply.Item1[4];
                if (cmdOutput == CmdOutput.GRAPH_GET_NEAR)
                    AddKeys(radioData, GET_NEAR(reply.Item1));
            }
            while (ix != 0x00 && replyCodes.Count <= 5);
            if (rmGrid.Count > 1 && cmdOutput == CmdOutput.GRAPH_GET_NEAR)
                if (CheckIn(rmGrid, radioData) == false)
                    for (int i = 0; i < replyCodes.Count; i++) replyCodes[i] = 4;
            return replyCodes;
        }
        private int GetSizeCMD(CmdOutput cmdOutput, DevType devType)
        {
            switch (devType)
            {
                case DevType.RM485:
                    {
                        Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOutput), out RM485MaxSize rm485);
                        return (int)rm485;
                    }
                case DevType.RMH:
                    {
                        Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOutput), out RMHMaxSize RMH);
                        return (int)RMH;
                    }
                default:
                    {
                        Enum.TryParse(Enum.GetName(typeof(CmdOutput), CmdOutput.STATUS), out CmdMaxSize cmdMaxSize);
                        return (int)cmdMaxSize;
                    }
            }
        }





        //new methods
        //who are you*
        //get near**
        //online status*
        //dist tof**

        //*no radio, get info about device
        //*radio
        // 0 - ok, 1 - no reply(null), 2 - bad reply, 3 - bad crc, 4 - bad radio
        private bool AddValueToDevice(DeviceClass device, ProtocolReply reply)
        {
            switch (reply)
            {
                case ProtocolReply.Ok: return true;
                case ProtocolReply.Null:
                    device.devNoReply += 1;
                    break;
                case ProtocolReply.WCrc:
                    device.devBadCRC += 1;
                    break;
            }
            return false;
        }

        async public Task GetDataFromDevice(DeviceClass device, CmdOutput cmdOutput)
        {
            Tuple <byte[], ProtocolReply> reply = await GetDataTest(
                FormatCmdOut(device.devSign.GetBytes(), cmdOutput, 0xff), 
                GetSizeCMD(cmdOutput, device.devType), 100);
            if (!AddValueToDevice(device, reply.Item2)) return;
            if (cmdOutput == CmdOutput.STATUS)
            {
                try
                {
                    TimeSpan time = TimeSpan.FromSeconds(
                                      reply.Item1[reply.Item1.Length - 5] << 24
                                    | reply.Item1[reply.Item1.Length - 6] << 16
                                    | reply.Item1[reply.Item1.Length - 7] << 8
                                    | reply.Item1[reply.Item1.Length - 8]);
                    device.devWorkTime = $"{time.Days}d " +
                                        $"{time.Hours}h " +
                                        $": {time.Minutes}m " +
                                        $": {time.Seconds}s";
                    device.devVer = reply.Item1[reply.Item1.Length - 3] << 8 | reply.Item1[reply.Item1.Length - 4];
                }
                catch
                {
                    device.devBadReply += 1;
                    return;
                }
            }
            device.devRx += 1;
        }

















    }

    internal class DeviceClass
    {
        public DeviceClass() { }
        public string devInterface { get; set; }
        public int devSign { get; set; }
        public DevType devType { get; set; }
        public string devStatus { get; set; }
        public int devTx { get; set; }
        private int DeviceRx { get; set; }
        public int devRx
        {
            get => DeviceRx;
            set
            {
                DeviceRx = value;
                try
                {
                    devPercentErrors = 100.000 - (100.000 * DeviceRx / devTx);
                }
                catch (DivideByZeroException)
                {
                    devPercentErrors = 0;
                }
                devStatus = devPercentErrors >= 1.000 ? "Bad" : "Good";
            }
        }
        public int devErrors { get; set; }
        public double devPercentErrors { get; set; }

        private int DeviceNoReply;
        public int devNoReply
        {
            get => DeviceNoReply;
            set
            {
                DeviceNoReply = value;
                devErrors = value;
            }
        }

        private int DeviceBadReply;
        public int devBadReply
        {
            get => DeviceBadReply;
            set
            {
                DeviceBadReply = value;
                devErrors = value;
            }
        }

        private int DeviceBadCRC;
        public int devBadCRC
        {
            get => DeviceBadCRC;
            set
            {
                DeviceBadCRC = value;
                devErrors = value;
            }
        }

        private int DeviceBadRadio;
        public int devBadRadio
        {
            get => DeviceBadRadio;
            set
            {
                DeviceBadRadio = value;
                devErrors = value;
            }
        }

        public string devWorkTime { get; set; }
        public int devVer { get; set; }
    }
}

using System.Collections.Generic;
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
            int cmdInSize;
            switch (devType)
            {
                case DevType.RM485:
                    {
                        Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOutput), out RM485MaxSize rm485);
                        cmdInSize = (int)rm485;
                        break;
                    }
                case DevType.RMH:
                    {
                        Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOutput), out RMHMaxSize RMH);
                        cmdInSize = (int)RMH;
                        break;
                    }
                default:
                    {
                        Enum.TryParse(Enum.GetName(typeof(CmdOutput), CmdOutput.STATUS), out CmdMaxSize cmdMaxSize);
                        cmdInSize = (int)cmdMaxSize;
                        break;
                    }
            }
            return cmdInSize;
        }
        async public Task<Tuple<int, TimeSpan?, int?>> GetWorkTimeAndVersion(byte[] rmSign, DevType devType)
        {
            /* 1) code
             * 2) time
             * 3) version */
            byte[] cmdOut = FormatCmdOut(rmSign, CmdOutput.STATUS, 0xff);
            Tuple<byte[], ProtocolReply> reply = await GetDataTest(cmdOut, GetSizeCMD(CmdOutput.STATUS, devType), 100);
            if (reply.Item2 == ProtocolReply.Ok)
            {
                int timeSeconds =
                  reply.Item1[reply.Item1.Length - 5] << 24
                | reply.Item1[reply.Item1.Length - 6] << 16
                | reply.Item1[reply.Item1.Length - 7] << 8
                | reply.Item1[reply.Item1.Length - 8];
                int version = reply.Item1[reply.Item1.Length - 3] << 8 | reply.Item1[reply.Item1.Length - 4];
                TimeSpan time = TimeSpan.FromSeconds(timeSeconds);
                return new Tuple<int, TimeSpan?, int?>(10, time, version);
            }
            else return new Tuple<int, TimeSpan?, int?>(GetCode(reply.Item2), null, null);
        }
        async public Task<int> RS485Test(byte[] rmSign, CmdOutput cmdOutput, DevType devType)
        {
            byte[] cmdOut = FormatCmdOut(rmSign, cmdOutput, 0xff);
            Tuple<byte[], ProtocolReply> reply = await GetDataTest(cmdOut, GetSizeCMD(cmdOutput, devType), 100);
            return GetCode(reply.Item2);
        }





    }

    internal class DeviceClass
    {
        public DeviceClass() { }
        public int devIndexRow { get; set; }
        public int devSign { get; set; }
        public DevType devType { get; set; }
        public int devVer { get; set; }
    }

}

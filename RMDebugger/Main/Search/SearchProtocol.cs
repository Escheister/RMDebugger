using Enums;
using RMDebugger.Main;
using StaticSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchProtocol
{
    internal class Searching : CommandsOutput
    {
        public Searching(object sender) : base(sender) { }

        async public Task<List<DeviceData>> GetDataFromDevice(CmdOutput cmdOutput, byte[] rmSign, byte[] rmThrough)
        {
            List<DeviceData> deviceDataList = new List<DeviceData>();
            byte ix = 0x00;
            byte iteration = 1;
            Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOutput), out CmdMaxSize cmdOutSize);
            int cmdInSize = Options.through
                ? (int)cmdOutSize + 4
                : (int)cmdOutSize;
            try
            {
                do
                {
                    byte[] cmdOut = Options.through
                        ? CmdThroughRm(FormatCmdOut(rmSign, cmdOutput, ix), rmThrough, CmdOutput.ROUTING_THROUGH)
                        : FormatCmdOut(rmSign, cmdOutput, ix);
                    Tuple<byte[], ProtocolReply> requestData = await GetData(cmdOut, cmdInSize, 100);
                    requestData = new Tuple<byte[], ProtocolReply>(ReturnWithoutThrough(requestData.Item1), requestData.Item2);

                    IEnumerable<DeviceData> _List = GetDataDevices(cmdOutput, requestData.Item1, out ix);
                    deviceDataList.AddRange(_List);
                    iteration++;
                    await Task.Delay(1);
                }
                while (ix != 0x00 && iteration <= 5);
            }
            catch (OperationCanceledException) { Options.activeToken.Token.ThrowIfCancellationRequested(); }
            catch { }
            return deviceDataList;
        }
        protected IEnumerable<DeviceData> GetDataDevices(CmdOutput cmdOutput, byte[] cmdIn, out byte ix)
        {
            ix = cmdIn[4];
            return cmdOutput switch
            {
                CmdOutput.GRAPH_GET_NEAR => GetNear(cmdIn.Skip(5).Take(cmdIn.Length - 7)),
                CmdOutput.ONLINE_DIST_TOF => DistTof(cmdIn.Skip(7).Take(cmdIn.Length - 9)),
                _ => new List<DeviceData>(),
            };
        }
        protected IEnumerable<DeviceData> GetNear(IEnumerable<byte> cmdIn)
        {
            /* data = 3
             * 0-1 - sign
             * 2 - type */
            List<DeviceData> data = new List<DeviceData>();
            if (cmdIn.Count() >= 3 && cmdIn.Count() % 3 == 0)
                foreach (IEnumerable<byte> IArray in cmdIn.Split(3))
                    data.Add(new DeviceData((ushort)((IArray.ElementAt(1) << 8) | IArray.ElementAt(0)))
                    {
                        devType = Enum.IsDefined(typeof(DevType), IArray.ElementAt(2)) ? (DevType)IArray.ElementAt(2) : DevType.NuLL
                    });
            return data;
        }
        protected IEnumerable<DeviceData> DistTof(IEnumerable<byte> cmdIn)
        {
            /* data = 5
             * 0-1 - sign
             * 2-3 - dist
             * 4   - rssi */
            List<DeviceData> data = new List<DeviceData>();
            if (cmdIn.Count() >= 5 && cmdIn.Count() % 5 == 0)
                foreach (IEnumerable<byte> IArray in cmdIn.Split(5))
                {
                    int dist = (IArray.ElementAt(3) << 8) | IArray.ElementAt(2);
                    byte rssi = IArray.ElementAt(4);
                    data.Add(new DeviceData((ushort)((IArray.ElementAt(1) << 8) | IArray.ElementAt(0)))
                    {
                        devDist = dist > 0 ? $"{dist}" : "",
                        devRSSI = rssi > 0 ? $"{rssi}" : ""
                    });
                }
            return data;
        }
        public int GetVersion(byte[] bufferIn) => bufferIn[bufferIn.Length - 3] << 8
                                                | bufferIn[bufferIn.Length - 4];
        public DevType GetType(byte[] bufferIn) => (DevType)bufferIn[5];
    }
    class DeviceData
    {
        public DeviceData(ushort sign) => devSign = sign;
        public ushort devSign { get; set; }
        public DevType devType { get; set; }
        public string devDist { get; set; } = string.Empty;
        public string devRSSI { get; set; } = string.Empty;
        public bool inOneBus = false;
        public List<DeviceData> iSee = new List<DeviceData>();
    }
}
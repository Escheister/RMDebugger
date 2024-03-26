using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO.Ports;
using System;

using ProtocolEnums;
using RMDebugger;

namespace SearchProtocol
{
    internal class Searching : CommandsOutput
    {
        public Searching(object sender) : base(sender) { }

        async public Task<Tuple<byte, Dictionary<int, int>>> RequestAndParseNew(CmdOutput cmdOutput, byte ix, byte[] rmSign, byte[] rmThrough, bool through)
        {
            Dictionary<int, int> values = new Dictionary<int, int>();
            CmdMaxSize cmdOutSize;
            Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOutput), out cmdOutSize);

            byte[] cmdOut = through 
                ? CmdThroughRm(FormatCmdOut(rmSign, cmdOutput, ix), rmThrough, CmdOutput.ROUTING_THROUGH) 
                : FormatCmdOut(rmSign, cmdOutput, ix);

            int cmdInSize = through 
                ? (int)cmdOutSize + 4 
                : (int)cmdOutSize;
            Tuple<byte[], ProtocolReply> requestData = await GetData(cmdOut, cmdInSize, 50);

            requestData = through 
                ? new Tuple<byte[], ProtocolReply>(ReturnWithoutThrough(requestData.Item1), requestData.Item2) 
                : requestData;

            switch (cmdOutput)
            {
                case CmdOutput.GRAPH_GET_NEAR:
                    values = GET_NEAR(requestData.Item1);
                    break;
                case CmdOutput.ONLINE_DIST_TOF:
                    values = DIST_TOF(requestData.Item1);
                    break;
            }
            return values is null || values.Count == 0
                ? null 
                : new Tuple<byte, Dictionary<int, int>>(requestData.Item1[4], values);
        }
        public Dictionary<int, int> AddKeys(Dictionary<int, int> data, Dictionary<int, int> dataOut)
        {
            if (dataOut is null) return data;
            foreach (int key in dataOut.Keys)
                if (!data.ContainsKey(key)) data[key] = dataOut[key];
            return data;
        }
        private Dictionary<int, int> GetInfoFrom(byte[] bufferIn, CmdInput cmdIn)
        {
            cmdSize cmdS;
            dataSize dataS;
            Enum.TryParse(Enum.GetName(typeof(CmdInput), cmdIn), out cmdS);
            Enum.TryParse(Enum.GetName(typeof(CmdInput), cmdIn), out dataS);

            int sizeDataIn = bufferIn.Length - (int)cmdS;
            byte[] dataOnly = new byte[sizeDataIn];
            int step = (int)dataS;
            Array.Copy(bufferIn, (int)cmdS - 2, dataOnly, 0, sizeDataIn);

            int deviceCount;

            if (sizeDataIn >= step && sizeDataIn % step == 0)
            {
                deviceCount = sizeDataIn / step;
                int[,] deviceArray = new int[deviceCount, step];

                for (int i = 0, j = 0; i < deviceCount; i++)
                    for (int a = 0; a < step; a++, j++)
                        deviceArray[i, a] = dataOnly[j];

                Dictionary<int, int> dataOut = new Dictionary<int, int>();
                for (int i = 0; i < deviceCount; i++)
                    dataOut[(deviceArray[i, 1] << 8) | deviceArray[i, 0]] = Convert.ToUInt16(deviceArray[i, (int)dataS-1]);

                return dataOut;
            }
            else return null;
        }
        public Dictionary<int, int> DIST_TOF(byte[] bufferIn) => GetInfoFrom(bufferIn, CmdInput.ONLINE_DIST_TOF);
        public Dictionary<int, int> GET_NEAR(byte[] bufferIn) => GetInfoFrom(bufferIn, CmdInput.GRAPH_GET_NEAR);
        public int GetVersion(byte[] bufferIn)
            => bufferIn[bufferIn.Length - 3] << 8 | bufferIn[bufferIn.Length - 4];
        public DevType GetType(byte[] bufferIn) => (DevType)bufferIn[5];
    }
}
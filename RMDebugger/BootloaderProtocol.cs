using System.Collections.Generic;
using System.Threading.Tasks;
using ProtocolEnums;
using System.Linq;
using System.IO;
using System;

using StaticMethods;
using RMDebugger;
using CRC16;

namespace BootloaderProtocol
{
    internal class Bootloader : CommandsOutput
    {
        public delegate byte[] BuildCmdDelegate(CmdOutput cmdOutput);
        public delegate byte[] BuildDataCmdDelegate(byte[] data);

        public Bootloader(object sender, byte[] targetSign) : base(sender)
        {
            _addrHex = new byte[2];
            _addrElar = new byte[2];
            _targetSign = targetSign;
            buildCmdDelegate += BuildCmd;
            buildDataCmdDelegate += BuildDataCmd;
        }
        public Bootloader(object sender, byte[] targetSign, byte[] throughSign) : base(sender)
        {
            _addrHex = new byte[2];
            _addrElar = new byte[2];
            _targetSign = targetSign;
            _throughSign = throughSign;
            buildCmdDelegate += BuildCmdThrough;
            buildDataCmdDelegate += BuildDataCmdThrough;
        }

        private byte[] _addrHex;
        private byte[] _addrElar;
        private byte[] _throughSign;
        private byte[] _targetSign;

        public BuildCmdDelegate buildCmdDelegate;
        public BuildDataCmdDelegate buildDataCmdDelegate;

        private byte[] BuildCmd(CmdOutput cmdOutput) => FormatCmdOut(_targetSign, cmdOutput, 0xff);
        private byte[] BuildCmdThrough(CmdOutput cmdOutput) => CmdThroughRm(FormatCmdOut(_targetSign, cmdOutput, 0xff), _throughSign, CmdOutput.ROUTING_PROG);

        private byte[] BuildDataCmd(byte[] data) => FormatUploadData(data);
        private byte[] BuildDataCmdThrough(byte[] data) => CmdThroughRm(FormatUploadData(data), _throughSign, CmdOutput.ROUTING_PROG);

        private byte[] FormatUploadData(byte[] data)
        {
            byte[] loadField = new byte[4 + data.Length];
            _targetSign.CopyTo(loadField, 0);
            Methods.uShortToTwoBytes((ushort)CmdOutput.LOAD_DATA_PAGE).CopyTo(loadField, 2);
            data.CopyTo(loadField, 4);
            return new CRC16_CCITT_FALSE().CRC_calc(loadField);
        }
        private string[] GetStringsFromFile(string path)
        {
            using StreamReader file = new StreamReader(path);
                Task<string> data = file.ReadToEndAsync();
                return data.Result.Trim().Split('\n');
        }
        private byte[] GetBytesFromString(string line) => StringToByteArray(line.Replace(":", string.Empty).Replace("\r", string.Empty));
        private byte[] StringToByteArray(string hex) => Enumerable.Range(0, hex.Length)
                                             .Where(x => x % 2 == 0)
                                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                                             .ToArray();
        private bool IntelHexCheckSum(byte[] array)
        {
            byte hex = 0;
            for (int i = 0; i < array.Length; i++) hex += array[i];
            return hex == 0;
        }
        public byte[][] GetByteDataFromFile(string filename)
        {
            string[] fileStrings = GetStringsFromFile(filename);
            List <byte[]> stringBytes = new List <byte[]>();
            for(int i = 0; i < fileStrings.Length; i++)
            {
                byte[] byteString = GetBytesFromString(fileStrings[i]);
                if (!IntelHexCheckSum(byteString)) throw new Exception($"{FileCheck.CrcError}: {i+1}");
                switch ((RecordType)byteString[3])
                {
                    case RecordType.ESAR: goto default;
                    case RecordType.SSAR: continue;
                    case RecordType.Rec: break;
                    case RecordType.EndRec: continue;
                    case RecordType.ELAR: break;
                    case RecordType.SLAR: continue;
                    default: throw new Exception($"{FileCheck.CmdError}: {i+1}");
                }
                stringBytes.Add(byteString);
            }
            return stringBytes.ToArray();
        }
        public Tuple<byte[], int> GetDataForUpload(byte[][] hexFile, int pageSize, int indexZero)
        {
            List<byte> data = new List<byte>();
            List<byte> cmd = new List<byte>();
            RecordType type = (RecordType)hexFile[indexZero][3];
            if (type == RecordType.ELAR)
            {
                _addrElar = new byte[2] { hexFile[indexZero][5], hexFile[indexZero][4] };
                indexZero++;
                return new Tuple<byte[], int>(null, indexZero);
            }
            else
            {
                byte[] sizePack = new byte[2];
                _addrHex = new byte[2] { hexFile[indexZero][2], hexFile[indexZero][1] };
                int size = 0;
                for (;size <= pageSize;)
                {
                    type = (RecordType)hexFile[indexZero][3];
                    if (type == RecordType.ELAR) break;
                    int sizeHex = hexFile[indexZero][0];
                    if (size + sizeHex <= pageSize)
                    {
                        size += sizeHex;
                        for (int i = 4; i < hexFile[indexZero].Length - 1; i++) data.Add(hexFile[indexZero][i]);
                        if (indexZero < hexFile.Length - 1)
                        {
                            ushort predictAddr = (ushort)((hexFile[indexZero][1] << 8 | hexFile[indexZero][2]) + 0x0010);
                            indexZero++;
                            if (predictAddr == (ushort)(hexFile[indexZero][1] << 8 | hexFile[indexZero][2]))
                                continue;
                            else break;
                        }
                        else
                        {
                            indexZero++; 
                            break;
                        }
                    }
                    else break;
                }
                sizePack[0] = (byte)size;
                cmd.AddRange(sizePack);
                cmd.AddRange(_addrHex);
                cmd.AddRange(_addrElar);
                cmd.AddRange(data);
                return new Tuple<byte[], int>(cmd.ToArray(), indexZero);
            }
        }
    }
}

using System.Collections.Generic;
using System.Net.Sockets;
using System.IO.Ports;
using ProtocolEnums;
using System.Linq;
using System.IO;
using System;

using StaticMethods;
using CRC16;
using RMDebugger;

namespace BootloaderProtocol
{
    internal class Bootloader : CommandsOutput
    {
        public Bootloader(object sender) : base(sender)
        {
            addrHex = new byte[2];
            addrElar = new byte[2];
        }
        private byte[] addrHex;
        private byte[] addrElar;
        private string[] GetStringsFromFile(string path)
        {
            StreamReader file = new StreamReader(path);
            string text = file.ReadToEnd();
            file.Close();
            return text.Trim().Split('\n');
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
            if (hex == 0) return true;
            return false;
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
        public byte[] SendCommand(byte[] rmSign, CmdOutput cmdOutput)
            => FormatCmdOut(rmSign, cmdOutput, 0xff);
        public byte[] SendCommand(byte[] rmSign, byte[] rmThrough, CmdOutput cmdOutput)
            => CmdThroughRm(FormatCmdOut(rmSign, cmdOutput, 0xff), rmThrough, CmdOutput.ROUTING_PROG);
        private byte[] FormatUploadData(byte[] rmSign, byte[] data)
        {
            byte[] loadField = new byte[4 + data.Length];
            rmSign.CopyTo(loadField, 0);
            Methods.uShortToTwoBytes((ushort)CmdOutput.LOAD_DATA_PAGE).CopyTo(loadField, 2);
            data.CopyTo(loadField, 4);
            return new CRC16_CCITT_FALSE().CRC_calc(loadField);
        }
        public byte[] SendCommand(byte[] rmSign, byte[] data)
            => FormatUploadData(rmSign, data);
        public byte[] SendCommand(byte[] rmSign, byte[] rmThrough, byte[] data)
            => CmdThroughRm(SendCommand(rmSign, data), rmThrough, CmdOutput.ROUTING_PROG);
        public Tuple<byte[], int> GetDataForUpload(byte[][] hexFile, int pageSize, int indexZero)
        {
            List<byte> data = new List<byte>();
            List<byte> cmd = new List<byte>();
            RecordType type = (RecordType)hexFile[indexZero][3];
            if (type == RecordType.ELAR)
            {
                addrElar = new byte[2] { hexFile[indexZero][5], hexFile[indexZero][4] };
                indexZero++;
                return new Tuple<byte[], int>(null, indexZero);
            }
            else
            {
                byte[] sizePack = new byte[2];
                addrHex = new byte[2] { hexFile[indexZero][2], hexFile[indexZero][1] };
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
                cmd.AddRange(addrHex);
                cmd.AddRange(addrElar);
                cmd.AddRange(data);
                return new Tuple<byte[], int>(cmd.ToArray(), indexZero);
            }
        }
    }
}

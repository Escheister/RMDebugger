using CRC16;
using ProtocolEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RMDebugger
{
    internal class RMLR : CommandsOutput
    {
        public RMLR(object sender, byte[] targetSign) : base(sender) => _targetSign = targetSign;
        private byte[] _targetSign;

        // R = 0x00, W = 0x01
        private byte[] FormationRmlrSettingsCmd(byte rw, byte[] data = null)
        {
            List<byte> cmdOut = new List<byte>();
            cmdOut.AddRange(_targetSign);
            cmdOut.AddRange(((ushort)CmdOutput.RMLR_RW_SETTINGS).GetReverseBytes());
            cmdOut.Add(rw);
            if (data != null) cmdOut.AddRange(data);
            return new CRC16_CCITT_FALSE().CrcCalc(cmdOut.ToArray());
        }

        public byte[] GetCmdLoad() => FormationRmlrSettingsCmd(0);
        public byte[] GetCmdUpload(byte[] data) => FormationRmlrSettingsCmd(1, data);
        public byte[] GetCmdReset() => FormationRmlrSettingsCmd(1);

        public byte[] GetCmdRGBB(bool red, bool green, bool blue, bool buzz, byte count)
        {
            List<byte> data = new List<byte>();
            data.AddRange(_targetSign);
            data.AddRange(BitConverter.GetBytes((ushort)CmdOutput.RMLR_RGB).Reverse());
            data.Add(red   ? count : (byte)0);
            data.Add(green ? count : (byte)0);
            data.Add(blue  ? count : (byte)0);
            data.Add(buzz  ? count : (byte)0);
            return new CRC16_CCITT_FALSE().CrcCalc(data.ToArray());
        }

        public byte[] GetCmdRegistration() => FormatCmdOut(_targetSign, CmdOutput.RMLR_REGISTRATION, 0xff);

    }
}

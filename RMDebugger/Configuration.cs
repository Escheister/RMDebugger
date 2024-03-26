using SearchProtocol;
using ProtocolEnums;
using StaticMethods;
using CRC16;

namespace ConfigurationProtocol
{
    internal class Configuration : Searching
    {
        public Configuration(object sender) : base(sender) { }

        private byte[] ValueToKOI8R(string value, int size, bool factory=false)
        {
            if (factory)
            {
                byte[] valueKoi8r = new byte[size];
                for (int i = 0; i < size-1; i++) 
                    valueKoi8r[i] = 0xff;
                return valueKoi8r;
            }
            else return EncodeToKOI8R(value);
        }
        private byte[] FormationLoadConfig(byte[] rmSign, byte[] field)
        {
            byte[] loadField = new byte[4 + field.Length];
            rmSign.CopyTo(loadField, 0);
            Methods.uShortToTwoBytes((ushort)CmdOutput.GET_CONFIG_FIELD).CopyTo(loadField, 2);
            field.CopyTo(loadField, 4);
            return new CRC16_CCITT_FALSE().CRC_calc(loadField);
        }
        private byte[] FormationUploadConfig(byte[] rmSign, byte[] field, byte[] value)
        {
            byte[] loadField = new byte[4 + field.Length + value.Length];
            rmSign.CopyTo(loadField, 0);
            Methods.uShortToTwoBytes((ushort)CmdOutput.SET_CONFIG).CopyTo(loadField, 2);
            field.CopyTo(loadField, 4);
            value.CopyTo(loadField, 4 + field.Length);
            return new CRC16_CCITT_FALSE().CRC_calc(loadField);
        }

        public byte[] ConfigLoad(byte[] rmSign, string field)
            => FormationLoadConfig(rmSign, EncodeToKOI8R(field));
        public byte[] ConfigLoad(byte[] rmSign, byte[] rmThrough, string field)
            => CmdThroughRm(ConfigLoad(rmSign, field), rmThrough, CmdOutput.ROUTING_THROUGH);

        public byte[] ConfigUploadNew(byte[] rmSign, string field, string value, int size, bool factory=false)
            => FormationUploadConfig(rmSign, EncodeToKOI8R(field), ValueToKOI8R(value, size, factory));
        public byte[] ConfigUploadNew(byte[] rmSign, byte[] rmThrough, string field, string value, int size, bool factory = false)
            => CmdThroughRm(ConfigUploadNew(rmSign, field, value, size, factory), rmThrough, CmdOutput.ROUTING_THROUGH);

    }
}
using ProtocolEnums;
using RMDebugger;
using CRC16;

namespace ConfigurationProtocol
{
    internal class Configuration : CommandsOutput
    {
        public delegate byte[] BuildCmdLoadDelegate(string field);
        public delegate byte[] BuildCmdUploadDelegate(string field, string value, int size, bool factory = false);

        public Configuration(object sender, byte[] targetSign) : base(sender)
        {
            _targetSign = targetSign;
            buildCmdLoadDelegate += ConfigLoad;
            buildCmdUploadDelegate += ConfigUpload;
            through = false;
        }
        public Configuration(object sender, byte[] targetSign, byte[] throughSign) : base(sender)
        {
            _targetSign = targetSign;
            _throughSign = throughSign;
            buildCmdLoadDelegate += ConfigLoadThrough;
            buildCmdUploadDelegate += ConfigUploadThrough;
            through = true;
        }
        public byte[] _throughSign;
        public byte[] _targetSign;
        public BuildCmdLoadDelegate buildCmdLoadDelegate;
        public BuildCmdUploadDelegate buildCmdUploadDelegate;


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
        private byte[] FormationLoadConfig(byte[] field)
        {
            byte[] loadField = new byte[4 + field.Length];
            _targetSign.CopyTo(loadField, 0);
            ((ushort)CmdOutput.GET_CONFIG_FIELD).GetReverseBytes().CopyTo(loadField, 2);
            field.CopyTo(loadField, 4);
            return new CRC16_CCITT_FALSE().CRC_calc(loadField);
        }
        private byte[] FormationUploadConfig(byte[] field, byte[] value)
        {
            byte[] loadField = new byte[4 + field.Length + value.Length];
            _targetSign.CopyTo(loadField, 0);
            ((ushort)CmdOutput.SET_CONFIG).GetReverseBytes().CopyTo(loadField, 2);
            field.CopyTo(loadField, 4);
            value.CopyTo(loadField, 4 + field.Length);
            return new CRC16_CCITT_FALSE().CRC_calc(loadField);
        }

        public byte[] ConfigLoad(string field)
            => FormationLoadConfig(EncodeToKOI8R(field));
        public byte[] ConfigLoadThrough(string field)
            => CmdThroughRm(ConfigLoad(field), _throughSign, CmdOutput.ROUTING_THROUGH);

        public byte[] ConfigUpload(string field, string value, int size, bool factory = false)
            => FormationUploadConfig(EncodeToKOI8R(field), ValueToKOI8R(value, size, factory));
        public byte[] ConfigUploadThrough(string field, string value, int size, bool factory = false)
            => CmdThroughRm(ConfigUpload(field, value, size, factory), _throughSign, CmdOutput.ROUTING_THROUGH);

        public string GetSymbols(byte[] array) => CheckSymbols(array);
    }
}
using System.ComponentModel;

using ProtocolEnums;
using RMDebugger;
using CRC16;

namespace ConfigurationProtocol
{
    internal class Configuration : CommandsOutput
    {
        public delegate byte[] BuildCmdLoadDelegate(string field);
        public delegate byte[] BuildCmdUploadDelegate(string field, string value, int size);

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
        public bool factory { get; set; } = false;

        private byte[] ValueToKOI8R(string value, int size)
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
            ((ushort)CmdOutput.GET_CONFIG).GetReverseBytes().CopyTo(loadField, 2);
            field.CopyTo(loadField, 4);
            return new CRC16_CCITT_FALSE().CrcCalc(loadField);
        }
        private byte[] FormationUploadConfig(byte[] field, byte[] value)
        {
            byte[] loadField = new byte[4 + field.Length + value.Length];
            _targetSign.CopyTo(loadField, 0);
            ((ushort)CmdOutput.SET_CONFIG).GetReverseBytes().CopyTo(loadField, 2);
            field.CopyTo(loadField, 4);
            value.CopyTo(loadField, 4 + field.Length);
            return new CRC16_CCITT_FALSE().CrcCalc(loadField);
        }

        public byte[] ConfigLoad(string field)
            => FormationLoadConfig(EncodeToKOI8R(field));
        public byte[] ConfigLoadThrough(string field)
            => CmdThroughRm(ConfigLoad(field), _throughSign, CmdOutput.ROUTING_THROUGH);

        public byte[] ConfigUpload(string field, string value, int size)
            => FormationUploadConfig(EncodeToKOI8R(field), ValueToKOI8R(value, size));
        public byte[] ConfigUploadThrough(string field, string value, int size)
            => CmdThroughRm(ConfigUpload(field, value, size), _throughSign, CmdOutput.ROUTING_THROUGH);

        public string GetSymbols(byte[] array) => CheckSymbols(array);
    }
    internal class FieldConfiguration
    {
        public FieldConfiguration() { }
        private string SetToolTip(ConfigRule ccl)
        {
            switch (ccl)
            {
                case ConfigRule.NoRule: return "";
                case ConfigRule.uInt16: return "0-65535\n";
                case ConfigRule.len4: return "4 any symbols length\n";
                case ConfigRule.len16: return "16 any symbols length\n";
            }
            return null;
        }
        private ConfigRule SetRule(string name)
        {
            switch (name.ToLower())
            {
                case "addr":
                case "rmb":
                case "puid":
                    return ConfigRule.uInt16;
                case "fio":
                    return ConfigRule.len16;
                case "lamp":
                    return ConfigRule.len4;
                default: 
                    return ConfigRule.NoRule;
            }
        }
        private bool CheckRule(string value)
        {
            switch (rule)
            {
                case ConfigRule.uInt16:
                    return ushort.TryParse(value, out ushort _);
                case ConfigRule.len16:
                    return value.Length > 0 && value.Length <= 16;
                case ConfigRule.len4:
                    return value.Length > 0 && value.Length <= 4;
                case ConfigRule.NoRule: 
                    goto default; 
                default: 
                    return true;
            }
        }
        public void ClearValues()
        {
            uploadValue = string.Empty;
            loadValue = string.Empty;
        }
        [Browsable(false)]
        public string toolTip { get; set; } = "";

        private ConfigRule Rule = ConfigRule.NoRule;
        [Browsable(false)]
        public ConfigRule rule 
        {
            get => Rule;
            set
            {
                toolTip = SetToolTip(value);
                Rule = value;
            }
        }

        private string FieldName;
        public string fieldName {
            get => FieldName;
            set
            {
                if (string.IsNullOrEmpty(FieldName))
                {
                    rule = SetRule(value);
                    FieldName = value;
                }
            }
        }
        public bool fieldActive { get; set; }
        public string loadValue { get; set; }
        private string UploadValue;
        public string uploadValue 
        {
            get => UploadValue;
            set
            {
                if (string.IsNullOrEmpty(value) || CheckRule(value)) 
                    UploadValue = value;
            }
        }
    }
}
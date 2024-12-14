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
        public delegate byte[] BuildCmdGetFieldsDelegate(byte ix);

        public Configuration(object sender, byte[] targetSign) : base(sender)
        {
            _targetSign = targetSign;
            buildCmdLoadDelegate += ConfigLoad;
            buildCmdUploadDelegate += ConfigUpload;
            buildCmdGetFieldsDelegate += ConfigGetFields;
            through = false;
        }
        public Configuration(object sender, byte[] targetSign, byte[] throughSign) : base(sender)
        {
            _targetSign = targetSign;
            _throughSign = throughSign;
            buildCmdLoadDelegate += ConfigLoadThrough;
            buildCmdUploadDelegate += ConfigUploadThrough;
            buildCmdGetFieldsDelegate += ConfigGetFieldsThrough;
            through = true;
        }
        public byte[] _throughSign;
        public byte[] _targetSign;
        public BuildCmdLoadDelegate buildCmdLoadDelegate;
        public BuildCmdUploadDelegate buildCmdUploadDelegate;
        public BuildCmdGetFieldsDelegate buildCmdGetFieldsDelegate;

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
        private byte[] FormationGetFieldsByIX(byte ix)
        {
            byte[] loadField = new byte[5];
            _targetSign.CopyTo(loadField, 0);
            ((ushort)CmdOutput.GET_CONFIG_IX).GetReverseBytes().CopyTo(loadField, 2);
            new byte[] { ix }.CopyTo(loadField, 4);
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

        public byte[] ConfigGetFields(byte ix)
            => FormationGetFieldsByIX(ix);
        public byte[] ConfigGetFieldsThrough(byte ix)
            => CmdThroughRm(FormationGetFieldsByIX(ix), _throughSign, CmdOutput.ROUTING_THROUGH);

        public string GetSymbols(byte[] array) => CheckSymbols(array);
    }
    internal class FieldConfiguration
    {
        public FieldConfiguration() { }
        public FieldConfiguration(string name) => fieldName = name;
        private string SetUploadToolTip(ConfigRule ccl)
        {
            switch (ccl)
            {
                case ConfigRule.NoRule: return "";
                case ConfigRule.uInt16: return "0-65535\n";
                case ConfigRule.uInt8: return "0-255";
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
                case "ps_rise_threshold":
                    return ConfigRule.uInt16;
                case "tag_min_pwr":
                case "tag_pack_cnt":
                case "tag_ttl":
                case "ps_als_avg_cnt":
                case "als_drop_threshold":
                case "als_ps_delay":
                    return ConfigRule.uInt8;
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
                case ConfigRule.uInt8:
                    return uint.TryParse(value, out uint _);
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
        public string GetFieldToolTip()
            => fieldName switch {
                "tag_min_pwr" => "Порог мощности сигнала",
                "tag_pack_cnt" => "Количество пакетов, по которому определяется метка",
                "tag_ttl" => "Время жизни записи о метке",
                "ps_als_avg_cnt" => "Количество измерений, по которому вычисляется среднее значение датчика приближения и освещенности",
                "ps_rise_threshold" => "На сколько должны вырасти показания датчика приближения, чтобы это воспринималось как наличие объекта перед УРС",
                "als_drop_threshold" => "Во сколько раз должна упасть освещенность, относительно средней, чтобы вместе со срабатыванием датчика приближения это воспринималось как помещение объекта к УРС",
                "als_ps_delay" => "Сколько раз, после первого срабатывания, должно выполниться условие по датчику освещенности и приближения, чтобы изменилось сосояние с \"Объет далеко\" на \"Объет близко\"",
                _ => "" };
        public void ClearValues()
        {
            uploadValue = string.Empty;
            loadValue = string.Empty;
        }
        [Browsable(false)]
        public string uploadToolTip { get; set; } = "";
        [Browsable(false)]
        public string fieldToolTip { get; set; } = "";

        private ConfigRule Rule = ConfigRule.NoRule;
        [Browsable(false)]
        public ConfigRule rule 
        {
            get => Rule;
            set
            {
                uploadToolTip = SetUploadToolTip(value);
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
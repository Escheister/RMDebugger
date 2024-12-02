using System.Collections.Generic;
using System;

using ProtocolEnums;
using System.Reflection;
using StaticSettings;

namespace RMDebugger
{
    internal class Information : CommandsOutput
    {
        public delegate byte[] BuildCmdDelegate(CmdOutput cmdOutput);

        public Information(object sender, byte[] targetSign) : base(sender)
        {
            _targetSign = targetSign;
            buildCmdDelegate += BuildCmd;
        }
        public Information(object sender, byte[] targetSign, byte[] throughSign) : base(sender)
        {
            _targetSign = targetSign;
            _throughSign = throughSign;
            buildCmdDelegate += BuildCmdThrough;
        }

        private readonly byte[] _throughSign;
        private readonly byte[] _targetSign;

        public BuildCmdDelegate buildCmdDelegate;

        public byte[] BuildCmd(CmdOutput cmdOutput)
            => FormatCmdOut(_targetSign, cmdOutput, 0xff);
        public byte[] BuildCmdThrough(CmdOutput cmdOutput)
            => CmdThroughRm(BuildCmd(cmdOutput), _throughSign, CmdOutput.ROUTING_THROUGH);

        private byte[] ByteArrayParse(byte[] data, byte exception)
        {
            List<byte> result = new List<byte>();
            foreach(byte b in data)
            {
                if (b == exception) break;
                result.Add(b);
            }
            return result.ToArray();
        }

        public Dictionary<string, string> CmdInParse(byte[] cmdIn)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            DevType type = (DevType)cmdIn[5];
            UID cmd = (UID)cmdIn[4];
            data[$"{infoEnum.Type}"] = type.ToString();
            data[$"{infoEnum.Signature}"] = $"{(cmdIn[1] << 8) | cmdIn[0]}";
            data["Command"] = cmd.ToString();
            switch (type)
            {
                case DevType.RM485:
                    {
                        if (cmd == UID.GRAPH_WHO_ARE_YOU)
                        {
                            byte[] location = new byte[8];
                            Array.Copy(cmdIn, 6, location, 0, location.Length);
                            data[$"{infoEnum.Location}"] = CheckSymbols(ByteArrayParse(location, 0x00));
                            data["Zone"] = $"{(int)cmdIn[14]}";
                            switch ((int)cmdIn[14])
                            {
                                case 0: { data["Zone"] += "-Undefined"; break; }
                                case 1: { data["Zone"] += "-Ламповая"; break; }
                                case 2: { data["Zone"] += "-На поверхности"; break; }
                                case 3: { data["Zone"] += "-Под землей"; break; }
                                default: { data["Zone"] += "-???"; break; }
                            }
                            int delayPGLR = (cmdIn[cmdIn.Length - 5] << 8) | cmdIn[cmdIn.Length - 6];
                            data["PGLR Delay"] = $"{(delayPGLR == 65535 ? 0 : delayPGLR) } seconds";
                        }
                        else if (cmd == UID.STATUS)
                        {
                            int intTime = cmdIn[9] << 24 | cmdIn[8] << 16 | cmdIn[7] << 8 | cmdIn[6]; 
                            TimeSpan time = TimeSpan.FromSeconds(intTime);
                            data["Work Time"] = $"{(time.Days > 0 ? $"{time.Days}d " : "")}" +
                                                $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}";
                        }
                        data[$"{infoEnum.Version}"] = $"{(cmdIn[cmdIn.Length - 3] << 8) | cmdIn[cmdIn.Length - 4]}";
                        break; 
                    }
                case DevType.RMP:
                    {
                        if (cmd == UID.GRAPH_WHO_ARE_YOU)
                        {
                            byte[] lampID = new byte[4];
                            Array.Copy(cmdIn, 6, lampID, 0, lampID.Length);
                            byte[] fio = new byte[16];
                            Array.Copy(cmdIn, 12, fio, 0, fio.Length);
                            data["Lamp"] = CheckSymbols(ByteArrayParse(lampID, 0x00));
                            data["PUID"] = $"{(cmdIn[11] << 8) | cmdIn[10]}";
                            data[$"{infoEnum.Fio}"] = CheckSymbols(ByteArrayParse(fio, 0x00));
                        }
                        else if (cmd == UID.STATUS)
                        {
                            int intTime = cmdIn[9] << 24 | cmdIn[8] << 16 | cmdIn[7] << 8 | cmdIn[6];
                            TimeSpan time = TimeSpan.FromSeconds(intTime);
                            data["Work Time"] = $"{(time.Days > 0 ? $"{time.Days}d " : "")}" +
                                                $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}";
                            data["Battery"] = $"{(cmdIn[10] == 0 ? 0 : cmdIn[10] / 10)}v";
                            data["Notification status"] = $"{(int)cmdIn[11]}";
                            switch ((int)cmdIn[11])
                            {
                                case 0: { data["Notification status"] += "-Nothing"; break; }
                                case 1: { data["Notification status"] += "-Not received"; break; }
                                case 2: { data["Notification status"] += "-Received"; break; }
                                case 3: { data["Notification status"] += "-Notification"; break; }
                                case 4: { data["Notification status"] += "-Accepted"; break; }
                                case 5: { data["Notification status"] += "-Tout"; break; }
                                case 6: { data["Notification status"] += "-Stop"; break; }
                                case 7: { data["Notification status"] += "-SOS"; break; }
                                default: { data["Notification status"] += "-???"; break; }
                            }
                            data["Notification ID"] = $"{(int)cmdIn[12]}";
                            switch ((int)cmdIn[12])
                            {
                                case 0: { data["Notification ID"] += "-Nothing"; break; }
                                case 1: { data["Notification ID"] += "-Unique"; break; }
                                case 2: { data["Notification ID"] += "-Test"; break; }
                                case 3: { data["Notification ID"] += "-Alarm"; break; }
                                default: { data["Notification ID"] += "-???"; break; }
                            }
                            data[$"{infoEnum.Version}"] = $"{(cmdIn[cmdIn.Length - 3] << 8) | cmdIn[cmdIn.Length - 4]}";
                        }
                        break;
                    }
                case DevType.RMG:
                    {
                        if (cmd == UID.GRAPH_WHO_ARE_YOU)
                        {
                            byte[] location = new byte[8];
                            Array.Copy(cmdIn, 6, location, 0, location.Length);
                            data[$"{infoEnum.Location}"] = CheckSymbols(ByteArrayParse(location, 0x00));
                            data[$"{infoEnum.Version}"] = $"{(cmdIn[cmdIn.Length - 3] << 8) | cmdIn[cmdIn.Length - 4]}";
                        }
                        else if (cmd == UID.STATUS)
                        {
                            int intTime = cmdIn[9] << 24 | cmdIn[8] << 16 | cmdIn[7] << 8 | cmdIn[6];
                            TimeSpan time = TimeSpan.FromSeconds(intTime);
                            data["Time from gas"] = $"{(time.Days > 0 ? $"{time.Days}d " : "")}" +
                                                $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}";

                            data["Sign in gas"] = $"{cmdIn[11] << 8 | cmdIn[10]}";
                            data["Serial gas"] = $"{cmdIn[13] << 8 | cmdIn[12]}";
                            data["(???)Type gas"] = $"{cmdIn[14]}"; // ???
                            data["Battery"] = $"{cmdIn[15]}%";
                            data["States"] = (cmdIn[17] & 0b0000_0001) != 0 ? "S1:Charging | " : "";
                            data["States"] += (cmdIn[17] & 0b1000_0000) != 0 ? "S8:Time from NPC ATB" : "";
                            string GetSensorType(byte b, int valIn, out string valOut)
                            {
                                string sensor = "";
                                switch (b / 16)
                                {
                                    case 0:
                                        valOut = "Zero";
                                        return "No sensor";
                                    case 1:
                                        valOut = $"{valIn / 100}.{valIn % 100:00} %";
                                        sensor = "CH4";
                                        break;
                                    case 2:
                                        valOut = $"{valIn / 10}.{valIn % 10} %";
                                        sensor = "O2";
                                        break;
                                    case 3:
                                        valOut = $"{valIn} ppm";
                                        sensor = "CO";
                                        break;
                                    case 4:
                                        valOut = $"{valIn} %";
                                        sensor = "CO2";
                                        break;
                                    case 5:
                                        valOut = $"{valIn}";
                                        sensor = "H2S";
                                        break;
                                    case 6:
                                        valOut = $"{valIn}";
                                        sensor = "NO";
                                        break;
                                    case 7:
                                        valOut = $"{valIn}";
                                        sensor = "NO2";
                                        break;
                                    case 8:
                                        valOut = $"{valIn}";
                                        sensor = "PID";
                                        break;
                                    default:
                                        valOut = "Error";
                                        return string.Empty;
                                }
                                sensor += ((b % 16) & 0b0001) != 0 ? " | Level 2" : "";
                                sensor += ((b % 16) & 0b0010) != 0 ? " | Level 1" : "";
                                sensor += ((b % 16) & 0b0100) != 0 ? " | Error" : "";
                                sensor += ((b % 16) & 0b1000) != 0 ? " | Reserv" : "";
                                return sensor;
                            }
                            string ch1 = GetSensorType(cmdIn[18], cmdIn[20] << 8 | cmdIn[19], out string ch1Val);
                            string ch2 = GetSensorType(cmdIn[21], cmdIn[23] << 8 | cmdIn[22], out string ch2Val);
                            string ch3 = GetSensorType(cmdIn[24], cmdIn[26] << 8 | cmdIn[25], out string ch3Val);
                            string ch4 = GetSensorType(cmdIn[27], cmdIn[29] << 8 | cmdIn[28], out string ch4Val);


                            data["CH1 info"] = $"Sensor type:{(ch1 != string.Empty ? $"{ch1}" : "Error")}";
                            data["CH1 Value"] = $"{ch1Val}";
                            data["CH2 info"] = $"Sensor type:{(ch2 != string.Empty ? $"{ch2}" : "Error")}";
                            data["CH2 Value"] = $"{ch2Val}";
                            data["CH3 info"] = $"Sensor type:{(ch3 != string.Empty ? $"{ch3}" : "Error")}";
                            data["CH3 Value"] = $"{ch3Val}";
                            data["CH4 info"] = $"Sensor type:{(ch4 != string.Empty ? $"{ch4}" : "Error")}";
                            data["CH4 Value"] = $"{ch4Val}";
                        }
                        break;
                    }
                case DevType.RMH:
                    {
                        if (cmd == UID.GRAPH_WHO_ARE_YOU)
                        {
                            byte[] location = new byte[16];
                            Array.Copy(cmdIn, 6, location, 0, location.Length);
                            data[$"{infoEnum.Location}"] = CheckSymbols(ByteArrayParse(location, 0x00));
                        }
                        else if (cmd == UID.STATUS)
                            goto case DevType.RM485;
                        data[$"{infoEnum.Version}"] = $"{(cmdIn[cmdIn.Length - 3] << 8) | cmdIn[cmdIn.Length - 4]}";
                        break;
                    }
                case DevType.RMTA:
                    {
                        if (cmd == UID.GRAPH_WHO_ARE_YOU)
                        {
                            data["Type inside device"] = $"{(DevType)cmdIn[6]}";
                            byte[] location = new byte[16];
                            Array.Copy(cmdIn, 7, location, 0, location.Length);
                            location = ByteArrayParse(location, 0x00);
                            data[$"{infoEnum.Location}"] = CheckSymbols(location);
                            data[$"{infoEnum.Version}"] = $"{(cmdIn[cmdIn.Length - 3] << 8) | cmdIn[cmdIn.Length - 4]}";
                        }
                        else if (cmd == UID.STATUS)
                        {
                            int intTime = cmdIn[9] << 24 | cmdIn[8] << 16 | cmdIn[7] << 8 | cmdIn[6];
                            TimeSpan time = TimeSpan.FromSeconds(intTime);
                            data["Work Time"] = $"{time.Days}d " +
                                $"{time.Hours}h " +
                                $": {time.Minutes}m " +
                                $": {time.Seconds}s";
                            data["Status"] = $"{cmdIn[10]}";
                            data["Battery"] = $"{(cmdIn[11] == 0 ? 0 : cmdIn[11] / 10)}v";
                        }
                        break;
                    }
                default:
                    {
                        data["eRrOr"] = $"--------------------";
                        data["Status"] = $"stay calm please, {type} is online, but";
                        data["Information"] = $"this function coming soon for this device";
                        data["ErRoR"] = $"--------------------";
                        break;
                    }
            }
            return data;
        }
    }
    class InformationData
    {
        public InformationData() { }
        public bool haveDataForCSV { get; set; } = false;
        public string[] GetFieldsValue()
            => new string[7] { Type, Signature, Version, Radio, Location, Fio, Date };

        private string _type;
        public string Type 
        {
            get => _type;
            set
            {
                _type = value;
                haveDataForCSV = value != "";
            } 
        }
        private string _sig;
        public string Signature
        {
            get => _sig;
            set
            {
                _sig = value;
                haveDataForCSV = value != "";
            }
        }
        private string _ver;
        public string Version
        {
            get => _ver;
            set
            {
                _ver = value;
                haveDataForCSV = value != "";
            }
        }
        private string _loc;
        public string Location
        {
            get => _loc;
            set
            {
                _loc = value;
                haveDataForCSV = value != "";
            }
        }
        private string _fio;
        public string Fio
        {
            get => _fio;
            set
            {
                _fio = value;
                haveDataForCSV = value != "";
            }
        }
        public string Radio { get; set; } = "None";
        public string Date { get; set; }
    }
}

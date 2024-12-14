using System.Collections.Generic;
using System;

using ProtocolEnums;
using System.Text;
using System.Linq;

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

        public bool RM485Info(byte[] cmdIn, out Dictionary<string, string> data)
        {
            data = new Dictionary<string, string>();
            RM485_UID cmd = (RM485_UID)cmdIn[4];
            data["Command"] = cmd.ToString();
            switch (cmd)
            {
                case RM485_UID.Who_are_you:
                    byte[] location = new byte[8];
                    Array.Copy(cmdIn, 6, location, 0, location.Length);
                    data[$"{infoEnum.Location}"] = CheckSymbols(ByteArrayParse(location, 0x00));
                    data["Zone"] = (int)cmdIn[14] switch {
                        0 => $"{cmdIn[14]}-Undefined",
                        1 => $"{cmdIn[14]}-Ламповая",
                        2 => $"{cmdIn[14]}-На поверхности",
                        3 => $"{cmdIn[14]}-Под землей",
                        _ => $"{cmdIn[14]}-???" };
                    ushort delayPGLR = (ushort)((cmdIn[cmdIn.Length - 5] << 8) | cmdIn[cmdIn.Length - 6]);
                    data["PGLR Delay"] = $"{(delayPGLR == 65535 ? 0 : delayPGLR)} seconds";
                    data[$"{infoEnum.Version}"] = $"{(cmdIn[cmdIn.Length - 3] << 8) | cmdIn[cmdIn.Length - 4]}";
                    break;
                case RM485_UID.Status:
                    int intTime = cmdIn[9] << 24 | cmdIn[8] << 16 | cmdIn[7] << 8 | cmdIn[6];
                    TimeSpan time = TimeSpan.FromSeconds(intTime);
                    data["Work Time"] = $"{(time.Days > 0 ? $"{time.Days}d " : "")}" +
                                        $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}";
                    data[$"{infoEnum.Version}"] = $"{(cmdIn[cmdIn.Length - 3] << 8) | cmdIn[cmdIn.Length - 4]}";
                    break;
                /*case RM485_UID.Mode:
                    break;*/
                default:
                    data["Command"] = $"{cmd} | Unsupported right now...";
                    return false;
            }
            return true;
        }
        public bool RMPInfo(byte[] cmdIn, out Dictionary<string, string> data)
        {
            data = new Dictionary<string, string>();
            RMP_UID cmd = (RMP_UID)cmdIn[4];
            data["Command"] = cmd.ToString();
            switch (cmd)
            {
                case RMP_UID.Who_are_you:
                    byte[] lampID = new byte[4];
                    Array.Copy(cmdIn, 6, lampID, 0, lampID.Length);
                    byte[] fio = new byte[16];
                    Array.Copy(cmdIn, 12, fio, 0, fio.Length);
                    data["Lamp"] = CheckSymbols(ByteArrayParse(lampID, 0x00));
                    data["PUID"] = $"{(cmdIn[11] << 8) | cmdIn[10]}";
                    data[$"{infoEnum.Fio}"] = CheckSymbols(ByteArrayParse(fio, 0x00));
                    break;
                case RMP_UID.Status:
                    int intTime = cmdIn[9] << 24 | cmdIn[8] << 16 | cmdIn[7] << 8 | cmdIn[6];
                    TimeSpan time = TimeSpan.FromSeconds(intTime);
                    data["Work Time"] = $"{(time.Days > 0 ? $"{time.Days}d " : "")}" +
                                        $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}";
                    data["Battery"] = $"{cmdIn[10] / 10}.{cmdIn[10] % 10}v";
                    data["Notification status"] = (int)cmdIn[11] switch
                    {
                        0 => $"{cmdIn[11]}-Nothing",
                        1 => $"{cmdIn[11]}-Not received",
                        2 => $"{cmdIn[11]}-Received",
                        3 => $"{cmdIn[11]}-Notification",
                        4 => $"{cmdIn[11]}-Accepted",
                        5 => $"{cmdIn[11]}-Tout",
                        6 => $"{cmdIn[11]}-Stop",
                        7 => $"{cmdIn[11]}-SOS",
                        _ => $"{cmdIn[11]}-???"
                    };
                    data["Notification ID"] = (int)cmdIn[12] switch
                    {
                        0 => $"{cmdIn[11]}-Nothing",
                        1 => $"{cmdIn[11]}-Unique",
                        2 => $"{cmdIn[11]}-Test",
                        3 => $"{cmdIn[11]}-Alarm",
                        _ => $"{cmdIn[11]}-???"
                    };
                    data[$"{infoEnum.Version}"] = $"{(cmdIn[cmdIn.Length - 3] << 8) | cmdIn[cmdIn.Length - 4]}";
                    break;
                /*case RMP_UID.Mode:
                    break;*/
                default:
                    data["Command"] = $"{cmd} | Unsupported right now...";
                    return false;
            }
            return true;
        }
        public bool RMGInfo(byte[] cmdIn, out Dictionary<string, string> data)
        {
            string GetRmgATestType(int type) => 
                type switch {   0 => "2M1",  1 => "2MU1", 2 => "2M2",
                                3 => "2MU2", 4 => "2D1",  5 => "2D2",
                                6 => "2D3",  7 => "2C1",  8 => "2CU1",                 
                                9 => "2C2", 10 => "2CU2", _ => "Error" };
            string GetRmgChanelState(int b)
            {
                if ((b & 0b0001) != 0) return " | Level 2";
                else if ((b & 0b0010) != 0) return " | Level 1";
                else if ((b & 0b0100) != 0) return " | Error";
                else if ((b & 0b1000) != 0) return " | Reserv";
                else return "";
            }
            string GetRmgSensorValue(int b, int valIn) => 
                b switch {  0 => "No sensor",
                            1 => $"{valIn / 100}.{valIn % 100:00} %",
                            2 => $"{valIn / 10}.{valIn % 10} %",
                            3 => $"{valIn} ppm",
                            4 => $"{valIn} ppm",
                            5 => $"{valIn} ppm",
                            6 => $"{valIn} ppm",
                            7 => $"{valIn} ppm",
                            8 => $"{valIn} ppm",
                            _ => "Error" };
            string GetRmgATestSensorValue(int b, int valIn) => 
                b switch {  0 => "No sensor",
                            1 => $"{valIn / 100}.{valIn % 100:00} %",
                            2 => $"{valIn / 10}.{valIn % 10} %",
                            3 => $"{valIn} ppm",
                            4 => $"{valIn / 100}.{valIn % 100:00} %",
                            5 => $"{valIn / 10}.{valIn % 10} ppm",
                            6 => $"{valIn / 10}.{valIn % 10} ppm",
                            7 => $"{valIn / 10}.{valIn % 10} ppm",
                            14 => $"{valIn} mmHg",
                            15 => $"{valIn} C",
                            _ => "Error" };

            data = new Dictionary<string, string>();
            RMG_UID cmd = (RMG_UID)cmdIn[4];
            data["Command"] = cmd.ToString();
            switch (cmd)
            {
                case RMG_UID.Who_are_you:
                    byte[] location = new byte[8];
                    Array.Copy(cmdIn, 6, location, 0, location.Length);
                    data[$"{infoEnum.Location}"] = CheckSymbols(ByteArrayParse(location, 0x00));
                    data[$"{infoEnum.Version}"] = $"{(cmdIn[cmdIn.Length - 3] << 8) | cmdIn[cmdIn.Length - 4]}";
                    break;
                case RMG_UID.Status:
                    TimeSpan time = TimeSpan.FromSeconds(cmdIn[9] << 24 | cmdIn[8] << 16 | cmdIn[7] << 8 | cmdIn[6]);
                    data["Time from gas"] = $"{(time.Days > 0 ? $"{time.Days}d " : "")}" +
                                        $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}";

                    data["Sign in gas"] = $"{cmdIn[11] << 8 | cmdIn[10]}";
                    data["Serial gas"] = $"{cmdIn[13] << 8 | cmdIn[12]}";
                    data["(???)Type gas"] = $"{cmdIn[14]}"; // ???
                    data["Battery"] = $"{cmdIn[15]}%";
                    data["States"] = (cmdIn[17] & 0b0000_0001) != 0 ? "S1:Charging | " : "";
                    data["States"] += (cmdIn[17] & 0b1000_0000) != 0 ? "S8:Time from NPC ATB" : "";

                    for(int i = 18, ch = 1; ch <= 4; ch++)
                    {
                        data[$"CH{ch} info"] = $"Sensor type: {(RMG_SensorType)(cmdIn[i] / 16)}{GetRmgChanelState(cmdIn[i] % 16)}";
                        data[$"CH{ch} value"] = GetRmgSensorValue(cmdIn[i] / 16, cmdIn[i+2] << 8 | cmdIn[i+1]);
                        i += 3;
                    }
                    break;
                case RMG_UID.Who_are_you_ATest:
                    data["Type gas"] = GetRmgATestType(cmdIn[7] << 8 | cmdIn[6]);
                    data["Serial gas"] = $"{cmdIn[11] << 12 | cmdIn[10] << 8 | cmdIn[9]}-{cmdIn[8]}";
                    byte[] verGA = new byte[16];
                    Array.Copy(cmdIn, 12, verGA, 0, verGA.Length);
                    data["Version gas"] = Encoding.GetEncoding("ascii").GetString(verGA.Reverse().ToArray());
                    byte[] numGA = new byte[4];
                    Array.Copy(cmdIn, 28, verGA, 0, numGA.Length);
                    data["Num gas"] = Encoding.GetEncoding("koi8r").GetString(numGA.Reverse().ToArray());
                    data[$"{infoEnum.Version}"] = $"{(cmdIn[cmdIn.Length - 3] << 8) | cmdIn[cmdIn.Length - 4]}";
                    break;
                case RMG_UID.Status_ATest:
                    data["Time in gas"] = $"{2000 + cmdIn[6]}.{cmdIn[7]}.{cmdIn[8]} {cmdIn[9]}:{cmdIn[10]}:{cmdIn[11]}";
                    data["Sign in gas"] = $"{cmdIn[13] << 8 | cmdIn[12]}";
                    data["Serial gas"] = $"{cmdIn[17] << 12 | cmdIn[16] << 8 | cmdIn[15]}-{cmdIn[14]}";
                    data["Type gas"] = GetRmgATestType(cmdIn[19] << 8 | cmdIn[18]);
                    int batVoltage = cmdIn[21] << 8 | cmdIn[20];
                    data["Battery"] = $"{batVoltage / 100}.{batVoltage % 100}v";

                    int state = cmdIn[23] << 8 | cmdIn[22];
                    data["States"] = (state & 0b0000_0000_0001) != 0 ? "S0:Saving in LOG RMG | " : "";
                    data["States"] += (state & 0b0000_0000_0010) != 0 ? "S1:Setting mode | " : "";
                    data["States"] += (state & 0b0000_0000_0100) != 0 ? "S2:Low power voltage | " : "";
                    data["States"] += (state & 0b0001_0000_0000) != 0 ? "S8:Charging | " : "";
                    data["States"] += (state & 0b0010_0000_0000) != 0 ? "S9:Warming up of sensors | " : "";
                    data["States"] += (state & 0b0100_0000_0000) != 0 ? "S10:Low temp, sensors are off | " : "";

                    for (int i = 24, ch = 1; ch <= 6; ch++)
                    {
                        data[$"CH{ch} info"] = $"Sensor type: {(RMG_ATestSensorType)(cmdIn[i] / 16)}{GetRmgChanelState(cmdIn[i] % 16)}";
                        data[$"CH{ch} value"] = GetRmgATestSensorValue(cmdIn[i] / 16, cmdIn[i + 2] << 8 | cmdIn[i + 1]);
                        i += 3;
                    }
                    break;
                /*case RMG_UID.Mode:
                    break;*/
                default:
                    data["Command"] = $"{cmd} | Unsupported right now...";
                    return false;
            }
            return true;
        }
        public bool RMHInfo(byte[] cmdIn, out Dictionary<string, string> data)
        {
            data = new Dictionary<string, string>();
            RMH_UID cmd = (RMH_UID)cmdIn[4];
            data["Command"] = cmd.ToString();
            switch (cmd)
            {
                case RMH_UID.Who_are_you:
                    byte[] location = new byte[16];
                    Array.Copy(cmdIn, 6, location, 0, location.Length);
                    data[$"{infoEnum.Location}"] = CheckSymbols(ByteArrayParse(location, 0x00));
                    data[$"{infoEnum.Version}"] = $"{(cmdIn[cmdIn.Length - 3] << 8) | cmdIn[cmdIn.Length - 4]}";
                    break;
                case RMH_UID.Status:
                    int intTime = cmdIn[9] << 24 | cmdIn[8] << 16 | cmdIn[7] << 8 | cmdIn[6];
                    TimeSpan time = TimeSpan.FromSeconds(intTime);
                    data["Work Time"] = $"{(time.Days > 0 ? $"{time.Days}d " : "")}" +
                                        $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}";
                    data[$"{infoEnum.Version}"] = $"{(cmdIn[cmdIn.Length - 3] << 8) | cmdIn[cmdIn.Length - 4]}";
                    break;
                /*case RMH_UID.Mode:
                    break;*/
                default:
                    data["Command"] = $"{cmd} | Unsupported right now...";
                    return false;
            }
            return true;
        }
        public bool RMTAInfo(byte[] cmdIn, out Dictionary<string, string> data)
        {
            data = new Dictionary<string, string>();
            RMTA_UID cmd = (RMTA_UID)cmdIn[4];
            data["Command"] = cmd.ToString();
            switch (cmd)
            {
                case RMTA_UID.Who_are_you:
                    data["Type inside device"] = $"{(DevType)cmdIn[6]}";
                    byte[] location = new byte[16];
                    Array.Copy(cmdIn, 7, location, 0, location.Length);
                    location = ByteArrayParse(location, 0x00);
                    data[$"{infoEnum.Location}"] = CheckSymbols(location);
                    data[$"{infoEnum.Version}"] = $"{(cmdIn[cmdIn.Length - 3] << 8) | cmdIn[cmdIn.Length - 4]}";
                    break;
                case RMTA_UID.Status:
                    int intTime = cmdIn[9] << 24 | cmdIn[8] << 16 | cmdIn[7] << 8 | cmdIn[6];
                    TimeSpan time = TimeSpan.FromSeconds(intTime);
                    data["Work Time"] = $"{time.Days}d " +
                        $"{time.Hours}h " +
                        $": {time.Minutes}m " +
                        $": {time.Seconds}s";
                    data["Status"] = $"{cmdIn[10]}";
                    data["Battery"] = $"{(cmdIn[11] == 0 ? 0 : cmdIn[11] * 10)}v";
                    break;
                /*case RMTA_UID.Mode:
                    break;*/
                default:
                    data["Command"] = $"{cmd} | Unsupported right now...";
                    return false;
            }
            return true;
        }
        public Dictionary<string, string> CmdInParse(byte[] cmdIn)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            DevType type = (DevType)cmdIn[5];
            data[$"{infoEnum.Type}"] = type.ToString();
            data[$"{infoEnum.Signature}"] = $"{(cmdIn[1] << 8) | cmdIn[0]}";
            Dictionary<string, string> devInfo = new Dictionary<string, string>(); ;
            switch (type)
            {
                case DevType.RM485:
                    if (RM485Info(cmdIn, out devInfo)) break;
                    else goto default;
                case DevType.RMP:
                    if (RMPInfo(cmdIn, out devInfo)) break;
                    else goto default;
                case DevType.RMG:
                    if (RMGInfo(cmdIn, out devInfo)) break;
                    else goto default;
                case DevType.RMH:
                    if (RMHInfo(cmdIn, out devInfo)) break;
                    else goto default;
                case DevType.RMTA:
                    if (RMTAInfo(cmdIn, out devInfo)) break;
                    else goto default;
                default:
                    {
                        data["eRrOr"] = $"--------------------";
                        data["Status"] = $"stay calm please, {type} is online, but";
                        data["Information"] = $"this function coming soon for this device";
                        data["ErRoR"] = $"--------------------";
                        break;
                    }
            }
            data = data.Concat(devInfo).ToDictionary(x => x.Key, x => x.Value);
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
        public string Version { get; set; }
        public string Location { get; set; }
        public string Fio { get; set; }
        public string Radio { get; set; } = "None";
        public string Date { get; set; }
    }
}

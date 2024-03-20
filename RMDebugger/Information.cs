using System.Collections.Generic;
using System.Net.Sockets;
using System.IO.Ports;
using System;

using SearchProtocol;
using ProtocolEnums;
using StaticMethods;

namespace RMDebugger
{
    internal class Information : Searching
    {
        public Information(SerialPort com, Socket sock) : base(com, sock) { }

        public byte[] GetInfo(byte[] rmSign, CmdOutput cmdOutput)
            => FormatCmdOut(rmSign, cmdOutput, 0xff);
        public byte[] GetInfo(byte[] rmSign, byte[] rmThrough, CmdOutput cmdOutput)
            => CmdThroughRm(GetInfo(rmSign, cmdOutput), rmThrough, CmdOutput.ROUTING_THROUGH);

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
            data["Signature"] = $"{(cmdIn[1] << 8) | cmdIn[0]}";
            data["Type"] = type.ToString();
            data["Command"] = cmd.ToString();
            switch (type)
            {
                case DevType.RM485:
                    {
                        if (cmd == UID.WHO_ARE_YOU)
                        {
                            byte[] location = new byte[8];
                            Array.Copy(cmdIn, 6, location, 0, location.Length);
                            data["Location"] = Methods.CheckSymbols(ByteArrayParse(location, 0x00));
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
                            data["Work Time"] = $"{time.Days}d " +
                                $"{time.Hours}h " +
                                $": {time.Minutes}m " +
                                $": {time.Seconds}s";
                        }
                        data["Version"] = $"{(cmdIn[cmdIn.Length - 3] << 8) | cmdIn[cmdIn.Length - 4]}";
                        break; 
                    }
                case DevType.RMP:
                    {
                        if (cmd == UID.WHO_ARE_YOU)
                        {
                            byte[] lampID = new byte[4];
                            Array.Copy(cmdIn, 6, lampID, 0, lampID.Length);
                            byte[] fio = new byte[16];
                            Array.Copy(cmdIn, 12, fio, 0, fio.Length);
                            data["Lamp"] = Methods.CheckSymbols(ByteArrayParse(lampID, 0x00));
                            data["PUID"] = $"{(cmdIn[11] << 8) | cmdIn[10]}";
                            data["Fio"] = Methods.CheckSymbols(ByteArrayParse(fio, 0x00));
                        }
                        else if (cmd == UID.STATUS)
                        {
                            int intTime = cmdIn[9] << 24 | cmdIn[8] << 16 | cmdIn[7] << 8 | cmdIn[6];
                            TimeSpan time = TimeSpan.FromSeconds(intTime);
                            data["Work Time"] = $"{time.Days}d " +
                                $"{time.Hours}h " +
                                $": {time.Minutes}m " +
                                $": {time.Seconds}s";
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
                            data["Version"] = $"{(cmdIn[cmdIn.Length - 3] << 8) | cmdIn[cmdIn.Length - 4]}";
                        }
                        break;
                    }
                /*case DevType.RMG: 
                    {
                        break; 
                    }*/
                case DevType.RMH:
                    {
                        if (cmd == UID.WHO_ARE_YOU)
                        {
                            byte[] location = new byte[16];
                            Array.Copy(cmdIn, 6, location, 0, location.Length);
                            data["Location"] = Methods.CheckSymbols(ByteArrayParse(location, 0x00));
                        }
                        else if (cmd == UID.STATUS)
                            goto case DevType.RM485;
                        data["Version"] = $"{(cmdIn[cmdIn.Length - 3] << 8) | cmdIn[cmdIn.Length - 4]}";
                        break;
                    }
                case DevType.RMTA:
                    {
                        if (cmd == UID.WHO_ARE_YOU)
                        {
                            data["Type inside device"] = $"{(DevType)cmdIn[6]}";
                            byte[] location = new byte[16];
                            Array.Copy(cmdIn, 7, location, 0, location.Length);
                            location = ByteArrayParse(location, 0x00);
                            data["Location"] = Methods.CheckSymbols(location);
                            data["Version"] = $"{(cmdIn[cmdIn.Length - 3] << 8) | cmdIn[cmdIn.Length - 4]}";
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
}

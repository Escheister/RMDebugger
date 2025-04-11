using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System;

using System.Drawing;
using SearchProtocol;
using StaticSettings;
using ProtocolEnums;
using CRC16;


namespace RMDebugger
{
    internal class ForTests : Searching
    {
        public delegate byte[] BuildCmdDelegate(CmdOutput cmdOutput);
        public ForTests(object sender, List<DeviceClass> _listDeviceClass) : base(sender) => listDeviceClass = _listDeviceClass;
        public delegate void TestDebugEvent(string msg, ProtocolReply reply);
        public event TestDebugEvent TestDebug;
        public delegate void TimeRefreshingEvent();
        public event TimeRefreshingEvent TimeRefresh;
        private List<DeviceClass> listDeviceClass;
        public List<DeviceClass> ListDeviceClass { get => listDeviceClass; }
        public bool clearAfterError { get; set; }

        async public override Task<Tuple<byte[], ProtocolReply>> GetData(byte[] cmdOut, int size, int ms = 250)
        {
            if (!Options.mainIsAvailable) throw new Exception("No interface");
            Options.activeToken.Token.ThrowIfCancellationRequested();
            sendData(cmdOut);
            string message = $"{DateTime.Now:dd.HH:mm:ss:fff} : {"send",-6}-> {cmdOut.GetStringOfBytes()}\n";
            Enum.TryParse(Enum.GetName(typeof(CmdOutput), (CmdOutput)((cmdOut[2] << 8) | cmdOut[3])), out CmdInput cmdMain);
            byte[] cmdIn = await receiveData(size, ms);
            ProtocolReply reply = GetReply(cmdIn, new byte[2] { cmdOut[0], cmdOut[1] }, cmdMain);
            message += $"{DateTime.Now:dd.HH:mm:ss:fff} : {reply,-6}<- {cmdIn.GetStringOfBytes()}\n";
            TestDebug?.BeginInvoke(message, reply, null, null);
            TimeRefresh?.Invoke();
            return new Tuple<byte[], ProtocolReply>(cmdIn, reply);
        }

        protected override ProtocolReply GetReply(byte[] bufferIn, byte[] rmSign, CmdInput cmdMain)
        {
            if (bufferIn.Length == 0) return ProtocolReply.Null;
            if (!CRC16_CCITT_FALSE.CrcCheck(bufferIn)) return ProtocolReply.WCrc;
            if (!SignatureEqual(bufferIn, rmSign)) return ProtocolReply.WSign;
            if (!CmdInputEqual(bufferIn, cmdMain)) return ProtocolReply.WCmd;
            return ProtocolReply.Ok;
        }

        private int GetSizeCMD(CmdOutput cmdOutput, DevType devType)
        {
            switch (devType)
            {
                case DevType.RM485:
                    {
                        Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOutput), out RM485MaxSize rm485);
                        return (int)rm485;
                    }
                case DevType.RMH:
                    {
                        Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOutput), out RMHMaxSize RMH);
                        return (int)RMH;
                    }
                default:
                    {
                        Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOutput), out CmdMaxSize cmdMaxSize);
                        return (int)cmdMaxSize;
                    }
            }
        }
        private bool AddValueToDevice(DeviceClass device, ProtocolReply reply)
        {
            switch (reply)
            {
                case ProtocolReply.Ok: return true;
                case ProtocolReply.Null:
                    device.devNoReply++;
                    break;
                case ProtocolReply.WCrc:
                    device.devBadCRC++;
                    break;
                case ProtocolReply.WCmd:
                case ProtocolReply.WSign:
                case ProtocolReply.WData:
                    device.devBadReply++;
                    break;
            }
            if (clearAfterError) clearBuffer();
            return false;
        }

        async public Task GetDataFromDevice(DeviceClass device, CmdOutput cmdOutput)
        {
            Tuple <byte[], ProtocolReply> reply = await GetData(
                FormatCmdOut(device.devSign.GetBytes(), cmdOutput, 0xff), 
                GetSizeCMD(cmdOutput, device.devType), 50);
            device.devTx++;
            if (!AddValueToDevice(device, reply.Item2)) return;
            if (cmdOutput == CmdOutput.STATUS)
            {
                try
                {
                    TimeSpan time = TimeSpan.FromSeconds(
                                      reply.Item1[reply.Item1.Length - 5] << 24
                                    | reply.Item1[reply.Item1.Length - 6] << 16
                                    | reply.Item1[reply.Item1.Length - 7] << 8
                                    | reply.Item1[reply.Item1.Length - 8]);

                    device.devWorkTime = $"{(time.Days > 0 ? $"{time.Days}d " : "")}" +
                                         $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}";
                }
                catch
                {
                    AddValueToDevice(device, ProtocolReply.WData);
                    return; 
                }
            }
            device.devRx++;
        }

        async public Task GetRadioDataFromDevice(DeviceClass device, CmdOutput cmdOutput)
        {
            List<DeviceData> deviceDataList = new List<DeviceData>();
            Tuple<byte[], ProtocolReply> reply;
            byte ix = 0x00;
            byte iteration = 1;
            Enum.TryParse(Enum.GetName(typeof(CmdOutput), cmdOutput), out CmdMaxSize cmdOutSize);
            do
            {
                byte[] cmdOut = FormatCmdOut(device.devSign.GetBytes(), cmdOutput, ix);
                reply = await GetData(cmdOut, (int)cmdOutSize, 50);
                device.devTx++;
                if (!AddValueToDevice(device, reply.Item2)) break;
                try
                {
                    deviceDataList.AddRange(GetDataDevices(cmdOutput, reply.Item1, out ix));
                    iteration++;
                    device.devRx++;
                }
                catch (OperationCanceledException) { return; }
                catch
                {
                    AddValueToDevice(device, ProtocolReply.WData);
                }
            }
            while (ix != 0x00 && iteration <= 5);
            if (cmdOutput == CmdOutput.GRAPH_GET_NEAR)
            {
                device.devNearbyDevs = deviceDataList.Count;
                if (device.devNearbyDevs == 0)
                {
                    device.devBadRadio++;
                    device.devRx--;
                }
            }
        }
    }

    internal class DeviceClass
    {
        public DeviceClass() { }
        public object this[string propertyName]
        {
            get
            {
                Type myType = typeof(DeviceClass);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return myPropInfo.GetValue(this, null);
            }
        }
        public void Reset()
        {
            DeviceRx = 0;
            devTx = 0;
            devBadCRC = 0;
            devBadRadio = 0;
            devBadReply = 0;
            devNoReply = 0;
            devNearbyDevs = 0;
            devWorkTime = string.Empty;
            devStatus = string.Empty;
        }
        public Color statusColor;
        public string devInterface { get; set; }
        public int devSign { get; set; }
        public DevType devType { get; set; }
        public string devStatus { get; set; }
        public int devTx { get; set; }
        private double SetPercentErrors()
        {
            try {
                return 100.000 - (100.000 * DeviceRx / devTx);
            } catch (DivideByZeroException) {
                return 0;
            }
        }
        private int DeviceRx;
        public int devRx 
        { 
            get => DeviceRx;
            set
            {
                DeviceRx = value;
                devPercentErrors = SetPercentErrors();
                devStatus = devPercentErrors >= 1.000 ? "Bad" : "Good";
            }
        }
        private int DeviceErrors;
        public int devErrors
        {
            get => DeviceErrors;
            set
            {
                DeviceErrors = value;
                devPercentErrors = SetPercentErrors();
                devStatus = devPercentErrors >= 1.000 ? "Bad" : "Good";
            }
        }
        private double DevPercentErrors;
        public double devPercentErrors
        {
            get => DevPercentErrors;
            set
            {
                DevPercentErrors = value;
                if (DevPercentErrors >= 4.5) statusColor = Color.Red;
                else if (DevPercentErrors >= 4) statusColor = Color.OrangeRed;
                else if (DevPercentErrors >= 3.5) statusColor = Color.Orange;
                else if (DevPercentErrors >= 3) statusColor = Color.Gold;
                else if (DevPercentErrors >= 2.5) statusColor = Color.Yellow;
                else if (DevPercentErrors >= 2) statusColor = Color.GreenYellow;
                else if (DevPercentErrors >= 1.5) statusColor = Color.Lime;
                else if (DevPercentErrors >= 1) statusColor = Color.LightGreen;
                else if (DevPercentErrors >= 0.5) statusColor = Color.PaleTurquoise;
                else statusColor = Color.White;
            }
        }

        private int SetError() => 
            devErrors = 
                DeviceNoReply + 
                DeviceBadCRC + 
                DeviceBadReply + 
                DeviceBadRadio;

        private int DeviceNoReply;
        public int devNoReply
        {
            get => DeviceNoReply;
            set {
                DeviceNoReply = value;
                SetError();
            }
        }

        private int DeviceBadReply;
        public int devBadReply
        {
            get => DeviceBadReply;
            set {
                DeviceBadReply = value;
                SetError();
            }
        }

        private int DeviceBadCRC;
        public int devBadCRC
        {
            get => DeviceBadCRC;
            set {
                DeviceBadCRC = value;
                SetError();
            }
        }

        private int DeviceBadRadio;
        public int devBadRadio
        {
            get => DeviceBadRadio;
            set {
                DeviceBadRadio = value;
                SetError();
            }
        }
        public int devNearbyDevs { get; set; }
        public string devWorkTime { get; set; }
        public int devVer { get; set; }
    }
}

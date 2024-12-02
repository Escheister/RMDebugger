using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using ProtocolEnums;
using RMDebugger;

namespace StaticSettings
{
    static class Options
    {
        public static bool activeProgress = false;
        public static Task activeTask;
        public static CancellationTokenSource activeToken;


        public static DataDebuggerForm debugForm = null;
        public static MainDebugger debugger = null;

        //Socket
        public static bool pingOk { get; set; } = false;

        //interfaces
        public static object mainInterface { get; set; }
        public static bool mainIsAvailable { get; set; } = false;

        public static bool through { get; set; } = false;
        public static bool showMessages {  get; set; }

        //Search
        public static int timeoutSearch { get; set; } = 200;
        public static List<DevType> devTypesSearch = new List<DevType>();


        //Hex uploader
        public static string hexPath { get; set; }
        public static int hexTimeout { get; set; } = 20;
        public static bool checkCrc { get; set; }

        //Config
        public static bool ConfigUploadState { get; set; } = false;
        public static bool ConfigLoadState { get; set; } = false;

        //Info
        public static InformationData infoData = new InformationData();

        //RS485Test
        public static bool RS485ManualScanState { get; set; } = false;

        //Logger
        public static LogState logState { get; set; } = LogState.ERRORState;
        public static LogSize logSize { get; set; } = LogSize.smallBuffer;
    }
}

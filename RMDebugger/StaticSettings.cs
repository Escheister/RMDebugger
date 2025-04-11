using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System;

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
        public static int hexTimeout { get; set; } = 20;
        public static bool checkCrc { get; set; }
        public static bool checkQueue { get; set; }
        public static bool checkFirstMain { get; set; }

        //Info
        public static InformationData infoData = new InformationData();

        //RS485Test
        public static TimeSpan easyTimer { get; set; }
        public static Stopwatch TesterTimer { get; set; } = new Stopwatch();

        //Logger
        public static LogState logState { get; set; } = LogState.ERRORState;
        public static LogSize logSize { get; set; } = LogSize.smallBuffer;
    }
}

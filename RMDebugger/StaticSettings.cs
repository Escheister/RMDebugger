using System.Diagnostics;

using ProtocolEnums;
using RMDebugger;

namespace StaticSettings
{
    static class Options
    {
        public static DataDebuggerForm debugForm = null;
        public static MainDebugger debugger = null;
        public static Stopwatch workTimer;

        //Socket
        public static bool pingOk { get; set; } = false;

        //interfaces
        public static object mainInterface;
        public static bool mainIsAvailable = false;

        public static bool through { get; set; } = false;
        public static bool showMessages {  get; set; }

        //DistTof
        public static bool autoDistTof { get; set; }
        public static int timeoutDistTof { get; set; }

        //GetNear
        public static bool autoGetNear { get; set; }

        public static int timeoutGetNear { get; set; }
        public static string typeOfGetNear { get; set; } = "<Any>";

        //Hex uploader
        public static string hexPath { get; set; }
        public static bool HexUploadState { get; set; } = false;
        public static int hexTimeout { get; set; } = 20;
        public static bool checkCrc { get; set; }

        //Config
        public static bool ConfigUploadState { get; set; } = false;
        public static bool ConfigLoadState { get; set; } = false;
        public static bool RMLRRed { get; set; } = true;
        public static bool RMLRGreen { get; set; } = true;
        public static bool RMLRBlue { get; set; } = true;
        public static bool RMLRBuzzer { get; set; } = true;

        //RS485Test
        public static bool RS485TestState { get; set; } = false;
        public static bool RS485ManualScanState { get; set; } = false;

        //Logger
        public static LogState logState { get; set; } = LogState.ERRORState;
        public static LogSize logSize { get; set; } = LogSize.smallBuffer;
        public static int linesRemove { get; set; } = (int)LogLinesRemove.smallBuffer;
    }
}

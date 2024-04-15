using RMDebugger;

namespace StaticSettings
{
    static class Options
    {
        public static DataDebuggerForm debugForm = null;
        public static MainDebugger debugger = null;
        /// <summary>
        /// Interfaces
        /// </summary>
        //Socket
/*        public static IPAddress iPAddr { get; set; }
        public static ushort uPort { get; set; } = 0;*/
        public static bool pingOk { get; set; } = false;
        //Serial
/*        public static string comPort { get; set; } 
        public static int baudRate { get; set; } = 38400;
        public static byte dataBits { get; set; } = 8;
        public static Parity parityCom { get; set; } = Parity.None;
        public static StopBits stopBits { get; set; } = StopBits.One;*/

        //interfaces
        public static object mainInterface;
        public static bool mainIsAvailable = false;


        /// <summary>
        /// Signature settings
        /// </summary>
        /*        public static bool through {  get; set; } = false;
                public static ushort signatureID { get; set; } = 0;
                public static ushort signatureThrough { get; set; } = 1;*/
        public static bool through { get; set; } = false;

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
        public static int awaitCorrectHexUpload { get; set; } = 20;

        //Config
        public static bool ConfigUploadState { get; set; } = false;
        public static bool ConfigLoadState { get; set; } = false;

        //RS485Test
        public static bool RS485TestState { get; set; } = false;
        public static bool RS485ManualScanState { get; set; } = false;














    }
}

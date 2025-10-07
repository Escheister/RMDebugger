namespace ProtocolEnums
{
    enum DevType : byte
    {
        RM485 = 0x01, RMP = 0x02, RMG = 0x03, RMB = 0x04, RMH = 0x05, 
        RMR = 0x06, ATO = 0x07, RMZ = 0x08, RMTA = 0x09, RMPA = 0x0A, 
        RMIO = 0x10, RMTG = 0x11, VCS = 0x21, VUC = 0x22, MCSP = 0x23,
        MCS = 0x24, TABLET_OR_MRIS = 0x25, BCOM = 0x31, NAP = 0x32,
        SERVER = 0x41, DI8 = 0x50, DI7C = 0x51, NAMUR = 0x52,
        DO8 = 0x53, DO7R = 0x54, AI4 = 0x55, AI4T = 0x56, LTL9 = 0x57,
        NuLL = 0x00
    }
    enum RS485Type : byte
    {
        RM485 = 0x01, RMH = 0x05
    }
    enum ProtocolReply : int
    {
        Ok = 0, 
        Null = 1,
        WCrc = 2,
        WSign = 3,
        WCmd = 4, 
        WData = 5,
        Error = 404,
    }
    enum CmdOutput : ushort // Request
    {
        NONE = 0x0000,
        GRAPH_WHO_ARE_YOU = 0x0101,
        GRAPH_GET_NEAR = 0x0102, 
        ROUTING_GET = 0x0202,
        ROUTING_THROUGH = 0x0210,
        ROUTING_PROG = 0x0211,
        STATUS = 0x0302,
        ONLINE_DIST_TOF = 0x0303,
        SET_CONFIG = 0x0311,
        GET_CONFIG = 0x0312,
        GET_CONFIG_IX = 0x0313,
        RESET = 0x0701,
        SLEEP = 0x0707,
        ONLINE = 0x0921,
        START_BOOTLOADER = 0x1000, 
        LOAD_DATA_PAGE = 0x1003, 
        UPDATE_DATA_PAGE = 0x1005, 
        STOP_BOOTLOADER = 0x1007,
        RMLR_RW_SETTINGS = 0x3005,
        RMLR_REGISTRATION = 0x3001,
        RMLR_RGB = 0x3003,
    }
    enum CmdInput : ushort  // Reply
    {
        NONE = 0x0000,
        GRAPH_WHO_ARE_YOU = 0x8101,
        GRAPH_GET_NEAR = 0x8102, 
        ROUTING_GET = 0x8202,
        ROUTING_THROUGH = 0x8210, 
        ROUTING_PROG = 0x8211,
        STATUS = 0x8302,
        ONLINE_DIST_TOF = 0x8303,
        SET_CONFIG = 0x8311,
        GET_CONFIG = 0x8312,
        GET_CONFIG_IX = 0x8313,
        RESET = 0x8701,
        SLEEP = 0x8707,
        ONLINE = 0x8921,
        START_BOOTLOADER = 0x9002, 
        LOAD_DATA_PAGE = 0x9004,
        UPDATE_DATA_PAGE = 0x9006, 
        STOP_BOOTLOADER = 0x9008,
        RMLR_RW_SETTINGS = 0xB006,
        RMLR_REGISTRATION = 0xB002,
        RMLR_RGB = 0xB004,
    }
    enum CmdMaxSize : int
    {
        ONLINE_DIST_TOF = 69,
        GRAPH_GET_NEAR = 43,
        STATUS = 36,
        GRAPH_WHO_ARE_YOU = 30,
        ONLINE = 7,
        ROUTING_GET = 6,
        RESET = 6,
        SLEEP = 6,
        STOP_BOOTLOADER = 6,
        START_BOOTLOADER = 6,
        RMLR_RW_SETTINGS = 15,
        RMLR_REGISTRATION = 10,
        RMLR_RGB = 6,
    }
    enum RM485_UID : byte
    {
        Who_are_you = 0x01,
        Status = 0x02,
        Mode = 0x03,
    }
    enum RMP_UID : byte
    {
        Who_are_you = 0x01,
        Status = 0x02,
        Mode = 0x04,
    }
    enum RMG_UID : byte
    {
        Who_are_you = 0x01,
        Status = 0x02,
        Who_are_you_ATest = 0x04,
        Status_ATest = 0x05,
        Mode_ATest = 0x07
    }
    enum RMG_SensorType { None, CH4, O2, CO, CO2, H2S, NO, NO2, PID }
    enum RMG_ATestSensorType { None, CH4, O2, CO, CO2, H2S, NO, NO2, Pressure=14, Temperature=15 }
    enum RMH_UID : byte
    {
        Who_are_you = 0x01,
        Status = 0x02,
        Mode = 0x03,
    }
    enum RMTA_UID : byte
    {
        Who_are_you = 0x01,
        Status = 0x02,
    }

    enum FileCheck : int 
    { 
        Ok = 0, 
        BadFile = 1, 
        CmdError = 2, 
        CrcError = 3 
    }
    enum RecordType : int
    {
        /// <summary>
        /// 0x00 - Data Record
        /// </summary>
        Rec = 0,
        /// <summary>
        /// 0x01 - End of File Record
        /// </summary>
        EndRec = 1,
        /// <summary>
        /// 0x02 - Extended Segment Address Record
        /// </summary>
        ESAR = 2,
        /// <summary>
        /// 0x03 - Start Segment Address Record
        /// </summary>
        SSAR = 3,
        /// <summary>
        /// 0x04 - Extended Linear Address Record
        /// </summary>
        ELAR = 4,
        /// <summary>
        /// 0x05 - Start Linear Address Record
        /// </summary>
        SLAR = 5
    }

    enum RS485Columns : int
    { 
        Interface, Sign, Type, Status, Tx, Rx, Errors, 
        PercentErrors, NoReply, BadReply, BadCrc, BadRadio,
        Nearby, WorkTime, Version
    }
    enum RM485MaxSize : int
    {
        GRAPH_WHO_ARE_YOU = 21,
        STATUS = 14,
        ROUTING_GET = 6,
    }
    enum RMHMaxSize : int
    {
        GRAPH_WHO_ARE_YOU = 26,
        STATUS = 14,
        ROUTING_GET = 6,
    }

    enum infoEnum { Type, Signature, Version, Radio, Location, Fio, Date }

    enum ConfigRule { NoRule, uInt16=5, uInt8=3, len4=4, len16=16 }
    enum ConfigColumns { ConfigColumn, enabled, ConfigLoad, ConfigUpload }
    enum SettingsRmlrColumns { RmlrField, RmlrLoad, RmlrUpload }

    enum RmResult : byte
    {
        Error = 0x00,
        Ok = 0x01
    }

    enum LogState { DEBUGState, ERRORState }
    enum LogSize  { lowestBuffer = 256, smallBuffer=512, normalBuffer=1024, mediumBuffer=2048, largeBuffer=4096}
}

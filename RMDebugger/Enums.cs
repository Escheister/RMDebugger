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
        GRAPH_GET_NEAR = 0x0102, 
        ONLINE_DIST_TOF = 0x0303,
        ROUTING_GET = 0x0202,
        ROUTING_PROG = 0x0211,
        START_BOOTLOADER = 0x1000, 
        LOAD_DATA_PAGE = 0x1003, 
        UPDATE_DATA_PAGE = 0x1005, 
        STOP_BOOTLOADER = 0x1007,
        SET_CONFIG = 0x0311,
        GET_CONFIG_FIELD = 0x0312,
        ROUTING_THROUGH = 0x0210,
        ONLINE = 0x0921,
        WHO_ARE_YOU = 0x0101,
        RESET = 0x0701,
        STATUS = 0x0302,
    }
    enum CmdInput : ushort  // Reply
    {
        GRAPH_GET_NEAR = 0x8102, 
        ONLINE_DIST_TOF = 0x8303,
        ROUTING_GET = 0x8202,
        ROUTING_THROUGH = 0x8210, 
        ROUTING_PROG = 0x8211,
        START_BOOTLOADER = 0x9002, 
        LOAD_DATA_PAGE = 0x9004,
        UPDATE_DATA_PAGE = 0x9006, 
        STOP_BOOTLOADER = 0x9008,
        SET_CONFIG = 0x8311,
        GET_CONFIG_FIELD = 0x8312,
        THROUGH = 0x8210,
        ONLINE = 0x8921,
        WHO_ARE_YOU = 0x8101,
        RESET = 0x8701,
        STATUS = 0x8302,
    }
    enum dataSize : int { ONLINE_DIST_TOF = 5, GRAPH_GET_NEAR = 3, }
    enum cmdSize : int { ONLINE_DIST_TOF = 9, GRAPH_GET_NEAR = 7, }
    enum CmdMaxSize : int
    {
        WHO_ARE_YOU = 30,
        GRAPH_GET_NEAR = 43,
        ONLINE_DIST_TOF = 69,
        ONLINE = 7,
        RESET = 6,
        STATUS = 32,
        ROUTING_GET = 6,
    }
    enum UID : byte { WHO_ARE_YOU = 0x01, STATUS = 0x02, }

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
        WorkTime, Version
    }
    enum RM485MaxSize : int
    {
        WHO_ARE_YOU = 21,
        STATUS = 14,
        ROUTING_GET = 6,
    }
    enum RMHMaxSize : int
    {
        WHO_ARE_YOU = 26,
        STATUS = 14,
        ROUTING_GET = 6,
    }

    enum InfoGrid { Signature, Type, Version, Radio, Location, Fio, Date }

    enum ConfigCheckList { None, uInt16=5, len4=4, len16=16 }
    enum ConfigColumns { ConfigColumn, enabled, ConfigLoad, ConfigUpload }

    enum RmResult : byte
    {
        Error = 0x00,
        Ok = 0x01
    }
}

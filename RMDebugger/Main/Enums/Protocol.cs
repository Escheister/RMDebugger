namespace Enums
{
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
        Null = 0x0000,
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
        Null = 0x0000,
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
}

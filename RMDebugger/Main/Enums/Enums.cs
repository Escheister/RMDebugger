namespace Enums
{

    enum RS485Type : byte
    {
        RM485 = 0x01,
        RMH = 0x05
    }

    enum RMG_SensorType { None, CH4, O2, CO, CO2, H2S, NO, NO2, PID }
    enum RMG_ATestSensorType { None, CH4, O2, CO, CO2, H2S, NO, NO2, Pressure = 14, Temperature = 15 }


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

    enum ConfigRule { NoRule, uInt16 = 5, uInt8 = 3, len4 = 4, len16 = 16 }
    enum ConfigColumns { ConfigColumn, enabled, ConfigLoad, ConfigUpload }
    enum SettingsRmlrColumns { RmlrField, RmlrLoad, RmlrUpload }

    enum RmResult : byte
    {
        Error = 0x00,
        Ok = 0x01
    }

    enum LogState { DEBUGState, ERRORState }
    enum LogSize { lowestBuffer = 256, smallBuffer = 512, normalBuffer = 1024, mediumBuffer = 2048, largeBuffer = 4096 }
}
